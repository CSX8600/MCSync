namespace CompanyStudio.Models
{
    public class PurchaseOrderApproval
    {
        public long? PurchaseOrderApprovalID { get; set; }
        public long? PurchaseOrderID { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
        public long? CompanyIDApprover { get; set; }
        public Company CompanyApprover { get; set; }
        public long? GovernmentIDApprover { get; set; }
        public Government GovernmentApprover { get; set; }
        public enum ApprovalStatuses
        {
            Pending,
            Approved,
            Rejected
        }
        public ApprovalStatuses ApprovalStatus { get; set; }
        public string ApprovalPurpose { get; set; }
        public string RejectionReason { get; set; }
        public bool FutureAutoApprove { get; set; }
    }
}
