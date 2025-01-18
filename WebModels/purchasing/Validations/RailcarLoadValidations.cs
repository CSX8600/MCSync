using ClussPro.ObjectBasedFramework.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModels.fleet;

namespace WebModels.purchasing.Validations
{
    public class RailcarLoadValidations : IValidationDefinition
    {
        public string Schema => "fleet";

        public string Object => nameof(RailcarLoad);

        public IEnumerable<ValidationRule> ValidationRules
        {
            get
            {
                yield return new ValidationRule()
                {
                    ID = new Guid("0F26A38D-C0A9-4C49-8EFE-53388F11F384"),
                    Field = nameof(RailcarLoad.RailcarLoadID),
                    Message = "Railcar Loads cannot be cleared until related Bills of Lading have been accepted.",
                    ApplyOnInsert = false,
                    ApplyOnUpdate = false,
                    ApplyOnDelete = true,
                    Condition = new RailcarLoadClearedAfterAcceptedBillsOfLadingCondition()
                };
            }
        }
    }
}
