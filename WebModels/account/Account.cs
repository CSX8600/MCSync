using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Schema;
using ClussPro.ObjectBasedFramework.Schema.Attributes;
using ClussPro.ObjectBasedFramework.Validation.Attributes;
using WebModels.company;
using WebModels.gov;
using WebModels.invoicing;

namespace WebModels.account
{
    [Table("2B91C03F-672C-47C0-AE17-4657967EB54B")]
    [Unique(new string[] { "AccountNumber" })]
    public class Account : DataObject
    {
        protected Account() : base() { }

        private long? _accountID;
        [Field("D91F3C4F-5455-432E-9A49-6BC69074EF6A")]
        public long? AccountID
        {
            get { CheckGet(); return _accountID; }
            set { CheckSet(); _accountID = value; }
        }

        private long? _companyID;
        [Field("C8B684BE-CBB0-4013-8DA7-C4CA9CE14CFD")]
        public long? CompanyID
        {
            get { CheckGet(); return _companyID; }
            set { CheckSet(); _companyID = value; }
        }

        private Company _company = null;
        [Relationship("5E8D35E2-A896-4C7B-BFC6-3F5CFF63C1DA")]
        public Company Company
        {
            get { CheckGet(); return _company; }
        }

        private long? _governmentID;
        [Field("CA6189D1-8C70-4EC0-86BE-D9C6285F2E61")]
        public long? GovernmentID
        {
            get { CheckGet(); return _governmentID; }
            set { CheckSet(); _governmentID = value; }
        }

        private Government _government;
        [Relationship("C829588D-0D7B-4DDC-9C17-94D6F3FDADE9")]
        public Government Government
        {
            get { CheckGet(); return _government; }
        }

        private long? _categoryID;
        [Field("393487AC-885A-477B-9F9E-043BDFF83B6D")]
        public long? CategoryID
        {
            get { CheckGet(); return _categoryID; }
            set { CheckSet(); _categoryID = value; }
        }

        private Category _category = null;
        [Relationship("F44026FA-1230-4C78-AB63-0AA17405EA07")]
        public Category Category
        {
            get { CheckGet(); return _category; }
        }

        private string _accountNumber;
        [Field("B27938A6-FC2B-4701-A3AA-3B098F465634", DataSize = 16)]
        [Required]
        public string AccountNumber
        {
            get { CheckGet(); return _accountNumber; }
            set { CheckSet(); _accountNumber = value; }
        }

        private string _description;
        [Field("8912E1E4-9933-4B9C-BE60-A8CDFEDB5E2A", DataSize = 50)]
        public string Description
        {
            get { CheckGet(); return _description; }
            set { CheckSet(); _description = value; }
        }

        private decimal? _balance;
        [Field("56F17B40-38E6-4246-9AA9-48C6D1A31FD9", DataSize = 11, DataScale = 2)]
        [Required]
        public decimal? Balance
        {
            get { CheckGet(); return _balance; }
            set { CheckSet(); _balance = value; }
        }

        public bool Close(long destinationAccountID, ITransaction transaction)
        {
            #region Move Accounts
            Search<SalesTax> salesTaxSearch = new Search<SalesTax>(new LongSearchCondition<SalesTax>()
            {
                Field = "AccountID",
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = AccountID
            });

            foreach(SalesTax salesTax in salesTaxSearch.GetEditableReader(transaction))
            {
                salesTax.AccountID = destinationAccountID;
                if (!salesTax.Save(transaction))
                {
                    Errors.AddRange(salesTax.Errors.ToArray());
                    return false;
                }
            }

            Search<Invoice> invoiceSearch = new Search<Invoice>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.Or,
                new LongSearchCondition<Invoice>()
                {
                    Field = "AccountIDFrom",
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = AccountID
                },
                new LongSearchCondition<Invoice>()
                {
                    Field = "AccountIDTo",
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = AccountID
                }));

            foreach(Invoice invoice in invoiceSearch.GetEditableReader(transaction))
            {
                if (invoice.AccountIDFrom == AccountID)
                {
                    invoice.AccountIDFrom = destinationAccountID;
                }

                if (invoice.AccountIDTo == AccountID)
                {
                    invoice.AccountIDTo = destinationAccountID;
                }

                if (!invoice.Save(transaction))
                {
                    Errors.AddRange(invoice.Errors.ToArray());
                    return false;
                }
            }
            #endregion
            Account destinationAccount = DataObject.GetEditableByPrimaryKey<Account>(destinationAccountID, transaction, null);

            FiscalQuarter fiscalQuarter;
            try
            {
                fiscalQuarter = FiscalQuarter.FindOrCreate(destinationAccountID, DateTime.Now, transaction);
            }
            catch (Exception ex)
            {
                Errors.AddBaseMessage("Could not find Fiscal Quarter:\r\n\r\n" + ex.Message);
                return false;
            }

