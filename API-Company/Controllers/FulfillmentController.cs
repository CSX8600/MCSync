using API.Common.Attributes;
using API.Common.Extensions;
using API_Company.Attributes;
using ClussPro.Base.Data;
using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Schema;
using ClussPro.ObjectBasedFramework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using WebModels.company;
using WebModels.purchasing;

namespace API_Company.Controllers
{
    [MesabrookAuthorization]
    [ProgramAccess("company")]
    [LocationAccess(RequiredPermissions = new[] { nameof(LocationEmployee.ManagePurchaseOrders)})]
    public class FulfillmentController : ApiController
    {
        protected long? LocationID => long.Parse(Request.Headers.GetValues("LocationID").First());

        private readonly List<string> _fields = Schema.GetSchemaObject<Fulfillment>().GetFields().Select(f => f.FieldName).ToList();

        [HttpPost]
        public async Task<IHttpActionResult> Post(Fulfillment fulfillment)
        {
            if (fulfillment.PurchaseOrderLineID == null)
            {
                fulfillment.Errors.Add("PurchaseOrderLineID", "Purchase Order Line is a required field");
                return fulfillment.HandleFailedValidation(this);
            }

            Search<PurchaseOrderLine> purchaseOrderLineSearch = new Search<PurchaseOrderLine>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                new LongSearchCondition<PurchaseOrderLine>()
                {
                    Field = nameof(PurchaseOrderLine.PurchaseOrderLineID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = fulfillment.PurchaseOrderLineID
                },
                new LongSearchCondition<PurchaseOrderLine>()
                {
                    Field = FieldPathUtility.CreateFieldPathsAsList<PurchaseOrderLine>(pol => new List<object>() { pol.PurchaseOrder.LocationIDDestination }).First(),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = LocationID
                }));

            if (!await Task.Run(() => purchaseOrderLineSearch.ExecuteExists(null)))
            {
                return new StatusCodeResult(System.Net.HttpStatusCode.Forbidden, this);
            }

            if(!await Task.Run(() => fulfillment.Save()))
            {
                return fulfillment.HandleFailedValidation(this);
            }

            return Ok(await Task.Run(() => DataObject.GetReadOnlyByPrimaryKey<Fulfillment>(fulfillment.FulfillmentID, null, _fields)));
        }

        public struct IssueBillsOfLadingParameter
        {
            public List<long?> FulfillmentIDs { get; set; }
        }

        [HttpPost]
        public async Task<IHttpActionResult> IssueBillsOfLading(IssueBillsOfLadingParameter parameter)
        {
            if (parameter.FulfillmentIDs == null || !parameter.FulfillmentIDs.Any())
            {
                return BadRequest("No FulfillmentIDs provided");
            }

            using (ITransaction transaction = SQLProviderFactory.GenerateTransaction())
            {
                List<BillOfLading> billsOfLading = new List<BillOfLading>();

                List<string> fieldPaths = FieldPathUtility.CreateFieldPathsAsList<Fulfillment>(f => new List<object>()
                {
                    f.RailcarID,
                    f.Quantity,
                    f.PurchaseOrderLine.PurchaseOrderID,
                    f.PurchaseOrderLine.ItemID,
                    f.PurchaseOrderLine.ItemDescription,
                    f.PurchaseOrderLine.ServiceDescription,
                    f.PurchaseOrderLine.UnitCost,
                    f.PurchaseOrderLine.PurchaseOrder.LocationDestination.CompanyID,
                    f.PurchaseOrderLine.PurchaseOrder.LocationOrigin.CompanyID,
                    f.PurchaseOrderLine.PurchaseOrder.GovernmentIDDestination,
                    f.PurchaseOrderLine.PurchaseOrder.GovernmentIDOrigin,
                    f.PurchaseOrderLine.FulfillmentPlanPurchaseOrderLines.First().FulfillmentPlan.RailcarID,
                    f.PurchaseOrderLine.FulfillmentPlanPurchaseOrderLines.First().FulfillmentPlan.FulfillmentPlanRoutes.First().SortOrder,
                    f.PurchaseOrderLine.FulfillmentPlanPurchaseOrderLines.First().FulfillmentPlan.FulfillmentPlanRoutes.First().CompanyIDTo,
                    f.PurchaseOrderLine.FulfillmentPlanPurchaseOrderLines.First().FulfillmentPlan.FulfillmentPlanRoutes.First().GovernmentIDTo
                });

                Search<Fulfillment> fulfillmentSearch = new Search<Fulfillment>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                    new LongSearchCondition<Fulfillment>()
                    {
                        Field = nameof(Fulfillment.FulfillmentID),
                        SearchConditionType = SearchCondition.SearchConditionTypes.List,
                        List = parameter.FulfillmentIDs.Where(id => id != null).Select(id => (long)id).ToList()
                    },
                    new LongSearchCondition<Fulfillment>()
                    {
                        Field = FieldPathUtility.CreateFieldPathsAsList<Fulfillment>(f => new List<object>() { f.PurchaseOrderLine.PurchaseOrder.LocationIDDestination }).First(),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = LocationID
                    }));

