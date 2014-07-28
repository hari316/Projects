using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Data;

namespace TRT_BL
{
    public static class TRT_Constants
    {

        public static readonly int[] dbstatusFlags = new int[] { 1017, 12154, 12203, 12500, 12545, 12560 };

        private static Dictionary<string, string> _cnfgErrMessages;
        private static Dictionary<string, string> _email;

        public const string mail_BodyFormat = "<center><h4> Taxi Request </h4> <table border=\"1\">"+
                                               "<tr><th>S/No</th><th>Label</th><th>Value</th></tr>"+
                                               "<tr><td>1</td><td>Employee ID</td><td>{0}</td></tr></tr>"+
                                               "<tr><td>2</td><td>Employee Name</td><td>{1}</td></tr></tr>" +
                                               "<tr><td>3</td><td>Organization Unit</td><td>{2}</td></tr></tr>" +
                                               "<tr><td>4</td><td>Approver</td><td>{3}</td></tr></tr>" +
                                               "<tr><td>5</td><td>Charge Code</td><td>{4}</td></tr></tr>" +
                                               "<tr><td>6</td><td>Purpose</td><td>{5}</td></tr></tr>" +
                                               "<tr><td>7</td><td>Type of car</td><td>{6}</td></tr></tr>" +
                                               "<tr><td>8</td><td>Required on Date</td><td>{7}</td></tr></tr>" +
                                               "<tr><td>9</td><td>From Place</td><td>{8}</td></tr></tr>" +
                                               "<tr><td>10</td><td>To Place</td><td>{9}</td></tr></tr>" +
                                               "<tr><td>11</td><td>Plot</td><td>{10}</td></tr></tr>" +
                                               "<tr><td>12</td><td>Flight Details</td><td>{11}</td></tr></tr>" +
                                               "<tr><td>13</td><td>Reporting Time (HH:MM 24hr. format)</td><td>{12}</td></tr></tr>" +
                                               "<tr><td>14</td><td>Duration</td><td>{13}</td></tr></tr>" +
                                               "<tr><td>15</td><td>Landmark</td><td>{14}</td></tr></tr>" +
                                               "<tr><td>16</td><td>Telephone/Mobile</td><td>{15}</td></tr></tr>" +
                                               "<tr><td>17</td><td>Comments</td><td>{16}</td></tr></tr>" +
                                               "<tr><td>18</td><td>Request Submit Time</td><td>{17}</td></tr></tr>" +                                       
                                               "</table></center>";

        public const string preqErrFormat = "Your Transaction {0} failed  as the status of the record has already been changed/moved to another level of workflow.";

        public const string Success = "SUCCESS";
        public const string Error = "ERROR";
        public const string Invalid = "Invalid Input Parameters";
        public const string InvalidEmpID = "Invalid EmpID";

        public const string AuthSuccess = "Authentification Successful";
        public const string AuthFailure = "Authentification Failed";

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
