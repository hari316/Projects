using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PST_BL
{
    public class PST_OutputEntity
    {

        public headerDetails PST_headerDetails;
        public empDetails[] PST_child_empDetails;    

        public PST_OutputEntity()
        {
            // TODO: Add constructor logic here  
            PST_headerDetails = new headerDetails();
        }

        public PST_OutputEntity(int empDetails_count)
        {
            // TODO: Add constructor logic here  
             PST_headerDetails = new headerDetails();
             PST_child_empDetails = new empDetails[empDetails_count];            
        }

        public class headerDetails
        {
            public headerDetails()
            {
                // TODO: Add constructor logic here 
            }
       
            int _totalRecords = 0;
            int _index = 0;
            int _statusFlag = 0;
            string _statusMsg = string.Empty;

            public int totalRecords
            {

                get { return _totalRecords; }

                set { _totalRecords = value; }

            }

            public int index
            {

                get { return _index; }

                set { _index = value; }

            }

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

        public class empDetails
        {
            
            public empDetails()
            {
                // TODO: Add constructor logic here 
            }

            int _empNo = 0;
            string _name = string.Empty;
            string _grp = string.Empty;
            string _grade = string.Empty;
            string _design = string.Empty;
            string _email = string.Empty;
            string _ext = string.Empty;
            string _seat = string.Empty;
            string _loc = string.Empty;
            string _plot = string.Empty;
            string _voip = string.Empty;
            string _etPerArea = string.Empty;
            string _personalArea = string.Empty;
            string _mobileCC = string.Empty;
            string _mobileSC = string.Empty;
            string _mobileNO = string.Empty;
            string _empDesig = string.Empty;
            string _offPHCC = string.Empty;
            string _offPHNO = string.Empty;
            string _offPHSC = string.Empty;
            string _image = string.Empty;
            string _jobDesc = string.Empty;
            string _sbu = string.Empty;
            string _bu = string.Empty;
            string _imageStr = string.Empty;

            public int empNo
            {

                get { return _empNo; }

                set { _empNo = value; }

            }

            public string name
            {

                get { return _name; }

                set { _name = value; }

            }
            public string grp
            {

                get { return _grp; }

                set { _grp = value; }

            }

            public string grade
            {

                get { return _grade; }

                set { _grade = value; }

            }

            public string design
            {

                get { return _design; }

                set { _design = value; }

            }

            public string email
            {

                get { return _email; }

                set { _email = value; }

            }

            public string ext
            {

                get { return _ext; }

                set { _ext = value; }

            }



            public string seat
            {

                get { return _seat; }

                set { _seat = value; }

            }

            public string loc
            {

                get { return _loc; }

                set { _loc = value; }

            }

            public string plot
            {

                get { return _plot; }

                set { _plot = value; }

            }

            public string voip
            {

                get { return _voip; }

                set { _voip = value; }

            }
            public string etPerArea
            {

                get { return _etPerArea; }

                set { _etPerArea = value; }

            }
            public string personalArea
            {

                get { return _personalArea; }

                set { _personalArea = value; }

            }
            public string mobileCC
            {

                get { return _mobileCC; }

                set { _mobileCC = value; }

            }
            public string mobileSC
            {

                get { return _mobileSC; }

                set { _mobileSC = value; }

            }
            public string mobileNO
            {

                get { return _mobileNO; }

                set { _mobileNO = value; }

            }
            public string empDesig
            {

                get { return _empDesig; }

                set { _empDesig = value; }

            }
            public string offPHCC
            {

                get { return _offPHCC; }

                set { _offPHCC = value; }

            }
            public string offPHNO
            {

                get { return _offPHNO; }

                set { _offPHNO = value; }

            }

            public string offPHSC
            {

                get { return _offPHSC; }

                set { _offPHSC = value; }

            }

            public string image
            {

                get { return _image; }

                set { _image = value; }

            }
            public string jobDesc
            {

                get { return _jobDesc; }

                set { _jobDesc = value; }

            }
            public string sbu
            {

                get { return _sbu; }

                set { _sbu = value; }

            }
            public string bu
            {

                get { return _bu; }

                set { _bu = value; }

            }
            public string imageStr
            {

                get { return _imageStr; }

                set { _imageStr = value; }

            }


        }

    }
 
}
