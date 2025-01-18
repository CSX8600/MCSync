using CompanyStudio.Extensions;
using CompanyStudio.Models;
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
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace CompanyStudio.Store
{
    public partial class frmPricingAutomation : Form
    {
        public long? CompanyID { get; set; }
        public long? LocationID { get; set; }
        public ThemeBase Theme { get; set; }

        private long? AutomationID;
        private List<StorePricingAutomationLocation> Locations;
        public frmPricingAutomation()
        {
            InitializeComponent();
        }

        private async void frmPricingAutomation_Load(object sender, EventArgs e)
        {
            if (Theme != null)
            {
                studioFormExtender.ApplyStyle(this, Theme);
            }

            PermissionsManager.OnLocationPermissionChange += PermissionsManager_OnLocationPermissionChange;

            loader.BringToFront();
            loader.Visible = true;
            try
            {
                lstLocations.Items.Clear();

                GetData get = new GetData(DataAccess.APIs.CompanyStudio, "StorePricingAutomation/Get");
                get.AddLocationHeader(CompanyID, LocationID);
                StorePricingAutomation automation = await get.GetObject<StorePricingAutomation>();
                AutomationID = automation.StorePricingAutomationID;

                Locations = new List<StorePricingAutomationLocation>();
                if (automation != null)
                {
                    chkEnabled.Checked = automation.IsEnabled;
                    chkAdd.Checked = automation.PushAdd;
                    chkUpdate.Checked = automation.PushUpdate;
                    chkDelete.Checked = automation.PushDelete;

                    Locations = automation.StorePricingAutomationLocations;
                }

                get = new GetData(DataAccess.APIs.CompanyStudio, "Company/GetForEmployee");
                List<Company> companies = await get.GetObject<List<Company>>() ?? new List<Company>();
                foreach(Company company in companies)
                {
                    foreach(Location location in company.Locations.Where(l => l.LocationID != LocationID && (PermissionsManager.HasPermission(l.LocationID.Value, PermissionsManager.LocationWidePermissions.ManagePrices) || PermissionsManager.HasPermission(l.LocationID.Value, PermissionsManager.LocationWidePermissions.ManagePurchaseOrders))))
                    {
                        DropDownItem<Location> ddi = new DropDownItem<Location>(location, company.Name + " (" + location.Name + ")");
                        lstLocations.Items.Add(ddi);
                    }
                }

                int hiddenCount = 0;
                lstLocations.ClearSelected();
                foreach (StorePricingAutomationLocation location in Locations)
                {
                    DropDownItem<Location> ddi = lstLocations.Items.OfType<DropDownItem<Location>>().FirstOrDefault(x => x.Object.LocationID == location.LocationIDDestination);
                    if (ddi != null)
                    {
                        lstLocations.SelectedItems.Add(ddi);
                    }
                    else
                    {
                        hiddenCount++;
                    }
                }

                if (hiddenCount > 0)
                {
                    DropDownItem<Location> ddi = new DropDownItem<Location>(null, $"{hiddenCount} Hidden Locations")
                    {
                        BackgroundColor = lstLocations.BackColor,
                        FontStyle = FontStyle.Italic,
                        FontColor = Color.Gray
                    };
                    lstLocations.Items.Add(ddi);
                }
            }
            finally
            {
                loader.Visible = false;
            }
        }

        private void PermissionsManager_OnLocationPermissionChange(object sender, PermissionsManager.LocationWidePermissionChangeEventArgs e)
        {
            if (e.LocationID == LocationID && e.Permission == PermissionsManager.LocationWidePermissions.ManagePrices && !e.Value)
            {
                if (!PermissionsManager.HasPermission(LocationID.Value, PermissionsManager.LocationWidePermissions.ManagePrices) &&
                    !PermissionsManager.HasPermission(LocationID.Value, PermissionsManager.LocationWidePermissions.ManagePurchaseOrders))
                {
                    Close();
                }
            }
        }

        private void lstLocations_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawDropDownItems<Location>(lstLocations);
        }

        private void chkEnabled_CheckedChanged(object sender, EventArgs e)
        {
            grpProperties.Enabled = chkEnabled.Checked;
            grpLocations.Enabled = chkEnabled.Checked;
        }

        private async void cmdSave_Click(object sender, EventArgs e)
        {
            loader.BringToFront();
            loader.Visible = true;

            try
            {
                StorePricingAutomation automation = new StorePricingAutomation();
                automation.StorePricingAutomationID = AutomationID;
                automation.LocationID = LocationID;
                automation.IsEnabled = chkEnabled.Checked;
                automation.PushAdd = chkAdd.Checked;
                automation.PushUpdate = chkUpdate.Checked;
                automation.PushDelete = chkDelete.Checked;
                PutData put = new PutData(DataAccess.APIs.CompanyStudio, "StorePricingAutomation/Put", automation);
                put.AddLocationHeader(CompanyID, LocationID);
                await put.ExecuteNoResult();

                if (put.RequestSuccessful && chkEnabled.Checked)
                {
                    List<Location> locationsToAdd = lstLocations.SelectedItems.OfType<DropDownItem<Location>>().Select(x => x.Object).Where(x => !Locations.Any(l => l.LocationIDDestination == x.LocationID)).ToList();
                    foreach (Location locationToAdd in locationsToAdd)
                    {
                        StorePricingAutomationLocation automationLocation = new StorePricingAutomationLocation();
                        automationLocation.StorePricingAutomationID = automation.StorePricingAutomationID;
                        automationLocation.LocationIDDestination = locationToAdd.LocationID;

                        PostData post = new PostData(DataAccess.APIs.CompanyStudio, "StorePricingAutomationLocation/Post", automationLocation);
                        post.AddLocationHeader(CompanyID, LocationID);
                        await post.ExecuteNoResult();
                    }

                    List<StorePricingAutomationLocation> locationsToDelete = Locations.Where(x => !lstLocations.SelectedItems.OfType<DropDownItem<Location>>().Any(ddi => ddi.Object.LocationID == x.LocationIDDestination)).ToList();
                    foreach (StorePricingAutomationLocation locationToDelete in locationsToDelete)
                    {
                        DeleteData delete = new DeleteData(DataAccess.APIs.CompanyStudio, "StorePricingAutomationLocation/Delete/" + locationToDelete.StorePricingAutomationLocationID);
                        delete.AddLocationHeader(CompanyID, LocationID);
                        await delete.Execute();
                    }
                }
            }
            finally
            {
                loader.Visible = false;
            }

            Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmPricingAutomation_FormClosed(object sender, FormClosedEventArgs e)
        {
            PermissionsManager.OnLocationPermissionChange -= PermissionsManager_OnLocationPermissionChange;
        }
    }
}
