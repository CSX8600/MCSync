using ClussPro.Base.Data;
using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Schema.Attributes;
using ClussPro.ObjectBasedFramework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebModels.fleet;

namespace WebModels.purchasing
{
    [Table("7B96D263-EE48-44DB-8754-B0BDE09051C0")]
    public class FulfillmentPlan : DataObject
    {
        protected FulfillmentPlan() : base() { }

        private long? _fulfillmentPlanID;
        [Field("42E4EA64-2594-4C85-82D1-B677409ACDD4")]
        public long? FulfillmentPlanID
        {
            get { CheckGet(); return _fulfillmentPlanID; }
            set { CheckSet(); _fulfillmentPlanID = value; }
        }

        private long? _railcarID;
        [Field("09B2BB59-5D64-48D3-92F3-CA8460DFE6BF")]
        public long? RailcarID
        {
            get { CheckGet(); return _railcarID; }
            set { CheckSet(); _railcarID = value; }
        }

        private Railcar _railcar = null;
        [Relationship("A9AAC640-EFAA-456C-84A6-A99ADD8C5A07")]
        public Railcar Railcar
        {
            get { CheckGet(); return _railcar; }
        }

        private long? _leaseRequestID;
        [Field("57AA17B1-9CD1-4029-8E25-67C24B209CCA")]
        public long? LeaseRequestID
        {
            get { CheckGet(); return _leaseRequestID; }
            set { CheckSet(); _leaseRequestID = value; }
        }

        private LeaseRequest _leaseRequest = null;
        [Relationship("C45945A9-A8F7-4694-A252-B4EA0A4A5ABB")]
        public LeaseRequest LeaseRequest
        {
            get { CheckGet(); return _leaseRequest; }
        }

        private long? _trackIDLoading;
        [Field("C12B06F2-4A72-4B21-A5D7-49C831C7296F")]
        public long? TrackIDLoading
        {
            get { CheckGet(); return _trackIDLoading; }
            set { CheckSet(); _trackIDLoading = value; }
        }

        private Track _trackLoading = null;
        [Relationship("A9F75A81-9E4D-4AA4-B3F4-4FF75E9F5A3B", ForeignKeyField = nameof(TrackIDLoading))]
        public Track TrackLoading
        {
            get { CheckGet(); return _trackLoading; }
        }

        private long? _trackIDDestination;
        [Field("E3B16B71-899B-4F92-8C9C-2D6A7E7E9A8F")]
        public long? TrackIDDestination
        {
            get { CheckGet(); return _trackIDDestination; }
            set { CheckSet(); _trackIDDestination = value; }
        }

        private Track _trackDestination = null;
        [Relationship("FF68AB12-AD3C-4CF9-9FB4-71C4F56A7D91", ForeignKeyField = nameof(TrackIDDestination))]
        public Track TrackDestination
        {
            get { CheckGet(); return _trackDestination; }
        }

        private long? _trackIDStrategicAfterLoad;
        [Field("69B99F3D-8E5B-4F69-A4E1-DA4B01736C27")]
        public long? TrackIDStrategicAfterLoad
        {
            get { CheckGet(); return _trackIDStrategicAfterLoad; }
            set { CheckSet(); _trackIDStrategicAfterLoad = value; }
        }

        private Track _trackStrategicAfterLoad = null;
        [Relationship("DD54C7F4-2A4D-493D-AB2C-BE33F5D7B42F", ForeignKeyField = nameof(TrackIDStrategicAfterLoad))]
        public Track TrackStrategicAfterLoad
        {
            get { CheckGet(); return _trackStrategicAfterLoad; }
        }

        private long? _trackIDStrategicAfterDestination;
        [Field("8434D78E-A526-4CD8-9147-2E62BD4B7C98")]
        public long? TrackIDStrategicAfterDestination
        {
            get { CheckGet(); return _trackIDStrategicAfterDestination; }
            set { CheckSet(); _trackIDStrategicAfterDestination = value; }
        }

        private Track _trackStrategicAfterDestination = null;
        [Relationship("8427A8E1-D172-4106-B4EE-DDB2C69F7A83", ForeignKeyField = nameof(TrackIDStrategicAfterDestination))]
        public Track TrackStrategicAfterDestination
        {
            get { CheckGet(); return _trackStrategicAfterDestination; }
        }

        private long? _trackIDPostFulfillment;
        [Field("B7CDE312-BC4A-4EBF-8B84-C18B5C3A88DA")]
        public long? TrackIDPostFulfillment
        {
            get { CheckGet(); return _trackIDPostFulfillment; }
            set { CheckSet(); _trackIDPostFulfillment = value; }
        }

