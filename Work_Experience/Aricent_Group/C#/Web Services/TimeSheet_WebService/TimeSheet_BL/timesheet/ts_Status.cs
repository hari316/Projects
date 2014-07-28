using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace TimeSheet_BL.timesheet
{
    public class ts_Status
    {
        public ts_Status()
        {
            // TODO: Add constructor logic here 
        }

        string _EmpName = string.Empty;
        string _Status = string.Empty;
        DateTime _TimesheetWeek = DateTime.MinValue;

        public string EmpName
        {

            get { return _EmpName; }

            set { _EmpName = value; }

        }

        public string Status
        {

            get { return _Status; }

            set { _Status = value; }

        }

        public DateTime TimesheetWeek
        {

            get { return _TimesheetWeek; }

            set { _TimesheetWeek = value; }

        }
        
    }
    
}
