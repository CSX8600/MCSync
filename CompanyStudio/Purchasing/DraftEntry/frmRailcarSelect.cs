using CompanyStudio.Extensions;
using CompanyStudio.Models;
using MesaSuite.Common.Data;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Forms;

namespace CompanyStudio.Purchasing.DraftEntry
{
    public partial class frmRailcarSelect : BaseCompanyStudioContent, ILocationScoped
    {
        private List<Railcar> _railcars = new List<Railcar>();
        private List<Image> _images = new List<Image>();
        public frmRailcarSelect()
        {
            InitializeComponent();
            DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Float;
        }

        public Location LocationModel { get; set; }

        public long? SelectedRailcarID { get; set; }

        private async void frmRailcarSelect_Load(object sender, EventArgs e)
        {
            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Railcar/GetIdleForCompany");
            get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            _railcars = await get.GetObject<List<Railcar>>() ?? new List<Railcar>();

            PopulateGrid();
        }

        private async void PopulateGrid()
        {
            dgvRailcars.Rows.Clear();
            foreach(Image image in _images)
            {
                image.Dispose();
            }

            Func<Railcar, bool> filter = r =>
            {
                if (rdoFilterAll.Checked)
                {
                    return true;
                }
                else if (rdoFilterLeased.Checked)
                {
                    return r.CompanyIDOwner != Company.CompanyID;
                }
                else if (rdoFilterOwned.Checked)
                {
                    return r.CompanyIDOwner == Company.CompanyID;
                }

                return false;
            };

            foreach(Railcar railcar in _railcars.Where(filter).OrderBy(r => r.ReportingID))
            {
                int rowIndex = dgvRailcars.Rows.Add();
                DataGridViewRow row = dgvRailcars.Rows[rowIndex];
                row.Cells[colReportingMark.Name].Value = railcar.ReportingID;
                row.Cells[colLocation.Name].Value = railcar.RailLocation?.CurrentLocation;
                row.Cells[colDestination.Name].Value = railcar.TrackDestination?.Name;
                row.Cells[colReleasedTo.Name].Value = railcar.CompanyPossessor?.Name ?? railcar.GovernmentPossessor?.Name;
                row.Cells[colLeased.Name].Value = railcar.CompanyIDOwner != Company.CompanyID;
                row.Cells[colModel.Name].Value = railcar.RailcarModel?.Name;
                row.Tag = railcar;
            }

            if (SelectedRailcarID != null)
            {
                foreach(DataGridViewRow row in dgvRailcars.Rows)
                {
                    row.Selected = (row.Tag as Railcar)?.RailcarID == SelectedRailcarID;
                }
            }

            try
            {
                foreach (DataGridViewRow row in dgvRailcars.Rows)
                {
                    Railcar railcar = row.Tag as Railcar;
                    if (railcar != null)
                    {
                        Image image = await railcar.GetImage(Company.CompanyID, LocationModel.LocationID);
                        _images.Add(image);
                        row.Cells[colImage.Name].Value = image;
                    }
                }
            }
            catch { }
        }

        private void Filter_CheckedChanged(object sender, EventArgs e)
        {
            PopulateGrid();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            SelectedRailcarID = (dgvRailcars.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault()?.Tag as Railcar)?.RailcarID;

            DialogResult = DialogResult.OK;
            Close();
            Dispose();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
            Dispose();
        }
    }
}
