using GovernmentPortal.Extensions;
using GovernmentPortal.Models;
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

namespace GovernmentPortal.Purchasing.PurchaseOrderScreen.Draft
{
    public partial class frmDraftFulfillmentPlan : Form
    {
        private Dictionary<int, RouteModel> routesByHash = new Dictionary<int, RouteModel>();

        public long? FulfillmentPlanID { get; set; }
        public long? GovernmentIDOrigin { get; set; }
        public long? CompanyIDDestination { get; set; }
        public long? GovernmentIDDestination { get; set; }
        public long? PurchaseOrderLineIDFor { get; set; }
        public frmPortal Shell { get; set; }

        public frmDraftFulfillmentPlan()
        {
            InitializeComponent();
            colRoute.ValueType = typeof(int);
            colRoute.DisplayMember = nameof(RouteModel.Display);
            colRoute.ValueMember = nameof(RouteModel.HashCode);
        }

        private async void frmDraftFulfillmentPlan_Load(object sender, EventArgs e)
        {
            GetData get = new GetData(DataAccess.APIs.FleetTracking, "Track/GetAll");
            List<FleetTracking.Models.Track> tracks = await get.GetObject<List<FleetTracking.Models.Track>>() ?? new List<FleetTracking.Models.Track>();

            foreach (FleetTracking.Models.Track track in tracks.OrderBy(t => t.Name))
            {
                DropDownItem<FleetTracking.Models.Track> ddi = new DropDownItem<FleetTracking.Models.Track>(track, track.Name);
                cboPickup.Items.Add(ddi);
                cboStrategicAfterPickup.Items.Add(ddi);
                cboDestination.Items.Add(ddi);
                cboStrategicAfterDestination.Items.Add(ddi);
                cboPostFulfillment.Items.Add(ddi);
            }

            List<RouteModel> routeModels = new List<RouteModel>();

            get.API = DataAccess.APIs.GovernmentPortal;
            get.Resource = "Government/GetAll";
            List<Government> allGovernments = await get.GetObject<List<Government>>() ?? new List<Government>();
            routeModels.AddRange(allGovernments.Select(g => new RouteModel() { Government = g }));

            get.Resource = "Company/GetAll";
            List<Company> allCompanies = await get.GetObject<List<Company>>() ?? new List<Company>();
            routeModels.AddRange(allCompanies.Select(c => new RouteModel() { Company = c }));

            routeModels = routeModels.OrderBy(rm => rm.Display).ToList();
            colRoute.DataSource = routeModels;
            routesByHash = routeModels.ToDictionary(rm => rm.HashCode);

            cmdInsertRoute.Enabled = true;

            await ReloadData();
        }

