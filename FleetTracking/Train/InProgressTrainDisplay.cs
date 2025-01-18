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
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;

namespace FleetTracking.Train
{
    [SecuredControl(SecuredControlAttribute.Permissions.IsTrainCrew, SecuredControlAttribute.Permissions.IsYardmaster)]
    public partial class InProgressTrainDisplay : UserControl, IFleetTrackingControl
    {
        private FleetTrackingApplication _application;
        public FleetTrackingApplication Application { set => _application = value; }

        public event EventHandler OnSave;
        public long? TrainID { get; set; }

        public InProgressTrainDisplay()
        {
            InitializeComponent();
            dataGridViewStylizer.ApplyStyle(dgvConsist);
            dataGridViewStylizer.ApplyStyle(dgvDutyTrans);
            dataGridViewStylizer.ApplyStyle(dgvLocoFuel);
            dataGridViewStylizer.ApplyStyle(dgvHandledCars);
            dgvConsist.MultiSelect = true;
            dgvHandledCars.MultiSelect = true;
        }

        private async void InProgressTrainDisplay_Load(object sender, EventArgs e)
        {
            LoadEverything();

            GetData get = _application.GetAccess<GetData>();
            get.API = DataAccess.APIs.FleetTracking;
            get.Resource = $"Train/Get/{TrainID}";
            Models.Train train = await get.GetObject<Models.Train>();
            if (train != null)
            {
                ParentForm.Text = train.TrainSymbol?.Name;

                toolModifyConsist.Enabled = _application.IsCurrentEntity(train.TrainSymbol.CompanyIDOperator, train.TrainSymbol.GovernmentIDOperator);
                toolLiveLoad.Enabled = _application.IsCurrentEntity(train.TrainSymbol.CompanyIDOperator, train.TrainSymbol.GovernmentIDOperator);
                toolAddTransaction.Enabled = _application.IsCurrentEntity(train.TrainSymbol.CompanyIDOperator, train.TrainSymbol.GovernmentIDOperator);
                toolFuelAddLoco.Enabled = _application.IsCurrentEntity(train.TrainSymbol.CompanyIDOperator, train.TrainSymbol.GovernmentIDOperator);
                toolFuelSetEnd.Enabled = _application.IsCurrentEntity(train.TrainSymbol.CompanyIDOperator, train.TrainSymbol.GovernmentIDOperator);
                toolFuelSetStart.Enabled = _application.IsCurrentEntity(train.TrainSymbol.CompanyIDOperator, train.TrainSymbol.GovernmentIDOperator);
                toolTogglePartialTrip.Enabled = _application.IsCurrentEntity(train.TrainSymbol.CompanyIDOperator, train.TrainSymbol.GovernmentIDOperator);

                if (train.LiveLoad?.LiveLoadID != null)
                {
                    LiveLoadServer server = new LiveLoadServer()
                    {
                        Application = _application,
                        LiveLoadID = train.LiveLoad.LiveLoadID,
                        TrainID = train.TrainID
                    };

                    Form serverForm = _application.OpenForm(server, FleetTrackingApplication.OpenFormOptions.Popout, ParentForm);
                    serverForm.FormClosed += async (s, ea) => 
                    {
                        Models.Train liveLoadCheckTrain = await get.GetObject<Models.Train>();
                        if (liveLoadCheckTrain.LiveLoad?.LiveLoadID != null)
                        {
                            ParentForm?.Close();
                            Dispose();
                            return;
                        }
                        else
                        {
                            ParentForm.Enabled = true;
                            ParentForm.BringToFront();
                            LoadConsist();
                        }
                    };
                    ParentForm.Enabled = false;
                }
            }
        }

        private void LoadEverything()
        {
            LoadTrainInfo();
            LoadConsist();
            LoadDutyTransactions();
            LoadLocoFuel();
            LoadHandledCars();
        }