        private Track _trackPostFulfillment = null;
        [Relationship("8CB6B826-5E31-4F39-9A45-4587E6A7F47D", ForeignKeyField = nameof(TrackIDPostFulfillment))]
        public Track TrackPostFulfillment
        {
            get { CheckGet(); return _trackPostFulfillment; }
        }

        protected override bool PreSave(ITransaction transaction)
        {
            if (!IsInsert && HasSignificantChanges())
            {
                Search<PurchaseOrder> purchaseOrderSearch = new Search<PurchaseOrder>(new ExistsSearchCondition<PurchaseOrder>()
                {
                    RelationshipName = nameof(PurchaseOrder.PurchaseOrderLines),
                    ExistsType = ExistsSearchCondition<PurchaseOrder>.ExistsTypes.Exists,
                    Condition = new ExistsSearchCondition<PurchaseOrderLine>()
                    {
                        RelationshipName = nameof(PurchaseOrderLine.FulfillmentPlanPurchaseOrderLines),
                        ExistsType = ExistsSearchCondition<PurchaseOrderLine>.ExistsTypes.Exists,
                        Condition = new LongSearchCondition<FulfillmentPlanPurchaseOrderLine>()
                        {
                            Field = nameof(FulfillmentPlanPurchaseOrderLine.FulfillmentPlanID),
                            SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                            Value = FulfillmentPlanID
                        }
                    }
                });

                foreach(PurchaseOrder purchaseOrder in purchaseOrderSearch.GetEditableReader(transaction))
                {
                    if (!purchaseOrder.ClearTemplateDataForPurchaseOrder(transaction) || !purchaseOrder.Save(transaction))
                    {
                        Errors.AddRange(purchaseOrder.Errors.ToArray());
                        return false;
                    }
                }
            }

            return base.PreSave(transaction);
        }

        protected override bool PostSave(ITransaction transaction)
        {
            if (!IsInsert && IsFieldDirty(nameof(RailcarID)) && RailcarID != null)
            {
                Search<PurchaseOrder> purchaseOrderSearch = new Search<PurchaseOrder>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                    new IntSearchCondition<PurchaseOrder>()
                    {
                        Field = nameof(PurchaseOrder.Status),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = (int)PurchaseOrder.Statuses.Pending
                    },
                    new ExistsSearchCondition<PurchaseOrder>()
                    {
                        RelationshipName = nameof(PurchaseOrder.PurchaseOrderLines),
                        ExistsType = ExistsSearchCondition<PurchaseOrder>.ExistsTypes.Exists,
                        Condition = new ExistsSearchCondition<PurchaseOrderLine>()
                        {
                            RelationshipName = nameof(PurchaseOrderLine.FulfillmentPlanPurchaseOrderLines),
                            ExistsType = ExistsSearchCondition<PurchaseOrderLine>.ExistsTypes.Exists,
                            Condition = new LongSearchCondition<FulfillmentPlanPurchaseOrderLine>()
                            {
                                Field = nameof(FulfillmentPlanPurchaseOrderLine.FulfillmentPlanID),
                                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                Value = FulfillmentPlanID
                            }
                        }
                    }));

                foreach (PurchaseOrder purchaseOrder in purchaseOrderSearch.GetEditableReader(transaction))
                {
                    if (!Task.Run(() => purchaseOrder.ApprovalSubmitted(transaction)).Result)
                    {
                        Errors.AddRange(purchaseOrder.Errors.ToArray());
                        return false;
                    }
                }
            }

