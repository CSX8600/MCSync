using ClussPro.Base.Data;
using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Utility;
using ClussPro.ObjectBasedFramework.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModels.company;
using WebModels.gov;
using WebModels.invoicing;
using WebModels.purchasing;

namespace MesaService.ServiceTasks
{
    public class IssueFulfillmentInvoices : IServiceTask
    {
        public string Name => "Issue Fulfillment Invoices";

        private DateTime _nextRunTime = DateTime.Now;
        public DateTime NextRunTime => _nextRunTime;

        public bool Run()
        {
            _nextRunTime = DateTime.Now.AddMinutes(15);

            Search<Fulfillment> fulfillmentSearch = new Search<Fulfillment>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.Or,
                new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                    new LongSearchCondition<Fulfillment>()
                    {
                        Field = nameof(Fulfillment.InvoiceLineID),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Null
                    },
                    new IntSearchCondition<Fulfillment>()
                    {
                        Field = FieldPathUtility.CreateFieldPath<Fulfillment>(f => f.PurchaseOrderLine.PurchaseOrder.InvoiceSchedule),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = (int)PurchaseOrder.InvoiceSchedules.UponShipment
                    }),
                new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                    new LongSearchCondition<Fulfillment>()
                    {
                        Field = nameof(Fulfillment.InvoiceLineID),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Null
                    },
                    new IntSearchCondition<Fulfillment>()
                    {
                        Field = FieldPathUtility.CreateFieldPath<Fulfillment>(f => f.PurchaseOrderLine.PurchaseOrder.InvoiceSchedule),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = (int)PurchaseOrder.InvoiceSchedules.UponDelivery
                    },
                    new BooleanSearchCondition<Fulfillment>()
                    {
                        Field = nameof(Fulfillment.IsComplete),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = true
                    })));

            List<string> fields = FieldPathUtility.CreateFieldPathsAsList<Fulfillment>(f => new object[]
            {
                f.RailcarID,
                f.Railcar.ReportingMark,
                f.Railcar.ReportingNumber,
                f.PurchaseOrderLine.PurchaseOrderID,
                f.PurchaseOrderLine.ItemID,
                f.PurchaseOrderLine.UnitCost,
                f.PurchaseOrderLine.ItemDescription,
                f.PurchaseOrderLine.PurchaseOrder.PurchaseOrderID,
                f.PurchaseOrderLine.PurchaseOrder.LocationIDOrigin,
                f.PurchaseOrderLine.PurchaseOrder.LocationIDDestination,
                f.PurchaseOrderLine.PurchaseOrder.GovernmentIDOrigin,
                f.PurchaseOrderLine.PurchaseOrder.GovernmentIDDestination,
                f.PurchaseOrderLine.PurchaseOrder.AccountIDReceiving
            });

            List<Fulfillment> fulfillments = fulfillmentSearch.GetEditableReader(null, fields).ToList();

            Errors saveErrors = new Errors();
            foreach(IGrouping<long?, Fulfillment> fulfillmentsByPurchaseOrder in fulfillments.GroupBy(f => f.PurchaseOrderLine.PurchaseOrderID))
            {
                using (ITransaction transaction = SQLProviderFactory.GenerateTransaction())
                {
                    PurchaseOrder purchaseOrder = fulfillmentsByPurchaseOrder.First().PurchaseOrderLine.PurchaseOrder;

                    string nextInvoiceNumber = "PO" + purchaseOrder.PurchaseOrderID;
                    if (purchaseOrder.LocationIDDestination != null)
                    {
                        Location location = DataObject.GetReadOnlyByPrimaryKey<Location>(purchaseOrder.LocationIDDestination, transaction, FieldPathUtility.CreateFieldPathsAsList<Location>(l => new object[]
                        {
                            l.InvoiceNumberPrefix,
                            l.NextInvoiceNumber
                        }));

                        if (!string.IsNullOrEmpty(location.NextInvoiceNumber))
                        {
                            nextInvoiceNumber = $"{location.InvoiceNumberPrefix}{location.NextInvoiceNumber}";
                        }
                    }
                    else if (purchaseOrder.GovernmentIDDestination != null)
                    {
                        Government government = DataObject.GetReadOnlyByPrimaryKey<Government>(purchaseOrder.GovernmentIDDestination, transaction, FieldPathUtility.CreateFieldPathsAsList<Government>(g => new object[]
                        {
                            g.InvoiceNumberPrefix,
                            g.NextInvoiceNumber
                        }));

                        if (!string.IsNullOrEmpty(government.NextInvoiceNumber))
                        {
                            nextInvoiceNumber = $"{government.InvoiceNumberPrefix}{government.NextInvoiceNumber}";
                        }
                    }

                    Invoice invoice = DataObjectFactory.Create<Invoice>();
                    invoice.GovernmentIDTo = purchaseOrder.GovernmentIDOrigin;
                    invoice.GovernmentIDFrom = purchaseOrder.GovernmentIDDestination;
                    invoice.LocationIDTo = purchaseOrder.LocationIDOrigin;
                    invoice.LocationIDFrom = purchaseOrder.LocationIDDestination;
                    invoice.PurchaseOrderID = purchaseOrder.PurchaseOrderID;
                    invoice.InvoiceNumber = nextInvoiceNumber;
                    invoice.InvoiceDate = DateTime.Now;
                    invoice.DueDate = DateTime.Now.AddDays(14);
                    invoice.Description = "Charges for Purchase Order " + purchaseOrder.PurchaseOrderID;
                    invoice.AccountIDTo = purchaseOrder.AccountIDReceiving;
                    if (!invoice.Save(transaction))
                    {
                        saveErrors.AddRange(invoice.Errors.ToArray());
                        continue;
                    }

                    int errorCountBeforeLineSaves = saveErrors.Count();
                    foreach(Fulfillment fulfillment in fulfillmentsByPurchaseOrder)
                    {
                        InvoiceLine invoiceLine = DataObjectFactory.Create<InvoiceLine>();
                        invoiceLine.InvoiceID = invoice.InvoiceID;
                        invoiceLine.Quantity = fulfillment.Quantity;
                        invoiceLine.UnitCost = fulfillment.PurchaseOrderLine.UnitCost;

                        string railcarIdentifier = fulfillment.RailcarID == null ? "[Unknown]" : $"{fulfillment.Railcar.ReportingMark}{fulfillment.Railcar.ReportingNumber}";
                        invoiceLine.Description = $"Shipment on {fulfillment.FulfillmentTime?.ToString("MM/dd/yyyy HH:mm")} CT in railcar {railcarIdentifier}";

                        invoiceLine.ItemID = fulfillment.PurchaseOrderLine.ItemID;
                        invoiceLine.PurchaseOrderLineID = fulfillment.PurchaseOrderLineID;
                        if (!invoiceLine.Save(transaction))
                        {
                            saveErrors.AddRange(invoiceLine.Errors.ToArray());
                            break;
                        }

                        fulfillment.InvoiceLineID = invoiceLine.InvoiceLineID;
                        if (!fulfillment.Save(transaction))
                        {
                            saveErrors.AddRange(fulfillment.Errors.ToArray());
                            break;
                        }
                    }

                    if (saveErrors.Count() > errorCountBeforeLineSaves)
                    {
                        transaction.Rollback();
                        continue;
                    }

                    invoice.IssueInvoice(transaction);
                    if (invoice.Errors.Any())
                    {
                        transaction.Rollback();
                        continue;
                    }

                    transaction.Commit();
                }
            }

            if (saveErrors.Any())
            {
                throw new Exception(saveErrors.ToString());
            }

            return true;
        }
    }
}
