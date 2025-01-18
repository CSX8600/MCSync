using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
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
using WebModels.company;
using WebModels.gov;
using WebModels.mesasys;
using WebModels.purchasing;

namespace WebModels.fleet
{
    [Table("9882F85A-BF24-4DA9-96A3-4DE7028ED44A")]
    public class Railcar : DataObject
    {
        public const decimal THUMBNAIL_WIDTH = 64M;
        protected Railcar() : base() { }

        private long? _railcarID;
        [Field("BC542631-BBAD-4577-9E3C-0AFD0E4A2864")]
        public long? RailcarID
        {
            get { CheckGet(); return _railcarID; }
            set { CheckSet(); _railcarID = value; }
        }

        private long? _railcarModelID;
        [Field("3D8E9CAC-11E8-4275-8927-AEC060C46EF8")]
        [Required]
        public long? RailcarModelID
        {
            get { CheckGet(); return _railcarModelID; }
            set { CheckSet(); _railcarModelID = value; }
        }

        private RailcarModel _railcarModel = null;
        [Relationship("7B567E44-8C75-479A-B9A9-ADAE6BB5A55C")]
        public RailcarModel RailcarModel
        {
            get { CheckGet(); return _railcarModel; }
        }

        private long? _governmentIDOwner;
        [Field("4BB5C98B-5F6E-4536-B5C8-39AD581379C9")]
        public long? GovernmentIDOwner
        {
            get { CheckGet(); return _governmentIDOwner; }
            set { CheckSet(); _governmentIDOwner = value; }
        }

        private Government _governmentOwner = null;
        [Relationship("D5ACE143-9B86-4044-84E4-E02D486FA9A0", ForeignKeyField = nameof(GovernmentIDOwner))]
        public Government GovernmentOwner
        {
            get { CheckGet(); return _governmentOwner; }
        }

        private long? _companyIDOwner;
        [Field("0C1ACEB6-F0CF-4B2D-BBB3-71A9A6A26CEA")]
        public long? CompanyIDOwner
        {
            get { CheckGet(); return _companyIDOwner; }
            set { CheckSet(); _companyIDOwner = value; }
        }

        private Company _companyOwner = null;
        [Relationship("A04C27FD-5DC3-4F94-A337-B6A71BC0AE1A", ForeignKeyField = nameof(CompanyIDOwner))]
        public Company CompanyOwner
        {
            get { CheckGet(); return _companyOwner; }
        }

        private long? _governmentIDPossessor;
        [Field("810AB8B1-BA64-4784-9C47-5D58156AB5D8")]
        public long? GovernmentIDPossessor
        {
            get { CheckGet(); return _governmentIDPossessor; }
            set { CheckSet(); _governmentIDPossessor = value; }
        }

        private Government _governmentPossessor = null;
        [Relationship("EA834C1C-DF05-45CB-A533-EDA8BE11AD5B", ForeignKeyField = nameof(GovernmentIDPossessor))]
        public Government GovernmentPossessor
        {
            get { CheckGet(); return _governmentPossessor; }
        }

        private long? _companyIDPossessor;
        [Field("2D08D958-14CD-465E-9DA4-ED4279972481")]
        public long? CompanyIDPossessor
        {
            get { CheckGet(); return _companyIDPossessor; }
            set { CheckSet(); _companyIDPossessor = value; }
        }

        private Company _companyPossessor = null;
        [Relationship("2D102BF4-4C55-432A-B634-F9632612A2BE", ForeignKeyField = nameof(CompanyIDPossessor))]
        public Company CompanyPossessor
        {
            get { CheckGet(); return _companyPossessor; }
        }

        private RailLocation _railLocation = null;
        [Relationship("527AEB8C-896E-4D8A-B8E2-95729BA611C4", OneToOneByForeignKey = true)]
        public RailLocation RailLocation
        {
            get { CheckGet(); return _railLocation; }
        }

        private string _reportingMark;
        [Field("DF38F559-C558-476E-B3D0-15828D80DAB0")]
        [Required]
        public string ReportingMark
        {
            get { CheckGet(); return _reportingMark; }
            set { CheckSet(); _reportingMark = value; }
        }

