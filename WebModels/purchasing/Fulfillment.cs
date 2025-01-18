using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Schema.Attributes;
using ClussPro.ObjectBasedFramework.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModels.fleet;
using WebModels.invoicing;

namespace WebModels.purchasing
{
    [Table("69A58C83-F71A-4D03-9014-66AC9F44F431")]
    public class Fulfillment : DataObject
    {
        protected Fulfillment() : base() { }

        private long? _fulfillmentID;
        [Field("BE38AD1A-D597-410B-A648-1A045A61BE8F")]
        public long? FulfillmentID
        {
            get { CheckGet(); return _fulfillmentID; }
            set { CheckSet(); _fulfillmentID = value; }
        }

        private long? _purchaseOrderLineID;
        [Field("FD061D55-3DC5-4971-9378-F574DF9379B4")]
        public long? PurchaseOrderLineID
        {
            get { CheckGet(); return _purchaseOrderLineID; }
            set { CheckSet(); _purchaseOrderLineID = value; }
        }

        private PurchaseOrderLine _purchaseOrderLine = null;
        [Relationship("B4C4F29A-5AAC-4764-B44E-4423FDE6F274")]
        public PurchaseOrderLine PurchaseOrderLine
        {
            get { CheckGet(); return _purchaseOrderLine; }
        }

        private long? _railcarID;
        [Field("5DF67988-505F-4C64-9033-B73DA9DA88CC")]
        public long? RailcarID
        {
            get { CheckGet(); return _railcarID; }
            set { CheckSet(); _railcarID = value; }
        }

        private Railcar _railcar = null;
        [Relationship("FF26162A-4B88-4A84-86C6-2835CDE7722D")]
        public Railcar Railcar
        {
            get { CheckGet(); return _railcar; }
        }

        private DateTime? _fulfillmentTime;
        [Field("1BEB679A-F97C-49B2-ADF7-6EECD4CB5BE5", DataSize = 7)]
        public DateTime? FulfillmentTime
        {
            get { CheckGet(); return _fulfillmentTime; }
            set { CheckSet(); _fulfillmentTime = value; }
        }

        private decimal? _quantity;
        [Field("13E1F961-EDD3-467F-A43E-FDEB38D116DD", DataSize = 9, DataScale = 2)]
        public decimal? Quantity
        {
            get { CheckGet(); return _quantity; }
            set { CheckSet(); _quantity = value; }
        }

        private bool _isComplete;
        [Field("8DCC6724-0CD0-4FDA-904D-6B0373AD776F")]
        public bool IsComplete
        {
            get { CheckGet(); return _isComplete; }
            set { CheckSet(); _isComplete = value; }
        }

        private long? _invoiceLineID;
        [Field("341BB27D-7FF6-4280-A610-0401C0458885")]
        public long? InvoiceLineID
        {
            get { CheckGet(); return _invoiceLineID; }
            set { CheckSet(); _invoiceLineID = value; }
        }

        private InvoiceLine _invoiceLine = null;
        [Relationship("8EDD6E39-D3EE-48B2-8BA0-E885484B5658")]
        public InvoiceLine InvoiceLine
        {
            get { CheckGet(); return _invoiceLine; }
        }

        protected override bool PostSave(ITransaction transaction)
        {
            if (IsInsert && RailcarID != null)
            {
                PurchaseOrderLine parentPOLine = DataObject.GetReadOnlyByPrimaryKey<PurchaseOrderLine>(PurchaseOrderLineID, transaction, FieldPathUtility.CreateFieldPathsAsList<PurchaseOrderLine>(pol => new object[]
                {
                    pol.PurchaseOrder.LocationDestination.CompanyID,
                    pol.PurchaseOrder.GovernmentIDDestination
                }));

                // Automation for railcar track and route assignments
                Search<FulfillmentPlan> fulfillmentPlanSearch = new Search<FulfillmentPlan>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                    new LongSearchCondition<FulfillmentPlan>()
                    {
                        Field = nameof(FulfillmentPlan.RailcarID),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = RailcarID
                    },
                    new ExistsSearchCondition<FulfillmentPlan>()
                    {
                        RelationshipName = nameof(FulfillmentPlan.FulfillmentPlanPurchaseOrderLines),
                        ExistsType = ExistsSearchCondition<FulfillmentPlan>.ExistsTypes.Exists,
                        Condition = new LongSearchCondition<FulfillmentPlanPurchaseOrderLine>()
                        {
                            Field = nameof(FulfillmentPlanPurchaseOrderLine.PurchaseOrderLineID),
                            SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                            Value = PurchaseOrderLineID
                        }
                    }));

