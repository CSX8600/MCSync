using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CompanyStudio.Extensions;
using CompanyStudio.Models;
using MesaSuite.Common;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;

namespace CompanyStudio.Invoicing
{
    public partial class frmAccountsReceivableExplorer : BaseCompanyStudioContent, ILocationScoped
    {
        private List<Invoice> invoices = new List<Invoice>();
        private FilterOptions filterOption;
        public frmAccountsReceivableExplorer()
        {
            InitializeComponent();
        }

        public Location LocationModel { get; set; }

        private void mnuAdd_Click(object sender, EventArgs e)
        {
            frmReceivableInvoice receivableInvoice = new frmReceivableInvoice();
            Studio.DecorateStudioContent(receivableInvoice);
            receivableInvoice.Company = Company;
            receivableInvoice.LocationModel = LocationModel;
            receivableInvoice.OnSave += ReceivableInvoice_OnSave;
            receivableInvoice.FormClosed += ReceivableInvoice_FormClosed;
            receivableInvoice.Show(Studio.dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }

        private void ReceivableInvoice_FormClosed(object sender, FormClosedEventArgs e)
        {
            ((frmReceivableInvoice)sender).FormClosed -= ReceivableInvoice_FormClosed;
        }

        private void ReceivableInvoice_OnSave(object sender, EventArgs e)
        {
            LoadInvoices();
        }

        private void frmAccountsReceivableExplorer_Load(object sender, EventArgs e)
        {
            Text += " - " + LocationModel.Name;

            PermissionsManager.OnLocationPermissionChange += PermissionsManager_OnLocationPermissionChange;

            ReadSettings();
            LoadInvoices();
        }

        private void PermissionsManager_OnLocationPermissionChange(object sender, PermissionsManager.LocationWidePermissionChangeEventArgs e)
        {
            if (e.LocationID == LocationModel.LocationID && e.Permission == PermissionsManager.LocationWidePermissions.ManageInvoices && !e.Value)
            {
                this.ShowError("You no longer have permission to use Receivable Invoices for " + LocationModel.Name);
                Close();
            }
        }

        private async void LoadInvoices()
        {
            loader.BringToFront();
            loader.Visible = true;

            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Invoice/GetReceivables");
            get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            invoices = await get.GetObject<List<Invoice>>() ?? new List<Invoice>();

            loader.Visible = false;

            AddInvoicesToList();
        }

        private void AddInvoicesToList()
        {
            lstInvoices.Items.Clear();

            foreach (Invoice invoice in invoices)
            {
                if ((invoice.Status == Invoice.Statuses.WorkInProgress && !filterOption.HasFlag(FilterOptions.WIP)) ||
                    (invoice.Status == Invoice.Statuses.Sent && !filterOption.HasFlag(FilterOptions.Sent)) ||
                    (invoice.Status == Invoice.Statuses.ReadyForReceipt && !filterOption.HasFlag(FilterOptions.ReadyForReceipt)) ||
                    (invoice.Status == Invoice.Statuses.Complete && !filterOption.HasFlag(FilterOptions.Complete)))
                {
                    continue;
                }

                string payor = "";
                if (invoice.LocationIDTo != default)
                {
                    payor = $"{invoice.LocationTo.Company.Name} ({invoice.LocationTo.Name})";
                }
                else if (invoice.GovernmentIDTo != default)
                {
                    payor = invoice.GovernmentTo.Name;
                }

                string amount = invoice.InvoiceLines.Sum(il => il.Total).ToString("N2");
                
                ListViewItem listViewItem = new ListViewItem(new[] { invoice.InvoiceNumber, payor, amount, invoice.Status.ToString() }) { Tag = invoice };
                if (invoice.Status != Invoice.Statuses.Complete && invoice.DueDate <= DateTime.Today)
                {
                    listViewItem.BackColor = Color.Yellow;
                    listViewItem.ForeColor = Color.Black;
                }

                lstInvoices.Items.Add(listViewItem);
            }
        }

        private void lstInvoices_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstInvoices.SelectedItems.Count == 0)
            {
                return;
            }

            frmReceivableInvoice frmReceivableInvoice = new frmReceivableInvoice();
            Studio.DecorateStudioContent(frmReceivableInvoice);
            frmReceivableInvoice.Company = Company;
            frmReceivableInvoice.LocationModel = LocationModel;
            frmReceivableInvoice.InvoiceID = ((Invoice)lstInvoices.SelectedItems[0].Tag).InvoiceID;
            frmReceivableInvoice.OnSave += frmReceivableInvoice_OnSave;
            frmReceivableInvoice.FormClosed += frmReceivableInvoice_FormClosed;
            frmReceivableInvoice.Show(Studio.dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }

        private void frmReceivableInvoice_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmReceivableInvoice receivableInvoice = (frmReceivableInvoice)sender;
            receivableInvoice.FormClosed -= frmReceivableInvoice_FormClosed;
            receivableInvoice.OnSave -= frmReceivableInvoice_OnSave;
        }

        private void frmReceivableInvoice_OnSave(object sender, EventArgs e)
        {
            LoadInvoices();
        }

