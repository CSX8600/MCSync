﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GovernmentPortal.Extensions;
using GovernmentPortal.Models;
using GovernmentPortal.Officials;
using MesaSuite.Common.Data;

namespace GovernmentPortal
{
    public partial class frmPortal : Form
    {
        private Government _government = null;
        private Dictionary<PermissionsManager.Permissions, ToolStripItem> _toolStripItemsByPermission = new Dictionary<PermissionsManager.Permissions, ToolStripItem>();
        private FleetTracking.Interop.FleetTrackingApplication _fleetTrackingApplication;
        private ToolStripMenuItem mnuFleetTracking;

        public frmPortal()
        {
            InitializeComponent();
            InitializeFleetTracking();
        }

        private void SetupPermissions()
        {
            _toolStripItemsByPermission = new Dictionary<PermissionsManager.Permissions, ToolStripItem>()
            {
                { PermissionsManager.Permissions.ManageOfficials, toolOfficials },
                { PermissionsManager.Permissions.ManageEmails, toolEmail },
                { PermissionsManager.Permissions.ManageAccounts, toolAccounts },
                { PermissionsManager.Permissions.CanMintCurrency, tsbMintCurrency },
                { PermissionsManager.Permissions.ManageTaxes, tsmiTaxes },
                { PermissionsManager.Permissions.ManageInvoices, mnuInvoices },
                { PermissionsManager.Permissions.IssueWireTransfers, mnuWireTransfers }
            };
        }

        private void frmPortal_Shown(object sender, EventArgs e)
        {
            frmSelectGovernment selectGovernment = new frmSelectGovernment();
            if (DialogResult.Cancel == selectGovernment.ShowDialog())
            {
                Close();
                return;
            }

            _government = selectGovernment.SelectedGovernment;
            UpdateMenuVisibility();
        }

        private void UpdateMenuVisibility()
        {
            bool shouldShowFinanceToolstrip = false;
            foreach (KeyValuePair<PermissionsManager.Permissions, ToolStripItem> kvp in _toolStripItemsByPermission)
            {
                if (_government == null)
                {
                    kvp.Value.Visible = false;
                    continue;
                }

                if (kvp.Value.OwnerItem == toolFinance)
                {
                    shouldShowFinanceToolstrip |= PermissionsManager.HasPermission(_government.GovernmentID.Value, kvp.Key);
                }

                kvp.Value.Visible = PermissionsManager.HasPermission(_government.GovernmentID.Value, kvp.Key);
            }

            toolFinance.Visible = shouldShowFinanceToolstrip;
            mnuFleetTracking.Visible = _government != null;
        }

        private void toolOfficials_Click(object sender, EventArgs e)
        {
            frmGenericExplorer<Official> genericExplorer = new frmGenericExplorer<Official>(new OfficialExplorerContext(_government.GovernmentID.Value, _government.CanMintCurrency));
            genericExplorer.MdiParent = this;
            genericExplorer.Show();
        }

        private void frmPortal_Load(object sender, EventArgs e)
        {
            SetupPermissions();
            PermissionsManager.OnPermissionChange += PermissionsManager_OnPermissionChange;
            PermissionsManager.StartCheckThread(action => Invoke(action));
        }

        private void InitializeFleetTracking()
        {
            _fleetTrackingApplication = new FleetTracking.Interop.FleetTrackingApplication();
            _fleetTrackingApplication.RegisterCallback(new FleetTracking.Interop.FleetTrackingApplication.CallbackDelegates.OpenForm(FleetTracking_OpenForm));
            _fleetTrackingApplication.RegisterCallback(new FleetTracking.Interop.FleetTrackingApplication.CallbackDelegates.GetAccess<GetData>(FleetTracking_GetData));
            _fleetTrackingApplication.RegisterCallback(new FleetTracking.Interop.FleetTrackingApplication.CallbackDelegates.GetAccess<PutData>(FleetTracking_PutData));
            _fleetTrackingApplication.RegisterCallback(new FleetTracking.Interop.FleetTrackingApplication.CallbackDelegates.GetAccess<PostData>(FleetTracking_PostData));
            _fleetTrackingApplication.RegisterCallback(new FleetTracking.Interop.FleetTrackingApplication.CallbackDelegates.GetAccess<DeleteData>(FleetTracking_DeleteData));
            _fleetTrackingApplication.RegisterCallback(new FleetTracking.Interop.FleetTrackingApplication.CallbackDelegates.GetAccess<PatchData>(FleetTracking_PatchData));
            _fleetTrackingApplication.RegisterCallback(new FleetTracking.Interop.FleetTrackingApplication.CallbackDelegates.IsCurrentEntity(FleetTracking_IsCurrentEntity));

            mnuFleetTracking = new ToolStripMenuItem("Fleet Tracking");
            foreach (FleetTracking.Interop.FleetTrackingApplication.MainNavigationItem mainNavigationItem in _fleetTrackingApplication.GetNavigationItems())
            {
                FleetTracking_AddNavigationItem(mnuFleetTracking.DropDownItems, mainNavigationItem);
            }

            mnuFleetTracking.Visible = false;
            toolStrip1.Items.Add(mnuFleetTracking);
        }

