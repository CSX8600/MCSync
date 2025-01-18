using CompanyStudio.Extensions;
using CompanyStudio.Models;
using MesaSuite.Common.Data;
using MesaSuite.Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompanyStudio.Purchasing
{
    public partial class frmQuoteExplorer : BaseCompanyStudioContent, ILocationScoped
    {
        private const string REQUEST = nameof(REQUEST);
        private const string INCOMING = nameof(INCOMING);
        private const string OUTGOING = nameof(OUTGOING);
        private const string QUOTE = nameof(QUOTE);
        private const string EXPIRED = nameof(EXPIRED);

        public frmQuoteExplorer()
        {
            InitializeComponent();
            imgList.Images.Add(REQUEST, Properties.Resources.comment);
            imgList.Images.Add(INCOMING, Properties.Resources.arrow_down);
            imgList.Images.Add(OUTGOING, Properties.Resources.arrow_up);
            imgList.Images.Add(QUOTE, Properties.Resources.comments);
            imgList.Images.Add(EXPIRED, Properties.Resources.clock);
        }

        public Location LocationModel { get; set; }

        private async void frmQuoteExplorer_Load(object sender, EventArgs e)
        {
            AppendCompanyLocationNameToWindowText();
            await ReloadList();
        }

        private async Task ReloadList()
        {
            treQuotes.Nodes.Clear();
            TreeNode nodeRequest = new TreeNode("Quote Requests") { ImageKey = REQUEST, SelectedImageKey = REQUEST };
            TreeNode nodeRequestIncoming = new TreeNode("Incoming") { ImageKey = INCOMING, SelectedImageKey = INCOMING };
            TreeNode nodeRequestOutgoing = new TreeNode("Outgoing") { ImageKey = OUTGOING, SelectedImageKey = OUTGOING };
            nodeRequest.Nodes.Add(nodeRequestIncoming);
            nodeRequest.Nodes.Add(nodeRequestOutgoing);
            treQuotes.Nodes.Add(nodeRequest);

            TreeNode nodeQuote = new TreeNode("Issued Quotes") { ImageKey = QUOTE, SelectedImageKey = QUOTE };
            TreeNode nodeQuoteIncoming = new TreeNode("Incoming") { ImageKey = INCOMING, SelectedImageKey = INCOMING };
            TreeNode nodeQuoteOutgoing = new TreeNode("Outgoing") { ImageKey = OUTGOING, SelectedImageKey = OUTGOING };
            TreeNode nodeQuoteExpired = new TreeNode("Expired") { ImageKey = EXPIRED, SelectedImageKey = EXPIRED };
            nodeQuote.Nodes.Add(nodeQuoteIncoming);
            nodeQuote.Nodes.Add(nodeQuoteOutgoing);
            nodeQuote.Nodes.Add(nodeQuoteExpired);
            treQuotes.Nodes.Add(nodeQuote);

            GetData get = new GetData(DataAccess.APIs.CompanyStudio, "QuotationRequest/GetAll");
            get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            List<QuotationRequest> quotationRequests = await get.GetObject<List<QuotationRequest>>() ?? new List<QuotationRequest>();

            foreach(QuotationRequest request in quotationRequests)
            {
                TreeNode parent = null;
                StringBuilder textBuilder = new StringBuilder();
                if (request.CompanyIDFrom == Company.CompanyID)
                {
                    textBuilder.Append($"To {request.CompanyTo.Name} - ");
                    parent = nodeRequestOutgoing;
                }
                else if (request.CompanyIDTo == Company.CompanyID)
                {
                    textBuilder.Append($"From {request.CompanyFrom.Name} - ");
                    parent = nodeRequestIncoming;
                }

                if (parent == null)
                {
                    continue;
                }

                textBuilder.Append(string.Join(", ", request.QuotationRequestItems.Select(i => $"{i.Quantity}x {i.Item.Name}")));

                TreeNode node = new TreeNode(textBuilder.ToString(0, Math.Min(200, textBuilder.Length)));
                node.ImageKey = REQUEST;
                node.SelectedImageKey = REQUEST;
                node.Tag = request;
                parent.Nodes.Add(node);
            }

            get = new GetData(DataAccess.APIs.CompanyStudio, "Quotation/GetAll");
            get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            List<Quotation> quotations = await get.GetObject<List<Quotation>>() ?? new List<Quotation>();

            get = new GetData(DataAccess.APIs.CompanyStudio, "Quotation/GetReceived");
            get.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            quotations.AddRange(await get.GetObject<List<Quotation>>() ?? new List<Quotation>());

            foreach(Quotation quotation in quotations.OrderByDescending(q => q.ExpirationTime))
            {
                TreeNode parent = null;
                StringBuilder textBuilder = new StringBuilder();
                if (quotation.CompanyIDFrom == Company.CompanyID)
                {
                    textBuilder.Append($"To {quotation.CompanyTo.Name} - ");
                    parent = quotation.ExpirationTime <= DateTime.Now ? nodeQuoteExpired : nodeQuoteOutgoing;
                }
                else if (quotation.CompanyIDTo == Company.CompanyID)
                {
                    textBuilder.Append($"From {quotation.CompanyFrom.Name} - ");
                    parent = quotation.ExpirationTime <= DateTime.Now ? nodeQuoteExpired : nodeQuoteIncoming;
                }

                if (parent == null)
                {
                    continue;
                }

                textBuilder.Append(string.Join(", ", quotation.QuotationItems.Select(i => $"{i.Item.Name} @ {i.UnitCost}")));

                TreeNode node = new TreeNode(textBuilder.ToString(0, Math.Min(200, textBuilder.Length)));
                node.ImageKey = QUOTE;
                node.SelectedImageKey = QUOTE;
                node.Tag = quotation;
                parent.Nodes.Add(node);
            }

            treQuotes.ExpandAll();
        }

        private void treQuotes_AfterSelect(object sender, TreeViewEventArgs e)
        {
            toolDeleteQuote.Enabled = false;
            toolCloneQuote.Enabled = false;
            toolDeleteRequest.Enabled = false;
            toolIssueFromRequest.Enabled = false;

            if (e.Node.Tag is Quotation quotation)
            {
                if (quotation.CompanyIDFrom == Company.CompanyID)
                {
                    toolDeleteQuote.Enabled = true;
                    toolCloneQuote.Enabled = true;
                }
            }
            else if (e.Node.Tag is QuotationRequest request)
            {
                toolDeleteRequest.Enabled = true;

                if (request.CompanyIDTo == Company.CompanyID)
                {
                    toolIssueFromRequest.Enabled = true;
                }
            }
        }

        private void toolRequestQuote_Click(object sender, EventArgs e)
        {
            OpenQuoteRequest();
        }

        private void OpenQuoteRequest(long? quotationRequestID = null)
        {
            Quotes.frmQuoteRequest request = new Quotes.frmQuoteRequest()
            {
                QuotationRequestID = quotationRequestID
            };
            Studio.DecorateStudioContent(request);
            request.Company = Company;
            request.LocationModel = LocationModel;
            request.RecordUpdated += QuoteRequest_RecordUpdated;
            request.QuoteIssued += QuoteRequest_QuoteIssued;
            request.Show(Studio.dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }

        private async void QuoteRequest_QuoteIssued(object sender, long? e)
        {
            OpenQuote(e);

            await ReloadList();
        }

        private async void QuoteRequest_RecordUpdated(object sender, EventArgs e)
        {
            await ReloadList();
        }

        private void treQuotes_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            QuotationRequest request = e.Node.Tag as QuotationRequest;

            if (request != null)
            {
                OpenQuoteRequest(request.QuotationRequestID);
                return;
            }

            Quotation quote = e.Node.Tag as Quotation;
            if (quote != null)
            {
                OpenQuote(quote.QuotationID);
                return;
            }
        }

        private async void toolDeleteRequest_Click(object sender, EventArgs e)
        {
            if (!this.Confirm("Are you sure you want to delete this Quote Request?"))
            {
                return;
            }

            QuotationRequest request = treQuotes.SelectedNode?.Tag as QuotationRequest;
            if (request == null)
            {
                return;
            }

            DeleteData delete = new DeleteData(DataAccess.APIs.CompanyStudio, "QuotationRequest/Delete/" + request.QuotationRequestID);
            delete.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            await delete.Execute();

            if (delete.RequestSuccessful)
            {
                foreach(Quotes.frmQuoteRequest frmRequest in Studio.dockPanel.Documents.OfType<Quotes.frmQuoteRequest>().Where(qr => qr.QuotationRequestID == request.QuotationRequestID).ToList())
                {
                    frmRequest.Close();
                }
            }

            await ReloadList();
        }

        private void OpenQuote(long? quotationID = null)
        {
            Quotes.frmQuote quote = new Quotes.frmQuote()
            {
                QuotationID = quotationID
            };
            Studio.DecorateStudioContent(quote);
            quote.Company = Company;
            quote.LocationModel = LocationModel;
            quote.RecordUpdated += Quote_RecordUpdated;
            quote.Show(Studio.dockPanel, WeifenLuo.WinFormsUI.Docking.DockState.Document);
        }

        private async void Quote_RecordUpdated(object sender, EventArgs e)
        {
            await ReloadList();
        }

        private void toolIssueQuote_Click(object sender, EventArgs e)
        {
            OpenQuote();
        }

        private async void toolDeleteQuote_Click(object sender, EventArgs e)
        {
            if (!this.Confirm("Are you sure you want to delete this Quote?"))
            {
                return;
            }

            Quotation quote = treQuotes.SelectedNode?.Tag as Quotation;
            if (quote == null)
            {
                return;
            }

            DeleteData delete = new DeleteData(DataAccess.APIs.CompanyStudio, "Quotation/Delete/" + quote.QuotationID);
            delete.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            await delete.Execute();

            if (delete.RequestSuccessful)
            {
                foreach (Quotes.frmQuote frmRequest in Studio.dockPanel.Documents.OfType<Quotes.frmQuote>().Where(qr => qr.QuotationID == quote.QuotationID).ToList())
                {
                    frmRequest.Close();
                }
            }

            await ReloadList();
        }

        private async void toolIssueFromRequest_Click(object sender, EventArgs e)
        {
            QuotationRequest request = treQuotes.SelectedNode?.Tag as QuotationRequest;
            if (request == null)
            {
                return;
            }

            long? quoteID = await request.IssueQuote(Company.CompanyID, LocationModel.LocationID, Theme);
            if (quoteID == null)
            {
                return;
            }

            OpenQuote(quoteID);

            await ReloadList();
        }

        private async void toolCloneQuote_Click(object sender, EventArgs e)
        {
            Quotation quote = treQuotes.SelectedNode?.Tag as Quotation;
            if (quote == null)
            {
                return;
            }

            Quotation newQuote = quote.ShallowClone();
            newQuote.QuotationID = null;

            PostData post = new PostData(DataAccess.APIs.CompanyStudio, "Quotation/Post", newQuote);
            post.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
            newQuote = await post.Execute<Quotation>();
            if (!post.RequestSuccessful)
            {
                return;
            }

            foreach(QuotationItem item in quote.QuotationItems)
            {
                QuotationItem newItem = item.ShallowClone();
                newItem.QuotationItemID = null;
                newItem.QuotationID = newQuote.QuotationID;
                post = new PostData(DataAccess.APIs.CompanyStudio, "QuotationItem/Post", newItem);
                post.AddLocationHeader(Company.CompanyID, LocationModel.LocationID);
                await post.ExecuteNoResult();
            }

            OpenQuote(newQuote.QuotationID);

            await ReloadList();
        }

        private async void frmQuoteExplorer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                await ReloadList();
            }
        }
    }
}
