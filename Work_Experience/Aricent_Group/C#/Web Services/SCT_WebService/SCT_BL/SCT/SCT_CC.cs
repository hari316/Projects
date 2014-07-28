using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCT_BL.SCT
{
    public class SCT_CC
    {

        public CCheader SCT_header;
        public CC[] SCT_Details;

        public SCT_CC()
        {
            // TODO: Add constructor logic here  
            SCT_header = new CCheader();
        }

        public SCT_CC(int Details_count)
        {
            // TODO: Add constructor logic here  
            SCT_header = new CCheader();
            SCT_Details = new CC[Details_count];  
        }

        public class CCheader
        {
            public CCheader()
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
 
        public class CC
        {

            public CC()
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
