using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CompanyStudio.Extensions;
using CompanyStudio.Models;
using CompanyStudio.Store.ClonePrices;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using MesaSuite.Common.Utility;
using WeifenLuo.WinFormsUI.Docking;

namespace CompanyStudio.Invoicing
{
    public partial class frmAutomaticPaymentConfigurationAddPayees : Form
    {
        public long? CompanyID { get; set; }
        public long? LocationID { get; set; }
        public ThemeBase Theme { get; set; }

        private List<AutomaticInvoicePaymentConfiguration> _otherLocationConfigurations = new List<AutomaticInvoicePaymentConfiguration>();

        public frmAutomaticPaymentConfigurationAddPayees()
        {
            InitializeComponent();
            colCloneFrom.ValueType = typeof(long?);
            colCloneFrom.DisplayMember = nameof(AutomaticInvoicePaymentConfiguration.DisplayName);
            colCloneFrom.ValueMember = nameof(AutomaticInvoicePaymentConfiguration.AutomaticInvoicePaymentConfigurationID);
        }

        private async void frmAutomaticPaymentConfigurationAddPayees_Load(object sender, EventArgs e)
        {
            loader.BringToFront();
            loader.Visible = true;

            try
            {
                studioFormExtender.ApplyStyle(this, Theme);

                // Get all configs for this location
                GetData get = new GetData(DataAccess.APIs.CompanyStudio, "AutomaticInvoicePaymentConfiguration/GetAll");
                get.AddLocationHeader(CompanyID, LocationID);
                List<AutomaticInvoicePaymentConfiguration> automaticInvoicePaymentConfigurations = await get.GetObject<List<AutomaticInvoicePaymentConfiguration>>() ?? new List<AutomaticInvoicePaymentConfiguration>();

                HashSet<long?> existingLocations = automaticInvoicePaymentConfigurations.Where(c => c.LocationIDPayee != null).Select(c => c.LocationIDPayee).ToHashSet();
                HashSet<long?> existingGovernments = automaticInvoicePaymentConfigurations.Where(c => c.GovernmentIDPayee != null).Select(c => c.GovernmentIDPayee).ToHashSet();

                // Get configs that this user has access to for other locations (so we can clone if necessary)
                get.Resource = "Company/GetAll";
                List<Company> companies = await get.GetObject<List<Company>>() ?? new List<Company>();

                foreach (long? locationID in PermissionsManager.AccessibleLocationIDs.Where(l => companies.FirstOrDefault(c => c.Locations.Any(loc => loc.LocationID == l)).CompanyID == CompanyID))
                {
                    if (LocationID == locationID)
                    {
                        continue;
                    }

                    if (PermissionsManager.HasPermission(locationID.Value, PermissionsManager.LocationWidePermissions.ManageInvoices))
                    {
                        GetData getOtherLocation = new GetData(DataAccess.APIs.CompanyStudio, "AutomaticInvoicePaymentConfiguration/GetAll");
                        getOtherLocation.AddLocationHeader(CompanyID, locationID);
                        _otherLocationConfigurations.AddRange(await getOtherLocation.GetObject<List<AutomaticInvoicePaymentConfiguration>>() ?? new List<AutomaticInvoicePaymentConfiguration>());
                    }
                }

                // Load available locations
                foreach (Company company in companies)
                {
                    foreach (Location location in company.Locations.Where(l => !existingLocations.Contains(l.LocationID)))
                    {
                        bool isCloneable = _otherLocationConfigurations.Any(c => c.LocationIDPayee == location.LocationID);

                        ListViewItem newItem = new ListViewItem(new[] { $"{company.Name} ({location.Name})", isCloneable ? "Yes" : "No" });
                        newItem.Tag = location;
                        lstAvailable.Items.Add(newItem);
                    }
                }

                // Load available govs
                get.Resource = "Government/GetAll";
                List<Government> governments = await get.GetObject<List<Government>>() ?? new List<Government>();
                foreach (Government government in governments.Where(g => !existingGovernments.Contains(g.GovernmentID)))
                {
                    bool isCloneable = _otherLocationConfigurations.Any(c => c.GovernmentIDPayee == government.GovernmentID);

                    ListViewItem newItem = new ListViewItem(new[] { government.Name + " (Government)", isCloneable ? "Yes" : "No" });
                    newItem.Tag = government;
                    lstAvailable.Items.Add(newItem);
                }

                HashSet<long?> otherLocationIDs = new HashSet<long?>();
                foreach(AutomaticInvoicePaymentConfiguration otherConfig in _otherLocationConfigurations.Where(c => otherLocationIDs.Add(c.LocationIDConfiguredFor)))
                {
                    DropDownItem<Location> ddi = new DropDownItem<Location>(otherConfig.LocationConfiguredFor, otherConfig.LocationConfiguredFor.Name);
                    cboCloneAllFrom.Items.Add(ddi);
                }
            }
            finally { loader.Visible = false; }
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            Add(lstAvailable.SelectedItems.ToList());
        }

        private void cmdAddAll_Click(object sender, EventArgs e)
        {
            Add(lstAvailable.Items.ToList());
        }

