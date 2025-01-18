using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CompanyStudio.Extensions;
using CompanyStudio.Models;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;

namespace CompanyStudio.CompanyForms
{
    public partial class frmLocation : BaseCompanyStudioContent, ISaveable
    {
        private bool _isDirty;
        private LocationEmployees _locationEmployees = null;
        private LocationGovernments _locationGovernments = null;
        
        public Location LocationModel { get; set; }
        public frmLocation()
        {
            InitializeComponent();
        }

        public event EventHandler OnSave;

        public async Task Save()
        {
            loader.BringToFront();
            loader.Visible = true;

            if (string.IsNullOrEmpty(txtName.Text))
            {
                this.ShowError("Name is a required field.");
                return;
            }

            Location locationToSave = LocationModel;
            if (locationToSave == null)
            {
                locationToSave = new Location();
                locationToSave.CompanyID = Company.CompanyID;
            }

            locationToSave.Name = txtName.Text;
            locationToSave.InvoiceNumberPrefix = txtInvoiceNumberPrefix.Text;
            locationToSave.NextInvoiceNumber = txtNextInvoiceNumber.Text;

            bool saveSuccessful = false;
            if (locationToSave.LocationID == default)
            {
                PostData post = new PostData(DataAccess.APIs.CompanyStudio, "Location/Post", locationToSave);
                post.AddCompanyHeader(Company.CompanyID);
                post.RequestFields = frmLocationExplorer.LOCATION_FIELDS;
                locationToSave = await post.Execute<Location>();

                if (post.RequestSuccessful)
                {
                    LocationModel = locationToSave;
                    _isDirty = false;
                    saveSuccessful = true;
                }
            }
            else
            {
                PutData put = new PutData(DataAccess.APIs.CompanyStudio, "Location/Put", locationToSave);
                put.AddCompanyHeader(Company.CompanyID);
                put.RequestFields = frmLocationExplorer.LOCATION_FIELDS;
                locationToSave = await put.Execute<Location>();

                if (put.RequestSuccessful)
                {
                    LocationModel = locationToSave;
                    _isDirty = false;
                    saveSuccessful = true;
                }
            }

            if (saveSuccessful)
            {
                _locationEmployees.LocationModel = LocationModel;
                await _locationEmployees.Save();

                _locationGovernments.LocationModel = LocationModel;
                await _locationGovernments.Save();
            }

            OnSave?.Invoke(this, new EventArgs());
            Text = LocationModel?.Name ?? "New Location";
            loader.Visible = false;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            _isDirty = true;
        }

        private void frmLocation_Load(object sender, EventArgs e)
        {
            txtName.Text = LocationModel?.Name;
            txtInvoiceNumberPrefix.Text = LocationModel?.InvoiceNumberPrefix;
            txtNextInvoiceNumber.Text = LocationModel?.NextInvoiceNumber;

            Text = LocationModel?.Name ?? "New Location";

            _locationEmployees = new LocationEmployees(Company, LocationModel);
            _locationEmployees.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            _locationEmployees.Size = tabEmployees.Size;
            tabEmployees.Controls.Add(_locationEmployees);

            _locationGovernments = new LocationGovernments(Company, LocationModel);
            _locationGovernments.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            _locationGovernments.Size = tabGovernments.Size;
            tabGovernments.Controls.Add(_locationGovernments);

            PermissionsManager.OnCompanyPermissionChange += PermissionsManager_OnCompanyPermissionChange;

            studioFormExtender.ApplyStyle(_locationEmployees, Theme);
            studioFormExtender.ApplyStyle(_locationGovernments, Theme);
        }

        private void PermissionsManager_OnCompanyPermissionChange(object sender, PermissionsManager.CompanyWidePermissionChangeEventArgs e)
        {
            if (e.CompanyID == Company.CompanyID && e.Permission == PermissionsManager.CompanyWidePermissions.ManageLocations && !e.Value)
            {
                _isDirty = false;
                Close();
            }
        }

        private void frmLocation_FormClosed(object sender, FormClosedEventArgs e)
        {
            PermissionsManager.OnCompanyPermissionChange -= PermissionsManager_OnCompanyPermissionChange;
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
