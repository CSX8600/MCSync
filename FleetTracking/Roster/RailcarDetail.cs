using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FleetTracking.Attributes;
using FleetTracking.Interop;
using FleetTracking.Models;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using MesaSuite.Common.Utility;

namespace FleetTracking.Roster
{
    [SecuredControl(SecuredControlAttribute.Permissions.AllowSetup, SecuredControlAttribute.Permissions.IsTrainCrew, SecuredControlAttribute.Permissions.IsYardmaster)]
    public partial class RailcarDetail : UserControl, IFleetTrackingControl
    {
        public event EventHandler<Models.Railcar> RailcarSaved;

        private FleetTrackingApplication _application;
        public FleetTrackingApplication Application { set => _application = value; }
        public long? RailcarID { get; set; }

        // These properties are conditionally readonly, so in the event they no longer read only, we need to grab the original value
        private long? OriginalTrackIDDestination { get; set; }
        private long? OriginalTrackIDStrategic { get; set; }

        public RailcarDetail()
        {
            InitializeComponent();
            dataGridViewStylizer.ApplyStyle(dgvHistory);
            dataGridViewStylizer.ApplyStyle(dgvRoute);
        }

        private void RailcarDetail_Load(object sender, EventArgs e)
        {
            if (ParentForm != null)
            {
                ParentForm.Text = "Railcar";
            }
            LoadGeneralData();
        }

