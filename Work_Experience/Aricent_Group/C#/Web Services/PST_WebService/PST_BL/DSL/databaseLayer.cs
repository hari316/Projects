using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PST_BL
{
    public class databaseLayer
    {
        public databaseLayer()
        {
            // TODO: Add constructor logic here 
        }

        private static string _ConnectionDetails = string.Empty;
        private static string _myConnectionDetails = string.Empty; 

        public string connectionInfo
        {
            get
            {
                return _ConnectionDetails;
            }
            set
            {
                _ConnectionDetails = value;
            }
        }

        public string myConnectionInfo
        {
            get
            {
                return _myConnectionDetails;
            }
            set
            {
                _myConnectionDetails = value;
            }
        }
    }
}
