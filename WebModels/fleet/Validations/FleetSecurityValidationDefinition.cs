using System;
using System.Collections.Generic;
using ClussPro.ObjectBasedFramework.Validation;
using WebModels.Common;

namespace WebModels.fleet.Validations
{
    public class FleetSecurityValidationDefinition : IValidationDefinition
    {
        public string Schema => "fleet";

        public string Object => nameof(FleetSecurity);

        public IEnumerable<ValidationRule> ValidationRules
        {
            get
            {
                yield return new ValidationRule()
                {
                    ID = new Guid("C335F0E5-183C-4079-82BE-E6A38232BA5C"),
                    Message = "Employee or Official are required, but not both",
                    Field = "EmployeeID,OfficialID",
                    Condition = new XOrPresenceCondition("EmployeeID", "OfficialID")
                };
            }
        }
    }
}
