using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using utilities;
using System.Data;

namespace TimeSheet_BL.timesheet
{
    public class timesheet_DAL
    {
        public timesheet_DAL()
        {
            // TODO: Add constructor logic here 
        }

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(timesheet_DAL));
        queryLog QueryLog = new queryLog();

        /// <summary>
        /// This function fetches the timesheet Details from the database based on Transaction ID
        /// </summary>
        /// <param name="TransactionID"></param>
        /// <returns>
        /// <paramref name="timesheetEntity"/>
        /// </returns>
        /// <history>
        ///     Hari haran      02/05/2012      created
        /// </history>

        public timesheetEntity getTimesheetDetails_DAL(string TransactionID)
        {           
            logger.Debug("timesheet_DAL:getTimesheetDetails_DAL() called");   
            logger.Debug("Transaction ID value : " + TransactionID);

            databaseLayer dbConStr = new databaseLayer();
            string connStr = dbConStr.connectionInfo;

            //logger.Debug("Connection string : " + connStr);

            OracleConnection conn = new OracleConnection(connStr);
            //OracleTransaction tsTrans = null;

            logger.Debug("Connetion to the database established");

            try
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                    logger.Debug("Connection Status opened ");
                }

                //tsTrans = conn.BeginTransaction();

                /*
                string headerQuery = " SELECT TTM_ID,TE_T_APPROVER,TTM_EMPNO,NAME,TTM_WEEK,TTM_SUBDATE,TTM_US_REM" +
                                     " FROM T_TS_EMPLOYEE T,T_EMPLOYEE E, T_TS_MAS_SUB  H" +
                                     " WHERE  T.EMPNO = E.TE_EMPNO AND T.EMPNO = H.TTM_EMPNO AND H.TTM_ID = :TransID";
                 */

                string headerQuery = "SUP_GETTIMESHEETMAS"; 

                OracleCommand header_cmd = new OracleCommand(headerQuery, conn);
                header_cmd.CommandType = CommandType.StoredProcedure;
                header_cmd.Parameters.AddWithValue("TRANSID", TransactionID);
                header_cmd.Parameters.Add("RESULTSET", OracleType.Cursor).Direction = ParameterDirection.Output;
                //header_cmd.Transaction = tsTrans;

                logger.Debug("Header_cmd Query parameters initialised");

                QueryLog.CmdInfo(header_cmd);


                OracleDataReader reader = header_cmd.ExecuteReader();

                logger.Debug("Header_cmd Executed by ExecuteReader()");

