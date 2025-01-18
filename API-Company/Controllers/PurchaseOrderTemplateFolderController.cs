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
    [LocationAccess(RequiredPermissions = new string[] { nameof(LocationEmployee.ManagePurchaseOrders) })]
    public class PurchaseOrderTemplateFolderController : DataObjectController<PurchaseOrderTemplateFolder>
    {
        protected long LocationID => long.Parse(Request.Headers.GetValues("LocationID").First());

        public override IEnumerable<string> DefaultRetrievedFields => FieldPathUtility.CreateFieldPathsAsList<PurchaseOrderTemplateFolder>(potf => new List<object>()
        {
            potf.PurchaseOrderTemplateFolderID,
            potf.PurchaseOrderTemplateFolderIDParent,
            potf.LocationID,
            potf.GovernmentID,
            potf.Name,
            potf.PurchaseOrderTemplateFolders.First().PurchaseOrderTemplateFolderID,
            potf.PurchaseOrderTemplateFolders.First().PurchaseOrderTemplateFolderIDParent,
            potf.PurchaseOrderTemplateFolders.First().LocationID,
            potf.PurchaseOrderTemplateFolders.First().GovernmentID,
            potf.PurchaseOrderTemplateFolders.First().Name
        });

        public override ISearchCondition GetBaseSearchCondition()
        {
            return new LongSearchCondition<PurchaseOrderTemplateFolder>()
            {
                Field = nameof(PurchaseOrderTemplateFolder.LocationID),
                SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                Value = LocationID
            };
        }

        public override bool AllowGetAll => true;
    }
}