        private async void LoadTrainInfo()
        {
            try
            {
                loaderTrainInfo.BringToFront();
                loaderTrainInfo.Visible = true;

                GetData get = _application.GetAccess<GetData>();
                get.API = DataAccess.APIs.FleetTracking;
                get.Resource = $"Train/Get/{TrainID}";
                Models.Train train = await get.GetObject<Models.Train>() ?? new Models.Train();

                lnkSymbol.Text = train.TrainSymbol?.Name;
                lnkSymbol.Tag = train.TrainSymbol;
                lblStatus.Text = train.Status.ToString().ToDisplayName();
                if (train.Status == Models.Train.Statuses.NotStarted)
                {
                    lblStatus.BackColor = Color.Yellow;
                }
                TrainDutyTransaction lastetOnDutyTransaction = train?.TrainDutyTransactions?.Where(tdt => tdt.TimeOffDuty == null).FirstOrDefault();
                if (lastetOnDutyTransaction == null)
                {
                    lblOnDutySince.Text = "--/--/---- --:--";
                }
                else
                {
                    lblOnDutySince.Text = lastetOnDutyTransaction.TimeOnDuty?.ToString("MM/dd/yyyy HH:mm");
                }
                txtInstructions.Text = train.TrainInstructions;

                cmdEndTrain.Enabled = train.Status == Models.Train.Statuses.EnRoute && _application.IsCurrentEntity(train.TrainSymbol?.CompanyIDOperator, train.TrainSymbol?.GovernmentIDOperator); ;
            }
            finally
            {
                loaderTrainInfo.Visible = false;
            }
        }

        private async void LoadConsist()
        {
            try
            {
                loaderConsist.BringToFront();
                loaderConsist.Visible = true;

                GetData get = _application.GetAccess<GetData>();
                get.API = DataAccess.APIs.FleetTracking;
                get.Resource = $"RailLocation/GetByTrain/{TrainID}";
                List<RailLocation> railLocations = await get.GetObject<List<RailLocation>>() ?? new List<RailLocation>();
                railLocations = railLocations.OrderBy(rl => rl.Position).ToList();

                dgvConsist.Rows.Clear();
                foreach (RailLocation railLocation in railLocations)
                {
                    int rowIndex = dgvConsist.Rows.Add();
                    DataGridViewRow row = dgvConsist.Rows[rowIndex];

                    row.Cells[colReportingMark.Name].Value = railLocation.Locomotive?.FormattedReportingMark ?? railLocation.Railcar?.FormattedReportingMark;
                    row.Cells[colPosition.Name].Value = railLocation.Position.ToString();
                    row.Cells[colConsistType.Name].Value = railLocation.LocomotiveID == null ? "Railcar" : "Locomotive";
                    if (railLocation.RailcarID != null)
                    {
                        row.Cells[colLoad.Name].Value = railLocation.Railcar?.FormattedRailcarLoads;
                        row.Cells[colDestination.Name].Value = railLocation.Railcar?.TrackDestination?.Name;
                        row.Cells[colStrategic.Name].Value = railLocation.Railcar?.TrackStrategic?.Name;
                    }
                    row.Tag = railLocation;
                }

                lblStockTotal.Text = railLocations.Count.ToString();
                lblTotalLength.Text = railLocations.Where(rl => rl.Railcar?.RailcarModel?.Length != null || rl.Locomotive?.LocomotiveModel?.Length != null).Sum(rl => rl?.Railcar?.RailcarModel?.Length != null ? rl.Railcar.RailcarModel.Length.Value : rl.Locomotive.LocomotiveModel.Length.Value).ToString();
            }
            finally
            {
                loaderConsist.Visible = false;
            }

            try
            {
                GetData get = _application.GetAccess<GetData>();
                get.API = DataAccess.APIs.FleetTracking;

                foreach (DataGridViewRow row in dgvConsist.Rows)
                {
                    RailLocation railLocation = row.Tag as RailLocation;
                    if (railLocation == null)
                    {
                        continue;
                    }

                    byte[] imageData;
                    if (railLocation.LocomotiveID != null)
                    {
                        get.Resource = $"Locomotive/GetImageThumbnail/{railLocation.LocomotiveID}";
                        imageData = await get.GetObject<byte[]>();
                    }
                    else
                    {
                        get.Resource = $"Railcar/GetImageThumbnail/{railLocation.RailcarID}";
                        imageData = await get.GetObject<byte[]>();
                    }

                    if (imageData != null)
                    {
                        using (MemoryStream stream = new MemoryStream(imageData))
                        {
                            Image image = Image.FromStream(stream);
                            row.Cells[colConsistImage.Name].Value = image;
                        }
                    }
                }
            }
            catch { }
        }

