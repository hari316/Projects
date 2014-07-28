using System;
using System.Collections.Generic;
using log4net;

namespace ICOMSProvisioningService
{
 public class ServiceRunTimeManager
    {
     private static readonly ILog logger = LogManager.GetLogger(typeof(ServiceRunTimeManager));

     /// <summary>
     ///  Process ICOMS input messages based on type CUI/CCI/EQI.
     /// </summary>
     /// <param name="strICOMS_input_Msg"></param>
     /// <param name="templistenerMembers"></param>
     /// <returns></returns>
     public string processICOMSMessages(string strICOMS_input_Msg, ServiceListenerMembers templistenerMembers)
       {

           string strMsgFormat = string.Empty;
           string retMsg2ICOM = string.Empty;
           string strResMainFormat = string.Empty;
           string strGetICOMSMsgTillDot = string.Empty;
           ServiceTranslationManager trsMgr = new ServiceTranslationManager();
           try
           {                             

               logger.Info("ServiceRunTimeManager::processICOMSMessages() called");

               // Find dot from last & get message till that dot
               strGetICOMSMsgTillDot = strICOMS_input_Msg.Substring(0, strICOMS_input_Msg.LastIndexOf("."));
               logger.Info("Extracted ICOMS Message for processing from received message...");
               logger.Info(string.Format("{0}",strGetICOMSMsgTillDot));

               // Find CUI/CCI/EQI
               strMsgFormat = trsMgr.findICOMInputFormat(strGetICOMSMsgTillDot);
               logger.Info(string.Format("ICOMS Message Format...  {0}", strMsgFormat));

               if (strMsgFormat.Equals("ACK"))
               {
                   // form outgoing message format - xxxxxVyyyACK
                   strResMainFormat = trsMgr.ReplaceAtIndex(strGetICOMSMsgTillDot.IndexOf("I"), 'V', strGetICOMSMsgTillDot);
                   logger.Info(string.Format("ICOMS Full response Format...  {0}", strResMainFormat));
                   return trsMgr.formatResponse(strMsgFormat, strResMainFormat, "0000000", trsMgr.getValue4Key(strGetICOMSMsgTillDot, "TI:"));
               }
               // get incoming message format - xxxxxIyyyCUI/xxxxxIyyyCCI/xxxxxIyyyEQI
               strResMainFormat = trsMgr.getFullInputFormatValue(strGetICOMSMsgTillDot);
               logger.Info(string.Format("ICOMS Full request Format...  {0}", strResMainFormat));
               
               // form outgoing message format - xxxxxVyyyCUI/xxxxxVyyyCCI/xxxxxVyyyEQI
               strResMainFormat = trsMgr.ReplaceAtIndex(strResMainFormat.IndexOf("I"), 'V', strResMainFormat);
               logger.Info(string.Format("ICOMS Full response Format...  {0}", strResMainFormat));
               
               switch (strMsgFormat)
               {
                   case "CUI":
                       retMsg2ICOM = processCUIMessage(strGetICOMSMsgTillDot, templistenerMembers);
                       break;
                   case "CCI":
                       retMsg2ICOM = processCCIMessage(strGetICOMSMsgTillDot, templistenerMembers);
                       break;
                   case "EQI":
                       retMsg2ICOM = processEQIMessage(strGetICOMSMsgTillDot, templistenerMembers);
                       break;
                   default:
                       logger.Warn(string.Format("Invalid message format received from ICOMS - {0}. Valid formats CUI/CCI/EQI", strMsgFormat));
                       retMsg2ICOM = trsMgr.formatResponse("XXX", strResMainFormat, "0000902", trsMgr.getValue4Key(strGetICOMSMsgTillDot, "TI:"));
                       break;
                }

               // Format full outgoing response to ICOMS
               logger.Info("ServiceRunTimeManager::processICOMSMessages() formatting the response ");
               return trsMgr.formatResponse(strMsgFormat, strResMainFormat, retMsg2ICOM, trsMgr.getValue4Key(strGetICOMSMsgTillDot, "TI:"));
           }
           catch (Exception ex)
           {
               logger.Error(string.Format("processICOMSMessages(): Exception: {0}", ex.Message));
               logger.Error("ServiceRunTimeManager::processICOMSMessages() returning error");
               return trsMgr.formatResponse("XXX", strResMainFormat, "0000901", "xxxxxx");
           }
       }