        private int? _reportingNumber;
        [Field("0EBE9191-DCA9-49A4-BE99-C0FEC9E27CAA")]
        [Required]
        public int? ReportingNumber
        {
            get { CheckGet(); return _reportingNumber; }
            set { CheckSet(); _reportingNumber = value; }
        }

        private byte[] _imageOverride;
        [Field("E3AC3FC5-C625-4BD2-9DB8-1E2AFDA60CCB")]
        public byte[] ImageOverride
        {
            get { CheckGet(); return _imageOverride; }
            set { CheckSet(); _imageOverride = value; }
        }

        private byte[] _imageOverrideThumbnail;
        [Field("69A4233C-6E0B-4389-8E81-6CB1C1C6B15E")]
        public byte[] ImageOverrideThumbnail
        {
            get { CheckGet(); return _imageOverrideThumbnail; }
            set { CheckSet(); _imageOverrideThumbnail = value; }
        }

        private long? _trackIDDestination;
        [Field("F6BB5870-968F-4926-A4B9-094ED05799DA")]
        public long? TrackIDDestination
        {
            get { CheckGet(); return _trackIDDestination; }
            set { CheckSet(); _trackIDDestination = value; }
        }

        private Track _trackDestination = null;
        [Relationship("3CBB617B-928D-4168-98FA-393342F8E46F", ForeignKeyField = nameof(TrackIDDestination))]
        public Track TrackDestination
        {
            get { CheckGet(); return _trackDestination; }
        }

        private long? _trackIDStrategic;
        [Field("C4E7C207-CC21-4FC6-B36D-B7E1B403CFFC")]
        public long? TrackIDStrategic
        {
            get { CheckGet(); return _trackIDStrategic; }
            set { CheckSet(); _trackIDStrategic = value; }
        }

        private Track _trackStrategic = null;
        [Relationship("71BC0DA0-46AC-4EFD-9CAF-62A8B338DCA3", ForeignKeyField = nameof(TrackIDStrategic))]
        public Track TrackStrategic
        {
            get { CheckGet(); return _trackStrategic; }
        }

        private bool _hasOpenBid = false;
        [Field("E1ACECA1-C305-4AAF-A4AE-DEBEE8DCEDCD", HasOperation = true)]
        public bool HasOpenBid
        {
            get { CheckGet(); return _hasOpenBid; }
        }

        public static OperationDelegate HasOpenBidOperation
        {
            get => (alias) =>
            {
                ISelectQuery selectQuery = SQLProviderFactory.GetSelectQuery();
                selectQuery.SelectList = new List<Select>()
                {
                    new Select()
                    {
                        SelectOperand = new Case()
                        {
                            Whens = new List<Case.When>()
                            {
                                new Case.When()
                                {
                                    Condition = new Condition()
                                    {
                                        Left = new Count((ClussPro.Base.Data.Operand.Field)$"RailcarID"),
                                        ConditionType = Condition.ConditionTypes.Greater,
                                        Right = new Literal(0)
                                    },
                                    Result = new Literal(true)
                                }
                            },
                            Else = new Literal(false)
                        }
                    }
                };
                selectQuery.Table = new Table("fleet", "LeaseBid");
                selectQuery.WhereCondition = new Condition()
                {
                    Left = (ClussPro.Base.Data.Operand.Field)"RailcarID",
                    ConditionType = Condition.ConditionTypes.Equal,
                    Right = (ClussPro.Base.Data.Operand.Field)$"{alias}.RailcarID"
                };

                return new SubQuery(selectQuery);
            };
        }

        protected override bool PreSave(ITransaction transaction)
        {
            if (IsFieldDirty(nameof(ImageOverride)))
            {
                using (MemoryStream inputStream = new MemoryStream(ImageOverride))
                using (Image image = Image.FromStream(inputStream))
                {
                    int height = (int)(image.Height * (THUMBNAIL_WIDTH / image.Width));
                    using (Image thumbnail = image.GetThumbnailImage((int)THUMBNAIL_WIDTH, height, () => false, IntPtr.Zero))
                    using (MemoryStream outputStream = new MemoryStream())
                    {
                        thumbnail.Save(outputStream, ImageFormat.Png);
                        outputStream.Position = 0;

                        ImageOverrideThumbnail = new byte[outputStream.Length];
                        outputStream.Read(ImageOverrideThumbnail, 0, (int)outputStream.Length);
                    }
                }
            }

            return base.PreSave(transaction);
        }

