using API.Common;
using API.Common.Attributes;
using API_Company.Attributes;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using WebModels.company;

namespace API_Company.Controllers
{
    [MesabrookAuthorization]
    [ProgramAccess("company")]
    [LocationAccess(OptionalPermissions = new[] { nameof(LocationEmployee.ManagePrices), nameof(LocationEmployee.ManagePurchaseOrders) })]
    public class StorePricingAutomationController : DataObjectController<StorePricingAutomation>
    {
        protected long? LocationID => long.Parse(Request.Headers.GetValues("LocationID").First());

        public override IEnumerable<string> DefaultRetrievedFields => FieldPathUtility.CreateFieldPathsAsList<StorePricingAutomation>(spa => new List<object>()
        {
            spa.StorePricingAutomationID,
            spa.LocationID,
            spa.IsEnabled,
            spa.PushAdd,
            spa.PushDelete,
            spa.PushUpdate,
            spa.StorePricingAutomationLocations.First().StorePricingAutomationLocationID,
            spa.StorePricingAutomationLocations.First().StorePricingAutomationID,
            spa.StorePricingAutomationLocations.First().LocationIDDestination,
            spa.StorePricingAutomationLocations.First().LocationDestination.LocationID,
            spa.StorePricingAutomationLocations.First().LocationDestination.CompanyID,
            spa.StorePricingAutomationLocations.First().LocationDestination.Company.CompanyID,
            spa.StorePricingAutomationLocations.First().LocationDestination.Company.Name,
            spa.StorePricingAutomationLocations.First().LocationDestination.Name
        });

        public override ISearchCondition GetBaseSearchCondition()
        {
            return new LongSearchCondition<StorePricingAutomation>()
            {
                Field = nameof(StorePricingAutomation.LocationID),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = LocationID
            };
        }

        [HttpGet]
        public async Task<StorePricingAutomation> Get()
        {
            Search<StorePricingAutomation> search = new Search<StorePricingAutomation>(GetBaseSearchCondition());
            StorePricingAutomation automation = search.GetReadOnly(null, await FieldsToRetrieve());
            if (automation == null)
            {
                automation = DataObjectFactory.Create<StorePricingAutomation>();
                automation.LocationID = LocationID;
                automation.Save();
            }

            return search.GetReadOnly(null, await FieldsToRetrieve());
        }
    }
}
