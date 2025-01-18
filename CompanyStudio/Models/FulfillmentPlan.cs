using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyStudio.Models
{
    public class FulfillmentPlan
    {
        public long? FulfillmentPlanID { get; set; }
        public long? RailcarID { get; set; }
        public Railcar Railcar { get; set; }
        public long? LeaseRequestID { get; set; }
        public LeaseRequest LeaseRequest { get; set; }
        public long? TrackIDLoading { get; set; }
        public Track TrackLoading { get; set; }
        public long? TrackIDDestination { get; set; }
        public Track TrackDestination { get; set; }
        public long? TrackIDStrategicAfterLoad { get; set; }
        public Track TrackStrategicAfterLoad { get; set; }
        public long? TrackIDStrategicAfterDestination { get; set; }
        public Track TrackStrategicAfterDestination { get; set; }
        public long? TrackIDPostFulfillment { get; set; }
        public Track TrackPostFulfillment { get; set; }

        public List<FulfillmentPlanPurchaseOrderLine> FulfillmentPlanPurchaseOrderLines { get; set; } = new List<FulfillmentPlanPurchaseOrderLine>();
        public List<FulfillmentPlanRoute> FulfillmentPlanRoutes { get; set; } = new List<FulfillmentPlanRoute>();
    }
}
