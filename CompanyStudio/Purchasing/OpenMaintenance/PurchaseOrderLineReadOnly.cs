using CompanyStudio.Models;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace CompanyStudio.Purchasing.OpenMaintenance
{
    [ToolboxItem(false)]
    public partial class PurchaseOrderLineReadOnly : UserControl
    {
        public event EventHandler OnFulfillmentPlanLinkClicked;
        public event EventHandler OnFulfillmentsLinkClicked;

        public PurchaseOrderLine PurchaseOrderLine { get; set; }

        public PurchaseOrderLineReadOnly()
        {
            InitializeComponent();
        }

        private void PurchaseOrderLineReadOnly_Load(object sender, EventArgs e)
        {
            string description = PurchaseOrderLine.IsService ? "Service - " : "Item - ";
            if (PurchaseOrderLine.IsService)
            {
                description += PurchaseOrderLine.ServiceDescription;
            }
            else
            {
                if (PurchaseOrderLine.ItemID != null)
                {
                    description += PurchaseOrderLine.Item.Name;

                    if (!string.IsNullOrEmpty(PurchaseOrderLine.ItemDescription))
                    {
                        description += " - ";
                    }
                }

                if (!string.IsNullOrEmpty(PurchaseOrderLine.ItemDescription))
                {
                    description += PurchaseOrderLine.ItemDescription;
                }
            }

            txtDescription.Text = description;
            txtQtyOrdered.Text = PurchaseOrderLine.Quantity.ToString();
            txtQtyFulfilled.Text = PurchaseOrderLine.Fulfillments?.Where(f => f.IsComplete).Sum(f => f.Quantity).ToString() ?? "0";
            txtQtyInTransit.Text = PurchaseOrderLine.Fulfillments?.Where(f => !f.IsComplete).Sum(f => f.Quantity).ToString() ?? "0";
            txtQtyRemaining.Text = (PurchaseOrderLine.Quantity - (PurchaseOrderLine.Fulfillments?.Sum(f => f.Quantity) ?? 0M)).ToString();

            lnkFulfillmentPlans.Text = $"{PurchaseOrderLine.FulfillmentPlanPurchaseOrderLines?.Count ?? 0} Fulfillment Plan(s)";
        }

        private void PurchaseOrderLineReadOnly_SizeChanged(object sender, EventArgs e)
        {
            int textboxWidth = (Width - 24) / 4;
            txtQtyOrdered.Left = 3;
            txtQtyOrdered.Width = textboxWidth;
            lblQtyOrdered.Left = 0;

            txtQtyFulfilled.Left = txtQtyOrdered.Right + 6;
            txtQtyFulfilled.Width = textboxWidth;
            lblQtyFulfilled.Left = txtQtyOrdered.Right + 3;

            txtQtyInTransit.Left = txtQtyFulfilled.Right + 6;
            txtQtyInTransit.Width = textboxWidth;
            lblQtyInTransit.Left = txtQtyFulfilled.Right + 3;

            txtQtyRemaining.Left = txtQtyInTransit.Right + 6;
            txtQtyRemaining.Width = textboxWidth;
            lblQtyRemaining.Left = txtQtyInTransit.Right + 3;
        }

        private void lnkFulfillmentPlans_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OnFulfillmentPlanLinkClicked?.Invoke(this, EventArgs.Empty);
        }

        private void lnkFulfillments_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OnFulfillmentsLinkClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}


