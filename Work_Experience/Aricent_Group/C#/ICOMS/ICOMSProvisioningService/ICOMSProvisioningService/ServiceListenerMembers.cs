using System;

namespace ICOMSProvisioningService
{
  public class ServiceListenerMembers
    {
        private string _lstSiteID;
        private string _lstSiteTokenName; 
        private string _lstIPAddress;
        private string _lstIPPort;
        private string _lstIPCustIDFlag;
        private string _lstDeviceIdFormat;
        private string _lstNativeFormat4cFlag;

        public string siteid
        {
            get { return this._lstSiteID; }
            set { this._lstSiteID = value; }
        }

        public string siteIdTokenName
        {
            get { return this._lstSiteTokenName; }
            set { this._lstSiteTokenName = value; }
        }

        public string listenerAddress
        {
            get { return this._lstIPAddress; }
            set { this._lstIPAddress = value; }
        }

         public string listenerPort
        {
            get { return this._lstIPPort; }
            set { this._lstIPPort = value; }
        }

         public string CustomerIdFlag
        {
            get { return this._lstIPCustIDFlag; }
            set { this._lstIPCustIDFlag = value; }
        }


         public string DeviceIdFormat
         {
             get { return this._lstDeviceIdFormat; }
             set { this._lstDeviceIdFormat = value; }
         }

         public string NativeFormat4cFlag
         {
             get { return this._lstNativeFormat4cFlag; }
             set { this._lstNativeFormat4cFlag = value; }
         }

    }
}