        private void FleetTracking_AddNavigationItem(ToolStripItemCollection collection, FleetTracking.Interop.FleetTrackingApplication.MainNavigationItem item)
        {
            ToolStripMenuItem tsmi = new ToolStripMenuItem(item.Text);

            if (item.SelectedAction != null)
            {
                tsmi.Click += (s, e) => item.SelectedAction.Invoke();
            }

            if (item.SubItems != null)
            {
                foreach(FleetTracking.Interop.FleetTrackingApplication.MainNavigationItem subItem in item.SubItems)
                {
                    FleetTracking_AddNavigationItem(tsmi.DropDownItems, subItem);
                }
            }

            collection.Add(tsmi);
        }

        private Form FleetTracking_OpenForm(FleetTracking.IFleetTrackingControl fleetTrackingControl, FleetTracking.Interop.FleetTrackingApplication.OpenFormOptions openFormOptions)
        {
            Fleet.frmFleetForm fleetForm = new Fleet.frmFleetForm()
            {
                MdiParent = this
            };
            fleetForm.FleetTrackingControl = fleetTrackingControl;
            fleetForm.Show();
            return fleetForm;
        }

        private TAccess FleetTracking_AppendHeaders<TAccess>(TAccess dataAccess) where TAccess : DataAccess
        {
            dataAccess.AddGovHeader(_government.GovernmentID.Value);
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

        private bool FleetTracking_IsCurrentEntity(long? companyID, long? governmentID)
        {
            return _government.GovernmentID == governmentID;
        }

        private void PermissionsManager_OnPermissionChange(object sender, PermissionsManager.PermissionChangeEventArgs e)
        {
            if (!_toolStripItemsByPermission.ContainsKey(e.Permission))
            {
                return;
            }

            _toolStripItemsByPermission[e.Permission].Visible = e.Value;
        }

        private void frmPortal_FormClosing(object sender, FormClosingEventArgs e)
        {
            PermissionsManager.OnPermissionChange -= PermissionsManager_OnPermissionChange;
            PermissionsManager.StopCheckThread();
        }

        private void tsbSwitchGovernment_Click(object sender, EventArgs e)
        {
            foreach(Form child in MdiChildren)
            {
                child.Close();
            }

            if (MdiChildren.Any())
            {
                return;
            }

            _government = null;
            UpdateMenuVisibility();

            frmSelectGovernment selectGovernment = new frmSelectGovernment();
            DialogResult result = selectGovernment.ShowDialog();

            if (result != DialogResult.OK)
            {
                Close();
            }

            _government = selectGovernment.SelectedGovernment;
            UpdateMenuVisibility();
        }

        private void tsmiAliases_Click(object sender, EventArgs e)
        {
            frmGenericExplorer<Alias> aliasExplorer = new frmGenericExplorer<Alias>(new Email.AliasExplorerContext(_government.GovernmentID.Value));
            aliasExplorer.MdiParent = this;
            aliasExplorer.Show();
        }

        private void tsmiDistributionLists_Click(object sender, EventArgs e)
        {
            frmGenericExplorer<DistributionList> distributionListExplorer = new frmGenericExplorer<DistributionList>(new Email.DistributionListExplorerContext(_government.GovernmentID.Value, _government.EmailDomain));
            distributionListExplorer.MdiParent = this;
            distributionListExplorer.Show();
        }

        private void tsmiAccountList_Click(object sender, EventArgs e)
        {
            frmGenericExplorer<Account> accountExplorer = new frmGenericExplorer<Account>(new Accounts.AccountExplorerContext(_government.GovernmentID.Value));
            accountExplorer.MdiParent = this;
            accountExplorer.Show();
        }

        private void tsmiAccountCategories_Click(object sender, EventArgs e)
        {
            new frmGenericExplorer<Category>(new Accounts.CategoryExplorerContext(_government.GovernmentID.Value))
            {
                MdiParent = this
            }.Show();
        }

        private void tsbMintCurrency_Click(object sender, EventArgs e)
        {
            new frmMintCurrency(_government.GovernmentID.Value).ShowDialog();
        }

        private void tsmiSalesTax_Click(object sender, EventArgs e)
        {
            new frmGenericExplorer<SalesTax>(new Taxes.SalesTaxContext(_government.GovernmentID.Value))
            {
                MdiParent = this
            }.Show();
        }

        private void mnuInvoiceReceivable_Click(object sender, EventArgs e)
        {
            new frmGenericExplorer<Invoice>(new Invoicing.ReceivableInvoiceContext(_government.GovernmentID.Value))
            {
                MdiParent = this
            }.Show();
        }

        private void mnuInvoicesInvoiceConfiguration_Click(object sender, EventArgs e)
        {
            new Invoicing.frmInvoiceConfiguration(_government.GovernmentID.Value).ShowDialog();
        }

        private void mnuInvoicePayable_Click(object sender, EventArgs e)
        {
            new frmGenericExplorer<Invoice>(new Invoicing.PayableInvoiceContext(_government.GovernmentID.Value))
            {
                MdiParent = this
            }.Show();
        }

        private void toolFinance_DropDownOpening(object sender, EventArgs e)
        {
            foreach(KeyValuePair<PermissionsManager.Permissions, ToolStripItem> kvp in _toolStripItemsByPermission)
            {
                if (kvp.Value.OwnerItem != toolFinance)
                {
                    continue;
                }

                kvp.Value.Visible = PermissionsManager.HasPermission(_government.GovernmentID.Value, kvp.Key);
            }
        }

        private void mnuWireTransferHistory_Click(object sender, EventArgs e)
        {
            new frmGenericExplorer<WireTransferHistory>(new WireTransfers.WireTransferHistoryContext(_government.GovernmentID.Value))
            {
                MdiParent = this
            }.Show();
        }

        private void mnuIssueWireTransfer_Click(object sender, EventArgs e)
        {
            new WireTransfers.frmIssue(_government.GovernmentID.Value).ShowDialog();
        }

        private async void mnuWireTransfersEmailConfiguration_Click(object sender, EventArgs e)
        {
            await ShowEmailEditor(nameof(Government.EmailImplementationIDWireTransferHistory), "Wire Transfer Received", "WireTransferHistory/SetWireTransferEmailImplementationID/{0}");
        }

        private async void mnuInvoicePayableReceived_Click(object sender, EventArgs e)
        {
            await ShowEmailEditor(nameof(Government.EmailImplementationIDPayableInvoice), "Payable Invoice Received", "Invoice/PutEmailImplementationIDPayableInvoice/{0}");
        }

        private async void mnuInvoiceReceivableReady_Click(object sender, EventArgs e)
        {
            await ShowEmailEditor(nameof(Government.EmailImplementationIDReadyForReceipt), "Receivable Invoice Ready For Receipt", "Invoice/PutEmailImplementationIDReadyForReceipt/{0}");
        }

        private async Task ShowEmailEditor(string emailField, string emailName, string putURLFormat)
        {
            loader.BringToFront();
            loader.Visible = true;

            GetData getForEmailImpID = new GetData(DataAccess.APIs.GovernmentPortal, $"Government/Get/{_government.GovernmentID}");
            getForEmailImpID.RequestFields = new List<string>()
            {
                emailField
            };
            getForEmailImpID.AddGovHeader(_government.GovernmentID.Value);
            Government governmentForEmailImpID = await getForEmailImpID.GetObject<Government>();
            if (governmentForEmailImpID == null)
            {
                loader.Visible = false;
                return;
            }

            frmEmailEditor emailEditor = new frmEmailEditor()
            {
                GovernmentID = _government.GovernmentID.Value,
                EmailImplementationID = typeof(Government).GetProperty(emailField).GetValue(governmentForEmailImpID) as long?,
                EmailName = emailName
            };

            loader.Visible = false;

            if (emailEditor.ShowDialog() == DialogResult.OK)
            {
                loader.Visible = true;

                PutData putNewID = new PutData(DataAccess.APIs.GovernmentPortal, string.Format(putURLFormat, emailEditor.EmailImplementationID ?? -1L), new object());
                putNewID.AddGovHeader(_government.GovernmentID.Value);
                await putNewID.ExecuteNoResult();

                loader.Visible = false;
            }
        }
    }
}