        private async void LoadDutyTransactions()
        {
            try
            {
                loaderDutyTrans.BringToFront();
                loaderDutyTrans.Visible = true;

                GetData get = _application.GetAccess<GetData>();
                get.API = DataAccess.APIs.FleetTracking;
                get.Resource = $"TrainDutyTransaction/GetByTrain/{TrainID}";
                List<TrainDutyTransaction> dutyTransactions = await get.GetObject<List<TrainDutyTransaction>>() ?? new List<TrainDutyTransaction>();

                dgvDutyTrans.Rows.Clear();
                foreach (TrainDutyTransaction trainDutyTransaction in dutyTransactions)
                {
                    int rowIndex = dgvDutyTrans.Rows.Add();
                    DataGridViewRow row = dgvDutyTrans.Rows[rowIndex];
                    row.Cells[colOperator.Name].Value = trainDutyTransaction.UserOperator?.Username;
                    row.Cells[colTimeStart.Name].Value = trainDutyTransaction.TimeOnDuty?.ToString("MM/dd/yyyy HH:mm");
                    row.Cells[colTimeEnd.Name].Value = trainDutyTransaction.TimeOffDuty?.ToString("MM/dd/yyyy HH:mm");
                    row.Tag = trainDutyTransaction;
                }
            }
            finally
            {
                loaderDutyTrans.Visible = false;
            }
        }

        private async void LoadLocoFuel()
        {
            try
            {
                loaderLocoFuel.BringToFront();
                loaderLocoFuel.Visible = true;

                GetData get = _application.GetAccess<GetData>();
                get.API = DataAccess.APIs.FleetTracking;
                get.Resource = $"TrainFuelRecord/GetByTrain/{TrainID}";
                List<TrainFuelRecord> fuelRecords = await get.GetObject<List<TrainFuelRecord>>() ?? new List<TrainFuelRecord>();

                dgvLocoFuel.Rows.Clear();
                foreach (TrainFuelRecord fuelRecord in fuelRecords)
                {
                    int rowIndex = dgvLocoFuel.Rows.Add();
                    DataGridViewRow row = dgvLocoFuel.Rows[rowIndex];
                    row.Cells[colFuelReportingMark.Name].Value = fuelRecord.Locomotive?.FormattedReportingMark;
                    row.Cells[colStartingFuel.Name].Value = fuelRecord.FuelStart?.ToString();
                    row.Cells[colEndingFuel.Name].Value = fuelRecord.FuelEnd?.ToString();
                    row.Tag = fuelRecord;
                }
            }
            finally
            {
                loaderLocoFuel.Visible = false;
            }

            try
            {
                GetData get = _application.GetAccess<GetData>();
                get.API = DataAccess.APIs.FleetTracking;

                foreach (DataGridViewRow row in dgvLocoFuel.Rows)
                {
                    TrainFuelRecord fuelRecord = row.Tag as TrainFuelRecord;
                    if (fuelRecord == null)
                    {
                        continue;
                    }

                    get.Resource = $"Locomotive/GetImageThumbnail/{fuelRecord.LocomotiveID}";
                    byte[] imageData = await get.GetObject<byte[]>();

                    if (imageData != null)
                    {
                        using (MemoryStream stream = new MemoryStream(imageData))
                        {
                            Image image = Image.FromStream(stream);
                            row.Cells[colFuelImage.Name].Value = image;
                        }
                    }
                }
            }
            catch { }
        }

