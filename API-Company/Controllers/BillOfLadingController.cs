using API.Common.Attributes;
using API.Common.Extensions;
using API_Company.Attributes;
using ClussPro.Base.Data;
using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Utility;
using System;
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
    [LocationAccess(RequiredPermissions = new[] { nameof(LocationEmployee.ManagePurchaseOrders) })]
    public class BillOfLadingController : ApiController
    {
        protected long? CompanyID => long.Parse(Request.Headers.GetValues("CompanyID").First());

        private readonly List<string> _fields = FieldPathUtility.CreateFieldPathsAsList<BillOfLading>(bol => new List<object>()
        {
            bol.BillOfLadingID,
            bol.CompanyIDShipper,
            bol.CompanyShipper.CompanyID,
            bol.CompanyShipper.Name,
            bol.GovernmentIDShipper,
            bol.GovernmentShipper.GovernmentID,
            bol.GovernmentShipper.Name,
            bol.CompanyIDConsignee,
            bol.CompanyConsignee.CompanyID,
            bol.CompanyConsignee.Name,
            bol.GovernmentIDConsignee,
            bol.GovernmentConsignee.GovernmentID,
            bol.GovernmentConsignee.Name,
            bol.CompanyIDCarrier,
            bol.CompanyCarrier.CompanyID,
            bol.CompanyCarrier.Name,
            bol.GovernmentIDCarrier,
            bol.GovernmentCarrier.GovernmentID,
            bol.GovernmentCarrier.Name,
            bol.RailcarID,
            bol.Railcar.RailcarID,
            bol.Railcar.ReportingMark,
            bol.Railcar.ReportingNumber,
            bol.IssuedDate,
            bol.DeliveredDate,
            bol.Type,
            bol.BillOfLadingItems.First().BillOfLadingItemID,
            bol.BillOfLadingItems.First().BillOfLadingID,
            bol.BillOfLadingItems.First().ItemID,
            bol.BillOfLadingItems.First().Item.ItemID,
            bol.BillOfLadingItems.First().Item.Name,
            bol.BillOfLadingItems.First().ItemDescription,
            bol.BillOfLadingItems.First().Quantity,
            bol.BillOfLadingItems.First().UnitCost
        });

        public struct AcceptBOLParameter
        {
            public long? BillOfLadingID { get; set; }
        }

        [HttpPost]
        public async Task<IHttpActionResult> AcceptBOL(AcceptBOLParameter parameter)
        {
            using (ITransaction transaction = SQLProviderFactory.GenerateTransaction())
            {
                Search<BillOfLading> bolSearch = new Search<BillOfLading>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                    new LongSearchCondition<BillOfLading>()
                    {
                        Field = nameof(BillOfLading.BillOfLadingID),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = parameter.BillOfLadingID
                    },
                    new LongSearchCondition<BillOfLading>()
                    {
                        Field = nameof(BillOfLading.CompanyIDConsignee),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = CompanyID
                    },
                    new DateTimeSearchCondition<BillOfLading>()
                    {
                        Field = nameof(BillOfLading.DeliveredDate),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Null,
                    }));

                BillOfLading billOfLading = await Task.Run(() => bolSearch.GetEditable(transaction));
                if (billOfLading == null)
                {
                    return NotFound();
                }

                billOfLading.DeliveredDate = DateTime.Now;
                if (!await Task.Run(() => billOfLading.Save(transaction)))
                {
                    return billOfLading.HandleFailedValidation(this);
                }

                Search<Fulfillment> fufillmentSearch = new Search<Fulfillment>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                    new LongSearchCondition<Fulfillment>()
                    {
                        Field = nameof(Fulfillment.RailcarID),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = billOfLading.RailcarID
                    },
                    new BooleanSearchCondition<Fulfillment>()
                    {
                        Field = nameof(Fulfillment.IsComplete),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = false
                    }));

                Fulfillment fulfillment = await Task.Run(() => fufillmentSearch.GetEditable(transaction));
                if (fulfillment != null)
                {
                    fulfillment.IsComplete = true;
                    if (!await Task.Run(() => fulfillment.Save(transaction)))
                    {
                        return fulfillment.HandleFailedValidation(this);
                    }
                }

                transaction.Commit();
            }

            return Ok();
        }

        [HttpGet]
        public async Task<List<BillOfLading>> GetForCompany()
        {
            Search<BillOfLading> billOfLadingSearch = new Search<BillOfLading>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.Or,
                new LongSearchCondition<BillOfLading>()
                {
                    Field = nameof(BillOfLading.CompanyIDShipper),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = CompanyID
                },
                new LongSearchCondition<BillOfLading>()
                {
                    Field = nameof(BillOfLading.CompanyIDConsignee),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = CompanyID
                },
                new LongSearchCondition<BillOfLading>()
                {
                    Field = nameof(BillOfLading.CompanyIDCarrier),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = CompanyID
                }));

            return await Task.Run(() => billOfLadingSearch.GetReadOnlyReader(null, _fields).ToList());
        }

        [HttpGet]
        public async Task<BillOfLading> Get(long? id)
        {
            Search<BillOfLading> billOfLadingSearch = new Search<BillOfLading>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.Or,
                    new LongSearchCondition<BillOfLading>()
                    {
                        Field = nameof(BillOfLading.CompanyIDShipper),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = CompanyID
                    },
                    new LongSearchCondition<BillOfLading>()
                    {
                        Field = nameof(BillOfLading.CompanyIDConsignee),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = CompanyID
                    },
                    new LongSearchCondition<BillOfLading>()
                    {
                        Field = nameof(BillOfLading.CompanyIDCarrier),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                        Value = CompanyID
                    }),
                new LongSearchCondition<BillOfLading>()
                {
                    Field = nameof(BillOfLading.BillOfLadingID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = id
                }));

            return await Task.Run(() => billOfLadingSearch.GetReadOnly(null, _fields));
        }

        [HttpGet]
        public async Task<List<BillOfLading>> GetByRailcar(long? id)
        {
            Search<BillOfLading> billOfLadingSearch = new Search<BillOfLading>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                new LongSearchCondition<BillOfLading>()
                {
                    Field = nameof(BillOfLading.RailcarID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = id
                },
                new DateTimeSearchCondition<BillOfLading>()
                {
                    Field = nameof(BillOfLading.DeliveredDate),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Null
                }));

            return await Task.Run(() => billOfLadingSearch.GetReadOnlyReader(null, _fields).ToList());
        }
    }
}
