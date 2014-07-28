using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRT_BL
{
    public class TRT_ChargeCodes
    {
        public header TRT_header;
        public ChargeCode[] TRT_child;    

        public TRT_ChargeCodes()
        {
            // TODO: Add constructor logic here  
            TRT_header = new header();
        }

        public TRT_ChargeCodes(int count)
        {
            // TODO: Add constructor logic here  
            TRT_header = new header();
            TRT_child = new ChargeCode[count];            
        }

        public class header
        {
            public header()
            {
                // TODO: Add constructor logic here 
            }

            int _statusFlag = 0;
            string _statusMsg = string.Empty;  

            public int statusFlag
            {

                get { return _statusFlag; }

                set { _statusFlag = value; }

            }

            public string statusMsg
            {

                get { return _statusMsg; }

                set { _statusMsg = value; }

            }


        }

        public class ChargeCode
        {

            public ChargeCode()
            {
                // TODO: Add constructor logic here 
            }

            string _ID = string.Empty;

            public string ID
            {

                get { return _ID; }

                set { _ID = value; }

            }      

        }
 
    }
}
