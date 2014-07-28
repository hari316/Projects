using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using utilities;
using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;


namespace PST_BL
{
    public class PST_DAL
    {
        public PST_DAL()
        {
            // TODO: Add constructor logic here 
        }

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(PST_DAL));

        queryLog QueryLog = new queryLog();

        /// <summary>
        /// This function fetches the PST Details from the database based on Search criteria
        /// </summary>
        /// <param name="PST_InputEntity"></param>
        /// <returns>
        /// <paramref name="PST_OutputEntity"/>
        /// </returns>
        /// <history>
        ///     Hari haran      07/05/2012      created
        /// </history>

        public PST_OutputEntity employeeSearch_DAL(PST_InputEntity searchCriteria_Dal,int from ,int to)
        {
            logger.Info("Method : employeeSearch_DAL Start");
            logger.DebugFormat("Employee Name : {0} ", searchCriteria_Dal.name);

            databaseLayer dbConStr = new databaseLayer();
            string connStr = dbConStr.connectionInfo;
            string myConnStr = dbConStr.myConnectionInfo;

            logger.Debug("Connection string : " + connStr);

            OracleConnection conn = new OracleConnection(connStr);
            SqlConnection myConn = new SqlConnection(myConnStr);

            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
                logger.Debug("Connection Status opened ");
            }

            logger.Debug("Connection for Image Database : " + myConnStr);

            if (myConn.State == System.Data.ConnectionState.Closed)
            {
                myConn.Open();
                logger.Debug("myConnection Status for Image Database opened ");
            }

            logger.Info("Connetion to the database established");

