using System;
using System.Collections.Generic;
using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.Validation;
using ClussPro.ObjectBasedFramework.Validation.Conditions;
using WebModels.Common;

namespace WebModels.fleet.Validations
{
    public class LeaseBidValidationDefinition : IValidationDefinition
    {
        public string Schema => "fleet";

        public string Object => nameof(LeaseBid);

        public IEnumerable<ValidationRule> ValidationRules
        {
            get
            {
                yield return new ValidationRule()
                {
                    ID = new Guid("F7B7BA79-B37D-43E5-9B7C-0B06BD3E6AB7"),
                    Message = "Railcar or Locomotive are required, but not both",
                    Field = "RailcarID,LocomotiveID",
                    Condition = new XOrPresenceCondition("RailcarID", "LocomotiveID")
                };

                yield return new ValidationRule()
                {
                    ID = new Guid("4C531859-15C6-4E87-A8E2-2BEFE99F26B8"),
                    Message = "Recurring Amount is a required field",
                    Field = nameof(LeaseBid.RecurringAmount),
                    Condition = new ConditionGroup(ConditionGroup.ConditionGroupTypes.Or,
                                    new EqualCondition(nameof(LeaseBid.RecurringAmountType), LeaseBid.RecurringAmountTypes.None),
                                    new PresenceCondition(nameof(LeaseBid.RecurringAmount)))
                };

                yield return new ValidationRule()
                {
                    ID = new Guid("C494A03B-2A0E-488E-A8FE-1ACDBE3AABC4"),
                    Message = "Invoice Destination is required when bidding as a Company",
                    Field = nameof(LeaseBid.LocationIDInvoiceDestination),
                    Condition = new LocationInvoiceDestinationRequiredCondition()
                };
            }
        }

        public class LocationInvoiceDestinationRequiredCondition : Condition
        {
            public override bool Evaluate(DataObject dataObject, ITransaction transaction)
            {
                if (!(dataObject is LeaseBid bid))
                {
                    throw new ArgumentException("dataObject must of type LeaseBid", "dataObject");
                }

                long? companyID;
                if (bid.RailcarID != null)
                {
                    Railcar railcar = DataObject.GetReadOnlyByPrimaryKey<Railcar>(bid.RailcarID, transaction, new[] { nameof(Railcar.CompanyIDOwner) });
                    companyID = railcar?.CompanyIDOwner;
                }
                else if (bid.LocomotiveID != null)
                {
                    Locomotive locomotive = DataObject.GetReadOnlyByPrimaryKey<Locomotive>(bid.LocomotiveID, transaction, new[] { nameof(Locomotive.CompanyIDOwner) });
                    companyID = locomotive?.CompanyIDOwner;
                }
                else
                {
                    return true;
                }

                return companyID == null || bid.LocationIDInvoiceDestination != null;
            }
        }
    }
}
