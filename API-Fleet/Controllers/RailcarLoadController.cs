using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using API.Common;
using API.Common.Attributes;
using API.Common.Extensions;
using API_Fleet.Extensions;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Utility;
using WebModels.fleet;

namespace API_Fleet.Controllers
{
    [MesabrookAuthorization]
    [ProgramAccess(new [] { "company", "gov" })]
    public class RailcarLoadController : DataObjectController<RailcarLoad>
    {
        public override IEnumerable<string> DefaultRetrievedFields => FieldPathUtility.CreateFieldPathsAsList<RailcarLoad>(rl => new List<object>()
        {
            rl.RailcarLoadID,
            rl.RailcarID,
            rl.ItemID,
            rl.Item.ItemID,
            rl.Item.Name,
            rl.Item.IsFluid,
            rl.Item.Image,
            rl.Quantity,
            rl.PurchaseOrderLineID
        });

        public override ISearchCondition GetBaseSearchCondition()
        {
            return new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.Or,
                    new LongSearchCondition<RailcarLoad>()
                    {
                        Field = FieldPathUtility.CreateFieldPathsAsList<RailcarLoad>(rl => new List<object>() { rl.Railcar.RailLocation.TrackID }).First(),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Null
                    },
                    new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                        new LongSearchCondition<RailcarLoad>()
                        {
                            Field = FieldPathUtility.CreateFieldPathsAsList<RailcarLoad>(rl => new List<object>() { rl.Railcar.RailLocation.Track.CompanyIDOwner }).First(),
                            SearchConditionType = this.CompanyID() == null ? SearchCondition.SearchConditionTypes.Null : SearchCondition.SearchConditionTypes.Equals,
                            Value = this.CompanyID()
                        },
                        new LongSearchCondition<RailcarLoad>()
                        {
                            Field = FieldPathUtility.CreateFieldPathsAsList<RailcarLoad>(rl => new List<object>() { rl.Railcar.RailLocation.Track.GovernmentIDOwner }).First(),
                            SearchConditionType = this.GovernmentID() == null ? SearchCondition.SearchConditionTypes.Null : SearchCondition.SearchConditionTypes.Equals,
                            Value = this.GovernmentID()
                        },
                        new LongSearchCondition<RailcarLoad>()
                        {
                            Field = FieldPathUtility.CreateFieldPathsAsList<RailcarLoad>(rl => new List<object>() { rl.Railcar.CompanyIDPossessor }).First(),
                            SearchConditionType = this.CompanyID() == null ? SearchCondition.SearchConditionTypes.Null : SearchCondition.SearchConditionTypes.Equals,
                            Value = this.CompanyID()
                        },
                        new LongSearchCondition<RailcarLoad>()
                        {
                            Field = FieldPathUtility.CreateFieldPathsAsList<RailcarLoad>(rl => new List<object>() { rl.Railcar.GovernmentIDPossessor }).First(),
                            SearchConditionType = this.GovernmentID() == null ? SearchCondition.SearchConditionTypes.Null : SearchCondition.SearchConditionTypes.Equals,
                            Value = this.GovernmentID()
                        })),
                new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.Or,
                    new LongSearchCondition<RailcarLoad>()
                    {
                        Field = FieldPathUtility.CreateFieldPathsAsList<RailcarLoad>(rl => new List<object>() { rl.Railcar.RailLocation.TrainID }).First(),
                        SearchConditionType = SearchCondition.SearchConditionTypes.Null
                    },
                    new ExistsSearchCondition<RailcarLoad>()
                    {
                        ExistsType = ExistsSearchCondition<RailcarLoad>.ExistsTypes.Exists,
                        RelationshipName = FieldPathUtility.CreateFieldPathsAsList<RailcarLoad>(rl => new List<object>() { rl.Railcar.RailLocation.Train.LiveLoad.LiveLoadSessions }).First(),
                        Condition = new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                            new LongSearchCondition<LiveLoadSession>()
                            {
                                Field = nameof(LiveLoadSession.UserID),
                                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                Value = SecurityProfile.UserID
                            },
                            new BooleanSearchCondition<LiveLoadSession>()
                            {
                                Field = nameof(LiveLoadSession.IsSessionValid),
                                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                                Value = true
                            })
                    }));
        }

        [HttpGet]
        public async Task<List<RailcarLoad>> GetByRailcarID(long? id)
        {
            Search<RailcarLoad> railcarLoadSearch = new Search<RailcarLoad>(new LongSearchCondition<RailcarLoad>()
            {
                Field = nameof(RailcarLoad.RailcarID),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = id
            });

            return railcarLoadSearch.GetReadOnlyReader(null, await FieldsToRetrieve()).ToList();
        }

        [HttpDelete]
        public IHttpActionResult DeleteForRailcar(long? id)
        {
            Search<RailcarLoad> loadSearch = new Search<RailcarLoad>(new LongSearchCondition<RailcarLoad>()
            {
                Field = nameof(RailcarLoad.RailcarID),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = id
            });

            foreach(RailcarLoad load in loadSearch.GetEditableReader())
            {
                if (!load.Delete())
                {
                    return load.HandleFailedValidation(this);
                }
            }

            return Ok();
        }
    }
}