using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Validation.Conditions;
using System;

namespace WebModels.invoicing.Validations
{
    internal class AutomaticInvoicePaymentConfigurationUniquenessCondition : Condition
    {
        public override bool Evaluate(DataObject dataObject, ITransaction transaction)
        {
            if (!(dataObject is AutomaticInvoicePaymentConfiguration automaticInvoicePaymentConfiguration))
            {
                throw new InvalidCastException("Unable to cast dataObject to AutomaticInvoicePaymentConfiguration.");
            }

            if ((automaticInvoicePaymentConfiguration.GovernmentIDConfiguredFor == null && automaticInvoicePaymentConfiguration.LocationIDConfiguredFor == null) ||
                (automaticInvoicePaymentConfiguration.GovernmentIDPayee != null && automaticInvoicePaymentConfiguration.LocationIDPayee != null))
            {
                return true;
            }

            SearchConditionGroup searchConditionGroup = new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And);

            if (automaticInvoicePaymentConfiguration.GovernmentIDConfiguredFor != null)
            {
                searchConditionGroup.SearchConditions.Add(new LongSearchCondition<AutomaticInvoicePaymentConfiguration>()
                {
                    Field = nameof(AutomaticInvoicePaymentConfiguration.GovernmentIDConfiguredFor),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = automaticInvoicePaymentConfiguration.GovernmentIDConfiguredFor
                });
            }

            if (automaticInvoicePaymentConfiguration.LocationIDConfiguredFor != null)
            {
                searchConditionGroup.SearchConditions.Add(new LongSearchCondition<AutomaticInvoicePaymentConfiguration>()
                {
                    Field = nameof(AutomaticInvoicePaymentConfiguration.LocationIDConfiguredFor),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = automaticInvoicePaymentConfiguration.LocationIDConfiguredFor
                });
            }

            if (automaticInvoicePaymentConfiguration.GovernmentIDPayee != null)
            {
                searchConditionGroup.SearchConditions.Add(new LongSearchCondition<AutomaticInvoicePaymentConfiguration>()
                {
                    Field = nameof(AutomaticInvoicePaymentConfiguration.GovernmentIDPayee),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = automaticInvoicePaymentConfiguration.GovernmentIDPayee
                });
            }

            if (automaticInvoicePaymentConfiguration.LocationIDPayee != null)
            {
                searchConditionGroup.SearchConditions.Add(new LongSearchCondition<AutomaticInvoicePaymentConfiguration>()
                {
                    Field = nameof(AutomaticInvoicePaymentConfiguration.LocationIDPayee),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = automaticInvoicePaymentConfiguration.LocationIDPayee
                });
            }

            if (automaticInvoicePaymentConfiguration.AutomaticInvoicePaymentConfigurationID != null)
            {
                searchConditionGroup.SearchConditions.Add(new LongSearchCondition<AutomaticInvoicePaymentConfiguration>()
                {
                    Field = nameof(AutomaticInvoicePaymentConfiguration.AutomaticInvoicePaymentConfigurationID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.NotEquals,
                    Value = automaticInvoicePaymentConfiguration.AutomaticInvoicePaymentConfigurationID
                });
            }

            Search<AutomaticInvoicePaymentConfiguration> search = new Search<AutomaticInvoicePaymentConfiguration>(searchConditionGroup);
            return !search.ExecuteExists(transaction);
        }
    }
}
