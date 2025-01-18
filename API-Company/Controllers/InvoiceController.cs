using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using API.Common;
using API.Common.Attributes;
using API.Common.Extensions;
using API_Company.Attributes;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using WebModels.company;
using WebModels.fleet;
using WebModels.gov;
using WebModels.invoicing;
using WebModels.mesasys;
using WebModels.purchasing;

namespace API_Company.Controllers
{
    [MesabrookAuthorization]
    [ProgramAccess("company")]
    [LocationAccess(RequiredPermissions = new[] { nameof(LocationEmployee.ManageInvoices) })]
    public class InvoiceController : DataObjectController<Invoice>
    {
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
            nameof(Invoice.PurchaseOrderID),
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
            $"{nameof(Invoice.InvoiceLines)}.{nameof(InvoiceLine.PurchaseOrderLineID)}",
            $"{nameof(Invoice.InvoiceLines)}.{nameof(InvoiceLine.PurchaseOrderLine)}.{nameof(PurchaseOrderLine.IsService)}",
            $"{nameof(Invoice.InvoiceLines)}.{nameof(InvoiceLine.PurchaseOrderLine)}.{nameof(PurchaseOrderLine.ServiceDescription)}",
            $"{nameof(Invoice.InvoiceLines)}.{nameof(InvoiceLine.PurchaseOrderLine)}.{nameof(PurchaseOrderLine.Quantity)}",
            $"{nameof(Invoice.InvoiceLines)}.{nameof(InvoiceLine.PurchaseOrderLine)}.{nameof(PurchaseOrderLine.ItemID)}",
            $"{nameof(Invoice.InvoiceLines)}.{nameof(InvoiceLine.PurchaseOrderLine)}.{nameof(PurchaseOrderLine.Item)}.{nameof(Item.Name)}",
            $"{nameof(Invoice.InvoiceLines)}.{nameof(InvoiceLine.PurchaseOrderLine)}.{nameof(PurchaseOrderLine.ItemDescription)}",
            $"{nameof(Invoice.InvoiceLines)}.{nameof(InvoiceLine.Fulfillment)}.{nameof(Fulfillment.FulfillmentID)}",
            $"{nameof(Invoice.InvoiceLines)}.{nameof(InvoiceLine.Fulfillment)}.{nameof(Fulfillment.FulfillmentTime)}",
            $"{nameof(Invoice.InvoiceLines)}.{nameof(InvoiceLine.Fulfillment)}.{nameof(Fulfillment.Quantity)}",
            $"{nameof(Invoice.InvoiceLines)}.{nameof(InvoiceLine.Fulfillment)}.{nameof(Fulfillment.Railcar)}.{nameof(Railcar.ReportingMark)}",
            $"{nameof(Invoice.InvoiceLines)}.{nameof(InvoiceLine.Fulfillment)}.{nameof(Fulfillment.Railcar)}.{nameof(Railcar.ReportingNumber)}",
            $"{nameof(Invoice.InvoiceLines)}.{nameof(InvoiceLine.Item)}.{nameof(Item.ItemID)}",
            $"{nameof(Invoice.InvoiceLines)}.{nameof(InvoiceLine.Item)}.{nameof(Item.Name)}",
            $"{nameof(Invoice.InvoiceLines)}.{nameof(InvoiceLine.Item)}.{nameof(Item.IsFluid)}",
            $"{nameof(Invoice.InvoiceSalesTaxes)}.{nameof(InvoiceSalesTax.InvoiceSalesTaxID)}",
            $"{nameof(Invoice.InvoiceSalesTaxes)}.{nameof(InvoiceSalesTax.InvoiceID)}",
            $"{nameof(Invoice.InvoiceSalesTaxes)}.{nameof(InvoiceSalesTax.Municipality)}",
            $"{nameof(Invoice.InvoiceSalesTaxes)}.{nameof(InvoiceSalesTax.Rate)}",
            $"{nameof(Invoice.InvoiceSalesTaxes)}.{nameof(InvoiceSalesTax.AppliedAmount)}"
        };

