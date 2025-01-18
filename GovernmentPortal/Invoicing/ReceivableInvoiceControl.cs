using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GovernmentPortal.Extensions;
using GovernmentPortal.Models;
using MesaSuite.Common;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using MesaSuite.Common.Utility;

namespace GovernmentPortal.Invoicing
{
    public partial class ReceivableInvoiceControl : UserControl, IExplorerControl<Invoice>
    {
        public event EventHandler IsDirtyChanged;
        private long _governmentID;

        public ReceivableInvoiceControl()
        {
            InitializeComponent();
        }

        public ReceivableInvoiceControl(long governmentID) : this()
        {
            _governmentID = governmentID;
        }

        private bool _isDirty;
        public bool IsDirty
        {
            get => _isDirty;
            set
            {
                _isDirty = value;
                IsDirtyChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public Invoice Model { get; set; }
        private frmGenericExplorer<Invoice> _explorer;
        public frmGenericExplorer<Invoice> Explorer { set => _explorer = value; }

        public async void OnDeleteClicked()
        {
            if (Model == null)
            {
                this.ShowError("Cannot delete an unsaved Invoice");
                return;
            }

            if (Model.Status == Invoice.Statuses.Complete)
            {
                this.ShowError("Cannot delete a completed Invoice");
                return;
            }

            if (!this.Confirm("Are you sure you want to delete this Invoice?"))
            {
                return;
            }

            loader.Visible = true;
            loader.BringToFront();

            DeleteData delete = new DeleteData(DataAccess.APIs.GovernmentPortal, $"Invoice/Delete/{Model.InvoiceID}");
            delete.AddGovHeader(_governmentID);
            await delete.Execute();

            if (delete.RequestSuccessful)
            {
                Dispose();
                IsDirty = false;
                _explorer.LoadAllItems(true);
                return;
            }

            loader.Visible = false;
        }

        public async void OnSaveClicked()
        {
            if (Model?.Status == Invoice.Statuses.Complete)
            {
                this.ShowError("Cannot save a completed Invoice.");
                return;
            }

            loader.BringToFront();
            loader.Visible = true;

            if (!await InternalSave())
            {
                loader.Visible = false;
                return;
            }

            Dispose();
            IsDirty = false;
            _explorer.LoadAllItems(true, ReceivableInvoiceContext.GetItemDisplayText(txtInvoiceNumber.Text, Model?.Status ?? Invoice.Statuses.WorkInProgress));

            loader.Visible = false;
        }

        private async Task<bool> InternalSave()
        {
            Invoice invoiceToSave = new Invoice();
            if (Model != null)
            {
                invoiceToSave.InvoiceID = Model.InvoiceID;
                invoiceToSave.GovernmentIDFrom = Model.GovernmentIDFrom;
                invoiceToSave.Status = Model.Status;
                invoiceToSave.AccountIDFrom = Model.AccountIDFrom;
                invoiceToSave.AccountFromHistorical = Model.AccountFromHistorical;
            }
            else
            {
                invoiceToSave.GovernmentIDFrom = _governmentID;
                invoiceToSave.Status = Invoice.Statuses.WorkInProgress;
            }

            if (rdoCompany.Checked)
            {
                invoiceToSave.LocationIDTo = cboLocation.SelectedItem.Cast<DropDownItem<Location>>()?.Object.LocationID;
            }
            else if (rdoGovernment.Checked)
            {
                invoiceToSave.GovernmentIDTo = cboGovernment.SelectedItem.Cast<DropDownItem<Government>>()?.Object.GovernmentID;
            }

            if (Model != null && (Model.Status == Invoice.Statuses.Sent || Model.Status == Invoice.Statuses.ReadyForReceipt))
            {
                if ((Model.LocationIDTo != null && Model.LocationIDTo != invoiceToSave.LocationIDTo) || (Model.GovernmentIDTo != null && Model.GovernmentIDTo != invoiceToSave.GovernmentIDTo))
                {
                    if (!this.Confirm("Changing Location/Government after an Invoice has already been sent will set the status back to Work In Progress.\r\n\r\nDo you want to continue?"))
                    {
                        return false;
                    }
                }
            }

            invoiceToSave.InvoiceNumber = txtInvoiceNumber.Text;
            invoiceToSave.InvoiceDate = dtpInvoiceDate.Value;
            invoiceToSave.DueDate = dtpDueDate.Value;
            invoiceToSave.Description = txtDescription.Text;
            if (invoiceToSave.Status == Invoice.Statuses.Complete)
            {
                invoiceToSave.AccountToHistorical = cboReceivingAccount.SelectedItem.Cast<DropDownItem<Account>>()?.Text;
            }
            else
            {
                invoiceToSave.AccountIDTo = cboReceivingAccount.SelectedItem.Cast<DropDownItem<Account>>()?.Object.AccountID;
            }

            invoiceToSave.AutoReceive = chkAutoReceive.Checked;

            long invoiceID;
            if (Model == null)
            {
                PostData post = new PostData(DataAccess.APIs.GovernmentPortal, "Invoice/Post", invoiceToSave);
                post.AddGovHeader(_governmentID);
                Invoice savedInvoice = await post.Execute<Invoice>();
                if (!post.RequestSuccessful)
                {
                    loader.Visible = false;
                    return false;
                }

                invoiceID = savedInvoice.InvoiceID.Value;
            }
            else
            {
                PutData put = new PutData(DataAccess.APIs.GovernmentPortal, "Invoice/Put", invoiceToSave);
                put.AddGovHeader(_governmentID);
                await put.ExecuteNoResult();
                if (!put.RequestSuccessful)
                {
                    loader.Visible = false;
                    return false;
                }

                invoiceID = invoiceToSave.InvoiceID.Value;
            }

            GetData getUpdatedInvoice = new GetData(DataAccess.APIs.GovernmentPortal, $"Invoice/Get/{invoiceID}");
            getUpdatedInvoice.AddGovHeader(_governmentID);
            Model = await getUpdatedInvoice.GetObject<Invoice>();

            bool saveSuccessful = true;
            HashSet<long> handledLineIDs = new HashSet<long>();
            foreach (DataGridViewRow row in dgvLines.Rows.OfType<DataGridViewRow>().Where(dgvr => !dgvr.IsNewRow))
            {
                string strLineID = row.Cells[colInvoiceLineID.Name].Value as string;

                InvoiceLine line = new InvoiceLine();
                if (!string.IsNullOrEmpty(strLineID) && long.TryParse(strLineID, out long lineID))
                {
                    line.InvoiceLineID = lineID;
                    handledLineIDs.Add(lineID);
                }

                line.InvoiceID = invoiceID;
                line.ItemID = row.Cells[colItem.Name].Tag as long?;
                line.Description = row.Cells[colDescription.Name].Value as string;
                line.Quantity = row.Cells[colQuantity.Name].Value as decimal? ?? 0M;
                line.UnitCost = row.Cells[colUnitCost.Name].Value as decimal? ?? 0M;
                line.Total = row.Cells[colTotal.Name].Value as decimal? ?? 0M;

                if (line.InvoiceLineID == default)
                {
                    PostData post = new PostData(DataAccess.APIs.GovernmentPortal, "InvoiceLine/Post", line);
                    post.AddGovHeader(_governmentID);
                    await post.ExecuteNoResult();
                    saveSuccessful &= post.RequestSuccessful;
                }
                else
                {
                    PutData put = new PutData(DataAccess.APIs.GovernmentPortal, "InvoiceLine/Put", line);
                    put.AddGovHeader(_governmentID);
                    await put.ExecuteNoResult();
                    saveSuccessful &= put.RequestSuccessful;
                }
            }

            if (Model != null)
            {
                foreach (InvoiceLine lineToDelete in Model.InvoiceLines.Where(il => !handledLineIDs.Contains(il.InvoiceLineID.Value)))
                {
                    DeleteData delete = new DeleteData(DataAccess.APIs.GovernmentPortal, $"InvoiceLine/Delete/{lineToDelete.InvoiceLineID.Value}");
                    delete.AddGovHeader(_governmentID);
                    await delete.Execute();
                    saveSuccessful &= delete.RequestSuccessful;
                }
            }

            if (saveSuccessful)
            {
                UserPreferences userPreferences = UserPreferences.Get();
                Dictionary<string, object> govPreferences = userPreferences.GetPreferencesForSection("gov");
                govPreferences["autoReceiveInvoice"] = chkAutoReceive.Checked;
                userPreferences.Save();
            }

            return saveSuccessful;
        }

        private async void ReceivableInvoiceControl_Load(object sender, EventArgs e)
        {
            try
            {
                loader.BringToFront();
                loader.Visible = true;

                GetData get = new GetData(DataAccess.APIs.GovernmentPortal, $"Government/Get/{_governmentID}");
                get.AddGovHeader(_governmentID);
                get.RequestFields = new List<string>()
                {
                    nameof(Government.Name),
                    nameof(Government.InvoiceNumberPrefix),
                    nameof(Government.NextInvoiceNumber)
                };
                Government currentGovernment = await get.GetObject<Government>() ?? new Government();
                get.RequestFields.Clear();

                txtPayee.Text = currentGovernment.Name;

                get.Resource = "Company/GetAll";
                List<Company> companies = await get.GetObject<List<Company>>() ?? new List<Company>();

                foreach (Company company in companies.OrderBy(c => c.Name))
                {
                    cboCompany.Items.Add(new DropDownItem<Company>(company, company.Name));
                }

                get.Resource = "Government/GetAll";
                List<Government> governments = await get.GetObject<List<Government>>() ?? new List<Government>();
                foreach (Government government in governments.OrderBy(g => g.Name))
                {
                    cboGovernment.Items.Add(new DropDownItem<Government>(government, government.Name));
                }

                txtInvoiceNumber.Text = $"{currentGovernment.InvoiceNumberPrefix}{currentGovernment.NextInvoiceNumber}";

                colQuantity.ValueType = typeof(decimal);
                colUnitCost.ValueType = typeof(decimal);
                colTotal.ValueType = typeof(decimal);

                cboReceivingAccount.Items.Clear();
                get.Resource = "Account/MyAccounts";
                List<Account> accounts = await get.GetObject<List<Account>>() ?? new List<Account>();
                foreach (Account account in accounts)
                {
                    cboReceivingAccount.Items.Add(new DropDownItem<Account>(account, $"{account.Description} ({account.AccountNumber})"));
                }

                lblStatus.Text = Model?.Status.ToString().ToDisplayName() ?? Invoice.Statuses.WorkInProgress.ToString().ToDisplayName();
                chkAutoReceive.Checked = UserPreferences.Get().GetPreferencesForSection("gov").GetOrDefault("autoReceiveInvoice", true).Cast(true);

                if (Model != null)
                {
                    if (Model.LocationIDTo != null)
                    {
                        rdoGovernment.Checked = false;
                        rdoCompany.Checked = true;
                        cboLocation.Items.Clear();
                        cboCompany.Enabled = true;
                        cboLocation.Enabled = true;
                        cboGovernment.Enabled = false;

                        DropDownItem<Company> selectedCompany = cboCompany.Items.OfType<DropDownItem<Company>>().FirstOrDefault(ddi => ddi.Object.Locations.Any(loc => loc.LocationID == Model.LocationIDTo));
                        cboCompany.SelectedItem = selectedCompany;

                        if (selectedCompany != null)
                        {
                            foreach (Location location in selectedCompany.Object.Locations)
                            {
                                DropDownItem<Location> locationDDI = new DropDownItem<Location>(location, location.Name);
                                cboLocation.Items.Add(locationDDI);

                                if (location.LocationID == Model.LocationIDTo)
                                {
                                    cboLocation.SelectedItem = locationDDI;
                                }
                            }
                        }
                    }
                    else if (Model.GovernmentIDTo != null)
                    {
                        rdoGovernment.Checked = true;
                        rdoCompany.Checked = false;
                        cboCompany.Enabled = false;
                        cboLocation.Enabled = false;
                        cboGovernment.Enabled = true;

                        DropDownItem<Government> selectedGovernment = cboGovernment.Items.OfType<DropDownItem<Government>>().FirstOrDefault(ddi => ddi.Object.GovernmentID == Model.GovernmentIDTo);
                        cboGovernment.SelectedItem = selectedGovernment;
                    }

                    txtInvoiceNumber.Text = Model.InvoiceNumber;
                    dtpInvoiceDate.Value = Model.InvoiceDate;
                    dtpDueDate.Value = Model.DueDate;
                    txtDescription.Text = Model.Description;
                    chkAutoReceive.Checked = Model.AutoReceive;

                    decimal invoiceTotal = 0M;
                    foreach (InvoiceLine invoiceLine in Model.InvoiceLines)
                    {
                        int rowIndex = dgvLines.Rows.Add();
                        DataGridViewRow row = dgvLines.Rows[rowIndex];
                        row.Cells[colInvoiceLineID.Name].Value = invoiceLine.InvoiceLineID.ToString();
                        row.Cells[colItem.Name].Value = invoiceLine.Item?.Name;
                        row.Cells[colItem.Name].Tag = invoiceLine.Item?.ItemID;
                        row.Cells[colDescription.Name].Value = invoiceLine.Description;
                        row.Cells[colQuantity.Name].Value = invoiceLine.Quantity;
                        row.Cells[colUnitCost.Name].Value = invoiceLine.UnitCost;
                        row.Cells[colTotal.Name].Value = invoiceLine.Total;
                        invoiceTotal += invoiceLine.Total;
                    }
                    txtInvoiceTotal.Text = invoiceTotal.ToString("N2");

                    if (Model.Status == Invoice.Statuses.Complete)
                    {
                        DropDownItem<Account> historicalAccount = new DropDownItem<Account>(new Account(), Model.AccountToHistorical);
                        cboReceivingAccount.Items.Add(historicalAccount);
                        cboReceivingAccount.SelectedItem = historicalAccount;
                    }
                    else
                    {
                        DropDownItem<Account> receivingAccount = cboReceivingAccount.Items.Cast<DropDownItem<Account>>().FirstOrDefault(acc => acc.Object.AccountID == Model.AccountIDTo);
                        cboReceivingAccount.SelectedItem = receivingAccount;
                    }

                    if (Model.Status == Invoice.Statuses.Complete)
                    {
                        foreach (Control control in tabPage1.Controls.OfType<Control>().Concat(tabPage2.Controls.OfType<Control>()).Concat(tabPage3.Controls.OfType<Control>()).Concat(tabPage4.Controls.OfType<Control>()))
                        {
                            control.Enabled = false;
                        }
                    }
                }

                if (Model == null || Model.Status == Invoice.Statuses.WorkInProgress)
                {
                    cmdActionButton.Visible = true;
                    cmdActionButton.Text = "Issue Invoice";
                }
                else if (Model != null && Model.Status == Invoice.Statuses.ReadyForReceipt)
                {
                    cmdActionButton.Visible = true;
                    cmdActionButton.Text = "Receive Invoice";
                }
                else if (Model != null && Model.Status == Invoice.Statuses.Complete)
                {
                    chkAutoReceive.Visible = false;
                    cmdActionButton.Visible = false;
                }

                IsDirty = false;

                loader.Visible = false;
            }
            catch(Exception ex)
            {
                if (!IsDisposed)
                {
                    throw ex;
                }
            }
        }

        private void DestinationCheckedChanged(object sender, EventArgs e)
        {
            cboCompany.Enabled = rdoCompany.Checked;
            cboLocation.Enabled = rdoCompany.Checked;
            cboGovernment.Enabled = rdoGovernment.Checked;

            IsDirty = true;
        }

        private void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboLocation.Items.Clear();

            DropDownItem<Company> selectedItem = cboCompany.SelectedItem.Cast<DropDownItem<Company>>();
            if (selectedItem == null)
            {
                return;
            }

            foreach(Location location in selectedItem.Object.Locations.OrderBy(l => l.Name))
            {
                cboLocation.Items.Add(new DropDownItem<Location>(location, location.Name));
            }

            IsDirty = true;
        }

        private void dgvLines_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == colQuantity.Index || e.ColumnIndex == colUnitCost.Index)
            {
                dgvLines[colTotal.Index, e.RowIndex].Value = Math.Round((dgvLines[colQuantity.Index, e.RowIndex].Value as decimal? ?? 0M) * (dgvLines[colUnitCost.Index, e.RowIndex].Value as decimal? ?? 0M), 2, MidpointRounding.AwayFromZero);
            }

