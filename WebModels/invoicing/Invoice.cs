using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ClussPro.Base.Data;
using ClussPro.Base.Data.Conditions;
using ClussPro.Base.Data.Operand;
using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Schema.Attributes;
using ClussPro.ObjectBasedFramework.Utility;
using ClussPro.ObjectBasedFramework.Validation.Attributes;
using WebModels.account;
using WebModels.company;
using WebModels.fleet;
using WebModels.gov;
using WebModels.invoicing.Attributes;
using WebModels.mesasys;
using WebModels.purchasing;

namespace WebModels.invoicing
{
    [Table("0EB0BE0D-2853-4D8E-927D-FA69176BAA8D")]
    public class Invoice : DataObject
    {
        protected Invoice() : base() { }

        private long? _invoiceID;
        [Field("11769A34-759B-4192-9BF8-1372D0319AC8")]
        public long? InvoiceID
        {
            get { CheckGet(); return _invoiceID; }
            set { CheckSet(); _invoiceID = value; }
        }

        private long? _governmentIDFrom;
        [Field("62330C50-6839-424E-AEED-93A84A5A789D")]
        [SentPermission(SentPermissionAttribute.SenderTypes.Recipient)]
        public long? GovernmentIDFrom
        {
            get { CheckGet(); return _governmentIDFrom; }
            set { CheckSet(); _governmentIDFrom = value; }
        }

        private Government _governmentFrom = null;
        [Relationship("AA91092D-F505-4E43-BCBC-654084563CFD", ForeignKeyField = nameof(GovernmentIDFrom))]
        public Government GovernmentFrom
        {
            get { CheckGet(); return _governmentFrom; }
        }

        private long? _locationIDFrom;
        [Field("80E15F51-CBDC-4BC2-9E4D-DA6083CEE91E")]
        [SentPermission(SentPermissionAttribute.SenderTypes.Recipient)]
        public long? LocationIDFrom
        {
            get { CheckGet(); return _locationIDFrom; }
            set { CheckSet(); _locationIDFrom = value; }
        }

        private Location _locationFrom = null;
        [Relationship("024A40BC-74B0-429D-8982-6327C36FE05A", ForeignKeyField = nameof(LocationIDFrom))]
        public Location LocationFrom
        {
            get { CheckGet(); return _locationFrom; }
        }

        private long? _governmentIDTo;
        [Field("5BA54999-723C-41B1-8D14-A56241031E0F")]
        [SentPermission(SentPermissionAttribute.SenderTypes.Recipient)]
        public long? GovernmentIDTo
        {
            get { CheckGet(); return _governmentIDTo; }
            set { CheckSet(); _governmentIDTo = value; }
        }

        private Government _governmentTo = null;
        [Relationship("F4702ABC-4AAF-41A2-B669-5E4B9547B58E", ForeignKeyField = nameof(GovernmentIDTo))]
        public Government GovernmentTo
        {
            get { CheckGet(); return _governmentTo; }
        }

        private long? _locationIDTo;
        [Field("7705AACE-7EFD-457D-92A2-307CAD435E09")]
        [SentPermission(SentPermissionAttribute.SenderTypes.Recipient)]
        public long? LocationIDTo
        {
            get { CheckGet(); return _locationIDTo; }
            set { CheckSet(); _locationIDTo = value; }
        }

        private Location _locationTo = null;
        [Relationship("D2882202-3CC6-4FD1-80DE-9051CE6FFA9D", ForeignKeyField = nameof(LocationIDTo))]
        public Location LocationTo
        {
            get { CheckGet(); return _locationTo; }
        }

        private long? _purchaseOrderID;
        [Field("4FB5C1D5-4FCF-4CE6-8787-2E31E1C9DF0D")]
        public long? PurchaseOrderID
        {
            get { CheckGet(); return _purchaseOrderID; }
            set { CheckSet(); _purchaseOrderID = value; }
        }

        private PurchaseOrder _purchaseOrder = null;
        [Relationship("363B082A-AB35-4901-9190-BD0D72BA0DE9")]
        public PurchaseOrder PurchaseOrder
        {
            get { CheckGet(); return _purchaseOrder; }
        }

        private string _invoiceNumber;
        [Field("30F4F4AC-8503-48D7-8377-BD847E174A49", DataSize = 11)]
        [Required]
        [SentPermission(SentPermissionAttribute.SenderTypes.Recipient)]
        public string InvoiceNumber
        {
            get { CheckGet(); return _invoiceNumber; }
            set { CheckSet(); _invoiceNumber = value; }
        }

