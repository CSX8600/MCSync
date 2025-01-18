using System;
using System.Collections.Generic;
using ClussPro.ObjectBasedFramework.Validation;
using WebModels.Common;

namespace WebModels.fleet.Validations
{
    public class RailLocationValidationDefinition : IValidationDefinition
    {
        public string Schema => "fleet";

        public string Object => nameof(RailLocation);

        public IEnumerable<ValidationRule> ValidationRules
        {
            get
            {
                yield return new ValidationRule()
                {
                    ID = new Guid("165531E4-6422-4C4C-938D-1BA22A972097"),
                    Message = "Locomotive or Railcar are required, but not both",
                    Field = "LocomotiveID,RailcarID",
                    Condition = new XOrPresenceCondition("LocomotiveID", "RailcarID")
                };

                yield return new ValidationRule()
                {
                    ID = RailLocation.ValidationIDs.TrackOrTrainRequired,
                    Message = "Track or Train are required, but not both",
                    Field = "TrackID,TrainID",
                    Condition = new XOrPresenceCondition("TrackID", "TrainID")
                };
            }
        }
    }
}
