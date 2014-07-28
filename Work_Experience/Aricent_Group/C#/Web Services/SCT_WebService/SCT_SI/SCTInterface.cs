using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using SCT_BL.SCT;

namespace SCT_SI.SCT
{
    public class SCTInterface
    {
        public SCTInterface()
        {
            // TODO: Add constructor logic here 
        }

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(SCTInterface));

        /// <summary>
        /// This Method is a authenticates the user
        /// </summary>
        /// <param name="PReqNo_SI"></param>
        /// <returns></returns>
        /// <history>
        ///     Hari haran      07/05/2012      created
        /// </history>

        public SCT_AuthUser authenticateUser_SI(string UserId_SI, string Pwd_SI)
        {
            try
            {
                logger.Info("Control Flow : Method - authenticateUser_SI Start");
                logger.DebugFormat("Input parameter Id : {0} ", UserId_SI);
                logger.DebugFormat("Input parameter Password : {0} ", Pwd_SI);

                SCT_BAL authUsr_BAL = new SCT_BAL();
                return(authUsr_BAL.authenticateUser_BAL(UserId_SI, Pwd_SI));

            }
            catch (Exception ex)
            {
                logger.Fatal("Exception  At SCTInterface - getPurchaseRequestDetails_SI  : " + ex.Message.ToString());
                logger.Error("Control Flow : Method - getPurchaseRequestDetails_SI Stop");

                throw ex;
            }
        }

        /// <summary>
        /// This Method is a interface to getPurchaseRequestDetails function
        /// </summary>
        /// <param name="PReqNo_SI"></param>
        /// <returns></returns>
        /// <history>
        ///     Hari haran      07/05/2012      created
        /// </history>

        public SCT_Emp searchEmployee_SI(string Name_SI, string MgrId_SI, string Flag)
        {
            try
            {
                logger.Info("Control Flow : Method - searchEmployee_SI Start");
                logger.DebugFormat("Input parameter Name : {0} ", Name_SI);
                logger.DebugFormat("Input parameter ManagerId : {0} ", MgrId_SI);

                SCT_BAL search_BAL = new SCT_BAL();
                return (search_BAL.searchEmployee_BAL(Name_SI, MgrId_SI, Flag));
            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Database Exception  At SCTInterface - getPurchaseRequestDetails_SI  : " + dbEx.Message.ToString());
                logger.Error("Control Flow : Method - getPurchaseRequestDetails_SI Stop");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception  At SCTInterface - getPurchaseRequestDetails_SI  : " + ex.Message.ToString());
                logger.Error("Control Flow : Method - getPurchaseRequestDetails_SI Stop");

                throw ex;
            }
        }

        public SCT_RChange getReasonForChange_SI()
        {
            try
            {
                logger.Info("Control Flow : Method : getReasonForChange_SI Start");

                SCT_BAL PA_BAL = new SCT_BAL();
                return (PA_BAL.getReasonForChange_BAL());
            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Database Exception  At SCTInterface - getReasonForChange_SI  : " + dbEx.Message.ToString());
                logger.Error("Control Flow : Method - getReasonForChange_SI Stop");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception  At SCTInterface - getReasonForChange_SI  : " + ex.Message.ToString());
                logger.Error("Control Flow : Method - getReasonForChange_SI Stop");

                throw ex;
            }
        }

        /*****************************************************************************/

        public SCT_PA getPersonnelArea_SI()
        {
            try
            {
                logger.Info("Control Flow : Method : getPersonnelArea_SI Start");

                SCT_BAL PA_BAL = new SCT_BAL();
                return (PA_BAL.getPersonnelArea_BAL());
            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Database Exception  At SCTInterface - getPersonnelArea_SI  : " + dbEx.Message.ToString());
                logger.Error("Control Flow : Method - getPersonnelArea_SI Stop");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception  At SCTInterface - getPersonnelArea_SI  : " + ex.Message.ToString());
                logger.Error("Control Flow : Method - getPersonnelArea_SI Stop");

                throw ex;
            }
        }

