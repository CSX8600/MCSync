using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CompanyStudio.Extensions;
using CompanyStudio.Models;
using MesaSuite.Common;
using MesaSuite.Common.Attributes;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using MesaSuite.Common.Utility;

namespace CompanyStudio.Invoicing
{
    [UriReachable("arinvoice/{InvoiceID}")]
    public partial class frmReceivableInvoice : /*Form*/ BaseCompanyStudioContent, ILocationScoped, ISaveable
    {
        private List<Company> companyList;
        private ActionButtonActions actionButtonAction;

        public event EventHandler OnSave;

        public Location LocationModel { get; set; }

        public long? InvoiceID { get; set; }

        private decimal _initialAmount = 0M;
        private Invoice.Statuses _currentStatus = Invoice.Statuses.WorkInProgress;
        private long? _currentPurchaseOrderID = null;

        public frmReceivableInvoice()
        {
            InitializeComponent();
            colPOLine.ValueType = typeof(long?);
            colPOLine.ValueMember = nameof(PurchaseOrderLine.PurchaseOrderLineID);
            colPOLine.DisplayMember = nameof(PurchaseOrderLine.DisplayString);

            colFulfillment.ValueType = typeof(long?);
            colFulfillment.ValueMember = nameof(Fulfillment.FulfillmentID);
            colFulfillment.DisplayMember = nameof(Fulfillment.DisplayString);
        }

        private async void frmReceivableInvoice_Load(object sender, EventArgs e)
        {
            PermissionsManager.OnLocationPermissionChange += PermissionsManager_OnLocationPermissionChange;

            loader.BringToFront();
            loader.Visible = true;

            await LoadForm();

            loader.Visible = false;
        }

