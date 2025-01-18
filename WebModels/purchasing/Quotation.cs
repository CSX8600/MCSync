using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.Schema.Attributes;
using ClussPro.ObjectBasedFramework.Validation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModels.company;
using WebModels.gov;

namespace WebModels.purchasing
{
    [Table("FFFCC934-2A56-4BD3-9372-27E62538AD43")]
    public class Quotation : DataObject
    {
        protected Quotation() : base() { }

        private long? _quotationID;
        [Field("7C4329A9-7B2E-4F27-8716-280C32AE99EC")]
        public long? QuotationID
        {
            get { CheckGet(); return _quotationID; }
            set { CheckGet(); _quotationID = value; }
        }

        private long? _companyIDFrom;
        [Field("547645B1-615B-4AFF-AE7D-6A515D9B7E30")]
        public long? CompanyIDFrom
        {
            get { CheckGet(); return _companyIDFrom; }
            set { CheckSet(); _companyIDFrom = value; }
        }

        private Company _companyFrom = null;
        [Relationship("D824F14E-6CCC-442C-91C9-58A8542F13C1", ForeignKeyField = nameof(CompanyIDFrom))]
        public Company CompanyFrom
        {
            get { CheckGet(); return _companyFrom; }
        }

        private long? _companyIDTo;
        [Field("C0A793A5-D767-4740-90B7-AF472A9490AE")]
        public long? CompanyIDTo
        {
            get { CheckGet(); return _companyIDTo; }
            set { CheckSet(); _companyIDTo = value; }
        }

        private Company _companyTo = null;
        [Relationship("71C0E606-939B-4D08-A8CE-430CD3E90912", ForeignKeyField = nameof(CompanyIDTo))]
        public Company CompanyTo
        {
            get { CheckGet(); return _companyTo; }
        }

        private long? _governmentIDFrom;
        [Field("24BE08CA-698F-435E-9687-D0856C986758")]
        public long? GovernmentIDFrom
        {
            get { CheckGet(); return _governmentIDFrom; }
            set { CheckSet(); _governmentIDFrom = value; }
        }

        private Government _governmentFrom = null;
        [Relationship("9A71ABE6-3A26-4B5B-8652-EB195DC1F5F2", ForeignKeyField = nameof(GovernmentIDFrom))]
        public Government GovernmentFrom
        {
            get { CheckGet(); return _governmentFrom; }
        }

        private long? _governmentIDTo;
        [Field("091C077A-20AC-4D7C-B095-06CFD0F6D406")]
        public long? GovernmentIDTo
        {
            get { CheckGet(); return _governmentIDTo; }
            set { CheckSet(); _governmentIDTo = value; }
        }

        private Government _governmentTo = null;
        [Relationship("122A383C-7CE1-43C7-A0CB-16D899771536", ForeignKeyField = nameof(GovernmentIDTo))]
        public Government GovernmentTo
        {
            get { CheckGet(); return _governmentTo; }
        }

        private bool _isRepeatable;
        [Field("70E0D179-8304-4888-8DF6-1B7DF7FE7216")]
        public bool IsRepeatable
        {
            get { CheckGet(); return _isRepeatable; }
            set { CheckSet(); _isRepeatable = value; }
        }

        private DateTime? _expirationTime;
        [Field("9A4B22B1-36D0-43FB-BF23-0FEC60B540F8", DataSize = 7)]
        [Required]
        public DateTime? ExpirationTime
        {
            get { CheckGet(); return _expirationTime; }
            set { CheckSet(); _expirationTime = value; }
        }

        #region Relationships
        #region purchasing
        private List<QuotationItem> _quotationItems = new List<QuotationItem>();
        [RelationshipList("6B51E8C6-7586-42DA-B5FE-1271160F8D23", nameof(QuotationItem.QuotationID), AutoDeleteReferences = true)]
        public IReadOnlyCollection<QuotationItem> QuotationItems
        {
            get { CheckGet(); return _quotationItems; }
        }
        #endregion
        #endregion
    }
}
