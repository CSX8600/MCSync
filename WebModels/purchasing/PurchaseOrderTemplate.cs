using ClussPro.Base.Data.Query;
using ClussPro.Base.Data;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.Schema.Attributes;
using ClussPro.ObjectBasedFramework.Validation.Attributes;
using System.Threading.Tasks;
using WebModels.company;
using WebModels.gov;
using System.Linq;
using ClussPro.ObjectBasedFramework.DataSearch;
using System.Collections.Generic;
using ClussPro.ObjectBasedFramework.Utility;
using WebModels.fleet;

namespace WebModels.purchasing
{
    [Table("47BC90AA-598A-4E48-931D-7796C72E91B5")]
    public class PurchaseOrderTemplate : DataObject
    {
        protected PurchaseOrderTemplate() : base() { }

        private long? _purchaseOrderTemplateID;
        [Field("A6FD264B-AFD0-482C-9532-34318B733AAF")]
        public long? PurchaseOrderTemplateID
        {
            get { CheckGet(); return _purchaseOrderTemplateID; }
            set { CheckSet(); _purchaseOrderTemplateID = value; }
        }

        private long? _locationID;
        [Field("5D0D96B1-972E-4D46-861E-1378A2A782C6")]
        public long? LocationID
        {
            get { CheckGet(); return _locationID; }
            set { CheckSet(); _locationID = value; }
        }

        private Location _location = null;
        [Relationship("751395DE-8D3E-46DD-9674-15A23AD65F80")]
        public Location Location
        {
            get { CheckGet(); return _location; }
            set { CheckSet(); _location = value; }
        }

        private long? _governmentID;
        [Field("4760E621-E3E7-4BC8-BF5B-20B7D077F286")]
        public long? GovernmentID
        {
            get { CheckGet(); return _governmentID; }
            set { CheckSet(); _governmentID = value; }
        }

        private Government _government = null;
        [Relationship("FF053568-7604-4931-85B4-C6D937F08A13")]
        public Government Government
        {
            get { CheckGet(); return _government; }
            set { CheckSet(); _government = value; }
        }

        private long? _purchaseOrderTemplateFolderID;
        [Field("D1228BCD-0A08-4BE7-B4CC-A67E0744A0DA")]
        public long? PurchaseOrderTemplateFolderID
        {
            get { CheckGet(); return _purchaseOrderTemplateFolderID; }
            set { CheckSet(); _purchaseOrderTemplateFolderID = value; }
        }

        private PurchaseOrderTemplateFolder _purchaseOrderTemplateFolder = null;
        [Relationship("3FC5226A-41F6-46E4-B0DB-373FAB38C3EA")]
        public PurchaseOrderTemplateFolder PurchaseOrderTemplateFolder
        {
            get { CheckGet(); return _purchaseOrderTemplateFolder; }
            set { CheckSet(); _purchaseOrderTemplateFolder = value; }
        }

        private long? _purchaseOrderID;
        [Field("D5F2368B-A49D-483B-82A7-ADCFA7FEB212")]
        [Required]
        public long? PurchaseOrderID
        {
            get { CheckGet(); return _purchaseOrderID; }
            set { CheckSet(); _purchaseOrderID = value; }
        }

        private PurchaseOrder _purchaseOrder = null;
        [Relationship("FD206D30-80BA-423D-9A60-FAC57E4D8176")]
        public PurchaseOrder PurchaseOrder
        {
            get { CheckGet(); return _purchaseOrder; }
            set { CheckSet(); _purchaseOrder = value; }
        }

        private string _name;
        [Field("22417948-32F8-4D2E-AD7A-8D537C65BFE8", DataSize = 255)]
        [Required]
        public string Name
        {
            get { CheckGet(); return _name; }
            set { CheckSet(); _name = value; }
        }

