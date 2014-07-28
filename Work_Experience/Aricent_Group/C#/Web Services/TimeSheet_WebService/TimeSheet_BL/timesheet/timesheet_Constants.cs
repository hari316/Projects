using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace TimeSheet_BL.timesheet
{
    public static class timesheet_Constants
    {

        public static readonly int[] dbErrorCodes = new int[] { 1017, 12154 ,12203 ,12500 ,12545 ,12560 };

        private static Dictionary<string, string> _cnfgErrMessages;
        private static Dictionary<string, string> _email;

        public const string mail_BodyFormat = "An Exception Occured At Date/Time : {0}" + "\n" + "Transaction ID : {1}" + "\n" + "Method Name : {2}" + "\n" + "Exception Description :" + "\n" + "{3}";
        public const string ts_StatusErrFormat = "Your Transaction on Timesheet failed for {0}, week: {1} as the status of the record has already been changed/moved to another level of workflow.";

        public const string Success = "SUCCESS";
        public const string Error = "ERROR";
        public const string Unknown = "UNKNOWN";
        public const string Approve = "APPROVE";
        public const string Reject = "REJECT";
        public const string Pending = "PENDING";       
        public const string ApproveChar = "A";
        public const string RejectChar = "R";
        public const string PendingChar = "P";
        public const string A_PRFlagChar = "S";
        public const string R_PRFlagChar = "U";
        public const string TransIdNull = "Entered Transaction ID is NULL/Empty";
        public const string TransIdInvalid = "Entered Transaction ID is Invalid";


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

        public static Dictionary<string, string> LoadTSConfigCustomSection(NameValueCollection secValue)
        {

            Dictionary<string, string> ConfigSection = new Dictionary<string, string>();

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
