namespace GovernmentPortal.Models
{
    public class Official
    {
        public long OfficialID { get; set; }
        public long GovernmentID { get; set; }
        public long UserID { get; set; }
        public bool ManageEmails { get; set; }
        public bool ManageOfficials { get; set; }
        public bool ManageAccounts { get; set; }
        public bool ManageTaxes { get; set; }
        public string OfficialName { get; set; }
        public bool CanMintCurrency { get; set; }
        public bool CanConfigureInterest { get; set; }
        public bool ManageInvoices { get; set; }
        public bool IssueWireTransfers { get; set; }
        public bool ManageLaws { get; set; }
        public bool ManagePurchaseOrders { get; set; }
        public FleetTracking.Models.FleetSecurity FleetSecurity { get; set; }
    }
}
