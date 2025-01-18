using CompanyStudio.Extensions;
using CompanyStudio.Invoicing;
using CompanyStudio.Models;
using CompanyStudio.Purchasing.OpenMaintenance;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyStudio.Purchasing.History
{
    public partial class frmHistoricalPurchaseOrder : BaseCompanyStudioContent, ILocationScoped, ISaveable
    {
        public long? PurchaseOrderID { get; set; }
        public frmHistoricalPurchaseOrder()
        {
            InitializeComponent();
        }

        public Location LocationModel { get; set; }

        public event EventHandler OnSave;

        private async void frmHistoricalPurchaseOrder_Load(object sender, EventArgs e)
        {
            Studio.dockPanel.Contents.OfType<frmPurchaseOrderExplorer>().Where(poe => poe.LocationModel.LocationID == LocationModel.LocationID).FirstOrDefault()?.RegisterPurchaseOrderForm(this, () => PurchaseOrderID);

            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "PurchaseOrder/Get/" + PurchaseOrderID);
            get.AdditionalFields.Add(nameof(PurchaseOrder.InvoiceSchedule));
            get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            PurchaseOrder purchaseOrder = await get.GetObject<PurchaseOrder>();
            if (purchaseOrder != null)
            {
                Text = "Complete PO - " + PurchaseOrderID;
                lblTitle.Text += " - " + PurchaseOrderID;
                toolSaveTemplate.Visible = purchaseOrder.LocationIDOrigin == LocationModel.LocationID;
                tsbAutoApproving.Visible = purchaseOrder.LocationIDDestination == LocationModel.LocationID;

                txtPurchaser.Text = purchaseOrder.OriginString;
                txtSeller.Text = purchaseOrder.DestinationString;
                txtOrderDate.Text = purchaseOrder.PurchaseOrderDate?.ToString("MM/dd/yyyy");
                txtDescription.Text = purchaseOrder.Description;
                txtInvoiceSchedule.Text = purchaseOrder.InvoiceSchedule.ToString().ToDisplayName();

                foreach(PurchaseOrderLine line in purchaseOrder.PurchaseOrderLines)
                {
                    AddPurchaseOrderLine(line);
                }
            }

            get.Resource = "FulfillmentPlan/GetByPurchaseOrderID/" + PurchaseOrderID;
            List<FulfillmentPlan> fulfillmentPlans = await get.GetObject<List<FulfillmentPlan>>() ?? new List<FulfillmentPlan>();
            foreach (FulfillmentPlan plan in fulfillmentPlans)
            {
                AddFulfillmentPlan(plan);
            }

            Dictionary<Models.Fulfillment, PurchaseOrderLine> purchaseOrderLineByFulfillment = new Dictionary<Models.Fulfillment, PurchaseOrderLine>();
            purchaseOrder.PurchaseOrderLines.ForEach(pol => pol.Fulfillments.ForEach(f => purchaseOrderLineByFulfillment[f] = pol));

            foreach (Models.Fulfillment fulfillment in purchaseOrder.PurchaseOrderLines.SelectMany(pol => pol.Fulfillments).OrderByDescending(f => f.FulfillmentTime))
            {
                DataGridViewRow row = dgvFulfillments.Rows[dgvFulfillments.Rows.Add()];
                row.Cells[colPurchaseOrderLine.Name].Value = purchaseOrderLineByFulfillment[fulfillment]?.DisplayString;
                row.Cells[colRailcar.Name].Value = fulfillment.Railcar?.ReportingID;
                row.Cells[colQuantity.Name].Value = fulfillment.Quantity;
                row.Cells[colFulfillmentTime.Name].Value = fulfillment.FulfillmentTime?.ToString("MM/dd/yyyy HH:mm");
                row.Cells[colIsComplete.Name].Value = fulfillment.IsComplete;
                row.Tag = fulfillment;
            }

            get.Resource = "Invoice/GetForPurchaseOrder/" + PurchaseOrderID;
            List<Invoice> invoices = await get.GetObject<List<Invoice>>() ?? new List<Invoice>();
            foreach (Invoice invoice in invoices)
            {
                AddInvoice(invoice);
            }

            if (tsbAutoApproving.Visible)
            {
                get.Resource = "PurchaseOrderApproval/GetForPurchaseOrder/" + PurchaseOrderID;
                PurchaseOrderApproval purchaseOrderApproval = await get.GetObject<PurchaseOrderApproval>();
                if (purchaseOrderApproval != null)
                {
                    tsbAutoApproving.Image = purchaseOrderApproval.FutureAutoApprove ? Properties.Resources.accept : Properties.Resources.cancel;
                    tsbEnableAutoApproval.Checked = purchaseOrderApproval.FutureAutoApprove;
                    tsbDisableAutoApproval.Checked = !purchaseOrderApproval.FutureAutoApprove;
                }
            }
        }

        private void AddInvoice(Invoice invoice)
        {
            DataGridViewRow row = dgvInvoices.Rows[dgvInvoices.Rows.Add()];
            row.Cells[colInvoiceNumber.Name].Value = invoice.InvoiceNumber;
            row.Cells[colInvoiceDate.Name].Value = invoice.InvoiceDate.ToString("MM/dd/yyyy");
            row.Cells[colDueDate.Name].Value = invoice.DueDate.ToString("MM/dd/yyyy");
            row.Cells[colStatus.Name].Value = invoice.Status.ToString().ToDisplayName();
            row.Cells[colInvoiceAmount.Name].Value = invoice.InvoiceLines?.Sum(il => il.Total).ToString("N2") ?? "0.00";
            row.Tag = invoice;
        }

        private class FulfillmentComparer : IEqualityComparer<Models.Fulfillment>
        {
            public bool Equals(Models.Fulfillment x, Models.Fulfillment y)
            {
                if (x == null && y == null) return true;

                if ((x == null && y != null) || (x != null && y == null)) return false;

                return x.FulfillmentID.Equals(y.FulfillmentID);
            }

            public int GetHashCode(Models.Fulfillment obj)
            {
                return obj.FulfillmentID.GetHashCode();
            }
        }

        private void AddFulfillmentPlan(FulfillmentPlan plan)
        {
            DataGridViewRow row = dgvFulfillmentPlans.Rows[dgvFulfillmentPlans.Rows.Add()];
            row.Cells[colFPRailcar.Name].Value = plan.Railcar?.ReportingID;
            row.Cells[colFPPOLines.Name].Value = (plan.FulfillmentPlanPurchaseOrderLines?.Count ?? 0) + " Purchase Order Line(s)";
            string route = "";
            FulfillmentPlanRoute firstRoute = plan.FulfillmentPlanRoutes.FirstOrDefault();
            if (firstRoute != null)
            {
                if (firstRoute.CompanyIDFrom != null)
                {
                    route = firstRoute.CompanyFrom?.Name + " -> ";
                }
                else if (firstRoute.GovernmentIDFrom != null)
                {
                    route = firstRoute.GovernmentFrom?.Name + " -> ";
                }
            }
            route += string.Join(" -> ", plan.FulfillmentPlanRoutes.Select(fpr => fpr.CompanyIDTo != null ? fpr.CompanyTo?.Name : fpr.GovernmentTo?.Name));
            row.Cells[colFPRoute.Name].Value = route;
            row.Tag = plan;
        }

        private void AddPurchaseOrderLine(PurchaseOrderLine line)
        {
            int top = 0;
            if (pnlLines.Controls.OfType<PurchaseOrderLineControl>().Any())
            {
                top = pnlLines.Controls.OfType<PurchaseOrderLineControl>().Max(polc => polc.Bottom);
            }

            PurchaseOrderLineControl lineControl = new PurchaseOrderLineControl();
            lineControl.PurchaseOrderLine = line;
            lineControl.Top = top;
            lineControl.Left = 0;
            lineControl.Width = pnlLines.Width;
            lineControl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lineControl.OnFulfillmentPlansClicked += PurchaseOrderLine_OnFulfillmentPlansClicked;
            lineControl.OnFulfillmentClicked += PurchaseOrderLine_OnFulfillmentClicked;
            pnlLines.Controls.Add(lineControl);
        }

        private void PurchaseOrderLine_OnFulfillmentClicked(object sender, EventArgs e)
        {
            PurchaseOrderLineControl control = sender as PurchaseOrderLineControl;
            if (control == null)
            {
                return;
            }

            tabControl1.SelectedTab = tabFulfillment;

            foreach (DataGridViewRow row in dgvFulfillments.Rows)
            {
                Models.Fulfillment fulfillment = row.Tag as Models.Fulfillment;
                dgvFulfillments.Rows[row.Index].Selected = fulfillment != null && fulfillment.PurchaseOrderLineID == control.PurchaseOrderLine.PurchaseOrderLineID;
            }
        }

        private void PurchaseOrderLine_OnFulfillmentPlansClicked(object sender, EventArgs e)
        {
            PurchaseOrderLineControl control = sender as PurchaseOrderLineControl;
            if (control == null)
            {
                return;
            }

            PurchaseOrderLine purchaseOrderLine = control.PurchaseOrderLine;
            DataGridViewRow selectedRow = dgvFulfillmentPlans
                                            .Rows
                                            .OfType<DataGridViewRow>()
                                            .Where(row =>
                                                row.Tag is FulfillmentPlan fp &&
                                                fp != null &&
                                                fp.FulfillmentPlanPurchaseOrderLines
                                                    .Any(fppol =>
                                                        fppol.PurchaseOrderLineID == purchaseOrderLine.PurchaseOrderLineID))
                                            .FirstOrDefault();

            if (selectedRow == null)
            {
                return;
            }

            tabControl1.SelectedTab = tabFulfillmentPlan;
            foreach (DataGridViewRow row in dgvFulfillmentPlans.Rows)
            {
                row.Selected = false;
            }

            selectedRow.Selected = true;
        }

        private void dgvFulfillmentPlans_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvFulfillmentPlans.SelectedRows.Count <= 0)
            {
                return;
            }

            DataGridViewRow row = dgvFulfillmentPlans.SelectedRows[0];
            FulfillmentPlan fulfillmentPlan = row.Tag as FulfillmentPlan;
            if (fulfillmentPlan == null)
            {
                return;
            }

            lnkFPRailcar.Text = fulfillmentPlan.Railcar?.ReportingID;
            lnkFPRailcar.Tag = fulfillmentPlan.Railcar;
            StringBuilder purchaseOrderLines = new StringBuilder();
            foreach (PurchaseOrderLine purchaseOrderLine in fulfillmentPlan.FulfillmentPlanPurchaseOrderLines.Select(fppol => fppol.PurchaseOrderLine))
            {
                if (purchaseOrderLines.Length > 0)
                {
                    purchaseOrderLines.Append(", ");
                }

                purchaseOrderLines.Append($"{purchaseOrderLine.Quantity}x ");
                if (purchaseOrderLine.IsService)
                {
                    purchaseOrderLines.Append(purchaseOrderLine.ServiceDescription);
                }
                else
                {
                    if (purchaseOrderLine.ItemID != null)
                    {
                        purchaseOrderLines.Append(purchaseOrderLine.Item.Name);
                        if (!string.IsNullOrEmpty(purchaseOrderLine.ItemDescription))
                        {
                            purchaseOrderLines.Append(" - ");
                        }
                    }

                    if (!string.IsNullOrEmpty(purchaseOrderLine.ItemDescription))
                    {
                        purchaseOrderLines.Append(purchaseOrderLine.ItemDescription);
                    }
                }
            }

            txtFPPurchaseOrderLines.Text = purchaseOrderLines.ToString();
            txtFPPickupTrack.Text = fulfillmentPlan.TrackLoading?.Name;
            txtFPStrategicAfterPickup.Text = fulfillmentPlan.TrackStrategicAfterLoad?.Name;
            txtFPDropOffTrack.Text = fulfillmentPlan.TrackDestination?.Name;
            txtFPStrategicAfterDropOff.Text = fulfillmentPlan.TrackStrategicAfterDestination?.Name;
            txtFPDestinationAfterFulfillment.Text = fulfillmentPlan.TrackPostFulfillment?.Name;
            txtFPRailcarRouting.Text = row.Cells[colFPRoute.Name].Value as string;
        }

        private void lnkFPRailcar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Railcar railcar = lnkFPRailcar.Tag as Railcar;
            if (railcar != null)
            {
                if (PermissionsManager.HasPermissionFleetTrack(Company.CompanyID ?? 0L, nameof(FleetTracking.Models.FleetSecurity.AllowSetup)) ||
                    PermissionsManager.HasPermissionFleetTrack(Company.CompanyID ?? 0L, nameof(FleetTracking.Models.FleetSecurity.IsTrainCrew)) ||
                    PermissionsManager.HasPermissionFleetTrack(Company.CompanyID ?? 0L, nameof(FleetTracking.Models.FleetSecurity.IsYardmaster)))
                {
                    Studio.GetFleetTrackingApplication(Company.CompanyID)?.OpenRailcarDetail(railcar.RailcarID);
                }
            }
        }

        private void dgvInvoices_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvInvoices.Rows.Count || !PermissionsManager.HasPermission(LocationModel.LocationID.Value, PermissionsManager.LocationWidePermissions.ManageInvoices))
            {
                return;
            }

            DataGridViewRow row = dgvInvoices.Rows[e.RowIndex];
            Invoice invoice = row.Tag as Invoice;

            if (invoice.LocationIDFrom == LocationModel.LocationID)
            {
                frmReceivableInvoice receivableInvoice = new frmReceivableInvoice();
                Studio.DecorateStudioContent(receivableInvoice);
                receivableInvoice.Company = Company;
                receivableInvoice.LocationModel = LocationModel;
                receivableInvoice.InvoiceID = invoice.InvoiceID;
                receivableInvoice.Show(Studio.dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            }
            else if (invoice.LocationIDTo == LocationModel.LocationID)
            {
                frmPayableInvoice payableInvoice = new frmPayableInvoice();
                Studio.DecorateStudioContent(payableInvoice);
                payableInvoice.Company = Company;
                payableInvoice.LocationModel = LocationModel;
                payableInvoice.InvoiceID = invoice.InvoiceID;
                payableInvoice.Show(Studio.dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
            }
        }

        private async void toolSaveTemplate_Click(object sender, EventArgs e)
        {
            await PurchaseOrderTemplate.PromptAndSavePurchaseOrderAsTemplate(Company, LocationModel, Theme, PurchaseOrderID);
        }

        private async void tsbEnableAutoApproval_Click(object sender, EventArgs e)
        {
            PostData post = new PostData(DataAccess.APIs.CompanyStudio, "PurchaseOrderApproval/SetAutoApprove", new { PurchaseOrderID, AutoApprove = true });
            post.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            await post.ExecuteNoResult();

            if (post.RequestSuccessful)
            {
                tsbAutoApproving.Image = Properties.Resources.accept;
                tsbEnableAutoApproval.Checked = true;
                tsbDisableAutoApproval.Checked = false;
            }
        }

        private async void tsbDisableAutoApproval_Click(object sender, EventArgs e)
        {
            PostData post = new PostData(DataAccess.APIs.CompanyStudio, "PurchaseOrderApproval/SetAutoApprove", new { PurchaseOrderID, AutoApprove = false });
            post.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            await post.ExecuteNoResult();

            if (post.RequestSuccessful)
            {
                tsbAutoApproving.Image = Properties.Resources.cancel;
                tsbEnableAutoApproval.Checked = false;
                tsbDisableAutoApproval.Checked = true;
            }
        }

        public Task Save()
        {
            return Task.CompletedTask;
        }
    }
}
