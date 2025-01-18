using ClussPro.ObjectBasedFramework.Validation;
using ClussPro.ObjectBasedFramework.Validation.Conditions;
using System;
using System.Collections.Generic;

namespace WebModels.purchasing.Validations
{
    public class PurchaseOrderValidations : IValidationDefinition
    {
        public string Schema => "purchasing";

        public string Object => nameof(PurchaseOrder);

        public IEnumerable<ValidationRule> ValidationRules
        {
            get
            {
                yield return new ValidationRule()
                {
                    ID = new Guid("C921A8E1-E594-42D1-AB53-114C736E58B1"),
                    Field = nameof(PurchaseOrder.Status),
                    Message = PurchaseOrderHasDetailsOnSubmitCondition.VALIDATION_MESSAGE,
                    Condition = new PurchaseOrderHasDetailsOnSubmitCondition()
                };

                yield return new ValidationRule()
                {
                    ID = PurchaseOrder.SaveFlags.V_StatusChange,
                    Field = nameof(PurchaseOrder.Status),
                    ApplyOnInsert = false,
                    Message = "Status cannot be changed",
                    Condition = new NotCondition(new IsFieldDirtyCondition(nameof(PurchaseOrder.Status)))
                };

                yield return new ValidationRule()
                {
                    ID = new Guid("AEFE2FA7-582D-41D5-93F6-C870B34637A6"),
                    Field = nameof(PurchaseOrder.Status),
                    ApplyOnUpdate = false,
                    Message = "Status must be saved as Draft",
                    Condition = new EqualCondition(nameof(PurchaseOrder.Status), PurchaseOrder.Statuses.Draft)
                };

                yield return new ValidationRule()
                {
                    ID = new Guid("0C6039F5-01DB-4323-928E-F269A1D74976"),
                    Field = nameof(PurchaseOrder.PurchaseOrderID),
                    Message = "All Purchase Order Lines must have the same Route as defined by all of their Fulfillment Plans",
                    Condition = new PurchaseOrderRoutesValidOnSubmitCondition()
                };

#warning Government Disabled
                yield return new ValidationRule()
                {
                    ID = new Guid("2147F531-48C4-4041-8FCA-992FFDEE3008"),
                    Field = nameof(PurchaseOrder.GovernmentIDOrigin),
                    Message = "Purchase Orders from Governments are not allowed at this time",
                    Condition = new EqualCondition(nameof(PurchaseOrder.GovernmentIDOrigin), null)
                };

                yield return new ValidationRule()
                {
                    ID = new Guid("1B1379B5-A0E3-4D43-8ECD-727E7416B89C"),
                    Field = nameof(PurchaseOrder.GovernmentIDDestination),
                    Message = "Purchase Orders to Governments are not allowed at this time",
                    Condition = new EqualCondition(nameof(PurchaseOrder.GovernmentIDDestination), null)
                };
            }
        }
    }
}
