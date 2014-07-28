using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace iSmart_BL.iSmart
{
    public static class iSmart_Constants
    {

        public static readonly int[] dbErrorCodes = new int[] { 10050, 10051, 10058, 10060, 10061, 10064, 11001, 18456 };

        private static Dictionary<string, string> _cnfgErrMessages;
        private static Dictionary<string, string> _email;

        public const string mail_BodyFormat = "An Exception Occured At Date/Time : {0}" + "\n" + "PReq Number : {1}"  + "\n" + "Method Name : {2}"  + "\n" + "Exception Description :" + "\n" + "{3}";
        public const string preqErrFormat = "Your Transaction on Purchase request #{0} failed  as the status of the record has already been changed/moved to another level of workflow.";

        public const string Success = "SUCCESS";
        public const string Error = "ERROR";

        public const int headerTbNo = 0;
        public const int itemTbNo = 1;
        public const int historyTbNo = 2;
        public const int purchaseGpTbNo = 3;
        public const int sendForClrTbNo = 4;

        public const string PReqNoNull = "Entered PReqNo is NULL/Empty";
        public const string PReqNoInvalid = "Entered PReqNo is Invalid";

        public static Dictionary<string, string> cnfgErrMessages
        {
            get
            {
                return _cnfgErrMessages;
            }
            set
            {
                _cnfgErrMessages = value;
            }

        }

        public static Dictionary<string, string> Email_Dic
        {
            get
            {
                return _email;
            }
            set
            {
                 _email = value;
            }

        }


        public static Dictionary<string,string> LoadISConfigCustomSection(NameValueCollection secValue)
        {

            Dictionary<string,string> ConfigSection=new Dictionary<string,string>();

            string key = string.Empty;
            string name = string.Empty;
            if ((secValue) != null)
            {
                for (int i = 0; i < secValue.Keys.Count; i++)
                {
                    key = secValue.Keys[i];
                    name = secValue.GetValues(key)[0];
                    ConfigSection.Add(key, name);
                }
            }

            return ConfigSection;

        }

    }
}
