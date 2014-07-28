using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace person.BL
{
    public class DataBaseLayer
    {

       private static string _ConnectionDetails=string.Empty;


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

    /*
        public string getConnectionStringDetails()
        {
            return _ConnectionDetails;
        }

        public void setConnectionStringDetails(string connStr)
        {
            _ConnectionDetails = connStr;           
        }
     
     */
    }
}
