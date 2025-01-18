using CompanyStudio.Extensions;
using CompanyStudio.Invoicing;
using CompanyStudio.Models;
using MesaSuite.Common.Attributes;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyStudio.Purchasing.OpenMaintenance
{
    [UriReachable("openposubmitter/{PurchaseOrderID}")]
    public partial class frmOpenViewerSubmitter : BaseCompanyStudioContent, ILocationScoped, ISaveable
    {
        public long? PurchaseOrderID { get; set; }
        public Location LocationModel { get; set; }

        public frmOpenViewerSubmitter()
        {
            InitializeComponent();
        }

        public event EventHandler OnSave;

        private async void frmOpenViewerSubmitter_Load(object sender, EventArgs e)
        {
            Text += $" - {PurchaseOrderID}";
            Studio.dockPanel.Contents.OfType<frmPurchaseOrderExplorer>().Where(poe => poe.LocationModel.LocationID == LocationModel.LocationID).FirstOrDefault()?.RegisterPurchaseOrderForm(this, () => PurchaseOrderID);
            try
            {
                loader.BringToFront();
                loader.Visible = true;

                GetData get = new GetData(DataAccess.APIs.CompanyStudio, "PurchaseOrder/Get/" + PurchaseOrderID);
                get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                PurchaseOrder purchaseOrder = await get.GetObject<PurchaseOrder>() ?? new PurchaseOrder();
                txtFrom.Text = purchaseOrder.DestinationString;
                txtDescription.Text = purchaseOrder.Description;
                txtDate.Text = purchaseOrder.PurchaseOrderDate?.ToString("MM/dd/yyyy");

                foreach(PurchaseOrderLine purchaseOrderLine in purchaseOrder.PurchaseOrderLines ?? new List<PurchaseOrderLine>())
                {
                    AddPurchaseOrderLine(purchaseOrderLine);
                }

                get = new GetData(DataAccess.APIs.CompanyStudio, "FulfillmentPlan/GetByPurchaseOrderID/" + PurchaseOrderID);
                get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                List<FulfillmentPlan> fulfillmentPlans = await get.GetObject<List<FulfillmentPlan>>() ?? new List<FulfillmentPlan>();
                foreach(FulfillmentPlan fulfillmentPlan in fulfillmentPlans)
                {
                    int rowIndex = dgvFulfillmentPlans.Rows.Add();
                    DataGridViewRow row = dgvFulfillmentPlans.Rows[rowIndex];
                    row.Cells[colFPRailcar.Name].Value = fulfillmentPlan.Railcar?.ReportingID;
                    row.Cells[colFPPOLines.Name].Value = (fulfillmentPlan.FulfillmentPlanPurchaseOrderLines?.Count ?? 0) + " Purchase Order Line(s)";
                    string route = "";
                    FulfillmentPlanRoute firstRoute = fulfillmentPlan.FulfillmentPlanRoutes.FirstOrDefault();
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
                    route += string.Join(" -> ", fulfillmentPlan.FulfillmentPlanRoutes.Select(fpr => fpr.CompanyIDTo != null ? fpr.CompanyTo?.Name : fpr.GovernmentTo?.Name));
                    row.Cells[colFPRoute.Name].Value = route;
                    row.Tag = fulfillmentPlan;
                }

                Dictionary<Models.Fulfillment, PurchaseOrderLine> purchaseOrderLineByFulfillment = new Dictionary<Models.Fulfillment, PurchaseOrderLine>();
                purchaseOrder.PurchaseOrderLines.ForEach(pol => pol.Fulfillments.ForEach(f => purchaseOrderLineByFulfillment[f] = pol));

                foreach(Models.Fulfillment fulfillment in purchaseOrder.PurchaseOrderLines.SelectMany(pol => pol.Fulfillments).OrderByDescending(f => f.FulfillmentTime))
                {
                    DataGridViewRow row = dgvFulfillments.Rows[dgvFulfillments.Rows.Add()];
                    row.Cells[colPurchaseOrderLine.Name].Value = purchaseOrderLineByFulfillment[fulfillment]?.DisplayString;
                    row.Cells[colRailcar.Name].Value = fulfillment.Railcar?.ReportingID;
                    row.Cells[colQuantity.Name].Value = fulfillment.Quantity;
                    row.Cells[colFulfillmentTime.Name].Value = fulfillment.FulfillmentTime?.ToString("MM/dd/yyyy HH:mm");
                    row.Cells[colIsComplete.Name].Value = fulfillment.IsComplete;
                    row.Tag = fulfillment;
                }

                get = new GetData(DataAccess.APIs.CompanyStudio, "Invoice/GetForPurchaseOrder/" + PurchaseOrderID);
                get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                List<Invoice> invoices = await get.GetObject<List<Invoice>>() ?? new List<Invoice>();
                foreach(Invoice invoice in invoices)
                {
                    DataGridViewRow row = dgvInvoices.Rows[dgvInvoices.Rows.Add()];
                    row.Cells[colInvoiceNumber.Name].Value = invoice.InvoiceNumber;
                    row.Cells[colInvoiceDate.Name].Value = invoice.InvoiceDate.ToString("MM/dd/yyyy");
                    row.Cells[colDueDate.Name].Value = invoice.DueDate.ToString("MM/dd/yyyy");
                    row.Cells[colStatus.Name].Value = invoice.Status.ToString().ToDisplayName();
                    row.Cells[colInvoiceAmount.Name].Value = invoice.InvoiceLines?.Sum(il => il.Total).ToString("N2") ?? "0.00";
                    row.Tag = invoice;
                }
            }
            finally
            {
                loader.Visible = false;
            }
        }

        private void AddPurchaseOrderLine(PurchaseOrderLine purchaseOrderLine)
        {
            PurchaseOrderLineReadOnly purchaseOrderLineReadOnly = new PurchaseOrderLineReadOnly()
            {
                PurchaseOrderLine = purchaseOrderLine,
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right
            };
            int top = 0;
            if (pnlPOLines.Controls.OfType<PurchaseOrderLineReadOnly>().Any())
            {
                top = pnlPOLines.Controls.OfType<PurchaseOrderLineReadOnly>().Max(c => c.Bottom);
            }
            purchaseOrderLineReadOnly.Top = top;
            purchaseOrderLineReadOnly.OnFulfillmentPlanLinkClicked += PurchaseOrderLineReadOnly_OnFulfillmentPlanLinkClicked;
            purchaseOrderLineReadOnly.OnFulfillmentsLinkClicked += PurchaseOrderLineReadOnly_OnFulfillmentsLinkClicked;
            pnlPOLines.Controls.Add(purchaseOrderLineReadOnly);
            purchaseOrderLineReadOnly.Width = pnlPOLines.Width;
        }

        private void PurchaseOrderLineReadOnly_OnFulfillmentsLinkClicked(object sender, EventArgs e)
        {
            PurchaseOrderLineReadOnly purchaseOrderLineReadOnly = sender as PurchaseOrderLineReadOnly;
            if (purchaseOrderLineReadOnly == null)
            {
                return;
            }

            tabControl1.SelectedTab = tabFulfillments;
            foreach (DataGridViewRow row in dgvFulfillments.Rows)
            {
                row.Selected = row.Tag is Models.Fulfillment fulfillment && fulfillment.PurchaseOrderLineID == purchaseOrderLineReadOnly.PurchaseOrderLine.PurchaseOrderLineID;
            }
        }

        private void PurchaseOrderLineReadOnly_OnFulfillmentPlanLinkClicked(object sender, EventArgs e)
        {
            PurchaseOrderLineReadOnly purchaseOrderLineReadOnly = sender as PurchaseOrderLineReadOnly;
            if (purchaseOrderLineReadOnly == null)
            {
                return;
            }

            PurchaseOrderLine purchaseOrderLine = purchaseOrderLineReadOnly.PurchaseOrderLine;
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

            tabControl1.SelectedTab = tabFulfillmentPlans;
            foreach(DataGridViewRow row in dgvFulfillmentPlans.Rows)
            {
                row.Selected = false;
            }

            selectedRow.Selected = true;
        }

        private void dgvFulfillmentPlans_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewRow row = dgvFulfillmentPlans.SelectedRows.OfType<DataGridViewRow>().FirstOrDefault();
            if (row == null)
            {
                return;
            }

            FulfillmentPlan fulfillmentPlan = row.Tag as FulfillmentPlan;
            if (fulfillmentPlan == null)
            {
                return;
            }

            lnkFPRailcar.Text = fulfillmentPlan.Railcar?.ReportingID;
            lnkFPRailcar.Tag = fulfillmentPlan.Railcar;
            StringBuilder purchaseOrderLines = new StringBuilder();
            foreach(PurchaseOrderLine purchaseOrderLine in fulfillmentPlan.FulfillmentPlanPurchaseOrderLines.Select(fppol => fppol.PurchaseOrderLine))
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

        private void dgvInvoices_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvInvoices.Rows.Count || !PermissionsManager.HasPermission(LocationModel.LocationID.Value, PermissionsManager.LocationWidePermissions.ManageInvoices))
            {
                return;
            }

            DataGridViewRow row = dgvInvoices.Rows[e.RowIndex];
            Invoice invoice = row.Tag as Invoice;

            frmPayableInvoice payableInvoice = new frmPayableInvoice();
            Studio.DecorateStudioContent(payableInvoice);
            payableInvoice.Company = Company;
            payableInvoice.LocationModel = LocationModel;
            payableInvoice.InvoiceID = invoice.InvoiceID;
            payableInvoice.Show(Studio.dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }

        private async void cmdClose_Click(object sender, EventArgs e)
        {
            if (!this.Confirm("Are you sure you want to close this Purchase Order? It cannot be reopened."))
            {
                return;
            }

            PostData post = new PostData(DataAccess.APIs.CompanyStudio, "PurchaseOrder/Close", new { PurchaseOrderID });
            post.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            await post.ExecuteNoResult();

            if (post.RequestSuccessful)
            {
                Close();
            }
        }

        private async void toolSaveTemplate_Click(object sender, EventArgs e)
        {
            await PurchaseOrderTemplate.PromptAndSavePurchaseOrderAsTemplate(Company, LocationModel, Theme, PurchaseOrderID);
        }

        public Task Save()
        {
            return Task.CompletedTask;
        }
    }
}
