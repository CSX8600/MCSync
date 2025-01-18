using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CompanyStudio.Extensions;
using CompanyStudio.Models;
using MesaSuite.Common.Attributes;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using MesaSuite.Common.Utility;

namespace CompanyStudio.Invoicing
{
    [UriReachable("apinvoice/{InvoiceID}")]
    public partial class frmPayableInvoice : /*Form*/ BaseCompanyStudioContent, ILocationScoped, ISaveable
    {
        public frmPayableInvoice()
        {
            InitializeComponent();
        }

        public long? InvoiceID { get; set; }

        public Location LocationModel { get; set; }

        public event EventHandler OnSave;

        public async Task Save()
        {
            await InternalSave(false);
        }

        private async Task InternalSave(bool fromAuthorizeButton)
        {
            loader.BringToFront();
            loader.Visible = true;

            long? accountID = cboAccount.SelectedItem.Cast<DropDownItem<Account>>()?.Object.AccountID;

            PatchData patch = new PatchData(DataAccess.APIs.CompanyStudio, "Invoice/Patch", PatchData.PatchMethods.Replace, InvoiceID, new Dictionary<string, object>()
            {
                { "AccountIDFrom", accountID }
            });
            patch.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            await patch.Execute();

            if (patch.RequestSuccessful)
            {
                IsDirty = false;
                OnSave?.Invoke(this, EventArgs.Empty);

                GetData getUpdatedinvoice = new GetData(DataAccess.APIs.CompanyStudio, $"Invoice/Get/{InvoiceID}");
                getUpdatedinvoice.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                InvoiceID = (await getUpdatedinvoice.GetObject<Invoice>())?.InvoiceID ?? InvoiceID;
                if (!getUpdatedinvoice.RequestSuccessful)
                {
                    Close();
                }

                await LoadForm();
            }
            else
            {
                _skipAuthorize = fromAuthorizeButton;
            }

            loader.Visible = false;
        }

        private async void frmPayableInvoice_Load(object sender, EventArgs e)
        {
            if (InvoiceID == null)
            {
                this.ShowError("Payable Invoices cannot be created");
                _closeOnShown = true;
                return;
            }

            PermissionsManager.OnLocationPermissionChange += PermissionsManager_OnLocationPermissionChange;

            await LoadForm();
        }

