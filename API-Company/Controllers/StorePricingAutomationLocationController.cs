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

namespace API_Company.Controllers
{
    [MesabrookAuthorization]
    [ProgramAccess("company")]
    [LocationAccess(OptionalPermissions = new[] { nameof(LocationEmployee.ManagePrices), nameof(LocationEmployee.ManagePurchaseOrders) })]
    public class StorePricingAutomationLocationController : DataObjectController<StorePricingAutomationLocation>
    {
        protected long? LocationID => long.Parse(Request.Headers.GetValues("LocationID").First());

        public override IEnumerable<string> DefaultRetrievedFields => FieldPathUtility.CreateFieldPathsAsList<StorePricingAutomationLocation>(spal => new List<object>()
        {
            spal.StorePricingAutomationLocationID,
            spal.StorePricingAutomationID,
            spal.LocationIDDestination,
            spal.LocationDestination.LocationID,
            spal.LocationDestination.CompanyID,
            spal.LocationDestination.Company.CompanyID,
            spal.LocationDestination.Company.Name,
            spal.LocationDestination.Name
        });

        public override ISearchCondition GetBaseSearchCondition()
        {
            return new LongSearchCondition<StorePricingAutomationLocation>()
            {
                Field = FieldPathUtility.CreateFieldPathsAsList<StorePricingAutomationLocation>(spal => new List<object>() { spal.StorePricingAutomation.LocationID }).First(),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = LocationID
            };
        }

        public override bool AllowGetAll => true;
    }
}
