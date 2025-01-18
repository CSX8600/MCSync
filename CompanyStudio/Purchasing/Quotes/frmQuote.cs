using CompanyStudio.Extensions;
using CompanyStudio.Models;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using MesaSuite.Common.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyStudio.Purchasing.Quotes
{
    public partial class frmQuote : BaseCompanyStudioContent, ILocationScoped
    {
        private List<Image> images = new List<Image>();

        public event EventHandler RecordUpdated;
        public long? QuotationID { get; set; }

        public Location LocationModel { get; set; }

        public frmQuote()
        {
            InitializeComponent();
            colUnitCost.ValueType = typeof(decimal);
            colMinimumQuantity.ValueType = typeof(decimal);
        }

        private async void frmQuote_Load(object sender, EventArgs e)
        {
            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Company/GetAll");
            List<Company> companies = await get.GetObject<List<Company>>() ?? new List<Company>();

            foreach (Company company in companies.OrderBy(c => c.Name))
            {
                DropDownItem<Company> item = new DropDownItem<Company>(company, company.Name);
                cboCompany.Items.Add(item);
            }

            get = new GetData(DataAccess.APIs.CompanyStudio, "Government/GetAll");
            get.AddCompanyHeader(Company.CompanyID);
            List<Government> governments = await get.GetObject<List<Government>>() ?? new List<Government>();
            foreach (Government government in governments.OrderBy(g => g.Name))
            {
                DropDownItem<Government> item = new DropDownItem<Government>(government, government.Name);
                cboGovernment.Items.Add(item);
            }

            await ReloadData();
        }

        private bool _loading;
        private async Task ReloadData(long? quotationItemID = null)
        {
            _loading = true;
            dgvItems.Rows.Clear();

            if (QuotationID != null)
            {
                GetData get = new GetData(DataAccess.APIs.CompanyStudio, "Quotation/Get/" + QuotationID);
                get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                Quotation quote = await get.GetObject<Quotation>();

                if (quote != null)
                {
                    rdoCompany.Checked = quote.CompanyIDTo != null;
                    rdoGovernment.Checked = quote.GovernmentIDTo != null;

                    cboCompany.SelectedItem = cboCompany.Items.OfType<DropDownItem<Company>>().FirstOrDefault(i => i.Object.CompanyID == quote.CompanyIDTo);
                    cboGovernment.SelectedItem = cboGovernment.Items.OfType<DropDownItem<Government>>().FirstOrDefault(i => i.Object.GovernmentID == quote.GovernmentIDTo);
                    chkRepeatable.Checked = quote.IsRepeatable;
                    dtpExpiration.Value = quote.ExpirationTime ?? DateTime.Now;

                    rdoCompany.Enabled = quote.CompanyIDFrom == Company.CompanyID;
                    rdoGovernment.Enabled = quote.CompanyIDFrom == Company.CompanyID;
                    cboCompany.Enabled &= quote.CompanyIDFrom == Company.CompanyID;
                    cboGovernment.Enabled &= quote.CompanyIDFrom == Company.CompanyID;
                    chkRepeatable.Enabled = quote.CompanyIDFrom == Company.CompanyID;
                    dtpExpiration.Enabled = quote.CompanyIDFrom == Company.CompanyID;
                    cboItem.Enabled = quote.CompanyIDFrom == Company.CompanyID;
                    toolAdd.Enabled = quote.CompanyIDFrom == Company.CompanyID;
                    colUnitCost.ReadOnly = quote.CompanyIDFrom != Company.CompanyID;
                    colMinimumQuantity.ReadOnly = quote.CompanyIDFrom != Company.CompanyID;

                    foreach (QuotationItem item in (quote.QuotationItems ?? new List<QuotationItem>()).OrderBy(qri => qri.Item.Name))
                    {
                        DataGridViewRow row = dgvItems.Rows[dgvItems.Rows.Add()];
                        row.Cells[colItem.Name].Value = item.Item?.Name;
                        row.Cells[colUnitCost.Name].Value = item.UnitCost ?? 0;
                        row.Cells[colMinimumQuantity.Name].Value = item.MinimumQuantity ?? 0;
                        row.Tag = item;
                    }

                    if (quotationItemID != null)
                    {
                        DataGridViewRow row = dgvItems.Rows.OfType<DataGridViewRow>().FirstOrDefault(dgvr => (dgvr.Tag as QuotationItem)?.QuotationItemID == quotationItemID);
                        if (row != null)
                        {
                            dgvItems.Select();
                            dgvItems.CurrentCell = dgvItems[colUnitCost.Index, row.Index];
                            dgvItems.BeginEdit(true);
                        }
                    }

                    foreach (DataGridViewRow row in dgvItems.Rows)
                    {
                        try
                        {
                            QuotationItem item = row.Tag as QuotationItem;
                            if (item != null)
                            {
                                Image image = item.Item.GetImage();
                                images.Add(image);
                                row.Cells[colImage.Name].Value = image;
                            }
                        }
                        catch { }
                    }
                }
            }
            lblExpired.Visible = dtpExpiration.Value <= DateTime.Now;

            _loading = false;
        }

        private void RadioButtonCheckedChanged(object sender, EventArgs e)
        {
            cboCompany.Enabled = toolAdd.Enabled && rdoCompany.Checked;
            rdoGovernment.Enabled = toolAdd.Enabled && rdoGovernment.Checked;
        }

        private async void CreateOrUpdateQuote(object sender, EventArgs e)
        {
            if (_loading) { return; }

            long? companyID = rdoCompany.Checked ? (cboCompany.SelectedItem as DropDownItem<Company>)?.Object.CompanyID : null;
            long? governmentID = rdoGovernment.Checked ? (cboGovernment.SelectedItem as DropDownItem<Government>)?.Object.GovernmentID : null;
            bool repeatable = chkRepeatable.Checked;
            DateTime expiration = dtpExpiration.Value;

            if (QuotationID != null)
            {
                PatchData patch = new PatchData(DataAccess.APIs.CompanyStudio, "Quotation/Patch", PatchData.PatchMethods.Replace, QuotationID, new Dictionary<string, object>()
                {
                    { "CompanyIDTo", companyID },
                    { "GovernmentIDTo", governmentID },
                    { "IsRepeatable", repeatable },
                    { "ExpirationTime", expiration }
                });
                patch.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                await patch.Execute();
            }
            else if (companyID != null || governmentID != null)
            {
                Quotation quotation = new Quotation()
                {
                    CompanyIDFrom = Company.CompanyID,
                    CompanyIDTo = companyID,
                    GovernmentIDTo = governmentID,
                    IsRepeatable = repeatable,
                    ExpirationTime = expiration
                };

                PostData post = new PostData(DataAccess.APIs.CompanyStudio, "Quotation/Post", quotation);
                post.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                Quotation savedQuotation = await post.Execute<Quotation>();
                QuotationID = savedQuotation?.QuotationID;
            }

            lblExpired.Visible = dtpExpiration.Value <= DateTime.Now;

            RecordUpdated?.Invoke(this, EventArgs.Empty);
        }

        private async void toolAdd_Click(object sender, EventArgs e)
        {
            if (QuotationID == null && (cboCompany.SelectedItem == null || cboGovernment.SelectedItem == null))
            {
                this.ShowError("Select a Company or Government before adding Items");
                return;
            }

            if (QuotationID == null)
            {
                this.ShowError("Quotation must be saved before items can be added");
                return;
            }

            if (cboItem.SelectedID == null)
            {
                this.ShowError("An Item must be selected before Adding");
                return;
            }

            QuotationItem quotationItem = new QuotationItem()
            {
                QuotationID = QuotationID,
                ItemID = cboItem.SelectedID,
                UnitCost = 0
            };

            PostData post = new PostData(DataAccess.APIs.CompanyStudio, "QuotationItem/Post", quotationItem);
            post.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            QuotationItem savedItem = await post.Execute<QuotationItem>();

            RecordUpdated?.Invoke(this, EventArgs.Empty);

            await ReloadData(savedItem.QuotationItemID);
        }

        private async void toolDelete_Click(object sender, EventArgs e)
        {
            if (!this.Confirm("Are you sure you want to delete the selected Items from this Quote?"))
            {
                return;
            }

            _loading = true;
            foreach (int rowIndex in dgvItems.SelectedCells.OfType<DataGridViewCell>().Select(c => c.RowIndex).ToHashSet().OrderByDescending(i => i))
            {
                QuotationItem item = dgvItems.Rows[rowIndex].Tag as QuotationItem;
                if (item != null)
                {
                    DeleteData delete = new DeleteData(DataAccess.APIs.CompanyStudio, "QuotationItem/Delete/" + item.QuotationItemID);
                    delete.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                    await delete.Execute();

                    dgvItems.Rows.RemoveAt(rowIndex);
                }
            }

            RecordUpdated?.Invoke(this, EventArgs.Empty);
            _loading = false;
        }

        private async void dgvItems_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (_loading || e.RowIndex < 0 || e.RowIndex >= dgvItems.Rows.Count || !toolAdd.Enabled)
            {
                return;
            }

            if (e.ColumnIndex == colMinimumQuantity.Index)
            {
                e.Cancel = !await HandleMinimumQuantityValidating(e.RowIndex, e.FormattedValue.ToString());
            }
            else if (e.ColumnIndex == colUnitCost.Index)
            {
                e.Cancel = !await HandleUnitCostValidating(e.RowIndex, e.FormattedValue.ToString());
            }
        }

        private async Task<bool> HandleUnitCostValidating(int rowIndex, string formattedValue)
        {
            if (!decimal.TryParse(formattedValue, out decimal newValue) || newValue < 0)
            {
                this.ShowError("Unit cost must be greater or equal to 0");
                return false;
            }

            QuotationItem item = dgvItems.Rows[rowIndex].Tag as QuotationItem;
            if (item != null)
            {
                item.UnitCost = newValue;

                PutData put = new PutData(DataAccess.APIs.CompanyStudio, "QuotationItem/Put", item);
                put.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                await put.ExecuteNoResult();

                RecordUpdated?.Invoke(this, EventArgs.Empty);
            }

            return true;
        }

        private async Task<bool> HandleMinimumQuantityValidating(int rowIndex, string formattedValue)
        {
            if (!decimal.TryParse(formattedValue, out decimal newValue) || newValue < 0)
            {
                this.ShowError("Minimum Quantity must be greater or equal to 0");
                return false;
            }

            QuotationItem item = dgvItems.Rows[rowIndex].Tag as QuotationItem;
            if (item != null)
            {
                item.MinimumQuantity = newValue;

                PutData put = new PutData(DataAccess.APIs.CompanyStudio, "QuotationItem/Put", item);
                put.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                await put.ExecuteNoResult();

                RecordUpdated?.Invoke(this, EventArgs.Empty);
            }

            return true;
        }

        private async void toolSetUnitByBulk_Click(object sender, EventArgs e)
        {
            List<frmQuoteBulkPricing.BulkPricingItem> bulkPricingItems = new List<frmQuoteBulkPricing.BulkPricingItem>();
            Dictionary<frmQuoteBulkPricing.BulkPricingItem, QuotationItem> quotationItemByBulkPricingItem = new Dictionary<frmQuoteBulkPricing.BulkPricingItem, QuotationItem>();

            foreach(DataGridViewRow row in dgvItems.SelectedCells.OfType<DataGridViewCell>().Select(c => dgvItems.Rows[c.RowIndex]).ToHashSet())
            {
                QuotationItem item = row.Tag as QuotationItem;
                if (item == null)
                {
                    continue;
                }

                frmQuoteBulkPricing.BulkPricingItem bulkPricingItem = new frmQuoteBulkPricing.BulkPricingItem()
                {
                    Image = item.Item.Image,
                    Name = item.Item.Name,
                    UnitCost = item.UnitCost,
                    BulkQuantity = item.MinimumQuantity
                };
                bulkPricingItems.Add(bulkPricingItem);
                quotationItemByBulkPricingItem.Add(bulkPricingItem, item);
            }

            frmQuoteBulkPricing quoteBulkPricing = new frmQuoteBulkPricing()
            {
                Theme = Theme,
                BulkPricingItems = bulkPricingItems
            };
            DialogResult result = quoteBulkPricing.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }

            foreach(frmQuoteBulkPricing.BulkPricingItem bulkPricingItem in bulkPricingItems)
            {
                QuotationItem item = quotationItemByBulkPricingItem[bulkPricingItem];
                if (item.UnitCost == bulkPricingItem.UnitCost)
                {
                    continue;
                }

                item.UnitCost = bulkPricingItem.UnitCost;

                DataGridViewRow row = dgvItems.Rows.OfType<DataGridViewRow>().Single(r => r.Tag == item);
                row.Cells[colUnitCost.Name].Value = item.UnitCost;
                await HandleUnitCostValidating(row.Index, item.UnitCost.Value.ToString("N2"));
            }
        }

        private void dgvItems_SelectionChanged(object sender, EventArgs e)
        {
            toolDelete.Enabled = toolAdd.Enabled && dgvItems.SelectedCells.Count > 0;
            toolSetUnitByBulk.Enabled = toolDelete.Enabled;
        }
    }
}
