using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Schema.Attributes;
using ClussPro.ObjectBasedFramework.Utility;
using ClussPro.ObjectBasedFramework.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModels.mesasys;
using WebModels.purchasing;

namespace WebModels.fleet
{
    [Table("8098A8C0-31C0-491C-AE05-05DC62D02170")]
    public class RailcarLoad : DataObject
    {
        protected RailcarLoad() : base() { }

        private long? _railcarLoadID;
        [Field("DABBC6AD-887B-481A-A2DB-6FDDBCD3F83C")]
        public long? RailcarLoadID
        {
            get { CheckGet(); return _railcarLoadID; }
            set { CheckSet(); _railcarLoadID = value; }
        }

        private long? _railcarID;
        [Field("C91974D9-90FA-438B-B1C6-AC9600D4F2F9")]
        [Required]
        public long? RailcarID
        {
            get { CheckGet(); return _railcarID; }
            set { CheckSet(); _railcarID = value; }
        }

        private Railcar _railcar = null;
        [Relationship("BB528337-51C5-4E2F-BF77-96AD09E9A8BB")]
        public Railcar Railcar
        {
            get { CheckGet(); return _railcar; }
        }

        private long? _itemID;
        [Field("BACB57AE-CE0D-433B-B846-34C73BD269E8")]
        [Required]
        public long? ItemID
        {
            get { CheckGet(); return _itemID; }
            set { CheckSet(); _itemID = value; }
        }

        private Item _item = null;
        [Relationship("48ED4640-834E-43E5-A52F-E3404636AA25")]
        public Item Item
        {
            get { CheckGet(); return _item; }
        }

        private decimal? _quantity;
        [Field("B8350982-0BD8-488C-8E77-9C9DC5F1A48D", DataSize = 9, DataScale = 2)]
        [Required]
        public decimal? Quantity
        {
            get { CheckGet(); return _quantity; }
            set { CheckSet(); _quantity = value; }
        }
        
        private long? _purchaseOrderLineID;
        [Field("0D9B7B1B-5F5F-4B1B-9F4F-5F7B5B0B5B5B")]
        public long? PurchaseOrderLineID
        {
            get { CheckGet(); return _purchaseOrderLineID; }
            set { CheckSet(); _purchaseOrderLineID = value; }
        }

        private PurchaseOrderLine _purchaseOrderLine = null;
        [Relationship("74F3B41C-35A5-44EA-AEC3-ADA201A233E1")]
        public PurchaseOrderLine PurchaseOrderLine
        {
            get { CheckGet(); return _purchaseOrderLine; }
        }

        protected override bool PostDelete(ITransaction transaction)
        {
            if (PurchaseOrderLineID != null)
            {
                Search<Railcar> railcarSearch = new Search<Railcar>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                    new LongSearchCondition<Railcar>()
                    {
                        Field = nameof(Railcar.RailcarID),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = RailcarID
                    },
                    new ExistsSearchCondition<Railcar>()
                    {
                        RelationshipName = nameof(Railcar.RailcarLoads),
                        ExistsType = ExistsSearchCondition<Railcar>.ExistsTypes.NotExists,
                        Condition = new LongSearchCondition<RailcarLoad>()
                        {
                            Field = nameof(PurchaseOrderLineID),
                            SearchConditionType = SearchCondition.SearchConditionTypes.NotNull
                        }
                    },
                    new ExistsSearchCondition<Railcar>()
                    {
                        RelationshipName = nameof(Railcar.FulfillmentPlans),
                        ExistsType = ExistsSearchCondition<Railcar>.ExistsTypes.NotExists,
                        Condition = new ExistsSearchCondition<FulfillmentPlan>()
                        {
                            RelationshipName = nameof(FulfillmentPlan.FulfillmentPlanPurchaseOrderLines),
                            ExistsType = ExistsSearchCondition<FulfillmentPlan>.ExistsTypes.Exists,
                            Condition = new DecimalSearchCondition<FulfillmentPlanPurchaseOrderLine>()
                            {
                                Field = FieldPathUtility.CreateFieldPathsAsList<FulfillmentPlanPurchaseOrderLine>(fppol => new List<object>() { fppol.PurchaseOrderLine.RemainingQuantity }).First(),
                                SearchConditionType = SearchCondition.SearchConditionTypes.Greater,
                                Value = 0
                            }
                        }
                    }));

                Railcar railcar = railcarSearch.GetEditable(transaction);
                if (railcar != null)
                {
                    if (!railcar.CompleteReceiving(transaction))
                    {
                        Errors.AddRange(railcar.Errors.ToArray());
                        return false;
                    }
                }
            }

            return base.PostDelete(transaction);
        }
    }
}