     /// <summary>
     /// Process ICOMS CUI messages. Messages will be translated in required format to invoke CRM 4c Rest Interface.
     /// </summary>
     /// <param name="strICOMS_CUI_Msg"></param>
     /// <param name="templistenerMembers"></param>
     /// <returns></returns>
     public string processCUIMessage(string strICOMS_CUI_Msg, ServiceListenerMembers templistenerMembers)
     {

         Dictionary<string, string> dictObj4c = new Dictionary<string, string>();
         SyncRequest objSyncReq;         
         ServiceTranslationManager trsMgr;
         ServiceBusinessRulesManager busMgr;
         string retMsg2ICOM = "0000901";
         string strResMainFormat=string.Empty;
         string strErrCode = string.Empty;
         trsMgr = new ServiceTranslationManager();
         string[] arrConfigSiteIdName=new string[2];
         
         try
         {           
          
           logger.Info("ServiceRunTimeManager::processCUIMessage() called");
            
           busMgr = new ServiceBusinessRulesManager();

           // Create dictionary object with all required tokens(key-value) for CUI
           dictObj4c = trsMgr.getDataFor4cInterface(strICOMS_CUI_Msg, ServiceConstantsManager.CONST_CUI, templistenerMembers);

           logger.Info(string.Format("Required tokens are extracted from CUI message...  {0}", trsMgr.GetLine(dictObj4c)));

           // Validation for missing tokens
           strErrCode = busMgr.checkRequiredTokensPresent(dictObj4c, ServiceConstantsManager.CONST_CUI);
           if (strErrCode != string.Empty)
           {
               logger.Error(string.Format("Please verify all required tokens are present in the CUI message. Required Tokens... \"{0}\"", string.Join(",", ServiceConstantsManager.CONST_CUI)));
               return strErrCode;
           }

           // Validation for token length
           strErrCode = busMgr.checkRequiredLengthOfTokens(dictObj4c,templistenerMembers);
           if (strErrCode != string.Empty)
           {
               logger.Error(string.Format("Length is not matching for one of tokens present in the CUI message(): {0}", string.Join(",", ServiceConstantsManager.CONST_CUI)));
               return strErrCode;
           }
           
           // update Customerid(AN=AN+SI) based on flag value in config value
           dictObj4c = busMgr.AddSiteId2CustId(dictObj4c, templistenerMembers);

           objSyncReq = new SyncRequest();
           objSyncReq.CustomerId = dictObj4c["AN:"];
           objSyncReq.CustomerStatus = dictObj4c["AS:"];
           objSyncReq.ICOMSMsgFormat = "CUI";

           CustomerInformationRequest crmreq = new CustomerInformationRequest(objSyncReq, objSyncReq);
           // Perform Add customer
           retMsg2ICOM = crmreq.AddCustomer();

           // Get success/failure error code to send to ICOMS
           retMsg2ICOM = transalteResCode2ICOM4m4C(retMsg2ICOM, "AddCustomer");

           logger.Info(string.Format("ServiceRunTimeManager::processCUIMessage() returning value {0}", retMsg2ICOM));
           return retMsg2ICOM;
         }
         catch (Exception ex)
         {
             logger.Error(string.Format("processCUIMessage(): Exception: {0}", ex.Message));
             logger.Error("ServiceRunTimeManager::processCUIMessage() returning error");
             return "0000901";
         }
     }