            Transaction depositTransaction = DataObjectFactory.Create<Transaction>();
            depositTransaction.FiscalQuarterID = fiscalQuarter.FiscalQuarterID;
            depositTransaction.TransactionTime = DateTime.Now;
            depositTransaction.Amount = Balance;
            depositTransaction.Description = string.Format(Transaction.DescriptionFormats.CLOSING_DEPOSIT, AccountNumber, Description);
            if (!depositTransaction.Save(transaction))
            {
                Errors.AddRange(depositTransaction.Errors.ToArray());
                return false;
            }

            destinationAccount.Balance += Balance;
            if (!destinationAccount.Save(transaction))
            {
                Errors.AddRange(destinationAccount.Errors.ToArray());
                return false;
            }

            if (!Delete(transaction))
            {
                return false;
            }

            return true;
        }

        public bool Transfer(long destinationAccountID, decimal amount, ITransaction transaction)
        {
            if (AccountID == destinationAccountID)
            {
                Errors.AddBaseMessage("Cannot transfer to same account.");
                return false;
            }

            if (amount == 0)
            {
                Errors.AddBaseMessage("Transfer Amount is a required field.");
                return false;
            }

            Account destinationAccount = DataObject.GetEditableByPrimaryKey<Account>(destinationAccountID, transaction, null);

            if (Balance < amount)
            {
                Errors.AddBaseMessage("Cannot transfer more funds than are available.");
                return false;
            }

            Balance -= amount;
            if (!Save(transaction))
            {
                return false;
            }

            destinationAccount.Balance += amount;
            if (!destinationAccount.Save(transaction))
            {
                Errors.AddRange(destinationAccount.Errors.ToArray());
                return false;
            }

            FiscalQuarter sourceFiscalQuarter;
            try
            {
                sourceFiscalQuarter = FiscalQuarter.FindOrCreate(AccountID.Value, DateTime.Now, transaction);
            }
            catch (Exception ex)
            {
                Errors.AddBaseMessage("Could not get Fiscal Quarter:\r\n\r\n" + ex.Message);
                return false;
            }

            FiscalQuarter destinationFiscalQuarter;
            try
            {
                destinationFiscalQuarter = FiscalQuarter.FindOrCreate(destinationAccountID, DateTime.Now, transaction);
            }
            catch (Exception ex)
            {
                Errors.AddBaseMessage("Could not get Fiscal Quarter:\r\n\r\n" + ex.Message);
                return false;
            }

            Transaction sourceTransaction = DataObjectFactory.Create<Transaction>();
            sourceTransaction.FiscalQuarterID = sourceFiscalQuarter.FiscalQuarterID;
            sourceTransaction.TransactionTime = DateTime.Now;
            sourceTransaction.Amount = -amount;
            sourceTransaction.Description = string.Format(Transaction.DescriptionFormats.TRANSFER_WITHDRAWAL, destinationAccount.AccountNumber, destinationAccount.Description);
            if (!sourceTransaction.Save(transaction))
            {
                Errors.AddRange(sourceTransaction.Errors.ToArray());
                return false;
            }

            Transaction destinationTransaction = DataObjectFactory.Create<Transaction>();
            destinationTransaction.FiscalQuarterID = destinationFiscalQuarter.FiscalQuarterID;
            destinationTransaction.TransactionTime = DateTime.Now;
            destinationTransaction.Amount = amount;
            destinationTransaction.Description = string.Format(Transaction.DescriptionFormats.TRANSFER_DEPOSIT, AccountNumber, Description);
            if (!destinationTransaction.Save(transaction))
            {
                Errors.AddRange(destinationTransaction.Errors.ToArray());
                return false;
            }

            return true;
        }

        public bool Deposit(decimal amount, string description, ITransaction transaction)
        {
            Balance += amount;
            if (!Save(transaction))
            {
                return false;
            }

            FiscalQuarter fiscalQuarter;
            try
            {
                fiscalQuarter = FiscalQuarter.FindOrCreate(AccountID.Value, DateTime.Now, transaction);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to find current Fiscal Quarter\r\n\r\n" + ex.Message, ex);
            }

            if (description.Length > Schema.GetSchemaObject<Transaction>().GetField(nameof(Transaction.Description)).DataSize)
            {
                description = description.Substring(0, Schema.GetSchemaObject<Transaction>().GetField(nameof(Transaction.Description)).DataSize);
            }

            Transaction depositTransaction = DataObjectFactory.Create<Transaction>();
            depositTransaction.FiscalQuarterID = fiscalQuarter.FiscalQuarterID;
            depositTransaction.Description = description;
            depositTransaction.Amount = amount;
            depositTransaction.TransactionTime = DateTime.Now;
            if (!depositTransaction.Save(transaction))
            {
                Errors.AddRange(depositTransaction.Errors.ToArray());
                return false;
            }

            return true;
        }

        protected override void PreValidate()
        {
            if (IsInsert)
            {
                StringBuilder accountNumberBuilder = new StringBuilder();
                Random rand = new Random();
                while (accountNumberBuilder.Length < 16)
                {
                    int num = rand.Next(0, 10);
                    if (num == 0 && accountNumberBuilder.Length == 0)
                    {
                        continue;
                    }

                    accountNumberBuilder.Append(num);
                }

                AccountNumber = accountNumberBuilder.ToString();
                Balance = 0;
            }
        }

        protected override bool PostSave(ITransaction transaction)
        {
            if (IsInsert)
            {
                try
                {
                    FiscalQuarter.FindOrCreate(AccountID.Value, DateTime.Now, transaction);
                }
                catch(Exception ex)
                {
                    Errors.AddBaseMessage(ex.Message);
                    return false;
                }
            }

            return true;
        }

        #region Relationships
        #region account
        private List<AccountClearance> _accountClearances = new List<AccountClearance>();
        [RelationshipList("6733E4CD-1C88-42A0-A7DF-CAD5299C19D2", "AccountID", AutoDeleteReferences = true)]
        public IReadOnlyCollection<AccountClearance> AccountClearances
        {
            get { CheckGet(); return _accountClearances; }
        }

        private List<FiscalQuarter> _fiscalQuarters = new List<FiscalQuarter>();
        [RelationshipList("02DC9883-FC0C-42BF-A361-C9C69325A807", "AccountID", AutoDeleteReferences = true)]
        public IReadOnlyCollection<FiscalQuarter> FiscalQuarters
        {
            get { CheckGet(); return _fiscalQuarters; }
        }

        private List<DebitCard> _debitCards = new List<DebitCard>();
        [RelationshipList("E5D1F9E6-8D6F-4B9A-9C4E-0E1C0E0A0E0A", "AccountID", AutoDeleteReferences = true)]
        public IReadOnlyCollection<DebitCard> DebitCards
        {
            get { CheckGet(); return _debitCards; }
        }
        #endregion
        #region company
        private List<Location> _locationStoreRevenues = new List<Location>();
        [RelationshipList("2468CD4E-5C93-44E3-AEEE-3BC681AE29B7", nameof(Location.AccountIDStoreRevenue))]
        public IReadOnlyCollection<Location> LocationStoreRevenues
        {
            get { CheckGet(); return _locationStoreRevenues;  }
        }
        #endregion
        #region gov
        private List<SalesTax> _salesTaxes = new List<SalesTax>();
        [RelationshipList("9B7659DE-80B6-44E4-9380-1576A62897AF", "AccountID")]
        public IReadOnlyCollection<SalesTax> SalesTaxes
        {
            get { CheckGet(); return _salesTaxes; }
        }
        #endregion
        #region invoicing
        private List<Invoice> _invoicesFrom = new List<Invoice>();
        [RelationshipList("78141444-B311-4C0D-8B80-BBD9E7412B37", "AccountIDFrom")]
        public IReadOnlyCollection<Invoice> InvoicesFrom
        {
            get { CheckGet(); return _invoicesFrom; }
        }

        private List<Invoice> _invoicesTo = new List<Invoice>();
        [RelationshipList("2EEE3F87-BFF4-4AE4-A1BE-7D07BB9817C1", "AccountIDTo")]
        public IReadOnlyCollection<Invoice> InvoicesTo
        {
            get { CheckGet(); return _invoicesTo; }
        }

        private List<AutomaticInvoicePaymentConfiguration> _automaticInvoicePaymentConfigurations = new List<AutomaticInvoicePaymentConfiguration>();
        [RelationshipList("C315B833-0E56-4966-B544-0667CF4F3B95", nameof(AutomaticInvoicePaymentConfiguration.AccountID))]
        public IReadOnlyCollection<AutomaticInvoicePaymentConfiguration> AutomaticInvoicePaymentConfigurations
        {
            get { CheckGet(); return _automaticInvoicePaymentConfigurations; }
        }
        #endregion
        #region purchasing
        private List<purchasing.PurchaseOrder> _purchaseOrderReceivers = new List<purchasing.PurchaseOrder>();
        [RelationshipList("B524172F-29A5-4244-8D15-DE27EB23604A", nameof(purchasing.PurchaseOrder.AccountIDReceiving))]
        public IReadOnlyCollection<purchasing.PurchaseOrder> PurchaseOrderReceivers
        {
            get { CheckGet(); return _purchaseOrderReceivers; }
        }
        #endregion
        #endregion
    }
}
