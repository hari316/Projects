using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Net;
using System.Data;
using TRT_BL.TaxiEmpDetails;


namespace TRT_BL
{
    public class TRT_BAL
    {
        public TRT_BAL()
        {
            // TODO: Add constructor logic here 
        }

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(TRT_BAL));

        TRTService TaxiWebService = new TRTService();
       

        //***************************************************************************************************

        public TRT_AuthUser authenticateUser_BAL(string UserId_BAL, string Pwd_BAL)
        {
            try
            {
                logger.Debug("TaxiRequest_BAL: authenticateUser_BAL() called");
                logger.DebugFormat("Input parameter Id : {0} ", UserId_BAL);
                logger.DebugFormat("Input parameter Password : {0} ", Pwd_BAL);

                string[] userIds = { "4400", "8297", "7327", "9308" };
                string[] locIds = { "IN-BLR", "IN-CHN", "IN-GUR" };
                string[] userNames = { "Rajat Gautam", "Harish", "Atul", "Deepak Jain" };

                TRT_AuthUser authResult = new TRT_AuthUser();
                authResult.StatusFlag = 1;
                authResult.Message = TRT_Constants.AuthFailure;

                for (int i = 0; i < userIds.Length; i++)
                {
                    if (UserId_BAL.Equals(userIds[i]) && Pwd_BAL.Equals("password"))
                    {
                        authResult.StatusFlag = 0;
                        authResult.Message = TRT_Constants.AuthSuccess;
                        authResult.Name = userNames[i];

                        string chargeCodeXmlString = TaxiWebService.GetAllocatedChargeCodes(UserId_BAL);
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(chargeCodeXmlString);

                        //XmlNode root = xmlDoc.DocumentElement;
                        
                        //XmlNode AllocCCode = root.SelectSingleNode("/NewDataSet/Projects");
                        //GeneralInformationNode.SelectSingleNode("ProjectId").InnerText;
                        XmlNode ProjectsNode;
                        ProjectsNode = xmlDoc.SelectSingleNode("/NewDataSet/Projects");
                        if (ProjectsNode == null)
                        {
                            ProjectsNode = xmlDoc.SelectSingleNode("/Projects");
                            authResult.ChargeCode = ProjectsNode.SelectSingleNode("ProjectId").InnerText.ToString();
                        }
                        else
                        {
                            authResult.ChargeCode = ProjectsNode.SelectSingleNode("ProjectID").InnerText.ToString();
                        }

                        //authResult.ChargeCode = AllocCCode["ProjectID"].InnerText;
                        Random _r = new Random();
                        authResult.LocId = locIds[_r.Next(3)];
                        break;
                    }
                }

                logger.InfoFormat("Authentication StatusFlag value : {0}", authResult.StatusFlag);
                logger.InfoFormat("Authentication Message value : {0}", authResult.Message);
                logger.InfoFormat("Employee Location ID returned: {0}", authResult.LocId);
                logger.InfoFormat("Employee Charge Code returned: {0}", authResult.ChargeCode);
                logger.Debug("TaxiRequest_BAL: authenticateUser_BAL() called");

                return authResult;
            }

            catch (Exception ex)
            {
                logger.Error("Exception  At BAL - authenticateUser_BAL  : " + ex.Message.ToString());
                logger.Error("TaxiRequest_BAL: authenticateUser_BAL() called");

                throw ex;
            }
        }

        //***************************************************************************************************

        public TRT_ChargeCodes getAllChargeCodes_BAL(string SearchCriteria_BAL)
        {
            try
            {
                logger.Debug("TaxiRequest_BAL: getAllChargeCodes_BAL() called");
                logger.DebugFormat("Input parameter Search String : {0} ", SearchCriteria_BAL);

                TRT_DAL getCC_BAL = new TRT_DAL();
                return (getCC_BAL.getAllChargeCodes_DAL(SearchCriteria_BAL));
                
            }

            catch (Exception ex)
            {
                logger.Error("Exception  At BAL - getAllChargeCodes_BAL  : " + ex.Message.ToString());
                logger.Error("TaxiRequest_BAL: getAllChargeCodes_BAL() Stop");

                throw ex;
            }
        }


        //***************************************************************************************************