                //string childQuery = " SELECT TTD_PROJECT as ChargeCode,p1.pm_desc as ProjectName,b1.trad_rm_per as Billable,TTD_ACTIVITY,t2.ta_desc as ActivityName,";
                //childQuery = childQuery + " MON,TUE,WED,THU,FRI,SAT,SUN,MON+TUE+WED+THU+FRI+SAT+SUN Total_hrs";
                //childQuery = childQuery + " FROM ( SELECT TTD_PROJECT,TTD_ACTIVITY,";
                //childQuery = childQuery + " max(decode(DAY,'Monday   ',ttd_hours,0))Mon,";
                //childQuery = childQuery + " max(decode(DAY,'Tuesday  ',ttd_hours,0))Tue,";
                //childQuery = childQuery + " max(decode(DAY,'Wednesday',ttd_hours,0))Wed,";
                //childQuery = childQuery + " max(decode(DAY,'Thursday ',ttd_hours,0))Thu,";
                //childQuery = childQuery + " max(decode(DAY,'Friday   ',ttd_hours,0))Fri,";
                //childQuery = childQuery + " max(decode(DAY,'Saturday ',ttd_hours,0))Sat,";
                //childQuery = childQuery + " max(decode(DAY,'Sunday   ',ttd_hours,0))Sun ";
                //childQuery = childQuery + " FROM ( SELECT ttd_ttmid, TO_CHAR(ttd_date, 'Day') Day ,ttd_hours ,TTD_PROJECT,TTD_ACTIVITY ";
                //childQuery = childQuery + " FROM T_TS_DET_SUB WHERE ttd_ttmid = :TransID)";
                //childQuery = childQuery + " GROUP BY(TTD_PROJECT,TTD_ACTIVITY,ttd_ttmid)";
                //childQuery = childQuery + " ) t1,project_master p1,t_activity t2,";
                //childQuery = childQuery + " (SELECT  prjId,trad_rm_per,rm.trad_empno";
                //childQuery = childQuery + " FROM(SELECT t.ttd_ttmid as Transactn,TTM_EMPNO,min(t.ttd_date) as mindate, t.ttd_project as prjId from T_TS_DET_SUB t, T_TS_MAS_SUB u";
                //childQuery = childQuery + " WHERE u.ttm_id = t.ttd_ttmid and t.ttd_ttmid = :TransID  group by t.ttd_ttmid,TTM_EMPNO, t.ttd_project";
                //childQuery = childQuery + " )tl,ts_rm_allocation_data rm";
                //childQuery = childQuery + " WHERE  tl.mindate >= rm.trad_alloc_from and tl.mindate <= rm.trad_alloc_to and TTM_EMPNO = rm.trad_empno and";
                //childQuery = childQuery + " tl.prjId = rm.trad_charge_code order by 1) b1";
                //childQuery = childQuery + " WHERE t1.ttd_project=p1.pm_id and t1.ttd_activity=t2.ta_id and t1.TTD_PROJECT = b1.prjId";

                string childQuery = "SUP_GetTimesheetDetails";

                OracleCommand child_cmd = new OracleCommand(childQuery, conn);
                child_cmd.CommandType = CommandType.StoredProcedure;

                logger.Debug("Child_cmd Query parameters initialised");

                child_cmd.Parameters.AddWithValue("TTMID", TransactionID);
                child_cmd.Parameters.Add("RESULTSET", OracleType.Cursor).Direction = ParameterDirection.Output;

                QueryLog.CmdInfo(child_cmd);

                OracleDataAdapter da = new OracleDataAdapter();
                da.SelectCommand = child_cmd;
                DataSet dSet = new DataSet();
                da.Fill(dSet);

                logger.Debug("Child_cmd excuted by OracleDataAdapter()");

                int count = dSet.Tables[0].Rows.Count;
                timesheetEntity ts_value = new timesheetEntity(count);
               
                int cntHeader=0;
                if (reader.HasRows)
                {
                    reader.Read();
                    ts_value.TS_headerDetails.TransactionID = reader["TTM_ID"].ToString();
                    ts_value.TS_headerDetails.EmpManagerID = Convert.ToInt32(reader["TE_T_APPROVER"]);
                    ts_value.TS_headerDetails.EmployeeNo = Convert.ToInt32(reader["TTM_EMPNO"]);
                    ts_value.TS_headerDetails.EmployeeName = reader["NAME"].ToString();
                    ts_value.TS_headerDetails.TimesheetWeek = Convert.ToDateTime(reader["TTM_WEEK"]);
                    ts_value.TS_headerDetails.SubmittedDate = Convert.ToDateTime(reader["TTM_SUBDATE"]);
                    ts_value.TS_headerDetails.ReporteeComments = Convert.ToString(reader["TTM_US_REM"]);
                    cntHeader = cntHeader + 1;
                }
                else
                {
                    //Error handling : For Invaild Transaction ID
                    ts_value.TS_headerDetails.ErrorCode = 31;
                    ts_value.TS_headerDetails.ErrorMessage = timesheet_Constants.TransIdInvalid;

                    logger.Debug("ErrorCode = " + ts_value.TS_headerDetails.ErrorCode.ToString());
                    logger.Debug("ErrorMessage = " + ts_value.TS_headerDetails.ErrorMessage);

                    return ts_value;
                }

                logger.Info(string.Format(" Number of returned records for Header Details : {0}", cntHeader));
                logger.Info(string.Format(" Number of returned records for Child Details : {0}", count));