     /// <summary>
     /// Process ICOMS CCI messages. Messages will be translated in required format to invoke CRM 4c Rest Interface.
     /// </summary>
     /// <param name="strICOMS_CCI_Msg"></param>
     /// <param name="templistenerMembers"></param>
     /// <returns></returns>
     public string processCCIMessage(string strICOMS_CCI_Msg, ServiceListenerMembers templistenerMembers)
     {
         Dictionary<string, string> dictObj4c = new Dictionary<string, string>();
         SyncRequest objSyncReq;         
         SubscriberSyncRequest objCusCre;
         ServiceTranslationManager trsMgr;
         ServiceBusinessRulesManager busMgr;
         string retMsg2ICOM = "0000901";
         string strResMainFormat = string.Empty;
         string strErrCode = string.Empty;         
         trsMgr = new ServiceTranslationManager();

         try
         {
             logger.Info("ServiceRunTimeManager::processCCIMessage() called");
             busMgr = new ServiceBusinessRulesManager();
             
             // Create dictionary object with all required tokens(key-value) for CCI
             dictObj4c = trsMgr.getDataFor4cInterface(strICOMS_CCI_Msg, ServiceConstantsManager.CONST_CCI, templistenerMembers);
             logger.Info(string.Format("Required tokens are extracted from CCI message...  {0}", trsMgr.GetLine(dictObj4c)));

             // Validation for missing tokens
             strErrCode = busMgr.checkRequiredTokensPresent(dictObj4c, ServiceConstantsManager.CONST_CCI);
             if (strErrCode != string.Empty)
             {
                 logger.Error(string.Format("Please verify all required tokens are present in the CCI message. Required Tokens... \"{0}\"", string.Join(",", ServiceConstantsManager.CONST_CCI)));                 
                 return strErrCode;
             }

             // Validation for token length
             strErrCode = busMgr.checkRequiredLengthOfTokens(dictObj4c, templistenerMembers);
             if (strErrCode != string.Empty)
             {
                 logger.Error(string.Format("Length is not matching for one of tokens present in the CCI message(): {0}", string.Join(",", ServiceConstantsManager.CONST_CCI)));
                 return strErrCode;
             }

             // update Customerid(AN=AN+SI) based on flag value in config value         
             dictObj4c = busMgr.AddSiteId2CustId(dictObj4c, templistenerMembers);

             objCusCre = new SubscriberSyncRequest();
             objCusCre.CustomerId = dictObj4c["AN:"];
             objCusCre.creditLimit = Convert.ToInt32(dictObj4c["CL:"]);

             objSyncReq = new SyncRequest();
             objSyncReq.CustomerId = dictObj4c["AN:"];
             objSyncReq.ICOMSMsgFormat = "CCI";
             // Perform Add customer
             CustomerCreditRequest objCusCreReq = new CustomerCreditRequest(objCusCre, objSyncReq);
             retMsg2ICOM = objCusCreReq.AddCCI();
             
             // Get success/failure error code to send to ICOMS
             retMsg2ICOM = transalteResCode2ICOM4m4C(retMsg2ICOM, "AddCCI");

             logger.Info(string.Format("ServiceRunTimeManager::processCCIMessage() returning value {0}", retMsg2ICOM));
             return retMsg2ICOM;
         }
         catch (Exception ex)
         {
             logger.Error(string.Format("processCIIMessage(): Exception: {0}", ex.Message));
             logger.Error("ServiceRunTimeManager::processCCIMessage() returning error");
             return retMsg2ICOM;
         }
     }

