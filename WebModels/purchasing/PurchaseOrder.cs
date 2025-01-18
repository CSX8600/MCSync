using ClussPro.Base.Data;
using ClussPro.Base.Data.Query;
using ClussPro.Base.Extensions;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Schema.Attributes;
using ClussPro.ObjectBasedFramework.Utility;
using ClussPro.ObjectBasedFramework.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebModels.company;
using WebModels.fleet;
using WebModels.gov;
using WebModels.hMailServer.Validations.Conditions;
using WebModels.invoicing;

namespace WebModels.purchasing
{
    [Table("1D36A22A-14A1-4D70-B413-F5FCAF3C1B87")]
    public class PurchaseOrder : DataObject
    {
        protected PurchaseOrder() : base() { }

        private long? _purchaseOrderID;
        [Field("F9C0C9E3-8F6B-4F6F-8D8F-6F8D8F8D8F8D")]
        public long? PurchaseOrderID
        {
            get { CheckGet(); return _purchaseOrderID; }
            set { CheckGet(); _purchaseOrderID = value; }
        }

        private long? _locationIDOrigin;
        [Field("914E2A3E-D966-47AA-9F0F-281685E0109E")]
        public long? LocationIDOrigin
        {
            get { CheckGet(); return _locationIDOrigin; }
            set { CheckGet(); _locationIDOrigin = value; }
        }

        private Location _locationOrigin = null;
        [Relationship("F8F74C15-F8A7-48F2-9101-202F5B31253E", ForeignKeyField = nameof(LocationIDOrigin))]
        public Location LocationOrigin
        {
            get { CheckGet(); return _locationOrigin; }
        }

        private long? _governmentIDOrigin;
        [Field("8AA90984-AF2D-485B-8BE1-5F446CEF504C")]
        public long? GovernmentIDOrigin
        {
            get { CheckGet(); return _governmentIDOrigin; }
            set { CheckGet(); _governmentIDOrigin = value; }
        }

        private Government _governmentOrigin = null;
        [Relationship("A3512448-6DCC-49B7-83EA-5883696C699B", ForeignKeyField = nameof(GovernmentIDOrigin))]
        public Government GovernmentOrigin
        {
            get { CheckGet(); return _governmentOrigin; }
        }

        private long? _locationIDDestination;
        [Field("55694D3A-8BBA-4F95-9884-45A85D9B8051")]
        public long? LocationIDDestination
        {
            get { CheckGet(); return _locationIDDestination; }
            set { CheckGet(); _locationIDDestination = value; }
        }

        private Location _locationDestination = null;
        [Relationship("BB019C65-99CD-40BA-A9F6-F0283D02FCC3", ForeignKeyField = nameof(LocationIDDestination))]
        public Location LocationDestination
        {
            get { CheckGet(); return _locationDestination; }
        }

        private long? _governmentIDDestination;
        [Field("94970F64-69D2-42AE-BD76-22700EE35591")]
        public long? GovernmentIDDestination
        {
            get { CheckGet(); return _governmentIDDestination; }
            set { CheckGet(); _governmentIDDestination = value; }
        }

        private Government _governmentDestination = null;
        [Relationship("F79988B1-0395-4CB1-BA08-C37AC1B59A8F", ForeignKeyField = nameof(GovernmentIDDestination))]
        public Government GovernmentDestination
        {
            get { CheckGet(); return _governmentDestination; }
        }

        private long? _purchaseOrderIDClonedFrom;
        [Field("CF809F10-2043-49B0-9935-0C743F3E9855")]
        public long? PurchaseOrderIDClonedFrom
        {
            get { CheckGet(); return _purchaseOrderIDClonedFrom; }
            set { CheckGet(); _purchaseOrderIDClonedFrom = value; }
        }

        private PurchaseOrder _purchaseOrderClonedFrom = null;
        [Relationship("1880B8B9-42E0-47D3-86F4-EF54E2936783", ForeignKeyField = nameof(PurchaseOrderIDClonedFrom))]
        public PurchaseOrder PurchaseOrderClonedFrom
        {
            get { CheckGet(); return _purchaseOrderClonedFrom; }
        }

        private DateTime? _purchaseOrderDate;
        [Field("5E4FEF5E-887B-4DC8-B1C3-1F9A33ABC4CD", DataSize = 7)]
        public DateTime? PurchaseOrderDate
        {
            get { CheckGet(); return _purchaseOrderDate; }
            set { CheckGet(); _purchaseOrderDate = value; }
        }

        public enum Statuses
        {
            Draft,
            Pending,
            Accepted,
            Rejected,
            InProgress,
            Completed
        }

        private Statuses _status;
        [Field("A50DF475-536B-4219-91C9-62C5729A8DDC")]
        public Statuses Status
        {
            get { CheckGet(); return _status; }
            set { CheckGet(); _status = value; }
        }

        private string _description;
        [Field("A977CA7B-98F4-4B01-85E0-BBF99106F4CA", DataSize = 250)]
        [Required]
        public string Description
        {
            get { CheckGet(); return _description; }
            set { CheckGet(); _description = value; }
        }

        public enum InvoiceSchedules
        {
            UponShipment,
            UponDelivery,
            Manual
        }

        private InvoiceSchedules _invoiceSchedule = InvoiceSchedules.UponShipment;
        [Field("CCD7C290-82AB-404E-B47B-67937F1EF6E0")]
        public InvoiceSchedules InvoiceSchedule
        {
            get { CheckGet(); return _invoiceSchedule; }
            set { CheckGet(); _invoiceSchedule = value; }
        }