        public async Task LoadGeneralData()
        {
            loaderGeneral.BringToFront();
            loaderGeneral.Visible = true;

            try
            {
                GetData get = _application.GetAccess<GetData>();
                get.API = DataAccess.APIs.FleetTracking;
                Models.Railcar railcar = new Models.Railcar();
                if (RailcarID != null)
                {
                    get.Resource = $"Railcar/Get/{RailcarID}";
                    railcar = await get.GetObject<Models.Railcar>() ?? new Models.Railcar();

                    txtReportingMark.Text = railcar.ReportingMark;
                    txtReportingNumber.Text = railcar.ReportingNumber?.ToString();
                    if (ParentForm != null)
                    {
                        ParentForm.Text = railcar.FormattedReportingMark;
                    }
                    txtLessee.Text = railcar.CompanyLeasedTo?.CompanyID != null ? railcar.CompanyLeasedTo.Name : railcar.GovernmentLeasedTo?.Name;
                    txtLessee.Tag = railcar.CompanyLeasedTo?.CompanyID != null ? (object)railcar.CompanyLeasedTo : railcar.GovernmentLeasedTo?.GovernmentID != null ? railcar.GovernmentLeasedTo : null;
                    txtCurrentLocation.Text = railcar.Location;
                    lnkLocation.Tag = railcar.RailLocation.TrackID != null ? (object)railcar.RailLocation.Track : railcar.RailLocation.Train;
                    txtContents.Text = railcar.FormattedRailcarLoads;
                    OriginalTrackIDDestination = railcar.TrackIDDestination;
                    OriginalTrackIDStrategic = railcar.TrackIDStrategic;

                    bool isOwner = _application.IsCurrentEntity(railcar.CompanyIDOwner, railcar.GovernmentIDOwner);
                    bool? isLessee = railcar.CompanyLeasedTo?.CompanyID == null && railcar.GovernmentLeasedTo?.GovernmentID == null ? (bool?)null : _application.IsCurrentEntity(railcar.CompanyLeasedTo?.CompanyID, railcar.GovernmentLeasedTo?.GovernmentID);
                    bool isPossessor = _application.IsCurrentEntity(railcar.CompanyIDPossessor, railcar.GovernmentIDPossessor);
                    bool isOnManagedEntity = _application.IsCurrentEntity(railcar.RailLocation.Track?.RailDistrict?.CompanyIDOperator ?? railcar.RailLocation.Train?.TrainSymbol.CompanyIDOperator, railcar.RailLocation.Track?.RailDistrict?.GovernmentIDOperator ?? railcar.RailLocation.Train?.TrainSymbol.GovernmentIDOperator);

                    cboModel.Enabled = isOwner;
                    txtReportingMark.Enabled = isOwner;
                    txtReportingNumber.Enabled = isOwner;
                    cboOwner.Enabled = isOwner;
                    cboPossessor.Enabled = isPossessor;
                    cboDestination.Enabled = isLessee ?? isOwner;
                    cboStrategicTrack.Enabled = isOnManagedEntity;

                    cmdUpdateImage.Enabled = isOwner;
                }
                else
                {
                    txtReportingMark.Clear();
                    txtReportingNumber.Clear();
                    txtCurrentLocation.Visible = false;
                    cboCurrentLocation.Visible = true;
                }

                get.Resource = "RailcarModel/GetAll";
                List<Models.RailcarModel> railcarModels = await get.GetObject<List<Models.RailcarModel>>() ?? new List<Models.RailcarModel>();

                cboModel.Items.Clear();
                foreach (Models.RailcarModel railcarModel in railcarModels)
                {
                    RailcarModel.RailcarModelDropDownItem ddi = new RailcarModel.RailcarModelDropDownItem()
                    {
                        Application = _application,
                        RailcarModelID = railcarModel.RailcarModelID
                    };
                    Label closedControl = new Label()
                    {
                        Text = railcarModel.Name
                    };
                    closedControl.Size = TextRenderer.MeasureText(closedControl.Text, closedControl.Font);
                    ControlSelector.ControlSelectorItem dropDownItem = new ControlSelector.ControlSelectorItem(ddi, closedControl);
                    cboModel.Items.Add(dropDownItem);

                    if (railcarModel.RailcarModelID == railcar.RailcarModelID)
                    {
                        cboModel.SelectedItem = dropDownItem;
                    }
                }

                get.Resource = $"Company/GetAll";
                List<Models.Company> companies = await get.GetObject<List<Models.Company>>() ?? new List<Models.Company>();
                cboOwner.Items.Clear();
                cboPossessor.Items.Clear();
                foreach (Models.Company company in companies)
                {
                    DropDownItem<Models.Company> dropDownItem = new DropDownItem<Models.Company>(company, company.Name);
                    cboOwner.Items.Add(dropDownItem);

                    if (railcar.CompanyIDOwner != null && company.CompanyID == railcar.CompanyIDOwner)
                    {
                        cboOwner.SelectedItem = dropDownItem;
                    }

                    dropDownItem = dropDownItem.CreateCopy();
                    cboPossessor.Items.Add(dropDownItem);
                    if (railcar.CompanyIDPossessor != null && company.CompanyID == railcar.CompanyIDPossessor)
                    {
                        cboPossessor.SelectedItem = dropDownItem;
                    }
                }

                get.Resource = $"Government/GetAll";
                List<Models.Government> governments = await get.GetObject<List<Models.Government>>() ?? new List<Models.Government>();
                foreach (Models.Government government in governments)
                {
                    DropDownItem<Models.Government> dropDownItem = new DropDownItem<Models.Government>(government, government.Name);
                    cboOwner.Items.Add(dropDownItem);

                    if (railcar.GovernmentIDOwner != null && railcar.GovernmentIDOwner == government.GovernmentID)
                    {
                        cboOwner.SelectedItem = dropDownItem;
                    }

                    dropDownItem = dropDownItem.CreateCopy();
                    cboPossessor.Items.Add(dropDownItem);
                    if (railcar.GovernmentIDPossessor != null && government.GovernmentID == railcar.GovernmentIDPossessor)
                    {
                        cboPossessor.SelectedItem = dropDownItem;
                    }
                }

                get.Resource = "Track/GetAll";
                List<Models.Track> tracks = await get.GetObject<List<Models.Track>>() ?? new List<Track>();
                cboDestination.Items.Clear();
                cboStrategicTrack.Items.Clear();
                cboCurrentLocation.Items.Clear();
                foreach(Models.Track track in tracks)
                {
                    DropDownItem<Models.Track> trackItem = new DropDownItem<Track>(track, track.Name);
                    cboDestination.Items.Add(trackItem);

                    if (railcar.TrackIDDestination == track.TrackID)
                    {
                        cboDestination.SelectedItem = trackItem;
                    }

                    trackItem = trackItem.CreateCopy();
                    cboStrategicTrack.Items.Add(trackItem);

                    if (railcar.TrackIDStrategic == track.TrackID)
                    {
                        cboStrategicTrack.SelectedItem = trackItem;
                    }

                    if (cboCurrentLocation.Visible)
                    {
                        trackItem = trackItem.CreateCopy();
                        cboCurrentLocation.Items.Add(trackItem);
                    }
                }
            }
            finally
            {
                loaderGeneral.Visible = false;
            }

            if (RailcarID != null)
            {
                try
                {
                    GetData get = _application.GetAccess<GetData>();
                    get.API = DataAccess.APIs.FleetTracking;
                    get.Resource = $"Railcar/GetImage/{RailcarID}";
                    byte[] imageData = await get.GetObject<byte[]>();

                    if (imageData != null)
                    {
                        MemoryStream stream = new MemoryStream(imageData);
                        Image image = Image.FromStream(stream);
                        pboxImage.Image = image;
                    }
                }
                catch { }
            }
        }