                int objCounter = 0;
                foreach (DataRow dr in dSet.Tables[0].Rows)
                {
                    timesheetEntity.childDetails ts_ch = new timesheetEntity.childDetails();
                    ts_ch.ChargeCode = dr["ChargeCode"].ToString();
                    ts_ch.ProjectName = dr["ProjectName"].ToString();
                    ts_ch.Billable = Convert.ToInt32(dr["Billable"]);
                    ts_ch.ActivityName = dr["ActivityName"].ToString();
                    ts_ch.Mon = Convert.ToDouble(dr["MON"]);
                    ts_ch.Tue = Convert.ToDouble(dr["TUE"]);
                    ts_ch.Wed = Convert.ToDouble(dr["WED"]);
                    ts_ch.Thur = Convert.ToDouble(dr["THU"]);
                    ts_ch.Fri = Convert.ToDouble(dr["FRI"]);
                    ts_ch.Sat = Convert.ToDouble(dr["SAT"]);
                    ts_ch.Sun = Convert.ToDouble(dr["SUN"]);
                    ts_ch.TotalHours = Convert.ToDouble(dr["Total_hrs"]);

                    logger.Debug("Row Count = " + objCounter.ToString());

                    ts_value.TS_childDetails[objCounter++] = ts_ch;
                }

