using FleetTracking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GovernmentPortal.Models
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

        public string RouteDisplayString
        {
            get
            {
                if (!FulfillmentPlanRoutes.Any())
                {
                    return string.Empty;
                }

                List<string> routes = new List<string>();
                routes.Add(FulfillmentPlanRoutes[0].From);
                foreach(FulfillmentPlanRoute route in FulfillmentPlanRoutes)
                {
                    routes.Add(route.To);
                }

                return string.Join(" -> ", routes.ToArray());
            }
        }

        public List<FulfillmentPlanPurchaseOrderLine> FulfillmentPlanPurchaseOrderLines { get; set; } = new List<FulfillmentPlanPurchaseOrderLine>();
        public List<FulfillmentPlanRoute> FulfillmentPlanRoutes { get; set; } = new List<FulfillmentPlanRoute>();
    }
}