        private async Task ReloadData()
        {
            dgvRoute.Rows.Clear();

            // Load default route data
            List<RouteModel> routeModels = routesByHash.Values.ToList();
            DataGridViewRow startRow = dgvRoute.Rows[dgvRoute.Rows.Add()];
            startRow.Cells[colRoute.Name].Value = routeModels.FirstOrDefault(rm => rm.Government?.GovernmentID == GovernmentIDDestination && rm.Company?.CompanyID == CompanyIDDestination)?.HashCode ?? 0;
            startRow.ReadOnly = true;

            DataGridViewRow endRow = dgvRoute.Rows[dgvRoute.Rows.Add()];
            endRow.Cells[colRoute.Name].Value = routeModels.FirstOrDefault(rm => rm.Government?.GovernmentID == GovernmentIDOrigin)?.HashCode ?? 0;
            endRow.ReadOnly = true;

            if (FulfillmentPlanID != null)
            {
                GetData get = new GetData(DataAccess.APIs.GovernmentPortal, "FulfillmentPlan/Get/" + FulfillmentPlanID);
                get.AddGovHeader(GovernmentIDOrigin.Value);
                FulfillmentPlan fulfillmentPlan = await get.GetObject<FulfillmentPlan>();

                if (fulfillmentPlan.RailcarID != null)
                {
                    lnkRailcar.Text = fulfillmentPlan.Railcar.FormattedReportingMark;
                    lnkRailcar.Tag = fulfillmentPlan.Railcar;
                    lnkRailcar.Enabled = true;
                }
                else if (fulfillmentPlan.LeaseRequestID != null)
                {
                    lnkRailcar.Text = $"Lease Request {fulfillmentPlan.LeaseRequestID} ({fulfillmentPlan.LeaseRequest?.LeaseBids?.Count ?? 0} Bids)";
                    lnkRailcar.Tag = fulfillmentPlan.LeaseRequest;
                    lnkRailcar.Enabled = true;
                }
                else
                {
                    lnkRailcar.Text = "[None]";
                    lnkRailcar.Tag = null;
                    lnkRailcar.Enabled = false;
                }

                List<string> purchaseOrderLines = new List<string>();
                foreach (FulfillmentPlanPurchaseOrderLine line in fulfillmentPlan.FulfillmentPlanPurchaseOrderLines ?? new List<FulfillmentPlanPurchaseOrderLine>())
                {
                    purchaseOrderLines.Add(line.PurchaseOrderLine.DisplayString);
                }

                txtPurchaseOrderLines.Text = string.Join(", ", purchaseOrderLines);

                cboPickup.SelectedItem = cboPickup.Items.OfType<DropDownItem<FleetTracking.Models.Track>>().FirstOrDefault(t => t.Object.TrackID == fulfillmentPlan.TrackIDLoading);
                cboStrategicAfterPickup.SelectedItem = cboStrategicAfterPickup.Items.OfType<DropDownItem<FleetTracking.Models.Track>>().FirstOrDefault(t => t.Object.TrackID == fulfillmentPlan.TrackIDStrategicAfterLoad);
                cboDestination.SelectedItem = cboDestination.Items.OfType<DropDownItem<FleetTracking.Models.Track>>().FirstOrDefault(t => t.Object.TrackID == fulfillmentPlan.TrackIDDestination);
                cboStrategicAfterDestination.SelectedItem = cboStrategicAfterDestination.Items.OfType<DropDownItem<FleetTracking.Models.Track>>().FirstOrDefault(t => t.Object.TrackID == fulfillmentPlan.TrackIDStrategicAfterDestination);
                cboPostFulfillment.SelectedItem = cboPostFulfillment.Items.OfType<DropDownItem<FleetTracking.Models.Track>>().FirstOrDefault(t => t.Object.TrackID == fulfillmentPlan.TrackIDPostFulfillment);

                foreach (FulfillmentPlanRoute route in (fulfillmentPlan.FulfillmentPlanRoutes?.Take(fulfillmentPlan.FulfillmentPlanRoutes.Count - 1) ?? new List<FulfillmentPlanRoute>()).OrderBy(fpr => fpr.SortOrder))
                {
                    dgvRoute.Rows.Insert(dgvRoute.Rows.Count - 1, 1);
                    DataGridViewRow row = dgvRoute.Rows[dgvRoute.Rows.Count - 2];
                    RouteModel routeModel = routesByHash.Values.FirstOrDefault(rm => rm.Government?.GovernmentID == route.GovernmentIDTo && rm.Company?.CompanyID == route.CompanyIDTo);
                    if (routeModel != null)
                    {
                        row.Cells[colRoute.Name].Value = routeModel.HashCode;
                    }
                }
            }
        }

        private class RouteModel
        {
            public Company Company { get; set; }
            public Government Government { get; set; }

            public string Display => string.IsNullOrEmpty(Company?.Name) ? Government?.Name : Company.Name;

            public int HashCode => GetHashCode();
        }

        private void dgvRoute_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.Handled = true;

            if (e.RowIndex < 0 || (e.ColumnIndex != colUp.Index && e.ColumnIndex != colDown.Index && e.ColumnIndex != colDelete.Index))
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                return;
            }

            e.Paint(e.CellBounds, DataGridViewPaintParts.All ^ DataGridViewPaintParts.ContentBackground ^ DataGridViewPaintParts.SelectionBackground);

            if (e.RowIndex == 0 || e.RowIndex >= dgvRoute.Rows.Count - 1 || (e.RowIndex == dgvRoute.Rows.Count - 2 && e.ColumnIndex == colDown.Index) || (e.RowIndex == 1 && e.ColumnIndex == colUp.Index))
            {
                e.Graphics.FillRectangle(SystemBrushes.GrayText, e.CellBounds);
                return;
            }

            Image image;
            if (e.ColumnIndex == colUp.Index)
            {
                image = Properties.Resources.arrow_up;
            }
            else if (e.ColumnIndex == colDown.Index)
            {
                image = Properties.Resources.arrow_down;
            }
            else if (e.ColumnIndex == colDelete.Index)
            {
                image = Properties.Resources.delete;
            }
            else
            {
                return;
            }