            return base.PostSave(transaction);
        }

        protected override bool PreDelete(ITransaction transaction)
        {
            Search<PurchaseOrder> purchaseOrderSearch = new Search<PurchaseOrder>(new ExistsSearchCondition<PurchaseOrder>()
            {
                RelationshipName = nameof(PurchaseOrder.PurchaseOrderLines),
                ExistsType = ExistsSearchCondition<PurchaseOrder>.ExistsTypes.Exists,
                Condition = new ExistsSearchCondition<PurchaseOrderLine>()
                {
                    RelationshipName = nameof(PurchaseOrderLine.FulfillmentPlanPurchaseOrderLines),
                    ExistsType = ExistsSearchCondition<PurchaseOrderLine>.ExistsTypes.Exists,
                    Condition = new LongSearchCondition<FulfillmentPlanPurchaseOrderLine>()
                    {
                        Field = nameof(FulfillmentPlanPurchaseOrderLine.FulfillmentPlanID),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = FulfillmentPlanID
                    }
                }
            });

            foreach (PurchaseOrder purchaseOrder in purchaseOrderSearch.GetEditableReader(transaction))
            {
                if (!purchaseOrder.ClearTemplateDataForPurchaseOrder(transaction) || !purchaseOrder.Save(transaction))
                {
                    Errors.AddRange(purchaseOrder.Errors.ToArray());
                    return false;
                }
            }

            return base.PreDelete(transaction);
        }

        private bool HasSignificantChanges()
        {
            bool IsFieldDirty(Expression<Func<FulfillmentPlan, object>> field)
            {
                string fieldPath = FieldPathUtility.CreateFieldPath(field);
                if (string.IsNullOrWhiteSpace(fieldPath) || fieldPath.Contains('.'))
                {
                    throw new InvalidOperationException("This method must not use field paths");
                }

                return this.IsFieldDirty(fieldPath);
            };
            
            return IsFieldDirty(fp => fp.TrackIDLoading) ||
                   IsFieldDirty(fp => fp.TrackIDDestination) ||
                   IsFieldDirty(fp => fp.TrackIDStrategicAfterLoad) ||
                   IsFieldDirty(fp => fp.TrackIDStrategicAfterDestination) ||
                   IsFieldDirty(fp => fp.TrackIDPostFulfillment);
        }

        public async Task<long?> Clone()
        {
            using (ITransaction transaction = SQLProviderFactory.GenerateTransaction())
            {
                FulfillmentPlan newPlan = DataObjectFactory.Create<FulfillmentPlan>();
                Copy(newPlan);
                newPlan.RailcarID = null;
                newPlan.LeaseRequestID = null;

                if (!await Task.Run(() => newPlan.Save(transaction)))
                {
                    Errors.AddRange(newPlan.Errors.ToArray());
                    return null;
                }

                Search<FulfillmentPlanPurchaseOrderLine> fppolSearch = new Search<FulfillmentPlanPurchaseOrderLine>(new LongSearchCondition<FulfillmentPlanPurchaseOrderLine>()
                {
                    Field = nameof(FulfillmentPlanPurchaseOrderLine.FulfillmentPlanID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = FulfillmentPlanID
                });

                foreach (FulfillmentPlanPurchaseOrderLine originalJoin in fppolSearch.GetEditableReader(transaction))
                {
                    FulfillmentPlanPurchaseOrderLine newJoin = DataObjectFactory.Create<FulfillmentPlanPurchaseOrderLine>();
                    originalJoin.Copy(newJoin);
                    newJoin.FulfillmentPlanID = newPlan.FulfillmentPlanID;
                    if (!await Task.Run(() => newJoin.Save(transaction)))
                    {
                        Errors.AddRange(newJoin.Errors.ToArray());
                        return null;
                    }
                }

                Search<FulfillmentPlanRoute> routeSearch = new Search<FulfillmentPlanRoute>(new LongSearchCondition<FulfillmentPlanRoute>()
                {
                    Field = nameof(FulfillmentPlanRoute.FulfillmentPlanID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = FulfillmentPlanID
                });

                foreach (FulfillmentPlanRoute originalRoute in routeSearch.GetEditableReader(transaction))
                {
                    FulfillmentPlanRoute newRoute = DataObjectFactory.Create<FulfillmentPlanRoute>();
                    originalRoute.Copy(newRoute);
                    newRoute.FulfillmentPlanID = newPlan.FulfillmentPlanID;
                    if (!await Task.Run(() => newRoute.Save(transaction)))
                    {
                        Errors.AddRange(newRoute.Errors.ToArray());
                        return null;
                    }
                }

                transaction.Commit();
                return newPlan.FulfillmentPlanID;
            }
        }

        #region Relationships
        #region purchasing
        private List<FulfillmentPlanRoute> _fulfillmentPlanRoutes = new List<FulfillmentPlanRoute>();
        [RelationshipList("4CCE7ACC-E964-4C7F-9A6C-E2268D47BA87", nameof(FulfillmentPlanRoute.FulfillmentPlanID), AutoDeleteReferences = true)]
        public IReadOnlyCollection<FulfillmentPlanRoute> FulfillmentPlanRoutes
        {
            get { CheckGet(); return _fulfillmentPlanRoutes; }
        }

        private List<FulfillmentPlanPurchaseOrderLine> _fulfillmentPlanPurchaseOrderLines = new List<FulfillmentPlanPurchaseOrderLine>();
        [RelationshipList("79F614DF-98B1-4AB2-A25D-0384B1B520A2", nameof(FulfillmentPlanPurchaseOrderLine.FulfillmentPlanID), AutoDeleteReferences = true)]
        public IReadOnlyCollection<FulfillmentPlanPurchaseOrderLine> FulfillmentPlanPurchaseOrderLines
        {
            get { CheckGet(); return _fulfillmentPlanPurchaseOrderLines; }
        }
        #endregion
        #endregion

        public static class SaveFlags
        {
            public static readonly Guid V_RailcarIsIdle = new Guid("F36370C5-D5B1-4F7B-9F50-5F04626826B3");
        }
    }
}