        private long? _accountIDReceiving;
        [Field("A82BD52B-60F9-4EBD-A652-722F629B0AA9")]
        public long? AccountIDReceiving
        {
            get { CheckGet(); return _accountIDReceiving; }
            set { CheckGet(); _accountIDReceiving = value; }
        }

        private account.Account _accountReceiving = null;
        [Relationship("9DEBDC1F-ABF2-4998-BB3D-5BA1FCBA79CC", ForeignKeyField = nameof(AccountIDReceiving))]
        public account.Account AccountReceiving
        {
            get { CheckGet(); return _accountReceiving; }
        }

        protected override bool PreSave(ITransaction transaction)
        {
            if (!IsInsert && HasSignficiantChanges() && !ClearTemplateDataForPurchaseOrder(transaction))
            {
                return false;
            }

            if (IsFieldDirty(nameof(Status)) && Status == Statuses.Completed && !ChargeQuoteDifference(transaction))
            {
                return false;
            }

            return base.PreSave(transaction);
        }

        public bool ClearTemplateDataForPurchaseOrder(ITransaction transaction)
        {
            PurchaseOrderIDClonedFrom = null;

            Search<PurchaseOrderTemplate> templateSearch = new Search<PurchaseOrderTemplate>(new LongSearchCondition<PurchaseOrder>()
            {
                Field = nameof(PurchaseOrderTemplate.PurchaseOrderID),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = PurchaseOrderID
            });

            foreach (PurchaseOrderTemplate template in templateSearch.GetEditableReader(transaction))
            {
                if (!template.Delete(transaction))
                {
                    Errors.AddRange(template.Errors.ToArray());
                    return false;
                }
            }

            Search<PurchaseOrder> cloneSearch = new Search<PurchaseOrder>(new LongSearchCondition<PurchaseOrder>()
            {
                Field = nameof(PurchaseOrder.PurchaseOrderIDClonedFrom),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = PurchaseOrderID
            });

            foreach (PurchaseOrder clone in cloneSearch.GetEditableReader(transaction))
            {
                clone.PurchaseOrderIDClonedFrom = null;
                if (!clone.Save(transaction))
                {
                    Errors.AddRange(clone.Errors.ToArray());
                    return false;
                }
            }

            return true;
        }

        private bool HasSignficiantChanges()
        {
            return IsFieldDirty(nameof(LocationIDOrigin)) ||
                   IsFieldDirty(nameof(GovernmentIDOrigin)) ||
                   IsFieldDirty(nameof(LocationIDDestination)) ||
                   IsFieldDirty(nameof(GovernmentIDDestination));
        }

        public async Task<bool> Submit(ITransaction transaction = null)
        {
            ITransaction localTransaction = transaction;

            try
            {
                if (localTransaction == null)
                {
                    localTransaction = SQLProviderFactory.GenerateTransaction();
                }

                if (Status != PurchaseOrder.Statuses.Draft)
                {
                    Errors.Add("Status", "Purchase Order must be in Draft status to be submitted");
                    return false;
                }

                if (!await UpdatePrices(localTransaction))
                {
                    return false;
                }

                Status = PurchaseOrder.Statuses.Pending;
                PurchaseOrderDate = DateTime.Now;
                if (!await Task.Run(() => Save(localTransaction, new List<Guid>() { PurchaseOrder.SaveFlags.V_StatusChange })))
                {
                    return false;
                }

                List<PurchaseOrderApproval> preApprovals = new List<PurchaseOrderApproval>();
                if (PurchaseOrderIDClonedFrom != null)
                {
                    Search<PurchaseOrderApproval> preApprovalSearch = new Search<PurchaseOrderApproval>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                        new LongSearchCondition<PurchaseOrderApproval>()
                        {
                            Field = nameof(PurchaseOrderApproval.PurchaseOrderID),
                            SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                            Value = PurchaseOrderIDClonedFrom
                        },
                        new BooleanSearchCondition<PurchaseOrderApproval>()
                        {
                            Field = nameof(PurchaseOrderApproval.FutureAutoApprove),
                            SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                            Value = true
                        },
                        new IntSearchCondition<PurchaseOrderApproval>()
                        {
                            Field = nameof(PurchaseOrderApproval.ApprovalStatus),
                            SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                            Value = (int)PurchaseOrderApproval.ApprovalStatuses.Approved
                        }));

                    preApprovals.AddRange(preApprovalSearch.GetReadOnlyReader(transaction, FieldPathUtility.CreateFieldPathsAsList<PurchaseOrderApproval>(poa => new object[]
                    {
                        poa.CompanyIDApprover,
                        poa.GovernmentIDApprover
                    })));
                }

                bool recipientPreApprovalExists = preApprovals.Any(poa => poa.CompanyIDApprover == LocationDestination?.CompanyID && poa.GovernmentIDApprover == GovernmentIDDestination);
                bool preApprovalApplied = recipientPreApprovalExists;
                PurchaseOrderApproval recipientApproval = DataObjectFactory.Create<PurchaseOrderApproval>();
                recipientApproval.PurchaseOrderID = PurchaseOrderID;
                recipientApproval.CompanyIDApprover = LocationDestination?.CompanyID;
                recipientApproval.GovernmentIDApprover = GovernmentIDDestination;
                recipientApproval.ApprovalPurpose = PurchaseOrderApproval.DESTINATION_PURPOSE;
                recipientApproval.ApprovalStatus = recipientPreApprovalExists ? PurchaseOrderApproval.ApprovalStatuses.Approved : PurchaseOrderApproval.ApprovalStatuses.Pending;
                recipientApproval.FutureAutoApprove = recipientPreApprovalExists;
                if (!await Task.Run(() => recipientApproval.Save(localTransaction)))
                {
                    Errors.AddRange(recipientApproval.Errors.ToArray());
                    return false;
                }