        /*****************************************************************************/

        public SCT_PSA getPersonnelSubArea_SI()
        {
            try
            {
                logger.Info("Control Flow : Method : getPersonnelSubArea_SI Start");

                SCT_BAL PA_BAL = new SCT_BAL();
                return (PA_BAL.getPersonnelSubArea_BAL());
            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Database Exception  At SCTInterface - getPersonnelSubArea_SI  : " + dbEx.Message.ToString());
                logger.Error("Control Flow : Method - getPersonnelSubArea_SI Stop");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception  At SCTInterface - getPersonnelSubArea_SI  : " + ex.Message.ToString());
                logger.Error("Control Flow : Method - getPersonnelSubArea_SI Stop");

                throw ex;
            }
        }

        /*****************************************************************************/

        public SCT_ORGUnit getOrganizationalUnit_SI()
        {
            try
            {
                logger.Info("Control Flow : Method : getOrganizationalUnit_SI Start");

                SCT_BAL get_BAL = new SCT_BAL();
                return (get_BAL.getOrganizationalUnit_BAL());
            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Database Exception  At SCTInterface - getOrganizationalUnit_SI  : " + dbEx.Message.ToString());
                logger.Error("Control Flow : Method - getOrganizationalUnit_SI Stop");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception  At SCTInterface - getOrganizationalUnit_SI  : " + ex.Message.ToString());
                logger.Error("Control Flow : Method - getOrganizationalUnit_SI Stop");

                throw ex;
            }
        }
        /*****************************************************************************/

        public SCT_CC getCostCenter_SI()
        {
            try
            {
                logger.Info("Control Flow : Method : getCostCenter_SI Start");

                SCT_BAL get_BAL = new SCT_BAL();
                return (get_BAL.getCostCenter_BAL());
            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Database Exception  At SCTInterface - getCostCenter_SI  : " + dbEx.Message.ToString());
                logger.Error("Control Flow : Method - getCostCenter_SI Stop");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception  At SCTInterface - getCostCenter_SI  : " + ex.Message.ToString());
                logger.Error("Control Flow : Method - getCostCenter_SI Stop");

                throw ex;
            }
        }
        /*****************************************************************************/

        public SCT_BU getBusinessUnit_SI()
        {
            try
            {
                logger.Info("Control Flow : Method : getBusinessUnit_SI Start");

                SCT_BAL get_BAL = new SCT_BAL();
                return (get_BAL.getBusinessUnit_BAL());
            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Database Exception  At SCTInterface - getBusinessUnit_SI  : " + dbEx.Message.ToString());
                logger.Error("Control Flow : Method - getBusinessUnit_SI Stop");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception  At SCTInterface - getBusinessUnit_SI  : " + ex.Message.ToString());
                logger.Error("Control Flow : Method - getBusinessUnit_SI Stop");

                throw ex;
            }
        }
        /*****************************************************************************/

        public SCT_SBU getStrategicBusinessUnit_SI()
        {
            try
            {
                logger.Info("Control Flow : Method : getStrategicBusinessUnit_SI Start");

                SCT_BAL get_BAL = new SCT_BAL();
                return (get_BAL.getStrategicBusinessUnit_BAL());
            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Database Exception  At SCTInterface - getStrategicBusinessUnit_SI  : " + dbEx.Message.ToString());
                logger.Error("Control Flow : Method - getStrategicBusinessUnit_SI Stop");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception  At SCTInterface - getStrategicBusinessUnit_SI  : " + ex.Message.ToString());
                logger.Error("Control Flow : Method - getStrategicBusinessUnit_SI Stop");

                throw ex;
            }
        }
        /*****************************************************************************/