                List<Fulfillment> fulfillments = await Task.Run(() => fulfillmentSearch.GetReadOnlyReader(transaction, fieldPaths).ToList());
                foreach (IGrouping<long?, Fulfillment> fulfillmentsByRailcar in fulfillments.GroupBy(f => f.RailcarID))
                {
                    Fulfillment firstFulfillment = fulfillmentsByRailcar.First();
                    FulfillmentPlan fulfillmentPlan = firstFulfillment.PurchaseOrderLine.FulfillmentPlanPurchaseOrderLines?.FirstOrDefault(fppol => fppol.FulfillmentPlan.RailcarID == firstFulfillment.RailcarID)?.FulfillmentPlan;
                    if (fulfillmentPlan == null || fulfillmentPlan.FulfillmentPlanRoutes == null || !fulfillmentPlan.FulfillmentPlanRoutes.Any())
                    {
                        continue;
                    }

                    BillOfLading billOfLading = DataObjectFactory.Create<BillOfLading>();
                    billOfLading.PurchaseOrderID = firstFulfillment.PurchaseOrderLine.PurchaseOrderID;
                    billOfLading.RailcarID = fulfillmentsByRailcar.Key;
                    billOfLading.CompanyIDShipper = firstFulfillment.PurchaseOrderLine.PurchaseOrder.LocationDestination.CompanyID;
                    billOfLading.CompanyIDConsignee = firstFulfillment.PurchaseOrderLine.PurchaseOrder.LocationOrigin.CompanyID;
                    billOfLading.GovernmentIDShipper = firstFulfillment.PurchaseOrderLine.PurchaseOrder.GovernmentIDDestination;
                    billOfLading.GovernmentIDConsignee = firstFulfillment.PurchaseOrderLine.PurchaseOrder.GovernmentIDOrigin;
                    billOfLading.CompanyIDCarrier = fulfillmentPlan.FulfillmentPlanRoutes.OrderBy(fpr => fpr.SortOrder).First().CompanyIDTo;
                    billOfLading.GovernmentIDCarrier = fulfillmentPlan.FulfillmentPlanRoutes.OrderBy(fpr => fpr.SortOrder).First().GovernmentIDTo;
                    billOfLading.Type = BillOfLading.Types.FirstMile;
                    if (fulfillmentPlan.FulfillmentPlanRoutes.Count <= 2)
                    {
                        billOfLading.Type |= BillOfLading.Types.LastMile;
                    }
                    else if (fulfillmentPlan.FulfillmentPlanRoutes.Count > 2)
                    {
                        billOfLading.Type |= BillOfLading.Types.Interchange;
                    }
                    billOfLading.IssuedDate = DateTime.Now;

                    if (!await Task.Run(() => billOfLading.Save(transaction)))
                    {
                        return billOfLading.HandleFailedValidation(this);
                    }

                    billsOfLading.Add(billOfLading);

                    foreach(Fulfillment fulfillment in fulfillmentsByRailcar)
                    {
                        BillOfLadingItem billOfLadingItem = DataObjectFactory.Create<BillOfLadingItem>();
                        billOfLadingItem.BillOfLadingID = billOfLading.BillOfLadingID;
                        billOfLadingItem.ItemID = fulfillment.PurchaseOrderLine.ItemID;
                        billOfLadingItem.ItemDescription = fulfillment.PurchaseOrderLine.ItemDescription ?? fulfillment.PurchaseOrderLine.ServiceDescription;
                        billOfLadingItem.Quantity = fulfillment.Quantity;
                        billOfLadingItem.UnitCost = fulfillment.PurchaseOrderLine.UnitCost;

                        if (!await Task.Run(() => billOfLadingItem.Save(transaction)))
                        {
                            return billOfLadingItem.HandleFailedValidation(this);
                        }
                    }
                }

