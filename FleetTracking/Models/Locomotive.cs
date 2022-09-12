﻿namespace FleetTracking.Models
{
    public class Locomotive
    {
        public long? LocomotiveID { get; set; }
        public long? LocomotiveModelID { get; set; }
        public LocomotiveModel LocomotiveModel { get; set; }
        public long? CompanyIDOwner { get; set; }
        public Company CompanyOwner { get; set; }
        public long? GovernmentIDOwner { get; set; }
        public Government GovernmentOwner { get; set; }
        public string ReportingMark { get; set; }
        public int? ReportingNumber { get; set; }
    }
}