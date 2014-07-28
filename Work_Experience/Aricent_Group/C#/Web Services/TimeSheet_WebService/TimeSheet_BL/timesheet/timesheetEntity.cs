using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeSheet_BL.timesheet
{
    public class timesheetEntity
    {

        public headerDetails TS_headerDetails;
        public childDetails[] TS_childDetails;

        public timesheetEntity()
        {
            // TODO: Add constructor logic here  
        }

        public timesheetEntity( int count)
        {
            // TODO: Add constructor logic here  
             TS_headerDetails = new headerDetails();
             TS_childDetails = new childDetails[count];
        }

        public class headerDetails
        {
            public headerDetails()
            {
                // TODO: Add constructor logic here 
            }

            string _TransactionID = string.Empty;
            int _EmpManagerID = 0;
            int _EmployeeNo = 0;
            string _EmployeeName = string.Empty;
            DateTime _TimesheetWeek = DateTime.MinValue;
            DateTime _SubmittedDate = DateTime.MinValue;
            string _ReporteeComments = string.Empty;
            int _ErrorCode = 0;
            string _ErrorMessage = string.Empty;

            public string TransactionID
            {

                get { return _TransactionID; }

                set { _TransactionID = value; }

            }

            public int EmpManagerID
            {

                get { return _EmpManagerID; }

                set { _EmpManagerID = value; }

            }

            public int EmployeeNo
            {

                get { return _EmployeeNo; }

                set { _EmployeeNo = value; }

            }

            public string EmployeeName
            {

                get { return _EmployeeName; }

                set { _EmployeeName = value; }

            }

            public DateTime TimesheetWeek
            {

                get { return _TimesheetWeek.Date; }

                set { _TimesheetWeek = value; }

            }

            public DateTime SubmittedDate
            {

                get { return _SubmittedDate; }

                set { _SubmittedDate = value; }

            } 

            public string ReporteeComments
            {

                get { return _ReporteeComments; }

                set { _ReporteeComments = value; }

            }

            public int ErrorCode
            {

                get { return _ErrorCode; }

                set { _ErrorCode = value; }

            }

            public string ErrorMessage
            {

                get { return _ErrorMessage; }

                set { _ErrorMessage = value; }

            }



        }

        public class childDetails
        {
            public childDetails()
            {
                // TODO: Add constructor logic here 
            }

            string _ChargeCode = string.Empty;
            string _ProjectName = string.Empty;
            int _Billable = 0;
            string _ActivityName = string.Empty;
            double _TotalHours = 0.0;
            double _Mon = 0.0;
            double _Tue = 0.0;
            double _Wed = 0.0;
            double _Thur = 0.0;
            double _Fri = 0.0;
            double _Sat = 0.0;
            double _Sun = 0.0;

            public string ChargeCode
            {

                get { return _ChargeCode; }

                set { _ChargeCode = value; }

            }

            public string ProjectName
            {

                get { return _ProjectName; }

                set { _ProjectName = value; }

            }

            public int Billable
            {

                get { return _Billable; }

                set { _Billable = value; }

            }

            public string ActivityName
            {

                get { return _ActivityName; }

                set { _ActivityName = value; }

            }

            public double TotalHours
            {

                get { return _TotalHours; }

                set { _TotalHours = value; }

            }

            public double Mon
            {

                get { return _Mon; }

                set { _Mon = value; }

            }

            public double Tue
            {

                get { return _Tue; }

                set { _Tue = value; }

            }

            public double Wed
            {

                get { return _Wed; }

                set { _Wed = value; }

            }

            public double Thur
            {

                get { return _Thur; }

                set { _Thur = value; }

            }

            public double Fri
            {

                get { return _Fri; }

                set { _Fri = value; }
            }

            public double Sat
            {

                get { return _Sat; }

                set { _Sat = value; }

            }

            public double Sun
            {

                get { return _Sun; }

                set { _Sun = value; }

            }
           
        }

    }
 
}