                Search<FulfillmentPlanRoute> routeSearch = new Search<FulfillmentPlanRoute>(new ExistsSearchCondition<FulfillmentPlanRoute>()
                {
                    RelationshipName = FieldPathUtility.CreateFieldPathsAsList<FulfillmentPlanRoute>(fpr => new List<object>() { fpr.FulfillmentPlan.FulfillmentPlanPurchaseOrderLines }).First(),
                    ExistsType = ExistsSearchCondition<FulfillmentPlanRoute>.ExistsTypes.Exists,
                    Condition = new LongSearchCondition<FulfillmentPlanPurchaseOrderLine>()
                    {
                        Field = FieldPathUtility.CreateFieldPathsAsList<FulfillmentPlanPurchaseOrderLine>(fppol => new List<object>() { fppol.PurchaseOrderLine.PurchaseOrderID }).First(),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = PurchaseOrderID
                    }
                });

                HashSet<long?> companyIDRoutes = new HashSet<long?>();
                HashSet<long?> governmentIDRoutes = new HashSet<long?>();
                List<string> routeFields = FieldPathUtility.CreateFieldPathsAsList<FulfillmentPlanRoute>(fpr => new List<object>()
                {
                    fpr.CompanyIDFrom,
                    fpr.CompanyIDTo,
                    fpr.GovernmentIDFrom,
                    fpr.GovernmentIDTo
                }).ToList();

                foreach (FulfillmentPlanRoute route in await Task.Run(() => routeSearch.GetReadOnlyReader(localTransaction, routeFields)))
                {
                    companyIDRoutes.AddRange(new[] { route.CompanyIDFrom, route.CompanyIDTo });
                    governmentIDRoutes.AddRange(new[] { route.GovernmentIDFrom, route.GovernmentIDTo });
                }

                companyIDRoutes.RemoveWhere(companyID => companyID == LocationDestination?.CompanyID || companyID == LocationOrigin?.CompanyID || companyID == null);
                governmentIDRoutes.RemoveWhere(governmentID => governmentID == GovernmentIDDestination || governmentID == null);

                foreach (long? companyID in companyIDRoutes)
                {
                    bool preApprovalExists = preApprovals.Any(poa => poa.CompanyIDApprover == companyID);
                    PurchaseOrderApproval companyApproval = DataObjectFactory.Create<PurchaseOrderApproval>();
                    companyApproval.PurchaseOrderID = PurchaseOrderID;
                    companyApproval.CompanyIDApprover = companyID;
                    companyApproval.ApprovalPurpose = PurchaseOrderApproval.ROUTE_PURPOSE;
                    companyApproval.ApprovalStatus = preApprovalExists ? PurchaseOrderApproval.ApprovalStatuses.Approved : PurchaseOrderApproval.ApprovalStatuses.Pending;
                    companyApproval.FutureAutoApprove = preApprovalExists;
                    if (!await Task.Run(() => companyApproval.Save(localTransaction)))
                    {
                        Errors.AddRange(companyApproval.Errors.ToArray());
                        return false;
                    }

                    preApprovalApplied |= preApprovalExists;
                }

                foreach (long? governmentID in governmentIDRoutes)
                {
                    bool preApprovalExists = preApprovals.Any(poa => poa.GovernmentIDApprover == governmentID);
                    PurchaseOrderApproval governmentApproval = DataObjectFactory.Create<PurchaseOrderApproval>();
                    governmentApproval.PurchaseOrderID = PurchaseOrderID;
                    governmentApproval.GovernmentIDApprover = governmentID;
                    governmentApproval.ApprovalPurpose = PurchaseOrderApproval.ROUTE_PURPOSE;
                    governmentApproval.ApprovalStatus = preApprovalExists ? PurchaseOrderApproval.ApprovalStatuses.Approved : PurchaseOrderApproval.ApprovalStatuses.Pending;
                    governmentApproval.FutureAutoApprove = preApprovalExists;
                    if (!await Task.Run(() => governmentApproval.Save(localTransaction)))
                    {
                        Errors.AddRange(governmentApproval.Errors.ToArray());
                        return false;
                    }

                    preApprovalApplied |= preApprovalExists;
                }

                if (preApprovalApplied)
                {
                    await ApprovalSubmitted(transaction);
                }

                if (transaction == null)
                {
                    localTransaction.Commit();
                }

