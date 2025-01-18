using GovernmentPortal.Extensions;
using GovernmentPortal.Models;
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

namespace GovernmentPortal.Purchasing.PurchaseOrderScreen.Draft
{
    [ToolboxItem(false)]
    public partial class DraftLine : UserControl
    {
        public event EventHandler FulfillmentPlanClicked;
        public event EventHandler LineCostChanged;
        public event EventHandler DeleteClicked;
        public PurchaseOrderLine PurchaseOrderLine { get; set; }

        public long GovernmentIDOrigin { get; set; }

        private long? _locationIDDestination;
        private long? _governmentIDDestination;
        public long? LocationIDDestination
        {
            get => _locationIDDestination;
            set
            {
                _locationIDDestination = value;
                if (value != null)
                {
                    UpdatePricing();
                }
            }
        }

        public long? GovernmentIDDestination
        {
            get => _governmentIDDestination;
            set
            {
                _governmentIDDestination = value;
                if (value != null)
                {
                    UpdatePricing();
                }
            }
        }

        public DraftLine()
        {
            InitializeComponent();
        }

        private void DraftLine_Load(object sender, EventArgs e)
        {
            if (PurchaseOrderLine.PurchaseOrderLineID == null)
            {
                return;
            }

            RefreshDisplay();
        }

        public void RefreshDisplay()
        {
            rdoItem.Checked = !PurchaseOrderLine.IsService;
            rdoService.Checked = PurchaseOrderLine.IsService;
            cboItem.SelectedID = PurchaseOrderLine.ItemID;
            txtItemDescription.Text = PurchaseOrderLine.ItemDescription;
            txtServiceDescription.Text = PurchaseOrderLine.ServiceDescription;
            txtQuantity.Text = PurchaseOrderLine.Quantity?.ToString("N2");
            txtUnitCost.Text = PurchaseOrderLine.UnitCost?.ToString("N2");
            txtLineTotal.Text = (PurchaseOrderLine.Quantity * PurchaseOrderLine.UnitCost)?.ToString("N2");
            lblFulfillmentPlanWarning.Visible = !PurchaseOrderLine.FulfillmentPlanPurchaseOrderLines?.Any() ?? true;
            cmdFulfillmentPlan.Enabled = true;
        }

        private void cmdFulfillmentPlan_Click(object sender, EventArgs e)
        {
            FulfillmentPlanClicked(this, EventArgs.Empty);
        }

        private void LineTypeRadioCheckedChanged(object sender, EventArgs e)
        {
            grpItem.Visible = rdoItem.Checked;
            grpService.Visible = rdoService.Checked;

            txtQuantity.Clear();
            txtUnitCost.Clear();
            txtLineTotal.Clear();

            txtUnitCost.Enabled = rdoService.Checked;
        }

        private async Task UpdatePricing()
        {
            if (rdoService.Checked)
            {
                if (!decimal.TryParse(txtQuantity.Text, out decimal serviceQuantity) || !decimal.TryParse(txtUnitCost.Text, out decimal serviceUnitCost))
                {
                    txtLineTotal.Clear();
                    return;
                }

                txtLineTotal.Text = (serviceQuantity * serviceUnitCost).ToString("N2");
                return;
            }

            if (!decimal.TryParse(txtQuantity.Text, out decimal quantity) || cboItem.SelectedID == null)
            {
                txtUnitCost.Text = "Invalid";
                txtLineTotal.Clear();
                return;
            }

            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "PriceCheck/GetItem");
            get.QueryString.Add("itemID", cboItem.SelectedID.ToString());
            get.QueryString.Add("quantity", quantity.ToString());
            get.QueryString.Add("locationID", LocationIDDestination.ToString());
            get.QueryString.Add("governmentID", GovernmentIDDestination.ToString());
            LocationItem itemPrice = await get.GetObject<LocationItem>();

            if (itemPrice == null)
            {
                get.QueryString.Remove("quantity");
                get.QueryString.Add("quantity", "1");

                itemPrice = await get.GetObject<LocationItem>();
            }

