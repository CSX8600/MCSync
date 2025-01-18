using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClussPro.Base.Data;
using ClussPro.Base.Data.Conditions;
using ClussPro.Base.Data.Operand;
using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.Schema;
using ClussPro.ObjectBasedFramework.Schema.Attributes;
using ClussPro.ObjectBasedFramework.Validation.Attributes;
using WebModels.company;
using WebModels.gov;
using WebModels.invoicing;

namespace WebModels.fleet
{
    [Table("1AE60907-D2E1-41E2-8A83-ECCBCCFDE4EE")]
    public class RailcarLocationTransaction : DataObject
    {
        protected RailcarLocationTransaction() : base() { }

        private long? _railcarLocationTransactionID;
        [Field("7A16493F-6E65-47F4-900E-A8906924FE5B")]
        public long? RailcarLocationTransactionID
        {
            get { CheckGet(); return _railcarLocationTransactionID; }
            set { CheckSet(); _railcarLocationTransactionID = value; }
        }

        private long? _railcarID;
        [Field("522B7C1D-8B85-4F79-99AB-9C544B580DAE")]
        [Required]
        public long? RailcarID
        {
            get { CheckGet(); return _railcarID; }
            set { CheckSet(); _railcarID = value; }
        }

        private Railcar _railcar = null;
        [Relationship("3D900125-A963-4F3A-B14A-24C647435524")]
        public Railcar Railcar
        {
            get { CheckGet(); return _railcar; }
        }

        private long? _trackIDNew;
        [Field("0D6E2013-4E8B-4881-BF3C-0EB9A591880E")]
        public long? TrackIDNew
        {
            get { CheckGet(); return _trackIDNew; }
            set { CheckSet(); _trackIDNew = value; }
        }

        private Track _trackNew = null;
        [Relationship("DF526231-1EB5-426B-A2DF-840510B80592", ForeignKeyField = nameof(TrackIDNew))]
        public Track TrackNew
        {
            get { CheckGet(); return _trackNew; }
        }

        private long? _trainIDNew;
        [Field("E88AF0F2-8ACB-46D8-A347-88F6F2609A2C")]
        public long? TrainIDNew
        {
            get { CheckGet(); return _trainIDNew; }
            set { CheckSet(); _trainIDNew = value; }
        }

        private Train _trainNew = null;
        [Relationship("C73C5130-8D0B-469A-93D5-75CCF161043B", ForeignKeyField = nameof(TrainIDNew))]
        public Train TrainNew
        {
            get { CheckGet(); return _trainNew; }
        }

        private bool _isPartialTrainTrip;
        [Field("217E7862-3B56-4BA1-B11A-61B53B89720C")]
        public bool IsPartialTrainTrip
        {
            get { CheckGet(); return _isPartialTrainTrip; }
            set { CheckSet(); _isPartialTrainTrip = value; }
        }

        private DateTime? _transactionTime;
        [Field("3932912A-D4C5-4520-B0C4-3DD77BFF2320", DataSize = 7)]
        [Required]
        public DateTime? TransactionTime
        {
            get { CheckGet(); return _transactionTime; }
            set { CheckSet(); _transactionTime = value; }
        }

        private RailcarLocationTransaction _previousTransaction = null;
        [Relationship("0FC674F9-886C-40D4-9D2E-B9E351076C0A", HasForeignKey = false)]
        public RailcarLocationTransaction PreviousTransaction
        {
            get { CheckGet(); return _previousTransaction; }
        }

        private RailcarLocationTransaction _nextTransaction = null;
        [Relationship("9CDB9A09-ABEC-4CB0-9775-913696D3D607", HasForeignKey = false)]
        public RailcarLocationTransaction NextTransaction
        {
            get { CheckGet(); return _nextTransaction; }
        }

        private Company _possessedByCompany = null;
        [Relationship("BD52B1BD-6649-4EFC-9354-91487A7A061A", HasForeignKey = false)]
        public Company PossessedByCompany
        {
            get { CheckGet(); return _possessedByCompany; }
        }

        private Government _possessedByGovernment = null;
        [Relationship("73A26C2C-2536-4EDE-9D4A-444CD99C0C8F", HasForeignKey = false)]
        public Government PossessedByGovernment
        {
            get { CheckGet(); return _possessedByGovernment; }
        }

