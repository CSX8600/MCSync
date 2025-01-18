using ClussPro.Base.Data;
using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Schema.Attributes;
using ClussPro.ObjectBasedFramework.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using WebModels.account;
using WebModels.hMailServer.dbo;
using WebModels.mesasys;
using WebModels.purchasing;

namespace WebModels.company
{
    [Table("A093C2E1-2DF9-4BF2-A56C-2C3B99073792")]
    public class Company : DataObject
    {
        protected Company() : base() { }

        private long? _companyID;
        [Field("4C5C6ED3-055B-405C-B888-4B40A799FAC0")]
        public long? CompanyID
        {
            get { CheckGet(); return _companyID; }
            set { CheckSet(); _companyID = value; }
        }

        private string _name;
        [Field("BBEE1AE6-34F3-4BCB-90AA-A137CE7EA655", DataSize = 50)]
        [Required]
        public string Name
        {
            get { CheckGet(); return _name; }
            set { CheckSet(); _name = value; }
        }

        private string _emailDomain;
        [Field("B28C2CCC-5BE7-4667-AB66-96A11067055B", DataSize = 80)]
        public string EmailDomain
        {
            get { CheckGet(); return _emailDomain; }
            set { CheckSet(); _emailDomain = value; }
        }

        private long? _emailImplementationIDWireTransferHistory;
        [Field("ADCD5F15-B180-4BEC-9CFD-E8FDB72BC51B")]
        public long? EmailImplementationIDWireTransferHistory
        {
            get { CheckGet(); return _emailImplementationIDWireTransferHistory; }
            set { CheckSet();_emailImplementationIDWireTransferHistory = value; }
        }

        private EmailImplementation _emailImplementationWireTransferHistory = null;
        [Relationship("E57BB310-8467-4963-AE74-0D7CDFD76E07", ForeignKeyField = nameof(EmailImplementationIDWireTransferHistory))]
        public EmailImplementation EmailImplementationWireTransferHistory
        {
            get { CheckGet(); return _emailImplementationWireTransferHistory; }
        }

        private fleet.MiscellaneousSettings _fleetMiscSettings = null;
        [Relationship("0307C719-B4CD-4F99-9704-E1C20A5950BF", OneToOneByForeignKey = true)]
        public fleet.MiscellaneousSettings FleetMiscSettings
        {
            get { CheckGet(); return _fleetMiscSettings; }
        }

        protected override bool PostSave(ITransaction transaction)
        {
            if (!IsInsert)
            {
                bool savesSuccessful = true;
                if (IsFieldDirty("EmailDomain") && !string.IsNullOrEmpty(EmailDomain))
                {
                    savesSuccessful &= UpdateEmailInfo();
                }

                if (IsFieldDirty(nameof(EmailImplementationIDWireTransferHistory)))
                {
                    long? previousEmailImpID = GetDirtyValue(nameof(EmailImplementationIDWireTransferHistory)) as long?;
                    if (previousEmailImpID != null)
                    {
                        EmailImplementation oldImplementation = DataObject.GetEditableByPrimaryKey<EmailImplementation>(previousEmailImpID, transaction, null);
                        if (!oldImplementation.Delete(transaction))
                        {
                            Errors.AddRange(oldImplementation.Errors.ToArray());
                            savesSuccessful = false;
                        }
                    }
                }

                return savesSuccessful;
            }

            return base.PostSave(transaction);
        }

