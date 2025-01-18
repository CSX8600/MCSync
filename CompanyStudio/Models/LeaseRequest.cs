using System;
using System.Collections.Generic;

namespace CompanyStudio.Models
{
    public class LeaseRequest
    {
        public long? LeaseRequestID { get; set; }

        public enum LeaseTypes
        {
            Locomotive,
            Railcar
        }
        public LeaseTypes LeaseType { get; set; }

        public enum RailcarTypes
        {
            // Freight
            Box = 0,
            Tank = 1,
            Hopper = 2,
            Flat = 3,
            BulkheadFlat = 4,
            BulkheadStanchionFlat = 5,
            Centerbeam = 6,
            Autorack = 7,
            Gondola = 8,
            Well = 9,

            // Passenger
            Coach = 50,
            Diner = 51,
            Sleeper = 52,
            Baggage = 53,
            Mail = 54,
            Caboose = 55,
            Cab = 56
        }

        public RailcarTypes RailcarType { get; set; }

        public DateTime? BidEndTime { get; set; }
        public string Purpose { get; set; }
        public List<LeaseBid> LeaseBids { get; set; }
    }
}
