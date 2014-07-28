using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;


namespace SCT_BL.SCT
{
    public class SCT_BAL
    {
        public SCT_BAL()
        {
            // TODO: Add constructor logic here 
        }

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(SCT_BAL));

        //***************************************************************************************************

        public SCT_AuthUser authenticateUser_BAL(string UserId_BAL, string Pwd_BAL)
        {
            try
            {
                logger.Info("Method : authenticateUser_BAL Start");
                logger.DebugFormat("Input parameter Id : {0} ", UserId_BAL);
                logger.DebugFormat("Input parameter Password : {0} ", Pwd_BAL);

                string[] userIds = {"4400","8297","7327","9308"};
                string[] userNames = { "Rajat Gautam", "Harish", "Atul", "Deepak Jain" };
                SCT_AuthUser authResult = new SCT_AuthUser();
                authResult.StatusFlag = 1;
                authResult.Message = SCT_Constants.AuthFailure;

                for (int i = 0; i < userIds.Length; i++)
                {
                    if (UserId_BAL.Equals(userIds[i]) && Pwd_BAL.Equals("password"))
                    {
                        authResult.StatusFlag = 0;
                        authResult.Message = SCT_Constants.AuthSuccess;
                        authResult.Name = userNames[i];
                        break;
                    }
                }

                logger.DebugFormat("Authentication StatusFlag value : {0}", authResult.StatusFlag);
                logger.DebugFormat("Authentication Message value : {0}", authResult.Message);
                logger.Info("Method : authenticateUser_BAL Stop");
                return authResult;
            }

            catch (Exception ex)
            {
                logger.Fatal("Exception  At BAL - authenticateUser_BAL  : " + ex.Message.ToString());
                logger.Error("Method : authenticateUser_BAL Stop");
                throw ex;
            }
        }

        // <summary>
        /// This Method validates the input parameter for the getPurchaseRequestDetails function
        /// </summary>
        /// <param name="PReqNo_BAL"></param>
        /// <returns></returns>
        /// <history>
        ///     Hari haran      07/05/2012      created
        /// </history>

        public SCT_Emp searchEmployee_BAL(string Name_BAL, string MgrId_BAL, string Flag)
        {
            try
            {  
                logger.Info("Method : searchEmployee_BAL Start");

                int validateParam = searchParam(Name_BAL, MgrId_BAL, Flag);

                if ( validateParam == 1 )
                {
                    SCT_Emp sct_Details = new SCT_Emp();
                    sct_Details.SCT_headerDetails.StatusFlag = 21;
                    sct_Details.SCT_headerDetails.StatusMsg = SCT_Constants.ParamNull;

                    logger.Debug("Method searchEmployee_BAL : ErrorCode = " + sct_Details.SCT_headerDetails.StatusFlag.ToString());
                    logger.Debug("Method searchEmployee_BAL : ErrorMessage = " + sct_Details.SCT_headerDetails.StatusMsg);
                    logger.Error("Method : searchEmployee_BAL Stop");

                    return sct_Details;
                }

                SCT_DAL search_DAL = new SCT_DAL();
                return (search_DAL.searchEmployee_DAL(Name_BAL, MgrId_BAL, Flag));
            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Exception  At BAL - searchEmployee_BAL  : " + dbEx.Message.ToString());
                logger.Error("Method : searchEmployee_BAL Stop");
                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception  At BAL - searchEmployee_BAL  : " + ex.Message.ToString());
                logger.Error("Method : searchEmployee_BAL Stop");
                throw ex;
            }
        }

        //***************************************************************************************************

        int searchParam(string Name_BAL, string MgrId_BAL, string Flag)
        {
            logger.Info("Method : searchParam Start");
            logger.DebugFormat("Input parameter Name : {0} ", Name_BAL);
            logger.DebugFormat("Input parameter ManagerId : {0} ", MgrId_BAL);
            logger.DebugFormat("Input parameter Flag : {0} ", Flag);

            if (string.IsNullOrEmpty(Name_BAL))
            {
                logger.Error("Search Criteria :Name has Null reference");
                logger.Info("Method : searchParam Stop");
                return 1;
            }
            if(string.IsNullOrEmpty(MgrId_BAL))
            {
                logger.Error("Manager ID :MgrID has Null reference");
                logger.Info("Method : searchParam Stop");
                return 1;
            }
            if (string.IsNullOrEmpty(Flag))
            {
                logger.Error("Search Type (0 - Manager Search and 1 - Employee Search) :Flag has Null reference");
                logger.Info("Method : searchParam Stop");
                return 1;
            }    

            logger.Info("Method : searchParam Stop");

            return 0;
        }

