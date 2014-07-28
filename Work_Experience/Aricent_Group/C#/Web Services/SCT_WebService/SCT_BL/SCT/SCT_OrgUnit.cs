using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCT_BL.SCT
{
    public class SCT_ORGUnit
    {

        public ORGheader SCT_header;
        public ORGUnit[] SCT_Details;

        public SCT_ORGUnit()
        {
            // TODO: Add constructor logic here  
            SCT_header = new ORGheader();
        }

        public SCT_ORGUnit(int Details_count)
        {
            // TODO: Add constructor logic here  
            SCT_header = new ORGheader();
            SCT_Details = new ORGUnit[Details_count];  
        }

        public class ORGheader
        {
            public ORGheader()
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
 
        public class ORGUnit
        {

            public ORGUnit()
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