        protected override bool PostSave(ITransaction transaction)
        {
            if (IsInsert)
            {
                RailLocation newRailLocation = DataObjectFactory.Create<RailLocation>();
                newRailLocation.RailcarID = RailcarID;
                newRailLocation.Position = int.MaxValue;
                if (!newRailLocation.Save(transaction, new List<System.Guid>() { RailLocation.ValidationIDs.TrackOrTrainRequired }))
                {
                    Errors.AddRange(newRailLocation.Errors.ToArray());
                    return false;
                }
            }
            else
            {
                bool carWasReleased = false;

                if (IsFieldDirty(nameof(CompanyIDPossessor)) && CompanyIDPossessor != null)
                {
                    MiscellaneousSettings settings = new Search<MiscellaneousSettings>(new LongSearchCondition<MiscellaneousSettings>()
                    {
                        Field = nameof(MiscellaneousSettings.CompanyID),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = CompanyIDPossessor
                    }).GetReadOnly(transaction, new[] { nameof(MiscellaneousSettings.EmailImplementationIDCarReleased) });
                    if (settings?.EmailImplementationIDCarReleased != null)
                    {
                        EmailImplementation emailImplementation = DataObject.GetEditableByPrimaryKey<EmailImplementation>(settings.EmailImplementationIDCarReleased, transaction, null);
                        emailImplementation.SendEmail<Railcar>(RailcarID, transaction);
                    }

                    carWasReleased = true;
                }

                if (IsFieldDirty(nameof(GovernmentIDPossessor)) && GovernmentIDPossessor != null)
                {
                    MiscellaneousSettings settings = new Search<MiscellaneousSettings>(new LongSearchCondition<MiscellaneousSettings>()
                    {
                        Field = nameof(MiscellaneousSettings.GovernmentID),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = GovernmentIDPossessor
                    }).GetReadOnly(transaction, new[] { nameof(MiscellaneousSettings.EmailImplementationIDCarReleased) });
                    if (settings?.EmailImplementationIDCarReleased != null)
                    {
                        EmailImplementation emailImplementation = DataObject.GetEditableByPrimaryKey<EmailImplementation>(settings.EmailImplementationIDCarReleased, transaction, null);
                        emailImplementation.SendEmail<Railcar>(RailcarID, transaction);
                    }

                    carWasReleased = true;
                }

                if (carWasReleased && !HandleRouteAndBOLUpdates(transaction))
                {
                    return false;
                }
            }
            return base.PostSave(transaction);
        }

        private bool HandleRouteAndBOLUpdates(ITransaction transaction)
        {
            Search<RailcarRoute> railcarRouteSearch = new Search<RailcarRoute>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                                    new LongSearchCondition<RailcarRoute>()
                                    {
                                        Field = nameof(RailcarRoute.RailcarID),
                                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                        Value = RailcarID
                                    },
                                    new ByteSearchCondition<RailcarRoute>()
                                    {
                                        Field = nameof(RailcarRoute.SortOrder),
                                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                        Value = 1
                                    }));

            RailcarRoute nextRoute = railcarRouteSearch.GetEditable(transaction);
            if (nextRoute != null &&
                (nextRoute.CompanyIDFrom == GetDirtyValue(nameof(CompanyIDPossessor)) as long? || nextRoute.GovernmentIDFrom == GetDirtyValue(nameof(GovernmentIDPossessor)) as long?) &&
                (nextRoute.CompanyIDTo == CompanyIDPossessor || nextRoute.GovernmentIDTo == GovernmentIDPossessor))
            {
                if (!nextRoute.Delete(transaction))
                {
                    Errors.AddRange(nextRoute.Errors.ToArray());
                    return false;
                }

                Search<RailcarRoute> nextRailcarRoutes = new Search<RailcarRoute>(new LongSearchCondition<RailcarRoute>()
                {
                    Field = nameof(RailcarRoute.RailcarID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = nextRoute.RailcarID
                });

                byte i = 0;
                nextRoute = null;
                foreach(RailcarRoute route in nextRailcarRoutes.GetEditableReader(transaction).OrderBy(r => r.SortOrder))
                {
                    route.SortOrder = ++i;
                    nextRoute = nextRoute ?? route;
                    if (!route.Save(transaction))
                    {
                        Errors.AddRange(route.Errors.ToArray());
                        return false;
                    }
                }

                Search<BillOfLading> existingBillOfLadingSearch = new Search<BillOfLading>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                    new LongSearchCondition<BillOfLading>()
                    {
                        Field = nameof(BillOfLading.RailcarID),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = RailcarID
                    },
                    new DateTimeSearchCondition<BillOfLading>()
                    {
                        Field = nameof(BillOfLading.DeliveredDate),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Null
                    }));

