using System;
using System.Collections.Generic;
using ClussPro.ObjectBasedFramework.Validation;
using WebModels.Common;

namespace WebModels.fleet.Validations
{
    public class LiveLoadSessionValidationDefinition : IValidationDefinition
    {
        public string Schema => "fleet";

        public string Object => nameof(LiveLoadSession);

        public IEnumerable<ValidationRule> ValidationRules
        {
            get
            {
                yield return new ValidationRule()
                {
                    ID = new Guid("C335F0E5-183C-4079-82BE-E6A38232BA5C"),
                    Message = "Company or Government are required, but not both",
                    Field = "CompanyID,GovernmentID",
                    Condition = new XOrPresenceCondition("CompanyID", "GovernmentID")
                };
            }
        }
    }
}
