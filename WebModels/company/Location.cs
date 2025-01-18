using System.Collections.Generic;
using System.Linq;
using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Schema.Attributes;
using ClussPro.ObjectBasedFramework.Validation.Attributes;
using WebModels.account;
using WebModels.fleet;
using WebModels.invoicing;
using WebModels.mesasys;
using WebModels.purchasing;

namespace WebModels.company
{
    [Table("A3A28E39-0FA0-423C-B6D4-43F2802ED19D")]
    [Unique(new [] { nameof(CompanyID), nameof(Name) })]
    public class Location : DataObject
    {
        protected Location() : base() { }

        private long? _locationID;
        [Field("BC041BDE-50B5-4D80-8081-9A06C79BFA65")]
        public long? LocationID
        {
            get { CheckGet(); return _locationID; }
            set { CheckSet(); _locationID = value; }
        }

        private long? _companyID;
        [Field("0254C75D-C356-45C0-AB7A-A68C75D8B42F")]
        [Required]
        public long? CompanyID
        {
            get { CheckGet(); return _companyID; }
            set { CheckSet(); _companyID = value; }
        }

        private Company _company = null;
        [Relationship("45F492F3-A94B-46DD-9D2C-7DEE83652741")]
        public Company Company
        {
            get { CheckGet(); return _company; }
        }

        private string _name;
        [Field("1FB1B286-C72B-462A-ABD4-E149EF13BCC4", DataSize = 50)]
        [Required]
        public string Name
        {
            get { CheckGet(); return _name; }
            set { CheckSet(); _name = value;}
        }

        private string _invoiceNumberPrefix;
        [Field("FC1EE2D9-48EA-4F74-88FE-A0559D2D60E0", DataSize = 3)]
        public string InvoiceNumberPrefix
        {
            get { CheckGet(); return _invoiceNumberPrefix; }
            set { CheckSet(); _invoiceNumberPrefix = value; }
        }

        private string _nextInvoiceNumber;
        [Field("C3BB92AA-9EEC-4B80-8116-9CDD43B558EC", DataSize = 8)]
        public string NextInvoiceNumber
        {
            get { CheckGet(); return _nextInvoiceNumber; }
            set { CheckSet(); _nextInvoiceNumber = value; }
        }

        private long? _accountIDStoreRevenue;
        [Field("55022849-0C01-4BDC-9023-DA46E19EB696")]
        public long? AccountIDStoreRevenue
        {
            get { CheckGet(); return _accountIDStoreRevenue; }
            set { CheckSet(); _accountIDStoreRevenue = value; }
        }

        private Account _accountStoreRevenue = null;
        [Relationship("77075B0F-D43C-4417-8262-C76481901811", ForeignKeyField = nameof(AccountIDStoreRevenue))]
        public Account AccountStoreRevenue
        {
            get { CheckGet(); return _accountStoreRevenue; }
        }

        private long? _emailImplementationIDRegisterOffline;
        [Field("EDE175FF-CBD6-42C0-921D-673E1B51F52E")]
        public long? EmailImplementationIDRegisterOffline
        {
            get { CheckGet(); return _emailImplementationIDRegisterOffline; }
            set { CheckSet(); _emailImplementationIDRegisterOffline = value; }
        }

        private EmailImplementation _emailImplementationRegisterOffline = null;
        [Relationship("E4C68449-500A-49CB-8FBB-3418ABB5645D", ForeignKeyField = nameof(EmailImplementationIDRegisterOffline))]
        public EmailImplementation EmailImplementationRegisterOffline
        {
            get { CheckGet(); return _emailImplementationRegisterOffline; }
        }

        private long? _emailImplementationIDPayableInvoice;
        [Field("EC680184-ADE4-4F81-8DA8-47BAA42E9647")]
        public long? EmailImplementationIDPayableInvoice
        {
            get { CheckGet(); return _emailImplementationIDPayableInvoice; }
            set { CheckSet(); _emailImplementationIDPayableInvoice = value; }
        }

        private EmailImplementation _emailImplementationPayableInvoice = null;
        [Relationship("8524C29D-6CA0-47FD-84AE-D742DB329090", ForeignKeyField = nameof(EmailImplementationIDPayableInvoice))]
        public EmailImplementation EmailImplementationPayableInvoice
        {
            get { CheckGet(); return _emailImplementationPayableInvoice; }
        }

        private long? _emailImplementationIDReadyForReceipt;
        [Field("EC680184-ADE4-4F81-8DA8-47BAA42E9647")]
        public long? EmailImplementationIDReadyForReceipt
        {
            get { CheckGet(); return _emailImplementationIDReadyForReceipt; }
            set { CheckSet(); _emailImplementationIDReadyForReceipt = value; }
        }