        public override ICondition GetRelationshipCondition(Relationship relationship, string myAlias, string otherAlias)
        {
            switch(relationship.RelationshipName)
            {
                case nameof(PreviousTransaction):
                    return GetPreviousTransactionCondition(myAlias, otherAlias);
                case nameof(NextTransaction):
                    return GetNextTransactionCondition(myAlias, otherAlias);
                case nameof(PossessedByCompany):
                    return GetPossesedByCompanyCondition(myAlias, otherAlias);
                case nameof(PossessedByGovernment):
                    return GetPossesedByGovernmentCondition(myAlias, otherAlias);
                default:
                    return base.GetRelationshipCondition(relationship, myAlias, otherAlias);

            }
        }

        private ICondition GetPreviousTransactionCondition(string myAlias, string otherAlias)
        {
            ISelectQuery subQuery = SQLProviderFactory.GetSelectQuery();
            subQuery.Table = new Table("fleet", "RailcarLocationTransaction", "nextTransSubQuery");
            subQuery.SelectList = new List<Select>() { new Select() { SelectOperand = (ClussPro.Base.Data.Operand.Field)"nextTransSubQuery.RailcarLocationTransactionID" } };
            subQuery.PageSize = 1;
            subQuery.WhereCondition = new ConditionGroup()
            {
                ConditionGroupType = ConditionGroup.ConditionGroupTypes.And,
                Conditions = new List<ICondition>()
                {
                    new Condition()
                    {
                        Left = (ClussPro.Base.Data.Operand.Field)$"{myAlias}.RailcarID",
                        ConditionType = Condition.ConditionTypes.Equal,
                        Right = (ClussPro.Base.Data.Operand.Field)$"nextTransSubQuery.RailcarID"
                    },
                    new Condition()
                    {
                        Left = (ClussPro.Base.Data.Operand.Field)$"nextTransSubQuery.TransactionTime",
                        ConditionType = Condition.ConditionTypes.Less,
                        Right = (ClussPro.Base.Data.Operand.Field)$"{myAlias}.TransactionTime"
                    }
                }
            };
            subQuery.OrderByList = new List<Order>()
            {
                new Order()
                {
                    OrderDirection = Order.OrderDirections.Descending,
                    Field = $"{myAlias}.TransactionTime"
                }
            };

            return new Condition()
            {
                Left = (ClussPro.Base.Data.Operand.Field)$"{otherAlias}.RailcarLocationTransactionID",
                ConditionType = Condition.ConditionTypes.Equal,
                Right = new SubQuery(subQuery)
            };
        }

        private ICondition GetNextTransactionCondition(string myAlias, string otherAlias)
        {
            ISelectQuery subQuery = SQLProviderFactory.GetSelectQuery();
            subQuery.Table = new Table("fleet", "RailcarLocationTransaction", "nextTransSubQuery");
            subQuery.SelectList = new List<Select>() { new Select() { SelectOperand = (ClussPro.Base.Data.Operand.Field)"nextTransSubQuery.RailcarLocationTransactionID" } };
            subQuery.PageSize = 1;
            subQuery.WhereCondition = new ConditionGroup()
            {
                ConditionGroupType = ConditionGroup.ConditionGroupTypes.And,
                Conditions = new List<ICondition>()
                {
                    new Condition()
                    {
                        Left = (ClussPro.Base.Data.Operand.Field)$"{myAlias}.RailcarID",
                        ConditionType = Condition.ConditionTypes.Equal,
                        Right = (ClussPro.Base.Data.Operand.Field)$"nextTransSubQuery.RailcarID"
                    },
                    new Condition()
                    {
                        Left = (ClussPro.Base.Data.Operand.Field)$"nextTransSubQuery.TransactionTime",
                        ConditionType = Condition.ConditionTypes.Greater,
                        Right = (ClussPro.Base.Data.Operand.Field)$"{myAlias}.TransactionTime"
                    }
                }
            };
            subQuery.OrderByList = new List<Order>()
            {
                new Order() { Field = $"{myAlias}.TransactionTime" }
            };

            return new Condition()
            {
                Left = (ClussPro.Base.Data.Operand.Field)$"{otherAlias}.RailcarLocationTransactionID",
                ConditionType = Condition.ConditionTypes.Equal,
                Right = new SubQuery(subQuery)
            };
        }