        public SCT_Horizontal getHorizontal_SI()
        {
            try
            {
                logger.Info("Control Flow : Method : getHorizontal_SI Start");

                SCT_BAL get_BAL = new SCT_BAL();
                return (get_BAL.getHorizontal_BAL());
            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Database Exception  At SCTInterface - getHorizontal_SI  : " + dbEx.Message.ToString());
                logger.Error("Control Flow : Method - getHorizontal_SI Stop");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception  At SCTInterface - getHorizontal_SI  : " + ex.Message.ToString());
                logger.Error("Control Flow : Method - getHorizontal_SI Stop");

                throw ex;
            }
        }
        /*****************************************************************************/

        public SCT_BLoc getBaseLocation_SI()
        {
            try
            {
                logger.Info("Control Flow : Method : getBaseLocation Start");

                SCT_BAL get_BAL = new SCT_BAL();
                return (get_BAL.getBaseLocation_BAL());
            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Database Exception  At SCTInterface - getBaseLocation  : " + dbEx.Message.ToString());
                logger.Error("Control Flow : Method - getBaseLocation Stop");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception  At SCTInterface - getBaseLocation  : " + ex.Message.ToString());
                logger.Error("Control Flow : Method - getBaseLocation Stop");

                throw ex;
            }
        }
        /*****************************************************************************/

        public SCT_CDeploy getCenterOfDeployment_SI()
        {
            try
            {
                logger.Info("Control Flow : Method : getCenterOfDeployment_SI Start");

                SCT_BAL get_BAL = new SCT_BAL();
                return (get_BAL.getCenterOfDeployment_BAL());
            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Database Exception  At SCTInterface - getCenterOfDeployment_SI  : " + dbEx.Message.ToString());
                logger.Error("Control Flow : Method - getCenterOfDeployment_SI Stop");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception  At SCTInterface - getCenterOfDeployment_SI  : " + ex.Message.ToString());
                logger.Error("Control Flow : Method - getCenterOfDeployment_SI Stop");

                throw ex;
            }

        }

        /*****************************************************************************/

        public SCT_Entity getSCEmployeeDetails_SI(string empId_SI, string mgrId_SI)
        {
            try
            {
                logger.Info("Control Flow : Method : getSCEmployeeDetails_SI Start");

                SCT_BAL get_BAL = new SCT_BAL();
                return (get_BAL.getSCEmployeeDetails_BAL(empId_SI,mgrId_SI));
            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Database Exception  At SCTInterface - getSCEmployeeDetails_SI  : " + dbEx.Message.ToString());
                logger.Error("Control Flow : Method - getSCEmployeeDetails_SI Stop");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception  At SCTInterface - getSCEmployeeDetails_SI  : " + ex.Message.ToString());
                logger.Error("Control Flow : Method - getSCEmployeeDetails_SI Stop");

                throw ex;
            }

        }
        /*****************************************************************************/

        /// <summary>
        /// This Method is a interface to updatePurchaseRequest fuction
        /// </summary>
        /// <param name="entry_SI"></param>
        /// <returns></returns>
        /// /// <history>
        ///     Hari haran      08/05/2012      created
        /// </history>

        public SCT_UpdateOutputEntity updateSCEmployeeDetails_SI(SCT_UpdateInputEntity entry_SI)
        {
            try
            {
                logger.Info("Control Flow : Method - updateSCEmployeeDetails_SI Start");
                logger.Debug("EmployeeID value : " + entry_SI.EmpNo.ToString());

                SCT_BAL updateTS_BAL = new SCT_BAL();
                return (updateTS_BAL.updateSCEmployeeDetails_BAL(entry_SI));
            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Database Exception  At SCTInterface - updateSCEmployeeDetails_SI  : " + dbEx.Message.ToString());
                logger.Error("Control Flow : Method - updateSCEmployeeDetails_SI Stop");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception  At SCTInterface - updateSCEmployeeDetails_SI  : " + ex.Message.ToString());
                logger.Error("Control Flow : Method - updateSCEmployeeDetails_SI Stop");

                throw ex;
            }
        }
    }
}
