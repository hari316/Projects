using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCT_BL.SCT
{
    public class SCT_UpdateOutputEntity
    {
        public SCT_UpdateOutputEntity()
        {
            // TODO: Add constructor logic here 
        }

        int _StatusFlag = 0;
        string _Message = string.Empty;

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
    }
}
