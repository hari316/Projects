﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCT_BL.SCT
{
    public class databaseLayer
    {
        public databaseLayer()
        {
            // TODO: Add constructor logic here 
        }

        private static string _ConnectionDetails = string.Empty;        

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
    }
}