        public TRT_LocEntity getLocationsDetails_BAL(string LocId_BAL)
        {
            try
            {
                logger.Debug("TaxiRequest_BAL: getLocationsDetails_BAL() called");
                logger.DebugFormat("Input parameter Location ID : {0} ", LocId_BAL);

                if(!string.IsNullOrEmpty(LocId_BAL))
                {
                    TRT_DAL getLD_BAL = new TRT_DAL();
                    return (getLD_BAL.getLocationsDetails_DAL(LocId_BAL));
                }
                else
                {
                    TRT_LocEntity Error = new TRT_LocEntity();
                    Error.TRT_header.statusFlag = 1;
                    Error.TRT_header.statusMsg = "Location ID is Null/Empty";

                    return Error;
                }

            }

            catch (Exception ex)
            {
                logger.Error("Exception  At BAL - getLocationsDetails_BAL  : " + ex.Message.ToString());
                logger.Error("TaxiRequest_BAL: getLocationsDetails_BAL() Stop");

                throw ex;
            }
        }


        //***************************************************************************************************

        public TRT_OutputEntity updateTaxiRequestDetails_BAL(TRT_InputEntity TRT_EntryBAL)
        {
            try
            {
                logger.Debug("TaxiRequest_BAL: updateTaxiRequestDetails_BAL() called");
                logger.DebugFormat("Input parameter Employee ID : {0} ", TRT_EntryBAL);


                int validate_trtParamFlag = 0;
                validate_trtParamFlag = validate_trtParam(TRT_EntryBAL);

                if (validate_trtParamFlag == 1)
                {
                    TRT_OutputEntity Error = new TRT_OutputEntity();
                    Error.statusFlag = 1;
                    Error.statusMsg = TRT_Constants.Invalid;

                    logger.Error("Error in input parameter values");
                    logger.Error("ErrorCode = " + Error.statusFlag.ToString());
                    logger.Error("ErrorMessage = " + Error.statusMsg);
                    logger.Error("TaxiRequest_BAL: authenticateUser_SI() Stop");

                    return Error;
                }
                else
                {
                    TRT_DAL getLD_BAL = new TRT_DAL();
                    return (getLD_BAL.updateTaxiRequestDetails_DAL_New(TRT_EntryBAL));
                }    

            }
            catch (SqlException dbEx)
            {
                logger.Error("Exception  At BAL - updateTaxiRequestDetails_BAL  : " + dbEx.Message.ToString());
                logger.Error("Method - updateTaxiRequestDetails_BAL Stop");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Error("Exception  At BAL - updateTaxiRequestDetails_BAL  : " + ex.Message.ToString());
                logger.Error("Method : updateTaxiRequestDetails_BAL Stop");
                throw ex;
            }
        }

        /// <summary>
        /// This Method validates the input parameter for the searchCriteria function
        /// </summary>
        /// <param name="searchCriteria_BAL"></param>
        /// <returns></returns>
        /// <history>
        ///     Hari haran      07/05/2012      created
        /// </history>
        /// 

        int validate_trtParam(TRT_InputEntity TRT_EntryBAL)
        {
            int flag = 0;
            logger.Debug("TaxiRequest_BAL: validate_trtParam() called");


            if (string.IsNullOrEmpty(TRT_EntryBAL.empNo))     
            {
                flag = 1;
                return flag;
            }

            if (string.IsNullOrEmpty(TRT_EntryBAL.location))
            {
                flag = 1;
                return flag;
            }

            if (string.IsNullOrEmpty(TRT_EntryBAL.plot))
            {
                flag = 1;
                return flag;
            }

            if (string.IsNullOrEmpty(TRT_EntryBAL.purpose))
            {
                flag = 1;
                return flag;
            }

            if (string.IsNullOrEmpty(TRT_EntryBAL.vehicleType))
            {
                flag = 1;
                return flag;
            }

            if (string.IsNullOrEmpty(TRT_EntryBAL.location))
            {
                flag = 1;
                return flag;
            }

            if (string.IsNullOrEmpty(TRT_EntryBAL.chargeCode))
            {
                flag = 1;
                return flag;
            }

            if (string.IsNullOrEmpty(TRT_EntryBAL.fromPlace))
            {
                flag = 1;
                return flag;
            }

            if (string.IsNullOrEmpty(TRT_EntryBAL.toPlace))
            {
                flag = 1;
                return flag;
            }

            if (string.IsNullOrEmpty(TRT_EntryBAL.comments))
            {
                flag = 1;
                return flag;
            }

            if (string.IsNullOrEmpty(TRT_EntryBAL.reqDate))
            {
                flag = 1;
                return flag;
            }

            if (TRT_EntryBAL.hhTime > 24 || TRT_EntryBAL.hhTime < 0)
            {
                flag = 1;
                return flag;
            }

            if (TRT_EntryBAL.mmTime > 60 || TRT_EntryBAL.mmTime < 0)
            {
                flag = 1;
                return flag;
            }

            if (TRT_EntryBAL.duration == 0)
            {
                flag = 1;
                return flag;
            }

            logger.Debug("TaxiRequest_BAL: validate_trtParam() Stop");
            return flag;

        }

           
    }
}
