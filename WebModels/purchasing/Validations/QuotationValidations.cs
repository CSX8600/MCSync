using ClussPro.ObjectBasedFramework.Validation;
using System;
using System.Collections.Generic;
using WebModels.Common;

namespace WebModels.purchasing.Validations
{
    public class QuotationValidations : IValidationDefinition
    {
        public string Schema => "purchasing";

        public string Object => nameof(Quotation);

        public IEnumerable<ValidationRule> ValidationRules
        {
            get
            {
                yield return new ValidationRule()
                {
                    ID = new Guid("90150D3E-228C-45A1-8394-9A03D36E1953"),
                    Field = nameof(Quotation.CompanyIDFrom) + "," + nameof(Quotation.GovernmentIDFrom),
                    Message = "Either Company From or Government From must be specified, but not both.",
                    Condition = new XOrPresenceCondition(nameof(Quotation.CompanyIDFrom), nameof(Quotation.GovernmentIDFrom))
                };

                yield return new ValidationRule()
                {
                    ID = new Guid("F3743776-F266-4B5B-9397-2CADABB46919"),
                    Field = nameof(Quotation.CompanyIDTo) + "," + nameof(Quotation.GovernmentIDTo),
                    Message = "Either Company To or Government To must be specified, but not both.",
                    Condition = new XOrPresenceCondition(nameof(Quotation.CompanyIDTo), nameof(Quotation.GovernmentIDTo))
                };
            }
        }
    }
}
