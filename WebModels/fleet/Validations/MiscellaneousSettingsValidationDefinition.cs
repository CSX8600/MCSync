using System;
using System.Collections.Generic;
using ClussPro.ObjectBasedFramework.Validation;
using WebModels.Common;

namespace WebModels.fleet.Validations
{
    public class MiscellaneousSettingsValidationDefinition : IValidationDefinition
    {
        public string Schema => "fleet";

        public string Object => nameof(MiscellaneousSettings);

        public IEnumerable<ValidationRule> ValidationRules
        {
            get
            {
                yield return new ValidationRule()
                {
                    ID = new Guid("966C647A-68A7-411F-9F19-4D777BF3AD67"),
                    Message = "Government or Company are required, but not both",
                    Field = "GovernmentID,CompanyID",
                    Condition = new XOrPresenceCondition("GovernmentID", "CompanyID")
                };
            }
        }
    }
}
