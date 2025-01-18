using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FleetTracking.Attributes;
using FleetTracking.Interop;
using FleetTracking.Models;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;

namespace FleetTracking.Reports.TrackListing
{
    [SecuredControl(SecuredControlAttribute.Permissions.IsYardmaster, SecuredControlAttribute.Permissions.IsTrainCrew, SecuredControlAttribute.Permissions.AllowLoadUnload)]
    public partial class SelectTracks : UserControl, IFleetTrackingControl
    {
        private FleetTrackingApplication _application;
        public FleetTrackingApplication Application { set => _application = value; }

        public SelectTracks()
        {
            InitializeComponent();

            dataGridViewStylizer.ApplyStyle(dgvTracks);
            dgvTracks.ReadOnly = false;
            colTrack.ReadOnly = true;
            colDistrict.ReadOnly = true;

            colCheck.ValueType = typeof(bool?);
        }


        private async void SelectTracks_Load(object sender, EventArgs e)
        {
            ParentForm.Text = "Select Tracks to Print";

            try
            {
                loader.BringToFront();
                loader.Visible = true;

                GetData get = _application.GetAccess<GetData>();
                get.API = DataAccess.APIs.FleetTracking;
                get.Resource = "Track/GetAll";
                List<Track> tracks = await get.GetObject<List<Track>>();
                tracks = tracks.OrderBy(t => t.Name).ToList();
                foreach(Track track in tracks)
                {
                    int rowIndex = dgvTracks.Rows.Add();
                    DataGridViewRow row = dgvTracks.Rows[rowIndex];
                    row.Cells[colTrack.Name].Value = track.Name;
                    row.Cells[colDistrict.Name].Value = track.RailDistrict?.Name;
                    row.Tag = track;
                }
            }
            finally
            {
                loader.Visible = false;
            }
        }

        private void dgvTracks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvTracks.Rows.Count || e.ColumnIndex != colCheck.Index)
            {
                return;
            }

            dgvTracks[e.ColumnIndex, e.RowIndex].Value = !((dgvTracks[e.ColumnIndex, e.RowIndex].Value as bool?) ?? false);
        }

        private void toolCheckAll_Click(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dgvTracks.Rows.OfType<DataGridViewRow>().Where(dgvr => dgvr.Visible))
            {
                row.Cells[colCheck.Name].Value = true;
            }
        }

        private void toolUncheckAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvTracks.Rows.OfType<DataGridViewRow>().Where(dgvr => dgvr.Visible))
            {
                row.Cells[colCheck.Name].Value = false;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dgvTracks.Rows)
            {
                bool visible = true;
                if (!string.IsNullOrEmpty(txtSearch.Text) && row.Tag is Track track)
                {
                    visible = track.Name.ToLowerInvariant().Contains(txtSearch.Text.ToLowerInvariant()) || (track.RailDistrict?.Name != null && track.RailDistrict.Name.ToLowerInvariant().Contains(txtSearch.Text.ToLowerInvariant()));
                }

                row.Visible = visible;
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            ParentForm.Close();
            Dispose();
        }

        private void cmdRunReport_Click(object sender, EventArgs e)
        {
            List<long?> trackIDs = new List<long?>();
            foreach(DataGridViewRow row in dgvTracks.Rows)
            {
                if (!(row.Cells[colCheck.Name].Value as bool? ?? false) || !(row.Tag is Track track))
                {
                    continue;
                }

                trackIDs.Add(track.TrackID);
            }

            PrintableReport report = new PrintableReport()
            {
                Application = _application,
                ReportContext = new TrackListingReportContext(trackIDs) { Application = _application }
            };
            _application.OpenForm(report);

            if (chkPrintBOLs.Checked)
            {
                Tracks.BOLRailcarPicker railcarPicker = new Tracks.BOLRailcarPicker()
                {
                    TrackIDs = trackIDs,
                    Application = _application
                };

                _application.OpenForm(railcarPicker, FleetTrackingApplication.OpenFormOptions.Dialog);
            }

            ParentForm.Close();
            Dispose();
        }
    }
}
