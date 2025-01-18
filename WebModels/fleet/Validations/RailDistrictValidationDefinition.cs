using System;
using System.Collections.Generic;
using ClussPro.ObjectBasedFramework.Validation;
using WebModels.Common;

namespace WebModels.fleet.Validations
{
    public class RailDistrictValidationDefinition : IValidationDefinition
    {
        public string Schema => "fleet";

        public string Object => nameof(RailDistrict);

        public IEnumerable<ValidationRule> ValidationRules
        {
            get
            {
                yield return new ValidationRule()
                {
                    ID = new Guid("9406A96C-66E2-4571-BD91-232750D37152"),
                    Message = "Company Operator or Government Operator are required, but not both",
                    Field = "CompanyIDOperator,GovernmentIDOperator",
                    Condition = new XOrPresenceCondition("CompanyIDOperator", "GovernmentIDOperator")
                };
            }
        }
    }
}