        private long LocationID => long.Parse(Request.Headers.GetValues("LocationID").First());

        public override ISearchCondition GetBaseSearchCondition()
        {
            return new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.Or,
                new LongSearchCondition<Invoice>()
                {
                    Field = "LocationIDFrom",
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = LocationID
                },
                new LongSearchCondition<Invoice>()
                {
                    Field = "LocationIDTo",
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = LocationID
                });
        }

        [HttpGet]
        public async Task<List<Invoice>> GetPayables()
        {
            Search<Invoice> invoiceSearch = new Search<Invoice>(new LongSearchCondition<Invoice>()
            {
                Field = nameof(Invoice.LocationIDTo),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = LocationID
            });

            return invoiceSearch.GetReadOnlyReader(null, await FieldsToRetrieve()).ToList();
        }

        [HttpGet]
        public async Task<List<Invoice>> GetReceivables()
        {
            Search<Invoice> invoiceSearch = new Search<Invoice>(new LongSearchCondition<Invoice>()
            {
                Field = nameof(Invoice.LocationIDFrom),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = LocationID
            });

            return invoiceSearch.GetReadOnlyReader(null, await FieldsToRetrieve()).ToList();
        }

        [HttpPut]
        public override async Task<IHttpActionResult> Put(Invoice dataObject)
        {
            Invoice dbInvoice = await GetPutObjectResult(dataObject);

            if (!dbInvoice.DoesEntityHavePermissionToUpdateByStatus(LocationID == dbInvoice.LocationIDFrom) || !dbInvoice.DoesEntityHavePermissionToUpdateFields(LocationID == dbInvoice.LocationIDFrom))
            {
                dbInvoice.Errors.AddBaseMessage("Location does not have permission to update at least one field");
                return dbInvoice.HandleFailedValidation(this);
            }
            
            return await base.Put(dataObject);
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

            if (invoice.LocationIDFrom != LocationID)
            {
                invoice.Errors.AddBaseMessage("Location does not have permission to delete this Invoice");
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

            if (dbInvoice.LocationIDFrom != LocationID)
            {
                dbInvoice.Errors.AddBaseMessage("Only Locations that created an Accounts Receivable Invoice may issue it");
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

            if (dbInvoice.LocationIDFrom != LocationID)
            {
                dbInvoice.Errors.AddBaseMessage("Only Locations that created an Accounts Receivable Invoice may receive it");
                return dbInvoice.HandleFailedValidation(this);
            }

            dbInvoice.ReceiveInvoice();
            if (dbInvoice.Errors.Any())
            {
                return dbInvoice.HandleFailedValidation(this);
            }

            return Ok(dbInvoice);
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

            if (invoice.LocationIDTo != LocationID)
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
            Location location = DataObject.GetEditableByPrimaryKey<Location>(LocationID, null, null);
            location.EmailImplementationIDPayableInvoice = id == -1L ? null : id;
            return location.Save() ? Ok() : location.HandleFailedValidation(this);
        }

        [HttpPut]
        public IHttpActionResult PutEmailImplementationIDReadyForReceipt(long? id)
        {
            Location location = DataObject.GetEditableByPrimaryKey<Location>(LocationID, null, null);
            location.EmailImplementationIDReadyForReceipt = id == -1L ? null : id;
            return location.Save() ? Ok() : location.HandleFailedValidation(this);
        }

        [HttpGet]
        [LocationAccess(OptionalPermissions = new[] { nameof(LocationEmployee.ManageInvoices), nameof(LocationEmployee.ManagePurchaseOrders)})]
        public async Task<IHttpActionResult> GetForPurchaseOrder(long? id)
        {
            Search<Invoice> invoiceSearch = new Search<Invoice>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                GetBaseSearchCondition(),
                new LongSearchCondition<Invoice>()
                {
                    Field = nameof(Invoice.PurchaseOrderID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = id
                }));

            return Ok(await Task.Run(async () => invoiceSearch.GetReadOnlyReader(null, await FieldsToRetrieve()).ToList()));
        }
    }
}