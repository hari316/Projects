using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCT_BL.SCT
{
    public class SCT_Horizontal
    {

        public HorizonHeader SCT_header;
        public Horizontal[] SCT_Details;

        public SCT_Horizontal()
        {
            // TODO: Add constructor logic here  
            SCT_header = new HorizonHeader();
        }

        public SCT_Horizontal(int Details_count)
        {
            // TODO: Add constructor logic here  
            SCT_header = new HorizonHeader();
            SCT_Details = new Horizontal[Details_count];  
        }

        public class HorizonHeader
        {
            public HorizonHeader()
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
 
        public class Horizontal
        {

            public Horizontal()
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
