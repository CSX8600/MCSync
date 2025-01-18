using CompanyStudio.Extensions;
using CompanyStudio.Models;
using CompanyStudio.Purchasing.DraftEntry;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using MesaSuite.Common.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyStudio.Purchasing.ApprovalViewer
{
    public partial class frmApprovalViewerSubmitter : BaseCompanyStudioContent, ILocationScoped, ISaveable
    {
        public long? PurchaseOrderID { get; set; }
        public Location LocationModel { get; set; }

        public frmApprovalViewerSubmitter()
        {
            InitializeComponent();
        }

        public event EventHandler OnSave;

        private async void frmApprovalViewerSubmitter_Load(object sender, EventArgs e)
        {
            Text += $" - {PurchaseOrderID}";
            try
            {
                loader.Visible = true;
                loader.BringToFront();

                Studio.dockPanel.Contents.OfType<frmPurchaseOrderExplorer>().Where(poe => poe.LocationModel.LocationID == LocationModel.LocationID).FirstOrDefault()?.RegisterPurchaseOrderForm(this, () => PurchaseOrderID);

                GetData get = new GetData(DataAccess.APIs.CompanyStudio, "PurchaseOrder/Get/" + PurchaseOrderID);
                get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                PurchaseOrder purchaseOrder = await get.GetObject<PurchaseOrder>();
                if (purchaseOrder.LocationIDDestination != null)
                {
                    txtFrom.Text = $"{purchaseOrder.LocationDestination?.Company?.Name} ({purchaseOrder.LocationDestination?.Name})";
                }
                else
                {
                    txtFrom.Text = purchaseOrder.GovernmentDestination?.Name;
                }

                txtDescription.Text = purchaseOrder.Description;
                txtDate.Text = purchaseOrder.PurchaseOrderDate?.ToString("MM/dd/yyyy");
                decimal runningTotal = 0M;
                foreach(PurchaseOrderLine line in purchaseOrder.PurchaseOrderLines ?? new List<PurchaseOrderLine>())
                {
                    int rowIndex = dgvLines.Rows.Add();
                    DataGridViewRow row = dgvLines.Rows[rowIndex];
                    row.Cells[colType.Name].Value = line.IsService ? "Service" : "Item";
                    string description = "";
                    if (line.ItemID != null)
                    {
                        description = line.Item?.Name;
                    }

                    if (!string.IsNullOrEmpty(line.ItemDescription))
                    {
                        description += " - " + line.ItemDescription;
                    }

                    if (!string.IsNullOrEmpty(line.ServiceDescription))
                    {
                        description += description.Length == 0 ? line.ServiceDescription : " - " + line.ServiceDescription;
                    }

                    row.Cells[colDescription.Name].Value = description;
                    row.Cells[colQuantity.Name].Value = line.Quantity;
                    row.Cells[colUnitCost.Name].Value = line.UnitCost;
                    row.Cells[colLineTotal.Name].Value = line.Quantity * line.UnitCost;
                    row.Cells[colHasFulfillmentPlan.Name].Value = (line.FulfillmentPlanPurchaseOrderLines?.Count ?? 0) > 0;
                    runningTotal += (line.Quantity * line.UnitCost) ?? 0M;
                }

                txtTotal.Text = runningTotal.ToString("N2");

                List<SalesTax> salesTaxes = new List<SalesTax>();
                if (purchaseOrder.GovernmentIDDestination != null)
                {
                    txtEstTax.Text = "0.00";
                    txtGrossTotal.Text = txtTotal.Text;
                }
                else
                {
                    get = new GetData(DataAccess.APIs.CompanyStudio, "SalesTax/GetEffectiveSalesTaxForLocation/" + purchaseOrder.LocationIDDestination);
                    get.AddCompanyHeader(Company.CompanyID);
                    salesTaxes = await get.GetObject<List<SalesTax>>() ?? new List<SalesTax>();

                    decimal taxRate = salesTaxes.Sum(t => t.Rate) / 100M;
                    txtEstTax.Text = (runningTotal * taxRate).ToString("N2");
                    txtGrossTotal.Text = (runningTotal + (runningTotal * taxRate)).ToString("N2");
                }

                lnkTaxBreakdown.Tag = salesTaxes;

                foreach(PurchaseOrderApproval approval in purchaseOrder.PurchaseOrderApprovals ?? new List<PurchaseOrderApproval>())
                {
                    AddApprovalControl(approval);
                }

                get = new GetData(DataAccess.APIs.CompanyStudio, "FulfillmentPlan/GetByPurchaseOrderID/" + PurchaseOrderID);
                get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                List<FulfillmentPlan> fulfillmentPlans = await get.GetObject<List<FulfillmentPlan>>() ?? new List<FulfillmentPlan>();
                bool hasLeaseRequest = false;
                bool hasMissingRailcar = false;
                foreach(FulfillmentPlan plan in fulfillmentPlans)
                {
                    int rowIndex = dgvFulfillmentPlans.Rows.Add();
                    DataGridViewRow row = dgvFulfillmentPlans.Rows[rowIndex];

                    string railcarValue = "";
                    if (plan.RailcarID != null)
                    {
                        railcarValue = plan.Railcar?.ReportingID;
                    }
                    else if (plan.LeaseRequestID != null)
                    {
                        railcarValue = string.Format("Lease Request ID: {0} (Bids: {1})", plan.LeaseRequestID, plan.LeaseRequest?.LeaseBids?.Count ?? 0);
                        row.Cells[colRailcar.Name].Style.BackColor = Color.Yellow;
                        hasLeaseRequest = true;
                    }
                    else
                    {
                        railcarValue = "None";
                        row.Cells[colRailcar.Name].Style.BackColor = Color.Red;
                        hasMissingRailcar = true;
                    }

                    row.Cells[colRailcar.Name].Value = railcarValue;
                    row.Cells[colPOLines.Name].Value = (plan.FulfillmentPlanPurchaseOrderLines?.Count ?? 0) + " Lines";
                    string routeValue = "";
                    FulfillmentPlanRoute firstRoute = plan.FulfillmentPlanRoutes?.FirstOrDefault();
                    if (firstRoute != null)
                    {
                        routeValue = (firstRoute.CompanyIDFrom != null ? firstRoute.CompanyFrom?.Name : firstRoute.GovernmentFrom?.Name) + " -> ";
                    }

                    row.Cells[colRoute.Name].Value = routeValue + string.Join(" -> ", plan.FulfillmentPlanRoutes?.Select(r => r.CompanyIDTo != null ? r.CompanyTo?.Name : r.GovernmentTo?.Name));
                    row.Tag = plan;
                }

                if (hasLeaseRequest)
                {
                    OutstandingLeaseRequests outstandingLeaseRequests = new OutstandingLeaseRequests()
                    {
                        Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right
                    };
                    pnlApprovals.Controls.Add(outstandingLeaseRequests);
                    outstandingLeaseRequests.Top = pnlApprovals.Controls.OfType<Control>().OrderByDescending(x => x.Bottom).FirstOrDefault()?.Bottom ?? 0;
                    outstandingLeaseRequests.Width = pnlApprovals.Width;
                }

                if (hasMissingRailcar)
                {
                    MissingRailcar missingRailcar = new MissingRailcar()
                    {
                        Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right
                    };
                    pnlApprovals.Controls.Add(missingRailcar);
                    missingRailcar.Top = pnlApprovals.Controls.OfType<Control>().OrderByDescending(x => x.Bottom).FirstOrDefault()?.Bottom ?? 0;
                    missingRailcar.Width = pnlApprovals.Width;
                }
            }
            finally
            {
                loader.Visible = false;
            }
        }

        private void AddApprovalControl(PurchaseOrderApproval approval)
        {
            ApprovalControl control = new ApprovalControl()
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right,
                PurchaseOrderApproval = approval
            };

            int top = pnlApprovals.Controls.OfType<ApprovalControl>().OrderByDescending(x => x.Bottom).FirstOrDefault()?.Bottom ?? 0;
            pnlApprovals.Controls.Add(control);
            control.Top = top;
            control.Width = pnlApprovals.Width;
        }

        private async void cmdWithdrawSubmission_Click(object sender, EventArgs e)
        {
            if (!this.Confirm("Withdrawing this submission will reset all approvals. Are you sure you want to continue?"))
            {
                return;
            }

            try
            {
                loader.Visible = true;
                loader.BringToFront();

                PostData post = new PostData(DataAccess.APIs.CompanyStudio, "PurchaseOrder/WithdrawSubmission/" + PurchaseOrderID, new object());
                post.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                await post.ExecuteNoResult();
                if (post.RequestSuccessful)
                {
                    DraftEntry.frmPurchaseOrder draftEntry = new DraftEntry.frmPurchaseOrder()
                    {
                        PurchaseOrderID = PurchaseOrderID
                    };
                    Studio.DecorateStudioContent(draftEntry);
                    draftEntry.Company = Company;
                    draftEntry.LocationModel = LocationModel;
                    draftEntry.Show(Studio.dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
                    Close();
                }
            }
            finally
            {
                loader.Visible = false;
            }
        }

        public Task Save()
        {
            return Task.CompletedTask;
        }

        private void dgvFulfillmentPlans_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvFulfillmentPlans.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
            if (row == null)
            {
                return;
            }

            FulfillmentPlan plan = row.Tag as FulfillmentPlan;
            if (plan == null)
            {
                return;
            }

            if (plan.RailcarID != null)
            {
                lnkRailcar.Text = plan.Railcar?.ReportingID;
                lnkRailcar.Tag = plan.Railcar;
            }
            else if (plan.LeaseRequestID != null)
            {
                lnkRailcar.Text = string.Format("Lease Request ID: {0} (Bids: {1})", plan.LeaseRequestID, plan.LeaseRequest?.LeaseBids?.Count ?? 0);
                lnkRailcar.Tag = plan.LeaseRequest;
            }
            else
            {
                lnkRailcar.Text = "None";
                lnkRailcar.Tag = null;
            }

            txtPurchaseOrderLines.Text = string.Join(", ", plan.FulfillmentPlanPurchaseOrderLines?.Select(l => l.PurchaseOrderLine.DisplayString));
            txtPickupTrack.Text = plan.TrackLoading?.Name;
            txtStrategicAfterPickup.Text = plan.TrackStrategicAfterLoad?.Name;
            txtDropOffTrack.Text = plan.TrackDestination?.Name;
            txtStrategicAfterDropOff.Text = plan.TrackStrategicAfterDestination?.Name;
            txtDestinationAfterFulfillment.Text = plan.TrackPostFulfillment?.Name;
            string routeValue = "";
            FulfillmentPlanRoute firstRoute = plan.FulfillmentPlanRoutes?.FirstOrDefault();
            if (firstRoute != null)
            {
                routeValue = (firstRoute.CompanyIDFrom != null ? firstRoute.CompanyFrom?.Name : firstRoute.GovernmentFrom?.Name) + " -> ";
            }
            txtRailcarRouting.Text = routeValue + string.Join(" -> ", plan.FulfillmentPlanRoutes?.Select(r => r.CompanyIDTo != null ? r.CompanyTo?.Name : r.GovernmentTo?.Name));
        }

        private void lnkRailcar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Railcar railcar = lnkRailcar.Tag as Railcar;
            if (railcar != null)
            {
                if (PermissionsManager.HasPermissionFleetTrack(Company.CompanyID ?? 0L, nameof(FleetTracking.Models.FleetSecurity.AllowSetup)) ||
                    PermissionsManager.HasPermissionFleetTrack(Company.CompanyID ?? 0L, nameof(FleetTracking.Models.FleetSecurity.IsTrainCrew)) ||
                    PermissionsManager.HasPermissionFleetTrack(Company.CompanyID ?? 0L, nameof(FleetTracking.Models.FleetSecurity.IsYardmaster)))
                {
                    Studio.GetFleetTrackingApplication(Company.CompanyID)?.OpenRailcarDetail(railcar.RailcarID);
                }
            }

            LeaseRequest leaseRequest = lnkRailcar.Tag as LeaseRequest;
            if (leaseRequest != null)
            {
                if (PermissionsManager.HasPermissionFleetTrack(Company.CompanyID ?? 0L, nameof(FleetTracking.Models.FleetSecurity.AllowLeasingManagement)))
                {
                    Studio.GetFleetTrackingApplication(Company.CompanyID)?.OpenLeaseRequestDetail(leaseRequest.LeaseRequestID);
                }
            }
        }

        private void lnkTaxBreakdown_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!(lnkTaxBreakdown.Tag is List<SalesTax> salesTaxes) || !salesTaxes.Any())
            {
                this.ShowInformation("No tax information available.");
                return;
            }

            StringBuilder taxInformation = new StringBuilder();
            foreach (SalesTax salesTax in salesTaxes)
            {
                taxInformation.AppendLine($"{salesTax.Government.Name}: {salesTax.Rate}%");
            }

            this.ShowInformation(taxInformation.ToString());
        }

        private async void toolSaveTemplate_Click(object sender, EventArgs e)
        {
            await PurchaseOrderTemplate.PromptAndSavePurchaseOrderAsTemplate(Company, LocationModel, Theme, PurchaseOrderID);
        }
    }
}