        //***************************************************************************************************

        public SCT_PA getPersonnelArea_BAL()
        {
            try
            {
                logger.Info("Method : getPersonnelArea_BAL Start");

                SCT_DAL search_DAL = new SCT_DAL();
                return (search_DAL.getPersonnelArea_DAL());
            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Exception  At BAL - getPersonnelArea_BAL  : " + dbEx.Message.ToString());
                logger.Error("Method : getPersonnelArea_BAL Stop");
                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception  At BAL - getPersonnelArea_BAL  : " + ex.Message.ToString());
                logger.Error("Method : getPersonnelArea_BAL Stop");
                throw ex;
            }
        }

        //***************************************************************************************************

        public SCT_RChange getReasonForChange_BAL()
        {
            try
            {
                logger.Info("Method : getReasonForChange_BAL Start");

                SCT_DAL search_DAL = new SCT_DAL();
                return (search_DAL.getReasonForChange_DAL());
            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Exception  At BAL - getReasonForChange_BAL  : " + dbEx.Message.ToString());
                logger.Error("Method : getReasonForChange_BAL Stop");
                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception  At BAL - getReasonForChange_BAL  : " + ex.Message.ToString());
                logger.Error("Method : getReasonForChange_BAL Stop");
                throw ex;
            }
        }

        //***************************************************************************************************

        public SCT_PSA getPersonnelSubArea_BAL()
        {
            try
            {
                logger.Info("Method : getPersonnelSubArea_BAL Start");

                SCT_DAL search_DAL = new SCT_DAL();
                return (search_DAL.getPersonnelSubArea_DAL());
            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Exception  At BAL - getPersonnelSubArea_BAL  : " + dbEx.Message.ToString());
                logger.Error("Method : getPersonnelSubArea_BAL Stop");
                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception  At BAL - getPersonnelSubArea_BAL  : " + ex.Message.ToString());
                logger.Error("Method : getPersonnelSubArea_BAL Stop");
                throw ex;
            }
        }

        //***************************************************************************************************

        public SCT_ORGUnit getOrganizationalUnit_BAL()
        {
            try
            {
                logger.Info("Method : getOrganizationalUnit_BAL Start");

                SCT_DAL get_DAL = new SCT_DAL();
                return (get_DAL.getOrganizationalUnit_DAL());
            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Exception  At BAL - getOrganizationalUnit_BAL  : " + dbEx.Message.ToString());
                logger.Error("Method : getOrganizationalUnit_BAL Stop");
                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception  At BAL - getOrganizationalUnit_BAL  : " + ex.Message.ToString());
                logger.Error("Method : getOrganizationalUnit_BAL Stop");
                throw ex;
            }
        }

        //***************************************************************************************************

        public SCT_CC getCostCenter_BAL()
        {
            try
            {
                logger.Info("Method : getCostCenter_BAL Start");

                SCT_DAL get_DAL = new SCT_DAL();
                return (get_DAL.getCostCenter_DAL());
            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Exception  At BAL - getCostCenter_BAL  : " + dbEx.Message.ToString());
                logger.Error("Method : getCostCenter_BAL Stop");
                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception  At BAL - getCostCenter_BAL  : " + ex.Message.ToString());
                logger.Error("Method : getCostCenter_BAL Stop");
                throw ex;
            }
        }

        //***************************************************************************************************

        public SCT_BU getBusinessUnit_BAL()
        {
            try
            {
                logger.Info("Method : getBusinessUnit_BAL Start");

                SCT_DAL get_DAL = new SCT_DAL();
                return (get_DAL.getBusinessUnit_DAL());
            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Exception  At BAL - getBusinessUnit_BAL  : " + dbEx.Message.ToString());
                logger.Error("Method : getBusinessUnit_BAL Stop");
                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception  At BAL - getBusinessUnit_BAL  : " + ex.Message.ToString());
                logger.Error("Method : getBusinessUnit_BAL Stop");
                throw ex;
            }
        }

