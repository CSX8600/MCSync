using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClussPro.Base.Data;
using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.DataSearch;
using ClussPro.ObjectBasedFramework.Schema.Attributes;
using ClussPro.ObjectBasedFramework.Validation;
using ClussPro.ObjectBasedFramework.Validation.Attributes;
using WebModels.company;
using WebModels.gov;
using WebModels.purchasing;

namespace WebModels.fleet
{
    [Table("A65E0F0C-6EC2-4943-B345-B5983B05DFD6")]
    [Unique(new[] { nameof(Name) })]
    public class Track : DataObject
    {
        protected Track() : base() { }

        private long? _trackID;
        [Field("435C7B8C-C8B5-497B-AF2D-E2A21AAC7A12")]
        public long? TrackID
        {
            get { CheckGet(); return _trackID; }
            set { CheckSet(); _trackID = value; }
        }

        private long? _railDistrictID;
        [Field("FAB1095C-38F9-4629-BFAB-0DF9EB15E56C")]
        public long? RailDistrictID
        {
            get { CheckGet(); return _railDistrictID; }
            set { CheckSet(); _railDistrictID = value; }
        }

        private RailDistrict _railDistrict = null;
        [Relationship("ECED49EB-5387-4EE6-86BB-B1BF2803EE56")]
        public RailDistrict RailDistrict
        {
            get { CheckGet(); return _railDistrict; }
        }

        private long? _companyIDOwner;
        [Field("7D10A7ED-4886-4BBF-87E9-EBE39AB51B5D")]
        public long? CompanyIDOwner
        {
            get { CheckGet(); return _companyIDOwner; }
            set { CheckSet(); _companyIDOwner = value; }
        }

        private Company _companyOwner = null;
        [Relationship("12705E87-42E6-478D-92D3-F9147FB86F36", ForeignKeyField = nameof(CompanyIDOwner) )]
        public Company CompanyOwner
        {
            get { CheckGet(); return _companyOwner; }
        }

        private long? _governmentIDOwner;
        [Field("68F0A114-E1FC-43D3-BAC5-8087C737DB28")]
        public long? GovernmentIDOwner
        {
            get { CheckGet(); return _governmentIDOwner; }
            set { CheckSet(); _governmentIDOwner = value; }
        }

        private Government _governmentOwner = null;
        [Relationship("6851039E-CDF1-4AA5-8C68-BA5AD2AC79F2", ForeignKeyField = nameof(GovernmentIDOwner) )]
        public Government GovernmentOwner
        {
            get { CheckGet(); return _governmentOwner; }
        }

        private string _name;
        [Field("D9D14F78-33C7-4C53-A4B4-31967FF51DE0", DataSize = 30)]
        [Required]
        public string Name
        {
            get { CheckGet(); return _name; }
            set { CheckSet(); _name = value; }
        }

        private decimal? _length;
        [Field("EA80FD21-C139-4682-9976-0EF39023AAFD", DataSize = 6, DataScale = 2)]
        [Required]
        public decimal? Length
        {
            get { CheckGet(); return _length; }
            set { CheckSet(); _length = value; }
        }

        public static Errors Reorder(long? trackID, ITransaction transaction = null)
        {
            Errors errors = new Errors();
            ITransaction localTransaction = transaction;
            try
            {
                if (localTransaction == null)
                {
                    localTransaction = SQLProviderFactory.GenerateTransaction();
                }

                Search<RailLocation> railLocationsForTrack = new Search<RailLocation>(new LongSearchCondition<RailLocation>()
                {
                    Field = nameof(RailLocation.TrackID),
                    SearchConditionType = SearchCondition.SearchConditionTypes.Equals,
                    Value = trackID
                });
                railLocationsForTrack.SearchOrders.Add(new SearchOrder()
                {
                    OrderField = nameof(RailLocation.Position)
                });

                int position = 1;
                foreach(RailLocation railLocation in railLocationsForTrack.GetEditableReader(localTransaction))
                {
                    if (railLocation.Position != position)
                    {
                        railLocation.Position = position;
                        if (!railLocation.Save(localTransaction))
                        {
                            errors.AddRange(railLocation.Errors.ToArray());
                        }
                    }

                    position++;
                }

                if (transaction == null)
                {
                    if (errors.Any())
                    {
                        localTransaction.Rollback();
                    }
                    else
                    {
                        localTransaction.Commit();
                    }
                }
            }
            finally
            {
                if (transaction == null && localTransaction != null && localTransaction.IsActive)
                {
                    localTransaction.Rollback();
                }
            }

            return errors;
        }

