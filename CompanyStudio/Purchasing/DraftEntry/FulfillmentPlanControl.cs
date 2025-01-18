using CompanyStudio.Extensions;
using CompanyStudio.Models;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using MesaSuite.Common.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyStudio.Purchasing.DraftEntry
{
    public partial class FulfillmentPlanControl : UserControl
    {
        public event EventHandler OnSave;
        public event EventHandler OnReset;

        public long? FulfillmentPlanID { get; set; }
        public long? CompanyID { get; set; }
        public long? LocationID { get; set; }
        public long? StartIDCompany { get; set; }
        public long? StartIDLocation { get; set; }
        public long? StartIDGovernment { get; set; }
        public string StartName { get; set; }
        public long? EndIDLocation { get; set; }
        public string EndName { get; set; }
        public frmStudio Studio { get; set; }

        private List<FulfillmentPlanPurchaseOrderLine> _fulfillmentPlanPurchaseOrderLines = new List<FulfillmentPlanPurchaseOrderLine>();
        private List<FulfillmentPlanRoute> _fulfillmentPlanRoutes = new List<FulfillmentPlanRoute>();

        public FulfillmentPlanControl()
        {
            InitializeComponent();
        }

        private async void FulfillmentPlanControl_Load(object sender, EventArgs e)
        {
            routeStart.LocationID = StartIDLocation;
            routeStart.EntityName = StartName;
            routeEnd.LocationID = EndIDLocation;
            routeEnd.EntityName = EndName;

            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Track/GetAll");
            List<Track> tracks = await get.GetObject<List<Track>>() ?? new List<Track>();

            foreach(Track track in tracks.OrderBy(t => t.Name))
            {
                cboPickup.Items.Add(new DropDownItem<Track>(track, track.Name));
                cboStrategicAfterPickup.Items.Add(new DropDownItem<Track>(track, track.Name));
                cboDestination.Items.Add(new DropDownItem<Track>(track, track.Name));
                cboStrategicAfterDestination.Items.Add(new DropDownItem<Track>(track, track.Name));
                cboPostFulfillment.Items.Add(new DropDownItem<Track>(track, track.Name));
            }

            if (FulfillmentPlanID != null)
            {
                get = new GetData(DataAccess.APIs.CompanyStudio, "FulfillmentPlan/Get/" + FulfillmentPlanID);
                get.AddLocationHeader(CompanyID, LocationID);
                FulfillmentPlan plan = await get.GetObject<FulfillmentPlan>() ?? new FulfillmentPlan();
                if (!IsHandleCreated)
                {
                    return;
                }
                lnkRailcar.Text = plan.Railcar?.ReportingID;
                lnkRailcar.Tag = plan.Railcar;
                lnkRailcar.Enabled = true;
                if (plan.RailcarID == null)
                {
                    lnkRailcar.Text = $"Lease Request ID: {plan.LeaseRequestID} (Bids: {plan.LeaseRequest?.LeaseBids?.Count() ?? 0})";
                    lnkRailcar.Tag = plan.LeaseRequest;
                    lnkRailcar.Enabled = true;

                    if (plan.LeaseRequestID == null)
                    {
                        lnkRailcar.Text = "None";
                        lnkRailcar.Enabled = false;
                    }
                }
                cboPickup.SelectedItem = cboPickup.Items.OfType<DropDownItem<Track>>().FirstOrDefault(t => t.Object.TrackID == plan.TrackIDLoading);
                cboStrategicAfterPickup.SelectedItem = cboStrategicAfterPickup.Items.OfType<DropDownItem<Track>>().FirstOrDefault(t => t.Object.TrackID == plan.TrackIDStrategicAfterLoad);
                cboDestination.SelectedItem = cboDestination.Items.OfType<DropDownItem<Track>>().FirstOrDefault(t => t.Object.TrackID == plan.TrackIDDestination);
                cboStrategicAfterDestination.SelectedItem = cboStrategicAfterDestination.Items.OfType<DropDownItem<Track>>().FirstOrDefault(t => t.Object.TrackID == plan.TrackIDStrategicAfterDestination);
                cboPostFulfillment.SelectedItem = cboPostFulfillment.Items.OfType<DropDownItem<Track>>().FirstOrDefault(t => t.Object.TrackID == plan.TrackIDPostFulfillment);

                _fulfillmentPlanPurchaseOrderLines = plan.FulfillmentPlanPurchaseOrderLines;
                _fulfillmentPlanRoutes = plan.FulfillmentPlanRoutes;

                foreach(DataGridViewRow row in dgvPurchaseOrderLines.Rows)
                {
                    row.Cells[colApplyPOLine.Name].Value = _fulfillmentPlanPurchaseOrderLines.FirstOrDefault(f => f.PurchaseOrderLineID == row.Tag as long?) != null;
                    row.Cells[colApplyPOLine.Name].Tag = _fulfillmentPlanPurchaseOrderLines.FirstOrDefault(f => f.PurchaseOrderLineID == row.Tag as long?)?.FulfillmentPlanPurchaseOrderLineID;
                }

                for(int i = 0; i < _fulfillmentPlanRoutes.Count - 1; i++)
                {
                    InsertRouteControl(i + 1, _fulfillmentPlanRoutes[i].CompanyIDTo);
                }
            }
        }

        public void SetPurchaseOrderLines(List<PurchaseOrderLine> purchaseOrderLines)
        {
            HashSet<long?> checkedPOLineIDs = new HashSet<long?>();
            foreach(DataGridViewRow row in dgvPurchaseOrderLines.Rows)
            {
                if (row.Cells[colApplyPOLine.Name].Value as bool? ?? false)
                {
                    checkedPOLineIDs.Add(row.Tag as long?);
                }
            }

            dgvPurchaseOrderLines.Rows.Clear();

            foreach(PurchaseOrderLine purchaseOrderLine in purchaseOrderLines)
            {
                int rowIndex = dgvPurchaseOrderLines.Rows.Add();
                DataGridViewRow row = dgvPurchaseOrderLines.Rows[rowIndex];
                if (checkedPOLineIDs.Contains(purchaseOrderLine.PurchaseOrderLineID))
                {
                    row.Cells[colApplyPOLine.Name].Value = true;
                    row.Cells[colApplyPOLine.Name].Tag = _fulfillmentPlanPurchaseOrderLines.FirstOrDefault(f => f.PurchaseOrderLineID == purchaseOrderLine.PurchaseOrderLineID)?.FulfillmentPlanPurchaseOrderLineID;
                }

                string purchaseOrderLineText = "";
                if (purchaseOrderLine.IsService)
                {
                    purchaseOrderLineText = $"{purchaseOrderLine.Quantity}x {purchaseOrderLine.ServiceDescription}";
                }
                else
                {
                    if (purchaseOrderLine.ItemID != null)
                    {
                        purchaseOrderLineText = $"{purchaseOrderLine.Quantity}x {purchaseOrderLine.Item.Name}";
                    }
                    else
                    {
                        purchaseOrderLineText = $"{purchaseOrderLine.Quantity}x {purchaseOrderLine.ItemDescription}";
                    }
                }

                row.Cells[colPOLine.Name].Value = purchaseOrderLineText;
                row.Tag = purchaseOrderLine.PurchaseOrderLineID;
            }

            dgvPurchaseOrderLines.Sort(colPOLine, ListSortDirection.Ascending);
        }

        private async void cmdSave_Click(object sender, EventArgs e)
        {
            if (!dgvPurchaseOrderLines.Rows.OfType<DataGridViewRow>().Any(dgvr => dgvr.Cells[colApplyPOLine.Name].Value as bool? ?? false))
            {
                this.ShowError("At least one Purchase Order Line must be selected");
                return;
            }

            try
            {
                loader.BringToFront();
                loader.Visible = true;

                FulfillmentPlan newPlan = new FulfillmentPlan()
                {
                    FulfillmentPlanID = FulfillmentPlanID,
                    TrackIDLoading = (cboPickup.SelectedItem as DropDownItem<Track>)?.Object.TrackID,
                    TrackIDStrategicAfterLoad = (cboStrategicAfterPickup.SelectedItem as DropDownItem<Track>)?.Object.TrackID,
                    TrackIDDestination = (cboDestination.SelectedItem as DropDownItem<Track>)?.Object.TrackID,
                    TrackIDStrategicAfterDestination = (cboStrategicAfterDestination.SelectedItem as DropDownItem<Track>)?.Object.TrackID,
                    TrackIDPostFulfillment = (cboPostFulfillment.SelectedItem as DropDownItem<Track>)?.Object.TrackID,
                    RailcarID = (lnkRailcar.Tag as Railcar)?.RailcarID,
                    LeaseRequestID = (lnkRailcar.Tag as LeaseRequest)?.LeaseRequestID
                };

                if (FulfillmentPlanID == null)
                {
                    PostData post = new PostData(DataAccess.APIs.CompanyStudio, "FulfillmentPlan/Post", newPlan);
                    post.AddLocationHeader(CompanyID, LocationID);
                    newPlan = await post.Execute<FulfillmentPlan>();
                    if (post.RequestSuccessful)
                    {
                        FulfillmentPlanID = newPlan.FulfillmentPlanID;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    PutData put = new PutData(DataAccess.APIs.CompanyStudio, "FulfillmentPlan/Put", newPlan);
                    put.AddLocationHeader(CompanyID, LocationID);
                    await put.ExecuteNoResult();
                    if (!put.RequestSuccessful)
                    {
                        return;
                    }
                }

                StringBuilder joinObjectErrors = new StringBuilder();
                List<(FulfillmentPlanPurchaseOrderLine, DataGridViewRow)> joinsToSave = new List<(FulfillmentPlanPurchaseOrderLine, DataGridViewRow)>();
                List<long?> joinsToDelete = new List<long?>();

                foreach(DataGridViewRow row in dgvPurchaseOrderLines.Rows.OfType<DataGridViewRow>().OrderByDescending(dgvr => (bool)(dgvr.Cells[colApplyPOLine.Name].Value ?? false)))
                {
                    bool isChecked = row.Cells[colApplyPOLine.Name].Value as bool? ?? false;
                    long? fulfillmentPlanPOLineID = row.Cells[colApplyPOLine.Name].Tag as long?;
                    long? purchaseOrderLineID = row.Tag as long?;

                    if (isChecked && fulfillmentPlanPOLineID == null)
                    {
                        FulfillmentPlanPurchaseOrderLine newFPPOL = new FulfillmentPlanPurchaseOrderLine();
                        newFPPOL.FulfillmentPlanID = FulfillmentPlanID;
                        newFPPOL.PurchaseOrderLineID = purchaseOrderLineID;

                        joinsToSave.Add((newFPPOL, row));
                    }
                    else if (!isChecked && fulfillmentPlanPOLineID != null)
                    {
                        joinsToDelete.Add(fulfillmentPlanPOLineID);
                    }
                }

                foreach((FulfillmentPlanPurchaseOrderLine, DataGridViewRow) joinToSave in joinsToSave)
                {
                    PostData post = new PostData(DataAccess.APIs.CompanyStudio, "FulfillmentPlanPurchaseOrderLine/Post", joinToSave.Item1);
                    post.AddLocationHeader(CompanyID, LocationID);
                    post.SuppressErrors = true;
                    FulfillmentPlanPurchaseOrderLine newFPPOL = await post.Execute<FulfillmentPlanPurchaseOrderLine>();
                    if (!post.RequestSuccessful)
                    {
                        joinObjectErrors.AppendLine("Failed to save a Purchase Order Line: " + post.LastError);
                    }
                    else
                    {
                        joinToSave.Item2.Cells[colApplyPOLine.Name].Tag = newFPPOL.FulfillmentPlanPurchaseOrderLineID;
                    }
                }

                foreach(long? joinToDelete in joinsToDelete)
                {
                    DeleteData delete = new DeleteData(DataAccess.APIs.CompanyStudio, "FulfillmentPlanPurchaseOrderLine/Delete/" + joinToDelete);
                    delete.AddLocationHeader(CompanyID, LocationID);
                    delete.SuppressErrors = true;
                    await delete.Execute();
                    if (!delete.RequestSuccessful)
                    {
                        joinObjectErrors.AppendLine("Failed to remove a Purchase Order Line: " + delete.LastError);
                    }
                }

                HashSet<FulfillmentPlanRoute> updatedRoutes = new HashSet<FulfillmentPlanRoute>();
                long? lastCompanyID = StartIDCompany;
                long? lastGovernmentID = StartIDGovernment;

                List<RouteControlEntity> routeControls = flowLayoutPanel1.Controls.OfType<RouteControlEntity>().ToList();
                for(int i = 0; i < routeControls.Count; i++)
                {
                    FulfillmentPlanRoute routeToSave = new FulfillmentPlanRoute()
                    {
                        FulfillmentPlanRouteID = _fulfillmentPlanRoutes.ElementAtOrDefault(i)?.FulfillmentPlanRouteID,
                        FulfillmentPlanID = FulfillmentPlanID,
                        CompanyIDFrom = lastCompanyID,
                        GovernmentIDFrom = lastGovernmentID,
                        CompanyIDTo = routeControls[i].SelectedCompanyID,
                        GovernmentIDTo = routeControls[i].SelectedGovernmentID,
                        SortOrder = (byte)(i + 1)
                    };

                    if (routeToSave.FulfillmentPlanRouteID == null)
                    {
                        PostData post = new PostData(DataAccess.APIs.CompanyStudio, "FulfillmentPlanRoute/Post", routeToSave);
                        post.AddLocationHeader(CompanyID, LocationID);
                        post.SuppressErrors = true;
                        routeToSave = await post.Execute<FulfillmentPlanRoute>();
                        if (!post.RequestSuccessful)
                        {
                            joinObjectErrors.AppendLine("Failed to save a Route: " + post.LastError);
                        }
                    }
                    else
                    {
                        PutData put = new PutData(DataAccess.APIs.CompanyStudio, "FulfillmentPlanRoute/Put", routeToSave);
                        put.AddLocationHeader(CompanyID, LocationID);
                        put.SuppressErrors = true;
                        await put.ExecuteNoResult();
                        if (!put.RequestSuccessful)
                        {
                            joinObjectErrors.AppendLine("Failed to save a Route: " + put.LastError);
                        }
                        else
                        {
                            updatedRoutes.Add(_fulfillmentPlanRoutes[i]);
                        }
                    }

                    lastCompanyID = routeToSave.CompanyIDTo;
                    lastGovernmentID = routeToSave.GovernmentIDTo;
                }

                FulfillmentPlanRoute lastMile = new FulfillmentPlanRoute()
                {
                    FulfillmentPlanRouteID = _fulfillmentPlanRoutes.ElementAtOrDefault(routeControls.Count)?.FulfillmentPlanRouteID,
                    FulfillmentPlanID = FulfillmentPlanID,
                    CompanyIDFrom = lastCompanyID,
                    GovernmentIDFrom = lastGovernmentID,
                    CompanyIDTo = CompanyID,
                    SortOrder = (byte)(routeControls.Count + 1)
                };
                if (lastMile.FulfillmentPlanRouteID == null)
                {
                    PostData post = new PostData(DataAccess.APIs.CompanyStudio, "FulfillmentPlanRoute/Post", lastMile);
                    post.AddLocationHeader(CompanyID, LocationID);
                    post.SuppressErrors = true;
                    lastMile = await post.Execute<FulfillmentPlanRoute>();
                    if (!post.RequestSuccessful)
                    {
                        joinObjectErrors.AppendLine("Failed to save a Route: " + post.LastError);
                    }
                }
                else
                {
                    PutData put = new PutData(DataAccess.APIs.CompanyStudio, "FulfillmentPlanRoute/Put", lastMile);
                    put.AddLocationHeader(CompanyID, LocationID);
                    put.SuppressErrors = true;
                    await put.ExecuteNoResult();
                    if (!put.RequestSuccessful)
                    {
                        joinObjectErrors.AppendLine("Failed to save a Route: " + put.LastError);
                    }
                    else
                    {
                        updatedRoutes.Add(_fulfillmentPlanRoutes.ElementAt(routeControls.Count));
                    }
                }

                foreach(FulfillmentPlanRoute route in _fulfillmentPlanRoutes.Except(updatedRoutes))
                {
                    DeleteData delete = new DeleteData(DataAccess.APIs.CompanyStudio, "FulfillmentPlanRoute/Delete/" + route.FulfillmentPlanRouteID);
                    delete.AddLocationHeader(CompanyID, LocationID);
                    delete.SuppressErrors = true;
                    await delete.Execute();
                    if (!delete.RequestSuccessful)
                    {
                        joinObjectErrors.AppendLine("Failed to remove a Route: " + delete.LastError);
                    }
                }

                if (joinObjectErrors.Length > 0)
                {
                    this.ShowError("The Fulfillment Plan saved successfully, however, the following errors occurred while saving related records:\r\n" + joinObjectErrors.ToString());
                }

                OnSave?.Invoke(this, EventArgs.Empty);
            }
            finally
            {
                loader.Visible = false;
            }
        }

        private void InsertRouteControl(int index, long? companyID = null)
        {
            RouteControlEntity routeControl = new RouteControlEntity()
            {
                SelectedCompanyID = companyID,
                ContextCompanyID = CompanyID,
                ContextLocationID = LocationID
            };
            routeControl.DeletePressed += routeControl_DeletePressed;
            routeControl.InsertPressed += routeControl_InsertPressed;
            routeControl.MovePressed += routeControl_MovePressed;

            flowLayoutPanel1.Controls.Add(routeControl);
            flowLayoutPanel1.Controls.SetChildIndex(routeControl, index);
            flowLayoutPanel1.ScrollControlIntoView(routeControl);
        }

        private void routeControl_MovePressed(object sender, RouteControlEntity.Directions e)
        {
            int currentIndex = flowLayoutPanel1.Controls.GetChildIndex(sender as RouteControlEntity);
            if ((currentIndex <= 1 && e == RouteControlEntity.Directions.Left) || (currentIndex >= flowLayoutPanel1.Controls.Count - 2 && e == RouteControlEntity.Directions.Right))
            {
                return;
            }

            switch(e)
            {
                case RouteControlEntity.Directions.Left:
                    flowLayoutPanel1.Controls.SetChildIndex(sender as RouteControlEntity, currentIndex - 1);
                    break;
                case RouteControlEntity.Directions.Right:
                    flowLayoutPanel1.Controls.SetChildIndex(sender as RouteControlEntity, currentIndex + 1);
                    break;
            }
        }

        private void routeControl_InsertPressed(object sender, RouteControlEntity.Directions e)
        {
            int currentIndex = flowLayoutPanel1.Controls.GetChildIndex(sender as RouteControlEntity);
            switch(e)
            {
                case RouteControlEntity.Directions.Left:
                    InsertRouteControl(currentIndex);
                    break;
                case RouteControlEntity.Directions.Right:
                    InsertRouteControl(currentIndex + 1);
                    break;
            }
        }

        private void routeControl_DeletePressed(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Remove(sender as RouteControlEntity);
        }

        private void routeStart_InsertPressed(object sender, EventArgs e)
        {
            InsertRouteControl(1);
        }

        private void routeEnd_InsertPressed(object sender, EventArgs e)
        {
            int routeEndIndex = flowLayoutPanel1.Controls.GetChildIndex(routeEnd);
            InsertRouteControl(routeEndIndex);
        }

        private void ctxSelectRailcar_Opening(object sender, CancelEventArgs e)
        {
            toolSelectLease.Enabled = PermissionsManager.HasPermissionFleetTrack(CompanyID ?? 0L, nameof(FleetTracking.Models.FleetSecurity.AllowLeasingManagement));
        }

        private void cmdSelectRailcar_Click(object sender, EventArgs e)
        {
            ctxSelectRailcar.Show(Cursor.Position);
        }

        private void toolSelectRailcar_Click(object sender, EventArgs e)
        {
            frmRailcarSelect railcarSelect = new frmRailcarSelect();
            Studio.DecorateStudioContent(railcarSelect);
            railcarSelect.SelectedRailcarID = (lnkRailcar.Tag as Railcar)?.RailcarID;
            railcarSelect.Company = new Company() { CompanyID = CompanyID };
            railcarSelect.LocationModel = new Location() { LocationID = LocationID, CompanyID = CompanyID, Company = railcarSelect.Company };
            railcarSelect.FormClosed += railcarSelect_FormClosed;
            railcarSelect.Show(Studio.dockPanel, new Rectangle(Screen.FromControl(this).Bounds.Width / 2 - 514, Screen.FromControl(this).Bounds.Height / 2 - 184, 1028, 368));
        }

        private async void railcarSelect_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!IsHandleCreated)
            {
                return;
            }

            frmRailcarSelect railcarSelect = sender as frmRailcarSelect;
            if (railcarSelect.DialogResult != DialogResult.OK)
            {
                return;
            }

            if (railcarSelect.SelectedRailcarID == null)
            {
                lnkRailcar.Text = "None";
                lnkRailcar.Tag = null;
                lnkRailcar.Enabled = false;
                return;
            }

            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Railcar/Get/" + railcarSelect.SelectedRailcarID);
            get.AddLocationHeader(CompanyID, LocationID);
            Railcar railcar = await get.GetObject<Railcar>();
            if (railcar == null)
            {
                lnkRailcar.Text = "None";
                lnkRailcar.Tag = null;
                lnkRailcar.Enabled = false;
                return;
            }

            lnkRailcar.Text = railcar.ReportingID;
            lnkRailcar.Tag = railcar;
            lnkRailcar.Enabled = true;
        }

        private void lnkRailcar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Railcar railcar = lnkRailcar.Tag as Railcar;
            if (railcar != null)
            {
                if (PermissionsManager.HasPermissionFleetTrack(CompanyID ?? 0L, nameof(FleetTracking.Models.FleetSecurity.AllowSetup)) ||
                    PermissionsManager.HasPermissionFleetTrack(CompanyID ?? 0L, nameof(FleetTracking.Models.FleetSecurity.IsTrainCrew)) ||
                    PermissionsManager.HasPermissionFleetTrack(CompanyID ?? 0L, nameof(FleetTracking.Models.FleetSecurity.IsYardmaster)))
                {
                    Studio.GetFleetTrackingApplication(CompanyID)?.OpenRailcarDetail(railcar.RailcarID);
                }
            }

            LeaseRequest leaseRequest = lnkRailcar.Tag as LeaseRequest;
            if (leaseRequest != null)
            {
                if (PermissionsManager.HasPermissionFleetTrack(CompanyID ?? 0L, nameof(FleetTracking.Models.FleetSecurity.AllowLeasingManagement)))
                {
                    Studio.GetFleetTrackingApplication(CompanyID)?.OpenLeaseRequestDetail(leaseRequest.LeaseRequestID);
                }
            }
        }

        private void toolSelectRemoveRailcar_Click(object sender, EventArgs e)
        {
            lnkRailcar.Text = "None";
            lnkRailcar.Tag = null;
            lnkRailcar.Enabled = false;
        }

        private async void toolSelectCreateLeaseRequest_Click(object sender, EventArgs e)
        {
            long? leaseRequestID = Studio.GetFleetTrackingApplication(CompanyID).CreateNewLease();
            if (leaseRequestID != null)
            {
                GetData get = new GetData(DataAccess.APIs.CompanyStudio, "LeaseRequest/Get/" + leaseRequestID);
                get.AddLocationHeader(CompanyID, LocationID);
                LeaseRequest leaseRequest = await get.GetObject<LeaseRequest>();
                if (leaseRequestID != null)
                {
                    lnkRailcar.Text = $"Lease Request ID: {leaseRequest.LeaseRequestID} (Bids: {leaseRequest.LeaseBids?.Count() ?? 0})";
                    lnkRailcar.Tag = leaseRequest;
                    lnkRailcar.Enabled = true;
                }
            }
        }

        private async void toolSelectExistingLeaseRequest_Click(object sender, EventArgs e)
        {
            frmLeaseRequestSelect leaseRequestSelect = new frmLeaseRequestSelect()
            {
                SelectedLeaseRequestID = (lnkRailcar.Tag as LeaseRequest)?.LeaseRequestID
            };
            Studio.DecorateStudioContent(leaseRequestSelect);
            leaseRequestSelect.Company = new Company() { CompanyID = CompanyID };
            leaseRequestSelect.LocationModel = new Location() { LocationID = LocationID, CompanyID = CompanyID, Company = leaseRequestSelect.Company };
            leaseRequestSelect.Show(Studio.dockPanel, new Rectangle(Screen.FromControl(this).Bounds.Width / 2 - 514, Screen.FromControl(this).Bounds.Height / 2 - 184, 1028, 368));
            leaseRequestSelect.FormClosed += async (s, ea) =>
            {
                if (leaseRequestSelect.SelectedLeaseRequestID != null && leaseRequestSelect.DialogResult == DialogResult.OK)
                {
                    GetData get = new GetData(DataAccess.APIs.CompanyStudio, "LeaseRequest/Get/" + leaseRequestSelect.SelectedLeaseRequestID);
                    get.AddLocationHeader(CompanyID, LocationID);
                    LeaseRequest leaseRequest = await get.GetObject<LeaseRequest>();
                    if (leaseRequest != null)
                    {
                        lnkRailcar.Text = $"Lease Request ID: {leaseRequest.LeaseRequestID} (Bids: {leaseRequest.LeaseBids?.Count() ?? 0})";
                        lnkRailcar.Tag = leaseRequest;
                        lnkRailcar.Enabled = true;
                    }
                }
            };
        }

        private void cmdReset_Click(object sender, EventArgs e)
        {
            OnReset?.Invoke(this, EventArgs.Empty);
        }
    }
}
