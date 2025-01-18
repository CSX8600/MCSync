namespace GovernmentPortal.Models
{
    public class AutomaticInvoicePaymentConfiguration
    {
        public long? AutomaticInvoicePaymentConfigurationID { get; set; }
        public long? GovernmentIDConfiguredFor { get; set; }
        public Government GovernmentConfiguredFor { get; set; }
        public long? LocationIDPayee { get; set; }
        public Location LocationPayee { get; set; }
        public long? GovernmentIDPayee { get; set; }
        public Government GovernmentPayee { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? MaxAmount { get; set; }
        public enum Schedules
        {
            OnDueDate,
            Immediately
        }
        public Schedules Schedule { get; set; }
        public long? AccountID { get; set; }
        public Account Account { get; set; }

        public string DisplayName => LocationIDPayee != null ? LocationPayee?.Company?.Name + " (" + LocationPayee?.Name + ")" : GovernmentPayee?.Name + " (Government)";
    }
}