                List<string> itemFields = Schema.GetSchemaObject<BillOfLadingItem>().GetFields().Select(f => nameof(BillOfLading.BillOfLadingItems) + "." + f.FieldName).ToList();
                BillOfLading existingBillOfLading = existingBillOfLadingSearch.GetEditable(transaction, itemFields);
                if (existingBillOfLading != null && !existingBillOfLading.Type.HasFlag(BillOfLading.Types.LastMile)) // Last mile BoLs must be manually accepted
                {
                    existingBillOfLading.DeliveredDate = DateTime.Now;
                    if (!existingBillOfLading.Save(transaction))
                    {
                        Errors.AddRange(existingBillOfLading.Errors.ToArray());
                        return false;
                    }

                    BillOfLading newBillOfLading = DataObjectFactory.Create<BillOfLading>();
                    existingBillOfLading.Copy(newBillOfLading);
                    newBillOfLading.DeliveredDate = null;
                    newBillOfLading.CompanyIDCarrier = nextRoute.CompanyIDTo;
                    newBillOfLading.GovernmentIDCarrier = nextRoute.GovernmentIDTo;
                    if (!newBillOfLading.Save(transaction))
                    {
                        Errors.AddRange(newBillOfLading.Errors.ToArray());
                        return false;
                    }

                    foreach(BillOfLadingItem item in existingBillOfLading.BillOfLadingItems)
                    {
                        BillOfLadingItem newItem = DataObjectFactory.Create<BillOfLadingItem>();
                        item.Copy(newItem);
                        newItem.BillOfLadingID = newBillOfLading.BillOfLadingID;

                        if (!newItem.Save(transaction))
                        {
                            Errors.AddRange(newItem.Errors.ToArray());
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public bool CompleteReceiving(ITransaction transaction)
        {
            if (!IsEditable)
            {
                throw new InvalidOperationException("Railcar must be editable to complete receiving.");
            }

            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction), "Transaction cannot be null.");
            }

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
                    Condition = new DecimalSearchCondition<FulfillmentPlanPurchaseOrderLine>()
                    {
                        Field = FieldPathUtility.CreateFieldPathsAsList<FulfillmentPlanPurchaseOrderLine>(fppol => new List<object>() { fppol.PurchaseOrderLine.RemainingQuantity }).First(),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Greater,
                        Value = 0
                    }
                }));

            List<string> fulfillmentPlanFields = FieldPathUtility.CreateFieldPathsAsList<FulfillmentPlan>(fp => new List<object>()
            {
                fp.TrackIDLoading,
                fp.TrackIDStrategicAfterDestination,
                fp.FulfillmentPlanRoutes.First().SortOrder,
                fp.FulfillmentPlanRoutes.First().CompanyIDFrom,
                fp.FulfillmentPlanRoutes.First().GovernmentIDFrom,
                fp.FulfillmentPlanRoutes.First().CompanyIDTo,
                fp.FulfillmentPlanRoutes.First().GovernmentIDTo
            });

            FulfillmentPlan fulfillmentPlan = fulfillmentPlanSearch.GetEditable(transaction, fulfillmentPlanFields);
            if (fulfillmentPlan != null) // Return to shipper
            {
                TrackIDDestination = fulfillmentPlan.TrackIDLoading;
                TrackIDStrategic = fulfillmentPlan.TrackIDStrategicAfterDestination;

                if (!Save(transaction))
                {
                    return false;
                }

                Search<RailcarRoute> deleteRouteSearch = new Search<RailcarRoute>(new LongSearchCondition<RailcarRoute>()
                {
                    Field = nameof(RailcarRoute.RailcarID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = RailcarID
                });
                foreach(RailcarRoute route in deleteRouteSearch.GetEditableReader(transaction))
                {
                    if (!route.Delete(transaction))
                    {
                        Errors.AddRange(route.Errors.ToArray());
                        return false;
                    }
                }

                for(int i = 0; i < (fulfillmentPlan.FulfillmentPlanRoutes?.Count ?? 0); i++)
                {
                    FulfillmentPlanRoute planRoute = fulfillmentPlan.FulfillmentPlanRoutes.OrderByDescending(fpr => fpr.SortOrder).ElementAt(i);

                    RailcarRoute newRoute = DataObjectFactory.Create<RailcarRoute>();
                    newRoute.RailcarID = RailcarID;
                    newRoute.SortOrder = (byte)i;
                    newRoute.CompanyIDFrom = planRoute.CompanyIDTo;
                    newRoute.GovernmentIDFrom = planRoute.GovernmentIDTo;
                    newRoute.CompanyIDTo = planRoute.CompanyIDFrom;
                    newRoute.GovernmentIDTo = planRoute.GovernmentIDFrom;

                    if (!newRoute.Save(transaction))
                    {
                        Errors.AddRange(newRoute.Errors.ToArray());
                        return false;
                    }
                }
            }
            else // Attempt to locate fulfillment plan by purchase order and send it to post fulfillment track. Also end lease contract
            {
                Search<PurchaseOrderLine> purchaseOrderLineSearch = new Search<PurchaseOrderLine>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                    new ExistsSearchCondition<PurchaseOrderLine>()
                    {
                        RelationshipName = nameof(PurchaseOrderLine.FulfillmentPlanPurchaseOrderLines),
                        ExistsType = ExistsSearchCondition<PurchaseOrderLine>.ExistsTypes.Exists,
                        Condition = new LongSearchCondition<FulfillmentPlanPurchaseOrderLine>()
                        {
                            Field = FieldPathUtility.CreateFieldPathsAsList<FulfillmentPlanPurchaseOrderLine>(fppol => new List<object>() { fppol.FulfillmentPlan.RailcarID }).First(),
                            SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                            Value = RailcarID
                        }
                    }));

                purchaseOrderLineSearch.SearchOrders = new List<SearchOrder>()
                {
                    new SearchOrder()
                    {
                        OrderField = FieldPathUtility.CreateFieldPathsAsList<PurchaseOrderLine>(pol => new List<object>() { pol.PurchaseOrder.PurchaseOrderDate }).First(),
                        OrderDirection = SearchOrder.OrderDirections.Descending
                    }
                };

                List<string> purchaseOrderLineFields = FieldPathUtility.CreateFieldPathsAsList<PurchaseOrderLine>(pol => new List<object>()
                {
                    pol.FulfillmentPlanPurchaseOrderLines.First().FulfillmentPlan.RailcarID,
                    pol.FulfillmentPlanPurchaseOrderLines.First().FulfillmentPlan.TrackIDPostFulfillment,
                    pol.FulfillmentPlanPurchaseOrderLines.First().FulfillmentPlan.FulfillmentPlanRoutes.First().SortOrder,
                    pol.FulfillmentPlanPurchaseOrderLines.First().FulfillmentPlan.FulfillmentPlanRoutes.First().CompanyIDFrom,
                    pol.FulfillmentPlanPurchaseOrderLines.First().FulfillmentPlan.FulfillmentPlanRoutes.First().GovernmentIDFrom
                });

                PurchaseOrderLine purchaseOrderLine = purchaseOrderLineSearch.GetReadOnly(transaction, purchaseOrderLineFields);

                if (purchaseOrderLine != null)
                {
                    fulfillmentPlan = purchaseOrderLine.FulfillmentPlanPurchaseOrderLines.First(fppol => fppol.FulfillmentPlan.RailcarID == RailcarID).FulfillmentPlan;
                    TrackIDDestination = fulfillmentPlan.TrackIDPostFulfillment;

                    if (fulfillmentPlan.FulfillmentPlanRoutes?.Count >= 2)
                    {
                        FulfillmentPlanRoute finalRoute = fulfillmentPlan.FulfillmentPlanRoutes.OrderByDescending(fpr => fpr.SortOrder).First();
                        GovernmentIDPossessor = finalRoute.GovernmentIDFrom;
                        CompanyIDPossessor = finalRoute.CompanyIDFrom;
                    }

                    if (!Save(transaction))
                    {
                        return false;
                    }
                }

                Search<LeaseContract> leaseContractSearch = new Search<LeaseContract>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                    new LongSearchCondition<LeaseContract>()
                    {
                        Field = nameof(LeaseContract.RailcarID),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = RailcarID
                    },
                    new DateTimeSearchCondition<LeaseContract>()
                    {
                        Field = nameof(LeaseContract.LeaseTimeEnd),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Null
                    }));

                LeaseContract leaseContract = leaseContractSearch.GetEditable(transaction);
                if (leaseContract != null)
                {
                    leaseContract.LeaseTimeEnd = DateTime.Now;

                    if (!leaseContract.Save(transaction))
                    {
                        Errors.AddRange(leaseContract.Errors.ToArray());
                        return false;
                    }
                }
            }

            return true;
        }

        protected override bool PreDelete(ITransaction transaction)
        {
            RailLocation railLocation = new Search<RailLocation>(new LongSearchCondition<RailLocation>()
            {
                Field = "RailcarID",
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = RailcarID
            }).GetEditable(transaction, null);

            if (railLocation != null)
            {
                if (!railLocation.Delete(transaction))
                {
                    Errors.AddRange(railLocation.Errors.ToArray());
                    return false;
                }
            }
            return base.PreDelete(transaction);
        }

        #region Custom Relationships
        public override ICondition GetRelationshipCondition(Relationship relationship, string myAlias, string otherAlias)
        {
            switch (relationship.RelationshipName)
            {
                case nameof(CompanyLeasedTo):
                    return CompanyLeasedToCondition(myAlias, otherAlias);
                case nameof(GovernmentLeasedTo):
                    return GovernmentLeasedToCondition(myAlias, otherAlias);
            }

            return base.GetRelationshipCondition(relationship, myAlias, otherAlias);
        }

        private Company _companyLeasedTo;
        [Relationship("46ECFAC8-D234-4A5A-84DD-4B491D23FF7C", HasForeignKey = false)]
        public Company CompanyLeasedTo
        {
            get { CheckGet(); return _companyLeasedTo; }
        }

        private ICondition CompanyLeasedToCondition(string myAlias, string otherAlias)
        {
            ISelectQuery select = SQLProviderFactory.GetSelectQuery();
            select.SelectList = new List<Select>()
            {
                new Select() { SelectOperand = (ClussPro.Base.Data.Operand.Field)"LC.CompanyIDLessee" }
            };
            select.Table = new Table("fleet", "LeaseContract", "LC");
            select.PageSize = 1;
            select.WhereCondition = new ConditionGroup()
            {
                ConditionGroupType = ConditionGroup.ConditionGroupTypes.And,
                Conditions = new List<ICondition>()
                {
                    new Condition()
                    {
                        Left = (ClussPro.Base.Data.Operand.Field)"LC.RailcarID",
                        ConditionType = Condition.ConditionTypes.Equal,
                        Right = (ClussPro.Base.Data.Operand.Field)$"{myAlias}.RailcarID"
                    },
                    new Condition()
                    {
                        Left = (ClussPro.Base.Data.Operand.Field)"LC.LeaseTimeEnd",
                        ConditionType = Condition.ConditionTypes.Null
                    }
                }
            };

            return new Condition()
            {
                Left = new SubQuery(select),
                ConditionType = Condition.ConditionTypes.Equal,
                Right = (ClussPro.Base.Data.Operand.Field)$"{otherAlias}.CompanyID"
            };
        }

        private Government _governmentLeasedTo;
        [Relationship("32BFE4A3-8E92-4CE7-9A7C-028EAB85E2DE", HasForeignKey = false)]
        public Government GovernmentLeasedTo
        {
            get { CheckGet(); return _governmentLeasedTo; }
        }

        private ICondition GovernmentLeasedToCondition(string myAlias, string otherAlias)
        {
            ISelectQuery select = SQLProviderFactory.GetSelectQuery();
            select.SelectList = new List<Select>()
            {
                new Select() { SelectOperand = (ClussPro.Base.Data.Operand.Field)"LC.GovernmentIDLessee" }
            };
            select.Table = new Table("fleet", "LeaseContract", "LC");
            select.PageSize = 1;
            select.WhereCondition = new ConditionGroup()
            {
                ConditionGroupType = ConditionGroup.ConditionGroupTypes.And,
                Conditions = new List<ICondition>()
                {
                    new Condition()
                    {
                        Left = (ClussPro.Base.Data.Operand.Field)"LC.RailcarID",
                        ConditionType = Condition.ConditionTypes.Equal,
                        Right = (ClussPro.Base.Data.Operand.Field)$"{myAlias}.RailcarID"
                    },
                    new Condition()
                    {
                        Left = (ClussPro.Base.Data.Operand.Field)"LC.LeaseTimeEnd",
                        ConditionType = Condition.ConditionTypes.Null
                    }
                }
            };

            return new Condition()
            {
                Left = new SubQuery(select),
                ConditionType = Condition.ConditionTypes.Equal,
                Right = (ClussPro.Base.Data.Operand.Field)$"{otherAlias}.GovernmentID"
            };
        }
        #endregion

        #region Relationships
        #region fleet
        private List<LeaseBid> _leaseBids = new List<LeaseBid>();
        [RelationshipList("0F70EFD5-AF06-4301-A5AD-48B7F7282AB0", nameof(LeaseBid.RailcarID))]
        public IReadOnlyCollection<LeaseBid> LeaseBids
        {
            get { CheckGet(); return _leaseBids; }
        }

        private List<LeaseContract> _leaseContracts = new List<LeaseContract>();
        [RelationshipList("DAF79BB5-BDD8-473B-B1E9-2DCEA47946D0", nameof(LeaseContract.RailcarID))]
        public IReadOnlyCollection<LeaseContract> LeaseContracts
        {
            get { CheckGet(); return _leaseContracts; }
        }

        private List<RailcarLocationTransaction> _railcarLocationTransactions = new List<RailcarLocationTransaction>();
        [RelationshipList("FCF6E1FE-F852-4D1D-B6C9-1816E07CD304", nameof(RailcarLocationTransaction.RailcarID))]
        public IReadOnlyCollection<RailcarLocationTransaction> RailcarLocationTransactions
        {
            get { CheckGet(); return _railcarLocationTransactions; }
        }

        private List<RailcarLoad> _railcarLoads = new List<RailcarLoad>();
        [RelationshipList("EFF663A4-8F95-41A9-A864-8C58EB06C358", nameof(RailcarLoad.RailcarID))]
        public IReadOnlyCollection<RailcarLoad> RailcarLoads
        {
            get { CheckGet(); return _railcarLoads; }
        }

        private List<RailcarRoute> _railcarRoutes = new List<RailcarRoute>();
        [RelationshipList("FF06FFCD-873C-4EAE-9DAC-FC06139E05AB", nameof(RailcarRoute.RailcarID), AutoDeleteReferences = true)]
        public IReadOnlyCollection<RailcarRoute> RailcarRoutes
        {
            get { CheckGet(); return _railcarRoutes; }
        }
        #endregion
        #region purchasing
        private List<FulfillmentPlan> _fulfillmentPlans = new List<FulfillmentPlan>();
        [RelationshipList("D529289D-CD90-43F1-BDFA-F55A6889CC1A", nameof(FulfillmentPlan.RailcarID))]
        public IReadOnlyCollection<FulfillmentPlan> FulfillmentPlans
        {
            get { CheckGet(); return _fulfillmentPlans; }
        }

        private List<BillOfLading> _billsOfLading = new List<BillOfLading>();
        [RelationshipList("134EBF5C-C067-4C35-9A68-9BC69D9D4790", nameof(BillOfLading.RailcarID))]
        public IReadOnlyCollection<BillOfLading> BillsOfLading
        {
            get { CheckGet(); return _billsOfLading; }
        }
        #endregion
        #endregion
    }
}