        private string _description;
        [Field("B6152DA9-0BDE-43B0-B428-49689E6BF21E", DataSize = 300)]
        [Required]
        [SentPermission(SentPermissionAttribute.SenderTypes.Recipient)]
        public string Description
        {
            get { CheckGet(); return _description; }
            set { CheckSet(); _description = value; }
        }

        private DateTime? _invoiceDate;
        [Field("0B19B097-5619-4C48-8C8B-B04F27CDB4C0", DataSize = 7)]
        [Required]
        [SentPermission(SentPermissionAttribute.SenderTypes.Recipient)]
        public DateTime? InvoiceDate
        {
            get { CheckGet(); return _invoiceDate; }
            set { CheckSet(); _invoiceDate = value; }
        }

        private DateTime? _dueDate;
        [Field("D53E474E-D7C2-4A09-B6A5-1A10070BDC9F", DataSize = 7)]
        [SentPermission(SentPermissionAttribute.SenderTypes.Recipient)]
        public DateTime? DueDate
        {
            get { CheckGet(); return _dueDate; }
            set { CheckSet(); _dueDate = value; }
        }

        public enum Statuses
        {
            WorkInProgress,
            Sent,
            ReadyForReceipt,
            Complete
        }

        private Statuses _status = Statuses.WorkInProgress;
        [Field("A889CD9D-34B1-42F8-B8C6-EF1D9C7AB834")]
        [Required]
        public Statuses Status
        {
            get { CheckGet(); return _status; }
            set { CheckSet(); _status = value; }
        }

        private long? _accountIDFrom;
        [Field("90F797A8-260C-4DF9-BDC2-D8903FC99921")]
        [SentPermission(SentPermissionAttribute.SenderTypes.Sender)]
        public long? AccountIDFrom
        {
            get { CheckGet(); return _accountIDFrom; }
            set { CheckSet(); _accountIDFrom = value; }
        }

        private Account _accountFrom = null;
        [Relationship("FF407B7F-7143-4CD2-8B2E-89163D98A0AA", ForeignKeyField = nameof(AccountIDFrom))]
        public Account AccountFrom
        {
            get { CheckGet(); return _accountFrom; }
        }

        private string _accountFromHistorical;
        [Field("2A433479-1DE9-4BC8-863D-B38E361C2304", DataSize = 69)]
        [SentPermission(SentPermissionAttribute.SenderTypes.Recipient)]
        public string AccountFromHistorical
        {
            get { CheckGet(); return _accountFromHistorical; }
            set { CheckSet(); _accountFromHistorical = value; }
        }

        private long? _accountIDTo;
        [Field("95AEABA8-2F38-41F3-BADC-567371799282")]
        [SentPermission(SentPermissionAttribute.SenderTypes.Recipient)]
        public long? AccountIDTo
        {
            get { CheckGet(); return _accountIDTo; }
            set { CheckSet(); _accountIDTo = value;}
        }

        private Account _accountTo = null;
        [Relationship("D598522C-227E-4414-A775-6D78F3B7D38E", ForeignKeyField = nameof(AccountIDTo))]
        public Account AccountTo
        {
            get { CheckGet(); return _accountTo; }
        }

        private string _accountToHistorical;
        [Field("585999BD-71D9-43D3-BFFA-E1973CC1D5B6")]
        [SentPermission(SentPermissionAttribute.SenderTypes.Recipient)]
        public string AccountToHistorical
        {
            get { CheckGet(); return _accountToHistorical; }
            set { CheckSet(); _accountToHistorical = value; }
        }

        private bool _autoReceive;
        [Field("F8069244-2AC4-4E90-B77A-7201DBA93DD8")]
        public bool AutoReceive
        {
            get { CheckGet(); return _autoReceive; }
            set { CheckSet(); _autoReceive = value; }
        }

        private decimal? _amount = null;
        [Field("AFC90715-B402-4337-965E-BA594A3E86E6", HasOperation = true)]
        public decimal? Amount
        {
            get { CheckGet(); return _amount; }
        }