        private async Task LoadForm()
        {
            loader.BringToFront();
            loader.Visible = true;

            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Invoice/Get/" + InvoiceID);
            get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            Invoice invoice = await get.GetObject<Invoice>();
            if (invoice == null)
            {
                Close();
                return;
            }

            Text = invoice.InvoiceNumber + " [AP]";

            txtPayee.Text = $"{invoice.LocationFrom?.Company?.Name ?? invoice.GovernmentFrom?.Name} (Government)";
            txtPayor.Text = $"{invoice.LocationTo.Company.Name} ({invoice.LocationTo.Name})";
            lnkPurchaseOrder.Text = invoice.PurchaseOrderID?.ToString() ?? "[None]";
            lnkPurchaseOrder.Tag = invoice.PurchaseOrderID;
            txtInvoiceNumber.Text = invoice.InvoiceNumber;
            dtpInvoiceDate.Value = invoice.InvoiceDate;
            dtpDueDate.Value = invoice.DueDate;
            txtDescription.Text = invoice.Description;

            dgvLineItems.Rows.Clear();
            decimal subTotal = 0M;
            foreach(InvoiceLine invoiceLine in invoice.InvoiceLines)
            {
                int rowIndex = dgvLineItems.Rows.Add();
                DataGridViewRow row = dgvLineItems.Rows[rowIndex];
                row.Cells[colDescription.Name].Value = invoiceLine.Description;
                row.Cells[colQuantity.Name].Value = invoiceLine.Quantity;
                row.Cells[colUnitCost.Name].Value = invoiceLine.UnitCost.ToString("N2");
                row.Cells[colLineTotal.Name].Value = invoiceLine.Total.ToString("N2");
                row.Cells[colItem.Name].Value = invoiceLine.Item?.Name;
                row.Cells[colItem.Name].Tag = invoiceLine.Item?.ItemID;
                row.Cells[colPOLine.Name].Value = invoiceLine.PurchaseOrderLine?.DisplayString;
                row.Cells[colFulfillment.Name].Value = invoiceLine.Fulfillment?.DisplayString;
                subTotal += invoiceLine.Total;
            }

            txtSubtotal.Text = subTotal.ToString("N2");

            dgvTaxes.Rows.Clear();
            decimal taxTotal = 0M;
            if (invoice.Status == Invoice.Statuses.Complete)
            {
                grpTaxes.Text = "Taxes Paid";
                foreach(InvoiceSalesTax salesTax in invoice.InvoiceSalesTaxes)
                {
                    int rowIndex = dgvTaxes.Rows.Add();
                    DataGridViewRow row = dgvTaxes.Rows[rowIndex];
                    row.Cells[colMunicipality.Name].Value = salesTax.Municipality;
                    row.Cells[colRate.Name].Value = salesTax.Rate.ToString("N2");
                    row.Cells[colAppliedAmount.Name].Value = salesTax.AppliedAmount.ToString("N2");
                    taxTotal += salesTax.AppliedAmount;
                }
            }
            else if (invoice.LocationIDFrom != null)
            {
                grpTaxes.Text = "Estimated Taxes";
                GetData getEffectiveTaxes = new GetData(DataAccess.APIs.CompanyStudio, $"SalesTax/GetEffectiveSalesTaxForLocation/{invoice.LocationIDFrom}");
                getEffectiveTaxes.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                List<SalesTax> effectiveTaxes = await getEffectiveTaxes.GetObject<List<SalesTax>>() ?? new List<SalesTax>();
                foreach(SalesTax salesTax in effectiveTaxes)
                {
                    int rowIndex = dgvTaxes.Rows.Add();
                    DataGridViewRow row = dgvTaxes.Rows[rowIndex];
                    row.Cells[colMunicipality.Name].Value = salesTax.Government?.Name;
                    row.Cells[colRate.Name].Value = salesTax.Rate.ToString("N2");
                    decimal taxAmount = Math.Round(salesTax.Rate / 100M * subTotal, 2, MidpointRounding.AwayFromZero);
                    row.Cells[colAppliedAmount.Name].Value = taxAmount.ToString("N2");
                    taxTotal += taxAmount;
                }
            }

            txtTaxTotal.Text = taxTotal.ToString("N2");

            txtInvoiceTotal.Text = (subTotal + taxTotal).ToString("N2");

            cboAccount.Items.Clear();

            if (invoice.Status == Invoice.Statuses.Complete)
            {
                cboAccount.Items.Add(new DropDownItem<Account>(new Account(), invoice.AccountFromHistorical));
                cboAccount.SelectedIndex = 0;
            }
            else
            {
                GetData accountGetter = new GetData(DataAccess.APIs.CompanyStudio, "Account/GetAllForUser");
                accountGetter.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                List<Account> accounts = await accountGetter.GetObject<List<Account>>() ?? new List<Account>();
                foreach (Account account in accounts)
                {
                    cboAccount.Items.Add(new DropDownItem<Account>(account, $"{account.Description} ({account.AccountNumber})"));
                }

                cboAccount.SelectedItem = cboAccount.Items.OfType<DropDownItem<Account>>().FirstOrDefault(ddi => ddi.Object.AccountID == invoice.AccountIDFrom);
            }

            cmdAuthorize.Visible = invoice.Status == Invoice.Statuses.Sent;
            cmdSave.Visible = invoice.Status == Invoice.Statuses.Sent;
            cmdCancel.Visible = invoice.Status == Invoice.Statuses.Sent;
            cboAccount.Enabled = invoice.Status == Invoice.Statuses.Sent;

            loader.Visible = false;
        }

