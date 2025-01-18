using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using API.Common;
using API.Common.Attributes;
using API.Common.Extensions;
using API_Government.Attributes;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using WebModels.company;
using WebModels.gov;
using WebModels.invoicing;
using WebModels.mesasys;

namespace API_Government.Controllers
{
    [MesabrookAuthorization]
    [ProgramAccess("gov")]
    [GovernmentAccess(RequiredPermissions = new[] { nameof(Official.ManageInvoices) })]
    public class InvoiceController : DataObjectController<Invoice>
    {
        private long GovernmentID => long.Parse(Request.Headers.GetValues("GovernmentID").First());

        public override IEnumerable<string> DefaultRetrievedFields => new[]
        {
            nameof(Invoice.InvoiceID),
            nameof(Invoice.LocationIDTo),
            $"{nameof(Invoice.LocationTo)}.{nameof(Location.Name)}",
            $"{nameof(Invoice.LocationTo)}.{nameof(Location.CompanyID)}",
            $"{nameof(Invoice.LocationTo)}.{nameof(Location.Company)}.{nameof(Company.Name)}",
            nameof(Invoice.LocationIDFrom),
            $"{nameof(Invoice.LocationFrom)}.{nameof(Location.Name)}",
            $"{nameof(Invoice.LocationFrom)}.{nameof(Location.CompanyID)}",
            $"{nameof(Invoice.LocationFrom)}.{nameof(Location.Company)}.{nameof(Company.Name)}",
            nameof(Invoice.GovernmentIDTo),
            $"{nameof(Invoice.GovernmentTo)}.{nameof(Government.Name)}",
            nameof(Invoice.GovernmentIDFrom),
            $"{nameof(Invoice.GovernmentFrom)}.{nameof(Government.Name)}",
            nameof(Invoice.InvoiceNumber),
            nameof(Invoice.InvoiceDate),
            nameof(Invoice.DueDate),
            nameof(Invoice.Description),
            nameof(Invoice.AccountIDTo),
            nameof(Invoice.AccountFromHistorical),
            nameof(Invoice.AccountIDFrom),
            nameof(Invoice.AccountToHistorical),
            nameof(Invoice.Status),
            nameof(Invoice.AutoReceive),
            $"{nameof(Invoice.InvoiceLines)}.{nameof(InvoiceLine.InvoiceLineID)}",
            $"{nameof(Invoice.InvoiceLines)}.{nameof(InvoiceLine.InvoiceID)}",
            $"{nameof(Invoice.InvoiceLines)}.{nameof(InvoiceLine.Quantity)}",
            $"{nameof(Invoice.InvoiceLines)}.{nameof(InvoiceLine.UnitCost)}",
            $"{nameof(Invoice.InvoiceLines)}.{nameof(InvoiceLine.Total)}",
            $"{nameof(Invoice.InvoiceLines)}.{nameof(InvoiceLine.Description)}",
            $"{nameof(Invoice.InvoiceLines)}.{nameof(InvoiceLine.ItemID)}",
            $"{nameof(Invoice.InvoiceLines)}.{nameof(InvoiceLine.Item)}.{nameof(Item.ItemID)}",
            $"{nameof(Invoice.InvoiceLines)}.{nameof(InvoiceLine.Item)}.{nameof(Item.Name)}",
            $"{nameof(Invoice.InvoiceLines)}.{nameof(InvoiceLine.Item)}.{nameof(Item.IsFluid)}"
        };

        public override ISearchCondition GetBaseSearchCondition() => new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.Or,
                new LongSearchCondition<Invoice>()
                {
                    Field = nameof(Invoice.GovernmentIDFrom),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = GovernmentID
                },
                new LongSearchCondition<Invoice>()
                {
                    Field = nameof(Invoice.GovernmentIDTo),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = GovernmentID
                });

        [HttpGet]
        public async Task<List<Invoice>> GetReceivables()
        {
            Search<Invoice> invoiceSearch = new Search<Invoice>(new LongSearchCondition<Invoice>()
            {
                Field = nameof(Invoice.GovernmentIDFrom),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = GovernmentID
            });

            return invoiceSearch.GetReadOnlyReader(null, await FieldsToRetrieve()).ToList();
        }

        [HttpPut]
        public override async Task<IHttpActionResult> Put(Invoice invoice)
        {
            Invoice dbInvoice = await GetPutObjectResult(invoice);

            if (!dbInvoice.DoesEntityHavePermissionToUpdateByStatus(dbInvoice.GovernmentIDFrom == GovernmentID) || !dbInvoice.DoesEntityHavePermissionToUpdateFields(dbInvoice.GovernmentIDFrom == GovernmentID))
            {
                dbInvoice.Errors.AddBaseMessage("Government does not have permission to update at least one field");
                return dbInvoice.HandleFailedValidation(this);
            }

            return await base.Put(dbInvoice);
        }

