﻿using System;
using System.Collections.Generic;
using ClussPro.ObjectBasedFramework;
using ClussPro.ObjectBasedFramework.Schema.Attributes;
using WebModels.company;
using WebModels.gov;

namespace WebModels.fleet
{
    [Table("E1752409-BB3C-4F6E-B15B-C4D4E181B5D1")]
    public class LeaseRequest : DataObject
    {
        protected LeaseRequest() : base() { }

        private long? _leaseRequestID;
        [Field("497E1D2A-3853-497D-8CA5-6BAB6D45BBF9")]
        public long? LeaseRequestID
        {
            get { CheckGet(); return _leaseRequestID; }
            set { CheckSet(); _leaseRequestID = value; }
        }

        private long? _companyIDRequester;
        [Field("8A312A17-0360-458F-9525-932BDCD72217")]
        public long? CompanyIDRequester
        {
            get { CheckGet(); return _companyIDRequester; }
            set { CheckSet(); _companyIDRequester = value; }
        }

        private Company _companyRequester = null;
        [Relationship("13D988EA-C90C-44E2-8ABD-0D6B9FA05A6F", ForeignKeyField = nameof(CompanyIDRequester))]
        public Company CompanyRequester
        {
            get { CheckGet(); return _companyRequester; }
        }

        private long? _locationIDChargeTo;
        [Field("EE279BCF-DED0-461C-8058-074623081880")]
        public long? LocationIDChargeTo
        {
            get { CheckGet(); return _locationIDChargeTo; }
            set { CheckSet(); _locationIDChargeTo = value; }
        }

        private Location _locationChargeTo;
        [Relationship("B22FCC41-7920-461B-9758-68A2CAD1F2F4", ForeignKeyField = nameof(LocationIDChargeTo))]
        public Location LocationChargeTo
        {
            get { CheckGet(); return _locationChargeTo; }
        }

        private long? _governmentIDRequester;
        [Field("30FEB2B8-777A-43F4-8340-CD84EE673E72")]
        public long? GovernmentIDRequester
        {
            get { CheckGet(); return _governmentIDRequester; }
            set { CheckSet(); _governmentIDRequester = value; }
        }

        private Government _governmentRequester = null;
        [Relationship("5EB1A103-22F7-485A-9F45-9C1B4C3C6F27", ForeignKeyField = nameof(GovernmentIDRequester))]
        public Government GovernmentRequester
        {
            get { CheckGet(); return _governmentRequester; }
        }

        public enum LeaseTypes
        {
            Locomotive,
            Railcar
        }

        private LeaseTypes _leaseType;
        [Field("AF95EBA6-6AF7-4F5B-BB47-60FDD24ADB55")]
        public LeaseTypes LeaseType
        {
            get { CheckGet(); return _leaseType; }
            set { CheckSet(); _leaseType = value; }
        }

        private RailcarModel.Types _railcarType;
        [Field("DCC3CD84-F00C-4DB2-8E6C-520CEB277BE2")]
        public RailcarModel.Types RailcarType
        {
            get { CheckGet(); return _railcarType; }
            set { CheckSet(); _railcarType = value; }
        }

        private string _deliveryLocation;
        [Field("364944B8-A401-4138-9292-88EBBFD884FB", DataSize = 50)]
        public string DeliveryLocation
        {
            get { CheckGet(); return _deliveryLocation; }
            set { CheckSet(); _deliveryLocation = value; }
        }

        private string _purpose;
        [Field("F2A2A1EB-2CA3-42FB-81D9-989363695819", DataSize = -1)]
        public string Purpose
        {
            get { CheckGet(); return _purpose; }
            set { CheckSet(); _purpose = value; }
        }

        private DateTime? _bidEndTime;
        [Field("13D767CA-4013-43D0-9DB2-59EBCB980CB4", DataSize = 7)]
        public DateTime? BidEndTime
        {
            get { CheckGet(); return _bidEndTime; }
            set { CheckSet(); _bidEndTime = value; }
        }

        #region Relationships
        #region fleet
        private List<LeaseBid> _leaseBids = new List<LeaseBid>();
        [RelationshipList("8E8DF7AB-5CA7-4B14-86E9-45342E08466E", nameof(LeaseBid.LeaseRequestID), AutoDeleteReferences = true)]
        public IReadOnlyCollection<LeaseBid> LeaseBids
        {
            get { CheckGet(); return _leaseBids; }
        }
        #endregion
        #endregion
    }
}