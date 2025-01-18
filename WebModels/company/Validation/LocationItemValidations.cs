using ClussPro.ObjectBasedFramework.Validation;
using ClussPro.ObjectBasedFramework.Validation.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModels.Common;
using WebModels.mesasys;

namespace WebModels.company.Validation
{
    public class LocationItemValidations : IValidationDefinition
    {
        public string Schema => "company";

        public string Object => nameof(LocationItem);

        public IEnumerable<ValidationRule> ValidationRules
        {
            get
            {
                yield return new ValidationRule()
                {
                    ID = new Guid("F7F9779A-D875-46C4-A75E-A7DF4D946072"),
                    Message = "Location or Government must be specified, but not both",
                    Condition = new XOrPresenceCondition(nameof(LocationItem.LocationID), nameof(LocationItem.GovernmentID))
                };

                yield return new ValidationRule()
                {
                    ID = new Guid("E8746C21-B4F6-47D6-A11A-8B20D6DAF656"),
                    Message = "Fluid items must be unique for this store",
                    Condition = new ConditionGroup(ConditionGroup.ConditionGroupTypes.Or,
                        new EqualCondition(nameof(LocationItem.Item) + "." + nameof(Item.IsFluid), false),
                        new FluidItemIsUniqueForStoreCondition())
                };
            }
        }
    }
}
