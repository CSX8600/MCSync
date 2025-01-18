using ClussPro.Base.Data.Operand;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.Schema.Attributes;
using ClussPro.ObjectBasedFramework.Validation.Attributes;
using System;
using WebModels.account;
using WebModels.company;
using WebModels.gov;
using WebModels.hMailServer.dbo;

namespace WebModels.invoicing
{
    [Table("05E97DB3-84BC-46C9-B6CA-4F9A0490C2CA")]
    public class AutomaticInvoicePaymentConfiguration : DataObject
    {
        protected AutomaticInvoicePaymentConfiguration() : base() { }

        private long? _automaticInvoicePaymentConfigurationID = null;
        [Field("09766429-F771-4BDB-A735-4519BACDEE0A")]
        public long? AutomaticInvoicePaymentConfigurationID
        {
            get { CheckGet(); return _automaticInvoicePaymentConfigurationID; }
            set { CheckSet(); _automaticInvoicePaymentConfigurationID = value; }
        }

        private long? _locationIDConfiguredFor;
        [Field("71F4D57D-8475-4FA8-AAED-F702F31C3830")]
        public long? LocationIDConfiguredFor
        {
            get { CheckGet(); return _locationIDConfiguredFor; }
            set { CheckSet(); _locationIDConfiguredFor = value; }
        }

        private Location _locationConfiguredFor = null;
        [Relationship("2E9964BE-7C27-408A-A5DD-8D02E10580A9", ForeignKeyField = nameof(LocationIDConfiguredFor))]
        public Location LocationConfiguredFor
        {
            get { CheckGet(); return _locationConfiguredFor; }
        }

        private long? _governmentIDConfiguredFor;
        [Field("799A62E3-3B9A-4EA7-A507-A743AAE6E982")]
        public long? GovernmentIDConfiguredFor
        {
            get { CheckGet(); return _governmentIDConfiguredFor; }
            set { CheckSet(); _governmentIDConfiguredFor = value; }
        }

        private Government _governmentConfiguredFor = null;
        [Relationship("2C4BC969-37C2-4763-9A71-73B40C92E43E", ForeignKeyField = nameof(GovernmentIDConfiguredFor))]
        public Government GovernmentConfiguredFor
        {
            get { CheckGet(); return _governmentConfiguredFor; }
        }

        private long? _locationIDPayee;
        [Field("50FBBB6F-F530-4698-963C-283A49BCF779")]
        public long? LocationIDPayee
        {
            get { CheckGet(); return _locationIDPayee; }
            set { CheckSet(); _locationIDPayee = value; }
        }

        private Location _locationPayee = null;
        [Relationship("5A1D4D02-09C8-460A-8B38-02C86CCB794C", ForeignKeyField = nameof(LocationIDPayee))]
        public Location LocationPayee
        {
            get { CheckGet(); return _locationPayee; }
        }

        private long? _governmentIDPayee;
        [Field("B4874DAD-07B4-4AC6-96C0-05F577D53B71")]
        public long? GovernmentIDPayee
        {
            get { CheckGet(); return _governmentIDPayee; }
            set { CheckSet(); _governmentIDPayee = value; }
        }

        private Government _governmentPayee = null;
        [Relationship("A0EF9548-98CC-4524-A500-1960E4D417FD", ForeignKeyField = nameof(GovernmentIDPayee))]
        public Government GovernmentPayee
        {
            get { CheckGet(); return _governmentPayee; }
        }

        private decimal? _paidAmount;
        [Field("7DACCD8D-D23C-4A80-85A8-51D0A0D42F9F", DataSize = 9, DataScale = 2)]
        [Required]
        public decimal? PaidAmount
        {
            get { CheckGet(); return _paidAmount; }
            set { CheckSet(); _paidAmount = value; }
        }

        private decimal? _maxAmount;
        [Field("BF47B8A4-42EE-4FF7-9CEF-84E9F2F1220C", DataSize = 9, DataScale = 2)]
        [Required]
        public decimal? MaxAmount
        {
            get { CheckGet(); return _maxAmount; }
            set { CheckSet(); _maxAmount = value; }
        }

        private decimal? _remainingAmount;
        [Field("3376E5CA-E49A-4389-B43F-0CD1F4D8FB09", HasOperation = true)]
        public decimal? RemainingAmount
        {
            get { CheckGet(); return _remainingAmount; }
        }

        public static OperationDelegate RemainingAmountOperation
        {
            get
            {
                return (alias) =>
                {
                    return new Subtraction((Field)$"{alias}.{nameof(MaxAmount)}", (Field)$"{alias}.{nameof(PaidAmount)}");
                };
            }
        }

        public enum Schedules
        {
            OnDueDate,
            Immediately
        }

        private Schedules _schedule;
        [Field("D1A8BDC4-FF5D-4F6A-BB7C-2B9B0F6F0B8F")]
        public Schedules Schedule
        {
            get { CheckGet(); return _schedule; }
            set { CheckSet(); _schedule = value; }
        }

        private long? _accountID;
        [Field("101971EE-E452-46C4-8E40-2A0BC31E1D2F")]
        public long? AccountID
        {
            get { CheckGet(); return _accountID; }
            set { CheckSet(); _accountID = value; }
        }

        private Account _account;
        [Relationship("8CAE7C4A-B75D-492B-8988-442F99E9C359")]
        public Account Account
        {
            get { CheckGet(); return _account; }
        }
    }
}