                transaction.Commit();

                return Ok(billsOfLading);
            }
        }

        [HttpGet]
        public async Task<List<Fulfillment>> GetCurrentByRailcar(long? id)
        {
            Search<Fulfillment> fulfillmentSearch = new Search<Fulfillment>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                new LongSearchCondition<Fulfillment>()
                {
                    Field = nameof(Fulfillment.RailcarID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = id
                },
                new BooleanSearchCondition<Fulfillment>()
                {
                    Field = nameof(Fulfillment.IsComplete),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = false
                }));

            return await Task.Run(() => fulfillmentSearch.GetReadOnlyReader(null, _fields).ToList());
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(long? id)
        {
            Fulfillment fulfillment = await Task.Run(() => DataObject.GetEditableByPrimaryKey<Fulfillment>(id, null, FieldPathUtility.CreateFieldPathsAsList<Fulfillment>(f => new object[]
            {
                f.PurchaseOrderLine.PurchaseOrder.LocationIDDestination
            })));

            if (fulfillment == null)
            {
                return NotFound();
            }

            if (fulfillment.PurchaseOrderLine.PurchaseOrder.LocationIDDestination != LocationID)
            {
                return new StatusCodeResult(System.Net.HttpStatusCode.Forbidden, this);
            }

            if (!await Task.Run(() => fulfillment.Delete()))
            {
                return fulfillment.HandleFailedValidation(this);
            }

            return Ok();
        }

        public struct PutInvoiceLineParameter
        {
            public long? FulfillmentID { get; set; }
            public long? InvoiceLineID { get; set; }
        }

        [HttpPost]
        [LocationAccess(OptionalPermissions = new[] { nameof(LocationEmployee.ManageInvoices), nameof(LocationEmployee.ManagePurchaseOrders) })]
        public async Task<IHttpActionResult> PutInvoiceLine(PutInvoiceLineParameter parameter)
        {
            Search<Fulfillment> fulfillmentSearch = new Search<Fulfillment>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                new LongSearchCondition<Fulfillment>()
                {
                    Field = nameof(Fulfillment.FulfillmentID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = parameter.FulfillmentID
                },
                new LongSearchCondition<Fulfillment>()
                {
                    Field = FieldPathUtility.CreateFieldPath<Fulfillment>(f => f.PurchaseOrderLine.PurchaseOrder.LocationIDDestination),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = LocationID
                }));

            Fulfillment fulfillment = await Task.Run(() => fulfillmentSearch.GetEditable());
            if (fulfillment == null)
            {
                return NotFound();
            }

            fulfillment.InvoiceLineID = parameter.InvoiceLineID;
            if (!await Task.Run(() => fulfillment.Save()))
            {
                return fulfillment.HandleFailedValidation(this);
            }

            return Ok(await Task.Run(() => DataObject.GetReadOnlyByPrimaryKey<Fulfillment>(fulfillment.FulfillmentID, null, _fields)));
        }
    }
}
