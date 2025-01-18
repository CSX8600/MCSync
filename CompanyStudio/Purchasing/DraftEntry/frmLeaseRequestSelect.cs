using CompanyStudio.Extensions;
using CompanyStudio.Models;
using MesaSuite.Common.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CompanyStudio.Purchasing.DraftEntry
{
    public partial class frmLeaseRequestSelect : BaseCompanyStudioContent, ILocationScoped
    {
        public Location LocationModel { get; set; }

        public long? SelectedLeaseRequestID { get; set; }

        public frmLeaseRequestSelect()
        {
            InitializeComponent();
            DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Float;
        }

        private async void frmLeaseRequestSelect_Load(object sender, EventArgs e)
        {
            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "LeaseRequest/GetAll");
            get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            List<LeaseRequest> leaseRequests = await get.GetObject<List<LeaseRequest>>() ?? new List<LeaseRequest>();

            foreach(LeaseRequest leaseRequest in leaseRequests)
            {
                int rowIndex = dgvLeaseRequests.Rows.Add();
                DataGridViewRow row = dgvLeaseRequests.Rows[rowIndex];
                row.Cells[colRequestID.Name].Value = leaseRequest.LeaseRequestID.ToString();
                string leaseType = leaseRequest.LeaseType.ToString();
                if (leaseRequest.LeaseType == LeaseRequest.LeaseTypes.Railcar)
                {
                    leaseType += " (" + leaseRequest.RailcarType.ToString() + ")";
                }

                row.Cells[colLeaseType.Name].Value = leaseType;
                row.Cells[colEndTime.Name].Value = leaseRequest.BidEndTime?.ToString("MM/dd/yyyy HH:mm");
                row.Cells[colBids.Name].Value = leaseRequest.LeaseBids?.Count() ?? 0;
                row.Cells[colPurpose.Name].Value = leaseRequest.Purpose;
                row.Tag = leaseRequest;
            }

            foreach(DataGridViewRow row in dgvLeaseRequests.Rows)
            {
                row.Selected = (row.Tag as LeaseRequest)?.LeaseRequestID == SelectedLeaseRequestID;
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            SelectedLeaseRequestID = (dgvLeaseRequests.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault()?.Tag as LeaseRequest)?.LeaseRequestID;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            Close();
        }
    }
}
