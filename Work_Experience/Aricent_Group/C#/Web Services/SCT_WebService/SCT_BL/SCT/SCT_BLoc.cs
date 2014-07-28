using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCT_BL.SCT
{
    public class SCT_BLoc
    {

        public BLocHeader SCT_header;
        public BLoc[] SCT_Details;

        public SCT_BLoc()
        {
            // TODO: Add constructor logic here  
            SCT_header = new BLocHeader();
        }

        public SCT_BLoc(int Details_count)
        {
            // TODO: Add constructor logic here  
            SCT_header = new BLocHeader();
            SCT_Details = new BLoc[Details_count];  
        }

        public class BLocHeader
        {
            public BLocHeader()
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
 
        public class BLoc
        {

            public BLoc()
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