        private void Add(List<ListViewItem> items)
        {
            foreach (ListViewItem item in items)
            {
                DataGridViewRow row = dgvChosen.Rows[dgvChosen.Rows.Add()];
                row.Cells[colPayee.Name].Value = item.Text;
                row.Tag = item.Tag;

                Location locationPayee = item.Tag as Location;
                Government governmentPayee = item.Tag as Government;

                List<AutomaticInvoicePaymentConfiguration> cloneableConfigurations = _otherLocationConfigurations.Where(aipc => aipc.LocationIDPayee == locationPayee?.LocationID && aipc.GovernmentIDPayee == governmentPayee?.GovernmentID).ToList();
                if (cloneableConfigurations.Any())
                {
                    ((DataGridViewComboBoxCell)row.Cells[colCloneFrom.Name]).DataSource = cloneableConfigurations;
                }
                else
                {
                    row.Cells[colCloneFrom.Name].ReadOnly = true;
                    ((DataGridViewComboBoxCell)row.Cells[colCloneFrom.Name]).DataSource = new List<object>()
                    {
                        new { AutomaticInvoicePaymentConfigurationID = (long?)0, DisplayName = "[Not Cloneable]" }
                    };
                    row.Cells[colCloneFrom.Name].Value = (long?)0;
                }

                lstAvailable.Items.Remove(item);
            }

            dgvChosen.Sort(colPayee, ListSortDirection.Ascending);
        }

        private void cmdRemove_Click(object sender, EventArgs e)
        {
            Remove(dgvChosen.SelectedRows.OfType<DataGridViewRow>().ToList());
        }

        private void Remove(List<DataGridViewRow> rows)
        {
            foreach (DataGridViewRow row in rows)
            {
                Location locationPayee = row.Tag as Location;
                Government governmentPayee = row.Tag as Government;

                bool cloneable = _otherLocationConfigurations.Any(c => c.GovernmentIDPayee == governmentPayee?.GovernmentID && c.LocationIDPayee == locationPayee?.LocationID);

                ListViewItem newItem = new ListViewItem(new[] { row.Cells[colPayee.Name].Value.ToString(), cloneable ? "Yes" : "No" });
                newItem.Tag = row.Tag;
                lstAvailable.Items.Add(newItem);

                dgvChosen.Rows.Remove(row);
            }
        }

        private void cmdRemoveAll_Click(object sender, EventArgs e)
        {
            Remove(dgvChosen.Rows.OfType<DataGridViewRow>().ToList());
        }

        private async void cmdSave_Click(object sender, EventArgs e)
        {
            loader.BringToFront();
            loader.Visible = true;
            cmdSave.Enabled = false;

            try
            {
                foreach (DataGridViewRow row in dgvChosen.Rows)
                {
                    long? cloneFromID = (long?)row.Cells[colCloneFrom.Name].Value;
                    if (cloneFromID != null && cloneFromID != 0)
                    {
                        AutomaticInvoicePaymentConfiguration otherConfiguration = _otherLocationConfigurations.FirstOrDefault(olc => olc.AutomaticInvoicePaymentConfigurationID == cloneFromID);
                        AutomaticInvoicePaymentConfiguration cloned = otherConfiguration.ShallowClone();
                        cloned.AutomaticInvoicePaymentConfigurationID = null;
                        cloned.LocationIDConfiguredFor = LocationID;
                        cloned.PaidAmount = 0;

                        PostData post = new PostData(DataAccess.APIs.CompanyStudio, "AutomaticInvoicePaymentConfiguration/Post", cloned);
                        post.AddLocationHeader(CompanyID, LocationID);
                        await post.ExecuteNoResult();
                    }
                    else
                    {
                        Location locationPayee = row.Tag as Location;
                        Government governmentPayee = row.Tag as Government;

                        AutomaticInvoicePaymentConfiguration newConfiguration = new AutomaticInvoicePaymentConfiguration()
                        {
                            LocationIDConfiguredFor = LocationID,
                            LocationIDPayee = locationPayee?.LocationID,
                            GovernmentIDPayee = governmentPayee?.GovernmentID,
                            PaidAmount = 0,
                            MaxAmount = 0
                        };

                        PostData post = new PostData(DataAccess.APIs.CompanyStudio, "AutomaticInvoicePaymentConfiguration/Post", newConfiguration);
                        post.AddLocationHeader(CompanyID, LocationID);
                        await post.ExecuteNoResult();
                    }
                }
            }
            finally { loader.Visible = false; cmdSave.Enabled = true; }

            Close();
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            long? cloneFromLocationID = (cboCloneAllFrom.SelectedItem as DropDownItem<Location>)?.Object.LocationID;
            foreach(DataGridViewRow row in dgvChosen.Rows)
            {
                DataGridViewComboBoxCell cloneFromCell = (DataGridViewComboBoxCell)row.Cells[colCloneFrom.Name];
                if (cloneFromCell.ReadOnly)
                {
                    continue;
                }

                Location locationPayee = row.Tag as Location;
                Government governmentPayee = row.Tag as Government;

                AutomaticInvoicePaymentConfiguration cloneFromConfig = _otherLocationConfigurations.FirstOrDefault(aipc => aipc.LocationIDConfiguredFor == cloneFromLocationID && aipc.LocationIDPayee == locationPayee?.LocationID && aipc.GovernmentIDPayee == governmentPayee?.GovernmentID);
                cloneFromCell.Value = cloneFromConfig?.AutomaticInvoicePaymentConfigurationID;
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
