using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Globalization;
using utilities;


namespace TimeSheet_BL.timesheet
{
    public class timesheet_BAL
    {
        public timesheet_BAL()
        {
            // TODO: Add constructor logic here 
        }

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(timesheet_BAL));
        private static readonly int count = 0;
        private ts_Status StatusRes = new ts_Status();

        public timesheetEntity getTimesheetDetails_BAL(string TransactionID_BAL)
        {
            try
            {
                logger.Debug("timesheet_BAL:getTimesheetDetails_BAL() called");    
                
                logger.Debug("Transaction ID value : " + TransactionID_BAL);

                if ( string.IsNullOrEmpty(TransactionID_BAL) )
                {
                    timesheetEntity ts_value = new timesheetEntity(count);
                    ts_value.TS_headerDetails.ErrorCode = 21;
                    ts_value.TS_headerDetails.ErrorMessage = timesheet_Constants.TransIdNull;

                    logger.Debug("Method getTimesheetDetails_BAL : ErrorCode = " + ts_value.TS_headerDetails.ErrorCode.ToString());
                    logger.Debug("Method getTimesheetDetails_BAL : ErrorMessage = " + ts_value.TS_headerDetails.ErrorMessage);
                    logger.Error("Method : getTimesheetDetails_BAL validation failed");

                    return ts_value;
                }

                timesheet_DAL getTS_DAL = new timesheet_DAL();
                return (getTS_DAL.getTimesheetDetails_DAL(TransactionID_BAL));
            }
            catch (OracleException dbEx)
            {
                logger.Error("Exception  At BAL - getTimesheetDetails_BAL  : " + dbEx.Message.ToString());
                logger.Error("timesheet_BAL:getTimesheetDetails_BAL() returning error");
                throw dbEx;
            }
            catch(Exception ex) 
            {
                logger.Error("Exception  At BAL - getTimesheetDetails_BAL  : " + ex.Message.ToString());
                logger.Error("timesheet_BAL:getTimesheetDetails_BAL() returning error");
                throw ex;
            }
        }


        public ts_UpdateOutputEntity updateTimesheetEntry_BAL(ts_UpdateInputEntity[] entry_BAL)
        {
            try
            {
                logger.Debug("timesheet_BAL:updateTimesheetEntry_BAL() called");               
                logger.Debug(" updateTimesheetEntry Transaction ID value : " + entry_BAL[0].TransactionID.ToString());

                ts_UpdateOutputEntity errRes = new ts_UpdateOutputEntity();
                errRes.StatusFlag = 1;
                errRes.Message = timesheet_Constants.Error;
                int validate_tsParamFlag = 0;
 
                validate_tsParamFlag = validate_tsParam(entry_BAL);

                logger.Debug("Timesheet Input parameter validation flag value : " + validate_tsParamFlag.ToString());
 
                if (validate_tsParamFlag == 1)
                {
                    logger.Debug("Error in input parameter values");
                    logger.Debug("ErrorCode = " + errRes.StatusFlag.ToString());
                    logger.Debug("ErrorMessage = " + errRes.Message);                    
                    return errRes;
                }
                else
                {
                    //ts_Status TimesheetStatus = string.Empty;
                    string validate_tsStatusFlag = string.Empty;

                    timesheet_DAL updateTS_DAL = new timesheet_DAL();
                    StatusRes = updateTS_DAL.getTimesheetStatus(entry_BAL[count].TransactionID);
                    if (StatusRes.Status == timesheet_Constants.Unknown)
                    {
                        errRes.StatusFlag = 1;
                        errRes.Message = timesheet_Constants.TransIdInvalid;

                        logger.Debug("ErrorCode = " + errRes.StatusFlag.ToString());
                        logger.Debug("ErrorMessage = " + errRes.Message);
                        logger.Error("Method : updateTimesheetEntry_BAL Stop");

                        return errRes;
                    }
                    validate_tsStatusFlag = validate_tsStatus(StatusRes.Status);
                    if (validate_tsStatusFlag.Equals(timesheet_Constants.Pending))
                    {
                        return (updateTS_DAL.updateTimesheetEntry_DAL(entry_BAL));
                    }
                    else
                    {
                        errRes.StatusFlag = 1;
                        errRes.Message = validate_tsStatusFlag;

                        logger.Debug("ErrorCode = " + errRes.StatusFlag.ToString());
                        logger.Debug("ErrorMessage = " + errRes.Message);
                        logger.Error("Method : updateTimesheetEntry_BAL validation failed");
                        return errRes;
                    }
                }
            }
            catch (OracleException dbEx)
            {
                logger.Error("Exception  At BAL - updateTimesheetEntry_BAL  : " + dbEx.Message.ToString());
                logger.Error("timesheet_BAL:updateTimesheetEntry_BAL() returning error");                
                throw dbEx;
            }
            catch
            {
                logger.Error("timesheet_BAL:updateTimesheetEntry_BAL() returning error");
                throw;
            }            

        }


