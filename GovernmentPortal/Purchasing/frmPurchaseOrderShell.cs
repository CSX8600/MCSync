using GovernmentPortal.Extensions;
using GovernmentPortal.Models;
using GovernmentPortal.Purchasing.PurchaseOrderScreen;
using MesaSuite.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GovernmentPortal.Purchasing
{
    public partial class frmPurchaseOrderShell : Form
    {
        private const string INCOMING_NODE_COMPANY_FORMAT = "From {0} ({1}) - PO {2} - Ordered on {3}";
        private const string INCOMING_NODE_GOV_FORMAT = "From {0} - PO {1} - Ordered on {2}";
        private const string OUTGOING_NODE_COMPANY_FORMAT = "To {0} ({1}) - PO {2} - Ordered on {3}";
        private const string OUTGOING_NODE_GOV_FORMAT = "To {0} - PO {1} - Ordered on {2}";

        public long GovernmentID { get; set; }
        private static class ImageKeys
        {
            public const string INBOUND = nameof(INBOUND);
            public const string OUTBOUND = nameof(OUTBOUND);

            public const string DRAFT = nameof(DRAFT);
            public const string PENDING = nameof(PENDING);
            public const string ACCEPTED = nameof(ACCEPTED);
            public const string REJECTED = nameof(REJECTED);
            public const string INPROGRESS = nameof(INPROGRESS);
            public const string COMPLETE = nameof(COMPLETE);
        }

        public frmPurchaseOrderShell()
        {
            InitializeComponent();
            treeImages.Images.Add(ImageKeys.INBOUND, Properties.Resources.arrow_in);
            treeImages.Images.Add(ImageKeys.OUTBOUND, Properties.Resources.arrow_out);

            treeImages.Images.Add(ImageKeys.DRAFT, Properties.Resources.cart_edit);
            treeImages.Images.Add(ImageKeys.PENDING, Properties.Resources.cart_go);
            treeImages.Images.Add(ImageKeys.ACCEPTED, Properties.Resources.cart_put);
            treeImages.Images.Add(ImageKeys.REJECTED, Properties.Resources.cart_delete);
            treeImages.Images.Add(ImageKeys.INPROGRESS, Properties.Resources.hourglass);
            treeImages.Images.Add(ImageKeys.COMPLETE, Properties.Resources.accept);

            PurchaseOrderScreen.Draft.NoneSelected noneSelected = new PurchaseOrderScreen.Draft.NoneSelected();
            noneSelected.Dock = DockStyle.Fill;
            noneSelected.BorderStyle = BorderStyle.FixedSingle;
            splitContainer1.Panel2.Controls.Add(noneSelected);
        }

        private async void frmPurchaseOrderShell_Load(object sender, EventArgs e)
        {
            await RefreshTree();
        }

        private bool _treeRefreshing = false;
        public async Task RefreshTree(long? selectedPurchaseOrderID = null)
        {
            try
            {
                _treeRefreshing = true;
                trePurchaseOrders.Nodes.Clear();

                GetData get = new GetData(DataAccess.APIs.GovernmentPortal, "PurchaseOrder/GetAllRelatedToGovernment");
                get.AddGovHeader(GovernmentID);
                List<PurchaseOrder> purchaseOrders = await get.GetObject<List<PurchaseOrder>>() ?? new List<PurchaseOrder>();

                TreeNode inbound = new TreeNode("Received", treeImages.Images.IndexOfKey(ImageKeys.INBOUND), treeImages.Images.IndexOfKey(ImageKeys.INBOUND));
                TreeNode outbound = new TreeNode("Sent", treeImages.Images.IndexOfKey(ImageKeys.OUTBOUND), treeImages.Images.IndexOfKey(ImageKeys.OUTBOUND));

                TreeNode inbound_pending = new TreeNode("Pending", treeImages.Images.IndexOfKey(ImageKeys.PENDING), treeImages.Images.IndexOfKey(ImageKeys.PENDING));
                TreeNode inbound_accepted = new TreeNode("Accepted", treeImages.Images.IndexOfKey(ImageKeys.ACCEPTED), treeImages.Images.IndexOfKey(ImageKeys.ACCEPTED));
                TreeNode inbound_inprogress = new TreeNode("In Progress", treeImages.Images.IndexOfKey(ImageKeys.INPROGRESS), treeImages.Images.IndexOfKey(ImageKeys.INPROGRESS));
                TreeNode inbound_complete = new TreeNode("Complete", treeImages.Images.IndexOfKey(ImageKeys.COMPLETE), treeImages.Images.IndexOfKey(ImageKeys.COMPLETE));
                inbound.Nodes.AddRange(new[] { inbound_pending, inbound_accepted, inbound_inprogress, inbound_complete });

                TreeNode outbound_draft = new TreeNode("Draft", treeImages.Images.IndexOfKey(ImageKeys.DRAFT), treeImages.Images.IndexOfKey(ImageKeys.DRAFT));
                TreeNode outbound_pending = new TreeNode("Pending", treeImages.Images.IndexOfKey(ImageKeys.PENDING), treeImages.Images.IndexOfKey(ImageKeys.PENDING));
                TreeNode outbound_accepted = new TreeNode("Accepted", treeImages.Images.IndexOfKey(ImageKeys.ACCEPTED), treeImages.Images.IndexOfKey(ImageKeys.ACCEPTED));
                TreeNode outbound_rejected = new TreeNode("Rejected", treeImages.Images.IndexOfKey(ImageKeys.REJECTED), treeImages.Images.IndexOfKey(ImageKeys.REJECTED));
                TreeNode outbound_inprogress = new TreeNode("In Progress", treeImages.Images.IndexOfKey(ImageKeys.INPROGRESS), treeImages.Images.IndexOfKey(ImageKeys.INPROGRESS));
                TreeNode outbound_complete = new TreeNode("Complete", treeImages.Images.IndexOfKey(ImageKeys.COMPLETE), treeImages.Images.IndexOfKey(ImageKeys.COMPLETE));
                outbound.Nodes.AddRange(new[] { outbound_draft, outbound_pending, outbound_accepted, outbound_rejected, outbound_inprogress, outbound_complete });

                TreeNode selectedNode = null;
                foreach (PurchaseOrder purchaseOrder in purchaseOrders)
                {
                    TreeNode purchaseOrderNode = new TreeNode();
                    TreeNode parentNode = null;

                    if (purchaseOrder.GovernmentIDDestination == GovernmentID)
                    {
                        if (purchaseOrder.GovernmentIDOrigin != null)
                        {
                            purchaseOrderNode.Text = string.Format(INCOMING_NODE_GOV_FORMAT, purchaseOrder.GovernmentOrigin?.Name, purchaseOrder.PurchaseOrderID, purchaseOrder.PurchaseOrderDate?.ToString("MM/dd/yyyy") ?? "[Unknown]");
                        }
                        else
                        {
                            purchaseOrderNode.Text = string.Format(INCOMING_NODE_COMPANY_FORMAT, purchaseOrder.LocationOrigin?.Name, purchaseOrder.LocationOrigin?.Company?.Name, purchaseOrder.PurchaseOrderID, purchaseOrder.PurchaseOrderDate?.ToString("MM/dd/yyyy") ?? "[Unknown]");
                        }

                        switch (purchaseOrder.Status)
                        {
                            case PurchaseOrder.Statuses.Pending:
                                parentNode = inbound_pending;
                                purchaseOrderNode.ImageKey = purchaseOrderNode.SelectedImageKey = ImageKeys.PENDING;
                                break;
                            case PurchaseOrder.Statuses.Accepted:
                                parentNode = inbound_accepted;
                                purchaseOrderNode.ImageKey = purchaseOrderNode.SelectedImageKey = ImageKeys.ACCEPTED;
                                break;
                            case PurchaseOrder.Statuses.InProgress:
                                parentNode = inbound_inprogress;
                                purchaseOrderNode.ImageKey = purchaseOrderNode.SelectedImageKey = ImageKeys.INPROGRESS;
                                break;
                            case PurchaseOrder.Statuses.Completed:
                                parentNode = inbound_complete;
                                purchaseOrderNode.ImageKey = purchaseOrderNode.SelectedImageKey = ImageKeys.COMPLETE;
                                break;
                        }
                    }
                    else
                    {
                        if (purchaseOrder.GovernmentIDDestination != null)
                        {
                            purchaseOrderNode.Text = string.Format(OUTGOING_NODE_GOV_FORMAT, purchaseOrder.GovernmentDestination?.Name, purchaseOrder.PurchaseOrderID, purchaseOrder.PurchaseOrderDate?.ToString("MM/dd/yyyy") ?? "[Unknown]");
                        }
                        else
                        {
                            purchaseOrderNode.Text = string.Format(OUTGOING_NODE_COMPANY_FORMAT, purchaseOrder.LocationDestination?.Name, purchaseOrder.LocationDestination?.Company?.Name, purchaseOrder.PurchaseOrderID, purchaseOrder.PurchaseOrderDate?.ToString("MM/dd/yyyy") ?? "[Unknown]");
                        }

                        switch (purchaseOrder.Status)
                        {
                            case PurchaseOrder.Statuses.Draft:
                                parentNode = outbound_draft;
                                purchaseOrderNode.ImageKey = purchaseOrderNode.SelectedImageKey = ImageKeys.DRAFT;
                                break;
                            case PurchaseOrder.Statuses.Pending:
                                parentNode = outbound_pending;
                                purchaseOrderNode.ImageKey = purchaseOrderNode.SelectedImageKey = ImageKeys.PENDING;
                                break;
                            case PurchaseOrder.Statuses.Accepted:
                                parentNode = outbound_accepted;
                                purchaseOrderNode.ImageKey = purchaseOrderNode.SelectedImageKey = ImageKeys.ACCEPTED;
                                break;
                            case PurchaseOrder.Statuses.Rejected:
                                parentNode = outbound_rejected;
                                purchaseOrderNode.ImageKey = purchaseOrderNode.SelectedImageKey = ImageKeys.REJECTED;
                                break;
                            case PurchaseOrder.Statuses.InProgress:
                                parentNode = outbound_inprogress;
                                purchaseOrderNode.ImageKey = purchaseOrderNode.SelectedImageKey = ImageKeys.INPROGRESS;
                                break;
                            case PurchaseOrder.Statuses.Completed:
                                parentNode = outbound_complete;
                                purchaseOrderNode.ImageKey = purchaseOrderNode.SelectedImageKey = ImageKeys.COMPLETE;
                                break;
                        }
                    }

                    if (purchaseOrder.PurchaseOrderID == selectedPurchaseOrderID)
                    {
                        selectedNode = purchaseOrderNode;
                    }

                    purchaseOrderNode.Tag = purchaseOrder;
                    parentNode.Nodes.Add(purchaseOrderNode);
                }

                trePurchaseOrders.Nodes.AddRange(new[] { inbound, outbound });

                trePurchaseOrders.ExpandAll();
                inbound_complete.Collapse();
                outbound_complete.Collapse();

                if (selectedNode != null)
                {
                    trePurchaseOrders.SelectedNode = selectedNode;
                    selectedNode.EnsureVisible();
                }
            }
            finally
            {
                _treeRefreshing = false;
            }
        }

        private void trePurchaseOrders_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (_treeRefreshing || !(e.Node.Tag is PurchaseOrder purchaseOrder))
            {
                return;
            }

            UserControl control = PurchaseOrder.CreateControlForPurchaseOrder(purchaseOrder);
            if (control != null)
            {
                DisplayControl(control);
            }
        }

        public void DisplayControl(UserControl control)
        {
            splitContainer1.Panel2.Controls.Clear();
            control.Dock = DockStyle.Fill;
            control.BorderStyle = BorderStyle.FixedSingle;
            if (control is IHasShellReference hasShellReference)
            {
                hasShellReference.Shell = this;
            }
            splitContainer1.Panel2.Controls.Add(control);
        }

        private void toolAddPurchaseOrder_ButtonClick(object sender, EventArgs e)
        {
            UserControl control = PurchaseOrder.CreateControlForPurchaseOrder();
            if (control != null)
            {
                DisplayControl(control);
            }
        }
    }
}
