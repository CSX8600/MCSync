using System;
using System.Collections.Generic;
using ClussPro.ObjectBasedFramework.Validation;
using WebModels.Common;

namespace WebModels.fleet.Validations
{
    public class RailcarValidationDefinition : IValidationDefinition
    {
        public string Schema => "fleet";

        public string Object => nameof(Railcar);

        public IEnumerable<ValidationRule> ValidationRules
        {
            get
            {
                yield return new ValidationRule()
                {
                    ID = new Guid("ACBAB81A-0D8F-4C1D-9BB9-3567AAE966A8"),
                    Message = "Government Owner or Company Owner are required, but not both",
                    Field = "CompanyIDOwner,GovernmentIDOwner",
                    Condition = new XOrPresenceCondition("CompanyIDOwner", "GovernmentIDOwner")
                };

                yield return new ValidationRule()
                {
                    ID = new Guid("DAB31BB5-D954-4359-8CCB-018209682653"),
                    Message = "Government Possessor or Company Possessor are required, but not both",
                    Field = "CompanyIDPossessor,GovernmentIDPossessor",
                    Condition = new XOrPresenceCondition("CompanyIDPossessor", "GovernmentIDPossessor")
                };

                yield return new ValidationRule()
                {
                    ID = new Guid("1478D106-7D79-48CC-814B-34AE96835031"),
                    Message = "Reporting Mark must be unique amongst Locomotives and Railcars",
                    Field = "ReportingMark,ReportingNumber",
                    Condition = new ReportingMarkUniqueCondition()
                };
            }
        }
    }
}