        int validate_tsParam(ts_UpdateInputEntity[] entry_BAL)
        {     
            logger.Debug("timesheet_BAL:validate_tsParam() called");  
            switch (entry_BAL[count].UpdateFlag.ToUpper())
            {
                case timesheet_Constants.Approve:
                    entry_BAL[count].UpdateFlag = timesheet_Constants.ApproveChar;
                    //entry_BAL[count].PRFlag = timesheet_Constants.A_PRFlagChar;
                    break;
                case timesheet_Constants.Reject:
                    entry_BAL[count].UpdateFlag = timesheet_Constants.RejectChar;
                    //entry_BAL[count].PRFlag = timesheet_Constants.R_PRFlagChar;
                    break;
                default:
                    logger.Error("Invalid Update Flag value Expected : (Approve/Reject)");                    
                    return 1;
                //break;
            }

            foreach (ts_UpdateInputEntity tsEntry_value in entry_BAL)
            {
                if ( string.IsNullOrEmpty(tsEntry_value.ChargeCode) ||  string.IsNullOrEmpty(tsEntry_value.TransactionID) )
                {
                    logger.Error("ChargeCode/TransactionID had NULL reference");
                    logger.Debug("Method : validate_tsParam Stop");
                    return 1;
                }
            }

            logger.Debug("Method : validate_tsParam Stop");
            return 0;
        }

        string validate_tsStatus(string Status)
        {
            logger.Debug("timesheet_BAL:validate_tsStatus() called");  
            string statusMsg = string.Empty;
            switch (Status.ToUpper())
            {
                case timesheet_Constants.ApproveChar :
                    logger.Warn("Status - Timesheet Already Approved");
                    logger.Debug("Method : validate_tsParam Stop");

                    //statusMsg = timesheet_Constants.cnfgErrMessages["103"];
                    statusMsg = string.Format(timesheet_Constants.ts_StatusErrFormat, StatusRes.EmpName, StatusRes.TimesheetWeek.ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("en-US")));
                    return statusMsg;
                case timesheet_Constants.RejectChar:
                    logger.Warn("Status - Timesheet Already rejected");
                    logger.Debug("Method : validate_tsParam Stop");

                    //statusMsg = timesheet_Constants.cnfgErrMessages["104"];
                    statusMsg = string.Format(timesheet_Constants.ts_StatusErrFormat, StatusRes.EmpName, StatusRes.TimesheetWeek.ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("en-US")));
                    return statusMsg;
                case timesheet_Constants.PendingChar:
                    logger.Debug("Status - Timesheet Status Pending");
                    logger.Debug("Method : validate_tsParam Stop");

                    statusMsg = timesheet_Constants.Pending;
                    return statusMsg;
                default:
                    logger.Debug("Timesheet validation Status : " + statusMsg);
                    logger.Debug("Method : validate_tsParam Stop");

                    return statusMsg;
            }
        }
    }
}