        private async void cmdSave_Click(object sender, EventArgs e)
        {
            if (!this.AreFieldsPresent(new List<(string, Control)>()
            {
                ("Model", cboModel),
                ("Reporting Mark", txtReportingMark),
                ("Reporting Mark", txtReportingNumber),
                ("Owner", cboOwner),
                ("Possessor", cboPossessor)
            }))
            {
                return;
            }

            if (!int.TryParse(txtReportingNumber.Text, out int reportingNumber))
            {
                this.ShowError("Reporting Mark must have a valid number");
                return;
            }

            ControlSelector.ControlSelectorItem selectedModelItem = cboModel.SelectedItem as ControlSelector.ControlSelectorItem;
            long? modelID = ((RailcarModel.RailcarModelDropDownItem)selectedModelItem.DropDownControl).RailcarModelID;
            long? governmentIDOwner = null;
            long? companyIDOwner = null;
            long? governmentIDPossessor = null;
            long? companyIDPossessor = null;
            long? trackIDDestination = OriginalTrackIDDestination;
            long? trackIDStrategic = OriginalTrackIDStrategic; 
            if (cboOwner.SelectedItem is DropDownItem<Models.Government> government)
            {
                governmentIDOwner = government.Object.GovernmentID;
            }
            else if (cboOwner.SelectedItem is DropDownItem<Models.Company> company)
            {
                companyIDOwner = company.Object.CompanyID;
            }

            if (cboPossessor.SelectedItem is DropDownItem<Models.Government> governmentPossessor)
            {
                governmentIDPossessor = governmentPossessor.Object.GovernmentID;
            }
            else if (cboPossessor.SelectedItem is DropDownItem<Models.Company> company)
            {
                companyIDPossessor = company.Object.CompanyID;
            }

            if (cboDestination.Enabled)
            {
                trackIDDestination = cboDestination.SelectedItem.Cast<DropDownItem<Track>>()?.Object.TrackID;
            }

            if (cboStrategicTrack.Enabled)
            {
                trackIDStrategic = cboStrategicTrack.SelectedItem.Cast<DropDownItem<Track>>()?.Object.TrackID;
            }

            Models.Railcar railcar = new Models.Railcar()
            {
                RailcarID = this.RailcarID,
                GovernmentIDOwner = governmentIDOwner,
                CompanyIDOwner = companyIDOwner,
                GovernmentIDPossessor = governmentIDPossessor,
                CompanyIDPossessor = companyIDPossessor,
                RailcarModelID = modelID,
                ReportingMark = txtReportingMark.Text,
                ReportingNumber = reportingNumber,
                TrackIDDestination = trackIDDestination,
                TrackIDStrategic = trackIDStrategic
            };

            bool saveSuccessful;
            if (RailcarID == null)
            {
                PostData post = _application.GetAccess<PostData>();
                post.API = DataAccess.APIs.FleetTracking;
                post.Resource = "Railcar/Post";
                post.ObjectToPost = railcar;
                railcar = await post.Execute<Models.Railcar>();
                saveSuccessful = post.RequestSuccessful;
            }
            else
            {
                PutData put = _application.GetAccess<PutData>();
                put.API = DataAccess.APIs.FleetTracking;
                put.Resource = "Railcar/Put";
                put.ObjectToPut = railcar;
                await put.ExecuteNoResult();
                saveSuccessful = put.RequestSuccessful;
            }

            if (saveSuccessful && pboxImage.Image != null)
            {
                byte[] imageData;
                using (MemoryStream stream = new MemoryStream())
                {
                    pboxImage.Image.Save(stream, ImageFormat.Png);
                    stream.Position = 0;

                    imageData = new byte[stream.Length];
                    stream.Read(imageData, 0, (int)stream.Length);
                }

                PutData updateImage = _application.GetAccess<PutData>();
                updateImage.API = DataAccess.APIs.FleetTracking;
                updateImage.Resource = "Railcar/UpdateImage";
                updateImage.ObjectToPut = new { railcarID = railcar.RailcarID, image = imageData };
                await updateImage.ExecuteNoResult();
                saveSuccessful = updateImage.RequestSuccessful;
            }
            
            if (saveSuccessful && RailcarID == null && cboCurrentLocation.SelectedItem is DropDownItem<Track> currentLocation)
            {
                GetData get = _application.GetAccess<GetData>();
                get.API = DataAccess.APIs.FleetTracking;
                get.Resource = $"RailLocation/GetForRailcar/{railcar.RailcarID}";
                RailLocation location = await get.GetObject<RailLocation>();
                location.TrackID = currentLocation.Object.TrackID;
                location.Position = int.MaxValue;

                get.Resource = $"RailLocation/GetByTrack/{location.TrackID}";
                List<RailLocation> railLocations = await get.GetObject<List<RailLocation>>();
                railLocations.Add(location);

                PutData modify = _application.GetAccess<PutData>();
                modify.API = DataAccess.APIs.FleetTracking;
                modify.Resource = "RailLocation/Modify";
                modify.ObjectToPut = new
                {
                    ModifiedTracksByID = new Dictionary<long?, List<RailLocation>>()
                    {
                        { currentLocation.Object.TrackID, railLocations }
                    },
                    TimeMoved = DateTime.Now
                };
                await modify.ExecuteNoResult();
                saveSuccessful = modify.RequestSuccessful;
            }

            if (saveSuccessful)
            {
                RailcarSaved?.Invoke(this, railcar);
                RailcarID = railcar.RailcarID;

                await LoadGeneralData();
            }
        }

