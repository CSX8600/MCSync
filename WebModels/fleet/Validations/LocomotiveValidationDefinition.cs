using System;
using System.Collections.Generic;
using ClussPro.ObjectBasedFramework.Validation;
using WebModels.Common;

namespace WebModels.fleet.Validations
{
    public class LocomotiveValidationDefinition : IValidationDefinition
    {
        public string Schema => "fleet";

        public string Object => nameof(Locomotive);

        public IEnumerable<ValidationRule> ValidationRules
        {
            get
            {
                yield return new ValidationRule()
                {
                    ID = new Guid("95ECAC13-F7B8-4405-BECA-BCA21E01672A"),
                    Message = "Government Owner or Company Owner are required, but not both",
                    Field = "CompanyIDOwner,GovernmentIDOwner",
                    Condition = new XOrPresenceCondition("CompanyIDOwner", "GovernmentIDOwner")
                };

                yield return new ValidationRule()
                {
                    ID = new Guid("6148CC81-18C4-490C-AF3F-39682D699D42"),
                    Message = "Government Possessor or Company Possessor are required, but not both",
                    Field = "CompanyIDPossessor,GovernmentIDPossessor",
                    Condition = new XOrPresenceCondition("CompanyIDPossessor", "GovernmentIDPossessor")
                };

                yield return new ValidationRule()
                {
                    ID = new Guid("8B664D12-7A7B-4267-B5FA-B4FF47B42AAB"),
                    Message = "Reporting Mark must be unique amongst Locomotives and Railcars",
                    Field = "ReportingMark,ReportingNumber",
                    Condition = new ReportingMarkUniqueCondition()
                };
            }
        }
    }
}
