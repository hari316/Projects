using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace utilities
{
    public class myCustomException : ApplicationException
    {

        public Int32 ErrorCode;

        public myCustomException(Int32 errorcode, string errorsource, Exception innerException)
            : base(errorsource, innerException)
        {
            this.ErrorCode = errorcode;
            this.Source = errorsource;
        }

        public myCustomException(Int32 errorcode, string message)
            : base(message)
        {
            this.ErrorCode = errorcode;
        }

        public myCustomException(Int32 errorcode, Exception innerException)
            : base("", innerException)
        {
            this.ErrorCode = errorcode;
        }

    }
}
