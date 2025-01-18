using ClussPro.ObjectBasedFramework.Validation;
using ClussPro.ObjectBasedFramework.Validation.Conditions;
using System;
using System.Collections.Generic;

namespace WebModels.purchasing.Validations
{
#warning Government Disabled
    public class RouteValidations : IValidationDefinition
    {
        public string Schema => "purchasing";

        public string Object => nameof(FulfillmentPlanRoute);

        public IEnumerable<ValidationRule> ValidationRules
        {
            get
            {
                yield return new ValidationRule()
                {
                    ID = new Guid("75293AE4-4DA8-4180-A901-C53DACD2C628"),
                    Field = nameof(FulfillmentPlanRoute.GovernmentIDFrom),
                    Message = "Routes involving Governments are not allowed at this time",
                    Condition = new EqualCondition(nameof(FulfillmentPlanRoute.GovernmentIDFrom), null)
                };

                yield return new ValidationRule()
                {
                    ID = new Guid("EF1A031C-7F23-4FFB-8A23-61D0D3982508"),
                    Field = nameof(FulfillmentPlanRoute.GovernmentIDTo),
                    Message = "Routes involving Governments are not allowed at this time",
                    Condition = new EqualCondition(nameof(FulfillmentPlanRoute.GovernmentIDTo), null)
                };
            }
        }
    }
}