        private bool UpdateEmailInfo()
        {
            string oldEmailInfo = (string)GetDirtyValue("EmailDomain");
            using (ITransaction transaction = SQLProviderFactory.GenerateTransaction("hmailserver"))
            {
                Search<Alias> aliasSearch = new Search<Alias>(new StringSearchCondition<Alias>()
                {
                    Field = "AliasName",
                    SearchConditionType = SearchCondition.SearchConditionTypes.Like,
                    Value = $"%@{oldEmailInfo}"
                });

                foreach(Alias alias in aliasSearch.GetEditableReader())
                {
                    alias.AliasName = alias.AliasName.Replace($"@{oldEmailInfo}", $"@{EmailDomain}");

                    if (!alias.Save(transaction, new List<Guid>() { Alias.ValidationFlags.V_AddressEndsWithCompanyAddress }))
                    {
                        Errors.AddBaseMessage($"Could not update Alias with ID {alias.AliasID}:\r\n{alias.Errors.ToString()}");
                        return false;
                    }
                }

                Search<DistributionList> distListSearch = new Search<DistributionList>(new StringSearchCondition<DistributionList>()
                {
                    Field = "DistributionListAddress",
                    SearchConditionType = SearchCondition.SearchConditionTypes.Like,
                    Value = $"%@{oldEmailInfo}"
                });

                foreach(DistributionList distList in distListSearch.GetEditableReader())
                {
                    distList.DistributionListAddress = distList.DistributionListAddress.Replace($"@{oldEmailInfo}", $"@{EmailDomain}");

                    if (!distList.Save(transaction, new List<Guid>() { DistributionList.ValidationFlags.V_AddressEndsWithCompanyAddress }))
                    {
                        Errors.AddBaseMessage($"Could not update Distribution List with ID {distList.DistributionListID}:\r\n{distList.Errors.ToString()}");
                        return false;
                    }
                }

                transaction.Commit();
            }

            return true;
        }

        #region Relationships
        #region account
        private List<Account> _accounts = new List<Account>();
        [RelationshipList("010B01A9-4656-4327-9B89-5B234EA7DF7D", "CompanyID")]
        public IReadOnlyCollection<Account> Accounts
        {
            get { CheckGet(); return _accounts; }
        }

        private List<Category> _categories = new List<Category>();
        [RelationshipList("83489E3E-4CE9-4D03-AB42-E08D738E74BD", "CompanyID")]
        public IReadOnlyCollection<Category> Categories
        {
            get { CheckGet(); return _categories; }
        }

        private List<account.WireTransferHistory> _wireTransferHistoryFroms = new List<WireTransferHistory>();
        [RelationshipList("1C4ADE88-1C88-49F2-BA2B-4233B163F010", nameof(account.WireTransferHistory.CompanyIDTo))]
        public IReadOnlyCollection<account.WireTransferHistory> WireTransferHistoryFroms
        {
            get { CheckGet(); return _wireTransferHistoryFroms; }
        }

        private List<account.WireTransferHistory> _wireTransferHistoryTos = new List<account.WireTransferHistory>();
        [RelationshipList("72AF3D64-C652-48AE-B43C-369ABDB83F2B", nameof(account.WireTransferHistory.CompanyIDFrom))]
        public IReadOnlyCollection<account.WireTransferHistory> WireTransferHistoryTos
        {
            get { CheckGet(); return _wireTransferHistoryTos; }
        }
        
        #endregion
        #region company
        private List<Employee> _employees = new List<Employee>();
        [RelationshipList("6C0E982B-0D55-466E-8E56-9A466D7A982C", "CompanyID", AutoDeleteReferences = true)]
        public IReadOnlyCollection<Employee> Employees
        {
            get { CheckGet(); return _employees; }
        }
        #endregion
        #region fleet
        private List<fleet.Locomotive> _locomotivesOwned = new List<fleet.Locomotive>();
        [RelationshipList("500A7C05-EA28-4460-80E7-031E2EAB7A4C", nameof(fleet.Locomotive.CompanyIDOwner))]
        public IReadOnlyCollection<fleet.Locomotive> LocomotivesOwned
        {
            get { CheckGet(); return _locomotivesOwned; }
        }

        private List<fleet.Railcar> _railcarsOwned = new List<fleet.Railcar>();
        [RelationshipList("73980C31-26C2-4063-85FF-C9921B5CF0E1", nameof(fleet.Railcar.CompanyIDOwner))]
        public IReadOnlyCollection<fleet.Railcar> RailcarsOwned
        {
            get { CheckGet(); return _railcarsOwned; }
        }

        private List<fleet.Locomotive> _locomotivesPossessed = new List<fleet.Locomotive>();
        [RelationshipList("1B224D6C-9898-4F6D-A281-8F100AA32A81", nameof(fleet.Locomotive.CompanyIDPossessor))]
        public IReadOnlyCollection<fleet.Locomotive> LocomotivesPossessed
        {
            get { CheckGet(); return _locomotivesPossessed; }
        }