        private EmailImplementation _emailImplementationReadyForReceipt = null;
        [Relationship("8524C29D-6CA0-47FD-84AE-D742DB329090", ForeignKeyField = nameof(EmailImplementationIDReadyForReceipt))]
        public EmailImplementation EmailImplementationReadyForReceipt
        {
            get { CheckGet(); return _emailImplementationReadyForReceipt; }
        }

        private StorePricingAutomation _storePricingAutomation = null;
        [Relationship("99239E57-9024-4226-96F5-BD3B976C1701", OneToOneByForeignKey = true)]
        public StorePricingAutomation StorePricingAutomation
        {
            get { CheckGet(); return _storePricingAutomation; }
        }

        protected override bool PostSave(ITransaction transaction)
        {
            if (!IsInsert)
            {
                bool deleteSuccessful = true;
                if (IsFieldDirty(nameof(EmailImplementationIDPayableInvoice)))
                {
                    long? previousEmailImpID = GetDirtyValue(nameof(EmailImplementationIDPayableInvoice)) as long?;
                    if (previousEmailImpID != null)
                    {
                        EmailImplementation oldImplementation = DataObject.GetEditableByPrimaryKey<EmailImplementation>(previousEmailImpID, transaction, null);
                        if (!oldImplementation.Delete(transaction))
                        {
                            Errors.AddRange(oldImplementation.Errors.ToArray());
                            deleteSuccessful = false;
                        }
                    }
                }


                if (IsFieldDirty(nameof(EmailImplementationIDReadyForReceipt)))
                {
                    long? previousEmailImpID = GetDirtyValue(nameof(EmailImplementationIDReadyForReceipt)) as long?;
                    if (previousEmailImpID != null)
                    {
                        EmailImplementation oldImplementation = DataObject.GetEditableByPrimaryKey<EmailImplementation>(previousEmailImpID, transaction, null);
                        if (!oldImplementation.Delete(transaction))
                        {
                            Errors.AddRange(oldImplementation.Errors.ToArray());
                            deleteSuccessful = false;
                        }
                    }
                }

                return deleteSuccessful;
            }

            return base.PostSave(transaction);
        }

        #region Relationships
        #region company
        private List<LocationEmployee> _locationEmployees = new List<LocationEmployee>();
        [RelationshipList("04569132-78B1-42E6-BD47-5729B5B392ED", "LocationID", AutoRemoveReferences = true)]
        public IReadOnlyCollection<LocationEmployee> LocationEmployees
        {
            get { CheckGet(); return _locationEmployees; }
        }

        private List<LocationGovernment> _locationGovernments = new List<LocationGovernment>();
        [RelationshipList("5C8358F5-676C-468E-A745-EBAF6754C67E", "LocationID", AutoRemoveReferences = true)]
        public IReadOnlyCollection<LocationGovernment> LocationGovernments
        {
            get { CheckGet(); return _locationGovernments; }
        }

        private List<Register> _registers = new List<Register>();
        [RelationshipList("56573B92-10FB-4ACF-BFB4-3B6B167D904B", nameof(Register.LocationID), AutoRemoveReferences = true)]
        public IReadOnlyCollection<Register> Registers
        {
            get { CheckGet(); return _registers; }
        }

        private List<LocationItem> _locationItems = new List<LocationItem>();
        [RelationshipList("9BA0848D-68DC-4A88-ADFE-E782D15AEC77", nameof(LocationItem.LocationID), AutoRemoveReferences = true)]
        public IReadOnlyCollection<LocationItem> LocationItems
        {
            get { CheckGet(); return _locationItems; }
        }

        private List<Promotion> _promotions = new List<Promotion>();
        [RelationshipList("90853C4A-31F5-4A46-BC4E-8A93FBE0ED42", nameof(Promotion.LocationID), AutoRemoveReferences = true)]
        public IReadOnlyCollection<Promotion> Promotions
        {
            get { CheckGet(); return _promotions; }
        }

        private List<StorePricingAutomationLocation> _storePricingAutomationLocations = new List<StorePricingAutomationLocation>();
        [RelationshipList("E3C485FA-C706-4172-ACEC-718B8D016E55", nameof(StorePricingAutomationLocation.LocationIDDestination), AutoDeleteReferences = true)]
        public IReadOnlyCollection<StorePricingAutomationLocation> StorePricingAutomationLocations
        {
            get { CheckGet(); return _storePricingAutomationLocations; }
        }
        #endregion
        #region fleet
        private List<LeaseRequest> _leaseRequests = new List<LeaseRequest>();
            [RelationshipList("CC3820D7-F975-44D3-B22C-69FC070A7704", nameof(LeaseRequest.LocationIDChargeTo))]
            public IReadOnlyCollection<LeaseRequest> LeaseRequests
            {
                get { CheckGet(); return _leaseRequests; }
            }