        public async Task<long?> CreateClonedPurchaseOrder()
        {
            if (!IsPathRetrieved(nameof(PurchaseOrderID)))
            {
                return null;
            }

            PurchaseOrder originalPurchaseOrder = await Task.Run(() => DataObject.GetEditableByPrimaryKey<PurchaseOrder>(PurchaseOrderID, null, FieldPathUtility.CreateFieldPathsAsList<PurchaseOrder>(po => new object[]
            {
                po.LocationOrigin.CompanyID,
                po.GovernmentIDOrigin
            })));

            if (originalPurchaseOrder == null)
            {
                return null;
            }

            using (ITransaction transaction = SQLProviderFactory.GenerateTransaction())
            {
                PurchaseOrder newPurchaseOrder = DataObjectFactory.Create<PurchaseOrder>();
                originalPurchaseOrder.Copy(newPurchaseOrder);
                newPurchaseOrder.PurchaseOrderIDClonedFrom = originalPurchaseOrder.PurchaseOrderID;
                newPurchaseOrder.Status = PurchaseOrder.Statuses.Draft;
                newPurchaseOrder.PurchaseOrderDate = null;
                
                if (!await Task.Run(() => newPurchaseOrder.Save(transaction)))
                {
                    Errors.AddRange(newPurchaseOrder.Errors.ToArray());
                    return null;
                }

                Search<PurchaseOrderLine> purchaseOrderLineSearch = new Search<PurchaseOrderLine>(new LongSearchCondition<PurchaseOrderLine>()
                {
                    Field = nameof(PurchaseOrderLine.PurchaseOrderID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = originalPurchaseOrder.PurchaseOrderID
                });

                Dictionary<long?, long?> newPurchaseOrderLineMap = new Dictionary<long?, long?>();
                foreach (PurchaseOrderLine purchaseOrderLine in purchaseOrderLineSearch.GetEditableReader(transaction))
                {
                    PurchaseOrderLine newPurchaseOrderLine = DataObjectFactory.Create<PurchaseOrderLine>();
                    purchaseOrderLine.Copy(newPurchaseOrderLine);
                    newPurchaseOrderLine.PurchaseOrderID = newPurchaseOrder.PurchaseOrderID;
                    if (!await Task.Run(() => newPurchaseOrderLine.Save(transaction)))
                    {
                        Errors.AddRange(newPurchaseOrderLine.Errors.ToArray());
                        return null;
                    }

                    newPurchaseOrderLineMap.Add(purchaseOrderLine.PurchaseOrderLineID, newPurchaseOrderLine.PurchaseOrderLineID);
                }

                Search<FulfillmentPlan> fulfillmentPlanSearch = new Search<FulfillmentPlan>(new ExistsSearchCondition<FulfillmentPlan>()
                {
                    RelationshipName = nameof(FulfillmentPlan.FulfillmentPlanPurchaseOrderLines),
                    ExistsType = ExistsSearchCondition<FulfillmentPlan>.ExistsTypes.Exists,
                    Condition = new LongSearchCondition<FulfillmentPlanPurchaseOrderLine>()
                    {
                        Field = FieldPathUtility.CreateFieldPath<FulfillmentPlanPurchaseOrderLine>(fppol => fppol.PurchaseOrderLine.PurchaseOrderID),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = originalPurchaseOrder.PurchaseOrderID
                    }
                });

                Dictionary<long?, long?> fulfillmentPlanMap = new Dictionary<long?, long?>();
                foreach (FulfillmentPlan fulfillmentPlan in fulfillmentPlanSearch.GetEditableReader(transaction))
                {
                    FulfillmentPlan newFulfillmentPlan = DataObjectFactory.Create<FulfillmentPlan>();
                    fulfillmentPlan.Copy(newFulfillmentPlan);
                    newFulfillmentPlan.LeaseRequestID = null;

                    // Check to see if railcar is available for this purpose
                    Railcar railcar = DataObject.GetReadOnlyByPrimaryKey<Railcar>(newFulfillmentPlan.RailcarID, transaction, FieldPathUtility.CreateFieldPathsAsList<Railcar>(r => new object[]
                    {
                        r.CompanyIDOwner,
                        r.GovernmentIDOwner,
                        r.CompanyLeasedTo.CompanyID,
                        r.GovernmentLeasedTo.GovernmentID,
                        r.FulfillmentPlans.First().FulfillmentPlanPurchaseOrderLines.First().PurchaseOrderLine.PurchaseOrder.Status
                    }));

                    if (railcar == null)
                    {
                        newFulfillmentPlan.RailcarID = null;
                    }
                    else if (railcar.FulfillmentPlans.Any(fp => fp.FulfillmentPlanPurchaseOrderLines.Any(fppol => fppol.PurchaseOrderLine.PurchaseOrder.Status != PurchaseOrder.Statuses.Completed)))
                    {
                        newFulfillmentPlan.RailcarID = null;
                    }
                    else if (originalPurchaseOrder.LocationOrigin?.CompanyID != null)
                    {
                        if ((railcar.CompanyIDOwner != originalPurchaseOrder.LocationOrigin.CompanyID && railcar.CompanyLeasedTo?.CompanyID != originalPurchaseOrder.LocationOrigin.CompanyID) ||
                            (railcar.CompanyIDOwner == originalPurchaseOrder.LocationOrigin.CompanyID && (railcar.CompanyLeasedTo?.CompanyID != null || railcar.GovernmentLeasedTo?.GovernmentID != null)))
                        {
                            // Car is not owned by the origin company and is not leased to the origin company OR
                            // Car is owned by the origin company and is leased to another entity
                            newFulfillmentPlan.RailcarID = null;
                        }
                    }
                    else if (originalPurchaseOrder.GovernmentIDOrigin != null)
                    {
                        if ((railcar.GovernmentIDOwner != originalPurchaseOrder.GovernmentIDOrigin && railcar.GovernmentLeasedTo?.GovernmentID != originalPurchaseOrder.GovernmentIDOrigin) ||
                            (railcar.GovernmentIDOwner == originalPurchaseOrder.GovernmentIDOrigin && (railcar.CompanyLeasedTo?.CompanyID != null || railcar.GovernmentLeasedTo?.GovernmentID != null)))
                        {
                            // Car is not owned by the origin government and is not leased to the origin government OR
                            // Car is owned by the origin government and is leased to another entity
                            newFulfillmentPlan.RailcarID = null;
                        }
                    }

                    if (!await Task.Run(() => newFulfillmentPlan.Save(transaction, new List<System.Guid>() { FulfillmentPlan.SaveFlags.V_RailcarIsIdle })))
                    {
                        Errors.AddRange(newFulfillmentPlan.Errors.ToArray());
                        return null;
                    }

                    fulfillmentPlanMap.Add(fulfillmentPlan.FulfillmentPlanID, newFulfillmentPlan.FulfillmentPlanID);
                }

                Search<FulfillmentPlanPurchaseOrderLine> search = new Search<FulfillmentPlanPurchaseOrderLine>(new LongSearchCondition<FulfillmentPlanPurchaseOrderLine>()
                {
                    Field = FieldPathUtility.CreateFieldPath<FulfillmentPlanPurchaseOrderLine>(fpol => fpol.PurchaseOrderLine.PurchaseOrderID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = originalPurchaseOrder.PurchaseOrderID
                });

                foreach(FulfillmentPlanPurchaseOrderLine fulfillmentPlanPurchaseOrderLine in search.GetEditableReader(transaction))
                {
                    if (!newPurchaseOrderLineMap.ContainsKey(fulfillmentPlanPurchaseOrderLine.PurchaseOrderLineID) ||
                        !fulfillmentPlanMap.ContainsKey(fulfillmentPlanPurchaseOrderLine.FulfillmentPlanID))
                    {
                        continue;
                    }

                    FulfillmentPlanPurchaseOrderLine newJoin = DataObjectFactory.Create<FulfillmentPlanPurchaseOrderLine>();
                    newJoin.FulfillmentPlanID = fulfillmentPlanMap[fulfillmentPlanPurchaseOrderLine.FulfillmentPlanID];
                    newJoin.PurchaseOrderLineID = newPurchaseOrderLineMap[fulfillmentPlanPurchaseOrderLine.PurchaseOrderLineID];

                    if (!await Task.Run(() => newJoin.Save(transaction, new List<System.Guid>() { FulfillmentPlanPurchaseOrderLine.SaveFlags.SkipClearTemplateDataForPurchaseOrder })))
                    {
                        Errors.AddRange(newJoin.Errors.ToArray());
                        return null;
                    }
                }

                Search<FulfillmentPlanRoute> routeSearch = new Search<FulfillmentPlanRoute>(new ExistsSearchCondition<FulfillmentPlanRoute>()
                {
                    RelationshipName = FieldPathUtility.CreateFieldPath<FulfillmentPlanRoute>(fpr => fpr.FulfillmentPlan.FulfillmentPlanPurchaseOrderLines),
                    ExistsType = ExistsSearchCondition<FulfillmentPlanRoute>.ExistsTypes.Exists,
                    Condition = new LongSearchCondition<FulfillmentPlanPurchaseOrderLine>()
                    {
                        Field = FieldPathUtility.CreateFieldPath<FulfillmentPlanPurchaseOrderLine>(fppol => fppol.PurchaseOrderLine.PurchaseOrderID),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = originalPurchaseOrder.PurchaseOrderID
                    }
                });

                foreach (FulfillmentPlanRoute route in routeSearch.GetEditableReader(transaction))
                {
                    if (!fulfillmentPlanMap.ContainsKey(route.FulfillmentPlanID))
                    {
                        continue;
                    }

                    FulfillmentPlanRoute newRoute = DataObjectFactory.Create<FulfillmentPlanRoute>();
                    route.Copy(newRoute);
                    newRoute.FulfillmentPlanID = fulfillmentPlanMap[route.FulfillmentPlanID];
                    if (!await Task.Run(() => newRoute.Save(transaction, new List<System.Guid>() { FulfillmentPlanRoute.SaveFlags.SkipClearTemplateDataForPurchaseOrder })))
                    {
                        Errors.AddRange(newRoute.Errors.ToArray());
                        return null;
                    }
                }

                transaction.Commit();

                return newPurchaseOrder.PurchaseOrderID;
            }
        }
    }
}
