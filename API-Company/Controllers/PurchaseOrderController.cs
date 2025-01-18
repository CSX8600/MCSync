using API.Common;
using API.Common.Attributes;
using API.Common.Extensions;
using API_Company.Attributes;
using ClussPro.Base.Data;
using ClussPro.Base.Data.Query;
using ClussPro.Base.Extensions;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using WebModels.company;
using WebModels.gov;
using WebModels.invoicing;
using WebModels.purchasing;

namespace API_Company.Controllers
{
    [MesabrookAuthorization]
    [ProgramAccess("company")]
    [LocationAccess(RequiredPermissions = new[] { nameof(LocationEmployee.ManagePurchaseOrders) })]
    public class PurchaseOrderController : DataObjectController<PurchaseOrder>
    {
        private long? CompanyID => long.TryParse(Request.Headers.GetValues("CompanyID").First(), out long companyID) ? companyID : (long?)null;
        private long? LocationID => long.TryParse(Request.Headers.GetValues("LocationID").First(), out long locationID) ? locationID : (long?)null;

        public override IEnumerable<string> DefaultRetrievedFields => FieldPathUtility.CreateFieldPathsAsList<PurchaseOrder>(po => new List<object>()
        {
            po.PurchaseOrderID,
            po.LocationIDOrigin,
            po.LocationOrigin.LocationID,
            po.LocationOrigin.Name,
            po.LocationOrigin.CompanyID,
            po.LocationOrigin.Company.CompanyID,
            po.LocationOrigin.Company.Name,
            po.LocationIDDestination,
            po.LocationDestination.LocationID,
            po.LocationDestination.Name,
            po.LocationDestination.CompanyID,
            po.LocationDestination.Company.CompanyID,
            po.LocationDestination.Company.Name,
            po.GovernmentIDOrigin,
            po.GovernmentOrigin.GovernmentID,
            po.GovernmentOrigin.Name,
            po.GovernmentIDDestination,
            po.GovernmentDestination.GovernmentID,
            po.GovernmentDestination.Name,
            po.PurchaseOrderIDClonedFrom,
            po.PurchaseOrderDate,
            po.Status,
            po.Description,
            po.PurchaseOrderApprovals.First().PurchaseOrderApprovalID,
            po.PurchaseOrderApprovals.First().CompanyIDApprover,
            po.PurchaseOrderApprovals.First().CompanyApprover.Name,
            po.PurchaseOrderApprovals.First().GovernmentIDApprover,
            po.PurchaseOrderApprovals.First().GovernmentApprover.Name,
            po.PurchaseOrderApprovals.First().ApprovalStatus,
            po.PurchaseOrderApprovals.First().ApprovalPurpose,
            po.PurchaseOrderApprovals.First().RejectionReason,
            po.PurchaseOrderLines.First().PurchaseOrderLineID,
            po.PurchaseOrderLines.First().PurchaseOrderID,
            po.PurchaseOrderLines.First().IsService,
            po.PurchaseOrderLines.First().ServiceDescription,
            po.PurchaseOrderLines.First().ItemID,
            po.PurchaseOrderLines.First().Item.ItemID,
            po.PurchaseOrderLines.First().Item.Name,
            po.PurchaseOrderLines.First().Item.ItemNamespaceID,
            po.PurchaseOrderLines.First().Item.ItemNamespace.ItemNamespaceID,
            po.PurchaseOrderLines.First().Item.ItemNamespace.FriendlyName,
            po.PurchaseOrderLines.First().ItemDescription,
            po.PurchaseOrderLines.First().Quantity,
            po.PurchaseOrderLines.First().UnitCost,
            po.PurchaseOrderLines.First().FulfillmentPlanPurchaseOrderLines.First().FulfillmentPlanPurchaseOrderLineID,
            po.PurchaseOrderLines.First().FulfillmentPlanPurchaseOrderLines.First().FulfillmentPlanID,
            po.PurchaseOrderLines.First().FulfillmentPlanPurchaseOrderLines.First().FulfillmentPlan.FulfillmentPlanID,
            po.PurchaseOrderLines.First().FulfillmentPlanPurchaseOrderLines.First().FulfillmentPlan.RailcarID,
            po.PurchaseOrderLines.First().FulfillmentPlanPurchaseOrderLines.First().FulfillmentPlan.Railcar.RailcarID,
            po.PurchaseOrderLines.First().FulfillmentPlanPurchaseOrderLines.First().FulfillmentPlan.Railcar.ReportingMark,
            po.PurchaseOrderLines.First().FulfillmentPlanPurchaseOrderLines.First().FulfillmentPlan.Railcar.ReportingNumber,
            po.PurchaseOrderLines.First().Fulfillments.First().FulfillmentID,
            po.PurchaseOrderLines.First().Fulfillments.First().PurchaseOrderLineID,
            po.PurchaseOrderLines.First().Fulfillments.First().RailcarID,
            po.PurchaseOrderLines.First().Fulfillments.First().Railcar.ReportingMark,
            po.PurchaseOrderLines.First().Fulfillments.First().Railcar.ReportingNumber,
            po.PurchaseOrderLines.First().Fulfillments.First().Quantity,
            po.PurchaseOrderLines.First().Fulfillments.First().FulfillmentTime,
            po.PurchaseOrderLines.First().Fulfillments.First().IsComplete,
            po.PurchaseOrderLines.First().Fulfillments.First().InvoiceLineID,
            po.PurchaseOrderLines.First().RailcarLoads.First().RailcarID,
            po.PurchaseOrderLines.First().RailcarLoads.First().ItemID,
            po.PurchaseOrderLines.First().RailcarLoads.First().Quantity,
            po.PurchaseOrderLines.First().RailcarLoads.First().PurchaseOrderLineID,
            po.PurchaseOrderClones.First().PurchaseOrderID,
            po.PurchaseOrderTemplates.First().PurchaseOrderTemplateID
        });

