﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCT_BL.SCT
{
    public class SCT_BU
    {

        public BUheader SCT_header;
        public BU[] SCT_Details;

        public SCT_BU()
        {
            // TODO: Add constructor logic here  
            SCT_header = new BUheader();
        }

        public SCT_BU(int Details_count)
        {
            // TODO: Add constructor logic here  
            SCT_header = new BUheader();
            SCT_Details = new BU[Details_count];  
        }

        public class BUheader
        {
            public BUheader()
            {
                // TODO: Add constructor logic here 
            }

            int _StatusFlag = 0;
            string _StatusMsg = string.Empty;


            public int StatusFlag
            {

                get { return _StatusFlag; }

                set { _StatusFlag = value; }

            }

            public string StatusMsg
            {

                get { return _StatusMsg; }

                set { _StatusMsg = value; }

            }

        }
 
        public class BU
        {

            public BU()
            {
                // TODO: Add constructor logic here 
            }

            string _ID = string.Empty;
            string _Value = string.Empty;

            public string ID
            {

                get { return _ID; }

                set { _ID = value; }

            }
            public string Label
            {

                get { return _Value; }

                set { _Value = value; }

            }
            
        }


    }
 
}