        private bool _loading = false;
        private async Task LoadForm()
        {
            try
            {
                _loading = true;
                dgvLines.Rows.Clear();

                GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Company/GetAll");
                get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                companyList = await get.GetObject<List<Company>>() ?? new List<Company>();

                get.Resource = $"Location/Get/{LocationModel.LocationID}";
                Location location = await get.GetObject<Location>() ?? new Location();

                txtPayee.Text = $"{Company.Name} - {LocationModel.Name}";
                txtInvoiceNumber.Text = location.InvoiceNumberPrefix + location.NextInvoiceNumber;

                if (!string.IsNullOrEmpty(txtInvoiceNumber.Text))
                {
                    Text = $"{txtInvoiceNumber.Text} [AR]";
                }

                get.Resource = "Government/GetAll";
                List<Government> governments = await get.GetObject<List<Government>>() ?? new List<Government>();

                get.Resource = "Account/GetAllForUser";
                List<Account> accounts = await get.GetObject<List<Account>>() ?? new List<Account>();

                cboCompany.Items.Clear();
                cboLocation.Items.Clear();
                foreach (Company company in companyList.OrderBy(c => c.Name))
                {
                    cboCompany.Items.Add(new DropDownItem<Company>(company, company.Name));
                }

                cboGovernment.Items.Clear();
                foreach (Government government in governments.OrderBy(g => g.Name))
                {
                    cboGovernment.Items.Add(new DropDownItem<Government>(government, government.Name));
                }

                Invoice invoice = null;
                if (InvoiceID.HasValue)
                {
                    get.Resource = "Invoice/Get/" + InvoiceID.Value;
                    invoice = await get.GetObject<Invoice>();
                }

                lblTitle.Text = "INVOICE - " + invoice?.Status.ToString().ToDisplayName() ?? Invoice.Statuses.WorkInProgress.ToString().ToDisplayName();

                cboAccount.Items.Clear();
                foreach (Account account in accounts)
                {
                    cboAccount.Items.Add(new DropDownItem<Account>(account, $"{account.AccountNumber} ({account.Description})"));
                }

                Dictionary<string, object> companyPreferences = UserPreferences.Get().GetPreferencesForSection("company");
                chkAutoReceive.Checked = companyPreferences.GetOrDefault("autoReceiveInvoice", true).Cast(true);
                cmdAction.Visible = false;
                if (invoice != null)
                {
                    _currentStatus = invoice.Status.Value;
                    _currentPurchaseOrderID = invoice.PurchaseOrderID;

                    rdoCompany.Checked = invoice.LocationIDTo != default;
                    rdoGovernment.Checked = invoice.GovernmentIDTo != default;

                    if (rdoCompany.Checked)
                    {
                        cboCompany.SelectedItem = cboCompany.Items.Cast<DropDownItem<Company>>().FirstOrDefault(ddi => ddi.Object.CompanyID == invoice.LocationTo.CompanyID);
                        cboLocation.SelectedItem = cboLocation.Items.Cast<DropDownItem<Location>>().FirstOrDefault(ddi => ddi.Object.LocationID == invoice.LocationIDTo);
                    }
                    else if (rdoGovernment.Checked)
                    {
                        cboGovernment.SelectedItem = cboGovernment.Items.Cast<DropDownItem<Government>>().FirstOrDefault(ddi => ddi.Object.GovernmentID == invoice.GovernmentIDTo);
                    }

                    await LoadPurchaseOrdersForEntity();

                    txtInvoiceNumber.Text = invoice.InvoiceNumber;
                    dtpInvoiceDate.Value = invoice.InvoiceDate;
                    dtpDueDate.Value = invoice.DueDate;
                    txtDescription.Text = invoice.Description;
                    if (invoice.Status == Invoice.Statuses.Complete)
                    {
                        cboAccount.Items.Insert(0, new DropDownItem<Account>(new Account(), invoice.AccountToHistorical));
                        cboAccount.SelectedIndex = 0;
                    }
                    else
                    {
                        cboAccount.SelectedItem = cboAccount.Items.Cast<DropDownItem<Account>>().FirstOrDefault(ddi => ddi.Object.AccountID == invoice.AccountIDTo);
                    }

                    chkAutoReceive.Checked = invoice.AutoReceive;

                    decimal invoiceTotal = 0M;
                    if (invoice.InvoiceLines != null)
                    {
                        foreach (InvoiceLine line in invoice.InvoiceLines)
                        {
                            int rowIndex = dgvLines.Rows.Add();
                            DataGridViewRow row = dgvLines.Rows[rowIndex];

                            row.Cells["colInvoiceLineID"].Value = line.InvoiceLineID?.ToString();
                            row.Cells[colItem.Name].Value = line.Item?.Name;
                            row.Cells[colItem.Name].Tag = line.Item?.ItemID;
                            row.Cells["colDescription"].Value = line.Description;
                            row.Cells["colQuantity"].Value = line.Quantity.ToString();
                            row.Cells["colUnitCost"].Value = line.UnitCost.ToString();
                            row.Cells["colTotal"].Value = line.Total.ToString();
                            row.Cells[colPOLine.Name].Value = line.PurchaseOrderLineID;
                            row.Cells[colFulfillment.Name].Value = line.Fulfillment?.FulfillmentID;
                            row.Tag = line;

                            invoiceTotal += line.Total;
                        }
                    }

                    txtTotal.Text = invoiceTotal.ToString("N2");
                    _initialAmount = invoiceTotal;

                    if (invoice.Status == Invoice.Statuses.Complete)
                    {
                        foreach (Control control in Controls)
                        {
                            control.Enabled = false;
                        }

                        chkAutoReceive.Visible = false;
                        cmdAction.Visible = false;
                        cmdSave.Visible = false;
                        cmdCancel.Visible = false;
                    }

                    if (invoice.Status == Invoice.Statuses.ReadyForReceipt)
                    {
                        actionButtonAction = ActionButtonActions.ReceivePayment;
                        cmdAction.Visible = true;
                        cmdAction.Text = "Receive Payment";
                    }
                    else if (invoice.Status == Invoice.Statuses.WorkInProgress)
                    {
                        actionButtonAction = ActionButtonActions.IssueInvoice;
                        cmdAction.Visible = true;
                        cmdAction.Text = "Issue Invoice";
                    }
                }
                else
                {
                    actionButtonAction = ActionButtonActions.IssueInvoice;
                    cmdAction.Visible = true;
                    cmdAction.Text = "Issue Invoice";
                }
            }
            finally
            {
                _loading = false;
            }
        }

        private void PermissionsManager_OnLocationPermissionChange(object sender, PermissionsManager.LocationWidePermissionChangeEventArgs e)
        {
            if (e.LocationID == LocationModel.LocationID && e.Permission == PermissionsManager.LocationWidePermissions.ManageInvoices && !e.Value)
            {
                IsDirty = false;
                Close();
            }
        }

