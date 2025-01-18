using CompanyStudio.Models;
using MesaSuite.Common.Collections;
using MesaSuite.Common.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyStudio.Email
{
    public partial class frmAlias : BaseCompanyStudioContent, ISaveable
    {
        public event EventHandler OnSave;
        public Alias Alias { get; set; }
        public frmAlias()
        {
            InitializeComponent();
        }

        private void frmAlias_Load(object sender, System.EventArgs e)
        {
            PermissionsManager.OnCompanyPermissionChange += PermissionsManager_OnPermissionChange;
            SetAliasFields();
        }

        private void PermissionsManager_OnPermissionChange(object sender, PermissionsManager.CompanyWidePermissionChangeEventArgs e)
        {
            if (Company.CompanyID == e.CompanyID && e.Permission == PermissionsManager.CompanyWidePermissions.ManageEmails && !e.Value)
            {
                IsDirty = false;
                Close();
            }
        }

        private void SetAliasFields()
        {
            if (Alias != null)
            {
                txtName.Text = Alias.AliasName;
                txtValue.Text = Alias.AliasValue;
            }

            IsDirty = false;
        }

        private void cmdSave_Click(object sender, System.EventArgs e)
        {
            Save();
        }

        private async Task Post()
        {
            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Domain/GetByDomainName");
            get.QueryString = new MultiMap<string, string>()
            {
                { "domainName", Company.EmailDomain }
            };
            get.Headers = new System.Collections.Generic.Dictionary<string, string>()
            {
                { "companyid", Company.CompanyID.ToString() }
            };

            Domain domain = await get.GetObject<Domain>();
            if (domain == null)
            {
                if (get.RequestSuccessful)
                {
                    MessageBox.Show("Could not locate domain name.  Contact a server administrator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return;
            }

            Alias newAlias = new Alias()
            {
                AliasDomainID = domain.DomainID,
                AliasName = txtName.Text,
                AliasValue = txtValue.Text
            };

            PostData post = new PostData(DataAccess.APIs.CompanyStudio, "Alias/Post", newAlias);
            post.Headers = new Dictionary<string, string>()
            {
                { "companyid", Company.CompanyID.ToString() }
            };
            newAlias = await post.Execute<Alias>();

            if (!post.RequestSuccessful)
            {
                return;
            }

            Alias = newAlias;

            IsDirty = false;
            Text = "Alias";
            OnSave?.Invoke(this, new EventArgs());
        }

        private async Task Put()
        {
            Alias newAlias = new Alias()
            {
                AliasID = Alias.AliasID,
                AliasDomainID = Alias.AliasDomainID,
                AliasName = txtName.Text,
                AliasValue = txtValue.Text
            };

            PutData put = new PutData(DataAccess.APIs.CompanyStudio, "Alias/Put", newAlias);
            put.Headers = new Dictionary<string, string>()
            {
                { "companyid", Company.CompanyID.ToString() }
            };
            await put.ExecuteNoResult();

            if (!put.RequestSuccessful)
            {
                return;
            }

            Alias = newAlias;

            IsDirty = false;
            OnSave?.Invoke(this, new EventArgs());
        }

        private void CheckDirty(object sender, EventArgs e)
        {
            IsDirty = false;

            if (!txtName.Text.Equals(Alias?.AliasName, StringComparison.OrdinalIgnoreCase))
            {
                IsDirty = true;
            }

            if (!txtValue.Text.Equals(Alias?.AliasValue, StringComparison.OrdinalIgnoreCase))
            {
                IsDirty = true;
            }
        }

        protected override JObject GetPersistObject()
        {
            return JObject.FromObject(Alias);
        }

        protected override void HandlePersistObject(JObject value)
        {
            Alias = value.Value<JObject>().ToObject<Alias>();

            SetAliasFields();
        }

        public async Task Save()
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Alias Name is a required field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtValue.Text))
            {
                MessageBox.Show("Alias Value is a required field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!txtName.Text.EndsWith("@" + Company.EmailDomain))
            {
                MessageBox.Show($"The alias must end with @{Company.EmailDomain}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Alias == null)
            {
                await Post();
            }
            else
            {
                await Put();
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmAlias_FormClosed(object sender, FormClosedEventArgs e)
        {
            PermissionsManager.OnCompanyPermissionChange -= PermissionsManager_OnPermissionChange;
        }
    }
}
