using ClussPro.Base.Data;
using ClussPro.Base.Data.Conditions;
using ClussPro.Base.Data.Operand;
using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Schema;
using ClussPro.ObjectBasedFramework.Schema.Attributes;
using ClussPro.ObjectBasedFramework.Utility;
using ClussPro.ObjectBasedFramework.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using WebModels.gov;
using WebModels.mesasys;

namespace WebModels.company
{
    [Table("E97C6315-06D2-462D-A9F2-FB2971047221")]
    [Unique(new[] { nameof(LocationID), nameof(GovernmentID), nameof(ItemID), nameof(Quantity) })]
    public class LocationItem : DataObject
    {
        protected LocationItem() : base() { }

        public bool SkipAutomaticPriceUpdate { get; set; }

        private long? _locationItemID;
        [Field("970E72FC-53CB-481B-B53D-989B78C7C7D7")]
        public long? LocationItemID
        {
            get { CheckGet(); return _locationItemID; }
            set { CheckSet(); _locationItemID = value; }
        }

        private long? _locationID;
        [Field("5F523791-7A97-48C3-B06B-893AB4C748A7")]
        public long? LocationID
        {
            get { CheckGet(); return _locationID; }
            set { CheckSet(); _locationID = value; }
        }

        private Location _location = null;
        [Relationship("3F0496E7-B27F-4F72-AF9F-CCB35622BBFA")]
        public Location Location
        {
            get { CheckGet(); return _location; }
        }

        private long? _governmentID;
        [Field("EDC68D0A-3514-4172-9AAB-9952A431FD0E")]
        public long? GovernmentID
        {
            get { CheckGet(); return _governmentID; }
            set { CheckSet(); _governmentID = value; }
        }

        private Government _government = null;
        [Relationship("7A3B00B2-57A7-4956-9CBD-08E95F204EFA")]
        public Government Government
        {
            get { CheckGet(); return _government; }
        }

        private long? _itemID;
        [Field("6036A1BD-C039-47EE-A8FD-F9F4797E5CE4")]
        [Required]
        public long? ItemID
        {
            get { CheckGet(); return _itemID; }
            set { CheckSet(); _itemID = value; }
        }

        private Item _item = null;
        [Relationship("1766ED1F-5D49-4073-8A13-9AFE9807CBD2")]
        public Item Item
        {
            get { CheckGet(); return _item; }
        }

        private decimal? _quantity;
        [Field("9425016B-54DB-495C-8078-A43484FFE39C", DataSize = 9, DataScale = 2)]
        [Required]
        public decimal? Quantity
        {
            get { CheckGet(); return _quantity; }
            set { CheckSet(); _quantity = value; }
        }

        private decimal? _basePrice;
        [Field("36956505-BA15-4EDF-850A-1A179933EE3E", DataSize = 9, DataScale = 2)]
        [Required]
        public decimal? BasePrice
        {
            get { CheckGet(); return _basePrice; }
            set { CheckSet(); _basePrice = value; }
        }

        private PromotionLocationItem _currentPromotionLocationItem = null;
        [Relationship("63123D3C-4FC2-4EFB-B8F0-0F79181F4572", HasForeignKey = false)]
        public PromotionLocationItem CurrentPromotionLocationItem
        {
            get { CheckGet(); return _currentPromotionLocationItem; }
        }

        public override ICondition GetRelationshipCondition(Relationship relationship, string myAlias, string otherAlias)
        {
            switch (relationship.RelationshipName)
            {
                case nameof(CurrentPromotionLocationItem):
                    return CurrentPromotionLocationItemRelationshipCondition(myAlias, otherAlias);
                default:
                    return base.GetRelationshipCondition(relationship, myAlias, otherAlias);
            }
        }

