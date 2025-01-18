using ClussPro.ObjectBasedFramework.Validation;
using ClussPro.ObjectBasedFramework.Validation.Conditions;
using System;
using System.Collections.Generic;

namespace WebModels.purchasing.Validations
{
    public class FulfillmentValidations : IValidationDefinition
    {
        public string Schema => "purchasing";

        public string Object => nameof(Fulfillment);

        public IEnumerable<ValidationRule> ValidationRules
        {
            get
            {
                yield return new ValidationRule()
                {
                    ID = new Guid("4710ABB2-7AA7-40A9-8773-0E9B6806291D"),
                    Field = nameof(Fulfillment.RailcarID),
                    Message = FulfillmentOnEligibleRailcarCondition.MESSAGE,
                    ApplyOnUpdate = false,
                    Condition = new FulfillmentOnEligibleRailcarCondition()
                };

                yield return new ValidationRule()
                {
                    ID = new Guid("3F3A7229-C7C2-45F8-A8F2-96F6086D548C"),
                    Field = nameof(Fulfillment.IsComplete),
                    ApplyOnInsert = false,
                    ApplyOnUpdate = false,
                    ApplyOnDelete = true,
                    Message = "Fulfillments marked as complete cannot be deleted.",
                    Condition = new NotCondition(new EqualCondition(nameof(Fulfillment.IsComplete), true))
                };
            }
        }
    }
}