        private void cmdUpdateImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openImage = new OpenFileDialog();
            openImage.Filter = "Images|*.bmp;*.gif;*.jpeg;*.jpg;*.png;*.tiff";
            openImage.Title = "Select Railcar Image";
            openImage.Multiselect = false;
            if (openImage.ShowDialog() != DialogResult.OK || string.IsNullOrEmpty(openImage.FileName))
            {
                return;
            }

            Image image;
            using (FileStream stream = new FileStream(openImage.FileName, FileMode.Open))
            {
                MemoryStream memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                memoryStream.Position = 0;
                if (memoryStream.Length > 1_048_576 && !this.Confirm("The selected file is larger than 1MB, which may result in degraded performance during download.\r\n\r\nDo you want to use this file anyway?"))
                {
                    return;
                }
                image = Image.FromStream(memoryStream);
            }

            pboxImage.Image = image;
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabGeneral)
            {
                LoadGeneralData();
            }
            else if (tabControl.SelectedTab == tabHistory)
            {
                LoadHistory();
            }
            else if (tabControl.SelectedTab == tabRoute)
            {
                LoadRoute();
            }
        }

        private int historySkip = 0;
        private int historyRemaining = 0;
        private const int historyTake = 50;

        private async Task LoadHistory()
        {
            try
            {
                loaderHistory.BringToFront();
                loaderHistory.Visible = true;

                dgvHistory.Rows.Clear();

                if (RailcarID == null)
                {
                    return;
                }

                GetData get = _application.GetAccess<GetData>();
                get.API = DataAccess.APIs.FleetTracking;
                get.Resource = "RailcarLocationTransaction/GetByRailcar";
                get.QueryString.Add("railcarID", RailcarID.Value.ToString());
                get.QueryString.Add("skip", historySkip.ToString());
                get.QueryString.Add("take", historyTake.ToString());

                var response = new
                {
                    remaining = 0L,
                    railcarLocationTransactions = new List<RailcarLocationTransaction>()
                };

                response = await get.GetAnonymousObject(response);

                if (response != null)
                {
                    cmdFirst.Enabled = historySkip != 0;
                    cmdPrevious.Enabled = historySkip != 0;
                    cmdNext.Enabled = response.remaining > 0;
                    cmdLast.Enabled = response.remaining > 0;

                    historyRemaining = (int)response.remaining;

                    foreach(RailcarLocationTransaction railcarLocationTransaction in response.railcarLocationTransactions)
                    {
                        int rowIndex = dgvHistory.Rows.Add();
                        DataGridViewRow row = dgvHistory.Rows[rowIndex];

                        row.Cells[colTime.Name].Value = railcarLocationTransaction.TransactionTime?.ToString("MM/dd/yyyy HH:mm");
                        row.Cells[colTrain.Name].Value = railcarLocationTransaction.TrainNew?.TrainSymbol?.Name;
                        row.Cells[colTrack.Name].Value = railcarLocationTransaction.TrackNew?.Name;
                        row.Tag = railcarLocationTransaction;
                    }
                }
                else
                {
                    cmdFirst.Enabled = false;
                    cmdPrevious.Enabled = false;
                    cmdNext.Enabled = false;
                    cmdLast.Enabled = false;
                    historyRemaining = 0;
                }
            }
            finally
            {
                loaderHistory.Visible = false;
            }
        }

        private void cmdFirst_Click(object sender, EventArgs e)
        {
            historySkip = 0;
            LoadHistory();
        }

        private void cmdPrevious_Click(object sender, EventArgs e)
        {
            historySkip -= historyTake;
            if (historySkip < 0)
            {
                historySkip = 0;
            }

            LoadHistory();
        }

        private void cmdNext_Click(object sender, EventArgs e)
        {
            if (historyRemaining < historyTake)
            {
                historySkip += historyRemaining;
            }
            else
            {
                historySkip += historyTake;
            }

            LoadHistory();
        }

        private void cmdLast_Click(object sender, EventArgs e)
        {
            historySkip = historyRemaining - historyTake;
            if (historySkip < 0)
            {
                historySkip = 0;
            }

            LoadHistory();
        }

        private void cboCurrentLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownItem<Track> selectedTrack = cboCurrentLocation.SelectedItem as DropDownItem<Track>;
            if (selectedTrack == null)
            {
                cboStrategicTrack.Enabled = false;
                return;
            }

            Track track = selectedTrack.Object;
            cboStrategicTrack.Enabled = _application.IsCurrentEntity(track.RailDistrict.CompanyIDOperator, track.RailDistrict.GovernmentIDOperator);
        }

        private void cboOwner_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txtLessee.Tag != null)
            {
                if (txtLessee.Tag is Company lesseeCompany)
                {
                    cboDestination.Enabled = _application.IsCurrentEntity(lesseeCompany.CompanyID, null);
                }
                else if (txtLessee.Tag is Government lesseeGov)
                {
                    cboDestination.Enabled = _application.IsCurrentEntity(null, lesseeGov.GovernmentID);
                }

                return;
            }

            if (cboOwner.SelectedItem is DropDownItem<Company> selectedCompany)
            {
                cboDestination.Enabled = _application.IsCurrentEntity(selectedCompany.Object.CompanyID, null);
            }
            else if (cboOwner.SelectedItem is DropDownItem<Government> selectedGovernment)
            {
                cboDestination.Enabled = _application.IsCurrentEntity(null, selectedGovernment.Object.GovernmentID);
            }
        }

        private void lnkLocation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lnkLocation.Tag == null)
            {
                return;
            }

            if (lnkLocation.Tag is Track track)
            {
                Tracks.TrackViewer viewer = new Tracks.TrackViewer()
                {
                    Application = _application,
                    InitialTrackID = track.TrackID
                };

                _application.OpenForm(viewer);
            }

            if (lnkLocation.Tag is Models.Train train)
            {
                Train.InProgressTrainDisplay trainDisplay = new Train.InProgressTrainDisplay()
                {
                    Application = _application,
                    TrainID = train.TrainID
                };

                _application.OpenForm(trainDisplay, FleetTrackingApplication.OpenFormOptions.Popout);
            }
        }

        private void cmdReset_Click(object sender, EventArgs e)
        {
            LoadGeneralData();
        }

        private async Task LoadRoute()
        {
            try
            {
                loaderRoute.BringToFront();
                loaderRoute.Visible = true;

                dgvRoute.Rows.Clear();

                GetData get = new GetData(DataAccess.APIs.FleetTracking, "RailcarRoute/GetForRailcar/" + RailcarID);
                List<RailcarRoute> routes = await get.GetObject<List<RailcarRoute>>() ?? new List<RailcarRoute>();
                foreach(RailcarRoute route in routes)
                {
                    DataGridViewRow row = dgvRoute.Rows[dgvRoute.Rows.Add()];
                    row.Cells[colRouteSort.Name].Value = route.SortOrder?.ToString();
                    row.Cells[colRouteFrom.Name].Value = route.From;
                    row.Cells[colRouteTo.Name].Value = route.To;
                }
            }
            finally
            {
                loaderRoute.Visible = false;
            }
        }
    }
}
