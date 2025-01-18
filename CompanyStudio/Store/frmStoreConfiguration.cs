using CompanyStudio.Extensions;
using CompanyStudio.Models;
using MesaSuite.Common.Data;
using MesaSuite.Common.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyStudio.Store
{
    public partial class frmStoreConfiguration : BaseCompanyStudioContent, ISaveable, ILocationScoped
    {

        public event EventHandler OnSave;
        public frmStoreConfiguration()
        {
            InitializeComponent();
        }
        public Location LocationModel { get; set; }

        private async void frmStoreConfiguration_Load(object sender, EventArgs e)
        {
            AppendCompanyLocationNameToWindowText();
            PermissionsManager.OnLocationPermissionChange += PermissionsManager_OnLocationPermissionChange;

            GetData get = new GetData(DataAccess.APIs.CompanyStudio, $"Location/Get/{LocationModel.LocationID}");
            get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            get.RequestFields.Add(nameof(Models.Location.AccountIDStoreRevenue));
            get.RequestFields.Add(nameof(Models.Location.EmailImplementationIDRegisterOffline));
            Location location = await get.GetObject<Location>() ?? new Location();
            cmdRegOffline.Tag = location.EmailImplementationIDRegisterOffline;

            get = new GetData(DataAccess.APIs.CompanyStudio, "Account/GetAllForUser");
            get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            List<Account> accounts = await get.GetObject<List<Account>>() ?? new List<Account>();

            foreach (Account account in accounts)
            {
                DropDownItem<Account> accountDDI = new DropDownItem<Account>(account, $"{account.AccountNumber} ({account.Description})");
                cboRevenueAccount.Items.Add(accountDDI);

                if (account.AccountID == location.AccountIDStoreRevenue)
                {
                    cboRevenueAccount.SelectedItem = accountDDI;
                }
            }
        }

        private void PermissionsManager_OnLocationPermissionChange(object sender, PermissionsManager.LocationWidePermissionChangeEventArgs e)
        {
            if (e.LocationID == LocationModel.LocationID && e.Permission == PermissionsManager.LocationWidePermissions.ManagePrices && !e.Value)
            {
                Close();
            }
        }

        public async Task Save()
        {
            Dictionary<string, object> patchData = new Dictionary<string, object>()
            {
                { nameof(Models.Location.AccountIDStoreRevenue), ((DropDownItem<Account>)cboRevenueAccount.SelectedItem)?.Object.AccountID }
            };
            PatchData patch = new PatchData(DataAccess.APIs.CompanyStudio, "Location/Patch", PatchData.PatchMethods.Replace, LocationModel.LocationID, patchData);
            patch.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            patch.RequestFields.Add(nameof(Models.Location.AccountIDStoreRevenue));
            await patch.Execute();
            if (patch.RequestSuccessful)
            {
                OnSave?.Invoke(this, new EventArgs());
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            Save();
            Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void cmdRegOffline_Click(object sender, EventArgs e)
        {
            frmEmailEditor emailEditor = new frmEmailEditor()
            {
                CompanyID = Company.CompanyID,
                LocationID = LocationModel.LocationID,
                EmailName = "Register Offline",
                EmailImplementationID = (long?)cmdRegOffline.Tag,
                Theme = Theme
            };
            if (emailEditor.ShowDialog() == DialogResult.OK)
            {
                Dictionary<string, object> patchData = new Dictionary<string, object>()
                {
                    { nameof(Models.Location.EmailImplementationIDRegisterOffline), emailEditor.EmailImplementationID }
                };

                PatchData patch = new PatchData(DataAccess.APIs.CompanyStudio, "Location/Patch", PatchData.PatchMethods.Replace, LocationModel.LocationID, patchData);
                patch.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                patch.RequestFields.Add(nameof(Models.Location.EmailImplementationIDRegisterOffline));
                await patch.Execute();

                if (patch.RequestSuccessful)
                {
                    cmdRegOffline.Tag = emailEditor.EmailImplementationID;
                }
            }
        }

        private void frmStoreConfiguration_FormClosed(object sender, FormClosedEventArgs e)
        {
            PermissionsManager.OnLocationPermissionChange -= PermissionsManager_OnLocationPermissionChange;
        }
    }
}
