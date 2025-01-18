using FleetTracking.Models;
using GovernmentPortal.Extensions;
using MesaSuite.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GovernmentPortal.Purchasing.PurchaseOrderScreen.Draft
{
    public partial class frmDraftRailcarSelect : Form
    {
        public long GovernmentID { get; set; }
        public long? SelectedRailcarID { get; private set; }
        private List<Railcar> _railcars = new List<Railcar>();

        public frmDraftRailcarSelect()
        {
            InitializeComponent();
        }

        private async void frmDraftRailcarSelect_Load(object sender, EventArgs e)
        {
            await ReloadData();
            await DisplayFilteredData();
        }

        private async Task ReloadData()
        {
            GetData get = new GetData(DataAccess.APIs.GovernmentPortal, "Railcar/GetIdleForGovernment");
            get.AddGovHeader(GovernmentID);
            _railcars = await get.GetObject<List<Railcar>>() ?? new List<Railcar>();
        }

        private async Task DisplayFilteredData()
        {
            dgvRailcars.Rows.Clear();
            imageDisposer.DisposeAllImages();

            Func<Railcar, bool> filter;
            if (rdoFilterOwned.Checked)
            {
                filter = r => r.GovernmentIDOwner == GovernmentID;
            }
            else if (rdoFilterLeased.Checked)
            {
                filter = r => r.GovernmentLeasedTo?.GovernmentID == GovernmentID;
            }
            else
            {
                filter = _ => true;
            }

            foreach (Railcar railcar in _railcars.Where(filter).OrderBy(r => r.FormattedReportingMark))
            {
                DataGridViewRow row = dgvRailcars.Rows[dgvRailcars.Rows.Add()];
                row.Cells[colReportingMark.Name].Value = railcar.FormattedReportingMark;
                row.Cells[colModel.Name].Value = railcar.RailcarModel.Name;
                row.Cells[colLocation.Name].Value = railcar.Location;
                row.Cells[colDestination.Name].Value = railcar.TrackDestination?.Name;
                row.Cells[colReleasedTo.Name].Value = string.IsNullOrEmpty(railcar.GovernmentPossessor?.Name) ? string.IsNullOrEmpty(railcar.CompanyPossessor?.Name) ? "[Unknown]" : railcar.CompanyPossessor.Name : railcar.GovernmentPossessor.Name + " (Government)";
                row.Cells[colLeased.Name].Value = railcar.GovernmentLeasedTo?.GovernmentID == GovernmentID;
                row.Tag = railcar;
            }

            GetData get = new GetData(DataAccess.APIs.GovernmentPortal, null);
            get.AddGovHeader(GovernmentID);

            try
            {
                foreach (DataGridViewRow row in dgvRailcars.Rows)
                {
                    if (!(row.Tag is Railcar railcar))
                    {
                        continue;
                    }

                    get.Resource = "Railcar/GetImage/" + railcar.RailcarID;
                    byte[] imageBytes = await get.GetObject<byte[]>();
                    if (imageBytes == null)
                    {
                        continue;
                    }

                    using (StreamReader reader = new StreamReader(new MemoryStream(imageBytes)))
                    {
                        Image image = Image.FromStream(reader.BaseStream);
                        imageDisposer.Images.Add(image);
                        row.Cells[colImage.Name].Value = image;
                    }
                }
            }
            catch { }
        }

        private async void FilterCheckedChanged(object sender, EventArgs e)
        {
            if (!((RadioButton)sender).Checked)
            {
                return;
            }

            await DisplayFilteredData();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvRailcars.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
            if (row == null || !(row.Tag is Railcar railcar))
            {
                SelectedRailcarID = null;
            }
            else
            {
                SelectedRailcarID = railcar.RailcarID;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