                List<string> fields = FieldPathUtility.CreateFieldPathsAsList<FulfillmentPlan>(fp => new List<object>()
                {
                    fp.Railcar.RailLocation.Track.CompanyIDOwner,
                    fp.Railcar.RailLocation.Track.GovernmentIDOwner,
                    fp.Railcar.ReportingMark,
                    fp.Railcar.ReportingNumber,
                    fp.TrackIDDestination,
                    fp.TrackIDStrategicAfterLoad,
                    fp.FulfillmentPlanRoutes.First().SortOrder,
                    fp.FulfillmentPlanRoutes.First().CompanyIDFrom,
                    fp.FulfillmentPlanRoutes.First().CompanyIDTo,
                    fp.FulfillmentPlanRoutes.First().GovernmentIDFrom,
                    fp.FulfillmentPlanRoutes.First().GovernmentIDTo
                });
                FulfillmentPlan plan = fulfillmentPlanSearch.GetReadOnly(transaction, fields);
                if (plan != null && parentPOLine != null &&
                    (plan?.Railcar?.RailLocation?.Track?.CompanyIDOwner == parentPOLine?.PurchaseOrder?.LocationDestination?.CompanyID ||
                    plan?.Railcar?.RailLocation?.Track?.GovernmentIDOwner == parentPOLine?.PurchaseOrder?.GovernmentIDDestination))
                {
                    Railcar railcar = DataObject.GetEditableByPrimaryKey<Railcar>(RailcarID, transaction, null);
                    railcar.TrackIDDestination = plan.TrackIDDestination;
                    railcar.TrackIDStrategic = plan.TrackIDStrategicAfterLoad;
                    if (!railcar.Save(transaction))
                    {
                        Errors.AddRange(railcar.Errors.ToArray());
                        return false;
                    }

                    Search<RailcarRoute> railcarRouteSearch = new Search<RailcarRoute>(new LongSearchCondition<RailcarRoute>()
                    {
                        Field = nameof(RailcarRoute.RailcarID),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = RailcarID
                    });
                    foreach(RailcarRoute railcarRoute in railcarRouteSearch.GetEditableReader(transaction))
                    {
                        if (!railcarRoute.Delete(transaction))
                        {
                            Errors.AddRange(railcarRoute.Errors.ToArray());
                            return false;
                        }
                    }

                    foreach(FulfillmentPlanRoute planRoute in (plan.FulfillmentPlanRoutes ?? new List<FulfillmentPlanRoute>()).OrderBy(rr => rr.SortOrder))
                    {
                        RailcarRoute railcarRoute = DataObjectFactory.Create<RailcarRoute>();
                        railcarRoute.RailcarID = RailcarID;
                        railcarRoute.CompanyIDFrom = planRoute.CompanyIDFrom;
                        railcarRoute.CompanyIDTo = planRoute.CompanyIDTo;
                        railcarRoute.GovernmentIDFrom = planRoute.GovernmentIDFrom;
                        railcarRoute.GovernmentIDTo = planRoute.GovernmentIDTo;
                        railcarRoute.SortOrder = planRoute.SortOrder;
                        if (!railcarRoute.Save(transaction))
                        {
                            Errors.AddRange(railcarRoute.Errors.ToArray());
                            return false;
                        }
                    }

                    // Add fulfillment load to railcar, if it hasn't been done already
                    Search<RailcarLoad> railcarLoadExistsSearch = new Search<RailcarLoad>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                        new LongSearchCondition<RailcarLoad>()
                        {
                            Field = nameof(RailcarLoad.RailcarID),
                            SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                            Value = RailcarID
                        },
                        new LongSearchCondition<RailcarLoad>()
                        {
                            Field = nameof(RailcarLoad.PurchaseOrderLineID),
                            SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                            Value = PurchaseOrderLineID
                        }));