        public static OperationDelegate AmountOperation => alias =>
        {
            ISelectQuery select = SQLProviderFactory.GetSelectQuery();
            select.SelectList = new List<Select>()
            {
                new Select()
                {
                    SelectOperand = new Sum((Field)"detail.Total")
                }
            };
            select.Table = new Table("invoicing", "InvoiceLine", "detail");
            select.WhereCondition = new Condition()
            {
                Left = (Field)"detail.InvoiceID",
                ConditionType = Condition.ConditionTypes.Equal,
                Right = (Field)$"{alias}.InvoiceID"
            };

            return new SubQuery(select);
        };

        #region Relationships
        #region fleet
        private List<fleet.LeaseContractInvoice> _leaseContractInvoices = new List<fleet.LeaseContractInvoice>();
        [RelationshipList("BC306004-B909-4E56-9BE4-C5153904350B", nameof(fleet.LeaseContractInvoice.InvoiceID), AutoDeleteReferences = true)]
        public IReadOnlyCollection<fleet.LeaseContractInvoice> LeaseContractInvoices
        {
            get { CheckGet(); return _leaseContractInvoices; }
        }
        #endregion
        #region invoicing
        private List<InvoiceLine> _invoiceLines = new List<InvoiceLine>();
        [RelationshipList("151E4019-12B6-47B3-9F62-3F5B9B5F7A4D", "InvoiceID", AutoDeleteReferences = true)]
        public IReadOnlyCollection<InvoiceLine> InvoiceLines
        {
            get { CheckGet(); return _invoiceLines;}
        }

        private List<InvoiceSalesTax> _invoiceSalesTaxes = new List<InvoiceSalesTax>();
        [RelationshipList("C0A1D2DB-B35A-446F-AF5C-3CF18C2D043A", "InvoiceID")]
        public IReadOnlyCollection<InvoiceSalesTax> InvoiceSalesTaxes
        {
            get { CheckGet(); return _invoiceSalesTaxes; }
        }
        #endregion
        #endregion

        public bool DoesEntityHavePermissionToUpdateByStatus(bool isPayee)
        {
            if (Status == Statuses.Complete ||
                (Status == Statuses.WorkInProgress && !isPayee))
            {
                return false;
            }

            return true;
        }

        public bool DoesEntityHavePermissionToUpdateFields(bool isPayee)
        {

            if (!DoesEntityHavePermissionToUpdateByStatus(isPayee))
            {
                return false;
            }

            foreach(PropertyInfo propInfo in GetType().GetProperties().Where(prop => prop.GetCustomAttribute<SentPermissionAttribute>() != null))
            {
                if (!IsFieldDirty(propInfo.Name))
                {
                    continue;
                }

                SentPermissionAttribute updaterPermission = propInfo.GetCustomAttribute<SentPermissionAttribute>();
                if ((updaterPermission.UpdaterOption == SentPermissionAttribute.SenderTypes.Recipient && !isPayee) ||
                    (updaterPermission.UpdaterOption == SentPermissionAttribute.SenderTypes.Sender && isPayee))
                {
                    return false;
                }
            }

            return true;
        }

        protected override bool PreSave(ITransaction transaction)
        {
            if (!IsInsert)
            {
                if ((Status == Statuses.Sent || Status == Statuses.ReadyForReceipt) && 
                    (IsFieldDirty(nameof(LocationIDFrom)) || IsFieldDirty(nameof(LocationIDTo)) || IsFieldDirty(nameof(GovernmentIDFrom)) || IsFieldDirty(nameof(GovernmentIDTo))))
                {
                    Status = Statuses.WorkInProgress;
                }

                if (IsFieldDirty(nameof(PurchaseOrderID)) && PurchaseOrderID == null)
                {
                    Search<InvoiceLine> invoiceLineSearch = new Search<InvoiceLine>(new LongSearchCondition<InvoiceLine>()
                    {
                        Field = nameof(InvoiceLine.InvoiceID),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = InvoiceID
                    });

                    foreach (InvoiceLine invoiceLine in invoiceLineSearch.GetEditableReader(transaction))
                    {
                        invoiceLine.PurchaseOrderLineID = null;
                        if (!invoiceLine.Save(transaction))
                        {
                            Errors.AddRange(invoiceLine.Errors.ToArray());
                            return false;
                        }
                    }
                }
            }

            return base.PreSave(transaction);
        }

