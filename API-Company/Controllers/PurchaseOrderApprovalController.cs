using API.Common.Attributes;
using API.Common.Extensions;
using API_Company.Attributes;
using ClussPro.Base.Data;
using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Schema;
using ClussPro.ObjectBasedFramework.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using WebModels.company;
using WebModels.purchasing;

namespace API_Company.Controllers
{
    [MesabrookAuthorization]
    [ProgramAccess("company")]
    [LocationAccess(RequiredPermissions = new[] { nameof(LocationEmployee.ManagePurchaseOrders)})]
    public class PurchaseOrderApprovalController : ApiController
    {
        private long? CompanyID => long.Parse(Request.Headers.GetValues("CompanyID").First());

        private static readonly IEnumerable<string> FieldsToRetrieve = Schema.GetSchemaObject<PurchaseOrderApproval>().GetFields().Select(f => f.FieldName);

        [HttpPost]
        public async Task<IHttpActionResult> Reject(long? id, [FromBody]RejectParameter reject)
        {
            Search<PurchaseOrderApproval> purchaseOrderApprovalSearch = new Search<PurchaseOrderApproval>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                new LongSearchCondition<PurchaseOrderApproval>()
                {
                    Field = nameof(PurchaseOrderApproval.PurchaseOrderApprovalID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = id
                },
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
                }));

            PurchaseOrderApproval purchaseOrderApproval = await Task.Run(() => purchaseOrderApprovalSearch.GetEditable());
            if (purchaseOrderApproval == null)
            {
                return NotFound();
            }

            using (ITransaction transaction = SQLProviderFactory.GenerateTransaction())
            {
                purchaseOrderApproval.ApprovalStatus = PurchaseOrderApproval.ApprovalStatuses.Rejected;
                purchaseOrderApproval.RejectionReason = reject.reason;
                if (!await Task.Run(() => purchaseOrderApproval.Save(transaction)))
                {
                    return purchaseOrderApproval.HandleFailedValidation(this);
                }

                PurchaseOrder purchaseOrder = await Task.Run(() => DataObject.GetEditableByPrimaryKey<PurchaseOrder>(purchaseOrderApproval.PurchaseOrderID, transaction, null));
                purchaseOrder.Status = PurchaseOrder.Statuses.Rejected;
                if (!await Task.Run(() => purchaseOrder.Save(transaction, new List<System.Guid>() { PurchaseOrder.SaveFlags.V_StatusChange })))
                {
                    return purchaseOrder.HandleFailedValidation(this);
                }

                transaction.Commit();
            }

            return Ok(DataObject.GetReadOnlyByPrimaryKey<PurchaseOrderApproval>(purchaseOrderApproval.PurchaseOrderApprovalID, null, FieldsToRetrieve));
        }

        public struct RejectParameter
        {
            public string reason { get; set; }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Approve(long? id, [FromBody]ApproveParameter approveParameter)
        {
            using (ITransaction transaction = SQLProviderFactory.GenerateTransaction())
            {
                Search<PurchaseOrderApproval> purchaseOrderApprovalSearch = new Search<PurchaseOrderApproval>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                    new LongSearchCondition<PurchaseOrderApproval>()
                    {
                        Field = nameof(PurchaseOrderApproval.PurchaseOrderApprovalID),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = id
                    },
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
                    }));

                PurchaseOrderApproval purchaseOrderApproval = await Task.Run(() => purchaseOrderApprovalSearch.GetEditable(transaction));
                if (purchaseOrderApproval == null)
                {
                    return NotFound();
                }

                purchaseOrderApproval.ApprovalStatus = PurchaseOrderApproval.ApprovalStatuses.Approved;
                purchaseOrderApproval.FutureAutoApprove = approveParameter.FutureAutoApprove;
                if (!await Task.Run(() => purchaseOrderApproval.Save(transaction)))
                {
                    return purchaseOrderApproval.HandleFailedValidation(this);
                }

                PurchaseOrder purchaseOrder = DataObject.GetEditableByPrimaryKey<PurchaseOrder>(purchaseOrderApproval.PurchaseOrderID, transaction, FieldPathUtility.CreateFieldPathsAsList<PurchaseOrder>(po => new List<object>() { po.LocationDestination.CompanyID }));
                if (CompanyID == purchaseOrder.LocationDestination.CompanyID)
                {
                    purchaseOrder.InvoiceSchedule = approveParameter.InvoiceSchedule;
                    purchaseOrder.AccountIDReceiving = approveParameter.AccountIDReceiving;
                }

                if (!await purchaseOrder.ApprovalSubmitted(transaction))
                {
                    return purchaseOrder.HandleFailedValidation(this);
                }

                transaction.Commit();
            }

            return Ok(DataObject.GetReadOnlyByPrimaryKey<PurchaseOrderApproval>(id, null, FieldsToRetrieve));
        }
        public struct ApproveParameter
        {
            public bool FutureAutoApprove { get; set; }
            public PurchaseOrder.InvoiceSchedules InvoiceSchedule { get; set; }
            public long? AccountIDReceiving { get; set; }
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetForPurchaseOrder(long? id)
        {
            Search<PurchaseOrderApproval> purchaseOrderApprovalSearch = new Search<PurchaseOrderApproval>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                new LongSearchCondition<PurchaseOrderApproval>()
                {
                    Field = nameof(PurchaseOrderApproval.PurchaseOrderID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = id
                },
                new LongSearchCondition<PurchaseOrderApproval>()
                {
                    Field = nameof(PurchaseOrderApproval.CompanyIDApprover),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = CompanyID
                }));

            PurchaseOrderApproval purchaseOrderApproval = await Task.Run(() => purchaseOrderApprovalSearch.GetReadOnlyReader(null, FieldsToRetrieve).FirstOrDefault());

            if (purchaseOrderApproval == null)
            {
                return NotFound();
            }

            return Ok(purchaseOrderApproval);
        }

        public struct SetAutoApproveParameter
        {
            public long? PurchaseOrderID { get; set; }
            public bool AutoApprove { get; set; }
        }

        [HttpPost]
        public async Task<IHttpActionResult> SetAutoApprove(SetAutoApproveParameter setAutoApproveParameter)
        {
            Search<PurchaseOrderApproval> purchaseOrderApprovalSearch = new Search<PurchaseOrderApproval>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                new LongSearchCondition<PurchaseOrderApproval>()
                {
                    Field = nameof(PurchaseOrderApproval.PurchaseOrderID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = setAutoApproveParameter.PurchaseOrderID
                },
                new LongSearchCondition<PurchaseOrderApproval>()
                {
                    Field = nameof(PurchaseOrderApproval.CompanyIDApprover),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = CompanyID
                }));

            PurchaseOrderApproval purchaseOrderApproval = await Task.Run(() => purchaseOrderApprovalSearch.GetEditable());
            if (purchaseOrderApproval == null)
            {
                return NotFound();
            }

            purchaseOrderApproval.FutureAutoApprove = setAutoApproveParameter.AutoApprove;
            if (!await Task.Run(() => purchaseOrderApproval.Save()))
            {
                return purchaseOrderApproval.HandleFailedValidation(this);
            }

            return Ok(await Task.Run(() => DataObject.GetReadOnlyByPrimaryKey<PurchaseOrderApproval>(purchaseOrderApproval.PurchaseOrderApprovalID, null, FieldsToRetrieve)));
        }
    }
}
