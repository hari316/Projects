using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCT_BL.SCT
{
    public class SCT_PSA
    {

        public PSAheader SCT_header;
        public PSA[] SCT_Details;

        public SCT_PSA()
        {
            // TODO: Add constructor logic here  
            SCT_header = new PSAheader();
        }

        public SCT_PSA(int Details_count)
        {
            // TODO: Add constructor logic here  
            SCT_header = new PSAheader();
            SCT_Details = new PSA[Details_count];  
        }

        public class PSAheader
        {
            public PSAheader()
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
 
        public class PSA
        {

            public PSA()
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