        private async void LoadHandledCars()
        {
            try
            {
                loaderHandledCars.BringToFront();
                loaderHandledCars.Visible = true;

                GetData get = _application.GetAccess<GetData>();
                get.API = DataAccess.APIs.FleetTracking;
                get.Resource = $"RailcarLocationTransaction/GetByTrain/{TrainID}";

                List<RailcarLocationTransaction> locationTransactions = await get.GetObject<List<RailcarLocationTransaction>>() ?? new List<RailcarLocationTransaction>();

                dgvHandledCars.Rows.Clear();
                foreach (RailcarLocationTransaction locationTransaction in locationTransactions)
                {
                    int rowIndex = dgvHandledCars.Rows.Add();
                    DataGridViewRow row = dgvHandledCars.Rows[rowIndex];

                    row.Cells[colHandledReportingMark.Name].Value = locationTransaction.Railcar?.FormattedReportingMark;
                    row.Cells[colPickedUp.Name].Value = locationTransaction.PreviousTransaction?.TrackNew?.Name ?? locationTransaction.PreviousTransaction?.TrainNew?.TrainSymbol?.Name;
                    row.Cells[colSetOut.Name].Value = locationTransaction.NextTransaction?.TrackNew?.Name ?? locationTransaction.NextTransaction?.TrainNew?.TrainSymbol?.Name;
                    row.Cells[colPartialTrip.Name].Value = locationTransaction.IsPartialTrainTrip;
                    row.Tag = locationTransaction;
                }
            }
            finally
            {
                loaderHandledCars.Visible = false;
            }

            try
            {
                GetData get = _application.GetAccess<GetData>();
                get.API = DataAccess.APIs.FleetTracking;

                foreach (DataGridViewRow row in dgvHandledCars.Rows)
                {
                    RailcarLocationTransaction locationTransaction = row.Tag as RailcarLocationTransaction;
                    if (locationTransaction == null)
                    {
                        continue;
                    }

                    get.Resource = $"Railcar/GetImageThumbnail/{locationTransaction.RailcarID}";
                    byte[] imageData = await get.GetObject<byte[]>();
                    if (imageData != null)
                    {
                        using (MemoryStream stream = new MemoryStream(imageData))
                        {
                            Image image = Image.FromStream(stream);
                            row.Cells[colHandledImage.Name].Value = image;
                        }
                    }
                }
            }
            catch { }
        }

        private void toolModifyConsist_Click(object sender, EventArgs e)
        {
            _application.OpenForm(new RailLocationModifier() { Application = _application, SelectedTrainID = TrainID }, FleetTrackingApplication.OpenFormOptions.Dialog);

            LoadConsist();
            LoadHandledCars();
        }

        private async void toolAddTransaction_Click(object sender, EventArgs e)
        {
            try
            {
                loaderDutyTrans.BringToFront();
                loaderDutyTrans.Visible = true;

                GetData get = _application.GetAccess<GetData>();
                get.API = DataAccess.APIs.FleetTracking;

                get.Resource = $"Train/Get/{TrainID}";
                Models.Train train = await get.GetObject<Models.Train>();

                if (train.Status == Models.Train.Statuses.NotStarted && !this.Confirm("Going on duty will put this train into 'En Route' status. Are you sure you want to go on duty?"))
                {
                    return;
                }

                TrainDutyTransactionDetail detail = new TrainDutyTransactionDetail()
                {
                    Application = _application,
                    TrainID = TrainID
                };

                Form detailForm = _application.OpenForm(detail, FleetTrackingApplication.OpenFormOptions.Popout);
                detailForm.Text = "Duty Transaction";

                detailForm.FormClosed += (s, ea) =>
                {
                    OnSave?.Invoke(this, EventArgs.Empty);

                    LoadTrainInfo();
                    LoadDutyTransactions();
                };
            }
            finally
            {
                loaderDutyTrans.Visible = false;
            }
        }