                logger.Info("In Success case : ErrorCode = " + ts_value.TS_headerDetails.ErrorCode.ToString());
                logger.Info("In Success case : ErrorMessage = " + ts_value.TS_headerDetails.ErrorMessage);
                //tsTrans.Commit();
                return ts_value;

            }
            catch (OracleException dbEx)
            {
                logger.Error("Exception Occured At timesheet_DAL - getTimesheetDetails_DAL  : ");
                logger.Error("Exception Code : " + dbEx.ErrorCode.ToString());
                logger.Error("Exception Description : " + dbEx.Message.ToString());
                logger.Error("timesheet_DAL:getTimesheetDetails_DAL() returning error");

                //tsTrans.Rollback();
                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Error("Exception Occured At timesheet_DAL - getTimesheetDetails_DAL  : " + ex.Message.ToString());
                logger.Error("timesheet_DAL:getTimesheetDetails_DAL() returning error");

                //tsTrans.Rollback();
                //throw new myCustomException(31, ex.Message);
                throw ex;
            }
            finally
            {
                logger.Debug("Connection Status Closed ");
                conn.Dispose();
            }
        }

        /// <summary>
        /// This function fetch Timesheet status from the Database based on TransactionID 
        /// </summary>
        /// <param name="TransactionID"></param>
        /// <returns>
        /// <paramref name="status"/>
        /// </returns>
        /// <history>
        ///     Hari haran      02/05/2012      created
        /// </history>

        public ts_Status getTimesheetStatus(string TransactionID)
        {

            try
            {

                logger.Debug("timesheet_DAL:getTimesheetStatus() called");  

                ts_Status statusEntity = new ts_Status();
                databaseLayer dbConStr = new databaseLayer();
                string connStr = dbConStr.connectionInfo;

                //logger.Debug("Connection string : " + connStr);

                OracleConnection conn = new OracleConnection(connStr);

                logger.Debug("Connetion to the database established");

                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                    logger.Debug("Connection Status opened ");
                }

                string validateQuery = "SELECT NAME ,TTM_STATUS as Status ,TTM_WEEK as Week from T_TS_EMPLOYEE T ,t_ts_mas_sub M where T.EMPNO = M.TTM_EMPNO  and TTM_ID = :TransID";
                OracleCommand validate_cmd = new OracleCommand(validateQuery, conn);
                validate_cmd.Parameters.AddWithValue("TransID", TransactionID);
                QueryLog.CmdInfo(validate_cmd);
                OracleDataReader reader = validate_cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    statusEntity.EmpName = reader["NAME"].ToString();
                    statusEntity.Status = reader["Status"].ToString();
                    statusEntity.TimesheetWeek = Convert.ToDateTime(reader["Week"]);
                }
                else
                {
                    statusEntity.Status = timesheet_Constants.Unknown;
                }

                logger.DebugFormat("Timesheet Status : {0}", statusEntity.Status);
                logger.Debug("Method : getTimesheetStatus Stop");

                return statusEntity;
            }
            catch (OracleException dbEx)
            {
                logger.Error("Exception Occured At timesheet_DAL - getTimesheetStatus  : " + dbEx.Message.ToString());
                logger.Error("timesheet_DAL:getTimesheetStatus() returning error");

                throw dbEx;
            }
            catch (Exception Ex)
            {
                logger.Error("Exception Occured At timesheet_DAL - getTimesheetStatus  : " + Ex.Message.ToString());
                logger.Error("timesheet_DAL:getTimesheetStatus() returning error");
                throw Ex;
            }
        }

        /// <summary>
        /// This Function will Update Timesheet details into the Database
        /// </summary>
        /// <param name="ts_entry_DAL"></param>
        /// <returns>
        /// <paramref name="result"/>
        /// </returns>
        /// /// <history>
        ///     Hari haran      02/05/2012      created
        /// </history>
        
        public ts_UpdateOutputEntity updateTimesheetEntry_DAL(ts_UpdateInputEntity[] ts_entry_DAL)
        {

            logger.Debug("timesheet_DAL:updateTimesheetEntry_DAL() called");  
            ts_UpdateOutputEntity result = new ts_UpdateOutputEntity();

            databaseLayer dbConStr = new databaseLayer();
            string connStr = dbConStr.connectionInfo;
            //logger.Debug("Connection string : " + connStr);
            OracleConnection conn = new OracleConnection(connStr);

            logger.Debug("Connetion to the database established");

            OracleTransaction Trans = null;
            int header_flag = 0;
            int child_flag = 0;
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
                logger.Debug("Connection Status opened ");
            }
            Trans = conn.BeginTransaction();

            try
            {
                //Approve=A,S, AcceptRejectRule=R,U

                string strPRFlag = string.Empty;

                switch (ts_entry_DAL[0].UpdateFlag)
                {
                    case timesheet_Constants.ApproveChar:
                        strPRFlag = timesheet_Constants.A_PRFlagChar;
                        break;
                    case timesheet_Constants.RejectChar:
                        strPRFlag = timesheet_Constants.R_PRFlagChar;
                        break;
                    default:
                        logger.Error("Invalid Update Flag value Expected : (A/R)");
                        logger.Debug("Method : updateTimesheetEntry_DAL Stop");

                        result.StatusFlag = 1;
                        result.Message = timesheet_Constants.Error;
                        return result;
                }
                
                string HeaderQuery = "UPDATE t_ts_mas_sub SET TTM_STATUS = :Status, TTM_PR_FLAG = :PRFlag, TTM_APP_DATE = :ApprDate, TTM_AP_REM = :Mcomments  WHERE TTM_ID = :TransID";
                OracleCommand header_cmd = new OracleCommand(HeaderQuery, conn);
                header_cmd.Transaction = Trans;
                header_cmd.Parameters.AddWithValue("TransID", ts_entry_DAL[0].TransactionID);                                        
                header_cmd.Parameters.AddWithValue("Status", ts_entry_DAL[0].UpdateFlag);
                //header_cmd.Parameters.AddWithValue("PRFlag", ts_entry_DAL[0].PRFlag);
                header_cmd.Parameters.AddWithValue("PRFlag", strPRFlag);
                header_cmd.Parameters.AddWithValue("ApprDate", System.DateTime.Now);
                header_cmd.Parameters.AddWithValue("Mcomments", ts_entry_DAL[0].MComments);

                QueryLog.CmdInfo(header_cmd);

                header_flag = header_cmd.ExecuteNonQuery();

                int objCount = 0;
                OracleCommand child_cmd = new OracleCommand();
                child_cmd.Connection = conn;
                child_cmd.Transaction = Trans;

                //string ChildQuery = " INSERT into TS_MAS_BILL_PER (TMBP_TTMID,TMBP_BILLPER,TMBP_DATE,TMBP_CHGCODE)" +
                //                    " VALUES (:TransID,:Billable,TO_DATE('04/26/2012 12:00:00','MM/DD/YYYY HH24:MI:SS'),:ChargeCode)";
                string ChildQuery = " INSERT into TS_MAS_BILL_PER (TMBP_TTMID,TMBP_BILLPER,TMBP_DATE,TMBP_CHGCODE)" +
                                        " VALUES (:TransID,:Billable,:BDate,:ChargeCode)";

                child_cmd.Parameters.Add("TransID",OracleType.VarChar);
                child_cmd.Parameters.Add("Billable",OracleType.Int32);
                child_cmd.Parameters.Add("BDate",OracleType.DateTime);
                child_cmd.Parameters.Add("ChargeCode",OracleType.VarChar);
                
                foreach (ts_UpdateInputEntity tsEntry_value in ts_entry_DAL)
                {

                    child_cmd.CommandText = ChildQuery;
                    logger.Debug("Child_cmd Query parameters initialised");
                    child_cmd.Parameters["TransID"].Value = tsEntry_value.TransactionID ;
                    child_cmd.Parameters["Billable"].Value = tsEntry_value.Billable;
                    child_cmd.Parameters["BDate"].Value = System.DateTime.Now;
                    child_cmd.Parameters["ChargeCode"].Value = tsEntry_value.ChargeCode;
                        
                    QueryLog.CmdInfo(child_cmd);

                    child_flag = child_cmd.ExecuteNonQuery();
                    if (child_flag == 0)
                        break;
                    objCount++;
                }

                logger.Info(string.Format(" Number of records updated : {0}", header_flag));
                logger.Info(string.Format(" Number of records inserted : {0}", objCount.ToString()));                

                if (header_flag == 1 && child_flag == 1)
                {
                    Trans.Commit();
                    result.StatusFlag = 0;
                    result.Message = timesheet_Constants.Success;
                    logger.Debug("Transaction Committed");
                    logger.Debug("Operation : Update & insert executed successfully");
                }
                else
                {
                    result.StatusFlag = 1;
                    result.Message = timesheet_Constants.Error;

                    logger.Debug("Tranasction Rollback Executed");

                    if (header_flag == 0)
                    {
                        logger.Error("Operation : Update Error - Invalid parameter values / Invalid Transction ID");
                    }
                    else
                    {
                        logger.Error("Operation : Insert Error - Invalid parameter values");
                    }
                    Trans.Rollback();
                }

                return result;
            }
            catch (OracleException dbEx)
            {
                logger.Error("Exception Occured At timesheet_DAL - updateTimesheetEntry_DAL  : ");
                logger.Error("Exception Code : " + dbEx.ErrorCode.ToString());
                logger.Error("Exception Description : " + dbEx.Message.ToString());
                logger.Error("timesheet_DAL:updateTimesheetEntry_DAL() returning error");
                try
                {
                    logger.Debug("Transaction Rollback Executed");
                    Trans.Rollback();
                }
                catch (Exception ex2)
                {
                    logger.Error("Transaction Rollback Failed : " + ex2.Message.ToString());
                    //throw ex2;
                }
                
                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Error("Exception Occured At timesheet_DAL - updateTimesheetEntry_DAL  : " + ex.Message.ToString());
                logger.Error("timesheet_DAL:updateTimesheetEntry_DAL() returning error");

                try
                {
                    logger.Debug("Transaction Rollback Executed");
                    Trans.Rollback();
                }
                catch (Exception ex2)
                {
                    logger.Error("Transaction Rollback Failed : " + ex2.Message.ToString());
                    //throw ex2;
                }

                //throw new myCustomException(32, ex.Message);
                throw ex;
            }
            finally
            {
                logger.Debug("Connection Status Closed ");
                
                conn.Dispose();
            }
        }
    }
}
