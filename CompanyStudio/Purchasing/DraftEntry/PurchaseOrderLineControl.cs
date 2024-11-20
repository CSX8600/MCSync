﻿using CompanyStudio.Extensions;
using CompanyStudio.Models;
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

namespace CompanyStudio.Purchasing.DraftEntry
{
    [ToolboxItem(false)]
    public partial class PurchaseOrderLineControl : UserControl
    {
        public event EventHandler DeleteClicked;
        public event EventHandler TotalChanged;
        public PurchaseOrderLine PurchaseOrderLine { get; set; }
        public long? PurchaseOrderID { get; set; }
        public long? CompanyID { get; set; }
        public long? LocationIDOrigin { get; set; }
        private long? _locationIDDestination = null;
        public long? LocationIDDestination
        {
            get => _locationIDDestination;
            set
            {
                _locationIDDestination = value;
                cboItem_ItemSelected(cboItem, EventArgs.Empty);
            }
        }

        public PurchaseOrderLineControl()
        {
            InitializeComponent();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            DeleteClicked?.Invoke(this, EventArgs.Empty);
        }

        private bool _loading;
        private void PurchaseOrderLineControl_Load(object sender, EventArgs e)
        {
            _loading = true;
            rdoItem.Checked = (!PurchaseOrderLine?.IsService) ?? false;
            rdoService.Checked = PurchaseOrderLine?.IsService ?? false;
            txtServiceDescription.Text = PurchaseOrderLine?.ServiceDescription;
            cboItem.SelectedID = PurchaseOrderLine?.ItemID;
            txtItemDescription.Text = PurchaseOrderLine?.ItemDescription;
            txtQuantity.Text = PurchaseOrderLine?.Quantity.ToString();
            txtUnitCost.Text = PurchaseOrderLine?.UnitCost.ToString();
            txtLineCost.Text = (PurchaseOrderLine?.Quantity * PurchaseOrderLine?.UnitCost)?.ToString();
            lblFulfillmentPlanWarning.Visible = !PurchaseOrderLine?.FulfillmentPlanPurchaseOrderLines?.Any() ?? true;
            _loading = false;
        }

        public async Task<string> PerformSave()
        {
            if (!decimal.TryParse(txtQuantity.Text, out decimal quantity))
            {
                return "Quantity must be a number";
            }

            if (!decimal.TryParse(txtUnitCost.Text, out decimal unitCost))
            {
                return "Unit Cost must be a number";
            }

            PurchaseOrderLine lineToSave = new PurchaseOrderLine()
            {
                PurchaseOrderLineID = PurchaseOrderLine?.PurchaseOrderLineID,
                PurchaseOrderID = PurchaseOrderID,
                IsService = rdoService.Checked,
                ServiceDescription = rdoService.Checked ? txtServiceDescription.Text : null,
                ItemID = rdoItem.Checked ? cboItem.SelectedID : null,
                ItemDescription = rdoItem.Checked ? txtItemDescription.Text : null,
                Quantity = quantity,
                UnitCost = unitCost
            };

            if (lineToSave.PurchaseOrderLineID == null)
            {
                PostData post = new PostData(DataAccess.APIs.CompanyStudio, "PurchaseOrderLine/Post", lineToSave);
                post.SuppressErrors = true;
                post.AddLocationHeader(CompanyID, LocationIDOrigin);
                lineToSave = await post.Execute<PurchaseOrderLine>();
                if (post.RequestSuccessful)
                {
                    PurchaseOrderLine = lineToSave;
                }
                else
                {
                    return post.LastError;
                }
            }
            else
            {
                PutData put = new PutData(DataAccess.APIs.CompanyStudio, "PurchaseOrderLine/Put", lineToSave);
                put.SuppressErrors = true;
                put.AddLocationHeader(CompanyID, LocationIDOrigin);
                lineToSave = await put.Execute<PurchaseOrderLine>();
                if (put.RequestSuccessful)
                {
                    PurchaseOrderLine = lineToSave;
                }
                else
                {
                    return put.LastError;
                }
            }

            return null;
        }