        protected override bool PostSave(ITransaction transaction)
        {
            if (IsInsert)
            {
                if (LocationIDFrom == null && GovernmentIDFrom == null)
                {
                    return base.PostSave(transaction);
                }

                string nextInvoiceNumber;
                string invoiceNumberPrefix;
                Location locationFrom = null;
                Government governmentFrom = null;

                if (LocationIDFrom != null)
                {
                    locationFrom = DataObject.GetEditableByPrimaryKey<Location>(LocationIDFrom, transaction, null);
                    invoiceNumberPrefix = locationFrom.InvoiceNumberPrefix;
                    nextInvoiceNumber = locationFrom.InvoiceNumberPrefix + locationFrom.NextInvoiceNumber;
                }
                else
                {
                    governmentFrom = DataObject.GetEditableByPrimaryKey<Government>(GovernmentIDFrom, transaction, null);
                    invoiceNumberPrefix = governmentFrom.InvoiceNumberPrefix;
                    nextInvoiceNumber = governmentFrom.InvoiceNumberPrefix + governmentFrom.NextInvoiceNumber;
                }

                if (!nextInvoiceNumber.Equals(InvoiceNumber))
                {
                    return base.PostSave(transaction);
                }

                string newInvoiceNumber = InvoiceNumber.Substring(invoiceNumberPrefix.Length);
                if (!int.TryParse(newInvoiceNumber, out int newInvoiceNumberInt))
                {
                    return base.PostSave(transaction);
                }

                newInvoiceNumberInt++;
                newInvoiceNumber = newInvoiceNumberInt.ToString().PadLeft(newInvoiceNumber.Length, '0');

                if (locationFrom != null)
                {
                    locationFrom.NextInvoiceNumber = newInvoiceNumber;
                    if (!locationFrom.Save(transaction))
                    {
                        Errors.AddRange(locationFrom.Errors.ToArray());
                    }
                }
                else
                {
                    governmentFrom.NextInvoiceNumber = newInvoiceNumber;
                    if (!governmentFrom.Save(transaction))
                    {
                        Errors.AddRange(governmentFrom.Errors.ToArray());
                    }
                }
            }
            else
            {
                if (IsFieldDirty(nameof(Status)))
                {
                    Statuses oldStatus = (Statuses)GetDirtyValue(nameof(Status));
                    if (oldStatus == Statuses.WorkInProgress && Status == Statuses.Sent)
                    {
                        if (TryAutoPay(transaction))
                        {
                            return true; // Skip sending emails since it's happening immediately
                        }

                        long? emailImpID;

                        if (GovernmentIDTo != null)
                        {
                            emailImpID = DataObject.GetReadOnlyByPrimaryKey<Government>(GovernmentIDTo, transaction, new[] { nameof(Government.EmailImplementationIDPayableInvoice) }).EmailImplementationIDPayableInvoice;
                        }
                        else
                        {
                            emailImpID = DataObject.GetReadOnlyByPrimaryKey<Location>(LocationIDTo, transaction, new[] { nameof(Location.EmailImplementationIDPayableInvoice) }).EmailImplementationIDPayableInvoice;
                        }

                        EmailImplementation emailImp = DataObject.GetReadOnlyByPrimaryKey<EmailImplementation>(emailImpID, transaction, ClussPro.ObjectBasedFramework.Schema.Schema.GetSchemaObject<EmailImplementation>().GetFields().Select(f => f.FieldName));

                        if (emailImp != null)
                        {
                            emailImp.SendEmail<Invoice>(InvoiceID, transaction);
                        }
                    }

                    if ((oldStatus == Statuses.Sent || oldStatus == Statuses.WorkInProgress) && Status == Statuses.ReadyForReceipt)
                    {
                        if (AutoReceive)
                        {
                            ReceiveInvoice(transaction);
                            return !Errors.Any();
                        }

                        long? emailImpID;

                        if (GovernmentIDFrom != null)
                        {
                            emailImpID = DataObject.GetReadOnlyByPrimaryKey<Government>(GovernmentIDFrom, transaction, new[] { nameof(Government.EmailImplementationIDReadyForReceipt) }).EmailImplementationIDReadyForReceipt;
                        }
                        else
                        {
                            emailImpID = DataObject.GetReadOnlyByPrimaryKey<Location>(LocationIDFrom, transaction, new[] { nameof(Location.EmailImplementationIDReadyForReceipt) }).EmailImplementationIDReadyForReceipt;
                        }

                        EmailImplementation emailImp = DataObject.GetReadOnlyByPrimaryKey<EmailImplementation>(emailImpID, transaction, ClussPro.ObjectBasedFramework.Schema.Schema.GetSchemaObject<EmailImplementation>().GetFields().Select(f => f.FieldName));

                        if (emailImp != null)
                        {
                            emailImp.SendEmail<Invoice>(InvoiceID, transaction);
                        }
                    }
                }
            }
            return base.PostSave(transaction);
        }

