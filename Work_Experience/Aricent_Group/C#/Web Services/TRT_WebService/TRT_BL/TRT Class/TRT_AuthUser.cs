using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRT_BL
{
    public class TRT_AuthUser
    {
        public TRT_AuthUser()
        {
            // TODO: Add constructor logic here 
        }

        int _StatusFlag = 0;
        string _Message = string.Empty;
        string _Name = string.Empty;
        string _ChargeCode = string.Empty;
        string _LocId = string.Empty;

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

        public string ChargeCode
        {

            get { return _ChargeCode; }

            set { _ChargeCode = value; }

        }

        public string LocId
        {

            get { return _LocId; }

            set { _LocId = value; }

        }
    }
}
