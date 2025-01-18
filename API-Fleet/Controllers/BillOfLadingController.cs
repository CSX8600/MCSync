using API.Common.Attributes;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebModels.purchasing;

namespace API_Fleet.Controllers
{
    [MesabrookAuthorization]
    [ProgramAccess(new[] { "company", "gov" })]
    public class BillOfLadingController : ApiController
    {
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

        [HttpGet]
        public List<BillOfLading> GetByTracks([FromUri]long[] trackIDs, [FromUri]long? governmentID = null, [FromUri]long? companyID = null)
        {
            if (governmentID == null && companyID == null)
            {
                return new List<BillOfLading>();
            }

            SearchConditionGroup bolSearchConditionGroup = new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                new DateTimeSearchCondition<BillOfLading>()
                {
                    Field = nameof(BillOfLading.DeliveredDate),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Null
                },
                new LongSearchCondition<BillOfLading>()
                {
                    Field = FieldPathUtility.CreateFieldPath<BillOfLading>(bol => bol.Railcar.RailLocation.TrackID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.List,
                    List = trackIDs.ToList()
                });

            if (companyID != null)
            {
                bolSearchConditionGroup.SearchConditions.Add(new LongSearchCondition<BillOfLading>()
                {
                    Field = nameof(BillOfLading.CompanyIDCarrier),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = companyID
                });
            }

            if (governmentID != null)
            {
                bolSearchConditionGroup.SearchConditions.Add(new LongSearchCondition<BillOfLading>()
                {
                    Field = nameof(BillOfLading.GovernmentIDCarrier),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = governmentID
                });
            }

            Search<BillOfLading> billsOfLadingSearch = new Search<BillOfLading>(bolSearchConditionGroup);

            return billsOfLadingSearch.GetReadOnlyReader(null, _fields).ToList();
        }

        [HttpGet]
        public List<BillOfLading> GetByTrain([FromUri]long trainID, [FromUri] long? governmentID = null, [FromUri] long? companyID = null)
        {
            if (governmentID == null && companyID == null)
            {
                return new List<BillOfLading>();
            }

            SearchConditionGroup bolSearchConditionGroup = new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                new DateTimeSearchCondition<BillOfLading>()
                {
                    Field = nameof(BillOfLading.DeliveredDate),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Null
                },
                new LongSearchCondition<BillOfLading>()
                {
                    Field = FieldPathUtility.CreateFieldPath<BillOfLading>(bol => bol.Railcar.RailLocation.TrainID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = trainID
                });

            if (companyID != null)
            {
                bolSearchConditionGroup.SearchConditions.Add(new LongSearchCondition<BillOfLading>()
                {
                    Field = nameof(BillOfLading.CompanyIDCarrier),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = companyID
                });
            }

            if (governmentID != null)
            {
                bolSearchConditionGroup.SearchConditions.Add(new LongSearchCondition<BillOfLading>()
                {
                    Field = nameof(BillOfLading.GovernmentIDCarrier),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = governmentID
                });
            }

            Search<BillOfLading> billsOfLadingSearch = new Search<BillOfLading>(bolSearchConditionGroup);

            return billsOfLadingSearch.GetReadOnlyReader(null, _fields).ToList();
        }
    }
}
