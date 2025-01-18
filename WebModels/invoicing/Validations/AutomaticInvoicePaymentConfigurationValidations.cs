using ClussPro.ObjectBasedFramework.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModels.Common;

namespace WebModels.invoicing.Validations
{
    internal class AutomaticInvoicePaymentConfigurationValidations : IValidationDefinition
    {
        public string Schema => "invoicing";

        public string Object => nameof(AutomaticInvoicePaymentConfiguration);

        public IEnumerable<ValidationRule> ValidationRules
        {
            get
            {
                yield return new ValidationRule()
                {
                    ID = new Guid("F6EB5B75-D015-43AF-A454-42D92A23FA39"),
                    Field = nameof(AutomaticInvoicePaymentConfiguration.LocationIDConfiguredFor) + "," + nameof(AutomaticInvoicePaymentConfiguration.GovernmentIDConfiguredFor),
                    Message = "Configured for entity is required",
                    Condition = new XOrPresenceCondition(nameof(AutomaticInvoicePaymentConfiguration.LocationIDConfiguredFor), nameof(AutomaticInvoicePaymentConfiguration.GovernmentIDConfiguredFor))
                };

                yield return new ValidationRule()
                {
                    ID = new Guid("D5EE1755-1426-44FD-854E-9CB4661ECC3F"),
                    Field = nameof(AutomaticInvoicePaymentConfiguration.LocationIDPayee) + "," + nameof(AutomaticInvoicePaymentConfiguration.GovernmentIDPayee),
                    Message = "Payee is required",
                    Condition = new XOrPresenceCondition(nameof(AutomaticInvoicePaymentConfiguration.LocationIDPayee), nameof(AutomaticInvoicePaymentConfiguration.GovernmentIDPayee))
                };

                yield return new ValidationRule()
                {
                    ID = new Guid("6553F704-CBE1-4711-B40F-FC48863BA903"),
                    Field = $"{nameof(AutomaticInvoicePaymentConfiguration.LocationIDConfiguredFor)},{nameof(AutomaticInvoicePaymentConfiguration.GovernmentIDConfiguredFor)}," +
                            $"{nameof(AutomaticInvoicePaymentConfiguration.GovernmentIDPayee)},{nameof(AutomaticInvoicePaymentConfiguration.LocationIDPayee)}",
                    Message = "Payee must be unique",
                    Condition = new AutomaticInvoicePaymentConfigurationUniquenessCondition()
                };
            }
        }
    }
}
