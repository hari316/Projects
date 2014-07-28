using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace PST_BL
{
    public static class PST_Constants
    {

        public static readonly int[] dbstatusFlags = new int[] { 1017, 12154, 12203, 12500, 12545, 12560 };

        private static Dictionary<string, string> _cnfgErrMessages;
        private static Dictionary<string, string> _email;

        public const string mail_BodyFormat = "An Exception Occured At Date/Time : {0}" + "\n" + "Number : {1}"  + "\n" + "Method Name : {2}"  + "\n" + "Exception Description :" + "\n" + "{3}";
        public const string preqErrFormat = "Your Transaction {0} failed  as the status of the record has already been changed/moved to another level of workflow.";

        public const string Success = "SUCCESS";
        public const string Error = "ERROR";
        public const string Invalid = "Invalid Input Parameters";

        public const int headerTbNo = 0;
        public const int itemTbNo = 1;
 
        public const string emptyRecords = "NO RECORDS FOUND";

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
