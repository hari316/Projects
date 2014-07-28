using System;

namespace ICOMSProvisioningService
{
    public class LoadBalancerMembers
    {
        private string _LBIPAddress;
        private string _LBIPPort;


        public string listenerAddress
        {
            get { return this._LBIPAddress; }
            set { this._LBIPAddress = value; }
        }

         public string listenerPort
        {
            get { return this._LBIPPort; }
            set { this._LBIPPort = value; }
        }

    }
}