        private void dgvDutyTrans_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            TrainDutyTransaction transaction = dgvDutyTrans.Rows[e.RowIndex].Tag as TrainDutyTransaction;
            if (transaction == null)
            {
                return;
            }

            TrainDutyTransactionDetail detail = new TrainDutyTransactionDetail()
            {
                Application = _application,
                TrainID = TrainID,
                TrainDutyTransactionID = transaction.TrainDutyTransactionID
            };

            Form detailForm = _application.OpenForm(detail, FleetTrackingApplication.OpenFormOptions.Popout);
            detailForm.Text = "Duty Transaction";

            detailForm.FormClosed += (s, ea) =>
            {
                LoadTrainInfo();
                LoadDutyTransactions();
                OnSave?.Invoke(this, EventArgs.Empty);
            };
        }

        private async void toolFuelAddLoco_Click(object sender, EventArgs e)
        {
            List<TrainFuelRecord> trainFuelRecords;
            try
            {
                loaderLocoFuel.BringToFront();
                loaderLocoFuel.Visible = true;

                GetData get = _application.GetAccess<GetData>();
                get.API = DataAccess.APIs.FleetTracking;
                get.Resource = $"TrainFuelRecord/GetByTrain/{TrainID}";
                trainFuelRecords = await get.GetObject<List<TrainFuelRecord>>() ?? new List<TrainFuelRecord>();
            }
            finally
            {
                loaderLocoFuel.Visible = false;
            }
            SelectLocoForFuel select = new SelectLocoForFuel()
            {
                Application = _application,
                LocomotiveFilter = (l) => !trainFuelRecords.Any(tfr => tfr.FuelEnd == null && tfr.LocomotiveID == l.LocomotiveID)
            };
            Form selectForm = _application.OpenForm(select, FleetTrackingApplication.OpenFormOptions.Dialog);
            if (selectForm.DialogResult == DialogResult.OK)
            {
                try
                {
                    loaderLocoFuel.BringToFront();
                    loaderLocoFuel.Visible = true;

                    PostData post = _application.GetAccess<PostData>();
                    post.API = DataAccess.APIs.FleetTracking;
                    post.Resource = "TrainFuelRecord/Post";

                    foreach (Locomotive selectedLocomotive in select.SelectedLocomotives)
                    {
                        TrainFuelRecord fuelRecord = new TrainFuelRecord()
                        {
                            TrainID = TrainID,
                            LocomotiveID = selectedLocomotive.LocomotiveID
                        };

                        post.ObjectToPost = fuelRecord;
                        await post.ExecuteNoResult();
                    }
                }
                finally
                {
                    loaderLocoFuel.Visible = false;
                }
            }

            LoadLocoFuel();
        }

        private async void toolFuelSetStart_Click(object sender, EventArgs e)
        {
            TrainFuelRecord currentFuelRecord = dgvLocoFuel.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault()?.Tag as TrainFuelRecord;
            if (currentFuelRecord == null)
            {
                return;
            }

            InputBox input = new InputBox()
            {
                Application = _application,
                Text = "Set Start Fuel",
                InputValueType = typeof(decimal)
            };
            input.cmdOK.Text = "Save";
            input.lblPrompt.Text = "Enter starting fuel for this locomotive:";

            Form inputForm = _application.OpenForm(input, FleetTrackingApplication.OpenFormOptions.Dialog);
            if (inputForm.DialogResult != DialogResult.OK)
            {
                return;
            }

            try
            {
                loaderLocoFuel.BringToFront();
                loaderLocoFuel.Visible = true;

                PatchData setFuel = _application.GetAccess<PatchData>();
                setFuel.API = DataAccess.APIs.FleetTracking;
                setFuel.PatchMethod = PatchData.PatchMethods.Replace;
                setFuel.PrimaryKey = currentFuelRecord.TrainFuelRecordID;
                setFuel.Resource = "TrainFuelRecord/Patch";
                setFuel.Values = new Dictionary<string, object>() { { nameof(TrainFuelRecord.FuelStart), input.InputValue } };
                await setFuel.Execute();

                if (setFuel.RequestSuccessful)
                {
                    LoadLocoFuel();
                }
            }
            finally
            {
                loaderLocoFuel.Visible = false;
            }
        }

