using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRT_BL
{
    public class TRT_OutputEntity
    {
        public TRT_OutputEntity()
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
 
}
