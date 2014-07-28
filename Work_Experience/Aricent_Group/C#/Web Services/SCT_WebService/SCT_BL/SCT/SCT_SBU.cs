using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCT_BL.SCT
{
    public class SCT_SBU
    {

        public SBUheader SCT_header;
        public SBU[] SCT_Details;

        public SCT_SBU()
        {
            // TODO: Add constructor logic here  
            SCT_header = new SBUheader();
        }

        public SCT_SBU(int Details_count)
        {
            // TODO: Add constructor logic here  
            SCT_header = new SBUheader();
            SCT_Details = new SBU[Details_count];  
        }

        public class SBUheader
        {
            public SBUheader()
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
 
        public class SBU
        {

            public SBU()
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