        private void PermissionsManager_OnLocationPermissionChange(object sender, PermissionsManager.LocationWidePermissionChangeEventArgs e)
        {
            if (e.LocationID == LocationModel.LocationID && e.Permission == PermissionsManager.LocationWidePermissions.ManageInvoices && !e.Value)
            {
                IsDirty = false;
                Close();
            }
        }

        private bool _closeOnShown = false;
        private void frmPayableInvoice_Shown(object sender, EventArgs e)
        {
            if (_closeOnShown)
            {
                Close();
            }
        }

        private void frmPayableInvoice_FormClosed(object sender, FormClosedEventArgs e)
        {
            PermissionsManager.OnLocationPermissionChange -= PermissionsManager_OnLocationPermissionChange;
        }

        private async void cmdSave_Click(object sender, EventArgs e)
        {
            await InternalSave(false);
        }

        private bool _skipAuthorize = false;
        private async void cmdAuthorize_Click(object sender, EventArgs e)
        {
            await InternalSave(true);

            if (_skipAuthorize)
            {
                _skipAuthorize = false;
                return;
            }

            DropDownItem<Account> account = cboAccount.SelectedItem.Cast<DropDownItem<Account>>();
            if (account == null)
            {
                this.ShowError("Account is required to Authorize Payment");
                return;
            }

            if (decimal.TryParse(txtInvoiceTotal.Text, out decimal total) && total > account.Object.Balance && !this.Confirm($"You do not currently have enough funds to pay this Invoice. The Payee may contact you if they try to receive the payment.\r\n\r\nInvoice Total: {total.ToString("N2")}\r\nAccount Balance: {account.Object.Balance.ToString("N2")}\r\n\r\nDo you wish to continue?"))
            {
                return;
            }

            loader.BringToFront();
            loader.Visible = true;

            PutData authorize = new PutData(DataAccess.APIs.CompanyStudio, $"Invoice/AuthorizePayment/{InvoiceID}", new { placeholder = "placeholder" });
            authorize.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            await authorize.ExecuteNoResult();
            if (authorize.RequestSuccessful)
            {
                IsDirty = false;
                OnSave?.Invoke(this, EventArgs.Empty);

                GetData getUpdatedinvoice = new GetData(DataAccess.APIs.CompanyStudio, $"Invoice/Get/{InvoiceID}");
                getUpdatedinvoice.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                InvoiceID = (await getUpdatedinvoice.GetObject<Invoice>())?.InvoiceID ?? InvoiceID;
                if (!getUpdatedinvoice.RequestSuccessful)
                {
                    Close();
                }

                await LoadForm();
            }

            loader.Visible = false;
        }

        private void dgvLineItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvLineItems.Rows.Count || e.ColumnIndex != colItem.Index)
            {
                return;
            }

            long? itemID = dgvLineItems[e.ColumnIndex, e.RowIndex].Tag as long?;

            Rectangle cellLocation = dgvLineItems.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

            ItemSelector shownSelector = new ItemSelector();
            shownSelector.ReadOnlyMode = true;
            shownSelector.SelectedItemID = itemID;
            shownSelector.Location = PointToClient(dgvLineItems.PointToScreen(cellLocation.Location));
            shownSelector.Leave += (s, ea) => { Controls.Remove(shownSelector); shownSelector = null; };
            Controls.Add(shownSelector);
            shownSelector.BringToFront();
            shownSelector.Focus();
        }

        private async void lnkPurchaseOrder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!(lnkPurchaseOrder.Tag is long?) || ((long?)lnkPurchaseOrder.Tag) == null || !PermissionsManager.HasPermission(LocationModel.LocationID.Value, PermissionsManager.LocationWidePermissions.ManagePurchaseOrders))
            {
                return;
            }

            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "PurchaseOrder/Get/" + (long?)lnkPurchaseOrder.Tag);
            get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            PurchaseOrder purchaseOrder = await get.GetObject<PurchaseOrder>();

            if (purchaseOrder != null)
            {
                PurchaseOrder.OpenPurchaseOrderForm(this, purchaseOrder);
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
