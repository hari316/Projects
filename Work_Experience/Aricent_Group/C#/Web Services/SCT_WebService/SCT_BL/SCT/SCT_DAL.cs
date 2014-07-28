using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Globalization;
using utilities;
using System.Data;


namespace SCT_BL.SCT
{
    public class SCT_DAL
    {
        public SCT_DAL()
        {
            // TODO: Add constructor logic here 
        }

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(SCT_DAL));    

        queryLog QueryLog = new queryLog();
        
        /// <summary>
        /// This function fetches the SCT Details from the database based on PReqNo
        /// </summary>
        /// <param name="PReqNo"></param>
        /// <returns>
        /// <paramref name="SCT_Entity"/>
        /// </returns>
        /// <history>
        ///     Hari haran      17/07/2012      created
        /// </history>

        public SCT_Emp searchEmployee_DAL(string Name_DAL, string MgrId_DAL ,string Flag)
        {
            logger.Info("Method : searchEmployee_DAL Start");
            logger.DebugFormat("Input parameter Name : {0} ", Name_DAL);
            logger.DebugFormat("Input parameter ManagerId : {0} ", MgrId_DAL);

            databaseLayer dbConStr = new databaseLayer();
            string connStr = dbConStr.connectionInfo;

            logger.Debug("Connection string : " + connStr);

            SqlConnection conn = new SqlConnection(connStr);

            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
                logger.Debug("Connection Status opened ");
            }

            logger.Info("Connetion to the database established");

