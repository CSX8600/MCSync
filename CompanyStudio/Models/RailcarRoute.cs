namespace CompanyStudio.Models
{
    public class RailcarRoute
    {
        public long? RailcarRouteID { get; set; }
        public long? RailcarID { get; set; }
        public Railcar Railcar { get; set; }
        public byte? SortOrder { get; set; }
        public long? CompanyIDFrom { get; set; }
        public Company CompanyFrom { get; set; }
        public long? CompanyIDTo { get; set; }
        public Company CompanyTo { get; set; }
        public long? GovernmentIDFrom { get; set; }
        public Government GovernmentFrom { get; set; }
        public long? GovernmentIDTo { get; set; }
        public Government GovernmentTo { get; set; }

        public string From => GovernmentIDFrom != null ? GovernmentFrom?.Name + " (Government)" : CompanyFrom?.Name;

        public string To => GovernmentIDTo != null ? GovernmentTo?.Name + " (Government)" : CompanyTo?.Name;
    }
}