        public List<string> ValidatePresence()
        {
            List<string> errors = new List<string>();
            if (!rdoItem.Checked && !rdoService.Checked)
            {
                errors.Add("Line Type is a required field");
            }

            if (rdoItem.Checked)
            {
                if (cboItem.SelectedID == null && string.IsNullOrEmpty(txtItemDescription.Text))
                {
                    errors.Add("Either Item or Item Description is a required field when Line Type is Item");
                }
            }
            else if (rdoService.Checked && string.IsNullOrEmpty(txtServiceDescription.Text))
            {
                errors.Add("Service Description is a required field when Line Type is Service");
            }

            if (string.IsNullOrEmpty(txtQuantity.Text))
            {
                errors.Add("Quantity is a required field");
            }
            if (!decimal.TryParse(txtQuantity.Text, out _))
            {
                errors.Add("Quantity must be a number");
            }

            if (string.IsNullOrEmpty(txtUnitCost.Text))
            {
                errors.Add("Unit Cost is a required field");
            }
            if (!decimal.TryParse(txtUnitCost.Text, out _))
            {
                errors.Add("Unit Cost must be a number");
            }

            return errors;
        }

        private void LineTypeCheckedChange(object sender, EventArgs e)
        {
            pnlItem.Visible = rdoItem.Checked;
            pnlService.Visible = rdoService.Checked;

            txtUnitCost.ReadOnly = rdoItem.Checked;
        }

        public void SetHasFulfillmentPlan(bool hasFulfillmentPlan)
        {
            lblFulfillmentPlanWarning.Visible = !hasFulfillmentPlan;
        }

        private async void cboItem_ItemSelected(object sender, EventArgs e)
        {
            await UpdatePricing();
        }

        private async Task UpdatePricing()
        {
            if (_loading) { return; }

            if (rdoItem.Checked)
            {
                if (cboItem.SelectedID == null || !short.TryParse(txtQuantity.Text, out short quantity))
                {
                    txtUnitCost.Text = "Invalid";
                    txtLineCost.Text = "";
                    TotalChanged?.Invoke(this, EventArgs.Empty);
                    return;
                }

                GetData get = new GetData(DataAccess.APIs.CompanyStudio, "PriceCheck/GetItem");
                get.AddLocationHeader(CompanyID, LocationIDOrigin);
                get.QueryString.Add("locationID", LocationIDDestination.ToString());
                get.QueryString.Add("itemID", cboItem.SelectedID?.ToString());
                get.QueryString.Add("quantity", txtQuantity.Text);
                LocationItem locationItem = await get.GetObject<LocationItem>();
                if (locationItem == null)
                {
                    get.QueryString["quantity"].Clear();
                    get.QueryString["quantity"].Add("1");
                    locationItem = await get.GetObject<LocationItem>();
                }

                if (locationItem == null)
                {
                    txtUnitCost.Text = "Invalid";
                    txtLineCost.Text = "";
                    TotalChanged?.Invoke(this, EventArgs.Empty);
                    return;
                }

                decimal unitCost = decimal.MaxValue;
                short unitQuantity = locationItem.Quantity ?? 1;
                short lineCostMultiplier = 1;
                if (locationItem.QuotedPrices != null)
                {
                    unitCost = locationItem.QuotedPrices.Where(qp => qp.MinimumQuantity <= quantity).OrderBy(qp => qp.UnitCost).FirstOrDefault()?.UnitCost ?? decimal.MaxValue;
                    unitQuantity = 1;
                    lineCostMultiplier = quantity;
                }

                unitCost = Math.Min(Math.Min(unitCost, locationItem.CurrentPromotionLocationItem?.PromotionPrice ?? decimal.MaxValue), locationItem.BasePrice ?? decimal.MaxValue);

                if (unitCost == decimal.MaxValue)
                {
                    unitCost = 0;
                }

                txtUnitCost.Text = (unitCost / unitQuantity).ToString("F");
                txtLineCost.Text = (unitCost * lineCostMultiplier).ToString("F");
                TotalChanged?.Invoke(this, EventArgs.Empty);
            }
            else if (rdoService.Checked)
            {
                if (!short.TryParse(txtQuantity.Text, out short quantity) || !decimal.TryParse(txtUnitCost.Text, out decimal unitCost))
                {
                    txtLineCost.Text = "";
                    return;
                }

                txtLineCost.Text = (quantity * unitCost).ToString("F");
                TotalChanged?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                txtLineCost.Text = "";
                TotalChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private async void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            await UpdatePricing();
        }

        private async void txtUnitCost_TextChanged(object sender, EventArgs e)
        {
            if (rdoService.Checked)
            {
                await UpdatePricing();
            }
        }

        public decimal? GetCurrentUnitCost()
        {
            if (!decimal.TryParse(txtUnitCost.Text, out decimal unitCost))
            {
                return null;
            }

            return unitCost;
        }

        public decimal? GetCurrentTotal()
        {
            if (!decimal.TryParse(txtLineCost.Text, out decimal lineCost))
            {
                return null;
            }

            return lineCost;
        }
    }
}