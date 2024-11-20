﻿using CompanyStudio.Extensions;
using CompanyStudio.Models;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using Microsoft.ReportingServices.Diagnostics.Internal;
using System;
using System.Collections.Generic;
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

                get = new GetData(DataAccess.APIs.CompanyStudio, "PurchaseOrder/GetInvoicesForPurchaseOrder/" + PurchaseOrderID);
                get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                List<Invoice> invoices = await get.GetObject<List<Invoice>>() ?? new List<Invoice>();
                foreach (Invoice invoice in invoices.OrderByDescending(i => i.InvoiceDate))
                {
                    AddInvoice(invoice);
                }
            }
            finally
            {
                loader.Visible = false;
            }
        }

        private void AddInvoice(Invoice invoice)
        {
            
        }

        private void AddFulfillment(Models.Fulfillment fulfillment)
        {
            DataGridViewRow row = dgvFulfillments.Rows[dgvFulfillments.Rows.Add()];
            row.Cells[colFulfillmentTime.Name].Value = fulfillment.FulfillmentTime?.ToString("MM/dd/yyyy HH:mm");
            row.Cells[colRailcar.Name].Value = fulfillment.Railcar?.ReportingID;
            row.Cells[colFulfillmentItems.Name].Value = string.Format("{0}x {1}", fulfillment.Quantity, fulfillment.PurchaseOrderLine?.DisplayStringNoQuantity);
            row.Cells[colFulfillmentReceived.Name].Value = fulfillment.IsComplete;
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
    }
}