        private void frmReceivableInvoice_FormClosed(object sender, FormClosedEventArgs e)
        {
            PermissionsManager.OnLocationPermissionChange -= PermissionsManager_OnLocationPermissionChange;
        }

        private void rdoCompany_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCompany.Checked)
            {
                cboGovernment.Enabled = false;

                cboCompany.Enabled = true;
                cboLocation.Enabled = true;
            }
        }

        private void rdoGovernment_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoGovernment.Checked)
            {
                cboCompany.Enabled = false;
                cboLocation.Enabled = false;
                cboGovernment.Enabled = true;
            }
        }

        private void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboLocation.Items.Clear();

            DropDownItem<Company> selectedItem = cboCompany.SelectedItem as DropDownItem<Company>;
            if (selectedItem == null)
            {
                return;
            }

            foreach(Location location in selectedItem.Object.Locations.OrderBy(l => l.Name))
            {
                cboLocation.Items.Add(new DropDownItem<Location>(location, location.Name));
            }
        }

        private void dgvLines_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            decimal runningTotal = 0M;

            foreach(DataGridViewRow row in dgvLines.Rows.OfType<DataGridViewRow>().Where(aRow => !aRow.IsNewRow))
            {
                string strQuantity = row.Cells["colQuantity"].Value as string;
                string strUnitCost = row.Cells["colUnitCost"].Value as string;

                decimal lineTotal;
                if (!decimal.TryParse(strQuantity, out decimal quantity) || !decimal.TryParse(strUnitCost, out decimal unitCost))
                {
                    lineTotal = 0;
                }
                else
                {
                    lineTotal = Math.Round(quantity * unitCost, 2, MidpointRounding.AwayFromZero);
                }
                runningTotal += lineTotal;

                row.Cells["colTotal"].Value = lineTotal.ToString("N2");
            }

            txtTotal.Text = runningTotal.ToString("N2");

            if (e.RowIndex >= 0 && e.RowIndex < dgvLines.Rows.Count)
            {
                if (e.ColumnIndex == colPOLine.Index)
                {
                    List<PurchaseOrderLine> purchaseOrderLines = colPOLine.DataSource as List<PurchaseOrderLine>;
                    if (purchaseOrderLines != null)
                    {
                        long? selectedPOLineID = dgvLines[colPOLine.Index, e.RowIndex].Value as long?;
                        dgvLines[colFulfillment.Index, e.RowIndex].Value = null;
                        List<Fulfillment> newFulfillmentSource = (colPOLine.DataSource as List<PurchaseOrderLine>)?.Single(pol => pol.PurchaseOrderLineID == selectedPOLineID).Fulfillments.ToList() ?? new List<Fulfillment>();
                        newFulfillmentSource.Insert(0, new Fulfillment());
                        ((DataGridViewComboBoxCell)dgvLines[colFulfillment.Index, e.RowIndex]).DataSource = newFulfillmentSource;
                        dgvLines.Refresh();
                    }
                }

                if (e.ColumnIndex == colFulfillment.Index)
                {
                    long? selectedFulfillmentID = dgvLines[colFulfillment.Index, e.RowIndex].Value as long?;
                    if (selectedFulfillmentID != null)
                    {
                        foreach(DataGridViewRow row in dgvLines.Rows.OfType<DataGridViewRow>().Where(aRow => !aRow.IsNewRow))
                        {
                            if (row.Index != e.RowIndex && row.Cells[colFulfillment.Index].Value as long? == selectedFulfillmentID)
                            {
                                row.Cells[colFulfillment.Index].Value = null;
                            }
                        }
                    }
                }
            }
        }

        private void txtInvoiceNumber_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtInvoiceNumber.Text))
            {
                Text = "Receivable Invoice";
            }
            else
            {
                Text = $"{txtInvoiceNumber.Text} [AR]";
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            Save();
            skipActionButton = false;
        }

        public async Task Save()
        {
            await InternalSave(false);
        }

        private bool skipActionButton = false;
        private async Task InternalSave(bool fromActionButton)
        {
            loader.BringToFront();
            loader.Visible = true;

            Invoice originalInvoice = null;
            if (InvoiceID != null)
            {
                GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Invoice/Get/" + InvoiceID);
                get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                originalInvoice = await get.GetObject<Invoice>();
            }

            Invoice invoiceToSave = new Invoice()
            {
                LocationIDFrom = LocationModel.LocationID,
                Status = Invoice.Statuses.WorkInProgress
            };

            if (InvoiceID != null)
            {
                invoiceToSave = new Invoice();
                invoiceToSave.InvoiceID = originalInvoice.InvoiceID;
                invoiceToSave.LocationIDFrom = originalInvoice.LocationIDFrom;
                invoiceToSave.Status = originalInvoice.Status;
                invoiceToSave.AccountIDFrom = originalInvoice.AccountIDFrom;
            }
            
            if ((invoiceToSave.Status == Invoice.Statuses.Sent || invoiceToSave.Status == Invoice.Statuses.ReadyForReceipt) && 
                decimal.TryParse(txtTotal.Text, out decimal newTotal) && 
                newTotal != _initialAmount)
            {
                skipActionButton = fromActionButton;
                if (!this.Confirm("Changing an Invoice's Total Amount after it has been sent will put the Invoice back into Work In Progress.\r\n\r\nDo you want to continue?"))
                {
                    loader.Visible = false;
                    return;
                }
            }

            invoiceToSave.LocationIDTo = null;
            invoiceToSave.GovernmentIDTo = null;
            if (rdoCompany.Checked)
            {
                invoiceToSave.LocationIDTo = cboLocation.SelectedItem.Cast<DropDownItem<Location>>()?.Object.LocationID;
            }

            if (rdoGovernment.Checked)
            {
                invoiceToSave.GovernmentIDTo = cboGovernment.SelectedItem.Cast<DropDownItem<Government>>()?.Object?.GovernmentID;
            }

            if (originalInvoice != null && (invoiceToSave.Status == Invoice.Statuses.Sent || invoiceToSave.Status == Invoice.Statuses.ReadyForReceipt))
            {
                if (invoiceToSave.LocationIDTo != originalInvoice.LocationIDTo || invoiceToSave.GovernmentIDTo != originalInvoice.GovernmentIDTo)
                {
                    skipActionButton = fromActionButton;
                    if (!this.Confirm("Changing an Invoice's Payor after it has been sent will put the Invoice back into Work In Progress.\r\n\r\nDo you want to continue?"))
                    {
                        loader.Visible = false;
                        return;
                    }
                }
            }

            invoiceToSave.PurchaseOrderID = cboPurchaseOrder.SelectedItem?.Cast<DropDownItem<PurchaseOrder>>()?.Object.PurchaseOrderID;
            invoiceToSave.InvoiceNumber = txtInvoiceNumber.Text;
            invoiceToSave.InvoiceDate = dtpInvoiceDate.Value;
            invoiceToSave.DueDate = dtpDueDate.Value;
            invoiceToSave.Description = txtDescription.Text;
            invoiceToSave.AccountIDTo = cboAccount.SelectedItem.Cast<DropDownItem<Account>>()?.Object.AccountID;
            invoiceToSave.AutoReceive = chkAutoReceive.Checked;

            bool saveSuccessful = false;
            if (originalInvoice == null)
            {
                PostData post = new PostData(DataAccess.APIs.CompanyStudio, "Invoice/Post", invoiceToSave);
                post.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                invoiceToSave = await post.Execute<Invoice>();
                saveSuccessful = post.RequestSuccessful;
            }
            else
            {
                PutData put = new PutData(DataAccess.APIs.CompanyStudio, "Invoice/Put", invoiceToSave);
                put.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                invoiceToSave = await put.Execute<Invoice>();
                saveSuccessful = put.RequestSuccessful;
            }

            if (saveSuccessful)
            {
                InvoiceID = invoiceToSave.InvoiceID;

                List<long?> previousLineIDs = invoiceToSave.InvoiceLines?.Select(il => il.InvoiceLineID).ToList() ?? new List<long?>();
                HashSet<long> handledLineIDs = new HashSet<long>();
                foreach (DataGridViewRow row in dgvLines.Rows.Cast<DataGridViewRow>().Where(dgv => !dgv.IsNewRow))
                {
                    string strLineID = row.Cells["colInvoiceLineID"].Value as string ?? "";
                    long? itemID = row.Cells[colItem.Name].Tag as long?;
                    string description = row.Cells["colDescription"].Value as string;
                    string strQuantity = row.Cells["colQuantity"].Value as string;
                    string strUnitCost = row.Cells["colUnitCost"].Value as string;

                    decimal.TryParse(strQuantity, out decimal quantity);
                    decimal.TryParse(strUnitCost, out decimal unitCost);

                    InvoiceLine invoiceLine = new InvoiceLine();
                    invoiceLine.InvoiceID = invoiceToSave.InvoiceID;
                    invoiceLine.Description = description;
                    invoiceLine.Quantity = quantity;
                    invoiceLine.UnitCost = unitCost;
                    invoiceLine.ItemID = itemID;
                    invoiceLine.PurchaseOrderLineID = row.Cells[colPOLine.Name].Value as long?;

                    if (!string.IsNullOrEmpty(strLineID) && long.TryParse(strLineID, out long lineID))
                    {
                        invoiceLine.InvoiceLineID = lineID;
                        handledLineIDs.Add(lineID);
                    }

                    bool lineSaveSuccessful = false;
                    if (invoiceLine.InvoiceLineID == null)
                    {
                        PostData post = new PostData(DataAccess.APIs.CompanyStudio, "InvoiceLine/Post", invoiceLine);
                        post.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                        invoiceLine = await post.Execute<InvoiceLine>();
                        saveSuccessful &= post.RequestSuccessful;
                        lineSaveSuccessful = post.RequestSuccessful;
                    }
                    else
                    {
                        PutData put = new PutData(DataAccess.APIs.CompanyStudio, "InvoiceLine/Put", invoiceLine);
                        put.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                        invoiceLine = await put.Execute<InvoiceLine>();
                        saveSuccessful &= put.RequestSuccessful;
                        lineSaveSuccessful = put.RequestSuccessful;
                    }

                    if (lineSaveSuccessful)
                    {
                        long? putInvoiceLineID = invoiceLine.InvoiceLineID;
                        long? putFulfillmentID = row.Cells[colFulfillment.Name].Value as long?;

                        if (putFulfillmentID == null)
                        {
                            InvoiceLine originalLine = row.Tag as InvoiceLine;
                            if (originalLine?.Fulfillment?.FulfillmentID == null) // No change
                            {
                                continue;
                            }

                            if (dgvLines.Rows.Cast<DataGridViewRow>().Any(r => r != row && (long?)r.Cells[colFulfillment.Name].Value == originalLine.Fulfillment.FulfillmentID))
                            {
                                // Looks like someone else will update this Fulfillment
                                continue;
                            }

                            putFulfillmentID = originalLine.Fulfillment.FulfillmentID;
                            putInvoiceLineID = null;
                        }

                        PostData post = new PostData(DataAccess.APIs.CompanyStudio, "Fulfillment/PutInvoiceLine", new { FulfillmentID = putFulfillmentID, InvoiceLineID = putInvoiceLineID });
                        post.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                        await post.ExecuteNoResult();
                        saveSuccessful &= post.RequestSuccessful;
                    }
                }

                if (invoiceToSave.InvoiceLines != null)
                {
                    foreach (long? deletedInvoiceLineID in invoiceToSave.InvoiceLines.Where(il => !handledLineIDs.Contains(il.InvoiceLineID ?? 0L)).Select(il => il.InvoiceLineID))
                    {
                        DeleteData delete = new DeleteData(DataAccess.APIs.CompanyStudio, $"InvoiceLine/Delete/{deletedInvoiceLineID}");
                        delete.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                        await delete.Execute();
                    }
                }
            }

            if (saveSuccessful)
            {
                UserPreferences preferences = UserPreferences.Get();
                Dictionary<string, object> companySettings = preferences.GetPreferencesForSection("company");
                companySettings["autoReceiveInvoice"] = chkAutoReceive.Checked;
                preferences.Save();

                IsDirty = false;
                OnSave?.Invoke(this, EventArgs.Empty);

                await LoadForm();
            }

            skipActionButton = fromActionButton && !saveSuccessful;

            loader.Visible = false;
        }

        private async void cmdAction_Click(object sender, EventArgs e)
        {
            await InternalSave(true);

            if (skipActionButton)
            {
                skipActionButton = false;
                return;
            }

            loader.BringToFront();
            loader.Visible = true;

            string destURL = actionButtonAction == ActionButtonActions.IssueInvoice ? "Invoice/Issue" : "Invoice/Receive";
            PutData put = new PutData(DataAccess.APIs.CompanyStudio, destURL, new Invoice() { InvoiceID = InvoiceID });
            put.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            Invoice returnedInvoice = await put.Execute<Invoice>();
            if (put.RequestSuccessful)
            {
                OnSave?.Invoke(this, EventArgs.Empty);
                InvoiceID = returnedInvoice.InvoiceID;
                await LoadForm();
            }

            loader.Visible = false;
        }

        private enum ActionButtonActions
        {
            ReceivePayment,
            IssueInvoice
        }

        private void dgvLines_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvLines.Rows.Count || e.ColumnIndex != colItem.Index || (dgvLines.Rows[e.RowIndex].IsNewRow && _currentStatus == Invoice.Statuses.Complete))
            {
                return;
            }

            if (dgvLines.Rows[e.RowIndex].IsNewRow)
            {
                int rowIndex = dgvLines.Rows.Add();
                dgvLines_CellClick(sender, new DataGridViewCellEventArgs(e.ColumnIndex, rowIndex));
            }

            long? itemID = dgvLines[e.ColumnIndex, e.RowIndex].Tag as long?;

            Rectangle cellRect = dgvLines.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);

            void HandleSelectorClosed(object s, EventArgs ea) {
                ItemSelector se = (ItemSelector)s;
                dgvLines[e.ColumnIndex, e.RowIndex].Value = se.SelectedItemText;
                dgvLines[e.ColumnIndex, e.RowIndex].Tag = se.SelectedItemID;
                Controls.Remove(se);
            };

            ItemSelector selector = new ItemSelector();
            selector.SelectedItemID = itemID;
            selector.Leave += HandleSelectorClosed;
            selector.ItemSelected += HandleSelectorClosed;
            selector.Location = PointToClient(dgvLines.PointToScreen(cellRect.Location));
            selector.ReadOnlyMode = InvoiceID != null && _currentStatus == Invoice.Statuses.Complete;
            Controls.Add(selector);
            selector.BringToFront();
            selector.Focus();
        }

        private void cboLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loading) return;

            LoadPurchaseOrdersForEntity();
        }

        private async Task LoadPurchaseOrdersForEntity()
        {
            cboPurchaseOrder.Items.Clear();
            cboPurchaseOrder.SelectedItem = null;

            if ((cboLocation.Enabled && cboLocation.SelectedItem != null && cboLocation.SelectedItem is DropDownItem<Location>) ||
                (cboGovernment.Enabled && cboGovernment.SelectedItem != null && cboGovernment.SelectedItem is DropDownItem<Government>))
            {
                GetData get = new GetData(DataAccess.APIs.CompanyStudio, "PurchaseOrder/GetAllRelatedToLocation");
                get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                List<PurchaseOrder> purchaseOrders = await get.GetObject<List<PurchaseOrder>>() ?? new List<PurchaseOrder>();

                foreach (PurchaseOrder po in purchaseOrders)
                {
                    cboPurchaseOrder.Items.Add(new DropDownItem<PurchaseOrder>(po, $"{po.PurchaseOrderID} (From {po.LocationOrigin?.Company.Name}{po.GovernmentOrigin?.Name})"));
                }
            }

            cboPurchaseOrder.SelectedItem = cboPurchaseOrder.Items.OfType<DropDownItem<PurchaseOrder>>().FirstOrDefault(p => p.Object.PurchaseOrderID == _currentPurchaseOrderID);
        }

        private void cboPurchaseOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach(DataGridViewRow row in dgvLines.Rows.OfType<DataGridViewRow>().Where(row => !row.IsNewRow))
            {
                row.Cells[colFulfillment.Name].Value = null;
                row.Cells[colPOLine.Name].Value = null;
            }

            colPOLine.DataSource = new List<PurchaseOrderLine>()
            {
                new PurchaseOrderLine()
                {
                    IsService = true
                }
            };
            colFulfillment.DataSource = new List<Fulfillment>();

            if (cboPurchaseOrder.SelectedItem is DropDownItem<PurchaseOrder> purchaseOrderItem)
            {
                List<PurchaseOrderLine> newPOLines = purchaseOrderItem.Object.PurchaseOrderLines.ToList();
                newPOLines.Insert(0, new PurchaseOrderLine() { IsService = true });
                colPOLine.DataSource = newPOLines;
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
