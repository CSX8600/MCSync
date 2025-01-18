using System;

namespace GovernmentPortal.Models
{
    [Serializable]
    public class PurchaseOrderTemplateFolder
    {
        public long? PurchaseOrderTemplateFolderID { get; set; }
        public long? LocationID { get; set; }
        public long? CompanyID { get; set; }
        public long? PurchaseOrderTemplateFolderIDParent { get; set; }
        public string Name { get; set; }

        public string FolderPath { get; set; }
    }
}
