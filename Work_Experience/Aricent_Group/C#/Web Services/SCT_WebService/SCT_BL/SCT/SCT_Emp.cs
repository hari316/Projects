using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCT_BL.SCT
{
    public class SCT_Emp
    {

        public headerDetails SCT_headerDetails;
        public childDetails[] SCT_childDetails;

        public SCT_Emp()
        {
            // TODO: Add constructor logic here  
            SCT_headerDetails = new headerDetails();
        }

        public SCT_Emp(int itemDetails_count)
        {
            // TODO: Add constructor logic here  
            SCT_headerDetails = new headerDetails();
            SCT_childDetails = new childDetails[itemDetails_count];  
        }

        public class headerDetails
        {
            public headerDetails()
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

        public class childDetails
        {
            
            public childDetails()
            {
                // TODO: Add constructor logic here 
            }

            string _EmpID = string.Empty;
            string _EmpName = string.Empty;
            string _Grade = string.Empty;

            public string EmpID
            {

                get { return _EmpID; }

                set { _EmpID = value; }

            }
            public string EmpName
            {

                get { return _EmpName; }

                set { _EmpName = value; }

            }
            public string Grade
            {

                get { return _Grade; }

                set { _Grade = value; }

            }
            
        }


    }
 
}
