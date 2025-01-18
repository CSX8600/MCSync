using CompanyStudio.Extensions;
using CompanyStudio.Models;
using MesaSuite.Common.Attributes;
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
    [UriReachable("quotationrequest/{QuotationRequestID}")]
    public partial class frmQuoteRequest : BaseCompanyStudioContent, ILocationScoped
    {
        public event EventHandler RecordUpdated;
        public event EventHandler<long?> QuoteIssued;

        List<Image> images = new List<Image>();
        public long? QuotationRequestID { get; set; }

        public frmQuoteRequest()
        {
            InitializeComponent();
            colQuantity.ValueType = typeof(decimal);
        }

        public Location LocationModel { get; set; }

        private async void frmQuoteRequest_Load(object sender, EventArgs e)
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

            await ReloadItems();
        }

        private bool _loading;
        private async Task ReloadItems(long? quotationRequestItemID = null)
        {
            _loading = true;
            dgvItems.Rows.Clear();

            if (QuotationRequestID != null)
            {
                GetData get = new GetData(DataAccess.APIs.CompanyStudio, "QuotationRequest/Get/" + QuotationRequestID);
                get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                QuotationRequest quotationRequest = await get.GetObject<QuotationRequest>();

                if (quotationRequest != null)
                {
                    rdoCompany.Checked = quotationRequest.CompanyIDTo != null;
                    rdoGovernment.Checked = quotationRequest.GovernmentIDTo != null;

                    cboCompany.SelectedItem = cboCompany.Items.OfType<DropDownItem<Company>>().FirstOrDefault(i => i.Object.CompanyID == quotationRequest.CompanyIDTo);
                    cboGovernment.SelectedItem = cboGovernment.Items.OfType<DropDownItem<Government>>().FirstOrDefault(i => i.Object.GovernmentID == quotationRequest.GovernmentIDTo);

                    txtNotes.Text = quotationRequest.Notes;

                    rdoCompany.Enabled = quotationRequest.CompanyIDFrom == Company.CompanyID;
                    rdoGovernment.Enabled = quotationRequest.CompanyIDFrom == Company.CompanyID;
                    cboCompany.Enabled &= quotationRequest.CompanyIDFrom == Company.CompanyID;
                    cboGovernment.Enabled &= quotationRequest.CompanyIDFrom == Company.CompanyID;
                    txtNotes.ReadOnly = quotationRequest.CompanyIDFrom != Company.CompanyID;
                    cboItem.Enabled = quotationRequest.CompanyIDFrom == Company.CompanyID;
                    toolAdd.Enabled = quotationRequest.CompanyIDFrom == Company.CompanyID;
                    colQuantity.ReadOnly = quotationRequest.CompanyIDFrom != Company.CompanyID;
                    cmdIssueQuote.Visible = quotationRequest.CompanyIDTo == Company.CompanyID;

                    foreach (QuotationRequestItem item in (quotationRequest.QuotationRequestItems ?? new List<QuotationRequestItem>()).OrderBy(qri => qri.Item.Name).ThenBy(qri => qri.Quantity))
                    {
                        DataGridViewRow row = dgvItems.Rows[dgvItems.Rows.Add()];
                        row.Cells[colItem.Name].Value = item.Item?.Name;
                        row.Cells[colQuantity.Name].Value = item.Quantity ?? 0;
                        row.Tag = item;
                    }

                    if (quotationRequestItemID != null)
                    {
                        DataGridViewRow row = dgvItems.Rows.OfType<DataGridViewRow>().FirstOrDefault(dgvr => (dgvr.Tag as QuotationRequestItem)?.QuotationRequestItemID == quotationRequestItemID);
                        if (row != null)
                        {
                            dgvItems.Select();
                            dgvItems.CurrentCell = dgvItems[colQuantity.Index, row.Index];
                            dgvItems.BeginEdit(true);
                        }
                    }

                    foreach (DataGridViewRow row in dgvItems.Rows)
                    {
                        try
                        {
                            QuotationRequestItem item = row.Tag as QuotationRequestItem;
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

            _loading = false;
        }

        private async void toolAdd_Click(object sender, EventArgs e)
        {
            if (QuotationRequestID == null && (cboCompany.SelectedItem == null || cboGovernment.SelectedItem == null))
            {
                this.ShowError("Select a Company or Government before adding Items");
                return;
            }

            if (QuotationRequestID == null)
            {
                this.ShowError("Quotation Request must be saved before items can be added");
                return;
            }

            if (cboItem.SelectedID == null)
            {
                this.ShowError("An Item must be selected before Adding");
                return;
            }

            QuotationRequestItem quotationRequestItem = new QuotationRequestItem()
            {
                QuotationRequestID = QuotationRequestID,
                ItemID = cboItem.SelectedID,
                Quantity = 0
            };

            PostData post = new PostData(DataAccess.APIs.CompanyStudio, "QuotationRequestItem/Post", quotationRequestItem);
            post.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            QuotationRequestItem savedItem = await post.Execute<QuotationRequestItem>();

            RecordUpdated?.Invoke(this, EventArgs.Empty);

            await ReloadItems(savedItem.QuotationRequestItemID);
        }

        private async void dgvItems_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (_loading || e.RowIndex < 0 || e.RowIndex >= dgvItems.Rows.Count || e.ColumnIndex != colQuantity.Index)
            {
                return;
            }

            if (!decimal.TryParse(e.FormattedValue.ToString(), out decimal newValue) || newValue < 0)
            {
                this.ShowError("Quantity must be greater or equal to 0");
                e.Cancel = true;
                return;
            }

            QuotationRequestItem item = dgvItems.Rows[e.RowIndex].Tag as QuotationRequestItem;
            if (item != null)
            {
                item.Quantity = newValue;

                PutData put = new PutData(DataAccess.APIs.CompanyStudio, "QuotationRequestItem/Put", item);
                put.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                await put.ExecuteNoResult();

                RecordUpdated?.Invoke(this, EventArgs.Empty);
            }
        }

        private async void toolDelete_Click(object sender, EventArgs e)
        {
            if (!this.Confirm("Are you sure you want to delete the selected Items from this Quote Request?"))
            {
                return;
            }

            _loading = true;
            foreach(int rowIndex in dgvItems.SelectedCells.OfType<DataGridViewCell>().Select(c => c.RowIndex).ToHashSet().OrderByDescending(i => i))
            {
                QuotationRequestItem item = dgvItems.Rows[rowIndex].Tag as QuotationRequestItem;
                if (item != null)
                {
                    DeleteData delete = new DeleteData(DataAccess.APIs.CompanyStudio, "QuotationRequestItem/Delete/" + item.QuotationRequestItemID);
                    delete.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                    await delete.Execute();

                    dgvItems.Rows.RemoveAt(rowIndex);
                }
            }

            RecordUpdated?.Invoke(this, EventArgs.Empty);
            _loading = false;
        }

        private void dgvItems_SelectionChanged(object sender, EventArgs e)
        {
            toolDelete.Enabled = toolAdd.Enabled && dgvItems.SelectedCells.Count > 0;
        }

        private void RadioCheckedChanged(object sender, EventArgs e)
        {
            cboCompany.Enabled = toolAdd.Enabled && rdoCompany.Checked;
            cboGovernment.Enabled = toolAdd.Enabled && rdoGovernment.Checked;
        }

        private async void CreateOrUpdateQuoteRequest(object sender, EventArgs e)
        {
            if (_loading) { return; }

            string notes = txtNotes.Text;
            long? companyID = rdoCompany.Checked ? (cboCompany.SelectedItem as DropDownItem<Company>)?.Object.CompanyID : null;
            long? governmentID = rdoGovernment.Checked ? (cboGovernment.SelectedItem as DropDownItem<Government>)?.Object.GovernmentID : null;

            if (QuotationRequestID != null)
            {
                PatchData patch = new PatchData(DataAccess.APIs.CompanyStudio, "QuotationRequest/Patch", PatchData.PatchMethods.Replace, QuotationRequestID, new Dictionary<string, object>()
                {
                    { "CompanyIDTo", companyID },
                    { "GovernmentIDTo", governmentID },
                    { "Notes", notes }
                });
                patch.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                await patch.Execute();
            }
            else
            {
                QuotationRequest quotationRequest = new QuotationRequest()
                {
                    CompanyIDFrom = Company.CompanyID,
                    CompanyIDTo = companyID,
                    GovernmentIDTo = governmentID,
                    Notes = notes
                };

                PostData post = new PostData(DataAccess.APIs.CompanyStudio, "QuotationRequest/Post", quotationRequest);
                post.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                QuotationRequest savedQuotationRequest = await post.Execute<QuotationRequest>();
                QuotationRequestID = savedQuotationRequest?.QuotationRequestID;
            }

            RecordUpdated?.Invoke(this, EventArgs.Empty);
        }

        private async void cmdIssueQuote_Click(object sender, EventArgs e)
        {
            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "QuotationRequest/Get/" + QuotationRequestID);
            get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            QuotationRequest quoteRequest = await get.GetObject<QuotationRequest>();

            if (quoteRequest != null)
            {
                long? quoteID = await quoteRequest.IssueQuote(Company.CompanyID, LocationModel.LocationID, Theme);
                if (quoteID != null)
                {
                    QuoteIssued?.Invoke(this, quoteID);
                    Close();
                }
            }
        }
    }
}
