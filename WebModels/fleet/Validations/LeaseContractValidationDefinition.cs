using System;
using System.Collections.Generic;
using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.Validation;
using ClussPro.ObjectBasedFramework.Validation.Conditions;
using WebModels.Common;

namespace WebModels.fleet.Validations
{
    public class LeaseContractValidationDefinition : IValidationDefinition
    {
        public string Schema => "fleet";

        public string Object => nameof(LeaseContract);

        public IEnumerable<ValidationRule> ValidationRules
        {
            get
            {
                yield return new ValidationRule()
                {
                    ID = new Guid("8ABCAF6A-5E7F-4790-AC60-B72EFF4E5B98"),
                    Message = "Railcar or Locomotive are required, but not both",
                    Field = "RailcarID,LocomotiveID",
                    Condition = new XOrPresenceCondition("RailcarID", "LocomotiveID")
                };

                yield return new ValidationRule()
                {
                    ID = new Guid("98FDE360-F11A-4BF8-8513-6E6A15B44EB1"),
                    Message = "Lessee Company or Lessee Government are required, but not both",
                    Field = "GovernmentIDLessee,CompanyIDLessee",
                    Condition = new XOrPresenceCondition("GovernmentIDLessee", "CompanyIDLessee")
                };

                yield return new ValidationRule()
                {
                    ID = new Guid("5F3C9E8F-133A-4552-9A59-037FFDCCA744"),
                    Message = "Recurring Amount is a required field",
                    Field = nameof(LeaseContract.RecurringAmount),
                    Condition = new ConditionGroup(ConditionGroup.ConditionGroupTypes.Or,
                                    new EqualCondition(nameof(LeaseContract.RecurringAmountType), LeaseBid.RecurringAmountTypes.None),
                                    new PresenceCondition(nameof(LeaseContract.RecurringAmount)))
                };

                yield return new ValidationRule()
                {
                    ID = new Guid("1A4586C0-E1FA-4E06-AFB2-45CC47737EA8"),
                    Message = "Recurring Amount Source is required when the Lessee is a Company and the Recurring Amount Type is not None",
                    Field = nameof(LeaseContract.LocationIDRecurringAmountSource),
                    Condition = new ConditionGroup(ConditionGroup.ConditionGroupTypes.Or,
                                    new NotCondition(new PresenceCondition(nameof(LeaseContract.CompanyIDLessee))),
                                    new EqualCondition(nameof(LeaseContract.RecurringAmountType), LeaseBid.RecurringAmountTypes.None),
                                    new PresenceCondition(nameof(LeaseContract.LocationIDRecurringAmountSource)))
                };

                yield return new ValidationRule()
                {
                    ID = new Guid("D7FB4308-A793-427B-BCD5-C503A7ADFAEF"),
                    Message = "Recurring Amount Destination is required when the Lessor is a Company and the Recurring Amount Type is not None",
                    Field = nameof(LeaseContract.LocationIDRecurringAmountDestination),
                    Condition = new ConditionGroup(ConditionGroup.ConditionGroupTypes.Or,
                                    new EqualCondition(nameof(LeaseContract.RecurringAmountType), LeaseBid.RecurringAmountTypes.None),
                                    new LocationRecurringDestinationRequiredCondition())
                };

                yield return new ValidationRule()
                {
                    ID = new Guid("CE913FC9-56EC-48FE-91AF-B0E8540FBE87"),
                    Message = "Lease End Date must be greater than Lease Start Date",
                    Field = $"{nameof(LeaseContract.LeaseTimeStart)},{nameof(LeaseContract.LeaseTimeEnd)}",
                    Condition = new ConditionGroup(ConditionGroup.ConditionGroupTypes.Or,
                                    new NotCondition(new PresenceCondition("LeaseTimeEnd")),
                                    new FieldToFieldInequalityCondition("LeaseTimeEnd", InequalityCondition.Operations.GreaterThan, "LeaseTimeStart"))
                };
            }
        }

        public class LocationRecurringDestinationRequiredCondition : Condition
        {
            public override bool Evaluate(DataObject dataObject, ITransaction transaction)
            {
                if (!(dataObject is LeaseContract contract))
                {
                    throw new ArgumentException("dataObject must of type LeaseContract", "dataObject");
                }

                long? companyID;
                if (contract.RailcarID != null)
                {
                    Railcar railcar = DataObject.GetReadOnlyByPrimaryKey<Railcar>(contract.RailcarID, transaction, new[] { nameof(Railcar.CompanyIDOwner) });
                    companyID = railcar?.CompanyIDOwner;
                }
                else if (contract.LocomotiveID != null)
                {
                    Locomotive locomotive = DataObject.GetReadOnlyByPrimaryKey<Locomotive>(contract.LocomotiveID, transaction, new[] { nameof(Locomotive.CompanyIDOwner) });
                    companyID = locomotive?.CompanyIDOwner;
                }
                else
                {
                    return true;
                }

                return companyID != null && contract.LocationIDRecurringAmountDestination != null;
            }
        }
    }
}