        private ICondition CurrentPromotionLocationItemRelationshipCondition(string myAlias, string otherAlias)
        {
            ISelectQuery promotionLocationItemQuery = SQLProviderFactory.GetSelectQuery();
            promotionLocationItemQuery.SelectList = new List<Select>() { new Select() { SelectOperand = (ClussPro.Base.Data.Operand.Field)nameof(PromotionLocationItem.PromotionLocationItemID) } };
            promotionLocationItemQuery.Table = new Table("company", "PromotionLocationItem", "currentPLI");
            promotionLocationItemQuery.JoinList = new List<Join>()
            {
                new Join()
                {
                    JoinType = Join.JoinTypes.Inner,
                    Table = new Table("company", "Promotion", "currentP"),
                    Condition = new Condition()
                    {
                        Left = (ClussPro.Base.Data.Operand.Field)$"currentP.{nameof(Promotion.PromotionID)}",
                        ConditionType = Condition.ConditionTypes.Equal,
                        Right = (ClussPro.Base.Data.Operand.Field)$"currentPLI.{nameof(PromotionLocationItem.PromotionID)}"
                    }
                }
            };
            promotionLocationItemQuery.WhereCondition = new ConditionGroup()
            {
                ConditionGroupType = ConditionGroup.ConditionGroupTypes.And,
                Conditions = new List<ICondition>()
                {
                    new Condition()
                    {
                        Left = (ClussPro.Base.Data.Operand.Field)$"currentPLI.{nameof(PromotionLocationItem.LocationItemID)}",
                        ConditionType = Condition.ConditionTypes.Equal,
                        Right = (ClussPro.Base.Data.Operand.Field)$"{myAlias}.{nameof(LocationItemID)}"
                    },
                    new Condition()
                    {
                        Left = (ClussPro.Base.Data.Operand.Field)$"currentP.{nameof(Promotion.StartTime)}",
                        ConditionType = Condition.ConditionTypes.LessEqual,
                        Right = new Literal(DateTime.Now)
                    },
                    new Condition()
                    {
                        Left = (ClussPro.Base.Data.Operand.Field)$"currentP.{nameof(Promotion.EndTime)}",
                        ConditionType = Condition.ConditionTypes.GreaterEqual,
                        Right = new Literal(DateTime.Now)
                    }
                }
            };
            promotionLocationItemQuery.OrderByList = new List<Order>()
            {
                new Order()
                {
                    Field = (ClussPro.Base.Data.Operand.Field)$"currentP.{nameof(Promotion.StartTime)}",
                    OrderDirection = Order.OrderDirections.Descending
                }
            };
            promotionLocationItemQuery.PageSize = 1;

            return new Condition()
            {
                Left = (ClussPro.Base.Data.Operand.Field)$"{otherAlias}.{nameof(PromotionLocationItem.PromotionLocationItemID)}",
                ConditionType = Condition.ConditionTypes.Equal,
                Right = new SubQuery(promotionLocationItemQuery)
            };
        }

        protected override bool PostSave(ITransaction transaction)
        {
            if (GovernmentID == null && !SkipAutomaticPriceUpdate)
            {
                StorePricingAutomation storePricingAutomation = new Search<StorePricingAutomation>(new LongSearchCondition<StorePricingAutomation>()
                {
                    Field = nameof(StorePricingAutomation.LocationID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = LocationID
                }).GetReadOnly(transaction, FieldPathUtility.CreateFieldPathsAsList<StorePricingAutomation>(spa => new List<object>()
                {
                    spa.IsEnabled,
                    spa.PushAdd,
                    spa.PushUpdate,
                    spa.StorePricingAutomationLocations.First().LocationIDDestination
                }));

                if (storePricingAutomation != null && storePricingAutomation.IsEnabled && (storePricingAutomation.PushAdd || storePricingAutomation.PushUpdate))
                {
                    decimal? oldQuantity = (decimal?)GetDirtyValue(nameof(Quantity));
                    oldQuantity = oldQuantity ?? Quantity;

                    foreach (StorePricingAutomationLocation storePricingAutomationLocation in storePricingAutomation.StorePricingAutomationLocations)
                    {
                        Search<LocationItem> locationItemExistsSearch = new Search<LocationItem>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                            new LongSearchCondition<LocationItem>()
                            {
                                Field = nameof(LocationID),
                                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                Value = storePricingAutomationLocation.LocationIDDestination
                            },
                            new LongSearchCondition<LocationItem>()
                            {
                                Field = nameof(ItemID),
                                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                Value = ItemID
                            },
                            new DecimalSearchCondition<LocationItem>()
                            {
                                Field = nameof(Quantity),
                                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                Value = oldQuantity
                            }));

                        if (storePricingAutomation.PushAdd && IsInsert && !locationItemExistsSearch.ExecuteExists(transaction))
                        {
                            LocationItem newLocationItem = DataObjectFactory.Create<LocationItem>();
                            Copy(newLocationItem);
                            newLocationItem.LocationID = storePricingAutomationLocation.LocationIDDestination;
                            newLocationItem.SkipAutomaticPriceUpdate = true;
                            newLocationItem.Save(transaction); // We'll just eat the errors I guess...
                        }
                        else if (storePricingAutomation.PushUpdate && !IsInsert && locationItemExistsSearch.ExecuteExists(transaction))
                        {
                            LocationItem otherLocationItem = locationItemExistsSearch.GetEditable(transaction);
                            if (otherLocationItem.BasePrice != BasePrice || otherLocationItem.Quantity != Quantity)
                            {
                                otherLocationItem.Quantity = Quantity;
                                otherLocationItem.BasePrice = BasePrice;
                                otherLocationItem.SkipAutomaticPriceUpdate = true;
                                otherLocationItem.Save(transaction); // We'll just eat the errors I guess...
                            }
                        }
                    }
                }
            }
            return base.PostSave(transaction);
        }