            private List<LeaseBid> _leaseBidRecurringDestinations = new List<LeaseBid>();
            [RelationshipList("F084805B-8F14-45EA-940B-646F97AF9268", nameof(LeaseBid.LocationIDInvoiceDestination))]
            public IReadOnlyCollection<LeaseBid> LeaseBidRecurringDestinations
            {
                get { CheckGet(); return _leaseBidRecurringDestinations; }
            }

            private List<LeaseContract> _leaseContractRecurringSources = new List<LeaseContract>();
            [RelationshipList("1B601D65-BFF5-40D0-8E11-A1624D12E3A3", nameof(LeaseContract.LocationIDRecurringAmountSource))]
            public IReadOnlyCollection<LeaseContract> LeaseContractRecurringSources
            {
                get { CheckGet(); return _leaseContractRecurringSources; }
            }

            private List<LeaseContract> _leaseContractRecurringDestinations = new List<LeaseContract>();
            [RelationshipList("DF30485C-668A-4686-B512-F7B15EC6B3E9", nameof(LeaseContract.LocationIDRecurringAmountDestination))]
            public IReadOnlyCollection<LeaseContract> LeaseContractRecurringDestinations
            {
                get { CheckGet(); return _leaseContractRecurringDestinations; }
            }
            #endregion
        #region invoicing
        private List<Invoice> _invoicesFrom = new List<Invoice>();
        [RelationshipList("6DD822E0-7449-4F56-AF7C-559C44E94EA0", "LocationIDFrom")]
        public IReadOnlyCollection<Invoice> InvoicesFrom
        {
            get { CheckGet(); return _invoicesFrom; }
        }

        private List<Invoice> _invoicesTo = new List<Invoice>();
        [RelationshipList("1C3F6214-C3E8-4882-9F10-10CF7BC7A8DE", "LocationIDTo")]
        public IReadOnlyCollection<Invoice> InvoicesTo
        {
            get { CheckGet(); return _invoicesTo; }
        }

        private List<AutomaticInvoicePaymentConfiguration> _automaticInvoicePaymentConfigurationConfiguredFors = new List<AutomaticInvoicePaymentConfiguration>();
        [RelationshipList("1DF050A5-8DF0-47FF-8902-46949F4B3156", nameof(AutomaticInvoicePaymentConfiguration.LocationIDConfiguredFor))]
        public IReadOnlyCollection<AutomaticInvoicePaymentConfiguration> AutomaticInvoicePaymentConfigurationConfiguredFors
        {
            get { CheckGet(); return _automaticInvoicePaymentConfigurationConfiguredFors; }
        }

        private List<AutomaticInvoicePaymentConfiguration> _automaticInvoicePaymentConfigurationPayees = new List<AutomaticInvoicePaymentConfiguration>();
        [RelationshipList("2B0E747E-6808-48A4-BEE6-6378A13223DC", nameof(AutomaticInvoicePaymentConfiguration.LocationIDPayee))]
        public IReadOnlyCollection<AutomaticInvoicePaymentConfiguration> AutomaticInvoicePaymentConfigurationPayees
        {
            get { CheckGet(); return _automaticInvoicePaymentConfigurationPayees; }
        }
        #endregion
        #region purchasing
        private List<PurchaseOrder> _purchaseOrderOrigins = new List<PurchaseOrder>();
        [RelationshipList("B6F7A8F8-8E8C-4B9F-8C8F-4A9C8C8C8C8C", nameof(PurchaseOrder.LocationIDOrigin))]
        public IReadOnlyCollection<PurchaseOrder> PurchaseOrderOrigins
        {
            get { CheckGet(); return _purchaseOrderOrigins; }
        }

        private List<PurchaseOrder> _purchaseOrderDestinations = new List<PurchaseOrder>();
        [RelationshipList("CE9D354F-D707-4511-9700-F47A1C8793E3", nameof(PurchaseOrder.LocationIDDestination))]
        public IReadOnlyCollection<PurchaseOrder> PurchaseOrderDestinations
        {
            get { CheckGet(); return _purchaseOrderDestinations; }
        }

        private List<PurchaseOrderTemplateFolder> _purchaseOrderTemplateFolders = new List<PurchaseOrderTemplateFolder>();
        [RelationshipList("D08A833C-92B7-4C38-9128-F7004A655D43", nameof(PurchaseOrderTemplateFolder.LocationID))]
        public IReadOnlyCollection<PurchaseOrderTemplateFolder> PurchaseOrderTemplateFolders
        {
            get { CheckGet(); return _purchaseOrderTemplateFolders; }
        }

        private List<PurchaseOrderTemplate> _purchaseOrderTemplates = new List<PurchaseOrderTemplate>();
        [RelationshipList("2F0268B7-5364-45CF-B0F6-691A7241B841", nameof(PurchaseOrderTemplate.LocationID))]
        public IReadOnlyCollection<PurchaseOrderTemplate> PurchaseOrderTemplates
        {
            get { CheckGet(); return _purchaseOrderTemplates; }
        }
        #endregion
        #endregion
    }
}
