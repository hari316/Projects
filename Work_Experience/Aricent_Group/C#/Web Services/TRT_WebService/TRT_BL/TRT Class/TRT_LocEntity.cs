using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRT_BL
{
    public class TRT_LocEntity
    {
        public headerDetail TRT_header;
        public Plot[] TRT_child_plot;
        public Purpose[] TRT_child_purpose;

        public TRT_LocEntity()
        {
            // TODO: Add constructor logic here  
            TRT_header = new headerDetail();
        }

        public TRT_LocEntity(int plotCount,int purposeCount)
        {
            // TODO: Add constructor logic here  
            TRT_header = new headerDetail();
            TRT_child_plot = new Plot[plotCount];
            TRT_child_purpose = new Purpose[purposeCount];
        }

        public class headerDetail
        {
            public headerDetail()
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

        public class Plot
        {

            public Plot()
            {
                // TODO: Add constructor logic here 
            }

            string _ID = string.Empty;
            string _Label = string.Empty;

            public string ID
            {

                get { return _ID; }

                set { _ID = value; }

            }

            public string Label
            {

                get { return _Label; }

                set { _Label = value; }

            }     

        }

        public class Purpose
        {

            public Purpose()
            {
                // TODO: Add constructor logic here 
            }

            string _ID = string.Empty;
            string _Label = string.Empty;

            public string ID
            {

                get { return _ID; }

                set { _ID = value; }

            }

            public string Label
            {

                get { return _Label; }

                set { _Label = value; }

            }   

        }
    }
}
