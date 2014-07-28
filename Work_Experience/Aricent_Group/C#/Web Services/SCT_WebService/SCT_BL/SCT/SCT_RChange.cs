using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCT_BL.SCT
{
    public class SCT_RChange
    {

        public RChangeHeader SCT_header;
        public RChange[] SCT_Details;

        public SCT_RChange()
        {
            // TODO: Add constructor logic here  
            SCT_header = new RChangeHeader();
        }

        public SCT_RChange(int Details_count)
        {
            // TODO: Add constructor logic here  
            SCT_header = new RChangeHeader();
            SCT_Details = new RChange[Details_count];  
        }

        public class RChangeHeader
        {
            public RChangeHeader()
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
 
        public class RChange
        {

            public RChange()
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