            try
            {
              
                string queryString = "Select * from (" +
					"SELECT ROW_NUMBER() OVER (ORDER BY Name) AS PA, EMPNO, NAME, GRP, GRADE, DESIG,EMAIL,SEAT,EXT,LOC" +
					",PLOT,VOIP,v.ET_PERAREA et_perArea,PERSONAL_AREA,MOBILECC,MOBILESC,MOBILENO,EMPDESIG," +
					"OFFPHCC,OFFPHSC,OFFPHNO, jb_desc JobDesc,sbu_description sbu,bu_description bu" +
					" FROM v_inhouse_new v,EMP_TRANSACTION,SBU_MASTER,BU_MASTER,JOB_MASTER,EMP_GROUP_IDENTIFY" +
					" WHERE ";

                if (searchCriteria_Dal.name != string.Empty)
                    queryString += " lower(NAME) LIKE '%" + searchCriteria_Dal.name.ToLower() + "%' AND";
                if (searchCriteria_Dal.bu != string.Empty)
                    queryString += " lower(et_bu)='" + searchCriteria_Dal.bu.ToLower() + "' AND";
                if (searchCriteria_Dal.empNo != 0)
                    queryString += " et_empno=" + searchCriteria_Dal.empNo + " AND";
                if (searchCriteria_Dal.jobDesc != string.Empty)
                    queryString += " lower(jb_id)='" + searchCriteria_Dal.jobDesc.ToLower() + "' AND";
                if (searchCriteria_Dal.location != string.Empty)
                    queryString += " lower(loc)='" + searchCriteria_Dal.location.ToLower() + "' AND";
                if (searchCriteria_Dal.sbu != string.Empty)
                    queryString += " lower(et_sbu)='" + searchCriteria_Dal.sbu.ToLower() + "' AND";

				queryString += " et_empno=empno AND et_sbu=sbu_code AND et_bu=bu_code AND jb_id = eg_job AND eg_empno=empno ORDER BY 'NAME' ASC";
				queryString += ")";

                 

                OracleCommand cmd_PSTDetails = new OracleCommand();
                cmd_PSTDetails.Connection = conn;
                cmd_PSTDetails.CommandText = queryString;
                //cmd_PSTDetails.CommandType = CommandType.StoredProcedure;
                ////cmd_PSTDetails.Parameters.AddWithValue("TRANSID", TransactionID);

                //logger.Info("cmd_PSTDetails Query parameters initialised");

                QueryLog.CmdInfo(cmd_PSTDetails);

                OracleDataAdapter da_PSTDetails = new OracleDataAdapter();
                da_PSTDetails.SelectCommand = cmd_PSTDetails;
                DataSet dSet_PSTDetails = new DataSet();
                da_PSTDetails.Fill(dSet_PSTDetails);       

                logger.Info("cmd_PSTDetails excuted by OracleDataAdapter()");
               
                int empDetails_count = dSet_PSTDetails.Tables[PST_Constants.headerTbNo].Rows.Count;

                logger.DebugFormat("No. of records found based on the search criteria :empDetails table {0}", empDetails_count.ToString());


                if (to > empDetails_count)
                {
                    to = empDetails_count;
                }

                int totalObjects = to - from;

                if (totalObjects < 0)
                {
                    totalObjects = 0;
                }

                PST_OutputEntity PST_Details = new PST_OutputEntity(totalObjects);
                PST_Details.PST_headerDetails.totalRecords = empDetails_count;

                logger.DebugFormat("Object Count : {0}", totalObjects);

                if (totalObjects > 0)
                {
                    //DataRow header_dr = dSet_PSTDetails.Tables[PST_Constants.headerTbNo].Rows[0];
                    PST_Details.PST_headerDetails.statusFlag = 0;
                    PST_Details.PST_headerDetails.statusMsg = "success";
                    PST_Details.PST_headerDetails.index = to;
                    int objCounter_item = 0;
                    
                    //DataRow img_dr = new DataRow();
                    //DataRow img_dr1 = dSet_PSTDetails.Tables[PST_Constants.headerTbNo].Rows[0];

                    //DataRow[] img_dr = img_tb.Select();
                    //string[] strings = new string[img_dr.Length];
                    //for (int i = 0; i < strings.Length; i++)
                    //    strings[i] = img_dr[i]["EMPNO"].ToString();

                    //StringBuilder sb = new StringBuilder();
                    //sb.Append('(');

                    //foreach (string i in strings)
                    //{
                    //    sb.Append(i).Append(',');
                    //}

                    //// remove final ,
                    //sb.Length -= 1;
                    //sb.Append(')');


                    //***************************************************************************

                    //DataTable img_tb = dSet_PSTDetails.Tables[PST_Constants.headerTbNo].DefaultView.ToTable(false, "EMPNO");
                    //var empIDs = img_tb.Rows.Cast<DataRow>()
                    //.Select(row => row["EMPNO"].ToString())
                    //.ToArray();
                    //string inValue = "(" + String.Join(",", empIDs) + ")";             
                    //string imgQuery = "SELECT * from EmployeeImage WHERE EmpId IN" + inValue;
                    string imgQuery = "SELECT EmpImg from EmployeeImage WHERE EmpId = @empId";
                    SqlCommand img_cmd = new SqlCommand();
                    img_cmd.Connection = myConn;
                    img_cmd.CommandText = imgQuery;
                    img_cmd.Parameters.Add("@empId", SqlDbType.VarChar);
                    //SqlDataAdapter da_imgDetails = new SqlDataAdapter();
                    //da_imgDetails.SelectCommand = img_cmd;
                    //DataSet dSet_image = new DataSet();
                    //da_imgDetails.Fill(dSet_image);
                    //int a = dSet_image.Tables[0].Rows.Count;

                    //***************************************************************************
                    

                    //foreach (DataRow item_dr in dSet_PSTDetails.Tables[PST_Constants.headerTbNo].Rows)
                    for (int i = from; i < to; i++)
                    {
                        PST_OutputEntity.empDetails PST_Emp = new PST_OutputEntity.empDetails();
                        DataRow item_dr = dSet_PSTDetails.Tables[PST_Constants.headerTbNo].Rows[i];

                        PST_Emp.empNo = Convert.ToInt32(item_dr["EMPNO"]);
                        PST_Emp.name = item_dr["Name"].ToString();
                        PST_Emp.personalArea = item_dr["PERSONAL_AREA"].ToString();
                        PST_Emp.grp = item_dr["GRP"].ToString();
                        PST_Emp.grade =  item_dr["GRADE"].ToString();
                        PST_Emp.design = item_dr["DESIG"].ToString();
                        PST_Emp.email = item_dr["EMAIL"].ToString();
                        PST_Emp.seat = item_dr["SEAT"].ToString();
                        PST_Emp.ext = item_dr["EXT"].ToString();
                        PST_Emp.loc = item_dr["LOC"].ToString();
                        PST_Emp.plot = item_dr["PLOT"].ToString();
                        PST_Emp.mobileCC = item_dr["MOBILECC"].ToString();
                        PST_Emp.mobileSC = item_dr["MOBILESC"].ToString();
                        PST_Emp.mobileNO = item_dr["MOBILENO"].ToString();
                        PST_Emp.empDesig = item_dr["EMPDESIG"].ToString();
                        PST_Emp.voip = item_dr["VOIP"].ToString();
                        PST_Emp.etPerArea = item_dr["et_perArea"].ToString();
                        PST_Emp.offPHCC = item_dr["OFFPHCC"].ToString();
                        PST_Emp.offPHSC = item_dr["OFFPHSC"].ToString();
                        PST_Emp.offPHNO = item_dr["OFFPHNO"].ToString();
                        PST_Emp.jobDesc = item_dr["JobDesc"].ToString();
                        PST_Emp.sbu = item_dr["sbu"].ToString();
                        PST_Emp.bu = item_dr["bu"].ToString();

                        img_cmd.Parameters["@empId"].Value = PST_Emp.empNo.ToString();

                        QueryLog.CmdInfo(img_cmd);

                        PST_Emp.imageStr = img_cmd.ExecuteScalar().ToString();
                        //PST_Emp.imageStr = dSet_image.Tables[0].Rows[index++][1].ToString();
                        PST_Details.PST_child_empDetails[objCounter_item++] = PST_Emp;
                    }
                   
                }
                else
                {
                    //Error handling : For No records found
                    PST_Details.PST_headerDetails.statusFlag = 1;
                    PST_Details.PST_headerDetails.statusMsg = "No Records Found";

                    logger.Debug("statusFlag = " + PST_Details.PST_headerDetails.statusFlag.ToString());
                    logger.Debug("statusMsg = " + PST_Details.PST_headerDetails.statusMsg);

                    return PST_Details;
                }

                logger.Debug("In Success case : statusFlag = " + PST_Details.PST_headerDetails.statusFlag.ToString());
                logger.Debug("In Success case : statusMsg = " + PST_Details.PST_headerDetails.statusMsg);
                logger.Info("Method : employeeSearch_DAL Stop");

                //tsTrans.Commit();
                return PST_Details;

            }
            catch (OracleException dbEx)
            {
                logger.Fatal("Exception Occured At PST_DAL - employeeSearch_DAL");
                logger.Debug("Exception Code : " + dbEx.ErrorCode.ToString());
                logger.Debug("Exception Description : " + dbEx.Message.ToString());
                logger.Error("Error : employeeSearch_DAL Stop");

                //tsTrans.Rollback();
                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception Occured At PST_DAL - employeeSearch_DAL  : " + ex.Message.ToString());
                logger.Error("Error : employeeSearch_DAL Stop");

                // tsTrans.Rollback();
                //throw new myCustomException(31, ex.Message);
                throw ex;
            }
            finally
            {
                logger.Debug("Connection Closed ");

                conn.Dispose();
                myConn.Dispose();
            }
        }

    }

}