            try
            {
                SqlCommand cmd_SCTDetails = new SqlCommand();
                cmd_SCTDetails.Connection = conn;
                string Query = string.Empty;

                switch (Flag)
                {
                    case "0":
                        {
                            
                            SqlCommand cmd_empFunc = new SqlCommand();
                            cmd_empFunc.Connection = conn;
                            cmd_empFunc.CommandText = "SELECT [Function] FROM employees WHERE EmployeeNumber = @MgrID ";
                            cmd_empFunc.Parameters.AddWithValue("@MgrID",Convert.ToInt32(MgrId_DAL));

                            QueryLog.CmdInfo(cmd_empFunc);

                            string func = cmd_empFunc.ExecuteScalar().ToString();

                            logger.DebugFormat("Manager ID {0} - Funtion type {1}", MgrId_DAL, func);

                            if (func.Equals("ENGG"))
                            {
                                logger.Info("Search Type : Manager Search - Function : ENGG");

                                Query = "select e.EMPLOYEENUMBER as EmployeeNumber,e.EMPLOYEENAME as employeeName,c.codedescription Grade from Employees e,codes c ";
                                Query += "where codedescription in ('E5','E6','E7','E8','VP','AVP','President & COO','SVP','A5','A6','A7','N5','N6','N7','C5','C6','C7') ";
                                Query += "and codetype='Grade' and code=e.currentgrade and EmployeeName like @Name and EmployeeStatus='E'";
                            }
                            else
                            {
                                logger.Info("Search Type : Manager Search - Function : NON ENGG");

                                Query = "select e.EMPLOYEENUMBER as EmployeeNumber ,e.EMPLOYEENAME as employeeName,c.codedescription Grade from Employees e,codes c ";
                                Query += "where codedescription in ('E4','E5','E6','E7','E8','VP','AVP','President & COO','SVP','A4','A5','A6','A7','N4','N5','N6','N7','C5','C6','C7') ";
                                Query += "and codetype='Grade' and code=e.currentgrade and EmployeeName like @Name and EmployeeStatus='E'";
                            }


                            cmd_SCTDetails.Parameters.AddWithValue("@Name", "%" + Name_DAL + "%");
                            
                        }
                        break;

                    case "1":
                    default:
                        {
                            logger.Info("Search Type : Employee Search");

                            Query = "select EmployeeNumber,employeeName,c.Codedescription Grade from Employees,Codes C,HRMSAC_EMP_COSTCENTER_SUBAREA ";
                            Query += "where EmployeeNumber=ECS_EMPNO and EmployeeStatus='E' and EmployeeNumber>1000 ";
                            Query += "and CurrentGrade=c.code and codetype='grade' and ";
                            Query += "reportingto=(select top 1 emailid from employees where employeenumber= @MgrId) and EmployeeName like @Name ";
                            cmd_SCTDetails.Parameters.AddWithValue("@Name", "%" + Name_DAL + "%");
                            cmd_SCTDetails.Parameters.Add("@MgrId", SqlDbType.Int);
                            cmd_SCTDetails.Parameters["@MgrId"].Value = Convert.ToInt32(MgrId_DAL);

                        }
                        break;

                }

                cmd_SCTDetails.CommandText = Query;

                logger.Info("cmd_SCTDetails Query parameters initialised");

                QueryLog.CmdInfo(cmd_SCTDetails);

                SqlDataAdapter da_SCTDetails = new SqlDataAdapter();
                da_SCTDetails.SelectCommand = cmd_SCTDetails;
                DataSet dSet_SCTDetails = new DataSet();
                da_SCTDetails.Fill(dSet_SCTDetails);

                logger.Info("cmd_SCTDetails excuted by SqlDataAdapter()");
                
                int empDetails_count = dSet_SCTDetails.Tables[0].Rows.Count;
                
                logger.DebugFormat("No. of records found based on the search criteria : {0}",empDetails_count.ToString());

                SCT_Emp result = new SCT_Emp(empDetails_count);

                if (empDetails_count > 0)
                {

                    result.SCT_headerDetails.StatusFlag = 0;
                    result.SCT_headerDetails.StatusMsg = SCT_Constants.Success;

                    int objCounter_item = 0;

                    foreach (DataRow item_dr in dSet_SCTDetails.Tables[0].Rows)
                    {
                        SCT_Emp.childDetails sct_Item = new SCT_Emp.childDetails();
                        sct_Item.EmpID = item_dr["EmployeeNumber"].ToString();
                        sct_Item.EmpName = item_dr["employeeName"].ToString();
                        sct_Item.Grade = item_dr["Grade"].ToString();

                        result.SCT_childDetails[objCounter_item++] = sct_Item;
                    }
                }
                else
                {
                    //Handling : No Match found case

                    result.SCT_headerDetails.StatusFlag = 31;
                    result.SCT_headerDetails.StatusMsg = SCT_Constants.NoMatch;

                    logger.DebugFormat("No rows returned from the table for the search criteria (Name) : {0}", Name_DAL.ToString());
                    logger.Debug("ErrorCode = " + result.SCT_headerDetails.StatusFlag.ToString());
                    logger.Debug("ErrorMessage = " + result.SCT_headerDetails.StatusMsg);

                    return result;
                }

                logger.Debug("In Success case : Status Flag = " + result.SCT_headerDetails.StatusFlag.ToString());
                logger.Debug("In Success case : Status Message = " + result.SCT_headerDetails.StatusMsg);
                logger.Info("Method : searchEmployee_DAL Stop");

                return result;

            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Exception Occured At SCT_DAL - searchEmployee_DAL");
                logger.Debug("Exception Code : " + dbEx.Number.ToString());
                logger.Debug("Exception Description : " + dbEx.Message.ToString());
                logger.Error("Error : searchEmployee_DAL Stop");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception Occured At SCT_DAL - searchEmployee_DAL  : " + ex.Message.ToString());
                logger.Error("Error : searchEmployee_DAL Stop");

                throw ex;
            }
            finally
            {
                logger.Debug("Connection Status Closed ");

                //conn.Dispose();
            }
        }


        /********************************************************************************************************/

        public SCT_RChange getReasonForChange_DAL()
        {
            logger.Info("Method : getReasonForChange_DAL Start");

            databaseLayer dbConStr = new databaseLayer();
            string connStr = dbConStr.connectionInfo;

            logger.Debug("Connection string : " + connStr);

            SqlConnection conn = new SqlConnection(connStr);

            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
                logger.Debug("Connection Status opened ");
            }

            logger.Info("Connetion to the database established");

            try
            {

                string Query = "select distinct REASONID, REASONDESC from HRMSAC_REPORTING_CHANGE_REASON";
                SqlCommand cmd_SCTDetails = new SqlCommand();
                cmd_SCTDetails.Connection = conn;
                cmd_SCTDetails.CommandText = Query;

                QueryLog.CmdInfo(cmd_SCTDetails);

                SqlDataAdapter da_SCTDetails = new SqlDataAdapter();
                da_SCTDetails.SelectCommand = cmd_SCTDetails;
                DataSet dSet_SCTDetails = new DataSet();
                da_SCTDetails.Fill(dSet_SCTDetails);

                logger.Info("cmd_SCTDetails excuted by SqlDataAdapter()");

                int Details_count = dSet_SCTDetails.Tables[0].Rows.Count;

                logger.DebugFormat("Row Count : ItemDetails table {0}", Details_count.ToString());

                SCT_RChange result = new SCT_RChange(Details_count);

                if (Details_count > 0)
                {

                    result.SCT_header.StatusFlag = 0;
                    result.SCT_header.StatusMsg = SCT_Constants.Success;

                    int objCounter_item = 0;

                    foreach (DataRow item_dr in dSet_SCTDetails.Tables[0].Rows)
                    {
                        SCT_RChange.RChange Item = new SCT_RChange.RChange();
                        Item.ID = item_dr["REASONID"].ToString();
                        Item.Label = item_dr["REASONDESC"].ToString();

                        result.SCT_Details[objCounter_item++] = Item;
                    }
                }

                else
                {
                    result.SCT_header.StatusFlag = 1;
                    result.SCT_header.StatusMsg = "No records Found";
                }


                logger.Info("Method : getReasonForChange_DAL Stop");

                return result;

            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Exception Occured At SCT_DAL - getReasonForChange_DAL");
                logger.Debug("Exception Code : " + dbEx.Number.ToString());
                logger.Debug("Exception Description : " + dbEx.Message.ToString());
                logger.Error("Error : getReasonForChange_DAL Stop");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception Occured At SCT_DAL - getReasonForChange_DAL  : " + ex.Message.ToString());
                logger.Error("Error : getReasonForChange_DAL Stop");

                throw ex;
            }
            finally
            {
                logger.Debug("Connection Status Closed ");

                conn.Dispose();
            }
        }


        /********************************************************************************************************/

        public SCT_PA getPersonnelArea_DAL()
        {
            logger.Info("Method : getPersonnelArea_DAL Start");

            databaseLayer dbConStr = new databaseLayer();
            string connStr = dbConStr.connectionInfo;

            logger.Debug("Connection string : " + connStr);

            SqlConnection conn = new SqlConnection(connStr);

            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
                logger.Debug("Connection Status opened ");
            }

            logger.Info("Connetion to the database established");

            try
            {

                string Query = "select code PE_ID,codedescription PE_DESC from codes where codetype='personnelarea' and displayorder=1";
                SqlCommand cmd_SCTDetails = new SqlCommand();
                cmd_SCTDetails.Connection = conn;
                cmd_SCTDetails.CommandText = Query;
                //cmd_SCTDetails.CommandType = CommandType.StoredProcedure;

                QueryLog.CmdInfo(cmd_SCTDetails);

                SqlDataAdapter da_SCTDetails = new SqlDataAdapter();
                da_SCTDetails.SelectCommand = cmd_SCTDetails;
                DataSet dSet_SCTDetails = new DataSet();
                da_SCTDetails.Fill(dSet_SCTDetails);

                logger.Info("cmd_SCTDetails excuted by SqlDataAdapter()");

                int Details_count = dSet_SCTDetails.Tables[0].Rows.Count;       

                logger.DebugFormat("Row Count : ItemDetails table {0}", Details_count.ToString());

                SCT_PA result = new SCT_PA(Details_count);

                if (Details_count > 0)
                {

                    result.SCT_header.StatusFlag = 0;
                    result.SCT_header.StatusMsg = SCT_Constants.Success;

                    int objCounter_item = 0;

                    foreach (DataRow item_dr in dSet_SCTDetails.Tables[0].Rows)                   
                    {
                        SCT_PA.PA Item = new SCT_PA.PA();
                        Item.ID = item_dr["PE_ID"].ToString();
                        Item.Label = item_dr["PE_DESC"].ToString();

                        result.SCT_Details[objCounter_item++] = Item;
                    }
                }

                else
                {
                    result.SCT_header.StatusFlag = 1;
                    result.SCT_header.StatusMsg = "No records Found";
                }


                logger.Info("Method : searchEmployee_DAL Stop");

                return result;

            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Exception Occured At SCT_DAL - getPersonnelArea_DAL");
                logger.Debug("Exception Code : " + dbEx.Number.ToString());
                logger.Debug("Exception Description : " + dbEx.Message.ToString());
                logger.Error("Error : getPersonnelArea_DAL Stop");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception Occured At SCT_DAL - getPersonnelArea_DAL  : " + ex.Message.ToString());
                logger.Error("Error : getPersonnelArea_DAL Stop");

                throw ex;
            }
            finally
            {
                logger.Debug("Connection Status Closed ");

                conn.Dispose();
            }
        }


        /********************************************************************************************************/

        public SCT_PSA getPersonnelSubArea_DAL()
        {
            logger.Info("Method : getPersonnelSubArea_DAL Start");

            databaseLayer dbConStr = new databaseLayer();
            string connStr = dbConStr.connectionInfo;

            logger.Debug("Connection string : " + connStr);

            SqlConnection conn = new SqlConnection(connStr);

            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
                logger.Debug("Connection Status opened ");
            }

            logger.Info("Connetion to the database established");

            try
            {

                string query = "select code sa_id,codedescription sa_desc from codes where codetype='personnelsubarea' and displayorder=1";
                SqlCommand cmd_SCTDetails = new SqlCommand();
                cmd_SCTDetails.Connection = conn;
                cmd_SCTDetails.CommandText = query;

                logger.Info("cmd_SCTDetails Query parameters initialised");

                QueryLog.CmdInfo(cmd_SCTDetails);

                SqlDataAdapter da_SCTDetails = new SqlDataAdapter();
                da_SCTDetails.SelectCommand = cmd_SCTDetails;
                DataSet dSet_SCTDetails = new DataSet();
                da_SCTDetails.Fill(dSet_SCTDetails);

                logger.Info("cmd_SCTDetails excuted by SqlDataAdapter()");

                int Details_count = dSet_SCTDetails.Tables[0].Rows.Count;
                
                logger.DebugFormat("Row Count : ItemDetails table {0}", Details_count.ToString());

                SCT_PSA result = new SCT_PSA(Details_count);

                if (Details_count > 0)
                {

                    result.SCT_header.StatusFlag = 0;
                    result.SCT_header.StatusMsg = SCT_Constants.Success;

                    int objCounter_item = 0;

                    foreach (DataRow item_dr in dSet_SCTDetails.Tables[0].Rows)
                    {
                        SCT_PSA.PSA Item = new SCT_PSA.PSA();
                        Item.ID = item_dr["SA_ID"].ToString();
                        Item.Label = item_dr["SA_DESC"].ToString();

                        result.SCT_Details[objCounter_item++] = Item;
                    }
                }

                else
                {
                    result.SCT_header.StatusFlag = 1;
                    result.SCT_header.StatusMsg = "No records Found";
                }


                logger.Info("Method : searchEmployee_DAL Stop");

                return result;

            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Exception Occured At SCT_DAL - getPersonnelSubArea_DAL");
                logger.Debug("Exception Code : " + dbEx.Number.ToString());
                logger.Debug("Exception Description : " + dbEx.Message.ToString());
                logger.Error("Error : getPersonnelSubArea_DAL Stop");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception Occured At SCT_DAL - getPersonnelSubArea_DAL  : " + ex.Message.ToString());
                logger.Error("Error : getPersonnelSubArea_DAL Stop");

                throw ex;
            }
            finally
            {
                logger.Debug("Connection Status Closed ");

                conn.Dispose();
            }
        }
        /********************************************************************************************************/

        public SCT_ORGUnit getOrganizationalUnit_DAL()
        {
            logger.Info("Method : getOrganizationalUnit_DAL Start");

            databaseLayer dbConStr = new databaseLayer();
            string connStr = dbConStr.connectionInfo;

            logger.Debug("Connection string : " + connStr);

            SqlConnection conn = new SqlConnection(connStr);

            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
                logger.Debug("Connection Status opened ");
            }

            logger.Info("Connetion to the database established");

            try
            {

                string Query = "select distinct GM_ID,GM_DESC from HRMSAC_GROUP_MASTER Order by GM_DESC";
                SqlCommand cmd_SCTDetails = new SqlCommand();
                cmd_SCTDetails.Connection = conn;
                cmd_SCTDetails.CommandText = Query;

                logger.Info("cmd_SCTDetails Query parameters initialised");

                QueryLog.CmdInfo(cmd_SCTDetails);

                SqlDataAdapter da_SCTDetails = new SqlDataAdapter();
                da_SCTDetails.SelectCommand = cmd_SCTDetails;
                DataSet dSet_SCTDetails = new DataSet();
                da_SCTDetails.Fill(dSet_SCTDetails);

                logger.Info("cmd_SCTDetails excuted by SqlDataAdapter()");

                int Details_count = dSet_SCTDetails.Tables[0].Rows.Count;
                
                logger.DebugFormat("Row Count : ItemDetails table {0}", Details_count.ToString());

                SCT_ORGUnit result = new SCT_ORGUnit(Details_count);

                if (Details_count > 0)
                {

                    result.SCT_header.StatusFlag = 0;
                    result.SCT_header.StatusMsg = SCT_Constants.Success;

                    int objCounter_item = 0;

                    foreach (DataRow item_dr in dSet_SCTDetails.Tables[0].Rows)            
                    {
                        SCT_ORGUnit.ORGUnit Item = new SCT_ORGUnit.ORGUnit();
                        Item.ID = item_dr["GM_ID"].ToString();
                        Item.Label = item_dr["GM_DESC"].ToString();
    
                        result.SCT_Details[objCounter_item++] = Item;
                    }
                }

                else
                {
                    result.SCT_header.StatusFlag = 1;
                    result.SCT_header.StatusMsg = "No records Found";
                }


                logger.Info("Method : getOrganizationalUnit_DAL Stop");

                return result;

            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Exception Occured At SCT_DAL - getOrganizationalUnit_DAL");
                logger.Debug("Exception Code : " + dbEx.Number.ToString());
                logger.Debug("Exception Description : " + dbEx.Message.ToString());
                logger.Error("Error : getOrganizationalUnit_DAL Stop");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception Occured At SCT_DAL - getOrganizationalUnit_DAL  : " + ex.Message.ToString());
                logger.Error("Error : getOrganizationalUnit_DAL Stop");

                throw ex;
            }
            finally
            {
                logger.Debug("Connection Status Closed ");

                conn.Dispose();
            }
        }

        /********************************************************************************************************/

        public SCT_CC getCostCenter_DAL()
        {
            logger.Info("Method : getCostCenter_DAL Start");

            databaseLayer dbConStr = new databaseLayer();
            string connStr = dbConStr.connectionInfo;

            logger.Debug("Connection string : " + connStr);

            SqlConnection conn = new SqlConnection(connStr);

            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
                logger.Debug("Connection Status opened ");
            }

            logger.Info("Connetion to the database established");

            try
            {

                string Query = "select distinct code CCM_ID,codedescription CCM_DESC from codes where codetype='costcenter'";
                SqlCommand cmd_SCTDetails = new SqlCommand();
                cmd_SCTDetails.Connection = conn;
                cmd_SCTDetails.CommandText = Query;

                logger.Info("cmd_SCTDetails Query parameters initialised");

                QueryLog.CmdInfo(cmd_SCTDetails);

                SqlDataAdapter da_SCTDetails = new SqlDataAdapter();
                da_SCTDetails.SelectCommand = cmd_SCTDetails;
                DataSet dSet_SCTDetails = new DataSet();
                da_SCTDetails.Fill(dSet_SCTDetails);

                logger.Info("cmd_SCTDetails excuted by SqlDataAdapter()");

                int Details_count = dSet_SCTDetails.Tables[0].Rows.Count;
                
                logger.DebugFormat("Row Count : ItemDetails table {0}", Details_count.ToString());

                SCT_CC result = new SCT_CC(Details_count);

                if (Details_count > 0)
                {

                    result.SCT_header.StatusFlag = 0;
                    result.SCT_header.StatusMsg = SCT_Constants.Success;

                    int objCounter_item = 0;

                    foreach (DataRow item_dr in dSet_SCTDetails.Tables[0].Rows)                 
                    {
                        SCT_CC.CC Item = new SCT_CC.CC();
                        Item.ID = item_dr["CCM_ID"].ToString();
                        Item.Label = item_dr["CCM_DESC"].ToString();

                        result.SCT_Details[objCounter_item++] = Item;
                    }
                }

                else
                {
                    result.SCT_header.StatusFlag = 1;
                    result.SCT_header.StatusMsg = "No records Found";
                }


                logger.Info("Method : getCostCenter_DAL Stop");

                return result;

            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Exception Occured At SCT_DAL - getCostCenter_DAL");
                logger.Debug("Exception Code : " + dbEx.Number.ToString());
                logger.Debug("Exception Description : " + dbEx.Message.ToString());
                logger.Error("Error : getCostCenter_DAL Stop");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception Occured At SCT_DAL - getCostCenter_DAL  : " + ex.Message.ToString());
                logger.Error("Error : getCostCenter_DAL Stop");

                throw ex;
            }
            finally
            {
                logger.Debug("Connection Status Closed ");

                conn.Dispose();
            }
        }

        /********************************************************************************************************/

        public SCT_BU getBusinessUnit_DAL()
        {
            logger.Info("Method : getBusinessUnit_DAL Start");

            databaseLayer dbConStr = new databaseLayer();
            string connStr = dbConStr.connectionInfo;

            logger.Debug("Connection string : " + connStr);

            SqlConnection conn = new SqlConnection(connStr);

            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
                logger.Debug("Connection Status opened ");
            }

            logger.Info("Connetion to the database established");

            try
            {

                string Query = "select distinct BU_CODE,BU_Description from HRMSAC_BU_Master where BU_Status='E' order by BU_Description";
                SqlCommand cmd_SCTDetails = new SqlCommand();
                cmd_SCTDetails.Connection = conn;
                cmd_SCTDetails.CommandText = Query;

                logger.Info("cmd_SCTDetails Query parameters initialised");

                QueryLog.CmdInfo(cmd_SCTDetails);

                SqlDataAdapter da_SCTDetails = new SqlDataAdapter();
                da_SCTDetails.SelectCommand = cmd_SCTDetails;
                DataSet dSet_SCTDetails = new DataSet();
                da_SCTDetails.Fill(dSet_SCTDetails);

                logger.Info("cmd_SCTDetails excuted by SqlDataAdapter()");

                int Details_count = dSet_SCTDetails.Tables[0].Rows.Count;

                logger.DebugFormat("Row Count : ItemDetails table {0}", Details_count.ToString());

                SCT_BU result = new SCT_BU(Details_count);

                if (Details_count > 0)
                {

                    result.SCT_header.StatusFlag = 0;
                    result.SCT_header.StatusMsg = SCT_Constants.Success;

                    int objCounter_item = 0;

                    foreach (DataRow item_dr in dSet_SCTDetails.Tables[0].Rows)
                    {
                        SCT_BU.BU Item = new SCT_BU.BU();
                        Item.ID = item_dr["BU_CODE"].ToString();
                        Item.Label = item_dr["BU_Description"].ToString();

                        result.SCT_Details[objCounter_item++] = Item;
                    }
                }

                else
                {
                    result.SCT_header.StatusFlag = 1;
                    result.SCT_header.StatusMsg = "No records Found";
                }


                logger.Info("Method : getBusinessUnit_DAL Stop");

                return result;

            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Exception Occured At SCT_DAL - getBusinessUnit_DAL");
                logger.Debug("Exception Code : " + dbEx.Number.ToString());
                logger.Debug("Exception Description : " + dbEx.Message.ToString());
                logger.Error("Error : getBusinessUnit_DAL Stop");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception Occured At SCT_DAL - getBusinessUnit_DAL  : " + ex.Message.ToString());
                logger.Error("Error : getBusinessUnit_DAL Stop");

                throw ex;
            }
            finally
            {
                logger.Debug("Connection Status Closed ");

                conn.Dispose();
            }
        }

        /********************************************************************************************************/

        public SCT_SBU getStrategicBusinessUnit_DAL()
        {
            logger.Info("Method : getStrategicBusinessUnit_DAL Start");

            databaseLayer dbConStr = new databaseLayer();
            string connStr = dbConStr.connectionInfo;

            logger.Debug("Connection string : " + connStr);

            SqlConnection conn = new SqlConnection(connStr);

            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
                logger.Debug("Connection Status opened ");
            }

            logger.Info("Connetion to the database established");

            try
            {

                string Query = "select distinct SBU_CODE,SBU_DESCRIPTION from HRMSAC_SBU_MASTER where SBU_Status='E' order by SBU_DESCRIPTION";
                SqlCommand cmd_SCTDetails = new SqlCommand();
                cmd_SCTDetails.Connection = conn;
                cmd_SCTDetails.CommandText = Query;

                logger.Info("cmd_SCTDetails Query parameters initialised");

                QueryLog.CmdInfo(cmd_SCTDetails);

                SqlDataAdapter da_SCTDetails = new SqlDataAdapter();
                da_SCTDetails.SelectCommand = cmd_SCTDetails;
                DataSet dSet_SCTDetails = new DataSet();
                da_SCTDetails.Fill(dSet_SCTDetails);

                logger.Info("cmd_SCTDetails excuted by SqlDataAdapter()");

                int Details_count = dSet_SCTDetails.Tables[0].Rows.Count;
               
                logger.DebugFormat("Row Count : ItemDetails table {0}", Details_count.ToString());

                SCT_SBU result = new SCT_SBU(Details_count);

                if (Details_count > 0)
                {

                    result.SCT_header.StatusFlag = 0;
                    result.SCT_header.StatusMsg = SCT_Constants.Success;

                    int objCounter_item = 0;

                    foreach (DataRow item_dr in dSet_SCTDetails.Tables[0].Rows)
                    {
                        SCT_SBU.SBU Item = new SCT_SBU.SBU();
                        Item.ID = item_dr["SBU_CODE"].ToString();
                        Item.Label = item_dr["SBU_Description"].ToString();

                        result.SCT_Details[objCounter_item++] = Item;
                    }
                }

                else
                {
                    result.SCT_header.StatusFlag = 1;
                    result.SCT_header.StatusMsg = "No records Found";
                }


                logger.Info("Method : getStrategicBusinessUnit_DAL Stop");

                return result;

            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Exception Occured At SCT_DAL - getStrategicBusinessUnit_DAL");
                logger.Debug("Exception Code : " + dbEx.Number.ToString());
                logger.Debug("Exception Description : " + dbEx.Message.ToString());
                logger.Error("Error : getStrategicBusinessUnit_DAL Stop");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception Occured At SCT_DAL - getStrategicBusinessUnit_DAL  : " + ex.Message.ToString());
                logger.Error("Error : getStrategicBusinessUnit_DAL Stop");

                throw ex;
            }
            finally
            {
                logger.Debug("Connection Status Closed ");

                conn.Dispose();
            }
        }

        /********************************************************************************************************/

        public SCT_Horizontal getHorizontal_DAL()
        {
            logger.Info("Method : getHorizontal_DAL Start");

            databaseLayer dbConStr = new databaseLayer();
            string connStr = dbConStr.connectionInfo;

            logger.Debug("Connection string : " + connStr);

            SqlConnection conn = new SqlConnection(connStr);

            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
                logger.Debug("Connection Status opened ");
            }

            logger.Info("Connetion to the database established");

            try
            {

                string Query = "select divisionid horizontal_code,divisionname horizontal_Description from division where divisiontype='HZ' and divisionstatus='A'";
                SqlCommand cmd_SCTDetails = new SqlCommand();
                cmd_SCTDetails.Connection = conn;
                cmd_SCTDetails.CommandText = Query;

                logger.Info("cmd_SCTDetails Query parameters initialised");

                QueryLog.CmdInfo(cmd_SCTDetails);

                SqlDataAdapter da_SCTDetails = new SqlDataAdapter();
                da_SCTDetails.SelectCommand = cmd_SCTDetails;
                DataSet dSet_SCTDetails = new DataSet();
                da_SCTDetails.Fill(dSet_SCTDetails);

                logger.Info("cmd_SCTDetails excuted by SqlDataAdapter()");

                int Details_count = dSet_SCTDetails.Tables[0].Rows.Count;
                
                logger.DebugFormat("Row Count : ItemDetails table {0}", Details_count.ToString());

                SCT_Horizontal result = new SCT_Horizontal(Details_count);

                if (Details_count > 0)
                {

                    result.SCT_header.StatusFlag = 0;
                    result.SCT_header.StatusMsg = SCT_Constants.Success;

                    int objCounter_item = 0;

                    foreach (DataRow item_dr in dSet_SCTDetails.Tables[0].Rows)
                    {
                        SCT_Horizontal.Horizontal Item = new SCT_Horizontal.Horizontal();
                        Item.ID = item_dr["horizontal_code"].ToString();
                        Item.Label = item_dr["horizontal_Description"].ToString();

                        result.SCT_Details[objCounter_item++] = Item;
                    }
                }

                else
                {
                    result.SCT_header.StatusFlag = 1;
                    result.SCT_header.StatusMsg = "No records Found";
                }


                logger.Info("Method : searchEmployee_DAL Stop");

                return result;

            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Exception Occured At SCT_DAL - getHorizontal_DAL");
                logger.Debug("Exception Code : " + dbEx.Number.ToString());
                logger.Debug("Exception Description : " + dbEx.Message.ToString());
                logger.Error("Error : getHorizontal_DAL Stop");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception Occured At SCT_DAL - getHorizontal_DAL  : " + ex.Message.ToString());
                logger.Error("Error : getHorizontal_DAL Stop");

                throw ex;
            }
            finally
            {
                logger.Debug("Connection Status Closed ");

                conn.Dispose();
            }
        }

        /********************************************************************************************************/

        public SCT_BLoc getBaseLocation_DAL()
        {
            logger.Info("Method : getBaseLocation_DAL Start");

            databaseLayer dbConStr = new databaseLayer();
            string connStr = dbConStr.connectionInfo;

            logger.Debug("Connection string : " + connStr);

            SqlConnection conn = new SqlConnection(connStr);

            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
                logger.Debug("Connection Status opened ");
            }

            logger.Info("Connetion to the database established");

            try
            {

                string Query = "select c.code PE_ID,c.codedescription PE_DESC from codes c where c.codetype='BaseLocation' and displayorder=1";
                SqlCommand cmd_SCTDetails = new SqlCommand();
                cmd_SCTDetails.Connection = conn;
                cmd_SCTDetails.CommandText = Query;

                logger.Info("cmd_SCTDetails Query parameters initialised");

                QueryLog.CmdInfo(cmd_SCTDetails);

                SqlDataAdapter da_SCTDetails = new SqlDataAdapter();
                da_SCTDetails.SelectCommand = cmd_SCTDetails;
                DataSet dSet_SCTDetails = new DataSet();
                da_SCTDetails.Fill(dSet_SCTDetails);

                logger.Info("cmd_SCTDetails excuted by SqlDataAdapter()");

                int Details_count = dSet_SCTDetails.Tables[0].Rows.Count;

                logger.DebugFormat("Row Count : ItemDetails table {0}", Details_count.ToString());

                SCT_BLoc result = new SCT_BLoc(Details_count);

                if (Details_count > 0)
                {

                    result.SCT_header.StatusFlag = 0;
                    result.SCT_header.StatusMsg = SCT_Constants.Success;

                    int objCounter_item = 0;

                    foreach (DataRow item_dr in dSet_SCTDetails.Tables[0].Rows) 
                    {
                        SCT_BLoc.BLoc Item = new SCT_BLoc.BLoc();
                        Item.ID = item_dr["PE_ID"].ToString();
                        Item.Label = item_dr["PE_DESC"].ToString();

                        result.SCT_Details[objCounter_item++] = Item;
                    }
                }

                else
                {
                    result.SCT_header.StatusFlag = 1;
                    result.SCT_header.StatusMsg = "No records Found";
                }


                logger.Info("Method : searchEmployee_DAL Stop");

                return result;

            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Exception Occured At SCT_DAL - getBaseLocation_DAL");
                logger.Debug("Exception Code : " + dbEx.Number.ToString());
                logger.Debug("Exception Description : " + dbEx.Message.ToString());
                logger.Error("Error : getBaseLocation_DAL Stop");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception Occured At SCT_DAL - getBaseLocation_DAL  : " + ex.Message.ToString());
                logger.Error("Error : getBaseLocation_DAL Stop");

                throw ex;
            }
            finally
            {
                logger.Debug("Connection Status Closed ");

                conn.Dispose();
            }
        }

        /********************************************************************************************************/

        public SCT_CDeploy getCenterOfDeployment_DAL()
        {
            logger.Info("Method : getCenterOfDeployment_DAL Start");

            databaseLayer dbConStr = new databaseLayer();
            string connStr = dbConStr.connectionInfo;

            logger.Debug("Connection string : " + connStr);

            SqlConnection conn = new SqlConnection(connStr);

            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
                logger.Debug("Connection Status opened ");
            }

            logger.Info("Connetion to the database established");

            try
            {

                string Query = "select code PE_ID,codedescription PE_DESC from codes where codetype='personnelarea' and displayorder=1";
                SqlCommand cmd_SCTDetails = new SqlCommand();
                cmd_SCTDetails.Connection = conn;
                cmd_SCTDetails.CommandText = Query;

                logger.Info("cmd_SCTDetails Query parameters initialised");

                QueryLog.CmdInfo(cmd_SCTDetails);

                SqlDataAdapter da_SCTDetails = new SqlDataAdapter();
                da_SCTDetails.SelectCommand = cmd_SCTDetails;
                DataSet dSet_SCTDetails = new DataSet();
                da_SCTDetails.Fill(dSet_SCTDetails);

                logger.Info("cmd_SCTDetails excuted by SqlDataAdapter()");

                int Details_count = dSet_SCTDetails.Tables[0].Rows.Count;

                logger.DebugFormat("Row Count : ItemDetails table {0}", Details_count.ToString());

                SCT_CDeploy result = new SCT_CDeploy(Details_count);

                if (Details_count > 0)
                {

                    result.SCT_header.StatusFlag = 0;
                    result.SCT_header.StatusMsg = SCT_Constants.Success;

                    int objCounter_item = 0;

                    foreach (DataRow item_dr in dSet_SCTDetails.Tables[0].Rows)
                    {
                        SCT_CDeploy.CDeploy Item = new SCT_CDeploy.CDeploy();
                        Item.ID = item_dr["PE_ID"].ToString();
                        Item.Label = item_dr["PE_DESC"].ToString();

                        result.SCT_Details[objCounter_item++] = Item;
                    }
                }

                else
                {
                    result.SCT_header.StatusFlag = 1;
                    result.SCT_header.StatusMsg = "No records Found";
                }

                logger.Info("Method : getCenterOfDeployment_DAL Stop");

                return result;

            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Exception Occured At SCT_DAL - getCenterOfDeployment_DAL");
                logger.Debug("Exception Code : " + dbEx.Number.ToString());
                logger.Debug("Exception Description : " + dbEx.Message.ToString());
                logger.Error("Error : getCenterOfDeployment_DAL Stop");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception Occured At SCT_DAL - getCenterOfDeployment_DAL  : " + ex.Message.ToString());
                logger.Error("Error : getCenterOfDeployment_DAL Stop");

                throw ex;
            }
            finally
            {
                logger.Debug("Connection Status Closed ");

                conn.Dispose();
            }
        }


        /********************************************************************************************************/

        public SCT_Entity getSCEmployeeDetails_DAL(string empId_DAL,string mgrId_DAL)
        {
            logger.Info("Method : getSCEmployeeDetails_DAL Start");

            databaseLayer dbConStr = new databaseLayer();
            string connStr = dbConStr.connectionInfo;

            logger.Debug("Connection string : " + connStr);

            SqlConnection conn = new SqlConnection(connStr);

            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
                logger.Debug("Connection Status opened ");
            }

            logger.Info("Connetion to the database established");

            try
            {

                string Query = "select EmployeeNumber,employeeName ";
                Query +=",SBUId ,SBU_DESCRIPTION ";
                Query +=",BUId ,BU_Description ";
                Query +=",Horizontalid ,h.divisionname as Horizontal ";
                Query +=",e.DivisionId as OrgId,GM_DESC as OrgDesc ";
                Query +=",PersonnelArea as PAId,pa.codedescription as PADESC ";
                Query +=",ECS_SUB_AREA ,ps.codedescription as SADESC ";
                Query +=",ECS_COST_CENTER ,cc.codedescription as CostDesc ";
                Query +=",c.Codedescription as Grade ";
                Query +=",BaselocationId ,bl.codedescription as BLocDesc ";
                Query +=",CenterofDeployment ,cd.codedescription as CDeployDesc ";

                Query +="from Employees E,Codes C,HRMSAC_EMP_COSTCENTER_SUBAREA,HRMSAC_SBU_MASTER ";
                Query +=",HRMSAC_BU_Master,division H,HRMSAC_GROUP_MASTER O ";
                Query +=",codes pa,codes ps,codes cc,codes bl,codes cd ";

                Query +="where EmployeeNumber=ECS_EMPNO and EmployeeStatus='E'  ";
                Query +="and SBUId = SBU_CODE and SBU_Status='E'  ";
                Query +="and BUId = BU_CODE and BU_Status='E' ";
                Query +="and Horizontalid = h.divisionid and h.divisiontype='HZ' and h.divisionstatus='A' ";
                Query +="and e.DivisionId = o.GM_ID ";
                Query +="and PersonnelArea = pa.code and pa.codetype='personnelarea' ";
                Query +="and ECS_SUB_AREA = ps.code and ps.codetype='personnelsubarea'  ";
                Query +="and c.codetype='grade'  ";
                Query +="and ECS_COST_CENTER = cc.code and cc.codetype='costcenter' ";
                Query +="and BaselocationId = bl.code and bl.codetype='BaseLocation' ";
                Query +="and CenterofDeployment = cd.code and cd.codetype='personnelarea' ";
                Query +="and EmployeeNumber>1000 and CurrentGrade=c.code ";

                Query += "and reportingto=(select top 1 emailid from employees where employeenumber= @mgrId)  ";
                Query += "and EmployeeNumber = @empId";

                SqlCommand cmd_SCTDetails = new SqlCommand();
                cmd_SCTDetails.Connection = conn;
                cmd_SCTDetails.CommandText = Query;
                cmd_SCTDetails.Parameters.AddWithValue("@empId", empId_DAL);
                cmd_SCTDetails.Parameters.AddWithValue("@mgrId", mgrId_DAL);


                logger.Info("cmd_SCTDetails Query parameters initialised");

                QueryLog.CmdInfo(cmd_SCTDetails);

                SqlDataAdapter da_SCTDetails = new SqlDataAdapter();
                da_SCTDetails.SelectCommand = cmd_SCTDetails;
                DataSet dSet_SCTDetails = new DataSet();
                da_SCTDetails.Fill(dSet_SCTDetails);

                logger.Info("cmd_SCTDetails excuted by SqlDataAdapter()");

                int Details_count = dSet_SCTDetails.Tables[0].Rows.Count;

                logger.DebugFormat("Row Count : ItemDetails table {0}", Details_count.ToString());

                SCT_Entity result = new SCT_Entity();

                if (Details_count > 0)
                {

                    result.StatusFlag = 0;
                    result.StatusMsg = SCT_Constants.Success;

                    DataRow dr = dSet_SCTDetails.Tables[0].Rows[0];
                    
                    result.EmpId = dr["EmployeeNumber"].ToString();

                    result.CurrBUId = dr["BUId"].ToString();
                    result.CurrSBUId = dr["SBUId"].ToString();
                    result.CurrHorizontalId = dr["Horizontalid"].ToString();
                    result.CurrOrgId = dr["OrgId"].ToString();
                    result.CurrCostId = dr["ECS_COST_CENTER"].ToString();
                    result.CurrPersonalId = dr["PAId"].ToString();
                    result.CurrSubAreaId = dr["ECS_SUB_AREA"].ToString();
                    result.CurrBLocId = dr["BaselocationId"].ToString();
                    result.CurrCDeployId = dr["CenterofDeployment"].ToString(); 

                    result.CurrentBU = dr["BU_Description"].ToString();
                    result.CurrentSBU = dr["SBU_DESCRIPTION"].ToString();
                    result.CurrentHorizontal = dr["Horizontal"].ToString();
                    result.CurrentOrg = dr["OrgDesc"].ToString();
                    result.CurrentCost = dr["CostDesc"].ToString();
                    result.CurrentPersonal = dr["PADESC"].ToString();
                    result.CurrentSubArea = dr["SADESC"].ToString();
                    result.CurrentGrade = dr["Grade"].ToString();
                    result.CurrBLocation = dr["BLocDesc"].ToString();
                    result.CurrCDeploy = dr["CDeployDesc"].ToString();

                }

                else
                {
                    result.StatusFlag = 1;
                    result.StatusMsg = "No records Found";
                }


                logger.Info("Method : searchEmployee_DAL Stop");

                return result;

            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Exception Occured At SCT_DAL - getSCEmployeeDetails_DAL");
                logger.Debug("Exception Code : " + dbEx.Number.ToString());
                logger.Debug("Exception Description : " + dbEx.Message.ToString());
                logger.Error("Error : getSCEmployeeDetails_DAL Stop");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception Occured At SCT_DAL - getSCEmployeeDetails_DAL  : " + ex.Message.ToString());
                logger.Error("Error : getSCEmployeeDetails_DAL Stop");

                throw ex;
            }
            finally
            {
                logger.Debug("Connection Status Closed ");

                //conn.Dispose();
            }
        }
        
       

        /// <summary>
        /// This function Updates SCT Details into the Database 
        /// </summary>
        /// <param name="is_entry_DAL"></param>
        /// <returns>
        /// <paramref name="result"/>
        /// </returns>
        /// <history>
        ///     Hari haran      08/05/2012      created
        /// </history>

        public SCT_UpdateOutputEntity updateSCEmployeeDetails_DAL(SCT_UpdateInputEntity entry_DAL)
        {

            logger.Info("Method : updateSCEmployeeDetails_DAL Start");
            logger.DebugFormat("Input Parameter entry_BAL : EmpNo value = {0} and WEF = {1}", entry_DAL.EmpNo, entry_DAL.WEF);           

            databaseLayer dbConStr = new databaseLayer();
            string connStr = dbConStr.connectionInfo;
            logger.Debug("Connection string : " + connStr);
            SqlConnection conn = new SqlConnection(connStr);
            SqlTransaction Trans = null;

            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
                logger.Debug("Connection Status opened ");
            }

            logger.Info("Connetion to the database established");
            try
            {
                SCT_UpdateOutputEntity result = new SCT_UpdateOutputEntity();

                SqlCommand insert_cmd = new SqlCommand();
                insert_cmd.Connection = conn;
                insert_cmd.CommandType = CommandType.StoredProcedure;
                Trans = conn.BeginTransaction("InsertTransaction");
                insert_cmd.Transaction = Trans;         
                string insertQuery = "HRMSACInsertRequestChange";
                insert_cmd.CommandText = insertQuery;

                insert_cmd.Parameters.AddWithValue("@EMPNO", Convert.ToInt32(entry_DAL.EmpNo));
                insert_cmd.Parameters.AddWithValue("@REASON", entry_DAL.Reason);
                insert_cmd.Parameters.AddWithValue("@WEF", Convert.ToDateTime(entry_DAL.WEF).ToString());
                insert_cmd.Parameters.AddWithValue("@CURRENTSBU",  entry_DAL.CurrentBU);
                insert_cmd.Parameters.AddWithValue("@NEWSBU",  entry_DAL.NewBU);
                insert_cmd.Parameters.AddWithValue("@CURRENTBU",  entry_DAL.CurrentSBU);
                insert_cmd.Parameters.AddWithValue("@NEWBU",  entry_DAL.NewSBU);
                insert_cmd.Parameters.AddWithValue("@CURRENTHORIZONTAL",  entry_DAL.CurrentHorizontal);
                insert_cmd.Parameters.AddWithValue("@NEWHORIZONTAL",  entry_DAL.NewHorizontal);
                insert_cmd.Parameters.AddWithValue("@CURRENTORG",  entry_DAL.CurrentOrg);
                insert_cmd.Parameters.AddWithValue("@NEWORG",  entry_DAL.NewOrg);
                insert_cmd.Parameters.AddWithValue("@CURRENTCOST",  entry_DAL.CurrentCost);
                insert_cmd.Parameters.AddWithValue("@NEWCOST",  entry_DAL.NewCost);
                insert_cmd.Parameters.AddWithValue("@CURRENTPERSONAL",  entry_DAL.CurrentPersonal);
                insert_cmd.Parameters.AddWithValue("@NEWPERSONAL",  entry_DAL.NewPersonal);
                insert_cmd.Parameters.AddWithValue("@CURRENTSUBAREA",  entry_DAL.CurrentSubArea);
                insert_cmd.Parameters.AddWithValue("@NEWSUBAREA",  entry_DAL.NewSubArea);
                insert_cmd.Parameters.AddWithValue("@CURRENTGRADE",  entry_DAL.CurrentGrade);
                insert_cmd.Parameters.AddWithValue("@NEWGRADE",  entry_DAL.NewGrade);
                insert_cmd.Parameters.AddWithValue("@CURRENTSUPPERID",  Convert.ToInt32(entry_DAL.CurrentSuperId));
                insert_cmd.Parameters.AddWithValue("@NEWSUPPERID",  Convert.ToInt32(entry_DAL.NewSuperId));
                insert_cmd.Parameters.AddWithValue("@SUBMMITEDCOMMENTS",  entry_DAL.SubmittedComment);
                insert_cmd.Parameters.AddWithValue("@SUBMITTEDBY",  Convert.ToInt32(entry_DAL.SubmittedBy));
                insert_cmd.Parameters.AddWithValue("@SUBMITTEDFROMIP",  DBNull.Value);
                insert_cmd.Parameters.AddWithValue("@STATUS",  "S");
                insert_cmd.Parameters.AddWithValue("@SUBMITTEDDATE", System.DateTime.Now.ToString("yyyy-MM-dd H:mm:ss.fff"));
                insert_cmd.Parameters.AddWithValue("@CURRENTET_PLACE",  entry_DAL.CurrBLocation);
                insert_cmd.Parameters.AddWithValue("@NEWET_PLACE",  entry_DAL.NewBLocation);
                insert_cmd.Parameters.AddWithValue("@REPORTING_TYPE",  'O');
                insert_cmd.Parameters.AddWithValue("@CUR_CENTER_DEPLOYMENT",  entry_DAL.CurrCDeploy);
                insert_cmd.Parameters.AddWithValue("@NEW_CENTER_DEPLOYMENT", entry_DAL.NewCDeploy);

                logger.Info("Inserting Details of SCT UpdateInput Entity");

                QueryLog.CmdInfo(insert_cmd);

                //SqlDataReader dr;
                int rowsAffected = insert_cmd.ExecuteNonQuery();
                //if (dr.HasRows)
                if (rowsAffected == 1)
                {
                    //dr.Read();
                    //if (dr["Return Value"].Equals(0))
                    //{
                        //dr.Close();
                        Trans.Commit();
                        result.StatusFlag = 0;
                        result.Message = SCT_Constants.Success;

                        logger.Info("Operation : Insert operation executed successfully");
                }
                else
                {
                    //dr.Close();
                    Trans.Rollback("InsertTransaction");

                    logger.Debug("Transaction Rollback Executed");

                    result.StatusFlag = 1;
                    result.Message = SCT_Constants.Error;

                    logger.Error("Operation : Insert operation Failed");
                }

  
                return result;
            }
            catch (SqlException dbEx)
            {
                logger.Fatal("SQL Exception Occured At SCT_DAL - updatePurchaseRequest_DAL  : ");
                logger.Error("Exception Code : " + dbEx.Number.ToString());
                logger.Error("Exception Description : " + dbEx.Message.ToString());
                logger.Error("Error : updateSCEmployeeDetails_DAL Stop");
                try
                {
                    logger.Debug("Transaction Rollback Executed");
                    Trans.Rollback("InsertTransaction");
                }
                catch (Exception ex2)
                {
                    logger.Error("Tranasction Rollback Failed : " + ex2.Message.ToString());
                    //throw ex2;
                }
                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception Occured At SCT_DAL - updateSCEmployeeDetails_DAL  : " + ex.Message.ToString());
                logger.Error("Method : updateSCEmployeeDetails_DAL Stop");

                try
                {
                    logger.Debug("Transaction Rollback Executed");
                    Trans.Rollback("InsertTransaction");
                }
                catch (Exception ex2)
                {
                    logger.Error("Transaction Rollback Failed : "+ex2.Message.ToString());
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
