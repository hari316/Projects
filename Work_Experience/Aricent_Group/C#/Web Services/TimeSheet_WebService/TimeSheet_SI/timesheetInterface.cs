using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using TimeSheet_BL.timesheet;

namespace TimeSheet_SI.timesheet
{
    public class timesheetInterface
    {
        public timesheetInterface()
        {
            // TODO: Add constructor logic here 
        }

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(timesheetInterface));

        /// <summary>
        /// This Method is a interface to gettimesheetDetails function
        /// </summary>
        /// <param name="TransactionID_SI"></param>
        /// <returns></returns>
        /// <history>
        ///     Hari haran      02/05/2012      created
        /// </history>
        
        public timesheetEntity getTimesheetDetails_SI(string TransactionID_SI)
        {
            try
            {
                logger.Debug("timesheetInterface:getTimesheetDetails_SI() called"); 
                logger.Debug("Transaction ID value : " + TransactionID_SI);

                timesheet_BAL getTS_BAL = new timesheet_BAL();

                logger.Debug("Control Flow : Method - getTimesheetDetails_SI Stop");

                return (getTS_BAL.getTimesheetDetails_BAL(TransactionID_SI));
            }
            catch (OracleException dbEx)
            {
                logger.Error("Database Exception  At timesheetInterface - getTimesheetDetails_SI  : " + dbEx.Message.ToString());
                logger.Error("timesheetInterface:getTimesheetDetails_SI() returning error");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception  At timesheetInterface - getTimesheetDetails_SI  : " + ex.Message.ToString());
                logger.Error("timesheetInterface:getTimesheetDetails_SI() returning error");

                throw ex;
            }
        }


        /// <summary>
        /// This Method is a interface to updateTimesheetEntry fuction
        /// </summary>
        /// <param name="entry_SI"></param>
        /// <returns></returns>
        /// /// <history>
        ///     Hari haran      02/05/2012      created
        /// </history>

        public ts_UpdateOutputEntity updateTimesheetEntry_SI(ts_UpdateInputEntity[] entry_SI)
        {
            try
            {
                logger.Debug("timesheetInterface:updateTimesheetEntry_SI() called"); 
                logger.Debug("Method : updateTimesheetEntry Transaction ID value : " + entry_SI[0].TransactionID.ToString());

                timesheet_BAL updateTS_BAL = new timesheet_BAL();

                logger.Debug("Control Flow : Method - updateTimesheetEntry_SI Stop");

                return (updateTS_BAL.updateTimesheetEntry_BAL(entry_SI));
            }
            catch (OracleException dbEx)
            {
                logger.Error("Database Exception  At timesheetInterface - updateTimesheetEntry_SI  : " + dbEx.Message.ToString());
                logger.Error("timesheetInterface:updateTimesheetEntry_SI() returning error");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Error("Exception  At timesheetInterface - updateTimesheetEntry_SI  : " + ex.Message.ToString());
                logger.Error("timesheetInterface:updateTimesheetEntry_SI() returning error");

                throw ex;
            }
        }
    }
}
