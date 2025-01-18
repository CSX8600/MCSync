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
using FleetTracking.Attributes;
using FleetTracking.Interop;
using FleetTracking.Models;
using FleetTracking.Reports.TrackListing;
using FleetTracking.Train;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using MesaSuite.Common.Utility;

namespace FleetTracking.Tracks
{
    [SecuredControl(SecuredControlAttribute.Permissions.AllowSetup, SecuredControlAttribute.Permissions.IsYardmaster, SecuredControlAttribute.Permissions.AllowLoadUnload)]
    public partial class TrackViewer : UserControl, IFleetTrackingControl
    {
        public TrackViewer()
        {
            InitializeComponent();
            dataGridViewStylizer.ApplyStyle(dgvStock);
        }

        private FleetTrackingApplication _application;
        public FleetTrackingApplication Application { set => _application = value; }

        public long? InitialTrackID { get; set; }

        private async void TrackViewer_Load(object sender, EventArgs e)
        {
            ParentForm.Text = "Track Viewer";
            LoadTracks(InitialTrackID);
        }

        private async void LoadTracks(long? selectedTrackID = null)
        {
            try
            {
                cboTrack.SelectedItem = null;
                cboTrack.Items.Clear();

                loaderMain.BringToFront();
                loaderMain.Visible = true;

                GetData get = _application.GetAccess<GetData>();
                get.API = DataAccess.APIs.FleetTracking;
                get.Resource = "Track/GetAll";

                List<Track> tracks = await get.GetObject<List<Track>>() ?? new List<Track>();
                tracks = tracks.OrderBy(t => t.Name).ToList();
                foreach (Track track in tracks)
                {
                    DropDownItem<Track> trackDDI = new DropDownItem<Track>(track, track.Name);
                    cboTrack.Items.Add(trackDDI);

                    if (track.TrackID == selectedTrackID)
                    {
                        cboTrack.SelectedItem = trackDDI;
                    }
                }

                cboOwner.Items.Clear();
                get.Resource = "Company/GetAll";
                List<Company> companies = await get.GetObject<List<Company>>() ?? new List<Company>();
                companies = companies.OrderBy(c => c.Name).ToList();

                foreach(Company company in companies)
                {
                    DropDownItem<Company> ddi = new DropDownItem<Company>(company, company.Name);
                    cboOwner.Items.Add(ddi);
                }

                get.Resource = "Government/GetAll";
                List<Government> governments = await get.GetObject<List<Government>>() ?? new List<Government>();
                governments = governments.OrderBy(c => c.Name).ToList();

                foreach(Government government in governments)
                {
                    DropDownItem<Government> ddi = new DropDownItem<Government>(government, government.Name);
                    cboOwner.Items.Add(ddi);
                }
            }
            finally
            {
                loaderMain.Visible = false;
            }

            cboTrack_SelectedIndexChanged(cboTrack, EventArgs.Empty);
        }

        private async void PopulateStock()
        {
            DropDownItem<Track> selectedItem = cboTrack.SelectedItem as DropDownItem<Track>;
            if (selectedItem == null)
            {
                return;
            }

            try
            {
                loaderStock.BringToFront();
                loaderStock.Visible = true;
                dgvStock.Rows.Clear();

                GetData get = _application.GetAccess<GetData>();
                get.API = DataAccess.APIs.FleetTracking;
                get.Resource = "RailLocation/GetByTrack";
                
                if (selectedItem.Object?.TrackID != null)
                {
                    get.Resource = $"RailLocation/GetByTrack/{selectedItem.Object.TrackID}";
                }
                else
                {
                    get.Resource = "RailLocation/GetByTrack";
                }
                
                List<RailLocation> railLocations = await get.GetObject<List<RailLocation>>() ?? new List<RailLocation>();
                railLocations = railLocations.OrderBy(rl => rl.Position).ToList();
                foreach(RailLocation railLocation in railLocations)
                {
                    int rowIndex = dgvStock.Rows.Add();
                    DataGridViewRow row = dgvStock.Rows[rowIndex];
                    
                    if (railLocation.Railcar?.RailcarID != null)
                    {
                        row.Cells[colReportingMark.Name].Value = railLocation.Railcar.FormattedReportingMark;
                        row.Cells[colType.Name].Value = railLocation.Railcar.RailcarModel?.Name;
                        row.Cells[colPos.Name].Value = railLocation.Position;
                        row.Cells[colDest.Name].Value = railLocation.Railcar.TrackDestination?.Name;
                        row.Cells[colStrategicDest.Name].Value = railLocation.Railcar.TrackStrategic?.Name;
                        row.Cells[colContents.Name].Value = railLocation.Railcar.FormattedRailcarLoads;
                    }
                    else if (railLocation.Locomotive?.LocomotiveID != null)
                    {
                        row.Cells[colReportingMark.Name].Value = railLocation.Locomotive.FormattedReportingMark;
                        row.Cells[colType.Name].Value = railLocation.Locomotive.LocomotiveModel?.Name;
                        row.Cells[colPos.Name].Value = railLocation.Position;
                    }

                    row.Tag = railLocation;
                }
            }
            finally
            {
                loaderStock.Visible = false;
            }

            try
            {
                GetData get = _application.GetAccess<GetData>();
                get.API = DataAccess.APIs.FleetTracking;
                
                foreach(DataGridViewRow row in dgvStock.Rows)
                {
                    RailLocation railLocation = row.Tag as RailLocation;
                    if (railLocation == null)
                    {
                        continue;
                    }

                    byte[] imageData = null;
                    if (railLocation.Railcar?.RailcarID != null)
                    {
                        get.Resource = $"Railcar/GetImageThumbnail/{railLocation.RailcarID}";
                        imageData = await get.GetObject<byte[]>();
                    }
                    else if (railLocation.Locomotive?.LocomotiveID != null)
                    {
                        get.Resource = $"Locomotive/GetImageThumbnail/{railLocation.LocomotiveID}";
                        imageData = await get.GetObject<byte[]>();
                    }

                    if (imageData != null)
                    {
                        using (MemoryStream memoryStream = new MemoryStream(imageData))
                        {
                            Image image = Image.FromStream(memoryStream);
                            row.Cells[colImage.Name].Value = image;
                        }
                    }
                }
            }
            catch { }
        }