        //***************************************************************************************************

        public SCT_SBU getStrategicBusinessUnit_BAL()
        {
            try
            {
                logger.Info("Method : getStrategicBusinessUnit_BAL Start");

                SCT_DAL get_DAL = new SCT_DAL();
                return (get_DAL.getStrategicBusinessUnit_DAL());
            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Exception  At BAL - getStrategicBusinessUnit_BAL  : " + dbEx.Message.ToString());
                logger.Error("Method : getStrategicBusinessUnit_BAL Stop");
                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception  At BAL - getStrategicBusinessUnit_BAL  : " + ex.Message.ToString());
                logger.Error("Method : getStrategicBusinessUnit_BAL Stop");
                throw ex;
            }
        }

        //***************************************************************************************************

        public SCT_Horizontal getHorizontal_BAL()
        {
            try
            {
                logger.Info("Method : getHorizontal_BAL Start");

                SCT_DAL get_DAL = new SCT_DAL();
                return (get_DAL.getHorizontal_DAL());
            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Exception  At BAL - getHorizontal_BAL  : " + dbEx.Message.ToString());
                logger.Error("Method : getHorizontal_BAL Stop");
                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception  At BAL - getHorizontal_BAL  : " + ex.Message.ToString());
                logger.Error("Method : getHorizontal_BAL Stop");
                throw ex;
            }
        }
        //***************************************************************************************************

        public SCT_BLoc getBaseLocation_BAL()
        {
            try
            {
                logger.Info("Method : getBaseLocation_BAL Start");

                SCT_DAL get_DAL = new SCT_DAL();
                return (get_DAL.getBaseLocation_DAL());
            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Exception  At BAL - getBaseLocation_BAL  : " + dbEx.Message.ToString());
                logger.Error("Method : getBaseLocation_BAL Stop");
                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception  At BAL - getBaseLocation_BAL  : " + ex.Message.ToString());
                logger.Error("Method : getBaseLocation_BAL Stop");
                throw ex;
            }
        }
        //***************************************************************************************************

        public SCT_CDeploy getCenterOfDeployment_BAL()
        {
            try
            {
                logger.Info("Method : getCenterOfDeployment_BAL Start");

                SCT_DAL get_DAL = new SCT_DAL();
                return (get_DAL.getCenterOfDeployment_DAL());
            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Exception  At BAL - getCenterOfDeployment_BAL  : " + dbEx.Message.ToString());
                logger.Error("Method : getCenterOfDeployment_BAL Stop");
                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception  At BAL - getCenterOfDeployment_BAL  : " + ex.Message.ToString());
                logger.Error("Method : getCenterOfDeployment_BAL Stop");
                throw ex;
            }
        }

        //***************************************************************************************************

        public SCT_Entity getSCEmployeeDetails_BAL(string empId_BAL, string mgrId_BAL)
        {
            try
            {
                logger.Info("Method : getSCEmployeeDetails_BAL Start");

                if (string.IsNullOrEmpty(empId_BAL) || string.IsNullOrEmpty(mgrId_BAL))
                {
                    SCT_Entity sct_Error = new SCT_Entity();
                    sct_Error.StatusFlag = 21;
                    sct_Error.StatusMsg = SCT_Constants.IdNull;

                    logger.Debug("Method getSCEmployeeDetails_BAL : ErrorCode = " + sct_Error.StatusFlag.ToString());
                    logger.Debug("Method getSCEmployeeDetails_BAL : ErrorMessage = " + sct_Error.StatusMsg);
                    logger.Error("Method : getSCEmployeeDetails_BAL Stop");

                    return sct_Error;
                }
                SCT_DAL get_DAL = new SCT_DAL();
                return (get_DAL.getSCEmployeeDetails_DAL(empId_BAL,mgrId_BAL));
            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Exception  At BAL - getSCEmployeeDetails_BAL  : " + dbEx.Message.ToString());
                logger.Error("Method : getSCEmployeeDetails_BAL Stop");
                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception  At BAL - getSCEmployeeDetails_BAL  : " + ex.Message.ToString());
                logger.Error("Method : getSCEmployeeDetails_BAL Stop");
                throw ex;
            }
        }

