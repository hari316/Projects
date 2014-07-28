using System;
using System.Net;
using log4net;

namespace ICOMSProvisioningService
{
    public class CustomerInformationRequest : CRM4cInterfaceAccessManager
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(CustomerInformationRequest));

        private string CustomerId = string.Empty;
        private string CustomerStatus = string.Empty;
        private string deviceId = string.Empty;
        private string productId = string.Empty;
        private bool isBarredFlag;
        
        public HttpStatusCode ResponseStatus = HttpStatusCode.NotImplemented;

        public CustomerInformationRequest(string CustomerId, SyncRequest sr)
            : base(sr)
        {
            this.CustomerId = CustomerId;          
        } // CRMCustomerRequest

        public CustomerInformationRequest(SyncRequest obj , SyncRequest sr) : base(sr)
        {
            this.CustomerId = obj.CustomerId;
            this.CustomerStatus = obj.CustomerStatus;

            switch (obj.CustomerStatus)
            {
                case "A":
                    this.isBarredFlag = false;
                    break;
                case "I":
                    this.isBarredFlag = true;
                    break;              

            }

        } // CRMCustomerRequest

        
       
      
        public string DeviceId
        {
            get
            {
                return (this.deviceId);
            }
            set
            {
                this.deviceId = value;
            }
        } // DeviceId

        public string ProductId
        {
            get
            {
                return (this.productId);
            }
            set
            {
                this.productId = value;
            }
        } // ProductId

        /// <summary>
        /// Add Customer
        /// </summary>
        /// <param name="sendBarredFlag"></param>
        /// <returns></returns>
        public string AddCustomer(bool sendBarredFlag=true)
        {
            string strRetErrCode2ICOM = "0000901";
            try
            {
                
                logger.Info("CRMCustomerRequest::AddCustomer() called");

                Method = "PUT";

                UrlPostFix = String.Format("/Customers/{0}", CustomerId);

                if (!sendBarredFlag)
                {
                    RequestBody = String.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?> <Customer id=\"{0}\" xmlns=\"urn:eventis:crm:2.0\"/>", CustomerId);
                }
                else
                {
                    RequestBody = String.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?> <Customer id=\"{0}\" xmlns=\"urn:eventis:crm:2.0\"><IsBarred xmlns=\"urn:eventis:crm:2.0\">{1}</IsBarred></Customer>", CustomerId, isBarredFlag.ToString().ToLower());
                }

                logger.Info(string.Format("AddCustomer(): UrlPostFix value...  {0}", UrlPostFix));
                logger.Info(string.Format("AddCustomer(): Request Body value...  {0}", RequestBody));

                string ResponseBody = "";

                // Send the request to CRM
                if (!SendCrmRequest(ref ResponseBody, ref ResponseStatus))
                {                   
                    logger.Error("AddCustomer(): SendCrmRequest failed");
                    logger.Error("CRMCustomerRequest::AddCustomer() returning false");                    
                } // end if

                // get success/failure error code to return according to CRM response
                return AnalyzeResponseCodeWithICOMSMapping(ResponseStatus, "AddCustomer");
            }
            catch (Exception ee)
            {                
                logger.Error(string.Format("AddCustomer(): Exception: {0}", ee.Message.ToString()));
                logger.Error("CRMCustomerRequest::AddCustomer() returning false");

                return strRetErrCode2ICOM;
            } // end try catch            
        }  // AddCustomer

               
        /// <summary>
        ///  Update customer information
        /// </summary>
        /// <returns></returns>
        public string ModifyCustomer()
        {
            return AddCustomer();
        }

        /// <summary>
        /// Delete Customer
        /// </summary>
        /// <returns></returns>
        public string DeleteCustomer()
        {
            string strRetErrCode = "0000901";
            try
            {
                
                logger.Info("CRMCustomerRequest::DeleteCustomer() called");

                Method = "DELETE";

                UrlPostFix = String.Format("/Customers/{0}", CustomerId);

                RequestBody = String.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?> <Customer id=\"{0}\" xmlns=\"urn:eventis:crm:2.0\"/><IsBarred value=\"{1}\" xmlns=\"urn:eventis:crm:2.0\"/>", CustomerId, true);

                logger.Info(string.Format("DeleteCustomer(): UrlPostFix value...  {0}", UrlPostFix));
                logger.Info(string.Format("DeleteCustomer(): Request Body value...  {0}", RequestBody));

                string ResponseBody = "";
                

                // Send the request to CRM
                if (!SendCrmRequest(ref ResponseBody, ref ResponseStatus))
                {                    
                    logger.Error("DeleteCustomer(): SendCrmRequest failed");
                    logger.Error("CRMCustomerRequest::DeleteCustomer() returning false");                    
                } // end if

                // get success/failure error code to return according to CRM response
                return AnalyzeResponseCodeWithICOMSMapping(ResponseStatus, "DeleteCustomer");
            }
            catch (Exception ee)
            {               
                logger.Error(string.Format("DeleteCustomer(): Exception: {0}", ee.Message));
                logger.Error("CRMCustomerRequest::DeleteCustomer() returning false");
                return strRetErrCode;
            } // end try catch            
        }  // DeleteCustomer

        /// <summary>
        /// Add device
        /// </summary>
        /// <returns></returns>
        public string AddCrmDevice()
        {
            string strRetErrCode = "0000901";
            try
            {
                logger.Info("CRMCustomerRequest::AddCrmDevice() called");

                Method = "PUT";

                UrlPostFix = String.Format("/Devices/{0}", DeviceId);

             //  RequestBody = String.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?> <Device id=\"{0}\" xmlns=\"urn:eventis:crm:2.0\"></Device>", DeviceId);

                RequestBody = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?><Device id=\"{0}\" xmlns=\"urn:eventis:crm:2.0\" xmlns:xsi=\"http://///www.w3.org//2001//XMLSchema-instance\" xmlns:xsd=\"http:////www.w3.org//2001//XMLSchema\"/>", DeviceId);

                logger.Info(string.Format("AddCrmDevice(): UrlPostFix value...  {0}", UrlPostFix));
                logger.Info(string.Format("AddCrmDevice(): Request Body value...  {0}", RequestBody));
       
                string ResponseBody = "";

                // send the request to the axiom portal
                if (!SendCrmRequest(ref ResponseBody, ref ResponseStatus))
                {                    
                    logger.Error("AddCrmDevice(): SendCrmRequest failed");
                    logger.Error("CRMDeviceRequest::AddCrmDevice() returning false");                    
                } // end if

                // get success/failure error code to return according to CRM response
                return AnalyzeResponseCodeWithICOMSMapping(ResponseStatus, "AddCrmDevice");
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("AddCrmDevice(): Exception: {0}", ee.Message));
                logger.Error("CRMDeviceRequest::AddCrmDevice() returning false");
                return strRetErrCode;
            } // end try catch
        }  // AddCrmDevice

        /// <summary>
        /// Delete device
        /// </summary>
        /// <returns></returns>
        public string DeleteCrmDevice()
        {
            string strRetErrCode = "0000901";
            try
            {                
                logger.Info("CRMCustomerRequest::DeleteCrmDevice() called");

                Method = "DELETE";

                UrlPostFix = String.Format("/Devices/{0}", DeviceId);

              //  RequestBody = String.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?> <Device id=\"{0}\" xmlns=\"urn:eventis:crm:2.0\"/>", DeviceId);
                  RequestBody = "";

                logger.Info(string.Format("DeleteCrmDevice(): UrlPostFix value...  {0}", UrlPostFix));
                logger.Info(string.Format("DeleteCrmDevice(): Request Body value...  {0}", RequestBody));

                string ResponseBody = "";

                // send the request to the axiom portal
                if (!SendCrmRequest(ref ResponseBody, ref ResponseStatus))
                {
                    logger.Error("DeleteCrmDevice(): SendCrmRequest failed");
                    logger.Error("CRMDeviceRequest::DeleteCrmDevice() returning false");                    
                } // end if

                // get success/failure error code to return according to CRM response
                return AnalyzeResponseCodeWithICOMSMapping(ResponseStatus, "DeleteCrmDevice");
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("DeleteCrmDevice(): Exception: {0}", ee.Message));
                logger.Error("CRMDeviceRequest::DeleteCrmDevice() returning false");
                return strRetErrCode;
            } // end try catch
        }  // DeleteCrmDevice

        /// <summary>
        /// Assign device to customer
        /// </summary>
        /// <returns></returns>
        public string AssignDevice()
        {
            string strRetErrCode = "0000901";
            try
            {                
                logger.Info("CRMCustomerRequest::AssignDevice() called");

                Method = "PUT";

                UrlPostFix = String.Format("/Customers/{0}/Devices/{1}", CustomerId, DeviceId);

                //RequestBody = String.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?> <Device id=\"{0}\" xmlns=\"urn:eventis:crm:2.0\"", MacAddress);
                RequestBody = "";

                logger.Info(string.Format("AssignDevice(): UrlPostFix value...  {0}", UrlPostFix));
                logger.Info(string.Format("AssignDevice(): Request Body value...  {0}", RequestBody));

                string ResponseBody = "";

                // send the request to the axiom portal
                if (!SendCrmRequest(ref ResponseBody, ref ResponseStatus))
                {                    
                    logger.Error("AssignDevice(): SendCrmRequest failed");
                    logger.Error("CRMCustomerRequest::AssignDevice() returning false");                    
                } // end if

                // get success/failure error code to return according to CRM response
                return AnalyzeResponseCodeWithICOMSMapping(ResponseStatus, "AssignDevice");           
            }
            catch (Exception ee)
            {               
                logger.Error(string.Format("AssignDevice(): Exception: {0}", ee.Message));
                logger.Error("CRMCustomerRequest::AssignDevice() returning false");
                return strRetErrCode;
            } // end try catch
        }  // AssignDevice

        /// <summary>
        /// Remove device from customer
        /// </summary>
        /// <returns></returns>
        public string RemoveDevice()
        {
            string strRetErrCode = "0000901";
            try
            {                
                logger.Info("CRMCustomerRequest::RemoveDevice() called");

                Method = "DELETE";

                UrlPostFix = String.Format("/Customers/{0}/Devices/{1}", CustomerId, DeviceId);

                //RequestBody = String.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?> <Customer id=\"{0}\" xmlns=\"urn:eventis:crm:2.0\"/>", CustomerId);
                RequestBody = "";

                logger.Info(string.Format("RemoveDevice(): UrlPostFix value...  {0}", UrlPostFix));
                logger.Info(string.Format("RemoveDevice(): Request Body value...  {0}", RequestBody));

                string ResponseBody = "";

                // Send the request to CRM
                if (!SendCrmRequest(ref ResponseBody, ref ResponseStatus))
                {                    
                    logger.Error("RemoveDevice(): SendCrmRequest failed");
                    logger.Error("CRMCustomerRequest::RemoveDevice() returning false");                    
                } // end if

                // get success/failure error code to return according to CRM response
                return AnalyzeResponseCodeWithICOMSMapping(ResponseStatus, "RemoveDevice");          
            }
            catch (Exception ee)
            {
                
                logger.Error(string.Format("RemoveDevice(): Exception: {0}", ee.Message));
                logger.Error("CRMCustomerRequest::RemoveDevice() returning false");
                return strRetErrCode;
            } // end try catch
        }  // RemoveDevice

        /// <summary>
        /// Remove all devices from customer
        /// </summary>
        /// <returns></returns>
        public string RemoveAllDevices()
        {
            string strRetErrCode = "0000901";
            try
            {
                
                logger.Info("CRMCustomerRequest::RemoveAllDevices() called");

                Method = "DELETE";

                UrlPostFix = String.Format("/Customers/{0}/Devices", CustomerId);

                //RequestBody = String.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?> <Customer id=\"{0}\" xmlns=\"urn:eventis:crm:2.0\"/>", CustomerId);
                RequestBody = "";

                logger.Info(string.Format("RemoveAllDevices(): UrlPostFix value...  {0}", UrlPostFix));
                logger.Info(string.Format("RemoveAllDevices(): Request Body value...  {0}", RequestBody));

                string ResponseBody = "";

                // Send the request to CRM
                if (!SendCrmRequest(ref ResponseBody, ref ResponseStatus))
                {                    
                    logger.Error("RemoveAllDevices(): SendCrmRequest failed");
                    logger.Error("CRMCustomerRequest::RemoveAllDevices() returning false");                    
                } // end if

                // get success/failure error code to return according to CRM response
                return AnalyzeResponseCodeWithICOMSMapping(ResponseStatus, "RemoveAllDevices");
            }
            catch (Exception ee)
            {   
                logger.Error(string.Format("RemoveAllDevices(): Exception: {0}", ee.Message));
                logger.Error("CRMCustomerRequest::RemoveAllDevices() returning false");
                return strRetErrCode;
            } // end try catch
        }  // RemoveAllDevices

        /// <summary>
        /// Assign products to customer
        /// </summary>
        /// <returns></returns>
        public string AssignSubscription()
        {
            string strRetErrCode = "0000901";
            try
            {                
                logger.Info("CRMCustomerRequest::AssignSubscription() called");

                Method = "PUT";

                UrlPostFix = String.Format("/Customers/{0}/SubscriptionProducts/{1}", CustomerId, ProductId);

                //RequestBody = String.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?> <Customer id=\"{0}\" xmlns=\"urn:eventis:crm:2.0\"/>", CustomerId);
                RequestBody = "";

                logger.Info(string.Format("AssignSubscription(): UrlPostFix value...  {0}", UrlPostFix));
                logger.Info(string.Format("AssignSubscription(): Request Body value...  {0}", RequestBody));

                string ResponseBody = "";

                // Send the request to CRM
                if (!SendCrmRequest(ref ResponseBody, ref ResponseStatus))
                {                    
                    logger.Error("AssignSubscription(): SendCrmRequest failed");
                    logger.Error("CRMCustomerRequest::AssignSubscription() returning false");                    
                } // end if

                // get success/failure error code to return according to CRM response
                return AnalyzeResponseCodeWithICOMSMapping(ResponseStatus, "AssignSubscription");           
            }
            catch (Exception ee)
            {                
                logger.Error(string.Format("AssignSubscription(): Exception: {0}", ee.Message));
                logger.Error("CRMCustomerRequest::AssignSubscription() returning false");
                return strRetErrCode;
            } // end try catch
        }  // AssignSubscription

        /// <summary>
        /// Remove products from customer
        /// </summary>
        /// <returns></returns>
        public string RemoveSubscription()
        {
            string strRetErrCode = "0000901";
            try
            {
                logger.Info("CRMCustomerRequest::RemoveSubscription() called");
                Method = "DELETE";

                UrlPostFix = String.Format("/Customers/{0}/SusbcriptionProducts/{1}", CustomerId, ProductId);

                //RequestBody = String.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?> <Customer id=\"{0}\" xmlns=\"urn:eventis:crm:2.0\"/>", CustomerId);
                RequestBody = "";

                logger.Info(string.Format("RemoveSubscription(): UrlPostFix value...  {0}", UrlPostFix));
                logger.Info(string.Format("RemoveSubscription(): Request Body value...  {0}", RequestBody));

                string ResponseBody = "";

                // Send the request to CRM
                if (!SendCrmRequest(ref ResponseBody, ref ResponseStatus))
                {                    
                    logger.Error("RemoveSubscription(): SendCrmRequest failed");
                    logger.Error("CRMCustomerRequest::RemoveSubscription() returning false");                    
                } // end if

                // get success/failure error code to return according to CRM response
                return AnalyzeResponseCodeWithICOMSMapping(ResponseStatus, "RemoveSubscription");           
            }
            catch (Exception ee)
            {                
                logger.Error(string.Format("RemoveSubscription(): Exception: {0}", ee.Message));
                logger.Error("CRMCustomerRequest::RemoveSubscription() returning false");
                return strRetErrCode;
            } // end try catch            
        }  // RemoveSubscription

        /// <summary>
        /// Remove all products from customer
        /// </summary>
        /// <returns></returns>
        public string RemoveAllSubscriptions()
        {
            string strRetErrCode = "0000901";
            try
            {
                
                logger.Info("CRMCustomerRequest::RemoveAllSubscriptions() called");
                Method = "DELETE";

                UrlPostFix = String.Format("/Customers/{0}/SubscriptionProducts", CustomerId);

                //RequestBody = String.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?> <Customer id=\"{0}\" xmlns=\"urn:eventis:crm:2.0\"/>", CustomerId);
                RequestBody = "";

                logger.Info(string.Format("RemoveAllSubscriptions(): UrlPostFix value...  {0}", UrlPostFix));
                logger.Info(string.Format("RemoveAllSubscriptions(): Request Body value...  {0}", RequestBody));

                string ResponseBody = "";

                // Send the request to CRM
                if (!SendCrmRequest(ref ResponseBody, ref ResponseStatus))
                {                    
                    logger.Error("RemoveAllSubscriptions(): SendCrmRequest failed");
                    logger.Error("CRMCustomerRequest::RemoveAllSubscriptions() returning false");                    
                } // end if

                // get success/failure error code to return according to CRM response
                return AnalyzeResponseCodeWithICOMSMapping(ResponseStatus, "RemoveAllSubscriptions");
            }
            catch (Exception ee)
            {                
                logger.Error(string.Format("RemoveAllSubscriptions(): Exception: {0}", ee.Message));
                logger.Error("CRMCustomerRequest::RemoveAllSubscriptions() returning false");
                return strRetErrCode;
            } // end try catch
        }  // RemoveAllSubscriptions
    } // CRMDeviceRequest
}