        private List<fleet.Railcar> _railcarsPossessed = new List<fleet.Railcar>();
        [RelationshipList("BEC24E2F-9F4C-4372-AD71-877331321DEE", nameof(fleet.Railcar.CompanyIDPossessor))]
        public IReadOnlyCollection<fleet.Railcar> RailcarsPossessed
        {
            get { CheckGet(); return _railcarsPossessed; }
        }

        private List<fleet.LeaseRequest> _leaseRequests = new List<fleet.LeaseRequest>();
        [RelationshipList("ABAD4624-7CB5-49F0-8B49-13C1F3C1788A", nameof(fleet.LeaseRequest.CompanyIDRequester))]
        public IReadOnlyCollection<fleet.LeaseRequest> LeaseRequests
        {
            get { CheckGet(); return _leaseRequests; }
        }

        private List<fleet.LeaseContract> _leaseContracts = new List<fleet.LeaseContract>();
        [RelationshipList("94DF511B-660A-441B-8EE4-F4D434A38EC5", nameof(fleet.LeaseContract.CompanyIDLessee))]
        public IReadOnlyCollection<fleet.LeaseContract> LeaseContracts
        {
            get { CheckGet(); return _leaseContracts; }
        }

        private List<fleet.Track> _tracksOwned = new List<fleet.Track>();
        [RelationshipList("616A35B8-366A-404F-8701-EF609B771622", nameof(fleet.Track.CompanyIDOwner))]
        public IReadOnlyCollection<fleet.Track> TracksOwned
        {
            get { CheckGet(); return _tracksOwned; }
        }

        private List<fleet.TrainSymbol> _trainSymbols = new List<fleet.TrainSymbol>();
        [RelationshipList("A4D050C4-3338-44CC-84E5-B7A98AC7658F", nameof(fleet.TrainSymbol.CompanyIDOperator))]
        public IReadOnlyCollection<fleet.TrainSymbol> TrainSymbols
        {
            get { CheckGet(); return _trainSymbols; }
        }

        private List<fleet.LiveLoadSession> _liveLoadSessions = new List<fleet.LiveLoadSession>();
        [RelationshipList("9DDB3DE6-FEB4-42C7-8AB7-19E44636C0D3", nameof(fleet.LiveLoadSession.CompanyID))]
        public IReadOnlyCollection<fleet.LiveLoadSession> LiveLoadSessions
        {
            get { CheckGet(); return _liveLoadSessions; }
        }

        private List<fleet.RailDistrict> _railDistricts = new List<fleet.RailDistrict>();
        [RelationshipList("5101CE2E-00F0-4559-95D5-F27850C7123A", nameof(fleet.RailDistrict.CompanyIDOperator))]
        public IReadOnlyCollection<fleet.RailDistrict> RailDistricts
        {
            get { CheckGet(); return _railDistricts; }
        }

        private List<fleet.RailcarRoute> _railcarRouteFroms = new List<fleet.RailcarRoute>();
        [RelationshipList("072BD4B3-81C1-4A4E-8BB2-98CDEC88FD7B", nameof(fleet.RailcarRoute.CompanyIDFrom))]
        public IReadOnlyCollection<fleet.RailcarRoute> RailcarRouteFroms
        {
            get { CheckGet(); return _railcarRouteFroms; }
        }

        private List<fleet.RailcarRoute> _railcarRouteTos = new List<fleet.RailcarRoute>();
        [RelationshipList("51D74C31-8373-48BD-A85F-720C2FC847A4", nameof(fleet.RailcarRoute.CompanyIDTo))]
        public IReadOnlyCollection<fleet.RailcarRoute> RailcarRouteTos
        {
            get { CheckGet(); return _railcarRouteTos; }
        }
        #endregion
        #region gov
        private List<Location> _locations = new List<Location>();
        [RelationshipList("FF16FF45-B566-4FA1-B2B4-616F9A58A1CE", "CompanyID")]
        public IReadOnlyCollection<Location> Locations
        {
            get { CheckGet(); return _locations; }
        }
        #endregion
        #region purchasing
        private List<FulfillmentPlanRoute> _fulfillmentPlanRouteFroms = new List<FulfillmentPlanRoute>();
        [RelationshipList("F440E77F-D9E7-4453-BAD4-83ACA83C8A3A", nameof(FulfillmentPlanRoute.CompanyIDFrom))]
        public IReadOnlyCollection<FulfillmentPlanRoute> FulfillmentPlanRouteFroms
        {
            get { CheckGet(); return _fulfillmentPlanRouteFroms; }
        }

