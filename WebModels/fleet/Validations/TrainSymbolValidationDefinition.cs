using System;
using System.Collections.Generic;
using ClussPro.ObjectBasedFramework.Validation;
using WebModels.Common;

namespace WebModels.fleet.Validations
{
    public class TrainSymbolValidationDefinition : IValidationDefinition
    {
        public string Schema => "fleet";

        public string Object => nameof(TrainSymbol);

        public IEnumerable<ValidationRule> ValidationRules
        {
            get
            {
                yield return new ValidationRule()
                {
                    ID = new Guid("BFDC0526-12E3-44A4-8C3B-C15A3A739A5A"),
                    Message = "Company Operator or Government Operator are required, but not both",
                    Field = "CompanyIDOperator,GovernmentIDOperator",
                    Condition = new XOrPresenceCondition("CompanyIDOperator", "GovernmentIDOperator")
                };
            }
        }
    }
}
