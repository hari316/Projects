using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using iSmart_BL.iSmart;

namespace iSmart_SI.iSmart
{
    public class iSmartInterface
    {
        public iSmartInterface()
        {
            // TODO: Add constructor logic here 
        }

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(iSmartInterface));

        /// <summary>
        /// This Method is a interface to getPurchaseRequestDetails function
        /// </summary>
        /// <param name="PReqNo_SI"></param>
        /// <returns></returns>
        /// <history>
        ///     Hari haran      07/05/2012      created
        /// </history>

        public iSmartEntity getPurchaseRequestDetails_SI(string PReqNo_SI)
        {
            try
            {
                logger.Debug("iSmartInterface: getPurchaseRequestDetails_SI() called"); 
                logger.Debug("PReqNo value : " + PReqNo_SI);

                iSmart_BAL getTS_BAL = new iSmart_BAL();

                logger.Debug("Control Flow : Method - getPurchaseRequestDetails_SI Stop");

                return (getTS_BAL.getPurchaseRequestDetails_BAL(PReqNo_SI));
            }
            catch (SqlException dbEx)
            {
                logger.Error("Database Exception  At iSmartInterface - getPurchaseRequestDetails_SI  : " + dbEx.Message.ToString());
                logger.Error("iSmartInterface: getPurchaseRequestDetails_SI() returning error");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Error("Exception  At iSmartInterface - getPurchaseRequestDetails_SI  : " + ex.Message.ToString());
                logger.Error("iSmartInterface: getPurchaseRequestDetails_SI() returning error");

                throw ex;
            }
        }


        /// <summary>
        /// This Method is a interface to updatePurchaseRequest fuction
        /// </summary>
        /// <param name="entry_SI"></param>
        /// <returns></returns>
        /// /// <history>
        ///     Hari haran      08/05/2012      created
        /// </history>

        public iSmart_UpdateOutputEntity updatePurchaseRequest_SI(iSmart_UpdateInputEntity[] entry_SI)
        {
            try
            {
                logger.Debug("iSmartInterface: updatePurchaseRequest_SI() called"); 
                logger.Debug("Method : updatePurchaseRequest_SI PReqNo value : " + entry_SI[0].PReqNo.ToString());

                iSmart_BAL updateTS_BAL = new iSmart_BAL();

                logger.Debug("Control Flow : Method - updatePurchaseRequest_SI Stop");

                return (updateTS_BAL.updatePurchaseRequest_BAL(entry_SI));
            }
            catch (SqlException dbEx)
            {
                logger.Error("Database Exception  At iSmartInterface - updatePurchaseRequest_SI  : " + dbEx.Message.ToString());
                logger.Error("iSmartInterface: updatePurchaseRequest_SI() returning error");

                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Error("Exception  At iSmartInterface - updatePurchaseRequest_SI  : " + ex.Message.ToString());
                logger.Error("iSmartInterface: updatePurchaseRequest_SI() returning error");

                throw ex;
            }
        }
    }
}
