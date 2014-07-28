using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;


namespace PST_BL
{
    public class PST_BAL
    {
        public PST_BAL()
        {
            // TODO: Add constructor logic here 
        }

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(PST_BAL));
        int iStart = 0;
        int iStop = 0;

        /// <summary>
        /// This Method validates the input parameter for the searchCriteria function
        /// </summary>
        /// <param name="searchCriteria_BAL"></param>
        /// <returns></returns>
        /// <history>
        ///     Hari haran      07/05/2012      created
        /// </history>

        public PST_OutputEntity employeeSearch_BAL(PST_InputEntity searchCriteria_BAL)
        {
            try
            {
                logger.Info("Method : searchCriteria_BAL Start");
                logger.DebugFormat("Employee Name : {0} ",searchCriteria_BAL.name);

                int validate_tsParamFlag = 0;
                validate_tsParamFlag = validate_tsParam(searchCriteria_BAL);
                if (validate_tsParamFlag == 1)
                {
                    PST_OutputEntity errRes = new PST_OutputEntity();
                    errRes.PST_headerDetails.statusFlag = 1;
                    errRes.PST_headerDetails.statusMsg = PST_Constants.Invalid;

                    logger.Debug("Error in input parameter values");
                    logger.Debug("ErrorCode = " + errRes.PST_headerDetails.statusFlag.ToString());
                    logger.Debug("ErrorMessage = " + errRes.PST_headerDetails.statusMsg);
                    logger.Error("Method : searchCriteria_BAL Stop");

                    return errRes;
                }
                else
                {
                    PST_DAL search_DAL = new PST_DAL();
                    return (search_DAL.employeeSearch_DAL(searchCriteria_BAL,iStart,iStop));
                }

            }
            catch (OracleException dbEx)
            {
                logger.Fatal("Exception  At BAL - searchCriteria_BAL  : " + dbEx.Message.ToString());
                logger.Error("Method : searchCriteria_BAL Stop");
                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception  At BAL - searchCriteria_BAL  : " + ex.Message.ToString());
                logger.Error("Method : searchCriteria_BAL Stop");
                throw ex;
            }
        }

        int validate_tsParam(PST_InputEntity searchCriteria_BAL)
        {
            int flag = 0;
            logger.Info("Method : validate_pstParam Start");

            iStart = searchCriteria_BAL.index;
            iStop = iStart + 15;

            if (searchCriteria_BAL.empNo == 0 && (string.IsNullOrEmpty(searchCriteria_BAL.name)) && 
                (string.IsNullOrEmpty(searchCriteria_BAL.sbu)) && (string.IsNullOrEmpty(searchCriteria_BAL.bu)) &&
                (string.IsNullOrEmpty(searchCriteria_BAL.jobDesc)) && (string.IsNullOrEmpty(searchCriteria_BAL.location)))
            {
                flag = 1;
            }

            if ((!string.IsNullOrEmpty(searchCriteria_BAL.name)) && (searchCriteria_BAL.name.Length < 3) || (searchCriteria_BAL.index < 0) )
            {
                flag = 1;
            }

            logger.Info("Method : validate_pstParam Stop");
            return flag;
            
        }

           
    }
}
