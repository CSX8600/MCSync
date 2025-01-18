using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.Schema.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModels.company;
using WebModels.gov;

namespace WebModels.purchasing
{
    [Table("4486A474-158C-4BBB-A12A-BA1C51FA8CBB")]
    public class QuotationRequest : DataObject
    {
        protected QuotationRequest() : base() { }

        private long? _quotationRequestID;
        [Field("63678C5E-195C-48EA-A541-AAD0E6662FB7")]
        public long? QuotationRequestID
        {
            get { CheckGet(); return _quotationRequestID; }
            set { CheckSet(); _quotationRequestID = value; }
        }

        private long? _companyIDFrom;
        [Field("D8B4F7D5-C917-4D60-A119-C4AD23481FCE")]
        public long? CompanyIDFrom
        {
            get { CheckGet(); return _companyIDFrom; }
            set { CheckSet(); _companyIDFrom = value; }
        }

        private Company _companyFrom = null;
        [Relationship("9340A38A-B8D9-488E-B029-910DCC897AB3", ForeignKeyField = nameof(CompanyIDFrom))]
        public Company CompanyFrom
        {
            get { CheckGet(); return _companyFrom; }
        }

        private long? _companyIDTo;
        [Field("FA028E51-3042-4041-BA58-7715B64D39B9")]
        public long? CompanyIDTo
        {
            get { CheckGet(); return _companyIDTo; }
            set { CheckSet(); _companyIDTo = value; }
        }

        private Company _companyTo = null;
        [Relationship("C5D7DD7C-55E0-4FA2-B283-C8ACA513C336", ForeignKeyField = nameof(CompanyIDTo))]
        public Company CompanyTo
        {
            get { CheckGet(); return _companyTo; }
        }

        private long? _governmentIDFrom;
        [Field("3448BB4F-B4A0-4C2D-8C7C-B2D92DA59DE0")]
        public long? GovernmentIDFrom
        {
            get { CheckGet(); return _governmentIDFrom; }
            set { CheckSet(); _governmentIDFrom = value; }
        }

        private Government _governmentFrom = null;
        [Relationship("9781C12B-9CD0-4969-8804-270B3C11AB00", ForeignKeyField = nameof(GovernmentIDFrom))]
        public Government GovernmentFrom
        {
            get { CheckGet(); return _governmentFrom; }
        }

        private long? _governmentIDTo;
        [Field("F8B5B1F5-5C7B-4E7D-8E0C-6A6F0F9A7D8F")]
        public long? GovernmentIDTo
        {
            get { CheckGet(); return _governmentIDTo; }
            set { CheckSet(); _governmentIDTo = value; }
        }

        private Government _governmentTo = null;
        [Relationship("82D47571-FFDC-43A4-B671-38A9ED52285A", ForeignKeyField = nameof(GovernmentIDTo))]
        public Government GovernmentTo
        {
            get { CheckGet(); return _governmentTo; }
        }

        private string _notes;
        [Field("956AFF7B-2074-4D87-A7A6-FE28EF91777E", DataScale = 4000)]
        public string Notes
        {
            get { CheckGet(); return _notes; }
            set { CheckSet(); _notes = value; }
        }

        #region Relationships
        #region purchasing
        private List<QuotationRequestItem> _quotationRequestItems = new List<QuotationRequestItem>();
        [RelationshipList("39D28A20-2882-41AF-B8C1-B54290584DF3", nameof(QuotationRequestItem.QuotationRequestID), AutoDeleteReferences = true)]
        public IReadOnlyCollection<QuotationRequestItem> QuotationRequestItems
        {
            get { CheckGet(); return _quotationRequestItems; }
        }
        #endregion
        #endregion
    }
}
