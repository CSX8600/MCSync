using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.Schema.Attributes;
using ClussPro.ObjectBasedFramework.Validation.Attributes;
using WebModels.company;
using WebModels.gov;

namespace WebModels.purchasing
{
    [Table("BEC03B48-B942-464C-906C-7FDCDD74966F")]
    public class PurchaseOrderApproval : DataObject
    {
        public const string DESTINATION_PURPOSE = "Purchase Order Recipient";
        public const string ROUTE_PURPOSE = "Purchase Order Carrier";

        protected PurchaseOrderApproval() : base() { }

        private long? _purchaseOrderApprovalID;
        [Field("CD6051AC-19EF-484F-8F27-F486D7570163")]
        public long? PurchaseOrderApprovalID
        {
            get { CheckGet(); return _purchaseOrderApprovalID; }
            set { CheckSet(); _purchaseOrderApprovalID = value; }
        }

        private long? _purchaseOrderID;
        [Field("96136A70-A56D-497E-BB83-806C4724A0BD")]
        [Required]
        public long? PurchaseOrderID
        {
            get { CheckGet(); return _purchaseOrderID; }
            set { CheckSet(); _purchaseOrderID = value; }
        }

        private PurchaseOrder _purchaseOrder = null;
        [Relationship("AB681585-9364-4F5F-B47F-94AA9FB2CE4D")]
        public PurchaseOrder PurchaseOrder
        {
            get { CheckGet(); return _purchaseOrder; }
        }

        private long? _companyIDApprover;
        [Field("ED7CCA7B-708B-4CB2-AEED-737E030215CA")]
        public long? CompanyIDApprover
        {
            get { CheckGet(); return _companyIDApprover; }
            set { CheckSet(); _companyIDApprover = value; }
        }

        private Company _companyApprover = null;
        [Relationship("0C673D3B-CC4A-41A3-B7CB-4D019F4509EE", ForeignKeyField = nameof(CompanyIDApprover))]
        public Company CompanyApprover
        {
            get { CheckGet(); return _companyApprover; }
        }

        private long? _governmentIDApprover;
        [Field("22034571-6915-4430-9004-75F9D629CF43")]
        public long? GovernmentIDApprover
        {
            get { CheckGet(); return _governmentIDApprover; }
            set { CheckSet(); _governmentIDApprover = value; }
        }

        private Government _governmentApprover = null;
        [Relationship("2EEEFF85-A545-4C00-943D-85F3BA5312BC", ForeignKeyField = nameof(GovernmentIDApprover))]
        public Government GovernmentApprover
        {
            get { CheckGet(); return _governmentApprover; }
        }

        public enum ApprovalStatuses
        {
            Pending,
            Approved,
            Rejected
        }

        private ApprovalStatuses _approvalStatus = ApprovalStatuses.Pending;
        [Field("FDAF03E9-E771-4334-BEEE-52D9C8CBE75E")]
        public ApprovalStatuses ApprovalStatus
        {
            get { CheckGet(); return _approvalStatus; }
            set { CheckSet(); _approvalStatus = value; }
        }

        private string _approvalPurpose;
        [Field("60F49EBA-3BE9-4DFB-ABA3-6976AB197C1A", DataSize = 200)]
        public string ApprovalPurpose
        {
            get { CheckGet(); return _approvalPurpose; }
            set { CheckSet(); _approvalPurpose = value; }
        }

        private string _rejectionReason;
        [Field("6EE6D333-27F9-42D9-9739-C70F75572D7E", DataSize = 200)]
        public string RejectionReason
        {
            get { CheckGet(); return _rejectionReason; }
            set { CheckSet(); _rejectionReason = value; }
        }

        private bool _futureAutoApprove = false;
        [Field("A56CCCB9-6760-46FB-A682-F6A507484A39")]
        public bool FutureAutoApprove
        {
            get { CheckGet(); return _futureAutoApprove; }
            set { CheckSet(); _futureAutoApprove = value; }
        }
    }
}