        #region Relationships
        #region fleet
        private List<LeaseRequest> _leaseRequests = new List<LeaseRequest>();
        [RelationshipList("B78C8212-F61A-48D4-AC85-CB69B9F9AF11", nameof(LeaseRequest.TrackIDDeliveryLocation))]
        public IReadOnlyCollection<LeaseRequest> LeaseRequests
        {
            get { CheckGet(); return _leaseRequests; }
        }

        private List<RailLocation> _railLocations = new List<RailLocation>();
        [RelationshipList("02D1BA12-0F58-4C9C-9BFC-EF5DD97C4807", nameof(RailLocation.TrackID))]
        public IReadOnlyCollection<RailLocation> RailLocations
        {
            get { CheckGet(); return _railLocations; }
        }

        private List<RailcarLocationTransaction> _railcarLocationTransactions = new List<RailcarLocationTransaction>();
        [RelationshipList("96B47121-BA09-4A89-8EDE-79211E8E9330", nameof(RailcarLocationTransaction.TrackIDNew))]
        public IReadOnlyCollection<RailcarLocationTransaction> RailcarLocationTransactions
        {
            get { CheckGet(); return _railcarLocationTransactions; }
        }

        private List<Railcar> _railcarDestinations = new List<Railcar>();
        [RelationshipList("6815DC26-5E6B-4716-BB66-D86D3BFC7EDE", nameof(Railcar.TrackIDDestination))]
        public IReadOnlyCollection<Railcar> RailcarDestinations
        {
            get { CheckGet(); return _railcarDestinations; }
        }

        private List<Railcar> _railcarStrategics = new List<Railcar>();
        [RelationshipList("2A76EAAA-1EB4-492C-815E-F97F7C32E098", nameof(Railcar.TrackIDStrategic))]
        public IReadOnlyCollection<Railcar> RailcarStrategics
        {
            get { CheckGet(); return _railcarStrategics; }
        }
        #endregion
        #region purchasing
        private List<FulfillmentPlan> _fulfillmentPlanLoadings = new List<FulfillmentPlan>();
        [RelationshipList("7F41DD64-F6E0-4DD5-A5F6-17E69C293422", nameof(FulfillmentPlan.TrackIDLoading))]
        public IReadOnlyCollection<FulfillmentPlan> FulfillmentPlanLoadings
        {
            get { CheckGet(); return _fulfillmentPlanLoadings; }
        }

        private List<FulfillmentPlan> _fulfillmentPlanDestinations = new List<FulfillmentPlan>();
        [RelationshipList("BC4AF18B-993C-4938-B786-F5A2BBABE5EA", nameof(FulfillmentPlan.TrackIDDestination))]
        public IReadOnlyCollection<FulfillmentPlan> FulfillmentPlanDestinations
        {
            get { CheckGet(); return _fulfillmentPlanDestinations; }
        }

        private List<FulfillmentPlan> _fulfillmentPlanStrategicAfterLoads = new List<FulfillmentPlan>();
        [RelationshipList("387BACFC-EDE0-4232-84AB-43270848F472", nameof(FulfillmentPlan.TrackIDStrategicAfterLoad))]
        public IReadOnlyCollection<FulfillmentPlan> FulfillmentPlanStrategicAfterLoads
        {
            get { CheckGet(); return _fulfillmentPlanStrategicAfterLoads; }
        }

        private List<FulfillmentPlan> _fulfillmentPlanStrategicAfterDestinations = new List<FulfillmentPlan>();
        [RelationshipList("7CCBFE1B-2DC8-4216-B31A-FDA256CEB63A", nameof(FulfillmentPlan.TrackIDStrategicAfterDestination))]
        public IReadOnlyCollection<FulfillmentPlan> FulfillmentPlanStrategicAfterDestinations
        {
            get { CheckGet(); return _fulfillmentPlanStrategicAfterDestinations; }
        }

        private List<FulfillmentPlan> _fulfillmentPlanPostFulfillments = new List<FulfillmentPlan>();
        [RelationshipList("6E97F804-B665-43CD-9F12-CA7C0951E0B0", nameof(FulfillmentPlan.TrackIDPostFulfillment))]
        public IReadOnlyCollection<FulfillmentPlan> FulfillmentPlanPostFulfillments
        {
            get { CheckGet(); return _fulfillmentPlanPostFulfillments; }
        }
        #endregion
        #endregion
    }
}