            decimal invoiceTotal = 0M;
            foreach(DataGridViewRow row in dgvLines.Rows.Cast<DataGridViewRow>().Where(dgvr => !dgvr.IsNewRow))
            {
                invoiceTotal += row.Cells[colTotal.Name].Value as decimal? ?? 0M;
            }

            txtInvoiceTotal.Text = invoiceTotal.ToString("N2");

            IsDirty = true;
        }

        private void FieldChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private async void cmdActionButton_Click(object sender, EventArgs e)
        {
            loader.BringToFront();
            loader.Visible = true;

            if (!await InternalSave())
            {
                loader.Visible = false;
                return;
            }

            PutData put = new PutData(DataAccess.APIs.GovernmentPortal, string.Empty, Model);
            put.AddGovHeader(_governmentID);
            if (Model == null || Model.Status == Invoice.Statuses.WorkInProgress)
            {
                put.Resource = "Invoice/Issue";
            }
            else if (Model != null && Model.Status == Invoice.Statuses.ReadyForReceipt)
            {
                put.Resource = "Invoice/Receive";
            }

            await put.ExecuteNoResult();

            Dispose();
            IsDirty = false;
            _explorer.LoadAllItems(true, ReceivableInvoiceContext.GetItemDisplayText(Model.InvoiceNumber, Model.Status.Value));

            loader.Visible = false;
        }

        private void dgvLines_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvLines.Rows.Count || e.ColumnIndex != colItem.Index || (dgvLines.Rows[e.RowIndex].IsNewRow && Model?.Status == Invoice.Statuses.Complete))
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

            void HandleSelectorClosed(object s, EventArgs ea)
            {
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
            selector.ReadOnlyMode = Model != null && Model.Status == Invoice.Statuses.Complete;
            Controls.Add(selector);
            selector.BringToFront();
            selector.Focus();
        }

        private void lblStatus_SizeChanged(object sender, EventArgs e)
        {
            chkAutoReceive.Left = lblStatus.Right + 3;
        }
    }
}