        private ICondition GetPossesedByCompanyCondition(string myAlias, string otherAlias)
        {
            ISelectQuery subQuery = SQLProviderFactory.GetSelectQuery();
            subQuery.SelectList = new List<Select>() { new Select() { SelectOperand = new IsNull((ClussPro.Base.Data.Operand.Field)"LC.CompanyIDLessee", (ClussPro.Base.Data.Operand.Field)"R.CompanyIDOwner") } };
            subQuery.Table = new Table("fleet", "Railcar", "R");
            subQuery.PageSize = 1;
            subQuery.JoinList = new List<Join>()
            {
                new Join() { JoinType = Join.JoinTypes.Left, Table = new Table("fleet", "LeaseContract", "LC"), Condition = new ConditionGroup()
                    {
                        ConditionGroupType= ConditionGroup.ConditionGroupTypes.And,
                        Conditions = new List<ICondition>()
                        {
                            new Condition()
                            {
                                Left = (ClussPro.Base.Data.Operand.Field)"LC.RailcarID",
                                ConditionType = Condition.ConditionTypes.Equal,
                                Right = (ClussPro.Base.Data.Operand.Field)$"R.RailcarID"
                            },
                            new Condition()
                            {
                                Left = (ClussPro.Base.Data.Operand.Field)"LC.LeaseTimeStart",
                                ConditionType = Condition.ConditionTypes.GreaterEqual,
                                Right = (ClussPro.Base.Data.Operand.Field)$"{myAlias}.TransactionTime"
                            },
                            new Condition()
                            {
                                Left = (ClussPro.Base.Data.Operand.Field)"LC.LeaseTimeEnd",
                                ConditionType = Condition.ConditionTypes.LessEqual,
                                Right = (ClussPro.Base.Data.Operand.Field)$"{myAlias}.TransactionTime"
                            }
                        }
                    }
                }
            };
            subQuery.WhereCondition = new Condition()
            {
                Left = (ClussPro.Base.Data.Operand.Field)"R.RailcarID",
                ConditionType = Condition.ConditionTypes.Equal,
                Right = (ClussPro.Base.Data.Operand.Field)$"{myAlias}.RailcarID"
            };

            return new Condition()
            {
                Left = (ClussPro.Base.Data.Operand.Field)$"{otherAlias}.CompanyID",
                ConditionType = Condition.ConditionTypes.Equal,
                Right = new SubQuery(subQuery)
            };
        }

        private ICondition GetPossesedByGovernmentCondition(string myAlias, string otherAlias)
        {
            ISelectQuery subQuery = SQLProviderFactory.GetSelectQuery();
            subQuery.SelectList = new List<Select>() { new Select() { SelectOperand = new IsNull((ClussPro.Base.Data.Operand.Field)"LC.GovernmentIDLessee", (ClussPro.Base.Data.Operand.Field)"R.GovernmentIDOwner") } };
            subQuery.Table = new Table("fleet", "Railcar", "R");
            subQuery.PageSize = 1;
            subQuery.JoinList = new List<Join>()
            {
                new Join() { JoinType = Join.JoinTypes.Left, Table = new Table("fleet", "LeaseContract", "LC"), Condition = new ConditionGroup()
                    {
                        ConditionGroupType= ConditionGroup.ConditionGroupTypes.And,
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
                                Left = (ClussPro.Base.Data.Operand.Field)"LC.LeaseTimeStart",
                                ConditionType = Condition.ConditionTypes.GreaterEqual,
                                Right = (ClussPro.Base.Data.Operand.Field)$"{myAlias}.TransactionTime"
                            },
                            new Condition()
                            {
                                Left = (ClussPro.Base.Data.Operand.Field)"LC.LeaseTimeEnd",
                                ConditionType = Condition.ConditionTypes.LessEqual,
                                Right = (ClussPro.Base.Data.Operand.Field)$"{myAlias}.TransactionTime"
                            }
                        }
                    }
                }
            };
            subQuery.WhereCondition = new Condition()
            {
                Left = (ClussPro.Base.Data.Operand.Field)"R.RailcarID",
                ConditionType = Condition.ConditionTypes.Equal,
                Right = (ClussPro.Base.Data.Operand.Field)$"{myAlias}.RailcarID"
            };

            return new Condition()
            {
                Left = (ClussPro.Base.Data.Operand.Field)$"{otherAlias}.GovernmentID",
                ConditionType = Condition.ConditionTypes.Equal,
                Right = new SubQuery(subQuery)
            };
        }
    }
}
