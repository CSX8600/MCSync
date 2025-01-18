using CompanyStudio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyStudio.Purchasing.ApprovalViewer
{
    [ToolboxItem(false)]
    public partial class ApprovalControl : UserControl
    {
        public PurchaseOrderApproval PurchaseOrderApproval { get; set; }
        public ApprovalControl()
        {
            InitializeComponent();
        }

        private void ApprovalControl_SizeChanged(object sender, EventArgs e)
        {
            int textBoxSize = (this.Width - 40) / 3;

            txtApprover.Width = textBoxSize;
            txtReasonAssigned.Left = txtApprover.Left + txtApprover.Width + 6;
            txtReasonAssigned.Width = textBoxSize;
            txtRejectReason.Left = txtReasonAssigned.Left + txtReasonAssigned.Width + 6;
            txtRejectReason.Width = textBoxSize;

            lblApprover.Left = txtApprover.Left - 3;
            lblReasonAssigned.Left = txtReasonAssigned.Left - 3;
            lblRejectReason.Left = txtRejectReason.Left - 3;
        }

        private void ApprovalControl_Load(object sender, EventArgs e)
        {
            Image image;
            switch(PurchaseOrderApproval.ApprovalStatus)
            {
                case PurchaseOrderApproval.ApprovalStatuses.Approved:
                    image = Properties.Resources.accept;
                    break;
                case PurchaseOrderApproval.ApprovalStatuses.Rejected:
                    image = Properties.Resources.cross;
                    break;
                case PurchaseOrderApproval.ApprovalStatuses.Pending:
                    image = Properties.Resources.hourglass;
                    break;
                default:
                    image = Properties.Resources.help;
                    break;
            }

            picIcon.Image = image;
            txtApprover.Text = PurchaseOrderApproval.CompanyApprover?.Name ?? PurchaseOrderApproval.GovernmentApprover?.Name;
            txtReasonAssigned.Text = PurchaseOrderApproval.ApprovalPurpose;
            txtRejectReason.Text = PurchaseOrderApproval.RejectionReason;
        }
    }
}
