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
using MesaSuite.Common.Attributes;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;

namespace CompanyStudio.Store
{
    [UriReachable("register/{RegisterID}")]
    public partial class frmRegister : BaseCompanyStudioContent, ILocationScoped, ISaveable
    {
        public long? RegisterID { get; set; }
        public Location LocationModel { get; set; }
        public frmRegister()
        {
            InitializeComponent();
        }

        public event EventHandler OnSave;

        private void frmRegister_Load(object sender, EventArgs e)
        {
            PermissionsManager.OnLocationPermissionChange += PermissionsManager_OnLocationPermissionChange;
            LoadForm();
        }

        private void PermissionsManager_OnLocationPermissionChange(object sender, PermissionsManager.LocationWidePermissionChangeEventArgs e)
        {
            if (e.LocationID == LocationModel.LocationID && e.Permission == PermissionsManager.LocationWidePermissions.ManageRegisters && !e.Value)
            {
                Close();
            }
        }

        private async Task LoadForm()
        {
            if (RegisterID == null)
            {
                cmdOnline.Enabled = false;
                cmdOffline.Enabled = false;
                return;
            }

            cmdOnline.Enabled = true;
            cmdOffline.Enabled = true;
            try
            {
                loader.BringToFront();
                loader.Visible = true;

                dgvStatuses.Rows.Clear();

                GetData get = new GetData(DataAccess.APIs.CompanyStudio, $"Register/Get/{RegisterID}");
                get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                Register register = await get.GetObject<Register>() ?? new Register();

                Text = register.Name;
                txtName.Text = register.Name;
                txtIdentifier.Text = register.Identifier?.ToString();
                lblCurrentStatus.Text = register.CurrentStatus?.Status.ToString().ToDisplayName();

                get = new GetData(DataAccess.APIs.CompanyStudio, $"RegisterStatus/GetForRegister/{RegisterID}");
                get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                List<RegisterStatus> statusList = await get.GetObject<List<RegisterStatus>>() ?? new List<RegisterStatus>();
                foreach(RegisterStatus status in statusList.OrderByDescending(s => s.ChangeTime))
                {
                    int rowIndex = dgvStatuses.Rows.Add();
                    DataGridViewRow row = dgvStatuses.Rows[rowIndex];
                    row.Cells[colTime.Name].Value = status.ChangeTime?.ToString("MM/dd/yyyy HH:mm");
                    row.Cells[colStatus.Name].Value = status.Status.ToString().ToDisplayName();
                    row.Cells[colInitiator.Name].Value = status.Initiator;
                    row.Tag = status;
                }
            }
            finally
            {
                loader.Visible = false;
            }
        }

        private async void cmdSave_Click(object sender, EventArgs e)
        {
            await Save();
        }

        public async Task Save()
        {
            try
            {
                loader.BringToFront();
                loader.Visible = true;

                Register register = new Register()
                {
                    RegisterID = RegisterID,
                    Name = txtName.Text,
                    Identifier = string.IsNullOrEmpty(txtIdentifier.Text) ? (Guid?)null : new Guid(txtIdentifier.Text),
                    LocationID = LocationModel.LocationID
                };

                if (RegisterID == null)
                {
                    PostData post = new PostData(DataAccess.APIs.CompanyStudio, "Register/Post", register);
                    post.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                    Register newModel = await post.Execute<Register>();
                    if (post.RequestSuccessful)
                    {
                        RegisterID = newModel.RegisterID;
                        await LoadForm();
                    }
                }
                else
                {
                    PutData put = new PutData(DataAccess.APIs.CompanyStudio, "Register/Put", register);
                    put.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                    await put.ExecuteNoResult();
                    if (put.RequestSuccessful)
                    {
                        Close();
                    }
                }
            }
            finally
            {
                loader.Visible = false;
                OnSave?.Invoke(this, EventArgs.Empty);
            }
        }

        private async void cmdOnline_Click(object sender, EventArgs e)
        {
            await SetStatus(RegisterStatus.Statuses.Online);
        }

        private async Task SetStatus(RegisterStatus.Statuses status)
        {
            try
            {
                loader.BringToFront();
                loader.Visible = true;

                RegisterStatus registerStatus = new RegisterStatus();
                registerStatus.RegisterID = RegisterID;
                registerStatus.Status = status;
            
                PostData post = new PostData(DataAccess.APIs.CompanyStudio, "RegisterStatus/Post", registerStatus);
                post.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                await post.ExecuteNoResult();
                if (post.RequestSuccessful)
                {
                    await LoadForm();
                }
            }
            finally { loader.Visible = false; }
        }

        private async void cmdOffline_Click(object sender, EventArgs e)
        {
            await SetStatus(RegisterStatus.Statuses.Offline);
        }

        private void frmRegister_FormClosed(object sender, FormClosedEventArgs e)
        {
            PermissionsManager.OnLocationPermissionChange -= PermissionsManager_OnLocationPermissionChange;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
