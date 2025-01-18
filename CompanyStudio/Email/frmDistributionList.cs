using CompanyStudio.Models;
using MesaSuite.Common.Collections;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyStudio.Email
{
    public partial class frmDistributionList : BaseCompanyStudioContent, ISaveable
    {
        public event EventHandler OnSave;
        public DistributionList DistributionList { get; set; }

        public frmDistributionList()
        {
            InitializeComponent();
        }

        private void rdoSpecific_CheckedChanged(object sender, EventArgs e)
        {
            txtSpecific.Enabled = rdoSpecific.Checked;

            SetIsDirty(sender, e);
        }

        private async void frmDistributionList_Load(object sender, EventArgs e)
        {
            PermissionsManager.OnCompanyPermissionChange += PermissionsManager_OnPermissionChange;
            if (DistributionList != null)
            {
                txtAddress.Text = DistributionList.DistributionListAddress;
                rdoPublic.Checked = DistributionList.DistributionListMode == 0;
                rdoMembers.Checked = DistributionList.DistributionListMode == 1;
                rdoSpecific.Checked = DistributionList.DistributionListMode == 2;
                txtSpecific.Enabled = rdoSpecific.Checked;
                txtSpecific.Text = DistributionList.DistributionListRequireAddress;

                await LoadDistributionListRecipients();

                IsDirty = false;
            }

            TryAddNewRecipientItem();
        }

        private void PermissionsManager_OnPermissionChange(object sender, PermissionsManager.CompanyWidePermissionChangeEventArgs e)
        {
            if (Company.CompanyID == e.CompanyID && e.Permission == PermissionsManager.CompanyWidePermissions.ManageEmails && !e.Value)
            {
                IsDirty = false;
                Close();
            }
        }

        private void TryAddNewRecipientItem()
        {
            if (!lstRecipients.Items.Any(lvi => "add".Equals(lvi.Tag)))
            {
                ListViewItem addItem = new ListViewItem()
                {
                    Text = "Click to add recipient"
                };
                addItem.Font = new Font(addItem.Font, FontStyle.Italic);
                addItem.Tag = "add";

                lstRecipients.Items.Add(addItem);
            }
        }

        private async Task LoadDistributionListRecipients()
        {
            loader.BringToFront();
            loader.Visible = true;
            lstRecipients.Items.Clear();

            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "DistributionListRecipient/GetByDistributionListID");
            get.QueryString = new MultiMap<string, string>()
            {
                { "id", DistributionList.DistributionListID.ToString() }
            };
            get.Headers = new Dictionary<string, string>()
            {
                { "companyid", Company.CompanyID.ToString() }
            };

            List<DistributionListRecipient> recipients = await get.GetObject<List<DistributionListRecipient>>();
            foreach (DistributionListRecipient distributionListRecipient in recipients)
            {
                lstRecipients.Items.Add(distributionListRecipient.DistributionListRecipientAddress);
            }

            TryAddNewRecipientItem();

            loader.Visible = false;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lstRecipients_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            ListViewItem item = lstRecipients.Items[e.Item];
            if (string.IsNullOrEmpty(e.Label))
            {
                item.Text = "Click to add recipient";
            }
            else
            {
                item.Tag = null;
                item.Font = new Font(item.Font, FontStyle.Regular);

                ListViewItem newAddItem = new ListViewItem("Click to add recipient");
                newAddItem.Tag = "add";
                newAddItem.Font = new Font(newAddItem.Font, FontStyle.Italic);
                lstRecipients.Items.Add(newAddItem);
            }

            SetIsDirty(sender, e);
        }

        private void lstRecipients_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            ListViewItem item = lstRecipients.Items[e.Item];
            if (!"add".Equals(item.Tag))
            {
                e.CancelEdit = true;
            }
        }

        private void lstRecipients_Click(object sender, EventArgs e)
        {
            if (lstRecipients.SelectedItems.Count == 1 && "add".Equals(lstRecipients.SelectedItems[0].Tag))
            {
                ListViewItem item = lstRecipients.SelectedItems[0];
                item.Text = string.Empty;
                item.BeginEdit();
            }
        }

        private void lstRecipients_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                List<ListViewItem> listViewItems = lstRecipients.SelectedItems.Where(lvi => !"add".Equals(lvi.Tag)).ToList();

                foreach(ListViewItem lvi in listViewItems)
                {
                    lstRecipients.Items.Remove(lvi);
                }

                SetIsDirty(sender, e);
            }
        }

        private void SetIsDirty(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        public async Task Save()
        {
            if (string.IsNullOrEmpty(txtAddress.Text))
            {
                this.ShowError("Address is a required field.");
                return;
            }

            if (!rdoPublic.Checked && !rdoMembers.Checked && !rdoSpecific.Checked)
            {
                this.ShowError("Send Type is a required field.");
                return;
            }

            if (rdoSpecific.Checked && string.IsNullOrEmpty(txtSpecific.Text))
            {
                this.ShowError("A specific email address is required when Specific Send Type is selected.");
                return;
            }

            if (DistributionList == null)
            {
                GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Domain/GetByDomainName");
                get.Headers.Add("companyid", Company.CompanyID.ToString());
                get.QueryString.Add("domainName", Company.EmailDomain);
                Domain domain = await get.GetObject<Domain>();

                DistributionList newList = new DistributionList()
                {
                    DistributionListDomainID = domain.DomainID,
                    DistributionListAddress = txtAddress.Text,
                    DistributionListMode = rdoPublic.Checked ? (byte)0 :
                                            rdoMembers.Checked ? (byte)1 :
                                            (byte)2,
                    DistributionListRequireAddress = rdoSpecific.Checked ? txtSpecific.Text : string.Empty
                };

                PostData post = new PostData(DataAccess.APIs.CompanyStudio, "DistributionList/Post", newList);
                post.Headers.Add("companyid", Company.CompanyID.ToString());
                newList = await post.Execute<DistributionList>();

                if (!post.RequestSuccessful)
                {
                    return;
                }

                DistributionList = newList;
            }
            else
            {
                DistributionList.DistributionListAddress = txtAddress.Text;
                DistributionList.DistributionListMode = rdoPublic.Checked ? (byte)0 :
                                                        rdoMembers.Checked ? (byte)1 :
                                                        (byte)2;
                DistributionList.DistributionListRequireAddress = rdoSpecific.Checked ? txtSpecific.Text : string.Empty;

                PutData put = new PutData(DataAccess.APIs.CompanyStudio, "DistributionList/Put", DistributionList);
                put.Headers.Add("companyid", Company.CompanyID.ToString());
                await put.ExecuteNoResult();

                if (!put.RequestSuccessful)
                {
                    return;
                }
            }

            GetData getData = new GetData(DataAccess.APIs.CompanyStudio, "DistributionListRecipient/GetByDistributionListID");
            getData.Headers.Add("companyid", Company.CompanyID.ToString());
            getData.QueryString.Add("id", DistributionList.DistributionListID.ToString());
            List<DistributionListRecipient> existingRecipients = await getData.GetObject<List<DistributionListRecipient>>();

            if (!getData.RequestSuccessful)
            {
                return;
            }

            bool dlrErrored = false;
            List<DistributionListRecipient> handledDistributionListRecipients = new List<DistributionListRecipient>();
            foreach (ListViewItem item in lstRecipients.Items.Where(lvi => !"add".Equals(lvi.Tag)))
            {
                DistributionListRecipient existingRecipient = existingRecipients.Where(dlr => dlr.DistributionListRecipientAddress.Equals(item.Text, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (existingRecipient == null)
                {
                    DistributionListRecipient newRecipient = new DistributionListRecipient()
                    {
                        DistributionListRecipientListID = DistributionList.DistributionListID,
                        DistributionListRecipientAddress = item.Text
                    };

                    PostData put = new PostData(DataAccess.APIs.CompanyStudio, "DistributionListRecipient/Post", newRecipient);
                    put.Headers.Add("companyid", Company.CompanyID.ToString());
                    await put.ExecuteNoResult();

                    if (!put.RequestSuccessful)
                    {
                        dlrErrored = true;
                    }
                }
                else
                {
                    handledDistributionListRecipients.Add(existingRecipient);
                }
            }

            foreach (DistributionListRecipient deletedRecipient in existingRecipients.Except(handledDistributionListRecipients))
            {
                DeleteData delete = new DeleteData(DataAccess.APIs.CompanyStudio, "DistributionListRecipient/Delete");
                delete.Headers.Add("companyid", Company.CompanyID.ToString());
                delete.QueryString.Add("id", deletedRecipient.DistributionListRecipientID.ToString());
                await delete.Execute();

                if (!delete.RequestSuccessful)
                {
                    dlrErrored = true;
                }
            }

            if (dlrErrored)
            {
                MessageBox.Show("Some Distribution List Recipients were not saved/deleted successfully.  Recommend reviewing list and trying again.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            await LoadDistributionListRecipients();

            IsDirty = false;

            OnSave?.Invoke(this, new EventArgs());
        }

        private void frmDistributionList_FormClosed(object sender, FormClosedEventArgs e)
        {
            PermissionsManager.OnCompanyPermissionChange -= PermissionsManager_OnPermissionChange;
        }
    }
}