        protected override bool PostDelete(ITransaction transaction)
        {
            if (GovernmentID == null && !SkipAutomaticPriceUpdate)
            {
                StorePricingAutomation storePricingAutomation = new Search<StorePricingAutomation>(new LongSearchCondition<StorePricingAutomation>()
                {
                    Field = nameof(StorePricingAutomation.LocationID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = LocationID
                }).GetReadOnly(transaction, FieldPathUtility.CreateFieldPathsAsList<StorePricingAutomation>(spa => new List<object>()
                {
                    spa.IsEnabled,
                    spa.PushDelete,
                    spa.StorePricingAutomationLocations.First().LocationIDDestination
                }));

                if (storePricingAutomation != null && storePricingAutomation.IsEnabled && storePricingAutomation.PushDelete)
                {
                    foreach (StorePricingAutomationLocation storePricingAutomationLocation in storePricingAutomation.StorePricingAutomationLocations)
                    {
                        Search<LocationItem> locationItemExistsSearch = new Search<LocationItem>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                            new LongSearchCondition<LocationItem>()
                            {
                                Field = nameof(LocationID),
                                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                Value = storePricingAutomationLocation.LocationIDDestination
                            },
                            new LongSearchCondition<LocationItem>()
                            {
                                Field = nameof(ItemID),
                                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                Value = ItemID
                            },
                            new DecimalSearchCondition<LocationItem>()
                            {
                                Field = nameof(Quantity),
                                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                Value = Quantity
                            }));

                        LocationItem otherLocationItem = locationItemExistsSearch.GetEditable(transaction);
                        if (otherLocationItem != null)
                        {
                            otherLocationItem.SkipAutomaticPriceUpdate = true;
                            if (!otherLocationItem.Delete(transaction))
                            {
                                Errors.AddRange(otherLocationItem.Errors.ToArray());
                                return false;
                            }
                        }
                    }
                }
            }
            return base.PostDelete(transaction);
        }

        #region Relationships
        #region company
        private List<StoreSaleItem> _storeSales = new List<StoreSaleItem>();
        [RelationshipList("EB7FD6EA-69A8-4CBC-8D1B-36A92F97991C", nameof(StoreSaleItem.LocationItemID), AutoRemoveReferences = true)]
        public IReadOnlyCollection<StoreSaleItem> StoreSales
        {
            get { CheckGet(); return _storeSales; }
        }

        private List<PromotionLocationItem> _promotionLocationItems = new List<PromotionLocationItem>();
        [RelationshipList("44428622-5CF6-47A0-804F-89871CD57952", nameof(PromotionLocationItem.LocationItemID), AutoRemoveReferences = true)]
        public IReadOnlyCollection<PromotionLocationItem> PromotionLocationItems
        {
            get { CheckGet(); return _promotionLocationItems; }
        }
        #endregion
        #endregion

        public static class SaveFlags
        {
            public static readonly Guid SkipAutomaticPricingUpdates = new Guid("5E6E3F9B-7B6D-4F3F-8F3F-1F6E3F9B7B6D");
        }
    }
}
