using System.Collections.Generic;
using System.Linq;
using API.Common;
using API.Common.Attributes;
using API_Government.Attributes;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Utility;
using WebModels.gov;
using WebModels.invoicing;

namespace API_Government.Controllers
{
    [MesabrookAuthorization]
    [ProgramAccess("gov")]
    [GovernmentAccess(RequiredPermissions = new[] { nameof(Official.ManageInvoices) })]
    public class AutomaticInvoicePaymentConfigurationController : DataObjectController<AutomaticInvoicePaymentConfiguration>
    {
        protected long GovernmentID => long.Parse(Request.Headers.GetValues("GovernmentID").First());

        public override IEnumerable<string> DefaultRetrievedFields => FieldPathUtility.CreateFieldPathsAsList<AutomaticInvoicePaymentConfiguration>(c => new object[]
        {
            c.AutomaticInvoicePaymentConfigurationID,
            c.GovernmentIDConfiguredFor,
            c.GovernmentConfiguredFor.GovernmentID,
            c.GovernmentConfiguredFor.Name,
            c.LocationIDPayee,
            c.LocationPayee.LocationID,
            c.LocationPayee.Name,
            c.LocationPayee.CompanyID,
            c.LocationPayee.Company.Name,
            c.GovernmentIDPayee,
            c.GovernmentPayee.GovernmentID,
            c.GovernmentPayee.Name,
            c.PaidAmount,
            c.MaxAmount,
            c.Schedule,
            c.AccountID,
            c.Account.Description,
            c.Account.AccountNumber
        });

        public override ISearchCondition GetBaseSearchCondition()
        {
            return new SearchConditionGroup(SearchConditionGroup.SearchConditionGroupTypes.And,
                new LongSearchCondition<AutomaticInvoicePaymentConfiguration>()
                {
                    Field = nameof(AutomaticInvoicePaymentConfiguration.GovernmentIDConfiguredFor),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = GovernmentID
                },
                new LongSearchCondition<AutomaticInvoicePaymentConfiguration>()
                {
                    Field = nameof(AutomaticInvoicePaymentConfiguration.LocationIDConfiguredFor),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Null
                });
        }

        public override bool AllowGetAll => true;
    }
}