            e.Graphics.DrawImage(image, e.CellBounds);
        }

        private void cmdInsertRoute_Click(object sender, EventArgs e)
        {
            dgvRoute.Rows.Insert(dgvRoute.Rows.Count - 1, 1);
        }

        private void dgvRoute_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex <= 0 || e.RowIndex >= dgvRoute.Rows.Count - 1 || (e.ColumnIndex != colUp.Index && e.ColumnIndex != colDown.Index && e.ColumnIndex != colDelete.Index))
            {
                return;
            }

            DataGridViewRow row = dgvRoute.Rows[e.RowIndex];
            if (e.ColumnIndex == colUp.Index && e.RowIndex > 1)
            {
                dgvRoute.Rows.Remove(row);
                dgvRoute.Rows.Insert(e.RowIndex - 1, row);
            }
            else if (e.ColumnIndex == colDown.Index && e.RowIndex < dgvRoute.Rows.Count - 2)
            {
                dgvRoute.Rows.Remove(row);
                dgvRoute.Rows.Insert(e.RowIndex + 1, row);
            }
            else if (e.ColumnIndex == colDelete.Index)
            {
                dgvRoute.Rows.Remove(row);
            }
        }

        private async void cmdSave_Click(object sender, EventArgs e)
        {
            if (dgvRoute.Rows.OfType<DataGridViewRow>().Any(dgvr => dgvr.Cells[colRoute.Name].Value == null))
            {
                this.ShowError("All routes must have a value.");
                return;
            }

            FulfillmentPlan fulfillmentPlan;
            if (FulfillmentPlanID == null)
            {
                fulfillmentPlan = new FulfillmentPlan()
                {
                    RailcarID = (lnkRailcar.Tag as FleetTracking.Models.Railcar)?.RailcarID,
                    LeaseRequestID = (lnkRailcar.Tag as FleetTracking.Models.LeaseRequest)?.LeaseRequestID,
                    TrackIDLoading = (cboPickup.SelectedItem as DropDownItem<FleetTracking.Models.Track>)?.Object.TrackID,
                    TrackIDStrategicAfterLoad = (cboStrategicAfterPickup.SelectedItem as DropDownItem<FleetTracking.Models.Track>)?.Object.TrackID,
                    TrackIDDestination = (cboDestination.SelectedItem as DropDownItem<FleetTracking.Models.Track>)?.Object.TrackID,
                    TrackIDStrategicAfterDestination = (cboStrategicAfterDestination.SelectedItem as DropDownItem<FleetTracking.Models.Track>)?.Object.TrackID,
                    TrackIDPostFulfillment = (cboPostFulfillment.SelectedItem as DropDownItem<FleetTracking.Models.Track>)?.Object.TrackID
                };

                PostData post = new PostData(DataAccess.APIs.GovernmentPortal, "FulfillmentPlan/Post", fulfillmentPlan);
                post.AddGovHeader(GovernmentIDOrigin.Value);
                fulfillmentPlan = await post.Execute<FulfillmentPlan>();
                if (!post.RequestSuccessful)
                {
                    return;
                }

                FulfillmentPlanPurchaseOrderLine fulfillmentPlanPurchaseOrderLine = new FulfillmentPlanPurchaseOrderLine()
                {
                    FulfillmentPlanID = fulfillmentPlan.FulfillmentPlanID,
                    PurchaseOrderLineID = PurchaseOrderLineIDFor
                };
                post.Resource = "FulfillmentPlanPurchaseOrderLine/Post";
                post.ObjectToPost = fulfillmentPlanPurchaseOrderLine;
                await post.ExecuteNoResult();
            }
            else
            {
                PatchData patch = new PatchData(DataAccess.APIs.GovernmentPortal, "FulfillmentPlan/Patch", PatchData.PatchMethods.Replace, FulfillmentPlanID, new Dictionary<string, object>()
                {
                    { nameof(FulfillmentPlan.RailcarID), (lnkRailcar.Tag as FleetTracking.Models.Railcar)?.RailcarID },
                    { nameof(FulfillmentPlan.LeaseRequestID), (lnkRailcar.Tag as FleetTracking.Models.LeaseRequest)?.LeaseRequestID },
                    { nameof(FulfillmentPlan.TrackIDLoading), (cboPickup.SelectedItem as DropDownItem<FleetTracking.Models.Track>)?.Object.TrackID },
                    { nameof(FulfillmentPlan.TrackIDStrategicAfterLoad), (cboStrategicAfterPickup.SelectedItem as DropDownItem<FleetTracking.Models.Track>)?.Object.TrackID },
                    { nameof(FulfillmentPlan.TrackIDDestination), (cboDestination.SelectedItem as DropDownItem<FleetTracking.Models.Track>)?.Object.TrackID },
                    { nameof(FulfillmentPlan.TrackIDStrategicAfterDestination), (cboStrategicAfterDestination.SelectedItem as DropDownItem<FleetTracking.Models.Track>)?.Object.TrackID },
                    { nameof(FulfillmentPlan.TrackIDPostFulfillment), (cboPostFulfillment.SelectedItem as DropDownItem<FleetTracking.Models.Track>)?.Object.TrackID }
                });
                patch.AddGovHeader(GovernmentIDOrigin.Value);
                await patch.Execute();
                if (!patch.RequestSuccessful)
                {
                    return;
                }

                GetData get = new GetData(DataAccess.APIs.GovernmentPortal, "FulfillmentPlan/Get/" + FulfillmentPlanID);
                get.AddGovHeader(GovernmentIDOrigin.Value);
                fulfillmentPlan = await get.GetObject<FulfillmentPlan>();
            }

            // Get what we expect the saved routes to be
            long? previousCompanyID = CompanyIDDestination;
            long? previousGovernmentID = GovernmentIDDestination;
            List<FulfillmentPlanRoute> expectedRoutes = new List<FulfillmentPlanRoute>()
            {
                new FulfillmentPlanRoute()
                {
                    FulfillmentPlanID = fulfillmentPlan.FulfillmentPlanID,
                    CompanyIDFrom = previousCompanyID,
                    GovernmentIDFrom = previousGovernmentID,
                    SortOrder = 1
                }
            };

            for(int i = 1; i < dgvRoute.Rows.Count - 1; i++)
            {
                RouteModel routeModel = routesByHash[(int)dgvRoute[colRoute.Name, i].Value];

                FulfillmentPlanRoute previousRoute = expectedRoutes.Last();
                previousRoute.GovernmentIDTo = routeModel.Government?.GovernmentID;
                previousRoute.CompanyIDTo = routeModel.Company?.CompanyID;

                expectedRoutes.Add(new FulfillmentPlanRoute()
                {
                    FulfillmentPlanID = fulfillmentPlan.FulfillmentPlanID,
                    CompanyIDFrom = previousRoute.CompanyIDTo,
                    GovernmentIDFrom = previousRoute.GovernmentIDTo,
                    SortOrder = (byte)(i + 1)
                });

                previousCompanyID = routeModel.Company?.CompanyID;
                previousGovernmentID = routeModel.Government?.GovernmentID;
            }

            expectedRoutes.Last().GovernmentIDTo = GovernmentIDOrigin;
            expectedRoutes.Last().CompanyIDTo = null;

            // Compare what we expect to what's saved, and make changes as necessary
            List<FulfillmentPlanRoute> routesToDelete = new List<FulfillmentPlanRoute>();
            List<FulfillmentPlanRoute> routesToSave = new List<FulfillmentPlanRoute>();
            if (fulfillmentPlan.FulfillmentPlanRoutes != null && fulfillmentPlan.FulfillmentPlanRoutes.Count > expectedRoutes.Count)
            {
                routesToDelete.AddRange(fulfillmentPlan.FulfillmentPlanRoutes.Skip(expectedRoutes.Count));
            }

            for (int i = 0; expectedRoutes.Any(); i++)
            {
                FulfillmentPlanRoute savedRoute = fulfillmentPlan.FulfillmentPlanRoutes?.ElementAtOrDefault(i);
                FulfillmentPlanRoute expectedRoute = expectedRoutes.First();

                if (savedRoute == null)
                {
                    routesToSave.Add(expectedRoute);
                }
                else if (savedRoute.GovernmentIDFrom != expectedRoute.GovernmentIDFrom || savedRoute.CompanyIDFrom != expectedRoute.CompanyIDFrom ||
                            savedRoute.GovernmentIDTo != expectedRoute.GovernmentIDTo || savedRoute.CompanyIDTo != expectedRoute.CompanyIDTo ||
                            savedRoute.SortOrder != expectedRoute.SortOrder)
                {
                    savedRoute.GovernmentIDFrom = expectedRoute.GovernmentIDFrom;
                    savedRoute.CompanyIDFrom = expectedRoute.CompanyIDFrom;
                    savedRoute.GovernmentIDTo = expectedRoute.GovernmentIDTo;
                    savedRoute.CompanyIDTo = expectedRoute.CompanyIDTo;
                    savedRoute.SortOrder = expectedRoute.SortOrder;

                    routesToSave.Add(savedRoute);
                }

                expectedRoutes.RemoveAt(0);
            }

            // Do api calls
            foreach(FulfillmentPlanRoute route in routesToDelete)
            {
                DeleteData delete = new DeleteData(DataAccess.APIs.GovernmentPortal, "FulfillmentPlanRoute/Delete/" + route.FulfillmentPlanRouteID);
                delete.AddGovHeader(GovernmentIDOrigin.Value);
                await delete.Execute();
            }

            foreach(FulfillmentPlanRoute route in routesToSave)
            {
                if (route.FulfillmentPlanRouteID == null)
                {
                    PostData post = new PostData(DataAccess.APIs.GovernmentPortal, "FulfillmentPlanRoute/Post", route);
                    post.AddGovHeader(GovernmentIDOrigin.Value);
                    await post.ExecuteNoResult();
                }
                else
                {
                    PutData put = new PutData(DataAccess.APIs.GovernmentPortal, "FulfillmentPlanRoute/Put", route);
                    put.AddGovHeader(GovernmentIDOrigin.Value);
                    await put.ExecuteNoResult();
                }
            }

            Close();
        }

        private void cmdSelectRailcarLeaseRequest_Click(object sender, EventArgs e)
        {
            ctxSelectRailcarLeaseRequest.Show(Cursor.Position);
        }

        private async void tsmiSelectRailcar_Click(object sender, EventArgs e)
        {
            frmDraftRailcarSelect selectRailcar = new frmDraftRailcarSelect();
            selectRailcar.GovernmentID = GovernmentIDOrigin.Value;
            selectRailcar.ShowDialog();

            if (selectRailcar.DialogResult != DialogResult.OK)
            {
                return;
            }

            if (selectRailcar.SelectedRailcarID == null)
            {
                lnkRailcar.Text = "[None]";
                lnkRailcar.Tag = null;
                lnkRailcar.Enabled = false;
            }
            else
            {
                GetData get = new GetData(DataAccess.APIs.GovernmentPortal, "Railcar/Get/" + selectRailcar.SelectedRailcarID);
                get.AddGovHeader(GovernmentIDOrigin.Value);
                FleetTracking.Models.Railcar railcar = await get.GetObject<FleetTracking.Models.Railcar>();
                lnkRailcar.Text = railcar.FormattedReportingMark;
                lnkRailcar.Tag = railcar;
                lnkRailcar.Enabled = true;
            }
        }

        private void tsmiClearRailcar_Click(object sender, EventArgs e)
        {
            lnkRailcar.Text = "[None]";
            lnkRailcar.Tag = null;
            lnkRailcar.Enabled = false;
        }

        private void ctxSelectRailcarLeaseRequest_Opening(object sender, CancelEventArgs e)
        {
            tsmiLeaseRequests.Enabled = PermissionsManager.HasFleetPermission(GovernmentIDOrigin.Value, nameof(FleetTracking.Models.FleetSecurity.AllowLeasingManagement));
        }

        private async void tsmiSelectLeaseRequest_Click(object sender, EventArgs e)
        {
            frmLeaseRequestSelect selectLeaseRequest = new frmLeaseRequestSelect();
            selectLeaseRequest.GovernmentID = GovernmentIDOrigin.Value;
            selectLeaseRequest.ShowDialog();

            if (selectLeaseRequest.DialogResult != DialogResult.OK)
            {
                return;
            }

            if (selectLeaseRequest.SelectedLeaseRequestID == null)
            {
                lnkRailcar.Text = "[None]";
                lnkRailcar.Tag = null;
                lnkRailcar.Enabled = false;
            }
            else
            {
                GetData get = new GetData(DataAccess.APIs.FleetTracking, "LeaseRequest/Get/" + selectLeaseRequest.SelectedLeaseRequestID);
                get.AddGovHeader(GovernmentIDOrigin.Value);
                FleetTracking.Models.LeaseRequest leaseRequest = await get.GetObject<FleetTracking.Models.LeaseRequest>();
                lnkRailcar.Text = $"Lease Request {leaseRequest.LeaseRequestID} ({leaseRequest.LeaseBids?.Count ?? 0} Bids)";
                lnkRailcar.Tag = leaseRequest;
                lnkRailcar.Enabled = true;
            }
        }

        private async void tsmiCreateLeaseRequest_Click(object sender, EventArgs e)
        {
            long? leaseRequestID = Shell.FleetTrackingApplication.CreateNewLease();

            if (leaseRequestID == null)
            {
                return;
            }

            GetData get = new GetData(DataAccess.APIs.FleetTracking, "LeaseRequest/Get/" + leaseRequestID);
            get.AddGovHeader(GovernmentIDOrigin.Value);
            FleetTracking.Models.LeaseRequest leaseRequest = await get.GetObject<FleetTracking.Models.LeaseRequest>();
            lnkRailcar.Text = $"Lease Request {leaseRequest.LeaseRequestID} ({leaseRequest.LeaseBids?.Count ?? 0} Bids)";
            lnkRailcar.Tag = leaseRequest;
            lnkRailcar.Enabled = true;
        }
    }
}
