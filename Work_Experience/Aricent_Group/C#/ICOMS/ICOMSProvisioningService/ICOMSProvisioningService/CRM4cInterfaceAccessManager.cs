using System;
using System.Net;
using System.Text;
using System.Web;
using log4net;
using System.Configuration;
using System.Threading;


namespace ICOMSProvisioningService
{
    public class CRM4cInterfaceAccessManager
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(CRM4cInterfaceAccessManager));

        private string UserAgent = "CRMBridgeV1.0";

        private SyncRequest syncRequest = null; 

        private HttpWebResponse response;
        private string requestBody;
        public string RequestBody
        {
            get
            {
                return (this.requestBody);
            }
            set
            {
                this.requestBody = value;
            }
        }  // RequestBody

        private string method;
        public string Method
        {
            get
            {
                return (this.method);
            }
            set
            {
                this.method = value;
            }
        }  // Method

        private string urlPostFix = ""; // the rest of the url for the request
        public string UrlPostFix
        {
            get
            {
                return (this.urlPostFix);
            }
            set
            {
                this.urlPostFix = value;
            }
        }  // UrlPostFix

    //    private string CrmBaseUrl;
   //     private string CrmBackupUrl;
        private string ContentType = "text/xml";

        public CRM4cInterfaceAccessManager(SyncRequest sr)
        {
            //CrmBaseUrl = CrmBridgeConfig.CrmUrl;                       
            syncRequest = sr;
        } // CRMRequest

        public bool SendCrmRequest(ref string RespBody, ref HttpStatusCode RespStatus)
        {

            logger.Info("CRM4cInterfaceAccessManager::SendCrmRequest() called ");
           
            bool ret = false;
            bool exitFlag = false;                       
            
            int retryDelay = Convert.ToInt32(ServiceConfigurationManager._CRMConnectionDetails["RETRYWAITDURATION"]);
            int retryDelay4CRM = Convert.ToInt32(ServiceConfigurationManager._CRMConnectionDetails["FAILOVERTIME"]);
           
            
            while(!exitFlag)
            {
                // retry mechanism (N + 1)
                int retries = Convert.ToInt32(ServiceConfigurationManager._CRMConnectionDetails["RETRYNUMBERS"]) + 1;
                int retryNumCount = 0;
                
                // to find out message processed CRMs
                switch (CRM4cInterfaceAvailabilityManager.getActiveCRMName.ToUpper())
                {
                    case "P":
                        CRM4cInterfaceAvailabilityManager.IsMsgGoing2Primary = true;
                        break;
                    case "B":
                        CRM4cInterfaceAvailabilityManager.IsMsgGoing2Backup = true;
                        break;
                    default:
                        CRM4cInterfaceAvailabilityManager.IsMsgGoing2Primary = true;
                        break;
                }

                while (retries > 0)            
                {
            
                    TimeSpan reqSendTS = System.DateTime.Now.TimeOfDay;                    
                    logger.Info(string.Format("Request send to CRM 4c REST Interface at...  {0}", reqSendTS));                                    
                    
                    ret = SendRequest(ref RespBody, ref RespStatus);            
                    TimeSpan resRecTS = System.DateTime.Now.TimeOfDay;
                    
                    logger.Info(string.Format("Response received from CRM 4c REST Interface at...  {0}", resRecTS));
                    logger.Info(string.Format("Total Process Time...  {0}  [{1}  -  {2}]", resRecTS.Subtract(reqSendTS), resRecTS, reqSendTS));
                    
                    // If success, jump out of while loop
                    if (ret)
                    {
                        exitFlag = true;
                        break; 
                    }

                   // go for retry, If CRM returned error code matchs with service configuration HTTPErrorCodes / time out error occurs
                    if (ServiceConfigurationManager._lst4cRetryErrorCodes.Contains(RespBody) || RespStatus.Equals(HttpStatusCode.RequestTimeout) || ServiceConfigurationManager._lst4cRetryErrorCodes.Contains(Convert.ToInt32(RespStatus).ToString()))
                    {
                          retries--;
                          if (retries>0) // not required for 0
                          {
                              logger.Info(string.Format("Active CRM Name...  {0}  ||  Retry...  {1}", CRM4cInterfaceAvailabilityManager.getActiveCRMFullName, ++retryNumCount));
                              Thread.Sleep(retryDelay);
                              logger.Info(string.Format("Service waited {0} secs. before proceeding next try", retryDelay / 1000));
                          }
                    }
                    else // jumping out of while loop for other scenarios 
                    {
                        exitFlag = true;
                        break;
                    }
                                      
                }

                // jump out of outer while loop for success or Backup CRM is not available in the service config file
                if (exitFlag || (!CRM4cInterfaceAvailabilityManager.IsBackupCRMExist))
                {
                    break;
                }

                // If both CRMs failed, reset the flags & jump out of outer while loop
                if (CRM4cInterfaceAvailabilityManager.IsMsgGoing2Primary && CRM4cInterfaceAvailabilityManager.IsMsgGoing2Backup)
                {
                    logger.Error("Sending message to both Primary & Backup CRMs failed");
                    CRM4cInterfaceAvailabilityManager.IsMsgGoing2Primary = false;
                    CRM4cInterfaceAvailabilityManager.IsMsgGoing2Backup = false;
                    CRM4cInterfaceAvailabilityManager.IsAllCRMsFailed = true;
                    CRM4cInterfaceAvailabilityManager.ActiveCRMURL = CRM4cInterfaceAvailabilityManager.changeActiveCRM(CRM4cInterfaceAvailabilityManager.getActiveCRMName);
                    break;
                }

                logger.Error(string.Format("{0} tries failed to get response from {1} ", retryNumCount, CRM4cInterfaceAvailabilityManager.ActiveCRMURL));
                
                CRM4cInterfaceAvailabilityManager.ActiveCRMURL = CRM4cInterfaceAvailabilityManager.changeActiveCRM(CRM4cInterfaceAvailabilityManager.getActiveCRMName);
                logger.Info(string.Format("Switching the CRM to {0} ", CRM4cInterfaceAvailabilityManager.ActiveCRMURL));   
             
                Thread.Sleep(retryDelay4CRM);
                logger.Info(string.Format("Service is waited {0} secs before start processing messages with switched CRM", retryDelay4CRM / 1000));
               
            }

            logger.Info(string.Format("CRM4cInterfaceAccessManager::SendCrmRequest() returned {0}", ret));
            return ret;            
        }

        public bool SendRequest(ref string RespBody, ref HttpStatusCode RespStatus)
        {

            logger.Info("CRM4cInterfaceAccessManager::SendRequest() called");
            
            System.IO.Stream resStream = null;
            System.IO.Stream dataStream = null;
            try
            {
                // Used on each read operation
                byte[] buf = new byte[8192];
                string tempString = null;
                StringBuilder sb = new StringBuilder();
                int count = 0;

                // Used to build entire input
               // string CrmUrl = CrmBaseUrl + UrlPostFix;
                
                //string CrmUrl = CRM4cInterfaceAvailabilityManager.ActiveCRMURL + UrlPostFix;
                string CrmUrl = getActiveCRMURL();

                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                byte[] ByteEncodedBody = encoding.GetBytes(requestBody);

                // Create the request
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(CrmUrl);
                request.UserAgent = UserAgent;                
                
                try
                {
                    logger.Info(string.Format("CRM4cInterfaceAccessManager(): Sending request to CRM. Full URL.. {0} ", request.RequestUri.ToString()));
                    if (Method != "GET")
                    {

                        request.Method = method;
                        request.ContentType = ContentType;
                        request.ContentLength = requestBody.Length;                     
                        request.Timeout = Convert.ToInt32(ServiceConfigurationManager._CRMConnectionDetails["HTTPTIMEOUT"]);                        
                        logger.Info(string.Format("CRM4cInterfaceAccessManager(): Request Header information... {0} ", request.Headers.ToString()));
                        logger.Info(string.Format("CRM4cInterfaceAccessManager(): Request Method type...  {0} ", request.Method.ToString()));
                        dataStream = request.GetRequestStream();
                        dataStream.Write(ByteEncodedBody, 0, ByteEncodedBody.Length);                        
                        dataStream.Close();

                    } // end if

                    // Execute the request and get the response
                    response = (HttpWebResponse)request.GetResponse();
                    RespStatus = response.StatusCode;
                    logger.Info(string.Format("CRM4cInterfaceAccessManager(): Response from CRM is HTTP {0}", RespStatus.ToString()));

                    // Retrieve the XML response via the response stream
                    resStream = response.GetResponseStream();

                    do
                    {
                        // Fill the buffer with data
                        count = resStream.Read(buf, 0, buf.Length);

                        // Make sure we read some data
                        if (count != 0)
                        {
                            // Translate from bytes to ASCII text
                            tempString = Encoding.ASCII.GetString(buf, 0, count);

                            // Continue building the string
                            sb.Append(tempString);
                        }
                    } while (count > 0); // Any more data to read?

                    RespBody = sb.ToString();
                    logger.Info(string.Format("CRM4cInterfaceAccessManager::SendRequest() returning true"));
                    return true;
                }
                catch(WebException we)
                {
                    switch (we.Status)
                    {                       
                        case WebExceptionStatus.ProtocolError:
                            RespStatus = ((HttpWebResponse)we.Response).StatusCode;                            
                            logger.Error(string.Format("SendRequest(): HttpWebResponse Error Code : {0}", (int)RespStatus));
                            logger.Error(string.Format("SendRequest(): HttpWebResponse Error Description : {0}", ((HttpWebResponse)we.Response).StatusDescription));
                            break;                     
                        case WebExceptionStatus.Timeout:
                            RespStatus = HttpStatusCode.RequestTimeout;                            
                            logger.Error(string.Format("SendRequest(): WebException Error Code : {0}", (int) we.Status));
                            logger.Error(string.Format("SendRequest(): WebException Error Name : {0}", we.Status));
                            break;                    
                        default:
                            logger.Error(string.Format("SendRequest(): WebException Error Code : {0}", (int) we.Status));
                            logger.Error(string.Format("SendRequest(): WebException Error Name : {0}", we.Status));
                            break;
                    }

                    logger.Error(string.Format("SendRequest(): WebException occured during request.GetResponse() : {0}", we.Message));
                    logger.Info("CRM4cInterfaceAccessManager::SendRequest() returning false");
                    return false;
                }
            catch (Exception e)
            {                    
                logger.Error(string.Format("SendRequest(): HttpWebResponse request.GetResponse() Exception: {0}", e.Message));                     
                logger.Info("CRM4cInterfaceAccessManager::SendRequest() returning false");
                return false;
            } // end try catch
            }
            catch (Exception ee)
            {
                logger.Error(string.Format("SendRequest(): Exception: {0}", ee.Message));
                logger.Info("CRM4cInterfaceAccessManager::SendRequest() returning false");                                
                return false;
            }
            finally
            {
                if (dataStream != null)
                {
                    dataStream.Close();
                }
                if (resStream != null)
                {
                    resStream.Close();
                }
            } // end catch
        } // SendCrmRequest

        protected bool AnalyzeResponseCode(HttpStatusCode ResponseStatus, string OperationName)
        {
 
            switch (ResponseStatus)
            {
                case HttpStatusCode.OK:                    
                    logger.Info(string.Format("{0}: Response is 200 OK", OperationName));
                    return true;
                case HttpStatusCode.Created:                    
                    logger.Info(string.Format("{0}: Response is 201 Created", OperationName));
                    return true;
                case HttpStatusCode.BadRequest:                    
                    logger.Error(string.Format("{0}: Response is 400 Bad Request", OperationName));
                    return false;
                case HttpStatusCode.Forbidden:                    
                    logger.Error(string.Format("{0}: Response is 403 Forbidden", OperationName));
                    return false;
                case HttpStatusCode.NotFound:                    
                    logger.Error(string.Format("{0}: Response is 404 Not Found", OperationName));
                    return false;
                case HttpStatusCode.MethodNotAllowed:                    
                    logger.Error(string.Format("{0}: Response is 405 Method Not Allowed", OperationName));
                    return false;
                default:                    
                    logger.Error(string.Format("{0}: Response is unsupported!", OperationName));
                    return false;
            }
        }


        protected string AnalyzeResponseCodeWithICOMSMapping(HttpStatusCode ResponseStatus, string OperationName)
        {
            logger.Info("CRM4cInterfaceAccessManager::AnalyzeResponseCodeWithICOMSMapping() called");
            string strErrCode = string.Empty;
            switch (ResponseStatus)
            {
                case HttpStatusCode.OK:
                    logger.Info(string.Format("{0}: Response is 200 OK", OperationName));
                    strErrCode = "0000000";
                    break;
                case HttpStatusCode.Created:
                    logger.Info(string.Format("{0}: Response is 201 Created", OperationName));
                    strErrCode = "0000000";
                    break;
                case HttpStatusCode.BadRequest:
                    logger.Error(string.Format("{0}: Response is 400 Bad Request", OperationName));
                    strErrCode = "0000400";
                    break;
                case HttpStatusCode.Forbidden:
                    logger.Error(string.Format("{0}: Response is 403 Forbidden", OperationName));
                    strErrCode = "0000403";
                    break;
                case HttpStatusCode.NotFound:
                    logger.Error(string.Format("{0}: Response is 404 Not Found", OperationName));                   
                    strErrCode = "0000404";
                    break;	                
                case HttpStatusCode.MethodNotAllowed:
                    logger.Error(string.Format("{0}: Response is 405 Method Not Allowed", OperationName));
                    strErrCode = "0000405";
                    break;
                default:
                    logger.Error(string.Format("{0}: Response is unsupported!", OperationName));
                    strErrCode = "0000901";
                    break;
            }
            logger.Info(string.Format("CRM4cInterfaceAccessManager::AnalyzeResponseCodeWithICOMSMapping() returns...  {0}", strErrCode));
            return strErrCode;
        }


        protected string getActiveCRMURL()
        {
            logger.Info("CRM4cInterfaceAccessManager::getActiveCRMURL() called");
            string CrmUrl = string.Empty;
            if (syncRequest.ICOMSMsgFormat.Equals("CCI"))
            {
                CrmUrl = CRM4cInterfaceAvailabilityManager.ActiveCRMURL4CCI + UrlPostFix;
            }
            else
            {
                CrmUrl = CRM4cInterfaceAvailabilityManager.ActiveCRMURL + UrlPostFix;
            }
            return CrmUrl;
        }

    } // CRMRequest
} // SeaChange.CRMBridgeService