                    if (!railcarLoadExistsSearch.ExecuteExists(transaction))
                    {
                        PurchaseOrderLine purchaseOrderLine = DataObject.GetReadOnlyByPrimaryKey<PurchaseOrderLine>(PurchaseOrderLineID, transaction, new[] { nameof(PurchaseOrderLine.ItemID) });
                        if (purchaseOrderLine != null && purchaseOrderLine.ItemID != null)
                        {
                            RailcarLoad railcarLoad = DataObjectFactory.Create<RailcarLoad>();
                            railcarLoad.RailcarID = RailcarID;
                            railcarLoad.Quantity = Quantity;
                            railcarLoad.ItemID = purchaseOrderLine.ItemID;
                            railcarLoad.PurchaseOrderLineID = PurchaseOrderLineID;
                            if (!railcarLoad.Save(transaction))
                            {
                                Errors.AddRange(railcarLoad.Errors.ToArray());
                                return false;
                            }
                        }
                    }
                }
            }

            if (IsInsert)
            {
                // Update Purchase Order status, if necessary
                Search<PurchaseOrder> purchaseOrderSearch = new Search<PurchaseOrder>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                    new ExistsSearchCondition<PurchaseOrder>()
                    {
                        RelationshipName = nameof(PurchaseOrder.PurchaseOrderLines),
                        ExistsType = ExistsSearchCondition<PurchaseOrder>.ExistsTypes.Exists,
                        Condition = new LongSearchCondition<PurchaseOrderLine>()
                        {
                            Field = nameof(PurchaseOrderLine.PurchaseOrderLineID),
                            SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                            Value = PurchaseOrderLineID
                        }
                    },
                    new IntSearchCondition<PurchaseOrder>()
                    {
                        Field = nameof(PurchaseOrder.Status),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = (int)PurchaseOrder.Statuses.Accepted
                    }));

                PurchaseOrder purchaseOrder = purchaseOrderSearch.GetEditable(transaction, FieldPathUtility.CreateFieldPathsAsList<PurchaseOrder>(po => new object[] 
                { 
                    po.LocationDestination.InvoiceNumberPrefix,
                    po.LocationDestination.NextInvoiceNumber
                }));
                if (purchaseOrder != null)
                {
                    purchaseOrder.Status = PurchaseOrder.Statuses.InProgress;
                    if (!purchaseOrder.Save(transaction, new List<Guid>() { PurchaseOrder.SaveFlags.V_StatusChange }))
                    {
                        Errors.AddRange(purchaseOrder.Errors.ToArray());
                        return false;
                    }
                }
            }

            if ((IsInsert || IsFieldDirty(nameof(IsComplete))) && IsComplete)
            {
                PurchaseOrderLine purchaseOrderLine = DataObject.GetReadOnlyByPrimaryKey<PurchaseOrderLine>(PurchaseOrderLineID, transaction, new[] { nameof(PurchaseOrderLine.PurchaseOrderID) });
                PurchaseOrder purchaseOrder = DataObject.GetEditableByPrimaryKey<PurchaseOrder>(purchaseOrderLine.PurchaseOrderID, transaction, FieldPathUtility.CreateFieldPathsAsList<PurchaseOrder>(po => new List<object>() { po.PurchaseOrderLines.First().RemainingQuantity }));
                if (purchaseOrder.PurchaseOrderLines.All(pol => (pol.RemainingQuantity ?? 0) == 0))
                {
                    purchaseOrder.Status = PurchaseOrder.Statuses.Completed;
                    if (!purchaseOrder.Save(transaction, new List<Guid>() { PurchaseOrder.SaveFlags.V_StatusChange }))
                    {
                        Errors.AddRange(purchaseOrder.Errors.ToArray());
                        return false;
                    }
                }
            }

            return base.PostSave(transaction);
        }
    }
}
