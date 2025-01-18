using CompanyStudio.Models;
using MesaSuite.Common.Extensions;
using MesaSuite.Common.Utility;
using ReaLTaiizor.Child.Crown;
using ReaLTaiizor.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ReaLTaiizor.Helper.CrownHelper;

namespace CompanyStudio
{
    public partial class frmStartPageEditQuickAccess : Form
    {
        public frmStudio Studio { get; set; }
        public frmStartPage.QuickAccessItem QuickAccessItem { get; set; }

        public frmStartPageEditQuickAccess()
        {
            InitializeComponent();
            ThemeProvider.Theme = new LightTheme();
            txtQuickAccessName.BackColor = ThemeProvider.Theme.Colors.LightBackground;
            txtQuickAccessName.ForeColor = ThemeProvider.Theme.Colors.LightText;
        }

        private void frmStartPageEditQuickAccess_Load(object sender, EventArgs e)
        {
            foreach(Company company in Studio.Companies.OrderBy(c => c.Name))
            {
                foreach(Location location in company.Locations.OrderBy(l => l.Name))
                {
                    DropDownItem<Location> locationItem = new DropDownItem<Location>(location, $"{company.Name} ({location.Name})");
                    cboLocations.Items.Add(locationItem);

                    if (QuickAccessItem.CompanyID == location.CompanyID && QuickAccessItem.LocationID == location.LocationID)
                    {
                        cboLocations.SelectedItem = locationItem;
                    }
                }

                if (!company.Locations.Any())
                {
                    DropDownItem<Location> locationItem = new DropDownItem<Location>(new Models.Location() { CompanyID = company.CompanyID }, $"{company.Name} (No Locations)");
                    cboLocations.Items.Add(locationItem);

                    if (QuickAccessItem.CompanyID == company.CompanyID && QuickAccessItem.LocationID == null)
                    {
                        cboLocations.SelectedItem = locationItem;
                    }
                }
            }

            void AddChildItems(ToolStripMenuItem parentItem, CrownTreeNode parentNode)
            {
                foreach(ToolStripMenuItem item in parentItem.DropDownItems.OfType<ToolStripMenuItem>())
                {
                    CrownTreeNode node = new CrownTreeNode(item.Text);
                    if (item.Image != null)
                    {
                        node.Icon = new Bitmap(item.Image);
                    }
                    node.Text = item.Text;
                    node.Tag = item.Name;
                    parentNode.Nodes.Add(node);

                    AddChildItems(item, node);

                    string toolName = item.Name;
                    ToolStripMenuItem parentItemForName = item;
                    while(parentItemForName.OwnerItem != null)
                    {
                        parentItemForName = parentItemForName.OwnerItem as ToolStripMenuItem;
                        toolName = parentItemForName.Name + "/" + toolName;
                    }

                    if (toolName.Equals(QuickAccessItem.ToolName, StringComparison.OrdinalIgnoreCase))
                    {
                        treMenus.SelectNode(node);
                        node.EnsureVisible();
                    }
                }
            };

            foreach(ToolStripMenuItem item in (Studio.Controls["mnuBanner"] as ToolStrip).Items.OfType<ToolStripMenuItem>())
            {
                CrownTreeNode node = new CrownTreeNode(item.Text);
                if (item.Image != null)
                {
                    node.Icon = new Bitmap(item.Image);
                }
                node.Text = item.Text;
                node.Tag = item.Name;
                treMenus.Nodes.Add(node);

                AddChildItems(item, node);
                
                if (QuickAccessItem.ToolName.Equals(item.Name, StringComparison.OrdinalIgnoreCase))
                {
                    treMenus.SelectNode(node);
                    node.EnsureVisible();
                }
            }

            txtQuickAccessName.Text = QuickAccessItem.FriendlyName;
        }

        private void treMenus_SelectedNodesChanged(object sender, EventArgs e)
        {
            if (treMenus.SelectedNodes.Count == 0)
            {
                return;
            }

            List<CrownTreeNode> nodeChain = new List<CrownTreeNode>();
            CrownTreeNode currentNode = treMenus.SelectedNodes[0];
            nodeChain.Add(currentNode);

            while(currentNode.ParentNode != null)
            {
                currentNode = currentNode.ParentNode;
                nodeChain.Add(currentNode);
            }

            nodeChain.Reverse();

            StringBuilder builder = new StringBuilder();
            foreach(CrownTreeNode node in nodeChain)
            {
                builder.Append("/");
                builder.Append(node.Text);
            }

            builder.Remove(0, 1);

            txtQuickAccessName.Text = $"{cboLocations.SelectedItem?.ToString()}: {builder.ToString()}";
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (cboLocations.SelectedItem == null)
            {
                CrownMessageBox.ShowError("Please select a location.", "Location Required");
                return;
            }

            if (treMenus.SelectedNodes.Count == 0)
            {
                CrownMessageBox.ShowError("Please select a menu item.", "Menu Item Required");
                return;
            }

            if (string.IsNullOrEmpty(txtQuickAccessName.Text))
            {
                CrownMessageBox.ShowError("Please enter a quick access name.", "Name Required");
                return;
            }

            List<CrownTreeNode> nodeChain = new List<CrownTreeNode>();
            CrownTreeNode currentNode = treMenus.SelectedNodes[0];
            nodeChain.Add(currentNode);

            while (currentNode.ParentNode != null)
            {
                currentNode = currentNode.ParentNode;
                nodeChain.Add(currentNode);
            }

            nodeChain.Reverse();

            StringBuilder builder = new StringBuilder();
            foreach(CrownTreeNode node in nodeChain)
            {
                builder.Append("/");
                builder.Append(node.Tag as string);
            }

            builder.Remove(0, 1);

            QuickAccessItem.ToolName = builder.ToString();
            QuickAccessItem.FriendlyName = txtQuickAccessName.Text;
            QuickAccessItem.CompanyID = ((DropDownItem<Location>)cboLocations.SelectedItem).Object.CompanyID;
            QuickAccessItem.LocationID = ((DropDownItem<Location>)cboLocations.SelectedItem).Object.LocationID;

            DialogResult = DialogResult.OK;

            Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            Close();
        }
    }
}
