using CompanyStudio.Extensions;
using CompanyStudio.Models;
using MesaSuite.Common.Attributes;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using Microsoft.ReportingServices.Diagnostics.Internal;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyStudio.Purchasing.OpenMaintenance
{
    public partial class frmOpenPurchaseOrderReceiver : BaseCompanyStudioContent, ILocationScoped, ISaveable
    {
        public long? PurchaseOrderID { get; set; }
        public frmOpenPurchaseOrderReceiver()
        {
            InitializeComponent();
        }

        public Location LocationModel { get; set; }

        public event EventHandler OnSave;

        private async void frmOpenPurchaseOrderReceiver_Load(object sender, System.EventArgs e)
        {
            Studio.dockPanel.Contents.OfType<frmPurchaseOrderExplorer>().Where(poe => poe.LocationModel.LocationID == LocationModel.LocationID).FirstOrDefault()?.RegisterPurchaseOrderForm(this, () => PurchaseOrderID);
            await RefreshData();
        }

        private async Task RefreshData()
        {
            try
            {
                loader.BringToFront();
                loader.Visible = true;

                GetData get = new GetData(DataAccess.APIs.CompanyStudio, "PurchaseOrder/Get/" + PurchaseOrderID);
                get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                PurchaseOrder purchaseOrder = await get.GetObject<PurchaseOrder>();
                if (purchaseOrder == null)
                {
                    return;
                }

                Text = $"Open Purchase Order - {purchaseOrder.PurchaseOrderID}";
                lblPONumber.Text = Text;
                lblOrderFrom.Text = purchaseOrder.GovernmentIDOrigin != null ? purchaseOrder.GovernmentOrigin?.Name : $"{purchaseOrder.LocationOrigin?.Company?.Name} ({purchaseOrder.LocationOrigin?.Name})";
                lblOrderDate.Text = purchaseOrder.PurchaseOrderDate?.ToString("MM/dd/yyyy");
                lblDescription.Text = purchaseOrder.Description;

                pnlUnfulfilledLines.Controls.Clear();
                foreach (PurchaseOrderLine unfulfilledLine in (purchaseOrder.PurchaseOrderLines ?? new List<PurchaseOrderLine>()).Where(l => l.Quantity > (l.Fulfillments?.Sum(f => f.Quantity) ?? 0)))
                {
                    AddPurchaseOrderLine(unfulfilledLine);
                }

                dgvFulfillments.Rows.Clear();
                foreach (Models.Fulfillment fulfillment in (purchaseOrder.PurchaseOrderLines?.SelectMany(pol => pol.Fulfillments.Edit(f => f.PurchaseOrderLine = pol)) ?? new List<Models.Fulfillment>()).OrderByDescending(f => f.FulfillmentTime))
                {
                    AddFulfillment(fulfillment);
                }
                dgvFulfillments_SelectionChanged(dgvFulfillments, EventArgs.Empty);

                dgvInvoices.Rows.Clear();
                get = new GetData(DataAccess.APIs.CompanyStudio, "PurchaseOrder/GetInvoicesForPurchaseOrder/" + PurchaseOrderID);
                get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                List<Invoice> invoices = await get.GetObject<List<Invoice>>() ?? new List<Invoice>();
                foreach (Invoice invoice in invoices.OrderByDescending(i => i.InvoiceDate))
                {
                    AddInvoice(invoice);
                }

                get.Resource = "PurchaseOrderApproval/GetForPurchaseOrder/" + PurchaseOrderID;
                PurchaseOrderApproval purchaseOrderApproval = await get.GetObject<PurchaseOrderApproval>();
                if (purchaseOrderApproval == null)
                {
                    tsbAutoApproving.Visible = false;
                }
                else
                {
                    tsbAutoApproving.Visible = true;
                    tsbAutoApproving.Image = purchaseOrderApproval.FutureAutoApprove ? Properties.Resources.accept : Properties.Resources.cancel;
                    tsbEnableAutoApproval.Checked = purchaseOrderApproval.FutureAutoApprove;
                    tsbDisableAutoApproval.Checked = !purchaseOrderApproval.FutureAutoApprove;
                }
            }
            finally
            {
                loader.Visible = false;
            }
        }

        private void AddInvoice(Invoice invoice)
        {
            DataGridViewRow row = dgvInvoices.Rows[dgvInvoices.Rows.Add()];
            row.Cells[colInvoiceNumber.Name].Value = invoice.InvoiceNumber;
            row.Cells[colInvoiceDate.Name].Value = invoice.InvoiceDate.ToString("MM/dd/yyyy");
            row.Cells[colDueDate.Name].Value = invoice.DueDate.ToString("MM/dd/yyyy");
            if (invoice.DueDate < DateTime.Now)
            {
                row.Cells[colDueDate.Name].Style.BackColor = Color.Red;
                row.Cells[colDueDate.Name].Style.ForeColor = Color.White;
            }
            row.Cells[colAmount.Name].Value = invoice.Amount?.ToString("N2");
            row.Cells[colStatus.Name].Value = invoice.Status.ToString().ToDisplayName();
            row.Tag = invoice;
        }

        private void AddFulfillment(Models.Fulfillment fulfillment)
        {
            DataGridViewRow row = dgvFulfillments.Rows[dgvFulfillments.Rows.Add()];
            row.Cells[colFulfillmentTime.Name].Value = fulfillment.FulfillmentTime?.ToString("MM/dd/yyyy HH:mm");
            row.Cells[colRailcar.Name].Value = fulfillment.Railcar?.ReportingID;
            row.Cells[colFulfillmentItems.Name].Value = string.Format("{0}x {1}", fulfillment.Quantity, fulfillment.PurchaseOrderLine?.DisplayStringNoQuantity);
            row.Cells[colFulfillmentReceived.Name].Value = fulfillment.IsComplete;
            row.Cells[colIsInvoiced.Name].Value = fulfillment.InvoiceLineID != null;
            row.Tag = fulfillment;
        }

        private void AddPurchaseOrderLine(PurchaseOrderLine unfulfilledLine)
        {
            PurchaseOrderLineUnfulfilled purchaseOrderLineUnfulfilled = new PurchaseOrderLineUnfulfilled()
            {
                Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right,
                PurchaseOrderLine = unfulfilledLine
            };
            int top = 0;
            if (pnlUnfulfilledLines.Controls.OfType<PurchaseOrderLineUnfulfilled>().Any())
            {
                top = pnlUnfulfilledLines.Controls.OfType<PurchaseOrderLineUnfulfilled>().Max(c => c.Bottom);
            }
            purchaseOrderLineUnfulfilled.Top = top;
            purchaseOrderLineUnfulfilled.Width = pnlUnfulfilledLines.Width;
            pnlUnfulfilledLines.Controls.Add(purchaseOrderLineUnfulfilled);
        }

        private void mnuAddFulfilllmentWizard_ButtonClick(object sender, EventArgs e)
        {
            Fulfillment.FulfillmentWizardController fulfillmentWizard = new Fulfillment.FulfillmentWizardController(Company.CompanyID, LocationModel.LocationID);
            fulfillmentWizard.PurchaseOrderID = PurchaseOrderID;
            fulfillmentWizard.WizardCompleted += async (_, __) => { await RefreshData(); OnSave?.Invoke(this, EventArgs.Empty); };
            fulfillmentWizard.StartWizard();
        }

        public Task Save()
        {
            return Task.CompletedTask;
        }

        private async void toolManualFulfillmentEntry_Click(object sender, EventArgs e)
        {
            frmManualFulfillmentEntry manualEntry = new frmManualFulfillmentEntry()
            {
                CompanyID = Company.CompanyID,
                LocationID = LocationModel.LocationID,
                PurchaseOrderID = PurchaseOrderID,
                Theme = Theme
            };

            if (manualEntry.ShowDialog() == DialogResult.OK)
            {
                await RefreshData();
                OnSave?.Invoke(this, EventArgs.Empty);
            }
        }

        private void dgvFulfillments_SelectionChanged(object sender, EventArgs e)
        {
            IEnumerable<Models.Fulfillment> fulfillments = dgvFulfillments.SelectedRows.OfType<DataGridViewRow>().Select(dgvr => dgvr.Tag).OfType<Models.Fulfillment>();
            toolDeleteFulfillment.Enabled = fulfillments.Any(f => !f.IsComplete);
        }

        private async void toolDeleteFulfillment_Click(object sender, EventArgs e)
        {
            if (!dgvFulfillments.SelectedRows.OfType<DataGridViewRow>().Where(dgvr => dgvr.Tag is Models.Fulfillment).Any() ||
                !this.Confirm("Are you sure you want to delete these Fulfillment(s)?"))
            {
                return;
            }

            List<Models.Fulfillment> fulfillmentsToDelete = dgvFulfillments.SelectedRows.OfType<DataGridViewRow>().Select(dgvr => dgvr.Tag).OfType<Models.Fulfillment>().ToList();
            foreach (Models.Fulfillment fulfillment in fulfillmentsToDelete)
            {
                DeleteData delete = new DeleteData(DataAccess.APIs.CompanyStudio, "Fulfillment/Delete/" + fulfillment.FulfillmentID);
                delete.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                await delete.Execute();
            }

            await RefreshData();
        }

        private async void dgvInvoices_CellMouseDoubleClickAsync(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvInvoices.Rows.Count || !(dgvInvoices.Rows[e.RowIndex].Tag is Invoice invoice) || !PermissionsManager.HasPermission(LocationModel.LocationID.Value, PermissionsManager.LocationWidePermissions.ManageInvoices))
            {
                return;
            }

            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Invoice/Get/" + invoice.InvoiceID);
            get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            invoice = await get.GetObject<Invoice>();

            Invoicing.frmReceivableInvoice receivableInvoice = new Invoicing.frmReceivableInvoice();
            Studio.DecorateStudioContent(receivableInvoice);
            receivableInvoice.Company = Company;
            receivableInvoice.LocationModel = LocationModel;
            receivableInvoice.InvoiceID = invoice.InvoiceID;
            receivableInvoice.Show(Studio.dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }

        private async void tsbClose_Click(object sender, EventArgs e)
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
    }
}
