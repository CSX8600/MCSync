using API.Common;
using API.Common.Attributes;
using ClussPro.Base.Data;
using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Schema;
using ClussPro.ObjectBasedFramework.Utility;
using ClussPro.ObjectBasedFramework.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebModels.fleet;

namespace API_Fleet.Controllers
{
    [MesabrookAuthorization]
    [ProgramAccess(new[] { "gov", "company" })]
    public class RailLocationController : ApiController
    {
        private static List<string> DefaultRetrievedFields => FieldPathUtility.CreateFieldPathsAsList<RailLocation>(rl => new List<object>()
        {
            rl.RailLocationID,
            rl.RailcarID,
            rl.LocomotiveID,
            rl.TrackID,
            rl.Track.TrackID,
            rl.Track.Name,
            rl.Track.RailDistrictID,
            rl.Track.RailDistrict.RailDistrictID,
            rl.Track.RailDistrict.Name,
            rl.TrainID,
            rl.Train.TrainID,
            rl.Train.TimeOnDuty,
            rl.Train.TrainSymbolID,
            rl.Train.TrainSymbol.TrainSymbolID,
            rl.Train.TrainSymbol.Name,
            rl.Train.TrainSymbol.GovernmentIDOperator,
            rl.Train.TrainSymbol.GovernmentOperator.GovernmentID,
            rl.Train.TrainSymbol.GovernmentOperator.Name,
            rl.Train.TrainSymbol.CompanyIDOperator,
            rl.Train.TrainSymbol.CompanyOperator.CompanyID,
            rl.Train.TrainSymbol.CompanyOperator.Name,
            rl.Train.TrainFuelRecords.First().LocomotiveID,
            rl.Train.TrainFuelRecords.First().FuelStart,
            rl.Position,
            rl.Railcar.RailcarID,
            rl.Railcar.ReportingMark,
            rl.Railcar.ReportingNumber,
            rl.Railcar.RailcarModelID,
            rl.Railcar.RailcarModel.RailcarModelID,
            rl.Railcar.RailcarModel.Name,
            rl.Railcar.RailcarModel.Length,
            rl.Railcar.CompanyIDPossessor,
            rl.Railcar.CompanyPossessor.CompanyID,
            rl.Railcar.CompanyPossessor.Name,
            rl.Railcar.GovernmentIDPossessor,
            rl.Railcar.GovernmentPossessor.GovernmentID,
            rl.Railcar.GovernmentPossessor.Name,
            rl.Railcar.CompanyLeasedTo.CompanyID,
            rl.Railcar.CompanyLeasedTo.Name,
            rl.Railcar.GovernmentLeasedTo.GovernmentID,
            rl.Railcar.GovernmentLeasedTo.Name,
            rl.Railcar.CompanyIDOwner,
            rl.Railcar.CompanyOwner.CompanyID,
            rl.Railcar.CompanyOwner.Name,
            rl.Railcar.GovernmentIDOwner,
            rl.Railcar.GovernmentOwner.GovernmentID,
            rl.Railcar.GovernmentOwner.Name,
            rl.Railcar.TrackIDDestination,
            rl.Railcar.TrackDestination.TrackID,
            rl.Railcar.TrackDestination.Name,
            rl.Railcar.TrackIDStrategic,
            rl.Railcar.TrackStrategic.TrackID,
            rl.Railcar.TrackStrategic.Name,
            rl.Railcar.RailcarLoads.First().RailcarLoadID,
            rl.Railcar.RailcarLoads.First().RailcarID,
            rl.Railcar.RailcarLoads.First().ItemID,
            rl.Railcar.RailcarLoads.First().Quantity,
            rl.Railcar.RailcarLoads.First().Item.ItemID,
            rl.Railcar.RailcarLoads.First().Item.Name,
            rl.Railcar.RailcarLoads.First().Item.IsFluid,
            rl.Railcar.RailcarLoads.First().Item.Image,
            rl.Locomotive.LocomotiveID,
            rl.Locomotive.ReportingMark,
            rl.Locomotive.ReportingNumber,
            rl.Locomotive.LocomotiveModelID,
            rl.Locomotive.LocomotiveModel.LocomotiveModelID,
            rl.Locomotive.LocomotiveModel.Name,
            rl.Locomotive.LocomotiveModel.Length,
            rl.Locomotive.CompanyIDPossessor,
            rl.Locomotive.CompanyPossessor.CompanyID,
            rl.Locomotive.CompanyPossessor.Name,
            rl.Locomotive.GovernmentIDPossessor,
            rl.Locomotive.GovernmentPossessor.GovernmentID,
            rl.Locomotive.GovernmentPossessor.Name,
            rl.Locomotive.CompanyLeasedTo.CompanyID,
            rl.Locomotive.CompanyLeasedTo.Name,
            rl.Locomotive.GovernmentLeasedTo.GovernmentID,
            rl.Locomotive.GovernmentLeasedTo.Name,
            rl.Locomotive.CompanyIDOwner,
            rl.Locomotive.CompanyOwner.CompanyID,
            rl.Locomotive.CompanyOwner.Name,
            rl.Locomotive.GovernmentIDOwner,
            rl.Locomotive.GovernmentOwner.GovernmentID,
            rl.Locomotive.GovernmentOwner.Name,
        });

