using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCT_BL.SCT
{
    public class SCT_AuthUser
    {
        public SCT_AuthUser()
        {
            // TODO: Add constructor logic here 
        }

        int _StatusFlag = 0;
        string _Message = string.Empty;
        string _Name = string.Empty;

        public int StatusFlag
        {

            get { return _StatusFlag; }

            set { _StatusFlag = value; }

        }

        public string Message
        {

            get { return _Message; }

            set { _Message = value; }

        }

        public string Name
        {

            get { return _Name; }

            set { _Name = value; }

        }
    }
}
