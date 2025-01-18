using GovernmentPortal.Extensions;
using GovernmentPortal.Models;
using MesaSuite.Common;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using System;
using System.Collections;
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
    public partial class frmPriceManager : Form
    {
        public long GovernmentID { get; set; }
        public frmPriceManager()
        {
            InitializeComponent();
            colQty.ValueType = typeof(decimal);
            colCost.ValueType = typeof(decimal);
        }

        private async void frmPriceManager_Load(object sender, EventArgs e)
        {
            await ReloadGrid();
        }

        private bool _isLoading = false;
        private async Task ReloadGrid()
        {
            _isLoading = true;

            try
            {
                dgvItems.Rows.Clear();
                imageDisposer.DisposeAllImages();

                GetData get = new GetData(DataAccess.APIs.GovernmentPortal, "LocationItem/GetAll");
                get.AddGovHeader(GovernmentID);
                List<LocationItem> locationItems = await get.GetObject<List<LocationItem>>() ?? new List<LocationItem>();

                foreach (LocationItem item in locationItems.OrderBy(li => li.Item?.Name).ThenBy(li => li.Quantity))
                {
                    DataGridViewRow row = dgvItems.Rows[dgvItems.Rows.Add()];
                    row.Cells[colItem.Name].Value = item.Item?.Name;
                    row.Cells[colQty.Name].Value = item.Quantity ?? 0M;
                    row.Cells[colCost.Name].Value = item.BasePrice ?? 0M;
                    row.Tag = item;
                }
            }
            finally
            {
                _isLoading = false;
            }

            try
            {
                foreach(DataGridViewRow row in dgvItems.Rows)
                {
                    LocationItem locationItem = row.Tag as LocationItem;
                    if (locationItem?.Item?.Image == null)
                    {
                        continue;
                    }

                    Image image = locationItem.Item.GetImage();
                    if (image != null)
                    {
                        imageDisposer.Images.Add(image);
                        row.Cells[colImage.Name].Value = image;
                    }
                }
            }
            catch { }
        }

        private async void mnuAddItem_Click(object sender, EventArgs e)
        {
            if (cboItem.SelectedID == null)
            {
                this.ShowError("An Item must be selected");
                return;
            }

            LocationItem locationItem = new LocationItem()
            {
                GovernmentID = GovernmentID,
                ItemID = cboItem.SelectedID.Value,
                Quantity = 0,
                BasePrice = 0
            };

            PostData post = new PostData(DataAccess.APIs.GovernmentPortal, "LocationItem/Post", locationItem);
            post.AddGovHeader(GovernmentID);
            locationItem = await post.Execute<LocationItem>();
            if (!post.RequestSuccessful)
            {
                return;
            }

            await ReloadGrid();

            DataGridViewRow rowToEdit = dgvItems.Rows.OfType<DataGridViewRow>().FirstOrDefault(r => r.Tag is LocationItem li && li.LocationItemID == locationItem.LocationItemID);
            if (rowToEdit != null)
            {
                dgvItems.CurrentCell = rowToEdit.Cells[colQty.Name];
                dgvItems.BeginEdit(true);
            }
        }

        private void dgvItems_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (_isLoading || e.RowIndex < 0 || e.RowIndex >= dgvItems.Rows.Count || (e.ColumnIndex != colQty.Index && e.ColumnIndex != colCost.Index))
            {
                return;
            }

            if (!decimal.TryParse(e.FormattedValue.ToString(), out decimal value))
            {
                this.ShowError("Value must be a number.");
                e.Cancel = true;
                return;
            }

            if (e.ColumnIndex == colQty.Index)
            {
                LocationItem currentLocationItem = dgvItems.Rows[e.RowIndex].Tag as LocationItem;

                if (dgvItems.Rows.OfType<DataGridViewRow>().Where(r => r.Index != e.RowIndex).Select(dgvr => dgvr.Tag as LocationItem).Any(li => li != null && li.ItemID == currentLocationItem.ItemID && li.Quantity == value))
                {
                    e.Cancel = true;
                    this.ShowError("Item and Quantity must be unique.");
                    return;
                }
            }
        }

        private bool _isSorting = false;
        private async void dgvItems_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (_isSorting || _isLoading || e.RowIndex < 0 || e.RowIndex >= dgvItems.Rows.Count || (e.ColumnIndex != colQty.Index && e.ColumnIndex != colCost.Index))
            {
                return;
            }

            LocationItem locationItem = dgvItems.Rows[e.RowIndex].Tag as LocationItem;
            if (locationItem == null)
            {
                return;
            }

            if (e.ColumnIndex == colQty.Index)
            {
                locationItem.Quantity = (decimal)dgvItems[e.ColumnIndex, e.RowIndex].Value;
            }
            else if (e.ColumnIndex == colCost.Index)
            {
                locationItem.BasePrice = (decimal)dgvItems[e.ColumnIndex, e.RowIndex].Value;
            }

            //int currentColumnIndex = dgvItems.CurrentCell.ColumnIndex;

            PutData put = new PutData(DataAccess.APIs.GovernmentPortal, "LocationItem/Put", locationItem);
            put.AddGovHeader(GovernmentID);
            await put.ExecuteNoResult();

            _isSorting = true;
            dgvItems.Sort(new ItemRowComparer(colItem, colQty));
            _isSorting = false;
        }

        private class ItemRowComparer : IComparer
        {
            private int ItemColumnIndex;
            private int QuantityColumnIndex;

            public ItemRowComparer(DataGridViewColumn itemColumn, DataGridViewColumn quantityColumn)
            {
                ItemColumnIndex = itemColumn.Index;
                QuantityColumnIndex = quantityColumn.Index;
            }

            public int Compare(object x, object y)
            {
                DataGridViewRow rowX = x as DataGridViewRow;
                DataGridViewRow rowY = y as DataGridViewRow;

                int compareResult = string.Compare(rowX.Cells[ItemColumnIndex].Value.ToString(), rowY.Cells[ItemColumnIndex].Value.ToString(), StringComparison.OrdinalIgnoreCase);

                if (compareResult == 0)
                {
                    compareResult = decimal.Compare((decimal)rowX.Cells[QuantityColumnIndex].Value, (decimal)rowY.Cells[QuantityColumnIndex].Value);
                }

                return compareResult;
            }
        }

        private async void mnuDeleteItem_Click(object sender, EventArgs e)
        {
            if (dgvItems.SelectedCells.Count <= 0 || !this.Confirm("Are you sure you want to delete these Items?"))
            {
                return;
            }

            foreach(int row in dgvItems.SelectedCells.OfType<DataGridViewCell>().Select(c => c.RowIndex).Distinct())
            {
                LocationItem locationItem = dgvItems.Rows[row].Tag as LocationItem;
                if (locationItem == null)
                {
                    continue;
                }

                DeleteData delete = new DeleteData(DataAccess.APIs.GovernmentPortal, "LocationItem/Delete/" + locationItem.LocationItemID);
                delete.AddGovHeader(GovernmentID);
                await delete.Execute();
            }

            await ReloadGrid();
        }

        private async void mnuMarkupSelected_Click(object sender, EventArgs e)
        {
            if (dgvItems.SelectedCells.Count <= 0)
            {
                return;
            }

            GenericInputBox markupDialog = new GenericInputBox()
            {
                Text = "Markup",
                Prompt = "Enter markup/markdown percentage:",
                AcceptText = "Save",
                ResultType = typeof(decimal)
            };

            if (markupDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            foreach (int row in dgvItems.SelectedCells.OfType<DataGridViewCell>().Select(c => c.RowIndex).Distinct())
            {
                LocationItem locationItem = dgvItems.Rows[row].Tag as LocationItem;
                if (locationItem == null)
                {
                    continue;
                }

                locationItem.BasePrice *= (decimal)markupDialog.Result / 100M;

                PutData put = new PutData(DataAccess.APIs.GovernmentPortal, "LocationItem/Put", locationItem);
                put.AddGovHeader(GovernmentID);
                await put.ExecuteNoResult();
            }

            await ReloadGrid();
        }
    }
}