        private async void toolFuelSetEnd_Click(object sender, EventArgs e)
        {
            TrainFuelRecord currentFuelRecord = dgvLocoFuel.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault()?.Tag as TrainFuelRecord;
            if (currentFuelRecord == null)
            {
                return;
            }

            InputBox input = new InputBox()
            {
                Application = _application,
                Text = "Set End Fuel",
                InputValueType = typeof(decimal)
            };
            input.cmdOK.Text = "Save";
            input.lblPrompt.Text = "Enter ending fuel for this locomotive:";

            Form inputForm = _application.OpenForm(input, FleetTrackingApplication.OpenFormOptions.Dialog);
            if (inputForm.DialogResult != DialogResult.OK)
            {
                return;
            }

            try
            {
                loaderLocoFuel.BringToFront();
                loaderLocoFuel.Visible = true;

                PatchData setFuel = _application.GetAccess<PatchData>();
                setFuel.API = DataAccess.APIs.FleetTracking;
                setFuel.PatchMethod = PatchData.PatchMethods.Replace;
                setFuel.PrimaryKey = currentFuelRecord.TrainFuelRecordID;
                setFuel.Resource = "TrainFuelRecord/Patch";
                setFuel.Values = new Dictionary<string, object>() { { nameof(TrainFuelRecord.FuelEnd), input.InputValue } };
                await setFuel.Execute();

                if (setFuel.RequestSuccessful)
                {
                    LoadLocoFuel();
                }
            }
            finally
            {
                loaderLocoFuel.Visible = false;
            }
        }

        private async void toolTogglePartialTrip_Click(object sender, EventArgs e)
        {
            try
            {
                loaderHandledCars.BringToFront();
                loaderHandledCars.Visible = true;

                PatchData patch = _application.GetAccess<PatchData>();
                patch.Resource = "RailcarLocationTransaction/Patch";
                patch.API = DataAccess.APIs.FleetTracking;

                foreach (DataGridViewRow row in dgvHandledCars.SelectedRows)
                {
                    RailcarLocationTransaction transaction = row.Tag as RailcarLocationTransaction;
                    if (transaction == null)
                    {
                        continue;
                    }

                    patch.Values = new Dictionary<string, object>()
                    {
                        { nameof(RailcarLocationTransaction.IsPartialTrainTrip), !transaction.IsPartialTrainTrip }
                    };
                    patch.PrimaryKey = transaction.RailcarLocationTransactionID;
                    await patch.Execute();
                }
            }
            finally
            {
                loaderHandledCars.Visible = false;
            }

            LoadHandledCars();
        }

        private async void cmdEndTrain_Click(object sender, EventArgs e)
        {
            try
            {
                loaderTrainInfo.BringToFront();
                loaderConsist.BringToFront();
                loaderDutyTrans.BringToFront();
                loaderLocoFuel.BringToFront();
                loaderHandledCars.BringToFront();
                loaderTrainInfo.Visible = true;
                loaderConsist.Visible = true;
                loaderDutyTrans.Visible = true;
                loaderLocoFuel.Visible = true;
                loaderHandledCars.Visible = true;

                PatchData patch = _application.GetAccess<PatchData>();
                patch.API = DataAccess.APIs.FleetTracking;
                patch.Resource = "Train/Patch";
                patch.PatchMethod = PatchData.PatchMethods.Replace;
                patch.PrimaryKey = TrainID;
                patch.Values = new Dictionary<string, object>()
                {
                    { nameof(Models.Train.Status), Models.Train.Statuses.Complete.ToString() }
                };
                await patch.Execute();

                if (!patch.RequestSuccessful)
                {
                    return;
                }

                GetData get = _application.GetAccess<GetData>();
                get.API = DataAccess.APIs.FleetTracking;
                get.Resource = $"Train/Get/{TrainID}";
                Models.Train train = await get.GetObject<Models.Train>();
                if (train.RailLocations.Any() && this.Confirm("Do you want to move this train's stock to a different location?"))
                {
                    RailLocationModifier modifier = new RailLocationModifier() { Application = _application, SelectedTrainID = TrainID };
                    Form modifierForm = _application.OpenForm(modifier, FleetTrackingApplication.OpenFormOptions.Dialog);
                    if (modifierForm.DialogResult != DialogResult.OK)
                    {
                        return;
                    }
                }
            }
            finally
            {
                loaderTrainInfo.Visible = false;
                loaderConsist.Visible = false;
                loaderDutyTrans.Visible = false;
                loaderLocoFuel.Visible = false;
                loaderHandledCars.Visible = false;

                LoadEverything();
                OnSave?.Invoke(this, EventArgs.Empty);
            }
        }

