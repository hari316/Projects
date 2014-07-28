using System;
using System.Collections.Generic;

namespace ICOMSProvisioningService
{
    public class SyncRequest
    {
        private String _customerID;
        private String _customerStatus;
        private String _icomsMsgFormat;

        public String CustomerId
        {
            set
            {
               this._customerID = value;
            }

            get
            {
                return this._customerID;
            }
        }
        public String CustomerStatus
        {
            set
            {
                this._customerStatus = value;
            }

            get
            {
                return this._customerStatus;
            }
        }
        public String ICOMSMsgFormat
        {
            set
            {
                this._icomsMsgFormat = value;
            }

            get
            {
                return this._icomsMsgFormat;
            }
        }

    }

    public class SubscriberSyncRequest : SyncRequest
    {
        private int _creditLimit;

        public int creditLimit
        {
            set
            {
                this._creditLimit = value;
            }

            get
            {
                return this._creditLimit;
            }
        }

    }

    public class EquipmentSyncRequest : SyncRequest
    {

        private String _macAddress;
        private String _smartCardId;
        private List<string> _offeringId;

        public string macAddress
        {
            set
            {
                this._macAddress = value;
            }

            get
            {
                return this._macAddress;
            }
        }

        public string smartCardId
        {
            set
            {
                this._smartCardId = value;
            }

            get
            {
                return this._smartCardId;
            }
        }

        public List<string> offeringId
        {
            set
            {
                this._offeringId = value;
            }

            get
            {
                return this._offeringId;
            }
        }

    }
}

