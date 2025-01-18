using MesaSuite.Common.Data;
using System;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GovernmentPortal.Models
{
    [Serializable]
    public class PurchaseOrderTemplate
    {
        public long? PurchaseOrderTemplateID { get; set; }
        public long? LocationID { get; set; }
        public long? CompanyID { get; set; }
        public long? PurchaseOrderTemplateFolderID { get; set; }
        public long? PurchaseOrderID { get; set; }
        public string Name { get; set; }

        //public static async Task<long?> PromptAndSavePurchaseOrderAsTemplate(Company company, Location location, ThemeBase theme, long? purchaseOrderID)
        //{
        //    Purchasing.Templates.frmTemplateDialog saveTemplate = new Purchasing.Templates.frmTemplateDialog()
        //    {
        //        CompanyID = company.CompanyID,
        //        LocationID = location.LocationID,
        //        Theme = theme,
        //        DialogMode = Purchasing.Templates.frmTemplateDialog.DialogModes.Save
        //    };
        //    DialogResult result = saveTemplate.ShowDialog();

        //    if (result != DialogResult.OK)
        //    {
        //        return null;
        //    }

        //    PurchaseOrderTemplate template = saveTemplate.SelectedTemplate;
        //    template.PurchaseOrderID = purchaseOrderID;

        //    if (template.PurchaseOrderTemplateID == null)
        //    {
        //        PostData post = new PostData(DataAccess.APIs.CompanyStudio, "PurchaseOrderTemplate/Post", template);
        //        post.AddLocationHeader(company.CompanyID, location.LocationID);
        //        template = await post.Execute<PurchaseOrderTemplate>();
        //        if (post.RequestSuccessful)
        //        {
        //            return template.PurchaseOrderTemplateID;
        //        }
        //    }
        //    else
        //    {
        //        PutData put = new PutData(DataAccess.APIs.CompanyStudio, "PurchaseOrderTemplate/Put", template);
        //        put.AddLocationHeader(company.CompanyID, location.LocationID);
        //        await put.ExecuteNoResult();

        //        if (put.RequestSuccessful)
        //        {
        //            return template.PurchaseOrderTemplateID;
        //        }
        //    }

        //    return null;
        //}
    }
}
