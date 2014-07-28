using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCT_BL.SCT
{
    public class SCT_PA
    {

        public PAheader SCT_header;
        public PA[] SCT_Details;

        public SCT_PA()
        {
            // TODO: Add constructor logic here  
            SCT_header = new PAheader();
        }

        public SCT_PA(int Details_count)
        {
            // TODO: Add constructor logic here  
            SCT_header = new PAheader();
            SCT_Details = new PA[Details_count];  
        }

        public class PAheader
        {
            public PAheader()
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
 
        public class PA
        {

            public PA()
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