        private bool TryAutoPay(ITransaction transaction)
        {
            SearchConditionGroup autoPayConfigCondition = new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                new IntSearchCondition<AutomaticInvoicePaymentConfiguration>()
                {
                    Field = nameof(AutomaticInvoicePaymentConfiguration.Schedule),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = (int)AutomaticInvoicePaymentConfiguration.Schedules.Immediately
                },
                new LongSearchCondition<AutomaticInvoicePaymentConfiguration>()
                {
                    Field = nameof(AutomaticInvoicePaymentConfiguration.AccountID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.NotNull
                },
                new DecimalSearchCondition<AutomaticInvoicePaymentConfiguration>()
                {
                    Field = nameof(AutomaticInvoicePaymentConfiguration.RemainingAmount),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Greater,
                    Value = 0
                });

            if (GovernmentIDFrom != null)
            {
                autoPayConfigCondition.SearchConditions.Add(new LongSearchCondition<AutomaticInvoicePaymentConfiguration>()
                {
                    Field = nameof(AutomaticInvoicePaymentConfiguration.GovernmentIDPayee),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = GovernmentIDFrom
                });
            }

            if (LocationIDFrom != null)
            {
                autoPayConfigCondition.SearchConditions.Add(new LongSearchCondition<AutomaticInvoicePaymentConfiguration>()
                {
                    Field = nameof(AutomaticInvoicePaymentConfiguration.LocationIDPayee),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = LocationIDFrom
                });
            }

            if (GovernmentIDTo != null)
            {
                autoPayConfigCondition.SearchConditions.Add(new LongSearchCondition<AutomaticInvoicePaymentConfiguration>()
                {
                    Field = nameof(AutomaticInvoicePaymentConfiguration.GovernmentIDConfiguredFor),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = GovernmentIDTo
                });
            }

            if (LocationIDTo != null)
            {
                autoPayConfigCondition.SearchConditions.Add(new LongSearchCondition<AutomaticInvoicePaymentConfiguration>()
                {
                    Field = nameof(AutomaticInvoicePaymentConfiguration.LocationIDConfiguredFor),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = LocationIDTo
                });
            }

            List<string> fields = FieldPathUtility.CreateFieldPathsAsList<AutomaticInvoicePaymentConfiguration>(aipc => new object[]
            {
                aipc.RemainingAmount,
                aipc.Account.Balance
            });

            Search<AutomaticInvoicePaymentConfiguration> autoPayConfigSearch = new Search<AutomaticInvoicePaymentConfiguration>(autoPayConfigCondition);
            AutomaticInvoicePaymentConfiguration configuration = autoPayConfigSearch.GetEditable(transaction, fields);

            if (configuration != null)
            {
                // We know there is a valid configuration - check to see how much this Invoice costs
                Search<InvoiceLine> invoiceLineSearch = new Search<InvoiceLine>(new LongSearchCondition<InvoiceLine> 
                {
                    Field = nameof(InvoiceLine.InvoiceID), 
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals, 
                    Value = InvoiceID
                });

                decimal invoiceTotal = invoiceLineSearch.GetReadOnlyReader(transaction, new[] { nameof(InvoiceLine.Total) }).Sum(il => il.Total ?? 0M);
                if (configuration.Account.Balance > invoiceTotal &&
                    configuration.RemainingAmount > invoiceTotal)
                {
                    long? originalAccountIDFrom = AccountIDFrom;
                    AccountIDFrom = configuration.AccountID;
                    Status = Statuses.ReadyForReceipt;

                    configuration.PaidAmount += invoiceTotal;
                    if (!configuration.Save(transaction))
                    {
                        Errors.AddRange(configuration.Errors.ToArray());
                        return false;
                    }

                    if (!Save(transaction))
                    {
                        Status = Statuses.Sent;
                        AccountIDFrom = originalAccountIDFrom;
                        return false;
                    }

                    return true;
                }
            }

            return false;
        }

        public void IssueInvoice(ITransaction transaction = null)
        {
            if (Status != Statuses.WorkInProgress)
            {
                Errors.AddBaseMessage("An Invoice may only be issued if it is in Work In Progress status");
            }

            Status = Statuses.Sent;
            Save(transaction);
        }

