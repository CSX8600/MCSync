using ClussPro.ObjectBasedFramework.Validation;
using System;
using System.Collections.Generic;
using WebModels.Common;

namespace WebModels.purchasing.Validations
{
    public class QuotationRequestValidations : IValidationDefinition
    {
        public string Schema => "purchasing";

        public string Object => nameof(QuotationRequest);

        public IEnumerable<ValidationRule> ValidationRules
        {
            get
            {
                yield return new ValidationRule()
                {
                    ID = new Guid("FF9768BD-F786-4480-B3F1-A56CCFFE75B1"),
                    Field = nameof(QuotationRequest.CompanyIDFrom) + "," + nameof(QuotationRequest.GovernmentIDFrom),
                    Message = "Either Company From or Government From must be specified, but not both.",
                    Condition = new XOrPresenceCondition(nameof(QuotationRequest.CompanyIDFrom), nameof(QuotationRequest.GovernmentIDFrom))
                };

                yield return new ValidationRule()
                {
                    ID = new Guid("01248413-FC02-4CE4-A512-FDE766E0DF22"),
                    Field = nameof(QuotationRequest.CompanyIDTo) + "," + nameof(QuotationRequest.GovernmentIDTo),
                    Message = "Either Company To or Government To must be specified, but not both.",
                    Condition = new XOrPresenceCondition(nameof(QuotationRequest.CompanyIDTo), nameof(QuotationRequest.GovernmentIDTo))
                };
            }
        }
    }
}
