using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PST_BL
{
    public class PST_InputEntity
    {

        public PST_InputEntity()
        {
            // TODO: Add constructor logic here 
        }

        int _index = 0;
        int _empNo = 0;     
        string _name = string.Empty;
        string _sbu = string.Empty;
        string _bu = string.Empty;
        string _jobDesc = string.Empty;
        string _location = string.Empty;


        public int index
        {

            get { return _index; }

            set { _index = value; }

        }
        
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

        public string jobDesc
        {

            get { return _jobDesc; }

            set { _jobDesc = value; }

        }

        public string location
        {

            get { return _location; }

            set { _location = value; }

        }

    }

}