        private void toolLiveLoad_Click(object sender, EventArgs e)
        {
            LiveLoadServer liveLoadServer = new LiveLoadServer()
            {
                Application = _application,
                TrainID = TrainID
            };

            Form serverForm = _application.OpenForm(liveLoadServer, FleetTrackingApplication.OpenFormOptions.Popout, ParentForm);
            serverForm.FormClosed += async (s, ea) =>
            {
                try
                {
                    loaderConsist.BringToFront();
                    loaderConsist.Visible = true;

                    GetData get = _application.GetAccess<GetData>();
                    get.API = DataAccess.APIs.FleetTracking;
                    get.Resource = $"Train/Get/{TrainID}";
                    Models.Train trainForLoadCheck = await get.GetObject<Models.Train>();

                    if (trainForLoadCheck?.LiveLoad?.LiveLoadID != null)
                    {
                        ParentForm?.Close();
                        Dispose();
                        return;
                    }
                    else
                    {
                        ParentForm.Enabled = true;
                        ParentForm.BringToFront();
                        LoadConsist();
                    }
                }
                finally
                {
                    loaderConsist.Visible = false;
                }
            };
            ParentForm.Enabled = false;
        }

        private void toolPrintManifest_Click(object sender, EventArgs e)
        {
            PrintableReport report = new PrintableReport()
            {
                Application = _application,
                ReportContext = new Reports.TrainManifest.TrainManifestReportContext(_application, new[] { TrainID })
            };
            _application.OpenForm(report);
        }

