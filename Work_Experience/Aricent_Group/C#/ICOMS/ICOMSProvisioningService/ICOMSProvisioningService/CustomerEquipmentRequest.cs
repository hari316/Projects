using System;
using System.Collections.Generic;
using System.Net;
using log4net;

namespace ICOMSProvisioningService
{
    class CustomerEquipmentRequest : CRM4cInterfaceAccessManager
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(CustomerEquipmentRequest));

        private string CustomerId = string.Empty;
        private string CustomerStatus = string.Empty;
        private string CustomerMACAddress = string.Empty;
        private string CustomerSMARTCardId = string.Empty;
        private List<string> OfferingIdList;

        public HttpStatusCode ResponseStatus = HttpStatusCode.NotImplemented;

        public CustomerEquipmentRequest(EquipmentSyncRequest obj, SyncRequest sr)
            : base(sr)
        {
            this.CustomerId = obj.CustomerId;
            this.CustomerStatus = sr.CustomerStatus;
            this.CustomerMACAddress = obj.macAddress;
            this.CustomerSMARTCardId = obj.smartCardId;
            this.OfferingIdList = obj.offeringId;
            
        } // CRMCustoEquipmentRequest
        
        /// <summary>
        ///  Add equipment information to customer
        /// </summary>
        /// <returns></returns>
        public string AddEQI()
        {
            string strRetErrCode2ICOM = "0000901";
            try
            {
                logger.Info("CRMCustomerEquipmentRequest::AddEQI() called");
                SyncRequest objSyncReq = new SyncRequest();
                objSyncReq.CustomerId = CustomerId;
                objSyncReq.CustomerStatus = CustomerStatus;
                objSyncReq.ICOMSMsgFormat = "EQI";

                CustomerInformationRequest objCusReq = new CustomerInformationRequest(objSyncReq, objSyncReq);
                objCusReq.DeviceId = CustomerMACAddress;

                // Perform Add Customer
                strRetErrCode2ICOM = objCusReq.AddCustomer(false);
                if (strRetErrCode2ICOM != "0000000")
                {
                    logger.Error("CRMCustomerEquipmentRequest::AddCustomer(): failed");
                    logger.Error("CRMCustomerEquipmentRequest::AddEQI() returning false");
                    return strRetErrCode2ICOM;
                }

                // Perform Add Device
                strRetErrCode2ICOM = objCusReq.AddCrmDevice();
                if (strRetErrCode2ICOM != "0000000")
                {
                    logger.Error("CRMCustomerEquipmentRequest::AddCrmDevice(): failed");
                    logger.Error("CRMCustomerEquipmentRequest::AddEQI() returning false");
                    return strRetErrCode2ICOM;
                }

                // Perform Add Device to Customer
                strRetErrCode2ICOM = objCusReq.AssignDevice();
                if (strRetErrCode2ICOM != "0000000")
                {
                    logger.Error("CRMCustomerEquipmentRequest::AssignDevice(): failed");
                    logger.Error("CRMCustomerEquipmentRequest::AddEQI() returning false");
                    return strRetErrCode2ICOM;
                }

                // Perform add/delete products for Customer
                strRetErrCode2ICOM = EntitlementReplaced();
                if (strRetErrCode2ICOM != "0000000")
                {
                    logger.Error("CRMCustomerEquipmentRequest::EntitlementReplaced() failed");
                    logger.Error("CRMCustomerEquipmentRequest::AddEQI() returning false");
                    return strRetErrCode2ICOM;
                }

                logger.Info("CRMCustomerEquipmentRequest::AddEQI() returning true");
                return strRetErrCode2ICOM;
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("AddEQI(): Exception: {0}", ee.Message));
                logger.Error("CRMCustomerEquipmentRequest::AddEQI() returning false");
                return strRetErrCode2ICOM;
            }
        }

        /// <summary>
        ///  Update equipment information to customer
        /// </summary>
        /// <returns></returns>
        public string UpdateEQI()
        {
            string strRetErrCode2ICOM = "0000901";
            try
            {
                logger.Info("CRMCustomerEquipmentRequest::UpdateEQI() called");

                // Perform Add EQI operation
                strRetErrCode2ICOM = AddEQI();
                if (strRetErrCode2ICOM != "0000000")
                {
                    logger.Error("CRMCustomerEquipmentRequest::AddEQI() failed");
                    logger.Error("CRMCustomerEquipmentRequest::UpdateEQI() returning false");
                    return strRetErrCode2ICOM;
                }

                // Perform add/delete products for Customer
                strRetErrCode2ICOM = EntitlementReplaced();
                if (strRetErrCode2ICOM != "0000000")
                {
                    logger.Error("CRMCustomerEquipmentRequest::EntitlementReplaced() failed");
                    logger.Error("CRMCustomerEquipmentRequest::UpdateEQI() returning false");
                    return strRetErrCode2ICOM;
                }

                logger.Info("CRMCustomerEquipmentRequest::UpdateEQI() returning true");
                return strRetErrCode2ICOM;
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("UpdateEQI(): Exception: {0}", ee.Message));
                logger.Error("CRMCustomerEquipmentRequest::UpdateEQI() returning false");
                return strRetErrCode2ICOM;
            }
        }

        public string DeleteEQI()
        {
            string strRetErrCode2ICOM = "0000901";
            try
            {
                logger.Info("CRMCustomerEquipmentRequest::DeleteEQI() called");
                SyncRequest objSyncReq = new SyncRequest();
                objSyncReq.CustomerId = CustomerId;
                objSyncReq.CustomerStatus = CustomerStatus;
                objSyncReq.ICOMSMsgFormat = "EQI";

                CustomerInformationRequest objCusReq = new CustomerInformationRequest(objSyncReq, objSyncReq);
                objCusReq.DeviceId = CustomerMACAddress;

                // Perform Deleting device from Customer
                strRetErrCode2ICOM = objCusReq.RemoveDevice();
                if (strRetErrCode2ICOM != "0000000")
                {
                    logger.Error("CRMCustomerEquipmentRequest::RemoveDevice() failed");
                    logger.Error("CRMCustomerEquipmentRequest::DeleteEQI() returning false");
                    return strRetErrCode2ICOM;
                }

                // Perform Deleting device
                strRetErrCode2ICOM = objCusReq.DeleteCrmDevice();
                if (strRetErrCode2ICOM != "0000000")
                {
                    logger.Error("CRMCustomerEquipmentRequest::DeleteCrmDevice() failed");
                    logger.Error("CRMCustomerEquipmentRequest::DeleteEQI() returning false");
                    return strRetErrCode2ICOM;
                }
                return strRetErrCode2ICOM;
                
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("DeleteEQI(): Exception: {0}", ee.Message));
                logger.Error("CustomerEquipmentRequest::DeleteEQI() returning false");
                return strRetErrCode2ICOM;
            } // end try catch
        }

        /// <summary>
        ///  Update Product(s) information to customer
        /// </summary>
        /// <returns></returns>
        public string EntitlementReplaced()
        {
            bool PackageFailed = false;
            string strRetErrCode2ICOM = "0000901";

            try
            {
                logger.Info("CRMCustomerEquipmentRequest::EntitlementReplaced() called");
                SyncRequest objSyncReq = new SyncRequest();
                objSyncReq.CustomerId = CustomerId;
                objSyncReq.CustomerStatus = CustomerStatus;
                objSyncReq.ICOMSMsgFormat = "EQI";

                CustomerInformationRequest objCusReq = new CustomerInformationRequest(CustomerId, objSyncReq);

                // 1. Remove all subscription products from a customer
                strRetErrCode2ICOM = objCusReq.RemoveAllSubscriptions();
                if (strRetErrCode2ICOM != "0000000")
                {                    
                    logger.Error("EntitlementReplaced(): RemoveAllSubscriptions() failed, returning false");
                    return strRetErrCode2ICOM;
                }
                ResponseStatus = objCusReq.ResponseStatus; 
                // 2. Assign a subscription product to a customer (send this multiple times depending on number of offerings)
                foreach (string OfferId in OfferingIdList)
                {
                    objCusReq.ProductId = OfferId;

                    strRetErrCode2ICOM = objCusReq.AssignSubscription();
                    if (strRetErrCode2ICOM != "0000000")
                    {
                        PackageFailed = true;                        
                        logger.Error(string.Format("EntitlementReplaced(): AssignSubscription() failed: OfferId = {0}", OfferId));
                    }
                }
                ResponseStatus = objCusReq.ResponseStatus;
                if (PackageFailed == true)
                {                    
                    logger.Error("EntitlementReplaced(): Some or all of the entitlements were not synchronized to CRM");
                    return strRetErrCode2ICOM;
                }
                logger.Info("CRMCustomerEquipmentRequest::EntitlementReplaced() successful");
                return "0000000";
            }
            catch (Exception ee)
            {                
                logger.Error(string.Format("EntitlementReplaced(): Exception: {0}", ee.Message));
                logger.Error("CRMCustomerEquipmentRequest::EntitlementReplaced() returning false");
                return strRetErrCode2ICOM;
            }
        } // EntitlementReplaced

    }
}