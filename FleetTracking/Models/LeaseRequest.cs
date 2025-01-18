using System;
using System.Collections.Generic;

namespace FleetTracking.Models
{
    public class LeaseRequest
    {
        public long? LeaseRequestID { get; set; }
        public long? CompanyIDRequester { get; set; }
        public Company CompanyRequester { get; set; }
        public long? LocationIDChargeTo { get; set; }
        public Location LocationChargeTo { get; set; }
        public long? GovernmentIDRequester { get; set; }
        public Government GovernmentRequester { get; set; }
        public enum LeaseTypes
        {
            Locomotive,
            Railcar
        }
        public LeaseTypes LeaseType { get; set; }
        public RailcarModel.Types RailcarType { get; set; }
        public long? TrackIDDeliveryLocation { get; set; }
        public Track TrackDeliveryLocation { get; set; }
        public string Purpose { get; set; }
        public DateTime? BidEndTime { get; set; }

        public List<LeaseBid> LeaseBids { get; set; } = new List<LeaseBid>();
    }
}
