using CompanyStudio.Extensions;
using CompanyStudio.Purchasing.Quotes;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace CompanyStudio.Models
{
    public class QuotationRequest
    {
        public long? QuotationRequestID { get; set; }
        public long? CompanyIDFrom { get; set; }
        public Company CompanyFrom { get; set; }
        public long? CompanyIDTo { get; set; }
        public Company CompanyTo { get; set; }
        public long? GovernmentIDFrom { get; set; }
        public Government GovernmentFrom { get; set; }
        public long? GovernmentIDTo { get; set; }
        public Government GovernmentTo { get; set; }
        public string Notes { get; set; }

        public List<QuotationRequestItem> QuotationRequestItems { get; set; } = new List<QuotationRequestItem>();

        public async Task<long?> IssueQuote(long? companyID, long? locationID, ThemeBase theme)
        {
            List<frmQuoteBulkPricing.BulkPricingItem> bulkPricingItems = new List<frmQuoteBulkPricing.BulkPricingItem>();
            Dictionary<QuotationRequestItem, frmQuoteBulkPricing.BulkPricingItem> bulkPricingItemByRequestItem = new Dictionary<QuotationRequestItem, frmQuoteBulkPricing.BulkPricingItem>();
            foreach(QuotationRequestItem quotationRequestItem in QuotationRequestItems)
            {
                GetData get = new GetData(DataAccess.APIs.CompanyStudio, "PriceCheck/GetItem");
                get.QueryString.Add("locationID", locationID.ToString());
                get.QueryString.Add("itemID", quotationRequestItem.ItemID.ToString());
                get.QueryString.Add("quantity", quotationRequestItem.Quantity.ToString());
                LocationItem item = await get.GetObject<LocationItem>();
                if (item == null)
                {
                    get.QueryString["quantity"].Clear();
                    get.QueryString["quantity"].Add("1");
                    item = await get.GetObject<LocationItem>();
                }

                if (item == null)
                {
                    frmQuoteBulkPricing.BulkPricingItem bulkPricingItem = new frmQuoteBulkPricing.BulkPricingItem()
                    {
                        Image = quotationRequestItem.Item.Image,
                        Name = quotationRequestItem.Item.Name,
                        UnitCost = 0,
                        BulkQuantity = quotationRequestItem.Quantity
                    };
                    bulkPricingItems.Add(bulkPricingItem);
                    bulkPricingItemByRequestItem.Add(quotationRequestItem, bulkPricingItem);
                }
                else
                {
                    frmQuoteBulkPricing.BulkPricingItem bulkPricingItem = new frmQuoteBulkPricing.BulkPricingItem()
                    {
                        Image = quotationRequestItem.Item.Image,
                        Name = quotationRequestItem.Item.Name,
                        UnitCost = item.Quantity == 0 ? 0 : item.BasePrice / item.Quantity,
                        BulkQuantity = quotationRequestItem.Quantity
                    };
                    bulkPricingItems.Add(bulkPricingItem);
                    bulkPricingItemByRequestItem.Add(quotationRequestItem, bulkPricingItem);
                }
            }

            if (bulkPricingItems.Any())
            {
                frmQuoteBulkPricing bulkPricing = new frmQuoteBulkPricing()
                {
                    Theme = theme,
                    BulkPricingItems = bulkPricingItems
                };
                DialogResult result = bulkPricing.ShowDialog();
                if (result == DialogResult.Cancel)
                {
                    return null;
                }
            }

            Quotation quote = new Quotation()
            {
                CompanyIDFrom = CompanyIDTo,
                CompanyIDTo = CompanyIDFrom,
                GovernmentIDTo = GovernmentIDFrom,
                ExpirationTime = DateTime.Now,
                IsRepeatable = false
            };

            PostData post = new PostData(DataAccess.APIs.CompanyStudio, "Quotation/Post", quote);
            post.AddLocationHeader(companyID, locationID);
            quote = await post.Execute<Quotation>();
            if (!post.RequestSuccessful)
            {
                return null;
            }

            foreach(QuotationRequestItem quotationRequestItem in QuotationRequestItems)
            {
                frmQuoteBulkPricing.BulkPricingItem bulkPricingItem = bulkPricingItemByRequestItem.GetOrDefault(quotationRequestItem);

                QuotationItem quotationItem = new QuotationItem()
                {
                    QuotationID = quote.QuotationID,
                    ItemID = quotationRequestItem.ItemID,
                    MinimumQuantity = bulkPricingItem?.BulkQuantity ?? 0,
                    UnitCost = bulkPricingItem?.UnitCost ?? 0
                };
                PostData postItem = new PostData(DataAccess.APIs.CompanyStudio, "QuotationItem/Post", quotationItem);
                postItem.AddLocationHeader(companyID, locationID);
                await postItem.ExecuteNoResult();
            }

            DeleteData delete = new DeleteData(DataAccess.APIs.CompanyStudio, "QuotationRequest/Delete/" + QuotationRequestID);
            delete.AddLocationHeader(companyID, locationID);
            await delete.Execute();

            return quote.QuotationID;
        }
    }
}