                return true;
            }
            finally
            {
                if (transaction == null && localTransaction != null && localTransaction.IsActive)
                {
                    localTransaction.Rollback();
                }
            }
        }

        private async Task<bool> UpdatePrices(ITransaction transaction)
        {
            List<ISearchCondition> quotationSearchConditions = new List<ISearchCondition>();
            if (LocationIDOrigin != null)
            {
                quotationSearchConditions.Add(new ExistsSearchCondition<Quotation>()
                {
                    RelationshipName = FieldPathUtility.CreateFieldPathsAsList<Quotation>(q => new List<object>() { q.CompanyTo.Locations }).First(),
                    ExistsType = ExistsSearchCondition<Quotation>.ExistsTypes.Exists,
                    Condition = new LongSearchCondition<Location>()
                    {
                        Field = nameof(Location.LocationID),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = LocationIDOrigin
                    }
                });
            }

            if (LocationIDDestination != null)
            {
                quotationSearchConditions.Add(new ExistsSearchCondition<Quotation>()
                {
                    RelationshipName = FieldPathUtility.CreateFieldPathsAsList<Quotation>(q => new List<object>() { q.CompanyFrom.Locations }).First(),
                    ExistsType = ExistsSearchCondition<Quotation>.ExistsTypes.Exists,
                    Condition = new LongSearchCondition<Location>()
                    {
                        Field = nameof(Location.LocationID),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = LocationIDDestination
                    }
                });
            }

            if (GovernmentIDOrigin != null)
            {
                quotationSearchConditions.Add(new LongSearchCondition<Quotation>()
                {
                    Field = nameof(Quotation.GovernmentIDTo),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = GovernmentIDOrigin
                });
            }

            if (GovernmentIDDestination != null)
            {
                quotationSearchConditions.Add(new LongSearchCondition<Quotation>()
                {
                    Field = nameof(Quotation.GovernmentIDFrom),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = GovernmentIDDestination
                });
            }

            quotationSearchConditions.Add(new DateTimeSearchCondition<Quotation>()
            {
                Field = nameof(Quotation.ExpirationTime),
                SearchConditionType = SearchCondition.SearchConditionTypes.Greater,
                Value = DateTime.Now
            });

            Search<Quotation> quoteSearch = new Search<Quotation>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And, quotationSearchConditions.ToArray()));
            List<Quotation> quotes = quoteSearch.GetEditableReader(transaction, FieldPathUtility.CreateFieldPathsAsList<Quotation>(q => new List<object>()
            {
                q.QuotationItems.First().ItemID,
                q.QuotationItems.First().MinimumQuantity,
                q.QuotationItems.First().UnitCost
            })).ToList();

            Dictionary<QuotationItem, Quotation> quotesByItem = new Dictionary<QuotationItem, Quotation>();
            quotes.ForEach(q => q.QuotationItems.ToList().ForEach(qi => quotesByItem[qi] = q));

            List<LocationItem> locationItems = new List<LocationItem>();
            LongSearchCondition<LocationItem> locationItemEntitySearchCondition;
            if (LocationIDDestination != null)
            {
                locationItemEntitySearchCondition = new LongSearchCondition<LocationItem>()
                {
                    Field = nameof(LocationItem.LocationID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = LocationIDDestination
                };
            }
            else
            {
                locationItemEntitySearchCondition = new LongSearchCondition<LocationItem>()
                {
                    Field = nameof(LocationItem.GovernmentID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = GovernmentIDDestination
                };
            }

            Search<LocationItem> locationItemSearch = new Search<LocationItem>(locationItemEntitySearchCondition);
            locationItems = locationItemSearch.GetReadOnlyReader(transaction, FieldPathUtility.CreateFieldPathsAsList<LocationItem>(li => new List<object>()
            {
                li.ItemID,
                li.Quantity,
                li.BasePrice,
                li.CurrentPromotionLocationItem.PromotionPrice
            })).ToList();

            Search<PurchaseOrderLine> lineSearch = new Search<PurchaseOrderLine>(new LongSearchCondition<PurchaseOrderLine>()
            {
                Field = nameof(PurchaseOrderLine.PurchaseOrderID),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = PurchaseOrderID
            });

            foreach(PurchaseOrderLine line in await Task.Run(() => lineSearch.GetEditableReader(transaction, FieldPathUtility.CreateFieldPathsAsList<PurchaseOrderLine>(pol => new object[] { pol.Item.Name }))))
            {
                if (line.ItemID == null)
                {
                    continue;
                }

                QuotationItem quoteItem = quotesByItem.Keys.Where(qi => qi.ItemID == line.ItemID && qi.MinimumQuantity <= line.Quantity).OrderBy(qi => qi.UnitCost).FirstOrDefault();
                decimal? unitCost = quoteItem?.UnitCost;
                if (unitCost == null)
                {
                    LocationItem relatedItem = locationItems.Where(li => li.ItemID == line.ItemID && li.Quantity == line.Quantity).FirstOrDefault();
                    if (relatedItem == null)
                    {
                        relatedItem = locationItems.Where(li => li.ItemID == line.ItemID && li.Quantity == 1).FirstOrDefault();
                    }

                    if (relatedItem == null)
                    {
                        Errors.AddBaseMessage($"Could not find price for {line.Quantity}x {line.Item.Name}");
                        return false;
                    }

                    unitCost = (relatedItem.CurrentPromotionLocationItem?.PromotionPrice ?? relatedItem.BasePrice) / relatedItem.Quantity;
                }

                line.UnitCost = unitCost.Value;

                if (!await Task.Run(() => line.Save(transaction)))
                {
                    Errors.AddRange(line.Errors.ToArray());
                    return false;
                }
            }

            return true;
        }

        private bool ChargeQuoteDifference(ITransaction transaction)
        {
            Search<PurchaseOrderLine> purchaseOrderLineSearch = new Search<PurchaseOrderLine>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                new LongSearchCondition<PurchaseOrderLine>()
                {
                    Field = nameof(PurchaseOrderLine.PurchaseOrderID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = PurchaseOrderID
                },
                new DecimalSearchCondition<PurchaseOrderLine>()
                {
                    Field = nameof(PurchaseOrderLine.RemainingQuantity),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Greater,
                    Value = 0M
                }));

            List<ISearchCondition> entitySearchConditions = new List<ISearchCondition>();
            if (GovernmentIDDestination != null)
            {
                entitySearchConditions.Add(new LongSearchCondition<QuotationItem>()
                {
                    Field = FieldPathUtility.CreateFieldPath<QuotationItem>(qi => qi.Quotation.GovernmentIDFrom),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = GovernmentIDDestination
                });
            }
            if (GovernmentIDOrigin != null)
            {
                entitySearchConditions.Add(new LongSearchCondition<QuotationItem>()
                {
                    Field = FieldPathUtility.CreateFieldPath<QuotationItem>(qi => qi.Quotation.GovernmentIDTo),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = GovernmentIDOrigin
                });
            }
            if (LocationIDDestination != null)
            {
                long? companyIDDestination = DataObject.GetReadOnlyByPrimaryKey<Location>(LocationIDDestination, transaction, new string[] { nameof(Location.CompanyID) }).CompanyID;

                entitySearchConditions.Add(new LongSearchCondition<QuotationItem>()
                {
                    Field = FieldPathUtility.CreateFieldPath<QuotationItem>(qi => qi.Quotation.CompanyIDFrom),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = companyIDDestination
                });
            }
            if (LocationIDOrigin != null)
            {
                long? companyIDOrigin = DataObject.GetReadOnlyByPrimaryKey<Location>(LocationIDOrigin, transaction, new string[] { nameof(Location.CompanyID) }).CompanyID;

                entitySearchConditions.Add(new LongSearchCondition<QuotationItem>()
                {
                    Field = FieldPathUtility.CreateFieldPath<QuotationItem>(qi => qi.Quotation.CompanyIDTo),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = companyIDOrigin
                });
            }
            entitySearchConditions.Add(new DateTimeSearchCondition<QuotationItem>()
            {
                Field = FieldPathUtility.CreateFieldPath<QuotationItem>(qi => qi.Quotation.ExpirationTime),
                SearchConditionType = SearchCondition.SearchConditionTypes.GreaterEquals,
                Value = PurchaseOrderDate
            });

            Dictionary<long?, decimal> underpaidQuantityByItemID = new Dictionary<long?, decimal>();
            Dictionary<long?, decimal> paidAmountByItemID = new Dictionary<long?, decimal>();
            foreach (PurchaseOrderLine purchaseOrderLine in purchaseOrderLineSearch.GetReadOnlyReader(transaction, FieldPathUtility.CreateFieldPathsAsList<PurchaseOrderLine>(pol => new object[]
            {
                pol.FulfilledQuantity,
                pol.ItemID,
                pol.Quantity,
                pol.UnitCost
            })))
            {
                List<ISearchCondition> andConditions = new List<ISearchCondition>(entitySearchConditions)
                {
                    new LongSearchCondition<QuotationItem>()
                    {
                        Field = nameof(QuotationItem.ItemID),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = purchaseOrderLine.ItemID
                    }
                };

                Search<QuotationItem> quotationItemSearch = new Search<QuotationItem>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And, andConditions.ToArray()));
                QuotationItem quotationItem = quotationItemSearch.GetReadOnly(transaction, FieldPathUtility.CreateFieldPathsAsList<QuotationItem>(qi => new object[]
                {
                    qi.MinimumQuantity
                }));

                if (quotationItem == null)
                {
                    continue;
                }

                if (purchaseOrderLine.FulfilledQuantity < quotationItem.MinimumQuantity)
                {
                    underpaidQuantityByItemID.GetOrSet(purchaseOrderLine.ItemID, () => 0M);
                    paidAmountByItemID.GetOrSet(purchaseOrderLine.ItemID, () => 0M);

                    underpaidQuantityByItemID[purchaseOrderLine.ItemID] += purchaseOrderLine.FulfilledQuantity.Value;
                    paidAmountByItemID[purchaseOrderLine.ItemID] += (purchaseOrderLine.UnitCost * purchaseOrderLine.FulfilledQuantity).Value;
                }
            }

            if (underpaidQuantityByItemID.Any())
            {
                Dictionary<long?, decimal> chargesByItemID = new Dictionary<long?, decimal>();
                Search<LocationItem> locationItemSearch = new Search<LocationItem>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                    new LongSearchCondition<LocationItem>()
                    {
                        Field = nameof(Location.LocationID),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = LocationIDDestination
                    },
                    new DecimalSearchCondition<LocationItem>()
                    {
                        Field = nameof(LocationItem.Quantity),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = 1
                    },
                    new LongSearchCondition<LocationItem>()
                    {
                        Field = nameof(LocationItem.ItemID),
                        SearchConditionType = SearchCondition.SearchConditionTypes.List,
                        List = underpaidQuantityByItemID.Keys.Select(k => k.Value).ToList()
                    }));

                foreach (LocationItem locationItem in locationItemSearch.GetReadOnlyReader(transaction, FieldPathUtility.CreateFieldPathsAsList<LocationItem>(li => new object[]
                {
                    li.ItemID,
                    li.BasePrice
                })))
                {
                    decimal? amountToCharge = locationItem.BasePrice * underpaidQuantityByItemID[locationItem.ItemID] - paidAmountByItemID[locationItem.ItemID];

                    if (amountToCharge > 0)
                    {
                        chargesByItemID[locationItem.ItemID] = amountToCharge.Value;
                    }
                }

                if (chargesByItemID.Any())
                {
                    string nextInvoiceNumber = "PO" + PurchaseOrderID;
                    if (LocationIDDestination != null)
                    {
                        Location location = DataObject.GetReadOnlyByPrimaryKey<Location>(LocationIDDestination, transaction, FieldPathUtility.CreateFieldPathsAsList<Location>(l => new object[]
                        {
                            l.InvoiceNumberPrefix,
                            l.NextInvoiceNumber
                        }));

                        if (!string.IsNullOrEmpty(location.NextInvoiceNumber))
                        {
                            nextInvoiceNumber = $"{location.InvoiceNumberPrefix}{location.NextInvoiceNumber}";
                        }
                    }
                    else if (GovernmentIDDestination != null)
                    {
                        Government government = DataObject.GetReadOnlyByPrimaryKey<Government>(GovernmentIDDestination, transaction, FieldPathUtility.CreateFieldPathsAsList<Government>(g => new object[]
                        {
                            g.InvoiceNumberPrefix,
                            g.NextInvoiceNumber
                        }));

                        if (!string.IsNullOrEmpty(government.NextInvoiceNumber))
                        {
                            nextInvoiceNumber = $"{government.InvoiceNumberPrefix}{government.NextInvoiceNumber}";
                        }
                    }

                    Invoice invoice = DataObjectFactory.Create<Invoice>();
                    invoice.GovernmentIDTo = GovernmentIDOrigin;
                    invoice.GovernmentIDFrom = GovernmentIDDestination;
                    invoice.LocationIDTo = LocationIDOrigin;
                    invoice.LocationIDFrom = LocationIDDestination;
                    invoice.PurchaseOrderID = PurchaseOrderID;
                    invoice.InvoiceNumber = nextInvoiceNumber;
                    invoice.InvoiceDate = DateTime.Now;
                    invoice.DueDate = DateTime.Now.AddDays(14);
                    invoice.Description = "Charges for Purchase Order " + PurchaseOrderID;
                    invoice.AccountIDTo = AccountIDReceiving;
                    if (!invoice.Save(transaction))
                    {
                        Errors.AddRange(invoice.Errors.ToArray());
                        return false;
                    }
                    else
                    {
                        foreach (KeyValuePair<long?, decimal> charge in chargesByItemID)
                        {
                            InvoiceLine invoiceLine = DataObjectFactory.Create<InvoiceLine>();
                            invoiceLine.InvoiceID = invoice.InvoiceID;
                            invoiceLine.ItemID = charge.Key;
                            invoiceLine.Quantity = 1;
                            invoiceLine.UnitCost = charge.Value;
                            invoiceLine.Description = "Makeup charge - quote minimum quantity not met";
                            if (!invoiceLine.Save(transaction))
                            {
                                Errors.AddRange(invoiceLine.Errors.ToArray());
                                return false;
                            }
                        }
                    }

                    invoice.IssueInvoice(transaction);
                    if (invoice.Errors.Any())
                    {
                        Errors.AddRange(invoice.Errors.ToArray());
                        return false;
                    }
                }
            }

            return true;
        }

        public async Task<bool> WithdrawSubmission(ITransaction transaction = null)
        {
            ITransaction localTransaction = transaction;

            try
            {
                if (localTransaction == null)
                {
                    localTransaction = SQLProviderFactory.GenerateTransaction();
                }

                if (Status != PurchaseOrder.Statuses.Pending && Status != PurchaseOrder.Statuses.Rejected)
                {
                    Errors.Add("Status", "Purchase order must be pending or rejected to withdraw submission");
                    return false;
                }

                Status = PurchaseOrder.Statuses.Draft;
                PurchaseOrderDate = null;
                if (!await Task.Run(() => Save(localTransaction, new List<Guid>() { PurchaseOrder.SaveFlags.V_StatusChange })))
                {
                    return false;
                }

                Search<PurchaseOrderApproval> approvalSearch = new Search<PurchaseOrderApproval>(new LongSearchCondition<PurchaseOrder>()
                {
                    Field = nameof(PurchaseOrderApproval.PurchaseOrderID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = PurchaseOrderID
                });

                foreach (PurchaseOrderApproval approval in await Task.Run(() => approvalSearch.GetEditableReader(localTransaction)))
                {
                    if (!await Task.Run(() => approval.Delete(localTransaction)))
                    {
                        Errors.AddRange(approval.Errors.ToArray());
                        return false;
                    }
                }

                if (transaction == null)
                {
                    localTransaction.Commit();
                }

                return true;
            }
            finally
            {
                if (transaction == null && localTransaction != null && localTransaction.IsActive)
                {
                    localTransaction.Rollback();
                }
            }
        }

        public async Task<bool> ApprovalSubmitted(ITransaction transaction = null)
        {
            ITransaction localTransaction = transaction;

            try
            {
                if (localTransaction == null)
                {
                    localTransaction = SQLProviderFactory.GenerateTransaction();
                }

                PurchaseOrder purchaseOrderForApprovalLookup = DataObject.GetReadOnlyByPrimaryKey<PurchaseOrder>(PurchaseOrderID, localTransaction, FieldPathUtility.CreateFieldPathsAsList<PurchaseOrder>(po => new List<object>()
                {
                    po.PurchaseOrderLines.First().IsService,
                    po.PurchaseOrderLines.First().ItemID,
                    po.PurchaseOrderLines.First().Quantity,
                    po.PurchaseOrderLines.First().FulfillmentPlanPurchaseOrderLines.First().FulfillmentPlan.LeaseRequestID,
                    po.PurchaseOrderLines.First().FulfillmentPlanPurchaseOrderLines.First().FulfillmentPlan.RailcarID,
                    po.PurchaseOrderLines.First().FulfillmentPlanPurchaseOrderLines.First().FulfillmentPlan.TrackIDLoading,
                    po.PurchaseOrderLines.First().FulfillmentPlanPurchaseOrderLines.First().FulfillmentPlan.FulfillmentPlanRoutes.First().CompanyIDFrom,
                    po.PurchaseOrderLines.First().FulfillmentPlanPurchaseOrderLines.First().FulfillmentPlan.FulfillmentPlanRoutes.First().CompanyIDTo,
                    po.PurchaseOrderLines.First().FulfillmentPlanPurchaseOrderLines.First().FulfillmentPlan.FulfillmentPlanRoutes.First().GovernmentIDFrom,
                    po.PurchaseOrderLines.First().FulfillmentPlanPurchaseOrderLines.First().FulfillmentPlan.FulfillmentPlanRoutes.First().GovernmentIDTo,
                    po.PurchaseOrderApprovals.First().ApprovalStatus
                }));

                bool hasRejections = purchaseOrderForApprovalLookup.PurchaseOrderApprovals?.Any(poa => poa.ApprovalStatus == PurchaseOrderApproval.ApprovalStatuses.Rejected) ?? false;
                bool hasNoRailcars = purchaseOrderForApprovalLookup.PurchaseOrderLines?.Any(pl => pl.FulfillmentPlanPurchaseOrderLines?.Any(fppol => fppol.FulfillmentPlan?.RailcarID == null) ?? false) ?? false;
                bool hasAnyPending = purchaseOrderForApprovalLookup.PurchaseOrderApprovals?.Any(poa => poa.ApprovalStatus == PurchaseOrderApproval.ApprovalStatuses.Pending) ?? false;

                if (!hasRejections && !hasNoRailcars && !hasAnyPending)
                {
                    Status = Statuses.Accepted;
                    if (!await Task.Run(() => Save(localTransaction, new List<Guid>() { PurchaseOrder.SaveFlags.V_StatusChange })))
                    {
                        return false;
                    }

                    // Update all railcars per fulfillment plans
                    foreach(FulfillmentPlan fulfillmentPlan in purchaseOrderForApprovalLookup.PurchaseOrderLines.SelectMany(pol => pol.FulfillmentPlanPurchaseOrderLines).Select(fppol => fppol.FulfillmentPlan))
                    {
                        // Update destination
                        Railcar railcar = DataObject.GetEditableByPrimaryKey<Railcar>(fulfillmentPlan.RailcarID, localTransaction, null);
                        railcar.TrackIDDestination = fulfillmentPlan.TrackIDLoading;
                        if (!await Task.Run(() => railcar.Save(localTransaction)))
                        {
                            Errors.AddRange(railcar.Errors.ToArray());
                            return false;
                        }

                        // Clear out existing routes
                        Search<RailcarRoute> deleteSearch = new Search<RailcarRoute>(new LongSearchCondition<RailcarRoute>()
                        {
                            Field = nameof(RailcarRoute.RailcarID),
                            SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                            Value = railcar.RailcarID
                        });
                        foreach(RailcarRoute railcarRoute in await Task.Run(() => deleteSearch.GetEditableReader(localTransaction)))
                        {
                            if (!await Task.Run(() => railcarRoute.Delete(localTransaction)))
                            {
                                Errors.AddRange(railcarRoute.Errors.ToArray());
                                return false;
                            }
                        }

                        // Routes will be created after product is picked up
                    }

                    // Expire any quotes that were used
                    List<ISearchCondition> quotationSearchConditions = new List<ISearchCondition>()
                    {
                        new DateTimeSearchCondition<Quotation>()
                        {
                            Field = nameof(Quotation.ExpirationTime),
                            SearchConditionType = SearchCondition.SearchConditionTypes.GreaterEquals,
                            Value = PurchaseOrderDate.Value
                        },
                        new BooleanSearchCondition<Quotation>()
                        {
                            Field = nameof(Quotation.IsRepeatable),
                            SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                            Value = false
                        },
                        new ExistsSearchCondition<Quotation>()
                        {
                            RelationshipName = nameof(Quotation.QuotationItems),
                            ExistsType = ExistsSearchCondition<Quotation>.ExistsTypes.Exists,
                            Condition = new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                                new LongSearchCondition<QuotationItem>()
                                {
                                    Field = nameof(QuotationItem.ItemID),
                                    SearchConditionType = SearchCondition.SearchConditionTypes.List,
                                    List = purchaseOrderForApprovalLookup.PurchaseOrderLines.Where(pol => !pol.IsService && pol.ItemID != null).Select(pol => pol.ItemID.Value).ToList()
                                })
                        }
                    };

                    if (LocationIDOrigin != null)
                    {
                        quotationSearchConditions.Add(new ExistsSearchCondition<Quotation>()
                        {
                            RelationshipName = FieldPathUtility.CreateFieldPathsAsList<Quotation>(q => new List<object>() { q.CompanyTo.Locations }).First(),
                            ExistsType = ExistsSearchCondition<Quotation>.ExistsTypes.Exists,
                            Condition = new LongSearchCondition<Location>()
                            {
                                Field = nameof(Location.LocationID),
                                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                Value = LocationIDOrigin
                            }
                        });
                    }

                    if (LocationIDDestination != null)
                    {
                        quotationSearchConditions.Add(new ExistsSearchCondition<Quotation>()
                        {
                            RelationshipName = FieldPathUtility.CreateFieldPathsAsList<Quotation>(q => new List<object>() { q.CompanyFrom.Locations }).First(),
                            ExistsType = ExistsSearchCondition<Quotation>.ExistsTypes.Exists,
                            Condition = new LongSearchCondition<Location>()
                            {
                                Field = nameof(Location.LocationID),
                                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                Value = LocationIDDestination
                            }
                        });
                    }

                    if (GovernmentIDOrigin != null)
                    {
                        quotationSearchConditions.Add(new LongSearchCondition<Quotation>()
                        {
                            Field = nameof(Quotation.GovernmentIDTo),
                            SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                            Value = GovernmentIDOrigin
                        });
                    }

                    if (GovernmentIDDestination != null)
                    {
                        quotationSearchConditions.Add(new LongSearchCondition<Quotation>()
                        {
                            Field = nameof(Quotation.GovernmentIDFrom),
                            SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                            Value = GovernmentIDDestination
                        });
                    }

                    Search<Quotation> quotationSearch = new Search<Quotation>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And, quotationSearchConditions.ToArray()));
                    List<Quotation> quotations = await Task.Run(() => quotationSearch.GetEditableReader(localTransaction, FieldPathUtility.CreateFieldPathsAsList<Quotation>(q => new List<object>()
                    {
                        q.QuotationItems.First().ItemID,
                        q.QuotationItems.First().UnitCost,
                        q.QuotationItems.First().MinimumQuantity
                    })).ToList());
                    Dictionary<QuotationItem, Quotation> quotationsByLine = new Dictionary<QuotationItem, Quotation>();
                    Dictionary<long?, List<QuotationItem>> quotationItemsByItemID = new Dictionary<long?, List<QuotationItem>>();

                    quotations.ForEach(q => q.QuotationItems.ToList().ForEach(qi => quotationsByLine.Add(qi, q)));
                    quotationsByLine.Keys.ToList().ForEach(qi => quotationItemsByItemID.GetOrSet(qi.ItemID, () => new List<QuotationItem>()).Add(qi));

                    foreach(PurchaseOrderLine line in purchaseOrderForApprovalLookup.PurchaseOrderLines)
                    {
                        if (line.IsService || line.ItemID == null)
                        {
                            continue;
                        }

                        QuotationItem item = quotationItemsByItemID.GetOrDefault(line.ItemID)?.Where(qi => qi.MinimumQuantity <= line.Quantity).OrderByDescending(qi => qi.UnitCost).FirstOrDefault();
                        if (item == null || !quotations.Any(q => q.QuotationItems.Contains(item)))
                        {
                            continue;
                        }

                        Quotation quotation = quotationsByLine[item];
                        quotation.ExpirationTime = DateTime.Now;
                        if (!await Task.Run(() => quotation.Save(localTransaction)))
                        {
                            Errors.AddRange(quotation.Errors.ToArray());
                            return false;
                        }

                        quotations.Remove(quotation);
                    }
                }

                List<Guid> saveFlags = new List<Guid>();
                if (hasRejections)
                {
                    Status = Statuses.Rejected;
                    saveFlags.Add(SaveFlags.V_StatusChange);
                }
                
                if (IsFieldDirty(nameof(InvoiceSchedule)) || IsFieldDirty(nameof(AccountIDReceiving)) || IsFieldDirty(nameof(Status)))
                {
                    if (!await Task.Run(() => Save(transaction, saveFlags)))
                    {
                        return false;
                    }
                }

                if (transaction == null)
                {
                    localTransaction.Commit();
                }

                return true;
            }
            finally
            {
                if (transaction == null && localTransaction != null && localTransaction.IsActive)
                {
                    localTransaction.Rollback();
                }
            }
        }

        #region Relationships
        #region invoicing
        private List<Invoice> _invoices = new List<Invoice>();
        [RelationshipList("B9866DDC-19F1-42F8-8905-E80C8CC8CEA1", nameof(Invoice.PurchaseOrderID))]
        public IReadOnlyCollection<Invoice> Invoices
        {
            get { CheckGet(); return _invoices; }
        }
        #endregion
        #region purchasing
        private List<PurchaseOrderLine> _purchaseOrderLines = new List<PurchaseOrderLine>();
        [RelationshipList("F890FC5F-207A-43FC-B8A1-A42DF5288D5F", nameof(PurchaseOrderLine.PurchaseOrderID), AutoDeleteReferences = true)]
        public IReadOnlyCollection<PurchaseOrderLine> PurchaseOrderLines
        {
            get { CheckGet(); return _purchaseOrderLines; }
        }

        private List<PurchaseOrderApproval> _purchaseOrderApprovals = new List<PurchaseOrderApproval>();
        [RelationshipList("CC393106-CC25-462B-A23C-36DF998E49F7", nameof(PurchaseOrderApproval.PurchaseOrderID), AutoDeleteReferences = true)]
        public IReadOnlyCollection<PurchaseOrderApproval> PurchaseOrderApprovals
        {
            get { CheckGet(); return _purchaseOrderApprovals; }
        }

        private List<BillOfLading> _billsOfLading = new List<BillOfLading>();
        [RelationshipList("8E46EC76-0E2E-4E8F-900F-836FC6C8BBC0", nameof(BillOfLading.PurchaseOrderID))]
        public IReadOnlyCollection<BillOfLading> BillsOfLading
        {
            get { CheckGet(); return _billsOfLading; }
        }

        private List<PurchaseOrderTemplate> _purchaseOrderTemplates = new List<PurchaseOrderTemplate>();
        [RelationshipList("345649FE-612C-44C7-9875-9004D2420C75", nameof(PurchaseOrderTemplate.PurchaseOrderID), AutoDeleteReferences = true)]
        public IReadOnlyCollection<PurchaseOrderTemplate> PurchaseOrderTemplates
        {
            get { CheckGet(); return _purchaseOrderTemplates; }
        }

        private List<PurchaseOrder> _purchaseOrderClones = new List<PurchaseOrder>();
        [RelationshipList("C2E037F7-07F1-4F48-A459-89EE5771EC8B", nameof(PurchaseOrderIDClonedFrom), AutoRemoveReferences = true)]
        public IReadOnlyCollection<PurchaseOrder> PurchaseOrderClones
        {
            get { CheckGet(); return _purchaseOrderClones; }
        }
        #endregion
        #endregion

        public static class SaveFlags
        {
            public static readonly Guid V_StatusChange = new Guid("5456CAE0-A7C9-43E1-842F-0CE3292710D3");
        }
    }
}
