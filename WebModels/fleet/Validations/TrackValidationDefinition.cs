using System;
using System.Collections.Generic;
using ClussPro.ObjectBasedFramework.Validation;
using WebModels.Common;

namespace WebModels.fleet.Validations
{
    public class TrackValidationDefinition : IValidationDefinition
    {
        public string Schema => "fleet";

        public string Object => nameof(Track);

        public IEnumerable<ValidationRule> ValidationRules
        {
            get
            {
                yield return new ValidationRule()
                {
                    ID = new Guid("DC55FAB0-E6E8-4EF4-B857-02D03B05A4BB"),
                    Message = "Company Owner or Government Owner are required, but not both",
                    Field = "CompanyIDOwner,GovernmentIDOwner",
                    Condition = new XOrPresenceCondition("CompanyIDOwner", "GovernmentIDOwner")
                };
            }
        }
    }
}
