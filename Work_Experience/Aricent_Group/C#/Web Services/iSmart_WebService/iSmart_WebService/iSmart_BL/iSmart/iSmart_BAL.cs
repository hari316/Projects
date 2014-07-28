using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;


namespace iSmart_BL.iSmart
{
    public class iSmart_BAL
    {
        public iSmart_BAL()
        {
            // TODO: Add constructor logic here 
        }

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(iSmart_BAL));
        private static readonly int count = 0;


        /// <summary>
        /// This Method validates the input parameter for the getPurchaseRequestDetails function
        /// </summary>
        /// <param name="PReqNo_BAL"></param>
        /// <returns></returns>
        /// <history>
        ///     Hari haran      07/05/2012      created
        /// </history>

        public iSmartEntity getPurchaseRequestDetails_BAL(string PReqNo_BAL)
        {
            try
            {
                logger.Debug("iSmart_BAL: getPurchaseRequestDetails_BAL() called");
                logger.Debug("PReqNo value : " + PReqNo_BAL);

                if ( string.IsNullOrEmpty(PReqNo_BAL) )
                {
                    iSmartEntity is_Details = new iSmartEntity();
                    is_Details.IS_headerDetails.ErrorCode = 21;
                    is_Details.IS_headerDetails.ErrorMessage = iSmart_Constants.PReqNoNull;

                    logger.Debug("Method getiSmartDetails_BAL : ErrorCode = " + is_Details.IS_headerDetails.ErrorCode.ToString());
                    logger.Debug("Method getiSmartDetails_BAL : ErrorMessage = " + is_Details.IS_headerDetails.ErrorMessage);
                    logger.Error("Method : getiSmartDetails_BAL validation failed");

                    return is_Details;
                }

                iSmart_DAL getIS_DAL = new iSmart_DAL();
                return (getIS_DAL.getPurchaseRequestDetails_DAL(PReqNo_BAL));
            }
            catch (SqlException dbEx)
            {
                logger.Error("Exception  At BAL - getPurchaseRequestDetails_BAL  : " + dbEx.Message.ToString());
                logger.Error("timesheet_BAL: getPurchaseRequestDetails_BAL() returning error");
                throw dbEx;
            }
            catch(Exception ex) 
            {
                logger.Error("Exception  At BAL - getPurchaseRequestDetails_BAL  : " + ex.Message.ToString());
                logger.Error("timesheet_BAL: getPurchaseRequestDetails_BAL() returning error");
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
        
        public iSmart_UpdateOutputEntity updatePurchaseRequest_BAL(iSmart_UpdateInputEntity[] entry_BAL)
        {
            try
            {
                logger.Debug("iSmart_BAL: updatePurchaseRequest_BAL() called");
                logger.Debug("updatePurchaseRequest_BAL PReqNo value : " + entry_BAL[0].PReqNo.ToString());

                iSmart_UpdateOutputEntity errRes = new iSmart_UpdateOutputEntity();
                errRes.StatusFlag = 1;
                errRes.Message = iSmart_Constants.Error;
                int validate_tsParamFlag = 0;
 
                validate_tsParamFlag = validate_isParam(entry_BAL);

                logger.Debug("iSmart Input parameter validation flag value(success = 0/failure = 1)  : " + validate_tsParamFlag.ToString());
 
                if (validate_tsParamFlag == 1)
                {
                    logger.Debug("Error in input parameter values");
                    logger.Debug("ErrorCode = " + errRes.StatusFlag.ToString());
                    logger.Debug("ErrorMessage = " + errRes.Message);
                    logger.Error("Method : getiSmartDetails_BAL validation failed");

                    return errRes;
                }
                else
                {
                    string iSmartStatus = string.Empty;
                    iSmart_DAL updateIS_DAL = new iSmart_DAL();
                    iSmartStatus = updateIS_DAL.getISmartStatus(entry_BAL[count].PReqNo);

                    logger.Debug("iSmartStatus value : " + iSmartStatus.ToString());
                    logger.Debug("EmpId value (To be same as iSmartStatus for Updation) : " + entry_BAL[count].EmpID.ToString());

                    if (string.IsNullOrEmpty(iSmartStatus))
                    {
                        errRes.StatusFlag = 1;
                        //errRes.Message = iSmart_Constants.PReqNoInvalid;
                        errRes.Message = string.Format(iSmart_Constants.preqErrFormat, entry_BAL[count].PReqNo); 

                        logger.Debug("ErrorCode = " + errRes.StatusFlag.ToString());
                        logger.Debug("ErrorMessage = " + errRes.Message);
                        logger.Error("Method : getiSmartDetails_BAL validation failed");

                        return errRes;
                    }

                    else if (iSmartStatus.Equals(entry_BAL[count].EmpID))
                    {
                        return (updateIS_DAL.updatePurchaseRequest_DAL(entry_BAL));
                    }

                    else
                    {
                        errRes.StatusFlag = 1;
                        //errRes.Message = "EmpId Miss Match";
                        errRes.Message = string.Format(iSmart_Constants.preqErrFormat,entry_BAL[count].PReqNo); 

                        logger.Debug("ErrorCode = " + errRes.StatusFlag.ToString());
                        logger.Debug("ErrorMessage = " + errRes.Message);
                        logger.Error("Method : updatePurchaseRequest_BAL Stop");

                        return errRes;
                    }
                }
            }
            catch (SqlException dbEx)
            {

                logger.Error("Exception  At BAL - updatePurchaseRequest_BAL  : " + dbEx.Message.ToString());
                logger.Error("iSmart_BAL:updatePurchaseRequest_BAL() returning error"); 

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception  At BAL - updatePurchaseRequest_BAL  : " + ex.Message.ToString());
                logger.Error("iSmart_BAL:updatePurchaseRequest_BAL() returning error"); 

                throw ex;
            }            

        }


        int validate_isParam(iSmart_UpdateInputEntity[] entry_BAL)
        {
            logger.Debug("iSmart_BAL: validate_isParam() called");

            string PreqNo = string.Empty;
            string EmpId = string.Empty;

            PreqNo = entry_BAL[count].PReqNo;
            EmpId = entry_BAL[count].EmpID;

            logger.DebugFormat("Input Parameter entry_BAL[0] : PreqNo value = {0} and EmpId value = {1}", PreqNo.ToString(), EmpId.ToString());

            foreach (iSmart_UpdateInputEntity isEntry_value in entry_BAL)
            {
                if ( string.IsNullOrEmpty(isEntry_value.PReqNo) )
                {
                    logger.Error("PReqNo has NULL reference");
                    
                    return 1;
                }

                if (!isEntry_value.PReqNo.Equals(PreqNo))
                {
                    logger.Error("PReqNo is Not same as Previous Entry");
                    
                    return 1;
                }

                if (isEntry_value.Status == 0)
                {
                    logger.Error("Status value is Invalid");
                    
                    return 1;
                }

                if (string.IsNullOrEmpty(isEntry_value.EmpID))
                {
                    logger.Error("EmpID has NULL reference");
                    
                    return 1;
                }

                if (!isEntry_value.EmpID.Equals(EmpId))
                {
                    logger.Error("EmpID is Not same as Previous Entry");
                    
                    return 1;
                }

                //if (string.IsNullOrEmpty(isEntry_value.PurGrpMember))
                //{
                //    logger.Error("PurGrpMember has NULL reference");
                //    logger.Info("Method : validate_isParam Stop");
                //    return 1;
                //}

                if (string.IsNullOrEmpty(isEntry_value.ItemTransactionId))
                {
                    logger.Error("ItemTransactionId has NULL reference");
                    
                    return 1;
                }

                //if (string.IsNullOrEmpty(isEntry_value.AucNo))
                //{
                //    logger.Error("AucNo has NULL reference");
                //    logger.Info("Method : validate_isParam Stop");
                //    return 1;
                //}

                //if (string.IsNullOrEmpty(isEntry_value.CustomerPONum))
                //{
                //    logger.Error("CustomerPONum has NULL reference");
                //    logger.Info("Method : validate_isParam Stop");
                //    return 1;
                //}

                //if (string.IsNullOrEmpty(isEntry_value.PReqRemarks))
                //{
                //    logger.Error("PReqRemarks has NULL reference");
                //    logger.Info("Method : validate_isParam Stop");
                //    return 1;
                //}

                //if (string.IsNullOrEmpty(isEntry_value.RejectedTo))
                //{
                //    logger.Error("PReqRemarks has NULL reference");
                //    logger.Info("Method : validate_isParam Stop");
                //    return 1;
                //}

                //else
                //{
                //    if (isEntry_value.RejectedTo.IndexOf(',') != -1)
                //    {
                //        string[] getRoleCode = isEntry_value.RejectedTo.Split(',');
                //        isEntry_value.RejectedTo = getRoleCode[1].ToString();
                //    }
                //}

                //if (string.IsNullOrEmpty(isEntry_value.AssignTo))
                //{
                //    logger.Error("AssignTo has NULL reference");
                //    logger.Info("Method : validate_isParam Stop");
                //    return 1;
                //}

                if (string.IsNullOrEmpty(isEntry_value.ActionByRole))
                {
                    logger.Error("ActionByRole has NULL reference");
                    
                    return 1;
                }

            }

            logger.Debug("iSmart_BAL: validate_isParam()  Stop");
            return 0;
        }

    }
}
