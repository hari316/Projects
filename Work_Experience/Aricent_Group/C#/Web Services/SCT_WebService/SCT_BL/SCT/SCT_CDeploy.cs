using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCT_BL.SCT
{
    public class SCT_CDeploy
    {

        public CDeployHeader SCT_header;
        public CDeploy[] SCT_Details;

        public SCT_CDeploy()
        {
            // TODO: Add constructor logic here  
            SCT_header = new CDeployHeader();
        }

        public SCT_CDeploy(int Details_count)
        {
            // TODO: Add constructor logic here  
            SCT_header = new CDeployHeader();
            SCT_Details = new CDeploy[Details_count];  
        }

        public class CDeployHeader
        {
            public CDeployHeader()
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
 
        public class CDeploy
        {

            public CDeploy()
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
