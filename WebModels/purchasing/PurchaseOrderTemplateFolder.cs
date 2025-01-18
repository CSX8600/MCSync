using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.Schema.Attributes;
using ClussPro.ObjectBasedFramework.Validation.Attributes;
using System.Collections.Generic;
using WebModels.company;
using WebModels.gov;

namespace WebModels.purchasing
{
    [Table("BA668381-76AA-4EE9-B4AD-625C860F386F")]
    public class PurchaseOrderTemplateFolder : DataObject
    {
        protected PurchaseOrderTemplateFolder() : base() { }

        private long? _purchaseOrderTemplateFolderID;
        [Field("43418697-6E0F-40F1-96BF-D3440F3F536C")]
        public long? PurchaseOrderTemplateFolderID
        {
            get { CheckGet(); return _purchaseOrderTemplateFolderID; }
            set { CheckSet(); _purchaseOrderTemplateFolderID = value; }
        }

        private long? _locationID;
        [Field("C358800E-8649-4462-8802-F0434A6C716C")]
        public long? LocationID
        {
            get { CheckGet(); return _locationID; }
            set { CheckSet(); _locationID = value; }
        }

        private Location _location = null;
        [Relationship("D8462FF9-14F4-47D0-BF82-FA4E418F846F")]
        public Location Location
        {
            get { CheckGet(); return _location; }
            set { CheckSet(); _location = value; }
        }

        private long? _governmentID;
        [Field("39ACEA58-B9D1-4845-B424-0E3EC3A39F31")]
        public long? GovernmentID
        {
            get { CheckGet(); return _governmentID; }
            set { CheckSet(); _governmentID = value; }
        }

        private Government _government = null;
        [Relationship("7AB189C0-7429-4F9E-B8B9-13514EEA0B6F")]
        public Government Government
        {
            get { CheckGet(); return _government; }
            set { CheckSet(); _government = value; }
        }

        private long? _purchaseOrderTemplateFolderIDParent;
        [Field("2C0EA744-D29A-40AA-9E7D-C61F2CC9F75B")]
        public long? PurchaseOrderTemplateFolderIDParent
        {
            get { CheckGet(); return _purchaseOrderTemplateFolderIDParent; }
            set { CheckSet(); _purchaseOrderTemplateFolderIDParent = value; }
        }

        private PurchaseOrderTemplateFolder _purchaseOrderTemplateFolderParent = null;
        [Relationship("748BA482-C9E1-4F17-B9A1-1906926F8774", ForeignKeyField = nameof(PurchaseOrderTemplateFolderIDParent))]
        public PurchaseOrderTemplateFolder PurchaseOrderTemplateFolderParent
        {
            get { CheckGet(); return _purchaseOrderTemplateFolderParent; }
            set { CheckSet(); _purchaseOrderTemplateFolderParent = value; }
        }

        private string _name;
        [Field("63B6B5B9-7A1F-4CCB-858D-21EA634E54B3", DataSize = 255)]
        [Required]
        public string Name
        {
            get { CheckGet(); return _name; }
            set { CheckSet(); _name = value; }
        }

        #region Relationships
        #region purchasing
        private List<PurchaseOrderTemplateFolder> _purchaseOrderTemplateFolders = new List<PurchaseOrderTemplateFolder>();
        [RelationshipList("824A50B2-066F-47BC-9CA0-4BD0FABAC24B", nameof(PurchaseOrderTemplateFolderIDParent), AutoDeleteReferences = true)]
        public IReadOnlyCollection<PurchaseOrderTemplateFolder> PurchaseOrderTemplateFolders
        {
            get { CheckGet(); return _purchaseOrderTemplateFolders; }
        }

        private List<PurchaseOrderTemplate> _purchaseOrderTemplates = new List<PurchaseOrderTemplate>();
        [RelationshipList("B3DAC4F7-DC01-4A7A-9648-8A8F281A0E5A", nameof(PurchaseOrderTemplate.PurchaseOrderTemplateFolderID), AutoDeleteReferences = true)]
        public IReadOnlyCollection<PurchaseOrderTemplate> PurchaseOrderTemplates
        {
            get { CheckGet(); return _purchaseOrderTemplates; }
        }
        #endregion
        #endregion
    }
}