     /// <summary>
     /// Process ICOMS EQI messages. Messages will be translated in required format to invoke CRM 4c Rest Interface.
     /// </summary>
     /// <param name="strICOMS_EQI_Msg"></param>
     /// <param name="templistenerMembers"></param>
     /// <returns></returns>
     public string processEQIMessage(string strICOMS_EQI_Msg, ServiceListenerMembers templistenerMembers)
     {

         Dictionary<string, string> dictObj4c = new Dictionary<string, string>();
         SyncRequest objSyncReq;
         EquipmentSyncRequest objCusEqi;         
         ServiceTranslationManager trsMgr;
         ServiceBusinessRulesManager busMgr;
         string retMsg2ICOM = "0000901";
         string strResMainFormat = string.Empty;
         string strErrCode = string.Empty;         
         trsMgr = new ServiceTranslationManager();

         try
         {
             logger.Info("ServiceRunTimeManager::processEQIMessage() called");
             busMgr = new ServiceBusinessRulesManager();

             // Create dictionary object with all required tokens(key-value) for EQI
             dictObj4c = trsMgr.getDataFor4cInterface(strICOMS_EQI_Msg, ServiceConstantsManager.CONST_EQI, templistenerMembers);
             logger.Info(string.Format("Required tokens are extracted from EQI message...  {0}", trsMgr.GetLine(dictObj4c)));

             // Validation for missing tokens
             strErrCode = busMgr.checkRequiredTokensPresent(dictObj4c, ServiceConstantsManager.CONST_EQI);
             if (strErrCode != string.Empty)
             {
                 logger.Error(string.Format("Please verify all required tokens are present in the EQI message. Required Tokens... \"{0}\"", string.Join(",", ServiceConstantsManager.CONST_EQI)));
                 return strErrCode;
             }

             // Validation for token length
             strErrCode = busMgr.checkRequiredLengthOfTokens(dictObj4c, templistenerMembers);
             if (strErrCode != string.Empty)
             {
                 logger.Error(string.Format("Length is not matching for one of tokens present in the EQI message(): {0}", string.Join(",", ServiceConstantsManager.CONST_EQI)));
                 return strErrCode;
             }

             // update Customerid(AN=AN+SI) based on flag value in config value  
             dictObj4c = busMgr.AddSiteId2CustId(dictObj4c, templistenerMembers);

             // Get Native format device id from ICOMS "EA" token based config value of "DeviceIdFormat" & "NativeFormat4cFlag"
             dictObj4c = busMgr.getNativeFormat4cAddress(dictObj4c, templistenerMembers);

             objCusEqi = new EquipmentSyncRequest();
             objCusEqi.CustomerId = dictObj4c["AN:"];
             objCusEqi.macAddress = dictObj4c["EA:"];
             // Create new list for product Ids        
             objCusEqi.offeringId = busMgr.getProductIdList(dictObj4c);        
             objSyncReq = new SyncRequest();
             objSyncReq.CustomerId = dictObj4c["AN:"];
             objSyncReq.CustomerStatus = string.Empty;
             objSyncReq.ICOMSMsgFormat = "EQI";

             CustomerEquipmentRequest objCusEQIReq = new CustomerEquipmentRequest(objCusEqi, objSyncReq);

             switch (dictObj4c["AC:"])
             {
                 // Add EQI
                 case "A":  
                     logger.Info("ADD EQI action called");
                     retMsg2ICOM = objCusEQIReq.AddEQI();
                     // Get success/failure error code to send to ICOMS
                     retMsg2ICOM = transalteResCode2ICOM4m4C(retMsg2ICOM, "AddEQI");
                     break;
                 // Update EQI
                 case "U":
                     logger.Info("Update EQI action called");
                     retMsg2ICOM = objCusEQIReq.UpdateEQI();
                     // Get success/failure error code to send to ICOMS
                     retMsg2ICOM = transalteResCode2ICOM4m4C(retMsg2ICOM, "UpdateEQI");
                     break;
                 // Delete EQI
                 case "R":
                     logger.Info("Delete EQI action called");
                     retMsg2ICOM = objCusEQIReq.DeleteEQI();
                     // Get success/failure error code to send to ICOMS
                     retMsg2ICOM = transalteResCode2ICOM4m4C(retMsg2ICOM, "DeleteEQI");                                      
                     break;
                 default:
                     logger.Warn(string.Format("Invalid token value received from EQI message for AC operation : {0}", dictObj4c["AC:"]));                      
                     break;
             }
             logger.Info(string.Format("ServiceRunTimeManager::processEQIMessage() returning value {0}", retMsg2ICOM));
             return retMsg2ICOM;
         }
         catch (Exception ex)
         {
             logger.Error(string.Format("processEQIMessage(): Exception: {0}", ex.Message));
             logger.Error("ServiceRunTimeManager::processEQIMessage() returning error");
             return retMsg2ICOM;
         }
     }

     /// <summary>
     /// this function used to translate 4c error code according to ICOMS
     /// </summary>
     /// <param name="str4cErrCode"></param>
     /// <param name="operationName"></param>
     /// <returns></returns>
     public string transalteResCode2ICOM4m4C(string str4cErrCode, string operationName)
     {
         logger.Info("ServiceRunTimeManager::transalteResCode2ICOM4m4C() called");

         // If all(Priamry/BackUp) the CRMs failed
         if (CRM4cInterfaceAvailabilityManager.IsAllCRMsFailed)
         {
             logger.Error("All(Priamry/BackUp) CRMs failed. Error Code \"0000908\" is sending to ICOMS. ");
             CRM4cInterfaceAvailabilityManager.IsAllCRMsFailed = false;
             return "0000908";
         }

         string ret4cErrCode = "0000901";
         switch (str4cErrCode)
         {
             case "0000000":
                 ret4cErrCode = "0000000";
                 break;
             case "0000404":
                 if (operationName.ToUpper().Equals("DELETEEQI"))
                 {
                     ret4cErrCode = "0000301";
                 }                 
                 break;
             default:                
                 break;
         }
         logger.Info(string.Format("ServiceRunTimeManager::transalteResCode2ICOM4m4C() returning \"{0}\" for the operation \"{1}\"", ret4cErrCode, operationName));
         return ret4cErrCode;
     }

    }
}
