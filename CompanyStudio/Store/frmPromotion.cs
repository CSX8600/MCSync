using CompanyStudio.Extensions;
using CompanyStudio.Models;
using MesaSuite.Common;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyStudio.Store
{
    public partial class frmPromotion : BaseCompanyStudioContent, ILocationScoped, ISaveable
    {
        public long? PromotionID { get; set; }
        private List<LocationItem> locationItems = new List<LocationItem>();
        private HashSet<PromotionLocationItem> promotionLocationItems = new HashSet<PromotionLocationItem>();
        private bool isLoading = false;

        public frmPromotion()
        {
            InitializeComponent();
        }

        public Location LocationModel { get; set; }

        public event EventHandler OnSave;

        public async Task Save()
        {
            if (!this.AreFieldsPresent(new List<(string, Control)>()
            {
                ("Name", txtName)
            }))
            {
                return;
            }

            try
            {
                loader.BringToFront();
                loader.Visible = true;

                Promotion promotion = new Promotion();
                promotion.PromotionID = PromotionID;
                promotion.Name = txtName.Text;
                promotion.LocationID = LocationModel.LocationID;
                promotion.StartTime = dtpStart.Value;
                promotion.EndTime = dtpEnd.Value;

                bool promoSaveSuccess = false;
                bool isNew;
                if (PromotionID == null)
                {
                    isNew = true;
                    PostData post = new PostData(DataAccess.APIs.CompanyStudio, "Promotion/Post", promotion);
                    post.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                    promotion = await post.Execute<Promotion>();
                    if (post.RequestSuccessful)
                    {
                        PromotionID = promotion.PromotionID;
                        promoSaveSuccess = true;
                    }
                }
                else
                {
                    isNew = false;
                    PutData put = new PutData(DataAccess.APIs.CompanyStudio, "Promotion/Put", promotion);
                    put.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                    await put.ExecuteNoResult();
                    promoSaveSuccess = put.RequestSuccessful;
                }

                if (promoSaveSuccess)
                {
                    foreach(PromotionLocationItem promotionLocationItem in promotionLocationItems)
                    {
                        if (promotionLocationItem.PromotionPrice == null && promotionLocationItem.PromotionLocationItemID != null)
                        {
                            DeleteData delete = new DeleteData(DataAccess.APIs.CompanyStudio, $"PromotionLocationItem/Delete/{promotionLocationItem.PromotionLocationItemID}");
                            delete.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                            await delete.Execute();
                            promoSaveSuccess &= delete.RequestSuccessful;

                            if (!promoSaveSuccess)
                            {
                                break;
                            }

                            continue;
                        }

                        if (promotionLocationItem.PromotionLocationItemID == null)
                        {
                            PostData post = new PostData(DataAccess.APIs.CompanyStudio, "PromotionLocationItem/Post", promotionLocationItem);
                            post.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                            await post.ExecuteNoResult();
                            promoSaveSuccess &= post.RequestSuccessful;
                        }
                        else
                        {
                            PutData put = new PutData(DataAccess.APIs.CompanyStudio, "PromotionLocationItem/Put", promotionLocationItem);
                            put.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                            await put.ExecuteNoResult();
                            promoSaveSuccess &= put.RequestSuccessful;
                        }

                        if (!promoSaveSuccess)
                        {
                            break;
                        }
                    }
                }

                OnSave?.Invoke(this, new EventArgs());
            }
            finally
            {
                loader.Visible = false;
            }

            LoadData();
        }

        private void frmPromotion_Load(object sender, EventArgs e)
        {
            PermissionsManager.OnLocationPermissionChange += PermissionsManager_OnLocationPermissionChange;
            LoadData();
        }

        private void PermissionsManager_OnLocationPermissionChange(object sender, PermissionsManager.LocationWidePermissionChangeEventArgs e)
        {
            if (e.LocationID == LocationModel.LocationID && e.Permission == PermissionsManager.LocationWidePermissions.ManagePrices && !e.Value)
            {
                Close();
            }
        }

        private async void LoadData()
        {
            try
            {
                loader.BringToFront();
                loader.Visible = true;

                GetData get = new GetData(DataAccess.APIs.CompanyStudio, "LocationItem/GetAll");
                get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                locationItems = await get.GetObject<List<LocationItem>>() ?? new List<LocationItem>();

                if (PromotionID != null)
                {
                    get = new GetData(DataAccess.APIs.CompanyStudio, "Promotion/Get/" + PromotionID);
                    get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                    Promotion promotion = await get.GetObject<Promotion>();
                    if (promotion == null)
                    {
                        return;
                    }

                    txtName.Text = promotion.Name;
                    dtpStart.Value = promotion.StartTime ?? DateTime.Now;
                    dtpEnd.Value = promotion.EndTime ?? DateTime.Now;

                    promotionLocationItems = promotion.PromotionLocationItems.ToHashSet();

                    Text = promotion.Name + " - Promotion";

                    lblSaveToAddItems.Visible = false;
                    dgvItems.Enabled = true;
                    mnuMarkdown.Enabled = true;
                    mnuSetPrice.Enabled = true;
                    mnuClearPrices.Enabled = true;
                }

                FillDataGrid();
            }
            finally
            {
                loader.Visible = false;
            }
        }

        private void FillDataGrid()
        {
            try
            {
                isLoading = true;

                CleanupImages(dgvItems.Rows.OfType<DataGridViewRow>());
                dgvItems.Rows.Clear();

                foreach (LocationItem item in locationItems.Where(li => string.IsNullOrEmpty(txtSearch.Text) || (li.Item?.Name != null && li.Item.Name.Contains(txtSearch.Text))).OrderBy(li => li.Item?.Name))
                {
                    int rowIndex = dgvItems.Rows.Add();
                    DataGridViewRow row = dgvItems.Rows[rowIndex];
                    row.Cells[colImage.Name].Value = item.Item?.GetImage();
                    row.Cells[colItem.Name].Value = item.Item?.Name;
                    row.Cells[colQty.Name].Value = item.Quantity ?? 0;
                    row.Cells[colCost.Name].Value = item.BasePrice ?? 0;
                    row.Tag = item;

                    PromotionLocationItem promotionLocationItem = promotionLocationItems.FirstOrDefault(pli => pli.LocationItemID == item.LocationItemID);
                    if (promotionLocationItem != null)
                    {
                        row.Cells[colPromoPrice.Name].Value = promotionLocationItem.PromotionPrice?.ToString("N2");
                        row.Cells[colPromoPrice.Name].Tag = promotionLocationItem;
                    }
                }
            }
            finally
            {
                isLoading = false;
            }
        }

        private void CleanupImages(IEnumerable<DataGridViewRow> rows)
        {
            foreach (DataGridViewRow row in rows)
            {
                if (row.Cells[colImage.Name].Value is Image image)
                {
                    image.Dispose();
                }
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void mnuMarkdown_Click(object sender, EventArgs e)
        {
            GenericInputBox input = new GenericInputBox();
            input.Text = "Markdown Percent";
            input.ResultType = typeof(decimal);
            input.Prompt = "Enter the markdown percentage:";
            DialogResult result = input.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            try
            {
                loader.BringToFront();
                loader.Visible = true;

                decimal markdown = (decimal)input.Result;
                markdown /= 100;
                markdown = 1 - markdown;
                foreach (DataGridViewRow row in dgvItems.SelectedCells.OfType<DataGridViewCell>().Select(c => c.OwningRow).ToHashSet())
                {
                    if (!(row.Tag is LocationItem locationItem))
                    {
                        continue;
                    }

                    PromotionLocationItem promotionLocationItem = row.Cells[colPromoPrice.Name].Tag as PromotionLocationItem;
                    if (promotionLocationItem == null)
                    {
                        promotionLocationItem = new PromotionLocationItem();
                        promotionLocationItem.LocationItemID = locationItem.LocationItemID;
                        promotionLocationItem.PromotionID = PromotionID;
                    }

                    promotionLocationItem.PromotionPrice = locationItem.BasePrice * markdown;

                    if (promotionLocationItem.PromotionLocationItemID == null)
                    {
                        PostData post = new PostData(DataAccess.APIs.CompanyStudio, "PromotionLocationItem/Post", promotionLocationItem);
                        post.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                        await post.ExecuteNoResult();
                        if (!post.RequestSuccessful)
                        {
                            break;
                        }
                    }
                    else
                    {
                        PutData put = new PutData(DataAccess.APIs.CompanyStudio, "PromotionLocationItem/Put", promotionLocationItem);
                        put.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                        await put.ExecuteNoResult();
                        if (!put.RequestSuccessful)
                        {
                            break;
                        }
                    }
                }
            }
            finally
            {
                loader.Visible = false;
            }

            OnSave?.Invoke(this, new EventArgs());
            LoadData();
        }

        private async void mnuSetPrice_Click(object sender, EventArgs e)
        {
            GenericInputBox input = new GenericInputBox();
            input.Text = "Set Price";
            input.ResultType = typeof(decimal);
            input.Prompt = "Enter the price for the selected items:";
            DialogResult result = input.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            try
            {
                loader.BringToFront();
                loader.Visible = true;

                decimal price = (decimal)input.Result;
                foreach (DataGridViewRow row in dgvItems.SelectedCells.OfType<DataGridViewCell>().Select(c => c.OwningRow).ToHashSet())
                {
                    if (!(row.Tag is LocationItem locationItem))
                    {
                        continue;
                    }

                    PromotionLocationItem promotionLocationItem = row.Cells[colPromoPrice.Name].Tag as PromotionLocationItem;
                    if (promotionLocationItem == null)
                    {
                        promotionLocationItem = new PromotionLocationItem();
                        promotionLocationItem.LocationItemID = locationItem.LocationItemID;
                        promotionLocationItem.PromotionID = PromotionID;
                    }

                    promotionLocationItem.PromotionPrice = price;

                    if (promotionLocationItem.PromotionLocationItemID == null)
                    {
                        PostData post = new PostData(DataAccess.APIs.CompanyStudio, "PromotionLocationItem/Post", promotionLocationItem);
                        post.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                        await post.ExecuteNoResult();
                        if (!post.RequestSuccessful)
                        {
                            break;
                        }
                    }
                    else
                    {
                        PutData put = new PutData(DataAccess.APIs.CompanyStudio, "PromotionLocationItem/Put", promotionLocationItem);
                        put.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                        await put.ExecuteNoResult();
                        if (!put.RequestSuccessful)
                        {
                            break;
                        }
                    }
                }
            }
            finally
            {
                loader.Visible = false;
            }
            
            OnSave?.Invoke(this, new EventArgs());
            LoadData();
        }

        private async void mnuClearPrices_Click(object sender, EventArgs e)
        {
            if (!this.Confirm("Are you sure you want to clear these promotion prices?"))
            {
                return;
            }

            try
            {
                loader.BringToFront();
                loader.Visible = true;

                foreach (DataGridViewRow row in dgvItems.SelectedCells.OfType<DataGridViewCell>().Select(c => c.OwningRow).ToHashSet())
                {
                    if (!(row.Cells[colPromoPrice.Name].Tag is PromotionLocationItem promotionLocationItem))
                    {
                        continue;
                    }

                    DeleteData delete = new DeleteData(DataAccess.APIs.CompanyStudio, $"PromotionLocationItem/Delete/{promotionLocationItem.PromotionLocationItemID}");
                    delete.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                    await delete.Execute();
                    if (!delete.RequestSuccessful)
                    {
                        break;
                    }
                }
            }
            finally
            {
                loader.Visible = false;
            }

            OnSave?.Invoke(this, new EventArgs());
            LoadData();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FillDataGrid();
        }

        private void dgvItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvItems.Rows.Count || e.ColumnIndex != colPromoPrice.Index || isLoading)
            {
                return;
            }

            DataGridViewCell promoCell = dgvItems[colPromoPrice.Index, e.RowIndex];

            LocationItem locationItem = dgvItems.Rows[e.RowIndex].Tag as LocationItem;
            PromotionLocationItem promotionLocationItem = promoCell.Tag as PromotionLocationItem;
            if (promotionLocationItem == null)
            {
                promotionLocationItem = new PromotionLocationItem();
                promotionLocationItem.PromotionID = PromotionID;
                promotionLocationItem.LocationItemID = locationItem.LocationItemID;
                promoCell.Tag = promotionLocationItem;
            }

            decimal? newPromoAmount = null;
            if (!string.IsNullOrEmpty(promoCell.Value?.ToString()) && decimal.TryParse(promoCell.Value.ToString(), out decimal promoAmount))
            {
                newPromoAmount = promoAmount;
            }

            promotionLocationItem.PromotionPrice = newPromoAmount;
            promotionLocationItems.Add(promotionLocationItem);
        }

        private void frmPromotion_FormClosed(object sender, FormClosedEventArgs e)
        {
            PermissionsManager.OnLocationPermissionChange -= PermissionsManager_OnLocationPermissionChange;
        }
    }
}
