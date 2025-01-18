using ClussPro.ObjectBasedFramework.Validation;
using System;
using System.Collections.Generic;

namespace WebModels.purchasing.Validations
{
    public class BillOfLadingValidations : IValidationDefinition
    {
        public string Schema => "purchasing";

        public string Object => nameof(BillOfLading);

        public IEnumerable<ValidationRule> ValidationRules
        {
            get
            {
                yield return new ValidationRule()
                {
                    ID = new Guid("E5A56CC0-A645-4695-B199-0DE2CA7395C2"),
                    Field = nameof(BillOfLading.PurchaseOrderID),
                    Message = "Only one active Bill Of Lading can exist per Railcar",
                    Condition = new BillOfLadingUniqueByRailcarCondition()
                };
            }
        }
    }
}