        private List<FulfillmentPlanRoute> _fulfillmentPlanRouteTos = new List<FulfillmentPlanRoute>();
        [RelationshipList("5E276A59-1BDE-4423-A691-F26672E8998D", nameof(FulfillmentPlanRoute.CompanyIDTo))]
        public IReadOnlyCollection<FulfillmentPlanRoute> FulfillmentPlanRouteTos
        {
            get { CheckGet(); return _fulfillmentPlanRouteTos; }
        }

        private List<PurchaseOrderApproval> _purchaseOrderApprovals = new List<PurchaseOrderApproval>();
        [RelationshipList("94B116B9-46D3-45B2-87EB-6E30D75E9BE9", nameof(PurchaseOrderApproval.CompanyIDApprover), AutoDeleteReferences = true)]
        public IReadOnlyCollection<PurchaseOrderApproval> PurchaseOrderApprovals
        {
            get { CheckGet(); return _purchaseOrderApprovals; }
        }

        private List<BillOfLading> _billsOfLadingShipper = new List<BillOfLading>();
        [RelationshipList("711B2CCA-5EE5-4779-A0C3-C94A5B1033E3", nameof(BillOfLading.CompanyIDShipper))]
        public IReadOnlyCollection<BillOfLading> BillsOfLadingShipper
        {
            get { CheckGet(); return _billsOfLadingShipper; }
        }

        private List<BillOfLading> _billsOfLadingConsignee = new List<BillOfLading>();
        [RelationshipList("D24794A5-135A-4BE3-B64F-69DBEC7FBED8", nameof(BillOfLading.CompanyIDConsignee))]
        public IReadOnlyCollection<BillOfLading> BillsOfLadingConsignee
        {
            get { CheckGet(); return _billsOfLadingConsignee; }
        }

        private List<BillOfLading> _billsOfLadingCarrier = new List<BillOfLading>();
        [RelationshipList("B5F96C5F-BA50-485B-AAEF-8F2415DC3DD5", nameof(BillOfLading.CompanyIDCarrier))]
        public IReadOnlyCollection<BillOfLading> BillsOfLadingCarrier
        {
            get { CheckGet(); return _billsOfLadingCarrier; }
        }

        private List<QuotationRequest> _quotationRequestFroms = new List<QuotationRequest>();
        [RelationshipList("795CD655-4DB0-43F1-9E6C-3ADCA127E7A3", nameof(QuotationRequest.CompanyIDFrom))]
        public IReadOnlyCollection<QuotationRequest> QuotationRequestFroms
        {
            get { CheckGet(); return _quotationRequestFroms; }
        }

        private List<QuotationRequest> _quotationRequestTos = new List<QuotationRequest>();
        [RelationshipList("768B8575-1802-4EC1-8F37-E006FABFE4D7", nameof(QuotationRequest.CompanyIDTo))]
        public IReadOnlyCollection<QuotationRequest> QuotationRequestTos
        {
            get { CheckGet(); return _quotationRequestTos; }
        }

        private List<Quotation> _quotationFroms = new List<Quotation>();
        [RelationshipList("15988ACB-E277-4CFA-A56A-C1631F366C30", nameof(Quotation.CompanyIDFrom))]
        public IReadOnlyCollection<Quotation> QuotationFroms
        {
            get { CheckGet(); return _quotationFroms; }
        }

        private List<Quotation> _quotationTos = new List<Quotation>();
        [RelationshipList("DB2341F7-2266-4A54-85B6-92F4C56B9302", nameof(Quotation.CompanyIDTo))]
        public IReadOnlyCollection<Quotation> QuotationTos
        {
            get { CheckGet(); return _quotationTos; }
        }
        #endregion
        #endregion
    }
}
