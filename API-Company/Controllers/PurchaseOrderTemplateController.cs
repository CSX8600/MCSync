using API.Common;
using API.Common.Attributes;
using API_Company.Attributes;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Utility;
using System.Collections.Generic;
using System.Linq;
using WebModels.company;
using WebModels.purchasing;

namespace API_Company.Controllers
{
    [MesabrookAuthorization]
    [ProgramAccess("company")]
    [LocationAccess(RequiredPermissions = new[] { nameof(LocationEmployee.ManagePurchaseOrders) })]
    public class PurchaseOrderTemplateController : DataObjectController<PurchaseOrderTemplate>
    {
        protected long? LocationID => long.Parse(Request.Headers.GetValues("LocationID").First());

        public override IEnumerable<string> DefaultRetrievedFields => FieldPathUtility.CreateFieldPathsAsList<PurchaseOrderTemplate>(pot => new List<object>()
        {
            pot.PurchaseOrderTemplateID,
            pot.PurchaseOrderTemplateFolderID,
            pot.LocationID,
            pot.GovernmentID,
            pot.PurchaseOrderID,
            pot.Name
        });

        public override bool AllowGetAll => true;

        public override ISearchCondition GetBaseSearchCondition()
        {
            return new LongSearchCondition<PurchaseOrderTemplate>()
            {
                Field = nameof(PurchaseOrderTemplate.LocationID),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = LocationID
            };
        }
    }
}
