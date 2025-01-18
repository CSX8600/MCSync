using CompanyStudio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyStudio.Purchasing.History
{
    [ToolboxItem(false)]
    public partial class PurchaseOrderLineControl : UserControl
    {
        public event EventHandler OnFulfillmentPlansClicked;
        public event EventHandler OnFulfillmentClicked;

        public PurchaseOrderLine PurchaseOrderLine { get; set; }
        public PurchaseOrderLineControl()
        {
            InitializeComponent();
        }

        private void PurchaseOrderLineControl_Load(object sender, EventArgs e)
        {
            txtDescription.Text = PurchaseOrderLine.DisplayStringNoQuantity;
            txtQuantity.Text = PurchaseOrderLine.Quantity.ToString();
            txtUnitCost.Text = PurchaseOrderLine.UnitCost.Value.ToString("N2");
            txtLineTotal.Text = (PurchaseOrderLine.UnitCost.Value * PurchaseOrderLine.Quantity.Value).ToString("N2");
            txtQuantityFulfilled.Text = PurchaseOrderLine.Fulfillments.Sum(x => x.Quantity).ToString();
            txtQuantityUnfulfilled.Text = PurchaseOrderLine.UnfulfilledQuantity.ToString();

            lnkFulfillmentPlans.Text = PurchaseOrderLine.FulfillmentPlanPurchaseOrderLines.Count.ToString() + " Fulfillment Plan(s)";
        }

        private void lnkFulfillmentPlans_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OnFulfillmentPlansClicked?.Invoke(this, EventArgs.Empty);
        }

        private void lnkFulfillments_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OnFulfillmentClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
