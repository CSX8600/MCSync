using System.Collections.Generic;
using ClussPro.Base.Data;
using ClussPro.Base.Data.Conditions;
using ClussPro.Base.Data.Query;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.Schema.Attributes;
using ClussPro.ObjectBasedFramework.Validation.Attributes;
using WebModels.security;

namespace WebModels.gov
{
    [Table("3C2F6AAE-3223-4E4F-B07C-844D31C0BE67")]
    [Unique(new string[] { "UserID", "GovernmentID" })]
    public class Official : DataObject
    {
        protected Official() : base() { }

        private long? _officialID;
        [Field("1C3754E1-2797-46E7-B7AA-7D6375DC447F")]
        public long? OfficialID
        {
            get { CheckGet(); return _officialID; }
            set { CheckSet(); _officialID = value; }
        }

        private long? _governmentID;
        [Field("B4937A6B-8336-4803-A233-8DE4A80E9440")]
        [Required]
        public long? GovernmentID
        {
            get { CheckGet(); return _governmentID; }
            set { CheckSet(); _governmentID = value; }
        }

        private Government _government = null;
        [Relationship("F3479D1C-11BC-4314-9EDE-5C7796B2CB2A")]
        public Government Government
        {
            get { CheckGet(); return _government; }
        }

        private long? _userID;
        [Field("CFC7EF40-C3A7-4706-BA61-1B43457C78CD")]
        [Required]
        public long? UserID
        {
            get { CheckGet(); return _userID; }
            set { CheckSet(); _userID = value; }
        }

        private User _user = null;
        [Relationship("56F4BB82-D9E9-48F8-BC96-683702586C51")]
        public User User
        {
            get { CheckGet(); return _user; }
        }

        private bool _manageEmails;
        [Field("2ED98E91-2F3E-4013-9335-182A35A712BC")]
        public bool ManageEmails
        {
            get { CheckGet(); return _manageEmails; }
            set { CheckSet(); _manageEmails = value; }
        }

        private bool _manageOfficials;
        [Field("5F32A587-D829-4842-AFCB-93262903D653")]
        public bool ManageOfficials
        {
            get { CheckGet(); return _manageOfficials; }
            set { CheckSet(); _manageOfficials = value; }
        }

        private bool _manageAccounts;
        [Field("A7304EA0-9424-4877-A6B9-D3C224D892F7")]
        public bool ManageAccounts
        {
            get { CheckGet(); return _manageAccounts; }
            set { CheckSet(); _manageAccounts = value; }
        }

        private string _officialName = null;
        [Field("51C127CE-45FB-4512-8325-00CF355510EA", HasOperation = true)]
        public string OfficialName
        {
            get { CheckGet(); return _officialName; }
        }

        private bool _canMintCurrency;
        [Field("3ED77E8D-E474-46C2-AB5A-ED6339F45C7A")]
        public bool CanMintCurrency
        {
            get { CheckGet(); return _canMintCurrency; }
            set { CheckSet(); _canMintCurrency = value; }
        }

        private bool _manageTaxes;
        [Field("1469A00B-F68B-4F95-8E34-9412029F5CE4")]
        public bool ManageTaxes
        {
            get { CheckGet(); return _manageTaxes; }
            set { CheckSet(); _manageTaxes = value; }
        }

        private bool _manageInvoices;
        [Field("721FFCCA-8FE4-4E20-BCD2-21B066B1AEBB")]
        public bool ManageInvoices
        {
            get { CheckGet(); return _manageInvoices; }
            set { CheckSet(); _manageInvoices = value; }
        }

        private bool _issueWireTransfers;
        [Field("52C0DA10-6D3C-4540-BC45-F95E80C07E97")]
        public bool IssueWireTransfers
        {
            get { CheckGet(); return _issueWireTransfers; }
            set { CheckSet(); _issueWireTransfers = value; }
        }

        private bool _canConfigureInterest;
        [Field("DBA3B315-F4D8-4C27-B771-B5FF35A67812")]
        public bool CanConfigureInterest
        {
            get { CheckGet(); return _canConfigureInterest; }
            set { CheckSet(); _canConfigureInterest = value; }
        }

        private bool _manageLaws;
        [Field("91D9DC2C-421C-4D55-B1B5-E260F2AD515A")]
        public bool ManageLaws
        {
            get { CheckGet(); return _manageLaws; }
            set { CheckSet(); _manageLaws = value; }
        }

        private bool _managePurchaseOrders;
        [Field("CF94F559-717A-40B2-8A06-1A7E1FB19998")]
        public bool ManagePurchaseOrders
        {
            get { CheckGet(); return _managePurchaseOrders; }
            set { CheckSet(); _managePurchaseOrders = value; }
        }

        private fleet.FleetSecurity _fleetSecurity = null;
        [Relationship("FED1EFD8-73ED-4E3F-88D9-C71E67D63725", OneToOneByForeignKey = true)]
        public fleet.FleetSecurity FleetSecurity
        {
            get { CheckGet(); return _fleetSecurity; }
        }

        public static OperationDelegate OfficialNameOperation
        {
            get
            {
                return alias =>
                {
                    ISelectQuery userSelect = SQLProviderFactory.GetSelectQuery();
                    userSelect.SelectList = new List<Select>()
                    {
                        new Select() { SelectOperand = (ClussPro.Base.Data.Operand.Field)"U.Username" }
                    };
                    userSelect.Table = new Table("security", "User", "U");
                    userSelect.WhereCondition = new Condition()
                    {
                        Left = (ClussPro.Base.Data.Operand.Field)"U.UserID",
                        ConditionType = Condition.ConditionTypes.Equal,
                        Right = (ClussPro.Base.Data.Operand.Field)$"{alias}.UserID"
                    };

                    return new ClussPro.Base.Data.Operand.SubQuery(userSelect);
                };
            }
        }

        public static IEnumerable<string> GetPermissionFieldNames()
        {
            yield return nameof(ManageEmails);
            yield return nameof(ManageOfficials);
            yield return nameof(ManageAccounts);
            yield return nameof(ManageTaxes);
            yield return nameof(ManageInvoices);
            yield return nameof(IssueWireTransfers);
            yield return nameof(CanConfigureInterest);
            yield return nameof(ManageLaws);
            yield return nameof(ManagePurchaseOrders);

            foreach(string fleetField in fleet.FleetSecurity.SecurityFields)
            {
                yield return $"{nameof(FleetSecurity)}.{fleetField}";
            }
        }
    }
}