        /// <summary>
        /// This Method validates the input parameters for the updatePurchaseRequest function
        /// </summary>
        /// <param name="entry_BAL"></param>
        /// <returns></returns>
        /// <history>
        ///     Hari haran      08/05/2012      created
        /// </history>

        public SCT_UpdateOutputEntity updateSCEmployeeDetails_BAL(SCT_UpdateInputEntity entry_BAL)
        {
            try
            {
                logger.Info("Method : updateSCEmployeeDetails_BAL Start");
                logger.Debug("Method : updateSCEmployeeDetails_BAL RequestId value : " + entry_BAL.EmpNo.ToString());

                SCT_UpdateOutputEntity errRes = new SCT_UpdateOutputEntity();
                errRes.StatusFlag = 1;
                errRes.Message = SCT_Constants.Error;
                int validate_sctParamFlag = 0;
 
                validate_sctParamFlag = validate_sctParam(entry_BAL);

                logger.Debug("SCT Input parameter validation flag value(success = 0/failure = 1)  : " + validate_sctParamFlag.ToString());
 
                if (validate_sctParamFlag == 1)
                {
                    logger.Debug("Error in input parameter values");
                    logger.Debug("ErrorCode = " + errRes.StatusFlag.ToString());
                    logger.Debug("ErrorMessage = " + errRes.Message);
                    logger.Error("Method : updateSCEmployeeDetails_BAL Stop");

                    return errRes;
                }
                else
                {
                    SCT_DAL updateSCT_DAL = new SCT_DAL();
                    return (updateSCT_DAL.updateSCEmployeeDetails_DAL(entry_BAL));                   
                }
            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Exception  At BAL - updateSCEmployeeDetails_BAL  : " + dbEx.Message.ToString());
                logger.Error("Method : updateSCEmployeeDetails_BAL Stop");
                
                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception  At BAL - updateSCEmployeeDetails_BAL  : " + ex.Message.ToString());
                logger.Error("Method : updateSCEmployeeDetails_BAL Stop");

                throw ex;
            }            

        }


