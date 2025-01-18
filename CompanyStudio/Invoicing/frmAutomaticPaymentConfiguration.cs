using CompanyStudio.Extensions;
using CompanyStudio.Models;
using MesaSuite.Common.Attributes;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using MesaSuite.Common.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyStudio.Invoicing
{
    [UriReachable("automaticinvoicepaymentconfiguration/{InitialConfigurationID}")]
    public partial class frmAutomaticPaymentConfiguration : BaseCompanyStudioContent, ILocationScoped
    {
        List<Account> accountsUserHasAccessTo = new List<Account>();
        public frmAutomaticPaymentConfiguration()
        {
            InitializeComponent();
        }

        public Location LocationModel { get; set; }

        public long? InitialConfigurationID { get; set; }

        private async void frmAutomaticPaymentConfiguration_Load(object sender, EventArgs e)
        {
            AppendCompanyLocationNameToWindowText();
            await ReloadData();
            InitialConfigurationID = null;
        }

        private async Task ReloadData()
        {
            pnlPlaceholder.Visible = true;
            pnlDetails.Visible = false;
            cboPaymentAccount.Items.Clear();

            long? selectedConfigurationID = InitialConfigurationID;
            if (lstConfigs.SelectedItems.Count > 0)
            {
                selectedConfigurationID = (lstConfigs.SelectedItems[0].Tag as AutomaticInvoicePaymentConfiguration)?.AutomaticInvoicePaymentConfigurationID;
            }

            lstConfigs.Items.Clear();
            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Account/GetAllForUser");
            get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            accountsUserHasAccessTo = await get.GetObject<List<Account>>() ?? new List<Account>();

            get.Resource = "AutomaticInvoicePaymentConfiguration/GetAll";
            List<AutomaticInvoicePaymentConfiguration> automaticInvoicePaymentConfigurations = await get.GetObject<List<AutomaticInvoicePaymentConfiguration>>() ?? new List<AutomaticInvoicePaymentConfiguration>();
            foreach (AutomaticInvoicePaymentConfiguration automaticInvoicePaymentConfiguration in automaticInvoicePaymentConfigurations.OrderBy(aipc => aipc.LocationPayee?.Company.Name ?? aipc.GovernmentPayee?.Name).ThenBy(aipc => aipc.LocationPayee?.Name))
            {
                ListViewItem listViewItem = new ListViewItem(new[]
                {
                    automaticInvoicePaymentConfiguration.LocationIDPayee != null ? $"{automaticInvoicePaymentConfiguration.LocationPayee.Company.Name} ({automaticInvoicePaymentConfiguration.LocationPayee.Name})" : automaticInvoicePaymentConfiguration.GovernmentPayee?.Name + " (Government)",
                    automaticInvoicePaymentConfiguration.PaidAmount?.ToString("N2") ?? "0.00",
                    automaticInvoicePaymentConfiguration.MaxAmount?.ToString("N2") ?? "0.00",
                    automaticInvoicePaymentConfiguration.AccountID != null ? $"{automaticInvoicePaymentConfiguration.Account.Description} ({automaticInvoicePaymentConfiguration.Account.AccountNumber})" : "[None Selected]"
                });
                listViewItem.Tag = automaticInvoicePaymentConfiguration;
                lstConfigs.Items.Add(listViewItem);
                listViewItem.Selected = automaticInvoicePaymentConfiguration.AutomaticInvoicePaymentConfigurationID == selectedConfigurationID;
            }
        }

        private async void tsbAddPayees_Click(object sender, EventArgs e)
        {
            frmAutomaticPaymentConfigurationAddPayees addPayees = new frmAutomaticPaymentConfigurationAddPayees()
            {
                Theme = Theme,
                CompanyID = Company.CompanyID,
                LocationID = LocationModel.LocationID
            };

            addPayees.ShowDialog();

            await ReloadData();
        }

        CancellationTokenSource loadInvoicesCancelTokenSource = null;
        private async void lstConfigs_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!e.IsSelected)
            {
                return;
            }

            if (loadInvoicesCancelTokenSource != null)
            {
                loadInvoicesCancelTokenSource.Cancel();
                loadInvoicesCancelTokenSource = null;
            }

            AutomaticInvoicePaymentConfiguration selectedConfiguration = null;
            if (lstConfigs.SelectedItems.Count > 0)
            {
                selectedConfiguration = lstConfigs.SelectedItems[0].Tag as AutomaticInvoicePaymentConfiguration;
            }

            if (selectedConfiguration == null)
            {
                pnlPlaceholder.Visible = true;
                pnlDetails.Visible = false;

                return;
            }

            cboPaymentAccount.Items.Clear();
            cboPaymentAccount.SelectedItem = null;
            cboPaymentAccount.Text = null;
            foreach (Account account in accountsUserHasAccessTo.OrderBy(a => a.Description))
            {
                DropDownItem<Account> ddi = new DropDownItem<Account>(account, $"{account.Description} ({account.AccountNumber})");
                cboPaymentAccount.Items.Add(ddi);
            }

            txtPayee.Text = selectedConfiguration.LocationIDPayee != null ? $"{selectedConfiguration.LocationPayee.Company.Name} ({selectedConfiguration.LocationPayee.Name})" : selectedConfiguration.GovernmentPayee?.Name + " (Government)";
            txtPayee.Tag = selectedConfiguration;
            txtAmountPaid.Text = selectedConfiguration.PaidAmount?.ToString("N2") ?? "0.00";
            txtMaxAmount.Text = selectedConfiguration.MaxAmount?.ToString("N2") ?? "0.00";
            rdoOnDueDate.Checked = selectedConfiguration.Schedule == AutomaticInvoicePaymentConfiguration.Schedules.OnDueDate;
            rdoImmediately.Checked = selectedConfiguration.Schedule == AutomaticInvoicePaymentConfiguration.Schedules.Immediately;

            if (selectedConfiguration.AccountID != null)
            {
                DropDownItem<Account> selectedAccount = cboPaymentAccount.Items.OfType<DropDownItem<Account>>().FirstOrDefault(a => a.Object.AccountID == selectedConfiguration.AccountID);
                if (selectedAccount == null)
                {
                    selectedAccount = new DropDownItem<Account>(selectedConfiguration.Account, $"{selectedConfiguration.Account.Description} ({selectedConfiguration.Account.AccountNumber})");
                    cboPaymentAccount.Items.Insert(0, selectedAccount);
                }

                cboPaymentAccount.SelectedItem = selectedAccount;
            }
            dgvUpcomingInvoices.Rows.Clear();
            pnlDetails.Visible = true;
            pnlPlaceholder.Visible = false;

            loadInvoicesCancelTokenSource = new CancellationTokenSource();
            CancellationToken cancelToken = loadInvoicesCancelTokenSource.Token;
            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Invoice/GetPayables");
            get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            List<Invoice> invoices = (await get.GetObject<List<Invoice>>() ?? new List<Invoice>()).Where(i => i.Status == Invoice.Statuses.Sent && i.LocationIDFrom == selectedConfiguration.LocationIDPayee && i.GovernmentIDFrom == selectedConfiguration.GovernmentIDPayee).ToList();
            if (!cancelToken.IsCancellationRequested)
            {
                foreach(Invoice invoice in invoices)
                {
                    if (cancelToken.IsCancellationRequested)
                    {
                        break;
                    }

                    DataGridViewRow row = dgvUpcomingInvoices.Rows[dgvUpcomingInvoices.Rows.Add()];
                    row.Cells[colInvoiceNumber.Name].Value = invoice.InvoiceNumber;
                    row.Cells[colInvoiceDate.Name].Value = invoice.InvoiceDate.ToString("MM/dd/yyyy");
                    row.Cells[colDueDate.Name].Value = invoice.DueDate.ToString("MM/dd/yyyy");
                    row.Cells[colDescription.Name].Value = invoice.Description;
                    row.Cells[colAmount.Name].Value = invoice.InvoiceLines.Sum(il => il.Total).ToString("N2");
                    row.Tag = invoice;

                    if (invoice.DueDate <= DateTime.Now)
                    {
                        row.Cells[colDueDate.Name].Style.BackColor = Color.Red;
                        row.Cells[colDueDate.Name].Style.SelectionBackColor = Color.Red;
                    }
                }
            }
        }

        private async void cmdSave_Click(object sender, EventArgs e)
        {
            cmdSave.Enabled = false;

            try
            {
                if (string.IsNullOrWhiteSpace(txtMaxAmount.Text) || !decimal.TryParse(txtMaxAmount.Text, out decimal maxAmount))
                {
                    this.ShowError("Max Amount is a required field");
                    return;
                }

                AutomaticInvoicePaymentConfiguration automaticInvoicePaymentConfiguration = txtPayee.Tag as AutomaticInvoicePaymentConfiguration;
                if (automaticInvoicePaymentConfiguration == null)
                {
                    return;
                }

                automaticInvoicePaymentConfiguration.MaxAmount = maxAmount;
                automaticInvoicePaymentConfiguration.Schedule = rdoImmediately.Checked ? AutomaticInvoicePaymentConfiguration.Schedules.Immediately : AutomaticInvoicePaymentConfiguration.Schedules.OnDueDate;
                automaticInvoicePaymentConfiguration.AccountID = (cboPaymentAccount.SelectedItem as DropDownItem<Account>)?.Object.AccountID;

                PutData put = new PutData(DataAccess.APIs.CompanyStudio, "AutomaticInvoicePaymentConfiguration/Put", automaticInvoicePaymentConfiguration);
                put.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                await put.ExecuteNoResult();

                await ReloadData();
            }
            finally { cmdSave.Enabled = true; }
        }

        private async void tsbDeletePayees_Click(object sender, EventArgs e)
        {
            if (lstConfigs.SelectedItems.Count == 0 || !this.Confirm("Are you sure you want to delete these Configurations?"))
            {
                return;
            }

            DeleteData delete = new DeleteData(DataAccess.APIs.CompanyStudio, null);
            delete.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);

            foreach(ListViewItem item in lstConfigs.SelectedItems)
            {
                AutomaticInvoicePaymentConfiguration automaticInvoicePaymentConfiguration = item.Tag as AutomaticInvoicePaymentConfiguration;
                delete.Resource = $"AutomaticInvoicePaymentConfiguration/Delete/{automaticInvoicePaymentConfiguration.AutomaticInvoicePaymentConfigurationID}";
                await delete.Execute();
            }

            await ReloadData();
        }

        private async void cmdReset_Click(object sender, EventArgs e)
        {
            if (!this.Confirm("Are you sure you want to reset the Paid Amount?"))
            {
                return;
            }

            AutomaticInvoicePaymentConfiguration configuration = txtPayee.Tag as AutomaticInvoicePaymentConfiguration;
            PatchData patch = new PatchData(DataAccess.APIs.CompanyStudio, "AutomaticInvoicePaymentConfiguration/Patch", PatchData.PatchMethods.Replace, configuration.AutomaticInvoicePaymentConfigurationID, new Dictionary<string, object>()
            {
                { nameof(AutomaticInvoicePaymentConfiguration.PaidAmount), 0M }
            });
            patch.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            await patch.Execute();

            await ReloadData();
        }

        private async void tsbResetPaidAmounts_Click(object sender, EventArgs e)
        {
            if (lstConfigs.SelectedItems.Count == 0 || !this.Confirm("Are you sure you want to reset the Paid Amount on the selected items?"))
            {
                return;
            }

            foreach(ListViewItem item in lstConfigs.SelectedItems)
            {
                AutomaticInvoicePaymentConfiguration automaticInvoicePaymentConfiguration = item.Tag as AutomaticInvoicePaymentConfiguration;
                if (automaticInvoicePaymentConfiguration == null)
                {
                    continue;
                }

                PatchData patch = new PatchData(DataAccess.APIs.CompanyStudio, "AutomaticInvoicePaymentConfiguration/Patch", PatchData.PatchMethods.Replace, automaticInvoicePaymentConfiguration.AutomaticInvoicePaymentConfigurationID, new Dictionary<string, object>()
                {
                    { nameof(AutomaticInvoicePaymentConfiguration.PaidAmount), 0M }
                });
                patch.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                await patch.Execute();

                await ReloadData();
            }
        }

        private async void tsbUpdateSelected_Click(object sender, EventArgs e)
        {
            if (lstConfigs.SelectedItems.Count == 0)
            {
                return;
            }

            frmAutomaticPaymentConfigurationMassUpdate massUpdate = new frmAutomaticPaymentConfigurationMassUpdate()
            {
                CompanyID = Company.CompanyID,
                LocationID = LocationModel.LocationID,
                Theme = Theme
            };

            if (massUpdate.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            Dictionary<string, object> updateValues = new Dictionary<string, object>()
            {
                { nameof(AutomaticInvoicePaymentConfiguration.Schedule), massUpdate.Schedule.ToString() }
            };

            if (massUpdate.MaxAmount != null)
            {
                updateValues[nameof(AutomaticInvoicePaymentConfiguration.MaxAmount)] = massUpdate.MaxAmount;
            }
            
            if (massUpdate.AccountID != null)
            {
                updateValues[nameof(AutomaticInvoicePaymentConfiguration.AccountID)] = massUpdate.AccountID;
            }

            PatchData patch = new PatchData(DataAccess.APIs.CompanyStudio, "AutomaticInvoicePaymentConfiguration/Patch", PatchData.PatchMethods.Replace, null, updateValues);
            patch.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);

            foreach (ListViewItem item in lstConfigs.SelectedItems)
            {
                AutomaticInvoicePaymentConfiguration automaticInvoicePaymentConfiguration = item.Tag as AutomaticInvoicePaymentConfiguration;
                if (automaticInvoicePaymentConfiguration == null)
                {
                    continue;
                }

                patch.PrimaryKey = automaticInvoicePaymentConfiguration.AutomaticInvoicePaymentConfigurationID;
                await patch.Execute();
            }

            await ReloadData();
        }

        private async void tsbCloneTo_Click(object sender, EventArgs e)
        {
            frmAutomaticPaymentConfigurationCloneTo cloneTo = new frmAutomaticPaymentConfigurationCloneTo()
            {
                CompanyID = Company.CompanyID,
                LocationID = LocationModel.LocationID,
                Theme = Theme
            };

            if (cloneTo.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            foreach(long? locationID in cloneTo.SelectedLocationIDs)
            {
                GetData get = new GetData(DataAccess.APIs.CompanyStudio, "AutomaticInvoicePaymentConfiguration/GetAll");
                get.AddLocationHeader(Company.CompanyID, locationID);
                List<AutomaticInvoicePaymentConfiguration> existingConfigurations = await get.GetObject<List<AutomaticInvoicePaymentConfiguration>>() ?? new List<AutomaticInvoicePaymentConfiguration>();

                foreach(ListViewItem listViewItem in lstConfigs.SelectedItems)
                {
                    AutomaticInvoicePaymentConfiguration configToClone = listViewItem.Tag as AutomaticInvoicePaymentConfiguration;
                    if (configToClone == null)
                    {
                        continue;
                    }

                    configToClone = configToClone.ShallowClone();
                    AutomaticInvoicePaymentConfiguration original = existingConfigurations.FirstOrDefault(aipc => aipc.LocationIDPayee == configToClone.LocationIDPayee && aipc.GovernmentIDPayee == configToClone.GovernmentIDPayee);
                    if (original != null)
                    {
                        configToClone.AutomaticInvoicePaymentConfigurationID = original.AutomaticInvoicePaymentConfigurationID;
                        configToClone.LocationIDConfiguredFor = original.LocationIDConfiguredFor;

                        PutData put = new PutData(DataAccess.APIs.CompanyStudio, "AutomaticInvoicePaymentConfiguration/Put", configToClone);
                        put.AddLocationHeader(Company.CompanyID, locationID);
                        await put.ExecuteNoResult();
                    }
                    else
                    {
                        configToClone.AutomaticInvoicePaymentConfigurationID = null;
                        configToClone.LocationIDConfiguredFor = locationID;

                        PostData post = new PostData(DataAccess.APIs.CompanyStudio, "AutomaticInvoicePaymentConfiguration/Post", configToClone);
                        post.AddLocationHeader(Company.CompanyID, locationID);
                        await post.ExecuteNoResult();
                    }
                }
            }
        }

        private void lstConfigs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                foreach (ListViewItem item in lstConfigs.Items)
                {
                    item.Selected = true;
                }
            }
        }

        private void dgvUpcomingInvoices_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvUpcomingInvoices.Rows.Count || !PermissionsManager.HasPermission(LocationModel.LocationID.Value, PermissionsManager.LocationWidePermissions.ManageInvoices))
            {
                return;
            }

            Invoice invoice = dgvUpcomingInvoices.Rows[e.RowIndex].Tag as Invoice;
            if (invoice == null)
            {
                return;
            }

            frmPayableInvoice payableInvoice = new frmPayableInvoice()
            {
                InvoiceID = invoice.InvoiceID
            };
            Studio.DecorateStudioContent(payableInvoice);
            payableInvoice.Company = Company;
            payableInvoice.LocationModel = LocationModel;
            payableInvoice.Show(Studio.dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }
    }
}