        [HttpDelete]
        public override IHttpActionResult Delete(long id)
        {
            Search<Invoice> invoiceSearch = new Search<Invoice>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                GetBaseSearchCondition(),
                new LongSearchCondition<Invoice>()
                {
                    Field = "InvoiceID",
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = id
                }));

            Invoice invoice = invoiceSearch.GetEditable();
            if (invoice == null)
            {
                return NotFound();
            }

            if (invoice.GovernmentIDFrom != GovernmentID)
            {
                invoice.Errors.AddBaseMessage("Government does not have permission to delete this Invoice");
                return invoice.HandleFailedValidation(this);
            }

            return base.Delete(id);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Issue(Invoice invoice)
        {
            Invoice dbInvoice = new Search<Invoice>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                GetBaseSearchCondition(),
                new LongSearchCondition<Invoice>()
                {
                    Field = "InvoiceID",
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = invoice.InvoiceID
                })).GetEditable(readOnlyFields: await FieldsToRetrieve());

            if (dbInvoice == null)
            {
                return NotFound();
            }

            if (dbInvoice.GovernmentIDFrom != GovernmentID)
            {
                dbInvoice.Errors.AddBaseMessage("Only Governments that created an Accounts Receivable Invoice may issue it");
                return dbInvoice.HandleFailedValidation(this);
            }

            dbInvoice.IssueInvoice();
            if (dbInvoice.Errors.Any())
            {
                return dbInvoice.HandleFailedValidation(this);
            }

            return Ok(dbInvoice);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Receive(Invoice invoice)
        {
            Invoice dbInvoice = new Search<Invoice>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                GetBaseSearchCondition(),
                new LongSearchCondition<Invoice>()
                {
                    Field = "InvoiceID",
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = invoice.InvoiceID
                })).GetEditable(readOnlyFields: await FieldsToRetrieve());

            if (dbInvoice == null)
            {
                return NotFound();
            }

            if (dbInvoice.GovernmentIDFrom != GovernmentID)
            {
                dbInvoice.Errors.AddBaseMessage("Only Governments that created an Accounts Receivable Invoice may receive it");
                return dbInvoice.HandleFailedValidation(this);
            }

            dbInvoice.ReceiveInvoice();
            if (dbInvoice.Errors.Any())
            {
                return dbInvoice.HandleFailedValidation(this);
            }

            return Ok(dbInvoice);
        }

        [HttpGet]
        public async Task<List<Invoice>> GetPayables()
        {
            Search<Invoice> invoiceSearch = new Search<Invoice>(new LongSearchCondition<Invoice>()
            {
                Field = nameof(Invoice.GovernmentIDTo),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = GovernmentID
            });

            return invoiceSearch.GetReadOnlyReader(null, await FieldsToRetrieve()).ToList();
        }

        [HttpPut]
        public IHttpActionResult AuthorizePayment(long id)
        {
            Search<Invoice> invoiceSearch = new Search<Invoice>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                GetBaseSearchCondition(),
                new LongSearchCondition<Invoice>()
                {
                    Field = nameof(Invoice.InvoiceID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = id
                }));

            Invoice invoice = invoiceSearch.GetEditable();
            if (invoice == null)
            {
                return NotFound();
            }

            if (invoice.GovernmentIDTo != GovernmentID)
            {
                invoice.Errors.Add(nameof(Invoice.Status), "Only Payors may authorize payment");
                return invoice.HandleFailedValidation(this);
            }

            invoice.Status = Invoice.Statuses.ReadyForReceipt;
            if (!invoice.Save())
            {
                return invoice.HandleFailedValidation(this);
            }

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult PutEmailImplementationIDPayableInvoice(long? id)
        {
            Government government = DataObject.GetEditableByPrimaryKey<Government>(GovernmentID, null, null);
            government.EmailImplementationIDPayableInvoice = id == -1L ? null : id;
            return government.Save() ? Ok() : government.HandleFailedValidation(this);
        }

        [HttpPut]
        public IHttpActionResult PutEmailImplementationIDReadyForReceipt(long? id)
        {
            Government government = DataObject.GetEditableByPrimaryKey<Government>(GovernmentID, null, null);
            government.EmailImplementationIDReadyForReceipt = id == -1L ? null : id;
            return government.Save() ? Ok() : government.HandleFailedValidation(this);
        }
    }
}