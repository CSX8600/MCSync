using MesaSuite.Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace CompanyStudio.Purchasing.Quotes
{
    public partial class frmQuoteBulkPricing : Form
    {
        
        public ThemeBase Theme { get; set; }
        public List<BulkPricingItem> BulkPricingItems = new List<BulkPricingItem>();

        public frmQuoteBulkPricing()
        {
            InitializeComponent();
            colUnitCost.ValueType = typeof(decimal);
            colBulkQty.ValueType = typeof(decimal?);
            colBulkCost.ValueType = typeof(decimal);
        }

        public class BulkPricingItem
        {
            public byte[] Image { get; set; }
            public string Name { get; set; }
            public decimal? UnitCost { get; set; }
            public decimal? BulkQuantity { get; set; }
            public Image GetImage()
            {
                using (MemoryStream memoryStream = new MemoryStream(Image))
                {
                    return System.Drawing.Image.FromStream(memoryStream);
                }
            }
        }

        private bool _loading;
        private void frmQuoteBulkPricing_Load(object sender, EventArgs e)
        {
            studioFormExtender.ApplyStyle(this, Theme);

            _loading = true;
            dgvItems.Rows.Clear();

            foreach(BulkPricingItem bulkPricingItem in BulkPricingItems)
            {
                DataGridViewRow row = dgvItems.Rows[dgvItems.Rows.Add()];
                Image image = bulkPricingItem.GetImage();
                imageDisposer.Images.Add(image);
                row.Cells[colImage.Name].Value = image;
                row.Cells[colItem.Name].Value = bulkPricingItem.Name;
                row.Cells[colUnitCost.Name].Value = bulkPricingItem.UnitCost ?? 0M;
                row.Cells[colBulkQty.Name].Value = bulkPricingItem.BulkQuantity ?? (bulkPricingItem.UnitCost != null ? (decimal?)1 : null);
                row.Cells[colBulkCost.Name].Value = bulkPricingItem.UnitCost * (decimal?)row.Cells[colBulkQty.Name].Value;
                row.Tag = bulkPricingItem;
            }
            _loading = false;
        }

        private void dgvItems_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (_loading || e.RowIndex < 0 || e.RowIndex >= dgvItems.Rows.Count)
            {
                return;
            }

            if (e.ColumnIndex == colBulkQty.Index)
            {
                if (string.IsNullOrEmpty(e.FormattedValue?.ToString()))
                {
                    return;
                }

                if (!decimal.TryParse(e.FormattedValue.ToString(), out decimal qty) || qty < 0)
                {
                    this.ShowError("Bulk Quantity must be blank, greater than, or equal to 0");
                    e.Cancel = true;
                }
            }

            if (e.ColumnIndex == colBulkQty.Index)
            {
                if (string.IsNullOrEmpty(e.FormattedValue?.ToString()))
                {
                    return;
                }

                if (!decimal.TryParse(e.FormattedValue.ToString(), out decimal amt) || amt < 0)
                {
                    this.ShowError("Bulk Cost must be blank, greater than, or equal to 0");
                    e.Cancel = true;
                }
            }
        }

        private void dgvItems_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (_loading || e.RowIndex < 0 || e.RowIndex >= dgvItems.Rows.Count || (e.ColumnIndex != colBulkCost.Index && e.ColumnIndex != colBulkQty.Index))
            {
                return;
            }

            decimal? qty = dgvItems.Rows[e.RowIndex].Cells[colBulkQty.Name].Value as decimal?;
            decimal? amt = dgvItems.Rows[e.RowIndex].Cells[colBulkCost.Name].Value as decimal?;
            BulkPricingItem bulkPricingItem = dgvItems.Rows[e.RowIndex].Tag as BulkPricingItem;

            if (qty == null || amt == null || qty == 0)
            {
                bulkPricingItem.UnitCost = 0;
            }
            else if (qty != null && amt != null)
            {
                bulkPricingItem.UnitCost = Math.Round(amt.Value / qty.Value, 2, MidpointRounding.AwayFromZero);
            }
            bulkPricingItem.BulkQuantity = qty;
            dgvItems.Rows[e.RowIndex].Cells[colUnitCost.Name].Value = bulkPricingItem.UnitCost.Value.ToString("N2");
            dgvItems.Rows[e.RowIndex].Cells[colBulkCost.Name].Value = bulkPricingItem.UnitCost.Value * qty;
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
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
