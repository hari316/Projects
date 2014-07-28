using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using TRT_BL;

namespace TRT_SI
{
    public class TRTInterface
    {
        public TRTInterface()
        {
            // TODO: Add constructor logic here 
        }

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(TRTInterface));


        /// <summary>
        /// This Method is a authenticates the user
        /// </summary>
        /// <param name="PReqNo_SI"></param>
        /// <returns></returns>
        /// <history>
        ///     Hari haran      07/05/2012      created
        /// </history>

        public TRT_AuthUser authenticateUser_SI(string UserId_SI, string Pwd_SI)
        {
            try
            {
                logger.Debug("TaxiRequest Interface: authenticateUser_SI() called");
                logger.DebugFormat("Input parameter Id : {0} ", UserId_SI);
                logger.DebugFormat("Input parameter Password : {0} ", Pwd_SI);

                TRT_BAL authUsr_BAL = new TRT_BAL();
                return (authUsr_BAL.authenticateUser_BAL(UserId_SI, Pwd_SI));

            }
            catch (Exception ex)
            {
                logger.Error("Exception  At TRTInterface - authenticateUser_SI  : " + ex.Message.ToString());
                logger.Error("TaxiRequest Interface: authenticateUser_SI() returning error");

                throw ex;
            }
        }

        /// <summary>
        /// This Method is a authenticates the user
        /// </summary>
        /// <param name="PReqNo_SI"></param>
        /// <returns></returns>
        /// <history>
        ///     Hari haran      07/05/2012      created
        /// </history>

        public TRT_ChargeCodes getAllChargeCodes_SI(string SearchCriteria_SI)
        {
            try
            {
                logger.Debug("TaxiRequest Interface: getAllChargeCodes_SI() called");
                logger.DebugFormat("Input parameter Search String : {0} ", SearchCriteria_SI);

                TRT_BAL getCC_BAL = new TRT_BAL();
                return (getCC_BAL.getAllChargeCodes_BAL(SearchCriteria_SI));

            }
            catch (Exception ex)
            {
                logger.Error("Exception  At TRTInterface - getAllChargeCodes_SI  : " + ex.Message.ToString());
                logger.Error("TaxiRequest Interface: getAllChargeCodes_SI() returning error");

                throw ex;
            }
        }

        /// <summary>
        /// This Method is a authenticates the user
        /// </summary>
        /// <param name="PReqNo_SI"></param>
        /// <returns></returns>
        /// <history>
        ///     Hari haran      07/05/2012      created
        /// </history>

        public TRT_LocEntity getLocationsDetails_SI(string LocId_SI)
        {
            try
            {
                logger.Debug("TaxiRequest Interface: getLocationsDetails_SI() called");
                logger.DebugFormat("Input parameter Location ID : {0} ", LocId_SI);

                TRT_BAL getLD_BAL = new TRT_BAL();
                return (getLD_BAL.getLocationsDetails_BAL(LocId_SI));

            }
            catch (Exception ex)
            {
                logger.Error("Exception  At TRTInterface - getLocationsDetails_SI  : " + ex.Message.ToString());
                logger.Error("TaxiRequest Interface: getLocationsDetails_SI() returning error");

                throw ex;
            }
        }


        /// <summary>
        /// This Method is a updates the Taxi Requests in Sharepoint list
        /// </summary>
        /// <param name="PReqNo_SI"></param>
        /// <returns></returns>
        /// <history>
        ///     Hari haran      07/05/2012      created
        /// </history>

        public TRT_OutputEntity updateTaxiRequestDetails_SI(TRT_InputEntity TRT_EntrySI)
        {
            try
            {
                logger.Debug("TaxiRequest Interface: updateTaxiRequestDetails_SI() called");
                logger.DebugFormat("Input parameter Location ID : {0} ", TRT_EntrySI.empNo);

                TRT_BAL updateTRD_BAL = new TRT_BAL();
                return (updateTRD_BAL.updateTaxiRequestDetails_BAL(TRT_EntrySI));
            }
            catch (SqlException dbEx)
            {
                logger.Error("Exception  At TRTInterface - updateTaxiRequestDetails_SI  : " + dbEx.Message.ToString());
                logger.Error("TaxiRequest Interface: updateTaxiRequestDetails_SI() returning error");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Error("Exception  At TRTInterface - updateTaxiRequestDetails_SI  : " + ex.Message.ToString());
                logger.Error("TaxiRequest Interface: updateTaxiRequestDetails_SI() returning error");

                throw ex;
            }
        }  
   
    }
}