        private void toolAddTrack_Click(object sender, EventArgs e)
        {
            cboTrack.SelectedItem = null;
            cboOwner.SelectedItem = null;
            txtDistrict.Clear();
            txtName.Clear();
            txtName.Enabled = true;
            txtLength.Clear();
            cboOwner.Enabled = true;
            txtLength.Enabled = true;
            cmdSave.Enabled = true;
            cmdReset.Enabled = true;
            dgvStock.Rows.Clear();

            (long?, long?) companyIDGovernmentID = _application.GetCurrentCompanyIDGovernmentID();
            if (companyIDGovernmentID.Item1 != null)
            {
                cboOwner.SelectedItem = cboOwner.Items.OfType<DropDownItem<Company>>().FirstOrDefault(ddi => ddi.Object.CompanyID == companyIDGovernmentID.Item1);
            }
            else
            {
                cboOwner.SelectedItem = cboOwner.Items.OfType<DropDownItem<Government>>().FirstOrDefault(ddi => ddi.Object.GovernmentID == companyIDGovernmentID.Item2);
            }

            txtName.Focus();
        }

        private async void cmdSave_Click(object sender, EventArgs e)
        {
            if (!this.AreFieldsPresent(new List<(string, Control)>()
            {
                ("Name", txtName),
                ("Length", txtLength),
                ("Owner", cboOwner)
            }))
            {
                return;
            }

            if (!decimal.TryParse(txtLength.Text, out decimal length))
            {
                this.ShowError("Length must be a valid number");
                return;
            }

            try
            {
                loaderMain.BringToFront();
                loaderMain.Visible = true;

                DropDownItem<Track> selectedTrack = cboTrack.SelectedItem as DropDownItem<Track>;
                Track track = new Track()
                {
                    TrackID = selectedTrack?.Object?.TrackID,
                    RailDistrictID = selectedTrack?.Object?.RailDistrictID,
                    Name = txtName.Text,
                    Length = length,
                    CompanyIDOwner = cboOwner.SelectedItem.Cast<DropDownItem<Company>>()?.Object.CompanyID,
                    GovernmentIDOwner = cboOwner.SelectedItem.Cast<DropDownItem<Government>>()?.Object.GovernmentID
                };

                if (selectedTrack?.Object?.TrackID == null)
                {
                    PostData post = _application.GetAccess<PostData>();
                    post.API = DataAccess.APIs.FleetTracking;
                    post.Resource = "Track/Post";
                    post.ObjectToPost = track;
                    Track savedTrack = await post.Execute<Track>();
                    if (post.RequestSuccessful)
                    {
                        LoadTracks(savedTrack.TrackID);
                    }
                }
                else
                {
                    PutData put = _application.GetAccess<PutData>();
                    put.API = DataAccess.APIs.FleetTracking;
                    put.Resource = "Track/Put";
                    put.ObjectToPut = track;
                    await put.ExecuteNoResult();
                    if (put.RequestSuccessful)
                    {
                        LoadTracks(selectedTrack.Object.TrackID);
                    }
                }
            }
            finally
            {
                loaderMain.Visible = false;
            }
        }

        private void cboTrack_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownItem<Track> selectedTrack = cboTrack.SelectedItem as DropDownItem<Track>;
            cmdSave.Enabled = selectedTrack?.Object?.TrackID == null || _application.IsCurrentEntity(selectedTrack?.Object?.CompanyIDOwner, selectedTrack?.Object?.GovernmentIDOwner);
            cmdReset.Enabled = selectedTrack?.Object?.TrackID == null || _application.IsCurrentEntity(selectedTrack?.Object?.CompanyIDOwner, selectedTrack?.Object?.GovernmentIDOwner);
            cboOwner.Enabled = _application.IsCurrentEntity(selectedTrack?.Object?.CompanyIDOwner, selectedTrack?.Object?.GovernmentIDOwner);
            txtName.Enabled = _application.IsCurrentEntity(selectedTrack?.Object?.CompanyIDOwner, selectedTrack?.Object?.GovernmentIDOwner);
            txtLength.Enabled = _application.IsCurrentEntity(selectedTrack?.Object?.CompanyIDOwner, selectedTrack?.Object?.GovernmentIDOwner);
            toolDeleteTrack.Enabled = _application.IsCurrentEntity(selectedTrack?.Object?.CompanyIDOwner, selectedTrack?.Object?.GovernmentIDOwner);