        public void ReceiveInvoice(ITransaction transaction = null)
        {
            if (AccountIDTo == null || AccountIDFrom == null)
            {
                Errors.Add("AccountIDFrom,AccountIDTo", "Account information must be complete to receive an Invoice");
                return;
            }

            ITransaction localTransaction = null;

            try
            {
                localTransaction = transaction ?? SQLProviderFactory.GenerateTransaction();

                Search<InvoiceLine> childLineSearch = new Search<InvoiceLine>(new LongSearchCondition<InvoiceLine>()
                {
                    Field = "InvoiceID",
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = InvoiceID
                });

                decimal invoiceTotal = childLineSearch.GetReadOnlyReader(localTransaction, new[] { "Total" }).Sum(il => il.Total).Value;

                decimal taxRate = 1M;
                List<Tuple<string, decimal, long>> taxRateByGovernment = new List<Tuple<string, decimal, long>>();

                if (LocationIDFrom != null && LocationIDTo != null && GovernmentIDFrom == null && GovernmentIDTo == null)
                {
                    Search<LocationGovernment> locationGovernmentSearch = new Search<LocationGovernment>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                        new LongSearchCondition<LocationGovernment>()
                        {
                            Field = nameof(LocationGovernment.LocationID),
                            SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                            Value = LocationIDFrom
                        },
                        new BooleanSearchCondition<LocationGovernment>()
                        {
                            Field = nameof(LocationGovernment.PaySalesTax),
                            SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                            Value = true
                        }));

                    string[] searchFields = new string[]
                    {
                        $"{nameof(LocationGovernment.Government)}.{nameof(Government.EffectiveSalesTax)}.{nameof(SalesTax.Rate)}",
                        $"{nameof(LocationGovernment.Government)}.{nameof(Government.EffectiveSalesTax)}.{nameof(SalesTax.AccountID)}",
                        $"{nameof(LocationGovernment.Government)}.{nameof(Government.Name)}"
                    };

                    foreach(LocationGovernment locationGovernment in locationGovernmentSearch.GetReadOnlyReader(localTransaction, searchFields))
                    {
                        if (locationGovernment.Government.EffectiveSalesTax == null || locationGovernment.Government.EffectiveSalesTax.Rate == 0M || locationGovernment.Government.EffectiveSalesTax.AccountID == null)
                        {
                            continue;
                        }

                        taxRate += locationGovernment.Government.EffectiveSalesTax.Rate.Value / 100M;

                        taxRateByGovernment.Add(new Tuple<string, decimal, long>(locationGovernment.Government.Name, locationGovernment.Government.EffectiveSalesTax.Rate.Value / 100M, locationGovernment.Government.EffectiveSalesTax.AccountID.Value));
                    }
                }

                decimal invoiceTotalWithTax = invoiceTotal * taxRate;

                Account sourceAccount = DataObject.GetEditableByPrimaryKey<Account>(AccountIDFrom, localTransaction, null);
                if (sourceAccount.Balance < invoiceTotalWithTax)
                {
                    Errors.AddBaseMessage("The Payor's Account has insufficient funds available");
                    return;
                }

                FiscalQuarter sourceAccountFQ = FiscalQuarter.FindOrCreate(sourceAccount.AccountID.Value, DateTime.Now, localTransaction);
                FiscalQuarter destinationAccountFQ = FiscalQuarter.FindOrCreate(AccountIDTo.Value, DateTime.Now, localTransaction);

                Transaction cashTransaction = DataObjectFactory.Create<Transaction>();
                cashTransaction.FiscalQuarterID = sourceAccountFQ.FiscalQuarterID;
                cashTransaction.TransactionTime = DateTime.Now;
                cashTransaction.Description = string.Format(Transaction.DescriptionFormats.INVOICE_PAYMENT, InvoiceNumber);
                cashTransaction.Amount = -invoiceTotal;
                if (!cashTransaction.Save(localTransaction))
                {
                    Errors.AddRange(cashTransaction.Errors.ToArray());
                    return;
                }

