using System;
using System.Text;
using System.Net;
using log4net;

namespace ICOMSProvisioningService
{
  public class CustomerCreditRequest : CRM4cInterfaceAccessManager
    {
       private static readonly ILog logger = LogManager.GetLogger(typeof(CustomerCreditRequest));
       private string CustomerId = string.Empty;
       private int CustomerExpLimit = 0;

       public HttpStatusCode ResponseStatus = HttpStatusCode.NotImplemented;

       public CustomerCreditRequest(SubscriberSyncRequest obj, SyncRequest sr)
           : base(sr)
        {
            this.CustomerId = sr.CustomerId;
            this.CustomerExpLimit = obj.creditLimit;

        } // CRMCustomerCreditRequest

       /// <summary>
       ///  Add expenditure limit to customer
       /// </summary>
       public string AddCCI()
       {
           string strRetErrCode2ICOM = "0000901";
           try
           {

               logger.Info("CRMCustomerCreditRequest::AddCCI() called");

               Method = "PUT";

               UrlPostFix = String.Format("/Customers/{0}", CustomerId);

               RequestBody = String.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?> <Customer id=\"{0}\" xmlns=\"urn:eventis:crm:2.0\"><expenditureLimit xmlns=\"urn:eventis:crm:2.0\">{1}</expenditureLimit></Customer>", CustomerId, CustomerExpLimit);
               
               string ResponseBody = "";

               logger.Info(string.Format("AddCCI(): UrlPostFix value...  {0}", UrlPostFix));
               logger.Info(string.Format("AddCCI(): Request Body value...  {0}", RequestBody));

               // Send the request to CRM
               if (!SendCrmRequest(ref ResponseBody, ref ResponseStatus))
               {
                   logger.Error("AddCCI(): SendCrmRequest failed");
                   logger.Error("CRMCustomerCreditRequest::AddCCI() returning false");                   
               } // end if
              
               return AnalyzeResponseCodeWithICOMSMapping(ResponseStatus, "AddCCI");
           }
           catch (Exception ee)
           {
               logger.Error(string.Format("AddCCI(): Exception: {0}", ee.Message));
               logger.Error("CRMCustomerCreditRequest::AddCCI() returning false");
               return strRetErrCode2ICOM;
           } // end try catch            
       }  // AddCustomer

       public string updateCCI()
       {
           return AddCCI();
       }

    }
}
