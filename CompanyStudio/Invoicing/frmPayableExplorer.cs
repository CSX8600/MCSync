using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CompanyStudio.Extensions;
using CompanyStudio.Models;
using MesaSuite.Common;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;

namespace CompanyStudio.Invoicing
{
    public partial class frmPayableExplorer : BaseCompanyStudioContent, ILocationScoped
    {
        private List<Invoice> _invoices = new List<Invoice>();
        FilterOptions filterOption = FilterOptions.None;

        public frmPayableExplorer()
        {
            InitializeComponent();
        }

        public Location LocationModel { get; set; }

        private async void frmPayableExplorer_Load(object sender, EventArgs e)
        {
            Text += " - " + LocationModel.Name;
            PermissionsManager.OnLocationPermissionChange += PermissionsManager_OnLocationPermissionChange;
            LoadSettings();
            await LoadInvoices();
            DisplayInvoices();
        }

        private void PermissionsManager_OnLocationPermissionChange(object sender, PermissionsManager.LocationWidePermissionChangeEventArgs e)
        {
            if (e.LocationID == LocationModel.LocationID && e.Permission == PermissionsManager.LocationWidePermissions.ManageInvoices && !e.Value)
            {
                this.ShowError("You no longer have permission to use Invoicing for " + LocationModel.Name);
                Close();
            }
        }

        private async Task LoadInvoices()
        {
            loader.BringToFront();
            loader.Visible = true;

            GetData getInvoices = new GetData(DataAccess.APIs.CompanyStudio, "Invoice/GetPayables");
            getInvoices.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            _invoices = await getInvoices.GetObject<List<Invoice>>();

            loader.Visible = false;
        }

        private void DisplayInvoices()
        {
            lstPayables.Items.Clear();

            foreach(Invoice invoice in _invoices.Where(i => (i.Status == Invoice.Statuses.Sent && filterOption.HasFlag(FilterOptions.Sent)) ||
                                                            (i.Status == Invoice.Statuses.ReadyForReceipt && filterOption.HasFlag(FilterOptions.ReadyForReceipt)) ||
                                                            (i.Status == Invoice.Statuses.Complete && filterOption.HasFlag(FilterOptions.Complete))))
            {
                ListViewItem listViewItem = new ListViewItem(new string[] { invoice.InvoiceNumber, invoice.LocationFrom?.Company.Name ?? invoice.GovernmentFrom?.Name, invoice.DueDate.ToString("MM/dd/yyyy"), invoice.InvoiceLines.Sum(il => il.Total).ToString("N2"), invoice.Status.ToString().ToDisplayName() });
                listViewItem.Tag = invoice;

                if (invoice.Status == Invoice.Statuses.Sent && invoice.DueDate <= DateTime.Today)
                {
                    listViewItem.BackColor = Color.Red;
                }

                lstPayables.Items.Add(listViewItem);
            }
        }

        private void frmPayableExplorer_FormClosed(object sender, FormClosedEventArgs e)
        {
            PermissionsManager.OnLocationPermissionChange -= PermissionsManager_OnLocationPermissionChange;
        }

        private void lstPayables_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem selectedItem = lstPayables.FocusedItem;
            if (selectedItem == null)
            {
                return;
            }

            Invoice invoice = (Invoice)selectedItem.Tag;
            if (invoice == null)
            {
                return;
            }

            frmPayableInvoice payableInvoice = Studio.dockPanel.Documents.OfType<frmPayableInvoice>().FirstOrDefault(payInv => payInv.InvoiceID == invoice.InvoiceID);
            if (payableInvoice != null)
            {
                payableInvoice.Activate();
                return;
            }

            payableInvoice = new frmPayableInvoice();
            Studio.DecorateStudioContent(payableInvoice);
            payableInvoice.Company = Company;
            payableInvoice.LocationModel = LocationModel;
            payableInvoice.InvoiceID = invoice.InvoiceID;
            payableInvoice.OnSave += PayableInvoice_OnSave;
            payableInvoice.FormClosed += PayableInvoice_FormClosed;
            payableInvoice.Show(Studio.dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }

        private void PayableInvoice_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmPayableInvoice payableInvoice = (frmPayableInvoice)sender;
            payableInvoice.OnSave -= PayableInvoice_OnSave;
            payableInvoice.FormClosed -= PayableInvoice_FormClosed;
        }

        private async void PayableInvoice_OnSave(object sender, EventArgs e)
        {
            await LoadInvoices();
            DisplayInvoices();
        }

        private async void frmPayableExplorer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                await LoadInvoices();
                DisplayInvoices();
            }
        }

        private bool loadingSettings = false;
        private void LoadSettings()
        {
            UserPreferences preferences = UserPreferences.Get();
            Dictionary<string, object> companySettings = preferences.GetPreferencesForSection("company");
            filterOption = (FilterOptions)companySettings.GetOrDefault("accountsPayableExplorerFilter").Cast<int>();

            if (filterOption == FilterOptions.None)
            {
                filterOption = FilterOptions.Sent | FilterOptions.ReadyForReceipt | FilterOptions.Complete;
            }

            loadingSettings = true;
            mnuSent.Checked = filterOption.HasFlag(FilterOptions.Sent);
            mnuReadyForReceipt.Checked = filterOption.HasFlag(FilterOptions.ReadyForReceipt);
            mnuComplete.Checked = filterOption.HasFlag(FilterOptions.Complete);

            string[] columnOrder = companySettings.GetOrDefault("accountsPayableExplorerColumnOrder").Cast<string[]>() ?? new string[0];
            int i = 0;
            foreach (string column in columnOrder)
            {
                ColumnHeader col = lstPayables.Columns.OfType<ColumnHeader>().FirstOrDefault(colu => colu.Text == column);
                if (col == null)
                {
                    continue;
                }

                col.DisplayIndex = i++;
            }

            loadingSettings = false;
        }

        private void SaveSettings()
        {
            UserPreferences preferences = UserPreferences.Get();
            Dictionary<string, object> companySettings = preferences.GetPreferencesForSection("company");
            companySettings["accountsPayableExplorerFilter"] = (int)filterOption;
            List<string> columns = new List<string>();
            foreach (ColumnHeader column in lstPayables.Columns.OfType<ColumnHeader>().OrderBy(col => col.DisplayIndex))
            {
                columns.Add(column.Text);
            }
            companySettings["accountsPayableExplorerColumnOrder"] = columns.ToArray();
            preferences.Save();
        }

        private enum FilterOptions
        {
            None = 0,
            Sent = 1,
            ReadyForReceipt = 2,
            Complete = 4
        }

        private void frmPayableExplorer_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }

        private void FilterCheckChanged(object sender, EventArgs e)
        {
            if (loadingSettings) return;

            filterOption = FilterOptions.None;
            if (mnuSent.Checked) filterOption |= FilterOptions.Sent;
            if (mnuReadyForReceipt.Checked) filterOption |= FilterOptions.ReadyForReceipt;
            if (mnuComplete.Checked) filterOption |= FilterOptions.Complete;

            DisplayInvoices();
        }
    }
}
