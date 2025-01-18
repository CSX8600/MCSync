using System;
using System.Collections.Generic;
using ClussPro.ObjectBasedFramework.Validation;
using ClussPro.ObjectBasedFramework.Validation.Conditions;
using WebModels.Common;

namespace WebModels.fleet.Validations
{
    public class LeaseRequestValidationDefinition : IValidationDefinition
    {
        public string Schema => "fleet";

        public string Object => nameof(LeaseRequest);

        public IEnumerable<ValidationRule> ValidationRules
        {
            get
            {
                yield return new ValidationRule()
                {
                    ID = new Guid("50FF8153-A445-4F92-80F2-42A784218BD9"),
                    Message = "Company Requester or Government Requester are required, but not both",
                    Field = "CompanyIDRequester,GovernmentIDRequester",
                    Condition = new XOrPresenceCondition("CompanyIDRequester", "GovernmentIDRequester")
                };

                yield return new ValidationRule()
                {
                    ID = new Guid("2F78FFB4-7E48-48AD-967B-5B7F65A81E37"),
                    Message = "Charge To Location is a required field when Company Requester is selected",
                    Field = nameof(LeaseRequest.LocationIDChargeTo),
                    Condition = new ConditionGroup(ConditionGroup.ConditionGroupTypes.Or,
                                    new NotCondition(new PresenceCondition(nameof(LeaseRequest.CompanyIDRequester))),
                                    new PresenceCondition(nameof(LeaseRequest.LocationIDChargeTo)))
                };

            }
        }
    }
}
