using CompanyStudio.Extensions;
using CompanyStudio.Models;
using CompanyStudio.Store.ClonePrices;
using MesaSuite.Common;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyStudio.Store
{
    public partial class frmStoreItems : BaseCompanyStudioContent, ILocationScoped
    {
        private ItemThenQuantityComparer colComparer;
        private bool isLoading = false;

        public frmStoreItems()
        {
            InitializeComponent();
            colComparer = new ItemThenQuantityComparer(colItem.Name, colQty.Name);
        }

        public Location LocationModel { get; set; }

        private void frmStoreItems_Load(object sender, EventArgs e)
        {
            AppendCompanyLocationNameToWindowText();
            PermissionsManager.OnLocationPermissionChange += PermissionsManager_OnLocationPermissionChange;

            colQty.ValueType = typeof(decimal);
            colCost.ValueType = typeof(decimal);

            LoadData();
        }

        private void PermissionsManager_OnLocationPermissionChange(object sender, PermissionsManager.LocationWidePermissionChangeEventArgs e)
        {
            if (e.LocationID == LocationModel.LocationID && !e.Value)
            {
                if (!PermissionsManager.HasPermission(LocationModel.LocationID.Value, PermissionsManager.LocationWidePermissions.ManagePrices) &&
                    !PermissionsManager.HasPermission(LocationModel.LocationID.Value, PermissionsManager.LocationWidePermissions.ManagePurchaseOrders))
                {
                    this.ShowError("You do not have permission to manage prices in this location.");
                    Close();
                }
            }
        }

        private async Task LoadData()
        {
            try
            {
                isLoading = true;
                CleanupImages(dgvItems.Rows.OfType<DataGridViewRow>());
                dgvItems.Rows.Clear();

                grpCurrentItems.Enabled = true;

                GetData get = new GetData(DataAccess.APIs.CompanyStudio, "LocationItem/GetAll");
                get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                List<LocationItem> locationItems = await get.GetObject<List<LocationItem>>() ?? new List<LocationItem>();
                dgvItems.CausesValidation = false;
                foreach (LocationItem item in locationItems)
                {
                    int rowIndex = dgvItems.Rows.Add();
                    DataGridViewRow row = dgvItems.Rows[rowIndex];
                    row.Cells[colImage.Name].Value = item.Item?.GetImage();
                    row.Cells[colItem.Name].Value = item.Item?.Name;
                    row.Cells[colQty.Name].Value = item.Quantity ?? 0;
                    row.Cells[colCost.Name].Value = item.BasePrice ?? 0;
                    row.Cells[colPromo.Name].Value = item.CurrentPromotionLocationItem?.PromotionLocationItemID != null ? item.CurrentPromotionLocationItem.PromotionPrice?.ToString("N2") + " (" + item.CurrentPromotionLocationItem.Promotion?.Name + ")" : "No Current Promotion";
                    row.Tag = item;
                }

                dgvItems.Sort(colComparer);
                if (dgvItems.Rows.Count > 0)
                {
                    dgvItems.CurrentCell = dgvItems[0, 0];
                }
                dgvItems.CausesValidation = true;
            }
            finally
            {
                isLoading = false;
            }
        }

        private void CleanupImages(IEnumerable<DataGridViewRow> rows)
        {
            foreach(DataGridViewRow row in rows)
            {
                if (row.Cells[colImage.Name].Value is Image image)
                {
                    image.Dispose();
                }
            }
        }

        private void dgvItems_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            StringBuilder errorBuilder = new StringBuilder();
            DataGridViewRow row = dgvItems.Rows[e.RowIndex];
            if (row.Cells[colQty.Name].Value is decimal quantity && quantity < 0)
            {
                errorBuilder.AppendLine("Quantity must be greater than 0");
            }

            if (row.Cells[colCost.Name].Value is decimal cost && cost < 0)
            {
                errorBuilder.AppendLine("Base Price must be greater than 0");
            }

            LocationItem thisLocItem = (LocationItem)row.Tag;
            foreach(DataGridViewRow otherRow in dgvItems.Rows.OfType<DataGridViewRow>())
            {
                if (otherRow == row)
                {
                    continue;
                }

                LocationItem item = (LocationItem)otherRow.Tag;
                if (item.ItemID == thisLocItem.ItemID && (decimal)row.Cells[colQty.Name].Value == (decimal)otherRow.Cells[colQty.Name].Value)
                {
                    errorBuilder.AppendLine("This combination of Item and Quantity already exists");
                    break;
                }
            }

            if (errorBuilder.Length > 0)
            {
                this.ShowError("The following errors occurred on this line:\r\n\r\n" + errorBuilder.ToString());
                e.Cancel = true;
            }
        }

        // 🥚 - egg is forever immortalized thanks to the power of version control
        private async void mnuAddItem_Click(object sender, EventArgs e)
        {
            if (itmSelector.SelectedID != null)
            {
                GetData getItem = new GetData(DataAccess.APIs.CompanyStudio, "Item/Get/" + itmSelector.SelectedID);
                Item item = await getItem.GetObject<Item>();
                if (item != null)
                {
                    LocationItem locationItem = new LocationItem()
                    {
                        LocationID = LocationModel.LocationID,
                        ItemID = item.ItemID,
                        Quantity = 0,
                        BasePrice = 0
                    };

                    PostData post = new PostData(DataAccess.APIs.CompanyStudio, "LocationItem/Post", locationItem);
                    post.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                    locationItem = await post.Execute<LocationItem>();
                    if (!post.RequestSuccessful)
                    {
                        return;
                    }

                    int rowIndex = dgvItems.Rows.Add();
                    DataGridViewRow row = dgvItems.Rows[rowIndex];

                    row.Cells[colImage.Name].Value = item.GetImage();
                    row.Cells[colItem.Name].Value = item.Name;
                    row.Cells[colQty.Name].Value = (decimal)0;
                    row.Cells[colCost.Name].Value = 0D;
                    row.Tag = locationItem;

                    dgvItems.Sort(colComparer);

                    dgvItems.Focus();
                    dgvItems.EndEdit();
                    dgvItems.CurrentCell = row.Cells[colQty.Name];
                    dgvItems.BeginEdit(true);
                }
            }
            else
            {
                this.ShowError("You must first select an Item from the dropdown above");
            }
        }

        private async void dgvItems_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (isLoading)
            {
                return;
            }

            DataGridViewRow row = dgvItems.Rows[e.RowIndex];
            if (!(row.Tag is LocationItem locationItem))
            {
                return;
            }

            LocationItem itemToSave = new LocationItem()
            {
                LocationItemID = locationItem.LocationItemID,
                LocationID = locationItem.LocationID,
                ItemID = locationItem.ItemID,
                BasePrice = locationItem.BasePrice,
                Quantity = locationItem.Quantity
            };
            itemToSave.Quantity = decimal.Parse(row.Cells[colQty.Name].Value.ToString());
            itemToSave.BasePrice = decimal.Parse(row.Cells[colCost.Name].Value.ToString());

            PutData putData = new PutData(DataAccess.APIs.CompanyStudio, "LocationItem/Put", itemToSave);
            putData.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            itemToSave = await putData.Execute<LocationItem>();
            if (putData.RequestSuccessful)
            {
                row.Tag = itemToSave;
            }
        }

        private async void mnuDeleteItem_Click(object sender, EventArgs e)
        {
            if (dgvItems.SelectedCells.Count <= 0 || !this.Confirm("Are you sure you want to remove these Items?"))
            {
                return;
            }

            HashSet<DataGridViewRow> selectedRows = dgvItems.SelectedCells.OfType<DataGridViewCell>().Select(cell => cell.OwningRow).ToHashSet();
            try
            {
                grpCurrentItems.Enabled = false;
                foreach (DataGridViewRow row in selectedRows)
                {
                    if (!(row.Tag is LocationItem locationItem))
                    {
                        continue;
                    }

                    DeleteData delete = new DeleteData(DataAccess.APIs.CompanyStudio, $"LocationItem/Delete/{locationItem.LocationItemID}");
                    delete.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                    await delete.Execute();
                }
            }
            finally
            {
                grpCurrentItems.Enabled = true;
            }

            CleanupImages(selectedRows);
            foreach (DataGridViewRow row in selectedRows)
            {
                dgvItems.Rows.Remove(row);
            }
        }

        public class ItemThenQuantityComparer : IComparer
        {
            private string itemColName;
            private string quantityColName;

            public ItemThenQuantityComparer(string itemColName, string quantityColName)
            {
                this.itemColName = itemColName;
                this.quantityColName = quantityColName;
            }

            public int Compare(object x, object y)
            {
                DataGridViewRow row1 = (DataGridViewRow)x;
                DataGridViewRow row2 = (DataGridViewRow)y;

                int compareResult = row1.Cells[itemColName].Value.ToString().CompareTo(row2.Cells[itemColName].Value.ToString());
                if (compareResult == 0)
                {
                    decimal quantity1 = decimal.Parse(row1.Cells[quantityColName].Value.ToString());
                    decimal quantity2 = decimal.Parse(row2.Cells[quantityColName].Value.ToString());

                    compareResult = quantity1.CompareTo(quantity2);
                }

                return compareResult;
            }
        }

        private void dgvItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                if (dgvItems.CurrentCell == null || dgvItems.CurrentCell.RowIndex != e.RowIndex)
                {
                    dgvItems.Sort(colComparer);
                    return;
                }

                DataGridViewCell cell = dgvItems[dgvItems.CurrentCell.ColumnIndex, e.RowIndex];
                dgvItems.Sort(colComparer);
                dgvItems.CurrentCell = cell;
            }));
        }

        private void itmSelector_ItemSelected(object sender, EventArgs e)
        {
            dgvItems.ClearSelection();
            dgvItems.CurrentCell = null;

            if (itmSelector.SelectedID != null)
            {
                foreach (DataGridViewRow row in dgvItems.Rows)
                {
                    if (row.Tag is LocationItem locationItem && locationItem.ItemID == itmSelector.SelectedID)
                    {
                        row.Selected = true;
                    }
                }
            }
        }

        private void mnuMarkupSelected_Click(object sender, EventArgs e)
        {
            GenericInputBox markupInput = new GenericInputBox()
            {
                Text = "Markup Rate",
                Prompt = "Enter the markup/markdown percentage rate:",
                ResultType = typeof(decimal)
            };
            DialogResult result = markupInput.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            decimal percentage = 1 + ((decimal)markupInput.Result) / 100M;
            foreach(DataGridViewRow row in dgvItems.SelectedCells.OfType<DataGridViewCell>().Select(c => c.OwningRow).ToHashSet())
            {
                decimal price = decimal.Parse(row.Cells[colCost.Name].Value.ToString());
                price *= percentage;
                row.Cells[colCost.Name].Value = Math.Round(price, 2, MidpointRounding.AwayFromZero);
                dgvItems_RowValidated(dgvItems, new DataGridViewCellEventArgs(colCost.Index, row.Index));
            }
        }

        private void mnuImportExportPrices_Click(object sender, EventArgs e)
        {
            ClonePricesWizardController clonePricesWizard = new ClonePricesWizardController(LocationModel.LocationID);
            clonePricesWizard.StartWizard();
        }

        private void mnuAutomation_Click(object sender, EventArgs e)
        {
            frmPricingAutomation automation = new frmPricingAutomation();
            automation.Location = toolStrip1.PointToScreen(new Point(mnuAutomation.Bounds.X + mnuAutomation.Bounds.Width, mnuAutomation.Bounds.Y));
            automation.Theme = Theme;
            automation.CompanyID = Company.CompanyID;
            automation.LocationID = LocationModel.LocationID;
            automation.ShowDialog();
        }

        private void frmStoreItems_FormClosed(object sender, FormClosedEventArgs e)
        {
            PermissionsManager.OnLocationPermissionChange -= PermissionsManager_OnLocationPermissionChange;
        }
    }
}
