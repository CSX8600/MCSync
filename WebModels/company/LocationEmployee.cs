using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.Schema.Attributes;
using ClussPro.ObjectBasedFramework.Validation.Attributes;

namespace WebModels.company
{
    [Table("497C8DED-10B5-4F24-BE72-5AECA70B2382")]
    [Unique(new [] { nameof(EmployeeID), nameof(LocationID) })]
    public class LocationEmployee : DataObject
    {
        protected LocationEmployee() : base() { }

        private long? _locationEmployeeID;
        [Field("EA47570C-5252-4411-B3FE-4864F24C9DC1")]
        public long? LocationEmployeeID
        {
            get { CheckGet(); return _locationEmployeeID; }
            set { CheckSet(); _locationEmployeeID = value; }
        }

        private long? _locationID;
        [Field("9954534E-F835-4685-9189-68390763FAB0")]
        [Required]
        public long? LocationID
        {
            get { CheckGet(); return _locationID; }
            set { CheckSet(); _locationID = value; }
        }

        private Location _location = null;
        [Relationship("BA1C689D-90CE-4C9A-9F90-3F1C79AACDFF")]
        public Location Location
        {
            get { CheckGet(); return _location; }
        }

        private long? _employeeID;
        [Field("EC60F9E4-C295-4907-BCFD-66DC6C7F95E4")]
        [Required]
        public long? EmployeeID
        {
            get { CheckGet(); return _employeeID; }
            set { CheckSet(); _employeeID = value; }
        }

        private Employee _employee = null;
        [Relationship("0DC3CFF5-F505-45BD-AA29-E79B1FDD6B61")]
        public Employee Employee
        {
            get { CheckGet(); return _employee; }
        }

        private bool _manageInvoices;
        [Field("63324678-DF65-4435-81BB-5F97BBB77D1C")]
        public bool ManageInvoices
        {
            get { CheckGet(); return _manageInvoices; }
            set { CheckSet(); _manageInvoices = value; }
        }

        private bool _managePrices = false;
        [Field("ADFB278E-D9DD-47C0-9F6E-B2C6794F3AFF")]
        public bool ManagePrices
        {
            get { CheckGet(); return _managePrices; }
            set { CheckSet(); _managePrices = value; }
        }

        private bool _manageRegisters = false;
        [Field("1B0D110F-3913-445D-AC2F-6BD093847BE7")]
        public bool ManageRegisters
        {
            get { CheckGet(); return _manageRegisters; }
            set { CheckSet(); _manageRegisters = value; }
        }

        private bool _manageInventory = false;
        [Field("38F0E4F2-B50E-4ED2-AB5D-E867DA3EA5D0")]
        public bool ManageInventory
        {
            get { CheckGet(); return _manageInventory; }
            set { CheckSet(); _manageInventory = value; }
        }

        private bool _managePurchaseOrders = false;
        [Field("941E2E57-7076-4D5C-A895-77E1BBA76E0D")]
        public bool ManagePurchaseOrders
        {
            get { CheckGet(); return _managePurchaseOrders; }
            set { CheckSet(); _managePurchaseOrders = value; }
        }
    }
}
