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
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using MesaSuite.Common.Utility;

namespace GovernmentPortal.Invoicing
{
    [ToolboxItem(false)]
    public partial class AutomaticPaymentControl : UserControl, IExplorerControl<AutomaticInvoicePaymentConfiguration>
    {
        public event EventHandler IsDirtyChanged;
        public long GovernmentID { get; set; }

        public AutomaticPaymentControl()
        {
            InitializeComponent();
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

        public AutomaticInvoicePaymentConfiguration Model { get; set; }
        private frmGenericExplorer<AutomaticInvoicePaymentConfiguration> _explorer;
        frmGenericExplorer<AutomaticInvoicePaymentConfiguration> IExplorerControl<AutomaticInvoicePaymentConfiguration>.Explorer { set => _explorer = value; }

        public async void OnDeleteClicked()
        {
            if (Model == null)
            {
                this.ShowError("Cannot delete an unsaved Automatic Invoice Payment Configuration");
                return;
            }

            if (!this.Confirm("Are you sure you want to delete this Automatic Invoice Payment Configuration?"))
            {
                return;
            }

            DeleteData delete = new DeleteData(DataAccess.APIs.GovernmentPortal, "AutomaticInvoicePaymentConfiguration/Delete/" + Model.AutomaticInvoicePaymentConfigurationID);
            delete.AddGovHeader(GovernmentID);
            await delete.Execute();

            if (delete.RequestSuccessful)
            {
                IsDirty = false;
                Dispose();
                _explorer.LoadAllItems(true);
            }
        }

        public async void OnSaveClicked()
        {
            if (Model == null)
            {
                this.ShowError("Automatic Invoice Payment Configurations cannot be created in this manner");
                return;
            }

            if (await InternalSave())
            {
                Dispose();
                _explorer.LoadAllItems(true, AutomaticPaymentsContext.GetDisplayName(Model));
            }
        }

        private async Task<bool> InternalSave()
        {
            if (string.IsNullOrEmpty(txtMaxAmount.Text) || !decimal.TryParse(txtMaxAmount.Text, out decimal maxAmount))
            {
                this.ShowError("Max Amount is a required field");
                return false;
            }

            PatchData patch = new PatchData(DataAccess.APIs.GovernmentPortal, "AutomaticInvoicePaymentConfiguration/Patch", PatchData.PatchMethods.Replace, Model.AutomaticInvoicePaymentConfigurationID, new Dictionary<string, object>()
            {
                { nameof(AutomaticInvoicePaymentConfiguration.MaxAmount), maxAmount },
                { nameof(AutomaticInvoicePaymentConfiguration.Schedule), rdoImmediately.Checked ? AutomaticInvoicePaymentConfiguration.Schedules.Immediately.ToString() : AutomaticInvoicePaymentConfiguration.Schedules.OnDueDate.ToString() },
                { nameof(AutomaticInvoicePaymentConfiguration.AccountID), (cboPaymentAccount.SelectedItem as DropDownItem<Account>)?.Object.AccountID }
            });
            patch.AddGovHeader(GovernmentID);
            await patch.Execute();

            if (patch.RequestSuccessful)
            {
                IsDirty = false;
                Model.MaxAmount = maxAmount;
            }

            return patch.RequestSuccessful;
        }

        private async void AutomaticPaymentControl_Load(object sender, EventArgs e)
        {
            if (Model == null)
            {
                frmAutomaticPaymentsSelectPayees payees = new frmAutomaticPaymentsSelectPayees()
                {
                    GovernmentID = GovernmentID
                };

                if (payees.ShowDialog() != DialogResult.OK || (!payees.Locations.Any() && !payees.Governments.Any()))
                {
                    Dispose();
                    return;
                }

                PostData post = new PostData(DataAccess.APIs.GovernmentPortal, "AutomaticInvoicePaymentConfiguration/Post");
                post.AddGovHeader(GovernmentID);

                AutomaticInvoicePaymentConfiguration firstSavedConfig = null;
                foreach(Location location in payees.Locations)
                {
                    AutomaticInvoicePaymentConfiguration newConfig = new AutomaticInvoicePaymentConfiguration()
                    {
                        GovernmentIDConfiguredFor = GovernmentID,
                        LocationIDPayee = location.LocationID,
                        PaidAmount = 0,
                        MaxAmount = 0
                    };

                    post.ObjectToPost = newConfig;
                    AutomaticInvoicePaymentConfiguration saved = await post.Execute<AutomaticInvoicePaymentConfiguration>();
                    firstSavedConfig = firstSavedConfig ?? saved;
                }

                foreach (Government government in payees.Governments)
                {
                    AutomaticInvoicePaymentConfiguration newConfig = new AutomaticInvoicePaymentConfiguration()
                    {
                        GovernmentIDConfiguredFor = GovernmentID,
                        GovernmentIDPayee = government.GovernmentID,
                        PaidAmount = 0,
                        MaxAmount = 0
                    };

                    post.ObjectToPost = newConfig;
                    AutomaticInvoicePaymentConfiguration saved = await post.Execute<AutomaticInvoicePaymentConfiguration>();
                    firstSavedConfig = firstSavedConfig ?? saved;
                }

                Dispose();
                _explorer.LoadAllItems(true, AutomaticPaymentsContext.GetDisplayName(firstSavedConfig));
                return;
            }
            GetData get = new GetData(DataAccess.APIs.GovernmentPortal, "Account/MyAccounts");
            get.AddGovHeader(GovernmentID);
            List<Account> accounts = await get.GetObject<List<Account>>() ?? new List<Account>();
            foreach (Account account in accounts)
            {
                DropDownItem<Account> ddi = new DropDownItem<Account>(account, $"{account.Description} ({account.AccountNumber})");
                cboPaymentAccount.Items.Add(ddi);
            }
            
            if (Model.AccountID != null && !accounts.Any(a => a.AccountID == Model.AccountID))
            {
                DropDownItem<Account> ddi = new DropDownItem<Account>(Model.Account, $"{Model.Account?.Description} ({Model.Account?.AccountNumber})");
                cboPaymentAccount.Items.Insert(0, ddi);
            }

            txtPayee.Text = Model.DisplayName;
            txtAmountPaid.Text = Model.PaidAmount?.ToString("N2");
            txtMaxAmount.Text = Model.MaxAmount?.ToString("N2");
            rdoImmediately.Checked = Model.Schedule == AutomaticInvoicePaymentConfiguration.Schedules.Immediately;
            rdoOnDueDate.Checked = Model.Schedule == AutomaticInvoicePaymentConfiguration.Schedules.OnDueDate;
            cboPaymentAccount.SelectedItem = cboPaymentAccount.Items.OfType<DropDownItem<Account>>().FirstOrDefault(ddi => ddi.Object.AccountID == Model.AccountID);

            IsDirty = false;

            get.Resource = "Invoice/GetPayables";
            List<Invoice> invoices = await get.GetObject<List<Invoice>>() ?? new List<Invoice>();
            foreach (Invoice invoice in invoices)
            {
                if (invoice.Status != Invoice.Statuses.Sent ||
                    invoice.GovernmentIDFrom != Model.GovernmentIDPayee ||
                    invoice.LocationIDFrom != Model.LocationIDPayee)
                {
                    continue;
                }

                DataGridViewRow row = dgvUpcomingInvoices.Rows[dgvUpcomingInvoices.Rows.Add()];
                row.Cells[colInvoiceNumber.Name].Value = invoice.InvoiceNumber;
                row.Cells[colInvoiceDate.Name].Value = invoice.InvoiceDate.ToString("MM/dd/yyyy");
                row.Cells[colDueDate.Name].Value = invoice.DueDate.ToString("MM/dd/yyyy");
                row.Cells[colDescription.Name].Value = invoice.Description;
                row.Cells[colAmount.Name].Value = invoice.InvoiceLines?.Sum(l => l.Total).ToString("N2");
                row.Tag = invoice;

                if (invoice.DueDate < DateTime.Now)
                {
                    row.Cells[colDueDate.Name].Style.BackColor = Color.Red;
                    row.Cells[colDueDate.Name].Style.ForeColor = Color.White;
                    row.Cells[colDueDate.Name].Style.SelectionBackColor = Color.Red;
                    row.Cells[colDueDate.Name].Style.SelectionForeColor = Color.White;
                }
            }
        }

        private async void cmdReset_Click(object sender, EventArgs e)
        {
            if (!this.Confirm("Are you sure you want to reset the Paid Amount?"))
            {
                return;
            }

            PatchData patch = new PatchData(DataAccess.APIs.GovernmentPortal, "AutomaticInvoicePaymentConfiguration/Patch", PatchData.PatchMethods.Replace, Model.AutomaticInvoicePaymentConfigurationID, new Dictionary<string, object>()
            {
                { nameof(AutomaticInvoicePaymentConfiguration.PaidAmount), 0 }
            });
            patch.AddGovHeader(GovernmentID);
            await patch.Execute();
            if (patch.RequestSuccessful)
            {
                IsDirty = false;
                Dispose();
                _explorer.LoadAllItems(true, AutomaticPaymentsContext.GetDisplayName(Model));
            }
        }

        private void ControlChanged(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        public async void CloneThisConfiguration()
        {
            if (IsDirty)
            {
                this.ShowError("Changes must be saved before cloning this configuration.");
                return;
            }

            frmAutomaticPaymentsCloneToPicker selectPayees = new frmAutomaticPaymentsCloneToPicker()
            {
                GovernmentID = GovernmentID,
                CurrentAutomaticInvoicePaymentConfigurationID = Model.AutomaticInvoicePaymentConfigurationID.Value
            };

            if (selectPayees.ShowDialog() != DialogResult.OK || !selectPayees.SelectedAutomaticInvoicePaymentConfigurations.Any())
            {
                return;
            }

            PutData put = new PutData(DataAccess.APIs.GovernmentPortal, "AutomaticInvoicePaymentConfiguration/Put", null);
            put.AddGovHeader(GovernmentID);
            foreach(AutomaticInvoicePaymentConfiguration cloneToConfiguration in selectPayees.SelectedAutomaticInvoicePaymentConfigurations)
            {
                AutomaticInvoicePaymentConfiguration configToPut = Model.ShallowClone();
                configToPut.AutomaticInvoicePaymentConfigurationID = cloneToConfiguration.AutomaticInvoicePaymentConfigurationID;
                configToPut.LocationIDPayee = cloneToConfiguration.LocationIDPayee;
                configToPut.GovernmentIDPayee = cloneToConfiguration.GovernmentIDPayee;
                put.ObjectToPut = configToPut;
                await put.ExecuteNoResult();
            }

            Dispose();
            _explorer.LoadAllItems(true, AutomaticPaymentsContext.GetDisplayName(Model));
        }

        private void dgvUpcomingInvoices_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvUpcomingInvoices.Rows.Count)
            {
                return;
            }

            Invoice invoice = dgvUpcomingInvoices.Rows[e.RowIndex].Tag as Invoice;
            frmPortal portal = _explorer.MdiParent as frmPortal;
            if (portal == null)
            {
                return;
            }

            PayableInvoiceContext invoiceContext = new PayableInvoiceContext(GovernmentID)
            {
                InitiallySelectedInvoiceID = invoice.InvoiceID
            };
            
            frmGenericExplorer<Invoice> invoiceExplorer = new frmGenericExplorer<Invoice>(invoiceContext);
            invoiceExplorer.MdiParent = portal;
            invoiceExplorer.Show();
        }
    }
}
