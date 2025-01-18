using FleetTracking.Models;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GovernmentPortal.Purchasing.PurchaseOrderScreen.Draft
{
    public partial class frmLeaseRequestSelect : Form
    {
        public long GovernmentID { get; set; }
        public long? SelectedLeaseRequestID { get; private set; }
        public frmLeaseRequestSelect()
        {
            InitializeComponent();
        }

        private async void frmLeaseRequestSelect_Load(object sender, EventArgs e)
        {
            GetData get = new GetData(DataAccess.APIs.FleetTracking, "LeaseRequest/GetAll");
            List<LeaseRequest> leaseRequests = await get.GetObject<List<LeaseRequest>>() ?? new List<LeaseRequest>();

            foreach(LeaseRequest leaseRequest in leaseRequests.Where(lr => lr.GovernmentIDRequester == GovernmentID).OrderBy(l => l.LeaseRequestID))
            {
                DataGridViewRow row = dgvLeaseRequests.Rows[dgvLeaseRequests.Rows.Add()];
                row.Cells[colRequestID.Name].Value = leaseRequest.LeaseRequestID.ToString();
                row.Cells[colLeaseType.Name].Value = leaseRequest.LeaseType.ToString().ToDisplayName();
                row.Cells[colEndTime.Name].Value = leaseRequest.BidEndTime?.ToString("MM/dd/yyyy HH:mm:ss");
                row.Cells[colBids.Name].Value = leaseRequest.LeaseBids?.Count.ToString() ?? "0";
                row.Cells[colPurpose.Name].Value = leaseRequest.Purpose;
                row.Tag = leaseRequest;
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            DataGridViewRow dataGridViewRow = dgvLeaseRequests.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
            if (dataGridViewRow != null && dataGridViewRow.Tag is LeaseRequest leaseRequest)
            {
                SelectedLeaseRequestID = leaseRequest.LeaseRequestID;
            }
            else
            {
                SelectedLeaseRequestID = null;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
