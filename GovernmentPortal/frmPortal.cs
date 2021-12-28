﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GovernmentPortal.Models;
using GovernmentPortal.Officials;

namespace GovernmentPortal
{
    public partial class frmPortal : Form
    {
        private Government _government = null;
        private Dictionary<PermissionsManager.Permissions, ToolStripItem> _toolStripItemsByPermission = new Dictionary<PermissionsManager.Permissions, ToolStripItem>();

        public frmPortal()
        {
            InitializeComponent();
        }

        private void SetupPermissions()
        {
            _toolStripItemsByPermission = new Dictionary<PermissionsManager.Permissions, ToolStripItem>()
            {
                { PermissionsManager.Permissions.ManageOfficials, toolOfficials },
                { PermissionsManager.Permissions.ManageEmails, toolEmail }
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
            foreach(KeyValuePair<PermissionsManager.Permissions, ToolStripItem> kvp in _toolStripItemsByPermission)
            {
                kvp.Value.Visible = PermissionsManager.HasPermission(_government.GovernmentID, kvp.Key);
            }
        }

        private void toolOfficials_Click(object sender, EventArgs e)
        {
            frmGenericExplorer<Official> genericExplorer = new frmGenericExplorer<Official>(new OfficialExplorerContext(_government.GovernmentID));
            genericExplorer.MdiParent = this;
            genericExplorer.Show();
        }

        private void frmPortal_Load(object sender, EventArgs e)
        {
            SetupPermissions();
            PermissionsManager.OnPermissionChange += PermissionsManager_OnPermissionChange;
            PermissionsManager.StartCheckThread(action => Invoke(action));
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

        private void tsmiAliases_Click(object sender, EventArgs e)
        {
            frmGenericExplorer<Alias> aliasExplorer = new frmGenericExplorer<Alias>(new Email.AliasExplorerContext(_government.GovernmentID));
            aliasExplorer.MdiParent = this;
            aliasExplorer.Show();
        }

        private void tsmiDistributionLists_Click(object sender, EventArgs e)
        {
            frmGenericExplorer<DistributionList> distributionListExplorer = new frmGenericExplorer<DistributionList>(new Email.DistributionListExplorerContext(_government.GovernmentID, _government.EmailDomain));
            distributionListExplorer.MdiParent = this;
            distributionListExplorer.Show();
        }
    }
}