        private void lstInvoices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstInvoices.SelectedItems.Count <= 0)
            {
                mnuDelete.Enabled = false;
            }

            foreach(ListViewItem item in lstInvoices.SelectedItems)
            {
                Invoice invoice = (Invoice)item.Tag;
                if (invoice.Status == Invoice.Statuses.WorkInProgress || invoice.Status == Invoice.Statuses.Sent || invoice.Status == Invoice.Statuses.ReadyForReceipt)
                {
                    mnuDelete.Enabled = true;
                    return;
                }
            }

            mnuDelete.Enabled = false;
        }

        private async void DeleteSelectedInvoices()
        {
            List<Invoice> invoicesToDelete = new List<Invoice>();
            foreach(ListViewItem item in lstInvoices.SelectedItems)
            {
                Invoice invoice = (Invoice)item.Tag;
                if (invoice.Status == Invoice.Statuses.WorkInProgress || invoice.Status == Invoice.Statuses.Sent || invoice.Status == Invoice.Statuses.ReadyForReceipt)
                {
                    invoicesToDelete.Add(invoice);
                }
            }

            if (!invoicesToDelete.Any())
            {
                return;
            }

            if (!this.Confirm("Are you sure you want to delete these Invoices?"))
            {
                return;
            }

            loader.BringToFront();
            loader.Visible = true;

            foreach(Invoice invoice in invoicesToDelete)
            {
                DeleteData delete = new DeleteData(DataAccess.APIs.CompanyStudio, $"Invoice/Delete/{invoice.InvoiceID}");
                delete.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                await delete.Execute();

                frmReceivableInvoice receivableInvoice = Studio.dockPanel.Contents.OfType<frmReceivableInvoice>().FirstOrDefault(content => content.InvoiceID == invoice.InvoiceID);
                if (receivableInvoice != null)
                {
                    receivableInvoice.Close();
                }
            }

            LoadInvoices();
        }

        private void mnuDelete_Click(object sender, EventArgs e)
        {
            DeleteSelectedInvoices();
        }

        private void lstInvoices_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedInvoices();
            }
        }

        bool loadingSettings = false;
        private void ReadSettings()
        {
            UserPreferences preferences = UserPreferences.Get();
            Dictionary<string, object> companySettings = preferences.GetPreferencesForSection("company");

            int filter = companySettings.GetOrDefault("accountsReceivableExplorerFilter").Cast<int>();
            filterOption = (FilterOptions)filter;

            loadingSettings = true;
            mnuFilterWIP.Checked = filterOption.HasFlag(FilterOptions.WIP);
            mnuFilterSent.Checked = filterOption.HasFlag(FilterOptions.Sent);
            mnuFilterCompleted.Checked = filterOption.HasFlag(FilterOptions.Complete);
            mnuFilterReadyToReceive.Checked = filterOption.HasFlag(FilterOptions.ReadyForReceipt);

            string[] columnOrder = companySettings.GetOrDefault("accountsReceivableExplorerColumnOrder").Cast<string[]>() ?? new string[0];
            int i = 0;
            foreach(string column in columnOrder)
            {
                ColumnHeader col = lstInvoices.Columns.OfType<ColumnHeader>().FirstOrDefault(colu => colu.Text == column);
                if (col == null)
                {
                    continue;
                }

                col.DisplayIndex = i++;
            }

            loadingSettings = false;
        }

        private void WriteSettings()
        {
            UserPreferences preferences = UserPreferences.Get();
            Dictionary<string, object> companySettings = preferences.GetPreferencesForSection("company");
            companySettings["accountsReceivableExplorerFilter"] = (int)filterOption;
            List<string> columns = new List<string>();
            foreach(ColumnHeader column in lstInvoices.Columns.OfType<ColumnHeader>().OrderBy(col => col.DisplayIndex))
            {
                columns.Add(column.Text);
            }
            companySettings["accountsReceivableExplorerColumnOrder"] = columns.ToArray();
            preferences.Save();
        }

        private void FilterItem_CheckedChanged(object sender, EventArgs e)
        {
            if (loadingSettings) return;

            filterOption = FilterOptions.None;
            if (mnuFilterWIP.Checked) filterOption |= FilterOptions.WIP;
            if (mnuFilterSent.Checked) filterOption |= FilterOptions.Sent;
            if (mnuFilterCompleted.Checked) filterOption |= FilterOptions.Complete;
            if (mnuFilterReadyToReceive.Checked) filterOption |= FilterOptions.ReadyForReceipt;

            AddInvoicesToList();
        }

        [Flags]
        private enum FilterOptions
        {
            None = 0,
            WIP = 1,
            Sent = 2,
            Complete = 4,
            ReadyForReceipt = 8
        }

        private void frmAccountsReceivableExplorer_FormClosing(object sender, FormClosingEventArgs e)
        {
            WriteSettings();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            try { WriteSettings(); } catch { }

            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void frmAccountsReceivableExplorer_FormClosed(object sender, FormClosedEventArgs e)
        {
            PermissionsManager.OnLocationPermissionChange -= PermissionsManager_OnLocationPermissionChange;
        }

        private void frmAccountsReceivableExplorer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                LoadInvoices();
            }
        }
    }
}