            if (selectedTrack?.Object?.TrackID == null)
            {
                dgvStock.Rows.Clear();
                cboOwner.SelectedItem = null;
                txtDistrict.Clear();
                txtName.Clear();
                txtLength.Clear();
                toolPrint.Enabled = false;
                tsmiPrintBOLs.Enabled = false;
                return;
            }

            toolPrint.Enabled = true;
            tsmiPrintBOLs.Enabled = true;
            cboOwner.SelectedItem = selectedTrack.Object.GovernmentIDOwner != null ?
                                    (object)cboOwner.Items.OfType<DropDownItem<Government>>().FirstOrDefault(ddi => ddi.Object.GovernmentID == selectedTrack.Object.GovernmentIDOwner) :
                                    cboOwner.Items.OfType<DropDownItem<Company>>().FirstOrDefault(ddi => ddi.Object.CompanyID == selectedTrack.Object.CompanyIDOwner);
            txtDistrict.Text = selectedTrack.Object.RailDistrict?.Name;
            txtName.Text = selectedTrack.Object.Name;
            txtLength.Text = selectedTrack.Object.Length?.ToString();

            PopulateStock();
        }

        private async void toolDeleteTrack_Click(object sender, EventArgs e)
        {
            DropDownItem<Track> selectedTrack = cboTrack.SelectedItem as DropDownItem<Track>;
            if (selectedTrack == null || !_application.IsCurrentEntity(selectedTrack.Object?.CompanyIDOwner, selectedTrack.Object?.GovernmentIDOwner))
            {
                return;
            }

            if (!this.Confirm("Are you sure you want to delete this Track?"))
            {
                return;
            }

            try
            {
                loaderMain.BringToFront();
                loaderMain.Visible = true;

                DeleteData delete = _application.GetAccess<DeleteData>();
                delete.API = DataAccess.APIs.FleetTracking;
                delete.Resource = $"Track/Delete/{selectedTrack.Object.TrackID}";
                await delete.Execute();

                if (delete.RequestSuccessful)
                {
                    LoadTracks();
                }
            }
            finally
            {
                loaderMain.Visible = false;
            }
        }

        private void toolModify_Click(object sender, EventArgs e)
        {
            DropDownItem<Track> selectedTrack = cboTrack.SelectedItem as DropDownItem<Track>;

            RailLocationModifier modifier = new RailLocationModifier()
            {
                Application = _application,
                SelectedTrackID = selectedTrack?.Object?.TrackID
            };

            _application.OpenForm(modifier, FleetTrackingApplication.OpenFormOptions.Dialog);

            PopulateStock();
        }

        private void toolPrint_Click(object sender, EventArgs e)
        {
            DropDownItem<Track> selectedItem = cboTrack.SelectedItem as DropDownItem<Track>;
            if (selectedItem == null)
            {
                return;
            }

            PrintableReport report = new PrintableReport()
            {
                Application = _application,
                ReportContext = new TrackListingReportContext(new[] { selectedItem.Object.TrackID }) { Application = _application }
            };

            _application.OpenForm(report);
        }

        private void cmdReset_Click(object sender, EventArgs e)
        {
            cboTrack_SelectedIndexChanged(cboTrack, EventArgs.Empty);
        }

        private void dgvStock_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvStock.Rows.Count)
            {
                return;
            }

            RailLocation location = dgvStock.Rows[e.RowIndex].Tag as RailLocation;
            if (location == null)
            {
                return;
            }

            if (location.RailcarID != null)
            {
                Roster.RailcarDetail detail = new Roster.RailcarDetail()
                {
                    Application = _application,
                    RailcarID = location.RailcarID
                };

                _application.OpenForm(detail);
            }
            else if (location.LocomotiveID != null)
            {
                Roster.LocomotiveDetail detail = new Roster.LocomotiveDetail()
                {
                    Application = _application,
                    LocomotiveID = location.LocomotiveID
                };

                _application.OpenForm(detail);
            }
        }

        private async void tsmiPrintBOLs_Click(object sender, EventArgs e)
        {
            DropDownItem<Track> selectedItem = cboTrack.SelectedItem as DropDownItem<Track>;
            if (selectedItem == null)
            {
                return;
            }

            BOLRailcarPicker railcarPicker = new BOLRailcarPicker()
            {
                TrackIDs = { selectedItem.Object.TrackID },
                Application = _application
            };

            _application.OpenForm(railcarPicker, FleetTrackingApplication.OpenFormOptions.Dialog);
        }
    }
}
