using API.Common;
using API.Common.Attributes;
using API_Company.Attributes;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModels.company;
using WebModels.invoicing;

namespace API_Company.Controllers
{
    [MesabrookAuthorization]
    [ProgramAccess("company")]
    [LocationAccess(RequiredPermissions = new[] { nameof(LocationEmployee.ManageInvoices) })]
    public class AutomaticInvoicePaymentConfigurationController : DataObjectController<AutomaticInvoicePaymentConfiguration>
    {
        protected long LocationID => long.Parse(Request.Headers.GetValues("LocationID").First());

        public override IEnumerable<string> DefaultRetrievedFields => FieldPathUtility.CreateFieldPathsAsList<AutomaticInvoicePaymentConfiguration>(c => new object[]
        {
            c.AutomaticInvoicePaymentConfigurationID,
            c.LocationIDConfiguredFor,
            c.LocationConfiguredFor.LocationID,
            c.LocationConfiguredFor.Name,
            c.LocationConfiguredFor.CompanyID,
            c.LocationConfiguredFor.Company.Name,
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
                    Field = nameof(AutomaticInvoicePaymentConfiguration.LocationIDConfiguredFor),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = LocationID
                },
                new LongSearchCondition<AutomaticInvoicePaymentConfiguration>()
                {
                    Field = nameof(AutomaticInvoicePaymentConfiguration.GovernmentIDConfiguredFor),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Null
                });
        }

        public override bool AllowGetAll => true;
    }
}