        private void lnkSymbol_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lnkSymbol.Tag is TrainSymbol symbol && (_application.SecurityPermissionCheck(nameof(FleetSecurity.AllowSetup)) || _application.SecurityPermissionCheck(nameof(FleetSecurity.IsYardmaster))))
            {
                TrainSymbols.TrainSymbolDetail detail = new TrainSymbols.TrainSymbolDetail()
                {
                    Application = _application,
                    TrainSymbolID = symbol.TrainSymbolID
                };
                _application.OpenForm(detail);
            }
        }

        private void dgvConsist_SelectionChanged(object sender, EventArgs e)
        {
            toolSpotAtStrategic.Enabled = dgvConsist.SelectedRows.OfType<DataGridViewRow>().Where(dgvr => dgvr.Tag is RailLocation railLocation && railLocation.RailcarID != null).Any();
        }

        private async void toolSpotAtStrategic_Click(object sender, EventArgs e)
        {
            try
            {
                loaderConsist.BringToFront();
                loaderConsist.Visible = true;

                // Values to populate that will be sent to api
                Dictionary<long?, List<RailLocation>> modifiedTracksByID = new Dictionary<long?, List<RailLocation>>();
                Dictionary<long?, List<RailLocation>> modifiedTrainsByID = new Dictionary<long?, List<RailLocation>>();

                // Identify which cars will be updated and which tracks they're going to
                HashSet<long?> trackIDsToGet = new HashSet<long?>();
                List<RailLocation> railLocationsToChange = new List<RailLocation>();
                foreach(DataGridViewRow row in dgvConsist.SelectedRows.OfType<DataGridViewRow>().Where(dgvr => dgvr.Tag is RailLocation railLocation && railLocation.RailcarID != null))
                {
                    RailLocation railLocation = row.Tag as RailLocation;
                    railLocationsToChange.Add(railLocation);
                    trackIDsToGet.Add(railLocation.Railcar?.TrackIDStrategic);
                }

                foreach(long? trackID in trackIDsToGet)
                {
                    if (trackID == null)
                    {
                        continue;
                    }

                    GetData getByTrack = new GetData(DataAccess.APIs.FleetTracking, "RailLocation/GetByTrack/" + trackID);
                    List<RailLocation> railLocationsOnTrack = await getByTrack.GetObject<List<RailLocation>>() ?? new List<RailLocation>();
                    modifiedTracksByID.Add(trackID, railLocationsOnTrack);
                }

                GetData get = new GetData(DataAccess.APIs.FleetTracking, "RailLocation/GetByTrain/" + TrainID);
                List<RailLocation> railLocations = await get.GetObject<List<RailLocation>>() ?? new List<RailLocation>();
                modifiedTrainsByID.Add(TrainID, railLocations);

                // Update rail locations and add/remove them to the appropriate places
                foreach (RailLocation readOnlyRailLocation in railLocationsToChange)
                {
                    RailLocation railLocationToChange = modifiedTrainsByID[TrainID].Single(rl => rl.RailcarID == readOnlyRailLocation.RailcarID);

                    railLocationToChange.TrackID = readOnlyRailLocation.Railcar?.TrackIDStrategic;
                    railLocationToChange.TrainID = null;

                    modifiedTracksByID[readOnlyRailLocation.Railcar?.TrackIDStrategic].Add(railLocationToChange);
                    railLocationToChange.Position = modifiedTracksByID[readOnlyRailLocation.Railcar?.TrackIDStrategic].Count;
                    modifiedTrainsByID[TrainID].Remove(railLocationToChange);
                }

                for(int i = 0; i < modifiedTrainsByID[TrainID].Count; i++)
                {
                    modifiedTrainsByID[TrainID][i].Position = i + 1;
                }

                // Update via api
                PutData put = new PutData(DataAccess.APIs.FleetTracking, "RailLocation/Modify", new
                {
                    modifiedTracksByID,
                    modifiedTrainsByID,
                    TimeMoved = DateTime.Now
                });
                await put.ExecuteNoResult();

                // Release the cars
                if (put.RequestSuccessful)
                {
                    foreach(RailLocation readOnlyRailLocation in railLocationsToChange)
                    {
                        get = new GetData(DataAccess.APIs.FleetTracking, "Track/Get/" + readOnlyRailLocation.Railcar?.TrackIDStrategic);
                        Track track = await get.GetObject<Track>();
                        if (track != null)
                        {
                            PatchData patch = new PatchData(DataAccess.APIs.FleetTracking, "Railcar/Patch", PatchData.PatchMethods.Replace, readOnlyRailLocation.RailcarID, new Dictionary<string, object>()
                            {
                                { nameof(Railcar.CompanyIDPossessor), track.CompanyIDOwner },
                                { nameof(Railcar.GovernmentIDPossessor), track.GovernmentIDOwner }
                            });
                            await patch.Execute();
                        }
                    }
                }
            }
            catch
            {
                loaderConsist.Visible = false;
            }

            LoadConsist();
            LoadHandledCars();
        }

        private void tsbPrintBOLs_Click(object sender, EventArgs e)
        {
            Tracks.BOLRailcarPicker railcarPicker = new Tracks.BOLRailcarPicker()
            {
                TrainID = TrainID,
                Application = _application
            };
            _application.OpenForm(railcarPicker, FleetTrackingApplication.OpenFormOptions.Dialog);
        }
    }
}