        protected override IEnumerable<string> RequestableFields => FieldPathUtility.CreateFieldPathsAsList<PurchaseOrder>(po => new List<object>()
        {
            po.InvoiceSchedule,
            po.AccountIDReceiving
        });

        public override ISearchCondition GetBaseSearchCondition()
        {
            return new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.Or,
                new ExistsSearchCondition<PurchaseOrder>()
                {
                    RelationshipName = FieldPathUtility.CreateFieldPathsAsList<PurchaseOrder>(po => new List<object>() { po.LocationOrigin.LocationEmployees }).First(),
                    ExistsType = ExistsSearchCondition<PurchaseOrder>.ExistsTypes.Exists,
                    Condition = new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                        new LongSearchCondition<LocationEmployee>()
                        {
                            Field = FieldPathUtility.CreateFieldPathsAsList<LocationEmployee>(le => new List<object>() { le.Employee.UserID }).First(),
                            SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                            Value = SecurityProfile.UserID
                        },
                        new BooleanSearchCondition<LocationEmployee>()
                        {
                            Field = nameof(LocationEmployee.ManagePurchaseOrders),
                            SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                            Value = true
                        }
                    )
                },
                new ExistsSearchCondition<PurchaseOrder>()
                {
                    RelationshipName = FieldPathUtility.CreateFieldPathsAsList<PurchaseOrder>(po => new List<object>() { po.LocationDestination.LocationEmployees }).First(),
                    ExistsType = ExistsSearchCondition<PurchaseOrder>.ExistsTypes.Exists,
                    Condition = new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                        new LongSearchCondition<LocationEmployee>()
                        {
                            Field = FieldPathUtility.CreateFieldPathsAsList<LocationEmployee>(le => new List<object>() { le.Employee.UserID }).First(),
                            SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                            Value = SecurityProfile.UserID
                        },
                        new BooleanSearchCondition<LocationEmployee>()
                        {
                            Field = nameof(LocationEmployee.ManagePurchaseOrders),
                            SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                            Value = true
                        }
                    )
                });
        }

        [HttpGet]
        [LocationAccess(OptionalPermissions = new[] { nameof(LocationEmployee.ManagePurchaseOrders), nameof(LocationEmployee.ManageInvoices) })]
        public async Task<List<PurchaseOrder>> GetAllRelatedToLocation()
        {
            return await Task.Run(async () => new Search<PurchaseOrder>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                GetBaseSearchCondition(),
                new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.Or,
                    new LongSearchCondition<PurchaseOrder>()
                    {
                        Field = nameof(PurchaseOrder.LocationIDOrigin),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = LocationID
                    },
                    new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                        new LongSearchCondition<PurchaseOrder>()
                        {
                            Field = nameof(PurchaseOrder.LocationIDDestination),
                            SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                            Value = LocationID
                        },
                        new IntSearchCondition<PurchaseOrder>()
                        {
                            Field = nameof(PurchaseOrder.Status),
                            SearchConditionType = SearchCondition.SearchConditionTypes.NotList,
                            List = new List<int>()
                            {
                                (int)PurchaseOrder.Statuses.Draft,
                                (int)PurchaseOrder.Statuses.Rejected
                            }
                        }),
                    new ExistsSearchCondition<PurchaseOrder>()
                    {
                        RelationshipName = nameof(PurchaseOrder.PurchaseOrderApprovals),
                        ExistsType = ExistsSearchCondition<PurchaseOrder>.ExistsTypes.Exists,
                        Condition = new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                            new LongSearchCondition<PurchaseOrderApproval>()
                            {
                                Field = nameof(PurchaseOrderApproval.CompanyIDApprover),
                                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                Value = CompanyID
                            },
                            new IntSearchCondition<PurchaseOrderApproval>()
                            {
                                Field = nameof(PurchaseOrderApproval.ApprovalStatus),
                                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                Value = (int)PurchaseOrderApproval.ApprovalStatuses.Pending
                            })
                    }))).GetReadOnlyReader(null, await FieldsToRetrieve()).ToList());
        }

        [HttpPost]
        public async Task<IHttpActionResult> Submit(long? id)
        {
            using (ITransaction transaction = SQLProviderFactory.GenerateTransaction())
            {
                PurchaseOrder purchaseOrder = await Task.Run(() => DataObject.GetEditableByPrimaryKey<PurchaseOrder>(id, transaction, FieldPathUtility.CreateFieldPathsAsList<PurchaseOrder>(po => new List<object>() { po.LocationOrigin.CompanyID, po.LocationDestination.CompanyID })));
                if (!await purchaseOrder.Submit(transaction))
                {
                    return purchaseOrder.HandleFailedValidation(this);
                }

                transaction.Commit();
            }

            return Ok(await Task.Run(async () => DataObject.GetReadOnlyByPrimaryKey<PurchaseOrder>(id, null, await FieldsToRetrieve())));
        }

        [HttpPost]
        public async Task<IHttpActionResult> WithdrawSubmission(long? id)
        {
            using(ITransaction transaction = SQLProviderFactory.GenerateTransaction())
            {
                PurchaseOrder purchaseOrder = DataObject.GetEditableByPrimaryKey<PurchaseOrder>(id, transaction, null);
                await purchaseOrder.WithdrawSubmission(transaction);

                transaction.Commit();
            }

            return Ok(await Task.Run(async () => DataObject.GetReadOnlyByPrimaryKey<PurchaseOrder>(id, null, await FieldsToRetrieve())));
        }

        private readonly List<string> InvoiceFields = FieldPathUtility.CreateFieldPathsAsList<Invoice>(i => new List<object>()
        {
            i.InvoiceID,
            i.InvoiceDate,
            i.DueDate,
            i.Status,
            i.Amount,
            i.InvoiceNumber
        });
        [HttpGet]
        public async Task<IHttpActionResult> GetInvoicesForPurchaseOrder(long? id)
        {
            Search<PurchaseOrder> purchaseOrderForSecurityCheck = new Search<PurchaseOrder>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                new LongSearchCondition<PurchaseOrder>()
                {
                    Field = nameof(PurchaseOrder.PurchaseOrderID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = id
                },
                new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.Or,
                    new LongSearchCondition<PurchaseOrder>()
                    {
                        Field = nameof(PurchaseOrder.LocationIDOrigin),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = LocationID
                    },
                    new LongSearchCondition<PurchaseOrder>()
                    {
                        Field = nameof(PurchaseOrder.LocationIDDestination),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = LocationID
                    })));

            if (!await Task.Run(() => purchaseOrderForSecurityCheck.ExecuteExists(null)))
            {
                return NotFound();
            }

            Search<Invoice> invoiceSearch = new Search<Invoice>(new LongSearchCondition<Invoice>()
            {
                Field = nameof(Invoice.PurchaseOrderID),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = id
            });

            return Ok(await Task.Run(() => invoiceSearch.GetReadOnlyReader(null, InvoiceFields).ToList()));
        }

        public class CloneFromTemplateParameter
        {
            public long? PurchaseOrderTemplateID { get; set; }
        }

        [HttpPost]
        public async Task<IHttpActionResult> CloneFromTemplate(CloneFromTemplateParameter parameter)
        {
            PurchaseOrderTemplate template = await Task.Run(() => DataObject.GetReadOnlyByPrimaryKey<PurchaseOrderTemplate>(parameter.PurchaseOrderTemplateID, null, new[] 
            {
                nameof(PurchaseOrderTemplate.PurchaseOrderID) ,
                nameof(PurchaseOrderTemplate.LocationID)
            }));
            if (template == null)
            {
                return NotFound();
            }

            if (template.LocationID != LocationID)
            {
                return new StatusCodeResult(System.Net.HttpStatusCode.Forbidden, this);
            }

            long? newPurchaseOrderID;
            if ((newPurchaseOrderID = await template.CreateClonedPurchaseOrder()) == null)
            {
                return template.HandleFailedValidation(this);
            }

            return Created("PurchaseOrder/Get/" + newPurchaseOrderID, DataObject.GetReadOnlyByPrimaryKey<PurchaseOrder>(newPurchaseOrderID, null, await FieldsToRetrieve()));
        }

        public struct CloseParameter
        {
            public long? PurchaseOrderID { get; set; }
        }
        [HttpPost]
        public async Task<IHttpActionResult> Close(CloseParameter parameter)
        {
            Search<PurchaseOrder> purchaseOrderSearch = new Search<PurchaseOrder>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                GetBaseSearchCondition(),
                new LongSearchCondition<PurchaseOrder>()
                {
                    Field = nameof(PurchaseOrder.PurchaseOrderID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = parameter.PurchaseOrderID
                }));

            PurchaseOrder purchaseOrder = await Task.Run(() => purchaseOrderSearch.GetEditable());
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            purchaseOrder.Status = PurchaseOrder.Statuses.Completed;
            if (!await Task.Run(() => purchaseOrder.Save(saveFlags: new List<Guid>() { PurchaseOrder.SaveFlags.V_StatusChange})))
            {
                return purchaseOrder.HandleFailedValidation(this);
            }

            return Ok(await Task.Run(async () => DataObject.GetReadOnlyByPrimaryKey<PurchaseOrder>(purchaseOrder.PurchaseOrderID, null, await FieldsToRetrieve())));
        }
    }
}
