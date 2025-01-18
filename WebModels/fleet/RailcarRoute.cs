using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.Schema.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModels.company;
using WebModels.gov;

namespace WebModels.fleet
{
    [Table("D24AFDFF-2E63-4D24-853A-0EFCA1127DF1")]
    public class RailcarRoute : DataObject
    {
        protected RailcarRoute() : base() { }

        private long? _railcarRouteID;
        [Field("F0E68093-1299-44B4-B9A6-E570D99A0E79")]
        public long? RailcarRouteID
        {
            get { CheckGet(); return _railcarRouteID; }
            set { CheckSet(); _railcarRouteID = value; }
        }

        private long? _railcarID;
        [Field("7A8EBEA7-8C80-4709-95AC-4DC3117C6974")]
        public long? RailcarID
        {
            get { CheckGet(); return _railcarID; }
            set { CheckSet(); _railcarID = value; }
        }

        private Railcar _railcar = null;
        [Relationship("D582181F-80D5-47E0-84BC-340FF8DA84EF")]
        public Railcar Railcar
        {
            get { CheckGet(); return _railcar; }
        }

        private byte? _sortOrder;
        [Field("3D6CE47D-F43E-4F0D-8211-38658014E8E8")]
        public byte? SortOrder
        {
            get { CheckGet(); return _sortOrder; }
            set { CheckSet(); _sortOrder = value; }
        }

        private long? _companyIDFrom;
        [Field("6E9DE3EC-BDF6-43C5-9E77-0A761E161D8B")]
        public long? CompanyIDFrom
        {
            get { CheckGet(); return _companyIDFrom; }
            set { CheckSet(); _companyIDFrom = value; }
        }

        private Company _companyFrom = null;
        [Relationship("B5D3D34C-E6C6-4F4E-BD8B-AA0C40C17060", ForeignKeyField = nameof(CompanyIDFrom))]
        public Company CompanyFrom
        {
            get { CheckGet(); return _companyFrom; }
        }

        private long? _companyIDTo;
        [Field("81C4E2E1-D460-4F75-A1FC-BA599816BEAD")]
        public long? CompanyIDTo
        {
            get { CheckGet(); return _companyIDTo; }
            set { CheckSet(); _companyIDTo = value; }
        }

        private Company _companyTo = null;
        [Relationship("5221CBF5-D470-4FE5-86EF-D6DCA487BBE5", ForeignKeyField = nameof(CompanyIDTo))]
        public Company CompanyTo
        {
            get { CheckGet(); return _companyTo; }
        }

        private long? _governmentIDFrom;
        [Field("5FB43550-9C6C-4FCC-B4E5-0795F92C1C3B")]
        public long? GovernmentIDFrom
        {
            get { CheckGet(); return _governmentIDFrom; }
            set { CheckSet(); _governmentIDFrom = value; }
        }

        private Government _governmentFrom = null;
        [Relationship("FC95C40A-373F-402C-9871-879C02E234FC", ForeignKeyField = nameof(GovernmentIDFrom))]
        public Government GovernmentFrom
        {
            get { CheckGet(); return _governmentFrom; }
        }

        private long? _governmentIDTo;
        [Field("12BA7D91-8DE3-43F1-BD49-116709D0220F")]
        public long? GovernmentIDTo
        {
            get { CheckGet(); return _governmentIDTo; }
            set { CheckSet(); _governmentIDTo = value; }
        }

        private Government _governmentTo = null;
        [Relationship("FFC0677A-0C6D-4DC5-8E60-0E477B7641EF", ForeignKeyField = nameof(GovernmentIDTo))]
        public Government GovernmentTo
        {
            get { CheckGet(); return _governmentTo; }
        }
    }
}