        int validate_sctParam(SCT_UpdateInputEntity entry_BAL)
        {
            logger.Info("Method : validate_sctParam Start");

            logger.DebugFormat("Input Parameter entry_BAL : EmpNo value = {0} and WEF = {1}", entry_BAL.EmpNo, entry_BAL.WEF);

            if (string.IsNullOrEmpty(entry_BAL.EmpNo))
            {
                logger.Error("EmpNo has NULL reference");
                logger.Info("Method : validate_sctParam Stop");
                return 1;
            }

            if (string.IsNullOrEmpty(entry_BAL.WEF))
            {
                logger.Error("WEF has NULL reference");
                logger.Info("Method : validate_sctParam Stop");
                return 1;
            }

            if (string.IsNullOrEmpty(entry_BAL.CurrentBU))
            {
                logger.Error("CurrentBU has NULL reference");
                logger.Info("Method : validate_sctParam Stop");
                return 1;
            }
            
            if (string.IsNullOrEmpty(entry_BAL.NewBU))
            {
                logger.Error("NewBU has NULL reference");
                logger.Info("Method : validate_sctParam Stop");
                return 1;
            }

            if (string.IsNullOrEmpty(entry_BAL.CurrentSBU))
            {
                logger.Error("CurrentSBU has NULL reference");
                logger.Info("Method : validate_sctParam Stop");
                return 1;
            }

            if (string.IsNullOrEmpty(entry_BAL.NewSBU))
            {
                logger.Error("NewSBU has NULL reference");
                logger.Info("Method : validate_sctParam Stop");
                return 1;
            }

            if (string.IsNullOrEmpty(entry_BAL.CurrentHorizontal))
            {
                logger.Error("CurrentHorizontal has NULL reference");
                logger.Info("Method : validate_sctParam Stop");
                return 1;
            }

            if (string.IsNullOrEmpty(entry_BAL.NewHorizontal))
            {
                logger.Error("NewHorizontal has NULL reference");
                logger.Info("Method : validate_sctParam Stop");
                return 1;
            }

            if (string.IsNullOrEmpty(entry_BAL.CurrentOrg))
            {
                logger.Error("CurrentOrg has NULL reference");
                logger.Info("Method : validate_sctParam Stop");
                return 1;
            }

            if (string.IsNullOrEmpty(entry_BAL.NewOrg))
            {
                logger.Error("NewOrg has NULL reference");
                logger.Info("Method : validate_sctParam Stop");
                return 1;
            }

            if (string.IsNullOrEmpty(entry_BAL.CurrentCost))
            {
                logger.Error("CurrentCost has NULL reference");
                logger.Info("Method : validate_sctParam Stop");
                return 1;
            }

            if (string.IsNullOrEmpty(entry_BAL.NewCost))
            {
                logger.Error("NewCost has NULL reference");
                logger.Info("Method : validate_sctParam Stop");
                return 1;
            }

            if (string.IsNullOrEmpty(entry_BAL.CurrentPersonal))
            {
                logger.Error("CurrentPersonal has NULL reference");
                logger.Info("Method : validate_sctParam Stop");
                return 1;
            }

            if (string.IsNullOrEmpty(entry_BAL.NewPersonal))
            {
                logger.Error("NewPersonal has NULL reference");
                logger.Info("Method : validate_sctParam Stop");
                return 1;
            }

            if (string.IsNullOrEmpty(entry_BAL.CurrentSubArea))
            {
                logger.Error("CurrentSubArea has NULL reference");
                logger.Info("Method : validate_sctParam Stop");
                return 1;
            }

            if (string.IsNullOrEmpty(entry_BAL.NewSubArea))
            {
                logger.Error("NewSubArea has NULL reference");
                logger.Info("Method : validate_sctParam Stop");
                return 1;
            }

            if (string.IsNullOrEmpty(entry_BAL.CurrentGrade))
            {
                logger.Error("CurrentGrade has NULL reference");
                logger.Info("Method : validate_sctParam Stop");
                return 1;
            }

            if (string.IsNullOrEmpty(entry_BAL.NewGrade))
            {
                logger.Error("NewGrade has NULL reference");
                logger.Info("Method : validate_sctParam Stop");
                return 1;
            }
            
            if (string.IsNullOrEmpty(entry_BAL.SubmittedBy))
            {
                logger.Error("SubmittedBy has NULL reference");
                logger.Info("Method : validate_sctParam Stop");
                return 1;
            }

            if (string.IsNullOrEmpty(entry_BAL.CurrBLocation))
            {
                logger.Error("CurrBLocation has NULL reference");
                logger.Info("Method : validate_sctParam Stop");
                return 1;
            }

             if (string.IsNullOrEmpty(entry_BAL.NewBLocation))
            {
                logger.Error("NewBLocation has NULL reference");
                logger.Info("Method : validate_sctParam Stop");
                return 1;
            }

            if (string.IsNullOrEmpty(entry_BAL.CurrBLocation))
            {
                logger.Error("CurrBLocation has NULL reference");
                logger.Info("Method : validate_sctParam Stop");
                return 1;
            }

            if (string.IsNullOrEmpty(entry_BAL.CurrCDeploy))
            {
                logger.Error("CurrCDeploy has NULL reference");
                logger.Info("Method : validate_sctParam Stop");
                return 1;
            }

            if (string.IsNullOrEmpty(entry_BAL.NewCDeploy))
            {
                logger.Error("NewCDeploy has NULL reference");
                logger.Info("Method : validate_sctParam Stop");
                return 1;
            }

            if (string.IsNullOrEmpty(entry_BAL.CurrentSuperId))
            {
                logger.Error("CurrentSuperId has NULL reference");
                logger.Info("Method : validate_sctParam Stop");
                return 1;
            }

            if (string.IsNullOrEmpty(entry_BAL.NewSuperId))
            {
                logger.Error("NewSuperId has NULL reference");
                logger.Info("Method : validate_sctParam Stop");
                return 1;
            }   

            if (entry_BAL.CurrentSuperId.Equals(entry_BAL.NewSuperId))
            {
                logger.Error("Current Supervisor Id is same as New Supervisor Id");
                logger.Info("Method : validate_sctParam Stop");
                return 1;
            }        

            logger.Info("Method : validate_sctParam Stop");
            return 0;
        }

    }
}