            if (itemPrice == null)
            {
                txtUnitCost.Text = "Invalid";
                txtLineTotal.Clear();
                return;
            }

            decimal unitCost = 0;
            
            if (itemPrice.QuotedPrices?.Any() ?? false)
            {
                QuotationItem quotationItem = itemPrice.QuotedPrices.Where(qi => qi.MinimumQuantity <= quantity).OrderBy(qi => qi.UnitCost).FirstOrDefault();

                if (quotationItem != null)
                {
                    unitCost = quotationItem.UnitCost.Value;
                }
            }
            else if (itemPrice.CurrentPromotionLocationItem != null && itemPrice.CurrentPromotionLocationItem.PromotionPrice != null)
            {
                unitCost = itemPrice.CurrentPromotionLocationItem.PromotionPrice.Value;
            }
            else
            {
                unitCost = itemPrice.BasePrice.Value / itemPrice.Quantity.Value;
            }

            txtUnitCost.Text = unitCost.ToString("N2");
            txtLineTotal.Text = (quantity * unitCost).ToString("N2");
        }

        private async void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            await UpdatePricing();
        }

        private async void cboItem_ItemSelected(object sender, EventArgs e)
        {
            await UpdatePricing();
        }

        private async void txtUnitCost_TextChanged(object sender, EventArgs e)
        {
            await UpdatePricing();
        }

        private void txtLineTotal_TextChanged(object sender, EventArgs e)
        {
            LineCostChanged?.Invoke(this, EventArgs.Empty);
        }

        public decimal GetLineCost()
        {
            if (decimal.TryParse(txtLineTotal.Text, out decimal lineTotal))
            {
                return lineTotal;
            }

            return default;
        }

        public async Task<bool> SaveLine()
        {
            if (PurchaseOrderLine.PurchaseOrderLineID == null)
            {
                PurchaseOrderLine.IsService = rdoService.Checked;
                PurchaseOrderLine.ItemID = cboItem.SelectedID;
                PurchaseOrderLine.ItemDescription = txtItemDescription.Text;
                PurchaseOrderLine.ServiceDescription = txtServiceDescription.Text;
                if (decimal.TryParse(txtQuantity.Text, out decimal quantity))
                {
                    PurchaseOrderLine.Quantity = quantity;
                }

                if (decimal.TryParse(txtUnitCost.Text, out decimal unitCost))
                {
                    PurchaseOrderLine.UnitCost = unitCost;
                }

                PostData post = new PostData(DataAccess.APIs.GovernmentPortal, "PurchaseOrderLine/Post", PurchaseOrderLine);
                post.AddGovHeader(GovernmentIDOrigin);
                PurchaseOrderLine = await post.Execute<PurchaseOrderLine>();
                cmdFulfillmentPlan.Enabled = post.RequestSuccessful;
                return post.RequestSuccessful;
            }
            else
            {
                PatchData patch = new PatchData(DataAccess.APIs.GovernmentPortal, "PurchaseOrderLine/Patch", PatchData.PatchMethods.Replace, PurchaseOrderLine.PurchaseOrderLineID, new Dictionary<string, object>()
                {
                    { nameof(PurchaseOrderLine.IsService), rdoService.Checked },
                    { nameof(PurchaseOrderLine.ItemID), cboItem.SelectedID },
                    { nameof(PurchaseOrderLine.ItemDescription), txtItemDescription.Text },
                    { nameof(PurchaseOrderLine.ServiceDescription), txtServiceDescription.Text },
                    { nameof(PurchaseOrderLine.Quantity), decimal.TryParse(txtQuantity.Text, out decimal quantity) ? (decimal?)quantity : null },
                    { nameof(PurchaseOrderLine.UnitCost), decimal.TryParse(txtUnitCost.Text, out decimal unitCost) ? (decimal?)unitCost : null }
                });
                patch.AddGovHeader(GovernmentIDOrigin);
                await patch.Execute();
                cmdFulfillmentPlan.Enabled = patch.RequestSuccessful;
                return patch.RequestSuccessful;
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            DeleteClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
