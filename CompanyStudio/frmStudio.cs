using CompanyStudio.Extensions;
using CompanyStudio.Models;
using FleetTracking;
using FleetTracking.Interop;
using MesaSuite.Common;
using MesaSuite.Common.Attributes;
using MesaSuite.Common.Collections;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using MesaSuite.Common.Utility;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace CompanyStudio
{
    [UriReachable("")]
    public partial class frmStudio : Form
    {
        Dictionary<string, ThemeBase> themes = new Dictionary<string, ThemeBase>();
        ThemeBase currentTheme = null;
        Dictionary<long, FleetTrackingApplication> fleetTrackingApplicationsByCompany = new Dictionary<long, FleetTrackingApplication>();

        private void InitializeThemeLookup()
        {
            themes.Add("dark", vS2015DarkTheme);
            themes.Add("light", vS2015LightTheme);
            themes.Add("blue", vS2015BlueTheme);
        }


        public event EventHandler<Company> OnCompanyAdded;
        public event EventHandler<Company> OnCompanyRemoved;

        Dictionary<Type, BaseCompanyStudioContent> formsByType = new Dictionary<Type, BaseCompanyStudioContent>();
        private Company _activeCompany;
        public Company ActiveCompany
        {
            get { return _activeCompany; }
            set
            {
                if (_activeCompany != null && fleetTrackingApplicationsByCompany.ContainsKey(_activeCompany.CompanyID.Value))
                {
                    FleetTrackingApplication fleetTrackingApplication = fleetTrackingApplicationsByCompany[_activeCompany.CompanyID.Value];
                    if (mnuBanner.Items.Contains(fleetTrackingApplication.GetNavigation()))
                    {
                        mnuBanner.Items.Remove(fleetTrackingApplication.GetNavigation());
                    }
                }

                _activeCompany = value;
                toolCompanyDropDown.Text = _activeCompany?.Name ?? "";

                financeToolStripMenuItem.Visible = PermissionsManager.HasPermission(_activeCompany?.CompanyID ?? -1, PermissionsManager.CompanyWidePermissions.ManageAccounts) ||
                                                   PermissionsManager.HasPermission(_activeLocation?.LocationID ?? -1, PermissionsManager.LocationWidePermissions.ManagePurchaseOrders) ||
                                                   PermissionsManager.HasPermission(_activeLocation?.LocationID ?? -1, PermissionsManager.LocationWidePermissions.ManageInvoices) ||
                                                   PermissionsManager.HasPermission(_activeCompany?.CompanyID ?? -1, PermissionsManager.CompanyWidePermissions.IssueWireTransfers);

                emailToolStripMenuItem.Visible = PermissionsManager.HasPermission(_activeCompany?.CompanyID ?? -1, PermissionsManager.CompanyWidePermissions.ManageEmails);
                employeesToolStripMenuItem.Visible = PermissionsManager.HasPermission(_activeCompany?.CompanyID ?? -1, PermissionsManager.CompanyWidePermissions.ManageEmployees);
                mnuLocationExplorer.Visible = PermissionsManager.HasPermission(_activeCompany?.CompanyID ?? -1, PermissionsManager.CompanyWidePermissions.ManageLocations);

                toolLocationDropDown.SelectedItem = null;
                toolLocationDropDown.Items.Clear();

                if (_activeCompany != null)
                {
                    foreach (Location location in _activeCompany.Locations)
                    {
                        toolLocationDropDown.Items.Add(new DropDownItem<Location>(location, location.Name));
                    }

                    toolLocationDropDown.SelectedItem = toolLocationDropDown.Items.Cast<DropDownItem<Location>>().FirstOrDefault();

                    if (fleetTrackingApplicationsByCompany.ContainsKey(_activeCompany.CompanyID.Value))
                    {
                        mnuBanner.Items.Add(fleetTrackingApplicationsByCompany[_activeCompany.CompanyID.Value].GetNavigation());
                        fleetTrackingApplicationsByCompany[_activeCompany.CompanyID.Value].VerifyNavigationVisibility();
                    }
                }
            }
        }

        private Location _activeLocation = null;
        public Location ActiveLocation
        {
            get => _activeLocation;
            set
            {
                _activeLocation = value;
                toolLocationDropDown.SelectedItem = toolLocationDropDown.Items.Cast<DropDownItem<Location>>().FirstOrDefault(ddi => ddi.Object == _activeLocation);

                financeToolStripMenuItem.Visible = PermissionsManager.HasPermission(_activeCompany?.CompanyID ?? -1, PermissionsManager.CompanyWidePermissions.ManageAccounts) ||
                                                   PermissionsManager.HasPermission(_activeLocation?.LocationID ?? -1, PermissionsManager.LocationWidePermissions.ManagePurchaseOrders) ||
                                                   PermissionsManager.HasPermission(_activeLocation?.LocationID ?? -1, PermissionsManager.LocationWidePermissions.ManageInvoices) ||
                                                   PermissionsManager.HasPermission(_activeCompany?.CompanyID ?? -1, PermissionsManager.CompanyWidePermissions.IssueWireTransfers);

                invoicingToolStripMenuItem.Visible = PermissionsManager.HasPermission(_activeLocation?.LocationID ?? -1, PermissionsManager.LocationWidePermissions.ManageInvoices);
                mnuWireTransfers.Visible = PermissionsManager.HasPermission(_activeCompany?.CompanyID ?? -1, PermissionsManager.CompanyWidePermissions.IssueWireTransfers);
                storeToolStripMenuItem.Visible = PermissionsManager.HasPermission(_activeLocation?.LocationID ?? -1, PermissionsManager.LocationWidePermissions.ManageRegisters) ||
                                                 PermissionsManager.HasPermission(_activeLocation?.LocationID ?? -1, PermissionsManager.LocationWidePermissions.ManagePrices);
            }
        }

        private List<Company> _companies = new List<Company>();
        public IReadOnlyCollection<Company> Companies => new List<Company>(_companies);

        public frmStudio()
        {
            InitializeComponent();
            InitializeThemeLookup();
            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
        }

        private void InitializeFleetTracking(FleetTrackingApplication fleetTrackingApplication)
        {
            fleetTrackingApplication.RegisterCallback(new FleetTrackingApplication.CallbackDelegates.OpenForm(FleetTracking_OpenForm));
            fleetTrackingApplication.RegisterCallback(new FleetTrackingApplication.CallbackDelegates.GetAccess<GetData>(FleetTracking_GetData));
            fleetTrackingApplication.RegisterCallback(new FleetTrackingApplication.CallbackDelegates.GetAccess<PutData>(FleetTracking_PutData));
            fleetTrackingApplication.RegisterCallback(new FleetTrackingApplication.CallbackDelegates.GetAccess<PostData>(FleetTracking_PostData));
            fleetTrackingApplication.RegisterCallback(new FleetTrackingApplication.CallbackDelegates.GetAccess<DeleteData>(FleetTracking_DeleteData));
            fleetTrackingApplication.RegisterCallback(new FleetTrackingApplication.CallbackDelegates.GetAccess<PatchData>(FleetTracking_PatchData));
            fleetTrackingApplication.RegisterCallback(new FleetTrackingApplication.CallbackDelegates.IsCurrentEntity((companyID, governmentID) => FleetTracking_IsCurrentEntity(fleetTrackingApplication, companyID, governmentID)));
            fleetTrackingApplication.RegisterCallback(new FleetTrackingApplication.CallbackDelegates.GetCurrentCompanyIDGovernmentID(() => ((long?)fleetTrackingApplicationsByCompany.FirstOrDefault(kvp => kvp.Value == fleetTrackingApplication).Key ?? null, null)));
            fleetTrackingApplication.RegisterCallback(new FleetTrackingApplication.CallbackDelegates.GetUsersForEntity(() => FleetTracking_GetUsersForEntity(fleetTrackingApplication)));
        }

        private void FleetTracking_AddNavigationItem(ToolStripItemCollection collection, FleetTrackingApplication.MainNavigationItem item)
        {
            ToolStripMenuItem tsmi = new ToolStripMenuItem(item.Text);
            tsmi.Name = "mnu" + item.Text.Replace(" ", "");
            if (item.SelectedAction != null)
            {
                tsmi.Click += (s, e) => item.SelectedAction.Invoke();
            }

            if (item.SubItems != null)
            {
                foreach (FleetTrackingApplication.MainNavigationItem subItem in item.SubItems)
                {
                    FleetTracking_AddNavigationItem(tsmi.DropDownItems, subItem);
                }
            }

            collection.Add(tsmi);
        }

        private Form FleetTracking_OpenForm(IFleetTrackingControl fleetTrackingControl, FleetTrackingApplication.OpenFormOptions formOptions, IWin32Window parent)
        {
            FleetTrackingForms.frmFleetForm fleetForm = new FleetTrackingForms.frmFleetForm();
            if (formOptions.HasFlag(FleetTrackingApplication.OpenFormOptions.ResizeToControl))
            {
                fleetForm.ClientSize = ((Control)fleetTrackingControl).Size;
            }
            fleetForm.FleetTrackingControl = fleetTrackingControl;
            DecorateStudioContent(fleetForm);

            if (formOptions.HasFlag(FleetTrackingApplication.OpenFormOptions.Popout))
            {
                if (parent == null)
                {
                    fleetForm.Show();
                }
                else
                {
                    fleetForm.Show(parent);
                }
            }
            else if (formOptions.HasFlag(FleetTrackingApplication.OpenFormOptions.Dialog))
            {
                if (parent == null)
                {
                    fleetForm.ShowDialog();
                }
                else
                {
                    fleetForm.ShowDialog(parent);
                }
            }
            else
            {
                fleetForm.Show(dockPanel, DockState.Document);
            }
            return fleetForm;
        }

        private TAccess FleetTracking_AppendHeaders<TAccess>(TAccess dataAccess) where TAccess : DataAccess
        {
            dataAccess.AddLocationHeader(ActiveCompany?.CompanyID, ActiveLocation?.LocationID);
            return dataAccess;
        }

        private GetData FleetTracking_GetData()
        {
            return FleetTracking_AppendHeaders(new GetData(DataAccess.APIs.FleetTracking, ""));
        }

        private PutData FleetTracking_PutData()
        {
            return FleetTracking_AppendHeaders(new PutData(DataAccess.APIs.FleetTracking, "", null));
        }

        private PostData FleetTracking_PostData()
        {
            return FleetTracking_AppendHeaders(new PostData(DataAccess.APIs.FleetTracking, ""));
        }

        private DeleteData FleetTracking_DeleteData()
        {
            return FleetTracking_AppendHeaders(new DeleteData(DataAccess.APIs.FleetTracking, ""));
        }

        private PatchData FleetTracking_PatchData()
        {
            return FleetTracking_AppendHeaders(new PatchData(DataAccess.APIs.FleetTracking, "", PatchData.PatchMethods.Replace, null, null));
        }

        private bool FleetTracking_IsCurrentEntity(FleetTrackingApplication application, long? companyID, long? governmentID)
        {
            return companyID != null && fleetTrackingApplicationsByCompany.GetOrDefault(companyID.Value) == application;
        }

        private async Task<List<FleetTracking.Models.User>> FleetTracking_GetUsersForEntity(FleetTrackingApplication fleetTrackingApplication)
        {
            long companyID = fleetTrackingApplicationsByCompany.First(kvp => kvp.Value == fleetTrackingApplication).Key;

            GetData get = new GetData(DataAccess.APIs.CompanyStudio, $"Employee/GetAllForCompany/{companyID}");
            FleetTracking_AppendHeaders(get);
            List<Employee> employees = await get.GetObject<List<Employee>>() ?? new List<Employee>();
            return employees.Select(e => new FleetTracking.Models.User() { UserID = e.UserID, Username = e.EmployeeName }).ToList();
        }

        private bool FleetTracking_SecurityPermissionCheck(string permission)
        {
            return PermissionsManager.HasPermissionFleetTrack(ActiveCompany?.CompanyID ?? 0L, permission);
        }

        public bool IsLoading = true;
        private async void frmStudio_Load(object sender, EventArgs e)
        {
            loader.BringToFront();
            loader.Visible = true;
            loader.Text = "Initializing Company Studio";
            string subKey = GlobalSettings.InternalEditionMode ? "MesaSuiteInternalEdition" : "MesaSuite";
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Clussman Productions\" + subKey);
            string theme = key.GetValue("CStudioTheme") as string;
            if (string.IsNullOrEmpty(theme) || !themes.ContainsKey(theme))
            {
                theme = "dark";
                key.SetValue("CStudioTheme", theme);
            }

            currentTheme = themes[theme];

            SetThemeMenuChecked();
            ApplyTheme();

            GetData getCompanies = new GetData(DataAccess.APIs.CompanyStudio, "Company/GetForEmployee");
            List<Company> companies = await getCompanies.GetObject<List<Company>>();
            if (getCompanies.RequestSuccessful)
            {
                companies.OrderBy(c => c.Name).ForEach(c => AddCompany(c));

                ActiveCompany = Companies.FirstOrDefault();
            }

            frmCompanyExplorer companyExplorer = GetForm<frmCompanyExplorer>();
            companyExplorer.Show(dockPanel, DockState.DockLeft);

            PermissionsManager.OnCompanyPermissionChange += PermissionsManager_OnCompanyPermissionChange;
            PermissionsManager.OnLocationPermissionChange += PermissionsManager_OnLocationPermissionChange;
            PermissionsManager.StartCheckThread((method) => Invoke(method));
            loader.Visible = false;

            bool showStart = UserPreferences.Get().GetPreferencesForSection("company").GetOrDefault("showStartPage", true) as bool? ?? true;

            if (showStart)
            {
                frmStartPage start = new frmStartPage();
                start.Studio = this;
                start.Show(dockPanel, DockState.Document);
            }

            IsLoading = false;
        }

        private void PermissionsManager_OnCompanyPermissionChange(object sender, PermissionsManager.CompanyWidePermissionChangeEventArgs e)
        {
            if (e.CompanyID == (ActiveCompany?.CompanyID ?? -1))
            {
                switch (e.Permission)
                {
                    case PermissionsManager.CompanyWidePermissions.ManageEmails:
                        emailToolStripMenuItem.Visible = e.Value;
                        break;
                    case PermissionsManager.CompanyWidePermissions.ManageEmployees:
                        employeesToolStripMenuItem.Visible = e.Value;
                        break;
                    case PermissionsManager.CompanyWidePermissions.ManageLocations:
                        mnuLocationExplorer.Visible = e.Value;
                        break;
                    case PermissionsManager.CompanyWidePermissions.ManageAccounts:
                        accountsToolStripMenuItem.Visible = e.Value;
                        break;
                }

                financeToolStripMenuItem.Visible = PermissionsManager.HasPermission(_activeCompany?.CompanyID ?? -1, PermissionsManager.CompanyWidePermissions.ManageAccounts) ||
                                                   PermissionsManager.HasPermission(_activeLocation?.LocationID ?? -1, PermissionsManager.LocationWidePermissions.ManagePurchaseOrders) ||
                                                   PermissionsManager.HasPermission(_activeLocation?.LocationID ?? -1, PermissionsManager.LocationWidePermissions.ManageInvoices) ||
                                                   PermissionsManager.HasPermission(_activeCompany?.CompanyID ?? -1, PermissionsManager.CompanyWidePermissions.IssueWireTransfers);
            }
        }

        private void PermissionsManager_OnLocationPermissionChange(object sender, PermissionsManager.LocationWidePermissionChangeEventArgs e)
        {
            if (e.LocationID == (ActiveLocation?.LocationID ?? 0))
            {
                switch (e.Permission)
                {
                    case PermissionsManager.LocationWidePermissions.ManageInvoices:
                        invoicingToolStripMenuItem.Visible = e.Value;
                        break;
                    case PermissionsManager.LocationWidePermissions.ManagePurchaseOrders:
                        purchasingFulfillmentToolStripMenuItem.Visible = e.Value;
                        break;
                }

                financeToolStripMenuItem.Visible = PermissionsManager.HasPermission(_activeCompany?.CompanyID ?? -1, PermissionsManager.CompanyWidePermissions.ManageAccounts) ||
                                                   PermissionsManager.HasPermission(_activeLocation?.LocationID ?? -1, PermissionsManager.LocationWidePermissions.ManagePurchaseOrders) ||
                                                   PermissionsManager.HasPermission(_activeLocation?.LocationID ?? -1, PermissionsManager.LocationWidePermissions.ManageInvoices) ||
                                                   PermissionsManager.HasPermission(_activeCompany?.CompanyID ?? -1, PermissionsManager.CompanyWidePermissions.IssueWireTransfers);
            }
        }

        private void SetThemeMenuChecked()
        {
            foreach (ToolStripMenuItem item in mnuThemes.DropDownItems)
            {
                item.Checked = false;
            }

            if (currentTheme == vS2015LightTheme)
            {
                mnuLightTheme.Checked = true;
            }
            else if (currentTheme == vS2015DarkTheme)
            {
                mnuDarkTheme.Checked = true;
            }
            else if (currentTheme == vS2015BlueTheme)
            {
                mnuBlueTheme.Checked = true;
            }
        }

        private void ApplyTheme()
        {
            if (dockPanel.Contents.OfType<BaseCompanyStudioContent>().Any(c => c.IsDirty))
            {
                MessageBox.Show("Please save all documents before switching themes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (MemoryStream memoryStream = new MemoryStream())
            {
                dockPanel.SaveAsXml(memoryStream, Encoding.UTF8);
                memoryStream.Position = 0;
                foreach (IDockContent content in dockPanel.Contents.ToList())
                {
                    content.DockHandler.Close();
                }

                currentTheme.ApplyTo(dockPanel);

                dockPanel.LoadFromXml(memoryStream, HandlePersistString);
            }
            toolStripExtender.SetStyle(mnuBanner, VisualStudioToolStripExtender.VsVersion.Vs2015, currentTheme);
            toolStripExtender.SetStyle(toolStrip, VisualStudioToolStripExtender.VsVersion.Vs2015, currentTheme);

            foreach (BaseCompanyStudioContent item in dockPanel.Contents.OfType<BaseCompanyStudioContent>())
            {
                item.Theme = currentTheme;
            }

            studioFormExtender.ApplyStyle(loader, currentTheme);
        }

        private IDockContent HandlePersistString(string persistString)
        {
            try
            {
                JObject jObject = JObject.Parse(persistString);
                Type studioType = Type.GetType(jObject.Property("type").Value.Value<string>());

                BaseCompanyStudioContent content = GetForm(studioType);
                content.HandlePersistString(persistString);

                return content;
            }
            catch { }

            Type nonStudioType = Type.GetType(persistString);
            return (IDockContent)Activator.CreateInstance(nonStudioType);
        }

        private void mnuTheme_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = (ToolStripMenuItem)sender;
            string theme = (string)menu.Tag;

            currentTheme = themes[theme];
            string subKey = GlobalSettings.InternalEditionMode ? "MesaSuiteInternalEdition" : "MesaSuite";
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Clussman Productions\" + subKey);
            key.SetValue("CStudioTheme", theme);

            SetThemeMenuChecked();
            ApplyTheme();
        }

        public T GetForm<T>(bool createNew = true) where T : BaseCompanyStudioContent
        {
            return GetForm(typeof(T), createNew) as T;
        }

        public BaseCompanyStudioContent GetForm(Type formType, bool createNew = true)
        {
            BaseCompanyStudioContent returnValue;
            if (!formsByType.ContainsKey(formType) || ((Form)formsByType[formType]).IsDisposed)
            {
                if (!createNew)
                {
                    return null;
                }
                else
                {
                    returnValue = (BaseCompanyStudioContent)Activator.CreateInstance(formType);
                    DecorateStudioContent(returnValue);

                    formsByType[formType] = returnValue;
                }
            }
            else
            {
                returnValue = formsByType[formType];
            }

            return returnValue;
        }

        public void DecorateStudioContent(BaseCompanyStudioContent content)
        {
            content.Theme = currentTheme;
            content.Studio = this;
            content.Company = ActiveCompany;
            content.OnIsDirtyChange += Content_OnIsDirtyChange;

            if (content is ILocationScoped locationScoped)
            {
                locationScoped.LocationModel = ActiveLocation;
            }
        }

        private void Content_OnIsDirtyChange(object sender, EventArgs e)
        {

        }

        public void AddCompany(Company company)
        {
            if (_companies.Any(c => c.CompanyID == company.CompanyID))
            {
                this.ShowError("You're already connected to this company");
                return;
            }

            _companies.Add(company);
            toolCompanyDropDown.Items.Add(company.Name);
            fleetTrackingApplicationsByCompany[company.CompanyID.Value] = new FleetTrackingApplication(FleetTracking_SecurityPermissionCheck);
            InitializeFleetTracking(fleetTrackingApplicationsByCompany[company.CompanyID.Value]);
            OnCompanyAdded?.Invoke(this, company);
        }

        public void RemoveCompany(Company company)
        {
            _companies.Remove(company);
            toolCompanyDropDown.Items.Remove(company.Name);

            if (company == ActiveCompany)
            {
                ActiveCompany = null;
            }

            if (fleetTrackingApplicationsByCompany.ContainsKey(company.CompanyID.Value))
            {
                fleetTrackingApplicationsByCompany[company.CompanyID.Value].Shutdown();
                fleetTrackingApplicationsByCompany.Remove(company.CompanyID.Value);
            }

            OnCompanyRemoved?.Invoke(this, company);
        }

        private void mnuCompanyExplorer_Click(object sender, EventArgs e)
        {
            GetForm<frmCompanyExplorer>().Show(dockPanel, DockState.DockLeft);
        }

        private void toolCompanyDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActiveCompany = Companies.FirstOrDefault(c => c.Name == (string)toolCompanyDropDown.SelectedItem);
        }

        string valueWhenCompanyDropDownEntered = null;
        private void toolCompanyDropDown_Enter(object sender, EventArgs e)
        {
            valueWhenCompanyDropDownEntered = toolCompanyDropDown.SelectedItem as string;
        }

        private void toolCompanyDropDown_Leave(object sender, EventArgs e)
        {
            if (!toolCompanyDropDown.Items.Contains(toolCompanyDropDown.Text))
            {
                toolCompanyDropDown.SelectedItem = valueWhenCompanyDropDownEntered;
            }

            valueWhenCompanyDropDownEntered = null;
        }

        private void mnuEmailExplorer_Click(object sender, EventArgs e)
        {
            foreach (Email.frmEmailExplorer openEmailExplorers in dockPanel.Contents.OfType<Email.frmEmailExplorer>())
            {
                if (openEmailExplorers.Company == ActiveCompany)
                {
                    openEmailExplorers.Activate();
                    return;
                }
            }

            Email.frmEmailExplorer emailExplorer = new Email.frmEmailExplorer();
            DecorateStudioContent(emailExplorer);
            emailExplorer.Show(dockPanel, DockState.DockRight);
        }

        private void toolSave_Click(object sender, EventArgs e)
        {
            if (dockPanel.ActiveDocument is ISaveable saveable)
            {
                try
                {
                    saveable.Save();
                }
                catch { }
            }
        }

        private void toolSaveAll_Click(object sender, EventArgs e)
        {
            foreach (ISaveable saveable in dockPanel.Documents.OfType<ISaveable>())
            {
                try
                {
                    saveable.Save();
                }
                catch { }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.S | Keys.Control | Keys.Shift:
                    toolSaveAll.PerformClick();
                    return true;
                case Keys.S | Keys.Control:
                    toolSave.PerformClick();
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void mnuEmployeeExplorer_Click(object sender, EventArgs e)
        {
            foreach (Employees.frmEmployeeExplorer openEmployeeExplorer in dockPanel.Contents.OfType<Employees.frmEmployeeExplorer>())
            {
                if (openEmployeeExplorer.Company == ActiveCompany)
                {
                    openEmployeeExplorer.Activate();
                    return;
                }
            }

            Employees.frmEmployeeExplorer employeeExplorer = new Employees.frmEmployeeExplorer();
            DecorateStudioContent(employeeExplorer);
            employeeExplorer.Show(dockPanel, DockState.DockRight);
        }

        private void frmStudio_FormClosing(object sender, FormClosingEventArgs e)
        {
            PermissionsManager.OnCompanyPermissionChange -= PermissionsManager_OnCompanyPermissionChange;
            PermissionsManager.OnLocationPermissionChange -= PermissionsManager_OnLocationPermissionChange;
            PermissionsManager.StopCheckThread();

            foreach (FleetTrackingApplication fleetTrackingApplication in fleetTrackingApplicationsByCompany.Values)
            {
                fleetTrackingApplication.Shutdown();
            }
        }

        private void mnuAccountExplorer_Click(object sender, EventArgs e)
        {
            Accounts.frmAccountExplorer accountExplorer = dockPanel.Contents.OfType<Accounts.frmAccountExplorer>().Where(acctExplorer => acctExplorer.Company == ActiveCompany).FirstOrDefault();

            if (accountExplorer != null)
            {
                accountExplorer.Activate();
                return;
            }

            accountExplorer = new Accounts.frmAccountExplorer();
            DecorateStudioContent(accountExplorer);
            accountExplorer.Show(dockPanel, DockState.DockRight);
        }

        private void mnuCategories_Click(object sender, EventArgs e)
        {
            Accounts.frmCategories categories = new Accounts.frmCategories();
            categories.Theme = currentTheme;
            categories.Company = ActiveCompany;
            categories.Show();
        }

        DropDownItem<Location> valueWhenLocationDropDownEntered = null;
        private void toolLocationDropDown_Enter(object sender, EventArgs e)
        {
            valueWhenLocationDropDownEntered = toolLocationDropDown.SelectedItem.Cast<DropDownItem<Location>>();
        }

        private void toolLocationDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActiveLocation = toolLocationDropDown.SelectedItem.Cast<DropDownItem<Location>>()?.Object;
        }

        private void toolLocationDropDown_Leave(object sender, EventArgs e)
        {
            DropDownItem<Location> selectedLocation = toolLocationDropDown.Items.Cast<DropDownItem<Location>>().FirstOrDefault(ddi => ddi.Text == toolLocationDropDown.Text);
            if (selectedLocation == null)
            {
                toolLocationDropDown.SelectedItem = valueWhenLocationDropDownEntered;
            }

            valueWhenLocationDropDownEntered = null;
        }

        private void mnuLocationExplorer_Click(object sender, EventArgs e)
        {
            CompanyForms.frmLocationExplorer existingLocationExplorer = dockPanel.Contents.OfType<CompanyForms.frmLocationExplorer>().FirstOrDefault(le => le.Company.CompanyID == ActiveCompany.CompanyID);

            if (existingLocationExplorer != null)
            {
                existingLocationExplorer.BringToFront();
                return;
            }

            CompanyForms.frmLocationExplorer locationExplorer = new CompanyForms.frmLocationExplorer();
            DecorateStudioContent(locationExplorer);
            locationExplorer.Show(dockPanel, DockState.DockRight);
        }

        private async void tmrLocationUpdater_Tick(object sender, EventArgs e)
        {
            if (ActiveCompany == null)
            {
                return;
            }

            tmrLocationUpdater.Enabled = false;

            try
            {
                bool shouldQuery = PermissionsManager.AccessibleLocationIDs.Except(ActiveCompany.Locations.Select(l => l.LocationID)).Any() || // Need to add
                                    Companies.SelectMany(c => c.Locations).Select(l => l.LocationID).Except(PermissionsManager.AccessibleLocationIDs).Any(); // Need to remove

                if (shouldQuery)
                {
                    GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Company/GetForEmployee");
                    List<Company> refreshedCompanies = await get.GetObject<List<Company>>() ?? new List<Company>();
                    Dictionary<long?, Company> companiesByCompanyID = refreshedCompanies.ToDictionary(c => c.CompanyID);

                    foreach (Company company in Companies.ToList())
                    {
                        Company refreshedCompany = companiesByCompanyID.GetOrDefault(company.CompanyID);
                        if (refreshedCompany == null)
                        {
                            RemoveCompany(company);
                            continue;
                        }

                        company.Locations = refreshedCompany.Locations;

                        if (company == ActiveCompany)
                        {
                            Location currentLocation = ActiveLocation;
                            ActiveCompany = company;

                            if (currentLocation != null)
                            {
                                ActiveLocation = company.Locations.FirstOrDefault(l => l.LocationID == currentLocation.LocationID);
                            }
                        }
                    }
                }
            }
            finally
            {
                tmrLocationUpdater.Enabled = true;
            }
        }

        private void mnuInvoicingReceivables_Click(object sender, EventArgs e)
        {
            Invoicing.frmAccountsReceivableExplorer explorer = dockPanel.Contents.OfType<Invoicing.frmAccountsReceivableExplorer>().FirstOrDefault(are => are.Company.CompanyID == ActiveCompany?.CompanyID);
            if (explorer != null)
            {
                explorer.Activate();
                return;
            }

            explorer = new Invoicing.frmAccountsReceivableExplorer();
            DecorateStudioContent(explorer);
            explorer.Show(dockPanel, DockState.DockRight);
        }

        private void mnuInvoicePayables_Click(object sender, EventArgs e)
        {
            Invoicing.frmPayableExplorer explorer = dockPanel.Contents.OfType<Invoicing.frmPayableExplorer>().FirstOrDefault(pe => pe.LocationModel.LocationID == ActiveLocation?.LocationID);
            if (explorer != null)
            {
                explorer.Activate();
                return;
            }

            explorer = new Invoicing.frmPayableExplorer();
            DecorateStudioContent(explorer);
            explorer.Show(dockPanel, DockState.DockRight);
        }

        private void financeToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            accountsToolStripMenuItem.Visible = PermissionsManager.HasPermission(_activeCompany?.CompanyID ?? -1, PermissionsManager.CompanyWidePermissions.ManageAccounts);
            purchasingFulfillmentToolStripMenuItem.Visible = PermissionsManager.HasPermission(_activeLocation?.LocationID ?? -1, PermissionsManager.LocationWidePermissions.ManagePurchaseOrders);
            invoicingToolStripMenuItem.Visible = PermissionsManager.HasPermission(_activeLocation?.LocationID ?? -1, PermissionsManager.LocationWidePermissions.ManageInvoices);
            mnuWireTransfers.Visible = PermissionsManager.HasPermission(_activeCompany?.CompanyID ?? -1, PermissionsManager.CompanyWidePermissions.IssueWireTransfers);
        }

        private void mnuWireTransfersHistoryExplorer_Click(object sender, EventArgs e)
        {
            WireTransfers.frmWireTransferHistoryExplorer explorer = dockPanel.Contents.OfType<WireTransfers.frmWireTransferHistoryExplorer>().FirstOrDefault(pe => pe.Company.CompanyID == ActiveCompany?.CompanyID);
            if (explorer != null)
            {
                explorer.Activate();
                return;
            }

            explorer = new WireTransfers.frmWireTransferHistoryExplorer();
            DecorateStudioContent(explorer);
            explorer.Show(dockPanel, DockState.Document);
        }

        /// <summary>
        /// Opens the Email Editor for the specified Email Implementation
        /// </summary>
        /// <param name="emailImplementationIDField">The field on Location which holds the EmailImplementationID</param>
        /// <param name="emailName">The name of the Email as specified in the database</param>
        /// <param name="formattedPutURL">The URL format in which to put the new Email Implementation ID (example: Location/SetEmailIDForInvoices/{0} where {0} is the new Email Implementation ID)</param>
        /// <returns></returns>
        private async Task OpenLocationBasedEmailEditor(string emailImplementationIDField, string emailName, string formattedPutURL)
        {
            loader.BringToFront();
            loader.Visible = true;

            GetData getLocationForEmailData = new GetData(DataAccess.APIs.CompanyStudio, $"Location/Get/{ActiveLocation?.LocationID}");
            getLocationForEmailData.RequestFields = new List<string>()
            {
                nameof(Models.Location.LocationID),
                emailImplementationIDField
            };
            getLocationForEmailData.AddLocationHeader(ActiveCompany?.CompanyID, ActiveLocation?.LocationID);
            Location location = await getLocationForEmailData.GetObject<Location>();
            loader.Visible = false;

            if (location == null)
            {
                return;
            }

            frmEmailEditor emailEditor = new frmEmailEditor()
            {
                CompanyID = ActiveCompany?.CompanyID,
                LocationID = ActiveLocation?.LocationID,
                EmailName = emailName,
                Theme = currentTheme,
                EmailImplementationID = typeof(Location).GetProperty(emailImplementationIDField).GetValue(location) as long?
            };

            if (emailEditor.ShowDialog() == DialogResult.OK)
            {
                loader.Visible = true;

                PutData putNewID = new PutData(DataAccess.APIs.CompanyStudio, string.Format(formattedPutURL, emailEditor.EmailImplementationID ?? -1L), new object());
                putNewID.AddLocationHeader(ActiveCompany?.CompanyID, ActiveLocation?.LocationID);
                await putNewID.ExecuteNoResult();

                loader.Visible = false;
            }
        }

        /// <summary>
        /// Opens the Email Editor for the specified Email Implementation
        /// </summary>
        /// <param name="emailImplementationIDField">The field on Location which holds the EmailImplementationID</param>
        /// <param name="emailName">The name of the Email as specified in the database</param>
        /// <param name="formattedPutURL">The URL format in which to put the new Email Implementation ID (example: Company/SetEmailIDForInvoices/{0} where {0} is the new Email Implementation ID)</param>
        /// <returns></returns>
        private async Task OpenCompanyBasedEmailEditor(string emailImplementationIDField, string emailName, string formattedPutURL)
        {
            loader.BringToFront();
            loader.Visible = true;

            GetData getCompanyForEmailData = new GetData(DataAccess.APIs.CompanyStudio, $"Company/Get/{ActiveCompany?.CompanyID}");
            getCompanyForEmailData.RequestFields = new List<string>()
            {
                nameof(Models.Company.CompanyID),
                emailImplementationIDField
            };
            getCompanyForEmailData.AddCompanyHeader(ActiveCompany?.CompanyID);
            Company company = await getCompanyForEmailData.GetObject<Company>();
            loader.Visible = false;

            if (company == null)
            {
                return;
            }

            frmEmailEditor emailEditor = new frmEmailEditor()
            {
                CompanyID = ActiveCompany?.CompanyID,
                EmailName = emailName,
                Theme = currentTheme,
                EmailImplementationID = typeof(Company).GetProperty(emailImplementationIDField).GetValue(company) as long?
            };

            if (emailEditor.ShowDialog() == DialogResult.OK)
            {
                loader.Visible = true;

                PutData putNewID = new PutData(DataAccess.APIs.CompanyStudio, string.Format(formattedPutURL, emailEditor.EmailImplementationID ?? -1L), new object());
                putNewID.AddLocationHeader(ActiveCompany?.CompanyID, ActiveLocation?.LocationID);
                await putNewID.ExecuteNoResult();

                loader.Visible = false;
            }
        }

        private async void mnuInvoicingEmailPayableReceived_Click(object sender, EventArgs e)
        {
            await OpenLocationBasedEmailEditor(nameof(Models.Location.EmailImplementationIDPayableInvoice), "Payable Invoice Received", "Invoice/PutEmailImplementationIDPayableInvoice/{0}");
        }

        private async void mnuInvoicingEmailReceivableReady_Click(object sender, EventArgs e)
        {
            await OpenLocationBasedEmailEditor(nameof(Models.Location.EmailImplementationIDReadyForReceipt), "Receivable Invoice Ready For Receipt", "Invoice/PutEmailImplementationIDReadyForReceipt/{0}");
        }

        private async void mnuWireTransferEmailConfiguration_Click(object sender, EventArgs e)
        {
            await OpenCompanyBasedEmailEditor(nameof(Company.EmailImplementationIDWireTransferHistory), "Wire Transfer Received", "WireTransferHistory/SetWireTransferEmailImplementationID/{0}");
        }

        private void mnuRegisters_Click(object sender, EventArgs e)
        {
            Store.frmRegisters registers = dockPanel.Contents.OfType<Store.frmRegisters>().FirstOrDefault(r => r.LocationModel.LocationID == ActiveLocation?.LocationID);
            if (registers != null)
            {
                registers.BringToFront();
                return;
            }

            registers = new Store.frmRegisters();
            DecorateStudioContent(registers);
            registers.Show(dockPanel, DockState.Document);
        }

        private void mnuStorePriceManager_Click(object sender, EventArgs e)
        {
            Store.frmStoreItems storeItems = dockPanel.Contents.OfType<Store.frmStoreItems>().FirstOrDefault(r => r.LocationModel.LocationID == ActiveLocation?.LocationID);
            if (storeItems != null)
            {
                storeItems.BringToFront();
                return;
            }

            storeItems = new Store.frmStoreItems();
            DecorateStudioContent(storeItems);
            storeItems.Show(dockPanel, DockState.Document);
        }

        private void storeToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            mnuRegisters.Visible = PermissionsManager.HasPermission(_activeLocation?.LocationID ?? 0, PermissionsManager.LocationWidePermissions.ManageRegisters);
            mnuStorePriceManager.Visible = PermissionsManager.HasPermission(_activeLocation?.LocationID ?? 0, PermissionsManager.LocationWidePermissions.ManagePrices);
            mnuPromotions.Visible = PermissionsManager.HasPermission(_activeLocation?.LocationID ?? 0, PermissionsManager.LocationWidePermissions.ManagePrices);
            mnuStoreConfiguration.Visible = PermissionsManager.HasPermission(_activeLocation?.LocationID ?? 0, PermissionsManager.LocationWidePermissions.ManagePrices);
            mnuStoreSalesReport.Visible = PermissionsManager.HasPermission(_activeLocation?.LocationID ?? 0, PermissionsManager.LocationWidePermissions.ManagePrices);
        }

        private void mnuStoreConfiguration_Click(object sender, EventArgs e)
        {
            Store.frmStoreConfiguration configuration = dockPanel.Contents.OfType<Store.frmStoreConfiguration>().FirstOrDefault(r => r.LocationModel.LocationID == ActiveLocation?.LocationID);
            if (configuration != null)
            {
                configuration.BringToFront();
                return;
            }

            configuration = new Store.frmStoreConfiguration();
            DecorateStudioContent(configuration);
            configuration.Show(dockPanel, DockState.Document);
        }

        private void mnuStoreSalesReport_Click(object sender, EventArgs e)
        {
            Store.frmStoreSales salesWizard = new Store.frmStoreSales();
            salesWizard.Company = ActiveCompany;
            salesWizard.LocationModel = ActiveLocation;
            salesWizard.StudioForm = this;
            salesWizard.ShowDialog();
        }

        private void mnuPromotions_Click(object sender, EventArgs e)
        {
            Store.frmPromotionExplorer promotionExplorer = dockPanel.Contents.OfType<Store.frmPromotionExplorer>().FirstOrDefault(r => r.LocationModel.LocationID == ActiveLocation?.LocationID);
            if (promotionExplorer != null)
            {
                promotionExplorer.BringToFront();
                return;
            }

            promotionExplorer = new Store.frmPromotionExplorer();
            DecorateStudioContent(promotionExplorer);
            promotionExplorer.Show(dockPanel, DockState.DockRight);
        }

        private void purchaseOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Purchasing.frmPurchaseOrderExplorer purchaseOrderExplorer = dockPanel.Contents.OfType<Purchasing.frmPurchaseOrderExplorer>().FirstOrDefault(explorer => explorer.LocationModel.LocationID == ActiveLocation?.LocationID);
            if (purchaseOrderExplorer != null)
            {
                purchaseOrderExplorer.BringToFront();
                return;
            }

            purchaseOrderExplorer = new Purchasing.frmPurchaseOrderExplorer();
            DecorateStudioContent(purchaseOrderExplorer);
            purchaseOrderExplorer.Show(dockPanel, DockState.DockRight);
        }

        public FleetTrackingApplication GetFleetTrackingApplication(long? companyID)
        {
            return fleetTrackingApplicationsByCompany.FirstOrDefault(kvp => kvp.Key == companyID).Value;
        }

        private void billsOfLadingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Purchasing.frmBillOfLadingExplorer purchaseOrderExplorer = dockPanel.Contents.OfType<Purchasing.frmBillOfLadingExplorer>().FirstOrDefault(explorer => explorer.LocationModel.LocationID == ActiveLocation?.LocationID);
            if (purchaseOrderExplorer != null)
            {
                purchaseOrderExplorer.BringToFront();
                return;
            }

            purchaseOrderExplorer = new Purchasing.frmBillOfLadingExplorer();
            DecorateStudioContent(purchaseOrderExplorer);
            purchaseOrderExplorer.Show(dockPanel, DockState.DockRight);
        }

        private async void receiveBillsOfLadingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await BillOfLading.AcceptMultiple(ActiveCompany?.CompanyID, ActiveLocation?.LocationID);

            Purchasing.frmBillOfLadingExplorer bolExplorer = dockPanel.Contents.OfType<Purchasing.frmBillOfLadingExplorer>().FirstOrDefault(explorer => explorer.LocationModel.LocationID == ActiveLocation?.LocationID);
            if (bolExplorer != null)
            {
                await bolExplorer.RefreshData();
            }
        }

        private void shippingReceivingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Purchasing.ShippingReceiving.frmShippingReceiving purchaseOrderExplorer = dockPanel.Contents.OfType<Purchasing.ShippingReceiving.frmShippingReceiving>().FirstOrDefault(explorer => explorer.LocationModel.LocationID == ActiveLocation?.LocationID);
            if (purchaseOrderExplorer != null)
            {
                purchaseOrderExplorer.BringToFront();
                return;
            }

            purchaseOrderExplorer = new Purchasing.ShippingReceiving.frmShippingReceiving();
            DecorateStudioContent(purchaseOrderExplorer);
            purchaseOrderExplorer.Show(dockPanel, DockState.Document);
        }

        private void toolQuotes_Click(object sender, EventArgs e)
        {
            Purchasing.frmQuoteExplorer quoteExplorer = dockPanel.Contents.OfType<Purchasing.frmQuoteExplorer>().FirstOrDefault(explorer => explorer.LocationModel.LocationID == ActiveLocation?.LocationID);
            if (quoteExplorer != null)
            {
                quoteExplorer.BringToFront();
                return;
            }

            quoteExplorer = new Purchasing.frmQuoteExplorer();
            DecorateStudioContent(quoteExplorer);
            quoteExplorer.Show(dockPanel, DockState.DockRight);
        }

        private void toolPurchasingPriceManager_Click(object sender, EventArgs e)
        {
            Store.frmStoreItems storeItems = dockPanel.Contents.OfType<Store.frmStoreItems>().FirstOrDefault(r => r.LocationModel.LocationID == ActiveLocation?.LocationID);
            if (storeItems != null)
            {
                storeItems.BringToFront();
                return;
            }

            storeItems = new Store.frmStoreItems();
            DecorateStudioContent(storeItems);
            storeItems.Show(dockPanel, DockState.Document);
        }

        private void tsmiStartPage_Click(object sender, EventArgs e)
        {
            frmStartPage startPage = new frmStartPage()
            {
                Studio = this
            };
            startPage.Show(dockPanel, DockState.Document);
        }

        private void tsmiAutomaticPayments_Click(object sender, EventArgs e)
        {
            Invoicing.frmAutomaticPaymentConfiguration configuration = dockPanel.Contents.OfType<Invoicing.frmAutomaticPaymentConfiguration>().FirstOrDefault(r => r.LocationModel.LocationID == ActiveLocation?.LocationID);
            if (configuration != null)
            {
                configuration.BringToFront();
                return;
            }

            configuration = new Invoicing.frmAutomaticPaymentConfiguration();
            DecorateStudioContent(configuration);
            configuration.Show(dockPanel, DockState.Document);
        }

        private void tsmiFulfillmentWizard_Click(object sender, EventArgs e)
        {
            Purchasing.Fulfillment.FulfillmentWizardController wizardController = new Purchasing.Fulfillment.FulfillmentWizardController(ActiveCompany?.CompanyID, ActiveLocation?.LocationID);
            wizardController.StartWizard();
        }
    }
}
