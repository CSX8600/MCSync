using CompanyStudio.Extensions;
using CompanyStudio.Models;
using MesaSuite.Common;
using MesaSuite.Common.Attributes;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using MesaSuite.Common.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyStudio.Purchasing.ApprovalViewer
{
    [UriReachable("poapproval/{PurchaseOrderID}/{PurchaseOrderApprovalID}")]
    public partial class frmApprovalViewerApprover : BaseCompanyStudioContent, ILocationScoped, ISaveable
    {
        public frmApprovalViewerApprover()
        {
            InitializeComponent();
        }

        public Location LocationModel { get; set; }

        public event EventHandler OnSave;

        public long? PurchaseOrderID { get; set; }
        public long? PurchaseOrderApprovalID { get; set; }

        private async void frmApprovalViewerApprover_Load(object sender, EventArgs e)
        {
            Text += $" - {PurchaseOrderID}";
            Studio.dockPanel.Contents.OfType<frmPurchaseOrderExplorer>().Where(poe => poe.LocationModel.LocationID == LocationModel.LocationID).FirstOrDefault()?.RegisterPurchaseOrderForm(this, () => PurchaseOrderID);

            try
            {
                loader.Visible = true;
                loader.BringToFront();

                GetData get = new GetData(DataAccess.APIs.CompanyStudio, "PurchaseOrder/Get/" + PurchaseOrderID);
                get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                get.AdditionalFields.Add(nameof(PurchaseOrder.InvoiceSchedule));
                get.AdditionalFields.Add(nameof(PurchaseOrder.AccountIDReceiving));
                PurchaseOrder purchaseOrder = await get.GetObject<PurchaseOrder>();
                if (purchaseOrder == null)
                {
                    Close();
                    return;
                }

                PurchaseOrderApproval purchaseOrderApproval = purchaseOrder.PurchaseOrderApprovals.FirstOrDefault(poa => poa.CompanyIDApprover == Company.CompanyID);
                if (purchaseOrderApproval == null)
                {
                    Close();
                    return;
                }

                PurchaseOrderApprovalID = purchaseOrderApproval.PurchaseOrderApprovalID;
                txtApprovalReason.Text = purchaseOrderApproval.ApprovalPurpose;
                cmdApprove.Visible = purchaseOrderApproval.ApprovalStatus == PurchaseOrderApproval.ApprovalStatuses.Pending;
                cmdDeny.Visible = purchaseOrderApproval.ApprovalStatus == PurchaseOrderApproval.ApprovalStatuses.Pending;

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
                foreach (PurchaseOrderLine line in purchaseOrder.PurchaseOrderLines ?? new List<PurchaseOrderLine>())
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

                if (purchaseOrder.LocationIDDestination != LocationModel.LocationID)
                {
                    tabControl1.TabPages.Remove(tabSellerSetup);
                }
                else
                {
                    cboInvoiceSchedule.Items.Clear();

                    foreach(PurchaseOrder.InvoiceSchedules schedule in Enum.GetValues(typeof(PurchaseOrder.InvoiceSchedules)))
                    {
                        cboInvoiceSchedule.Items.Add(new DropDownItem<PurchaseOrder.InvoiceSchedules>(schedule, schedule.ToString().ToDisplayName()));
                    }

                    cboInvoiceSchedule.SelectedItem = cboInvoiceSchedule.Items.OfType<DropDownItem<PurchaseOrder.InvoiceSchedules>>().FirstOrDefault(x => x.Object == purchaseOrder.InvoiceSchedule);

                    get = new GetData(DataAccess.APIs.CompanyStudio, "Account/GetAllForUser");
                    get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                    List<Account> accounts = await get.GetObject<List<Account>>() ?? new List<Account>();
                    cboReceivingAccount.Items.Clear();
                    foreach (Account account in accounts)
                    {
                        cboReceivingAccount.Items.Add(new DropDownItem<Account>(account, $"{account.Description} ({account.AccountNumber})"));
                    }
                    cboReceivingAccount.SelectedItem = cboReceivingAccount.Items.OfType<DropDownItem<Account>>().FirstOrDefault(x => x.Object.AccountID == purchaseOrder.AccountIDReceiving);
                }

                get = new GetData(DataAccess.APIs.CompanyStudio, "FulfillmentPlan/GetByPurchaseOrderID/" + PurchaseOrderID);
                get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                List<FulfillmentPlan> fulfillmentPlans = await get.GetObject<List<FulfillmentPlan>>() ?? new List<FulfillmentPlan>();
                foreach (FulfillmentPlan plan in fulfillmentPlans)
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
                    }
                    else
                    {
                        railcarValue = "None";
                        row.Cells[colRailcar.Name].Style.BackColor = Color.Red;
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

        private async void cmdDeny_Click(object sender, EventArgs e)
        {
            GenericInputBox inputBox = new GenericInputBox()
            {
                Text = "Reason for Rejection",
                Prompt = "Enter the reason for rejection:",
                AcceptText = "Submit",
                ResultType = typeof(string)
            };

            if (inputBox.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                loader.BringToFront();
                loader.Visible = true;

                PostData post = new PostData(DataAccess.APIs.CompanyStudio, "PurchaseOrderApproval/Reject/" + PurchaseOrderApprovalID, new { reason = inputBox.Result });
                post.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                await post.ExecuteNoResult();
                if (post.RequestSuccessful)
                {
                    Close();
                    return;
                }
            }
            finally
            {
                loader.Visible = false;
            }
        }

        private async void cmdApprove_Click(object sender, EventArgs e)
        {
            try
            {
                loader.BringToFront();
                loader.Visible = true;

                var approveData = new
                {
                    FutureAutoApprove = chkFutureAutoApprove.Checked,
                    InvoiceSchedule = tabControl1.TabPages.Contains(tabSellerSetup) ? (cboInvoiceSchedule.SelectedItem as DropDownItem<PurchaseOrder.InvoiceSchedules>)?.Object : null,
                    AccountIDReceiving = tabControl1.TabPages.Contains(tabSellerSetup) ? (cboReceivingAccount.SelectedItem as DropDownItem<Account>)?.Object.AccountID : null
                };

                PostData post = new PostData(DataAccess.APIs.CompanyStudio, "PurchaseOrderApproval/Approve/" + PurchaseOrderApprovalID, approveData);
                post.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                await post.ExecuteNoResult();
                if (post.RequestSuccessful)
                {
                    Close();
                    return;
                }
            }
            finally
            {
                loader.Visible = false;
            }
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
    }
}