        public List<RailLocation> GetByTrain(long? id)
        {
            Search<RailLocation> railLocationSearch = new Search<RailLocation>();
            if (id != null)
            {
                railLocationSearch.SearchCondition = new LongSearchCondition<RailLocation>()
                {
                    Field = nameof(RailLocation.TrainID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = id
                };
            }
            else
            {
                railLocationSearch.SearchCondition = new LongSearchCondition<RailLocation>()
                {
                    Field = nameof(RailLocation.TrainID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.NotNull
                };
            }

            return railLocationSearch.GetReadOnlyReader(null, DefaultRetrievedFields).ToList();
        }

        public List<RailLocation> GetByTrack(long? id)
        {
            Search<RailLocation> railLocationSearch = new Search<RailLocation>();
            if (id != null)
            {
                railLocationSearch.SearchCondition = new LongSearchCondition<RailLocation>()
                {
                    Field = nameof(RailLocation.TrackID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = id
                };
            }
            else
            {
                railLocationSearch.SearchCondition = new LongSearchCondition<RailLocation>()
                {
                    Field = nameof(RailLocation.TrackID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.NotNull
                };
            }

            return railLocationSearch.GetReadOnlyReader(null, DefaultRetrievedFields).ToList();
        }

        public RailLocation GetForRailcar(long? id)
        {
            Search<RailLocation> search = new Search<RailLocation>(new LongSearchCondition<RailLocation>()
            {
                Field = nameof(RailLocation.RailcarID),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = id
            });

            return search.GetReadOnly(null, DefaultRetrievedFields);
        }

        public RailLocation GetForLocomotive(long? id)
        {
            Search<RailLocation> search = new Search<RailLocation>(new LongSearchCondition<RailLocation>()
            {
                Field = nameof(RailLocation.LocomotiveID),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = id
            });

            return search.GetReadOnly(null, DefaultRetrievedFields);
        }

        private static readonly List<string> _modifyFields = Schema.GetSchemaObject<RailLocation>().GetFields().Select(f => f.FieldName).ToList();
        [HttpPut]
        public IHttpActionResult Modify(ModifyParameter data)
        {
            Errors errors = new Errors();

            using (ITransaction transaction = SQLProviderFactory.GenerateTransaction())
            {
                HashSet<long?> railLocationIDsOrphaned = new HashSet<long?>();
                HashSet<long?> railLocationIDsAccountedFor = new HashSet<long?>();
                if (data.ModifiedTracksByID != null)
                {
                    foreach (KeyValuePair<long?, List<RailLocation>> modifiedLocationsByTrack in data.ModifiedTracksByID)
                    {
                        Dictionary<long?, RailLocation> modifiedLocationsByID = modifiedLocationsByTrack.Value.ToDictionary(rl => rl.RailLocationID);

                        Search<RailLocation> currentSituationSearch = new Search<RailLocation>(new LongSearchCondition<RailLocation>()
                        {
                            Field = nameof(RailLocation.TrackID),
                            SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                            Value = modifiedLocationsByTrack.Key
                        });
                        List<RailLocation> currentLocations = currentSituationSearch.GetReadOnlyReader(transaction, _modifyFields).ToList();

                        if (modifiedLocationsByTrack.Value.Any())
                        {
                            Search<RailLocation> modifiedLocationsSearch = new Search<RailLocation>(new LongSearchCondition<RailLocation>()
                            {
                                Field = nameof(RailLocation.RailLocationID),
                                SearchConditionType = SearchCondition.SearchConditionTypes.List,
                                List = modifiedLocationsByTrack.Value.Select(rl => rl.RailLocationID.Value).ToList()
                            });
                            foreach (RailLocation modifiedLocation in modifiedLocationsSearch.GetEditableReader(transaction))
                            {
                                RailLocation modifiedLocationFromData = modifiedLocationsByID[modifiedLocation.RailLocationID];
                                modifiedLocation.TrainID = modifiedLocationFromData.TrainID;
                                modifiedLocation.TrackID = modifiedLocationFromData.TrackID;
                                modifiedLocation.Position = modifiedLocationFromData.Position;

                                CreateRailLocationTransaction(modifiedLocation, data.TimeMoved, transaction);

                                if (!modifiedLocation.Save(transaction))
                                {
                                    errors.AddRange(modifiedLocation.Errors.ToArray());
                                }

                                if (currentLocations.Any(rl => rl.RailLocationID == modifiedLocation.RailLocationID))
                                {
                                    currentLocations.RemoveAll(rl => rl.RailLocationID == modifiedLocation.RailLocationID);
                                }

                                railLocationIDsAccountedFor.Add(modifiedLocation.RailLocationID);
                                railLocationIDsOrphaned.Remove(modifiedLocation.RailLocationID);
                            }
                        }

                        foreach (RailLocation orphanedLocation in currentLocations.Where(rl => !railLocationIDsAccountedFor.Contains(rl.RailLocationID)))
                        {
                            railLocationIDsOrphaned.Add(orphanedLocation.RailLocationID);
                        }
                    }
                }

                if (data.ModifiedTrainsByID != null)
                {
                    foreach (KeyValuePair<long?, List<RailLocation>> modifiedLocationsByTrain in data.ModifiedTrainsByID)
                    {
                        Dictionary<long?, RailLocation> modifiedLocationsByID = modifiedLocationsByTrain.Value.ToDictionary(rl => rl.RailLocationID);

                        Search<RailLocation> currentSituationSearch = new Search<RailLocation>(new LongSearchCondition<RailLocation>()
                        {
                            Field = nameof(RailLocation.TrainID),
                            SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                            Value = modifiedLocationsByTrain.Key
                        });
                        List<RailLocation> currentLocations = currentSituationSearch.GetReadOnlyReader(transaction, _modifyFields).ToList();

                        if (modifiedLocationsByTrain.Value.Any())
                        {
                            Search<RailLocation> modifiedLocationsSearch = new Search<RailLocation>(new LongSearchCondition<RailLocation>()
                            {
                                Field = nameof(RailLocation.RailLocationID),
                                SearchConditionType = SearchCondition.SearchConditionTypes.List,
                                List = modifiedLocationsByTrain.Value.Select(rl => rl.RailLocationID.Value).ToList()
                            });
                            foreach (RailLocation modifiedLocation in modifiedLocationsSearch.GetEditableReader(transaction))
                            {
                                RailLocation modifiedLocationFromData = modifiedLocationsByID[modifiedLocation.RailLocationID];
                                modifiedLocation.TrainID = modifiedLocationFromData.TrainID;
                                modifiedLocation.TrackID = modifiedLocationFromData.TrackID;
                                modifiedLocation.Position = modifiedLocationFromData.Position;

                                CreateRailLocationTransaction(modifiedLocation, data.TimeMoved, transaction);

                                if (!modifiedLocation.Save(transaction))
                                {
                                    errors.AddRange(modifiedLocation.Errors.ToArray());
                                }

                                if (currentLocations.Any(rl => rl.RailLocationID == modifiedLocation.RailLocationID))
                                {
                                    currentLocations.RemoveAll(rl => rl.RailLocationID == modifiedLocation.RailLocationID);
                                }

                                railLocationIDsAccountedFor.Add(modifiedLocation.RailLocationID);
                                railLocationIDsOrphaned.Remove(modifiedLocation.RailLocationID);
                            }
                        }

                        foreach (RailLocation orphanedLocation in currentLocations.Where(rl => !railLocationIDsAccountedFor.Contains(rl.RailLocationID)))
                        {
                            railLocationIDsOrphaned.Add(orphanedLocation.RailLocationID);
                        }
                    }
                }

                if (data.ModifiedTracksByID != null)
                {
                    foreach (long? modifiedTrackID in data.ModifiedTracksByID.Keys)
                    {
                        errors.AddRange(Track.Reorder(modifiedTrackID, transaction).ToArray());
                    }
                }

                if (data.ModifiedTrainsByID != null)
                {
                    foreach (long? modifiedTrainID in data.ModifiedTrainsByID.Keys)
                    {
                        errors.AddRange(Train.Reorder(modifiedTrainID, transaction).ToArray());
                    }
                }

                if (railLocationIDsOrphaned.Any())
                {
                    errors.AddBaseMessage("The modification request results in stock not being located on either a track or train. This is not allowed.");
                }

                if (errors.Any())
                {
                    transaction.Rollback();
                    return BadRequest(errors.ToString());
                }
                else
                {
                    transaction.Commit();
                    return Ok();
                }
            }
        }

        private Errors CreateRailLocationTransaction(RailLocation railLocation, DateTime? timeMoved, ITransaction transaction)
        {
            if (railLocation.RailcarID == null)
            {
                return new Errors();
            }

            if (railLocation.IsFieldDirty(nameof(RailLocation.TrackID)) && railLocation.TrackID != null)
            {
                RailcarLocationTransaction railcarLocationTransaction = DataObjectFactory.Create<RailcarLocationTransaction>();
                railcarLocationTransaction.RailcarID = railLocation.RailcarID;
                railcarLocationTransaction.TrackIDNew = railLocation.TrackID;
                railcarLocationTransaction.TransactionTime = timeMoved;
                if (!railcarLocationTransaction.Save(transaction))
                {
                    return railcarLocationTransaction.Errors;
                }
            }
            
            if (railLocation.IsFieldDirty(nameof(RailLocation.TrainID)))
            {
                if (railLocation.TrainID != null)
                {
                    Train train = DataObject.GetReadOnlyByPrimaryKey<Train>(railLocation.TrainID, transaction, new List<string>() { nameof(Train.Status) });
                    bool partialTrip = train?.Status == Train.Statuses.EnRoute;

                    RailcarLocationTransaction railcarLocationTransaction = DataObjectFactory.Create<RailcarLocationTransaction>();
                    railcarLocationTransaction.RailcarID = railLocation.RailcarID;
                    railcarLocationTransaction.TrainIDNew = railLocation.TrainID;
                    railcarLocationTransaction.TransactionTime = timeMoved;
                    railcarLocationTransaction.IsPartialTrainTrip = partialTrip;
                    if (!railcarLocationTransaction.Save(transaction))
                    {
                        return railcarLocationTransaction.Errors;
                    }
                }

                if (railLocation.GetDirtyValue(nameof(RailLocation.TrainID)) != null)
                {
                    long? oldTrainID = railLocation.GetDirtyValue(nameof(RailLocation.TrainID)) as long?;

                    Search<RailcarLocationTransaction> latestTransactionFromTrain = new Search<RailcarLocationTransaction>(new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                        new LongSearchCondition<RailcarLocationTransaction>()
                        {
                            Field = nameof(RailcarLocationTransaction.TrainIDNew),
                            SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                            Value = oldTrainID
                        },
                        new LongSearchCondition<RailcarLocationTransaction>()
                        {
                            Field = nameof(RailcarLocationTransaction.RailcarID),
                            SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                            Value = railLocation.RailcarID
                        }))
                    {
                        SearchOrders = new List<SearchOrder>()
                        {
                            new SearchOrder()
                            {
                                OrderField = nameof(RailcarLocationTransaction.TransactionTime),
                                OrderDirection = SearchOrder.OrderDirections.Descending
                            }
                        }
                    };

                    RailcarLocationTransaction oldTransaction = latestTransactionFromTrain.GetEditable(transaction, FieldPathUtility.CreateFieldPathsAsList<RailcarLocationTransaction>(rlt => new List<object>() { rlt.TrainNew.Status }));
                    if (oldTransaction != null && oldTransaction.TrainNew.Status == Train.Statuses.EnRoute)
                    {
                        oldTransaction.IsPartialTrainTrip = true;
                        if (!oldTransaction.Save(transaction))
                        {
                            return oldTransaction.Errors;
                        }
                    }
                    
                }
            }

            return new Errors();
        }

        public class ModifyParameter
        {
            public Dictionary<long?, List<RailLocation>> ModifiedTracksByID { get; set; }
            public Dictionary<long?, List<RailLocation>> ModifiedTrainsByID { get; set; }
            public DateTime? TimeMoved { get; set; }
        }
    }
}