                foreach (Tuple<string, decimal, long> taxRateGovNameGovAccount in taxRateByGovernment)
                {
                    decimal taxAmount = invoiceTotal * taxRateGovNameGovAccount.Item2;
                    Account govAccount = DataObject.GetEditableByPrimaryKey<Account>(taxRateGovNameGovAccount.Item3, localTransaction, null);
                    FiscalQuarter govAccountFQ = FiscalQuarter.FindOrCreate(taxRateGovNameGovAccount.Item3, DateTime.Now, localTransaction);

                    cashTransaction = DataObjectFactory.Create<Transaction>();
                    cashTransaction.FiscalQuarterID = sourceAccountFQ.FiscalQuarterID;
                    cashTransaction.TransactionTime = DateTime.Now;
                    cashTransaction.Description = string.Format(Transaction.DescriptionFormats.TAX_PAYMENT, InvoiceNumber, taxRateGovNameGovAccount.Item1);
                    cashTransaction.Amount = -taxAmount;
                    if (!cashTransaction.Save(localTransaction))
                    {
                        Errors.AddRange(cashTransaction.Errors.ToArray());
                        return;
                    }

                    cashTransaction = DataObjectFactory.Create<Transaction>();
                    cashTransaction.FiscalQuarterID = govAccountFQ.FiscalQuarterID;
                    cashTransaction.TransactionTime = DateTime.Now;
                    cashTransaction.Description = string.Format(Transaction.DescriptionFormats.TAX_COLLECTED_INVOICE, InvoiceNumber);
                    cashTransaction.Amount = taxAmount;
                    if (!cashTransaction.Save(localTransaction))
                    {
                        Errors.AddRange(cashTransaction.Errors.ToArray());
                        return;
                    }

                    govAccount.Balance += taxAmount;
                    if (!govAccount.Save(localTransaction))
                    {
                        Errors.AddRange(govAccount.Errors.ToArray());
                        return;
                    }

                    InvoiceSalesTax invoiceSalesTax = DataObjectFactory.Create<InvoiceSalesTax>();
                    invoiceSalesTax.InvoiceID = InvoiceID;
                    invoiceSalesTax.Rate = taxRateGovNameGovAccount.Item2;
                    invoiceSalesTax.AppliedAmount = taxAmount;
                    invoiceSalesTax.Municipality = taxRateGovNameGovAccount.Item1;
                    if (!invoiceSalesTax.Save(localTransaction))
                    {
                        Errors.AddRange(invoiceSalesTax.Errors.ToArray());
                        return;
                    }
                }

                cashTransaction = DataObjectFactory.Create<Transaction>();
                cashTransaction.FiscalQuarterID = destinationAccountFQ.FiscalQuarterID;
                cashTransaction.TransactionTime = DateTime.Now;
                cashTransaction.Description = string.Format(Transaction.DescriptionFormats.INVOICE_COLLECTED, InvoiceNumber);
                cashTransaction.Amount = invoiceTotal;
                if (!cashTransaction.Save(localTransaction))
                {
                    Errors.AddRange(cashTransaction.Errors.ToArray());
                    return;
                }

                sourceAccount.Balance -= invoiceTotalWithTax;
                if (!sourceAccount.Save(localTransaction))
                {
                    Errors.AddRange(sourceAccount.Errors.ToArray());
                    return;
                }

                Account destinationAccount = DataObject.GetEditableByPrimaryKey<Account>(AccountIDTo, localTransaction, null);
                destinationAccount.Balance += invoiceTotal;
                if (!destinationAccount.Save(localTransaction))
                {
                    Errors.AddRange(destinationAccount.Errors.ToArray());
                    return;
                }

                Status = Statuses.Complete;
                AccountFromHistorical = $"{sourceAccount.Description} ({sourceAccount.AccountNumber})";
                AccountToHistorical = $"{destinationAccount.Description} ({destinationAccount.AccountNumber})";
                AccountIDFrom = null;
                AccountIDTo = null;

                Save(localTransaction, new List<Guid>() { ValidationIDs.V_HistoryStatusChanges });
                if (Errors.Any())
                {
                    return;
                }

                if (transaction == null)
                {
                    localTransaction.Commit();
                }
            }
            catch (Exception ex)
            {
                Errors.AddBaseMessage(ex.Message);

                if (transaction == null && localTransaction != null && localTransaction.IsActive)
                {
                    localTransaction.Rollback();
                }
            }
        }

        public static class ValidationIDs
        {
            public static readonly Guid V_HistoryStatusChanges = new Guid("DE904CC5-FF28-4250-BCEA-D31D749FEF51");
            public static readonly Guid V_SentStatusValid = new Guid("DE904CC5-FF28-4250-BCEA-D31D749FEF51");
        }
    }
}
