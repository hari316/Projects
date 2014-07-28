using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using log4net;
using System.Linq;

namespace ICOMSProvisioningService
{
    public class ServiceBusinessRulesManager
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(ServiceBusinessRulesManager));

       /// <summary>
        /// Add site ID into Customer Id based on CustomerIdFlag value specified in configuration xml
       /// </summary>x
       /// <param name="objdic"></param>
       /// <param name="objLSMem"></param>
       /// <returns></returns>
        public Dictionary<string, string> AddSiteId2CustId(Dictionary<string, string> objdic, ServiceListenerMembers objLSMem)
        {
            logger.Info("ServiceBusinessRulesManager::AddSiteId2CustId() called");
            if (Convert.ToBoolean(objLSMem.CustomerIdFlag) == true)
            {
              
                logger.Info(string.Format("ServiceBusinessRulesManager::AddSiteId2CustId:: Recieved SiteId {0}", objdic[objLSMem.siteIdTokenName]));

                if (objdic[objLSMem.siteIdTokenName].Length < 3)
                {
                    objdic["AN:"] = objdic[objLSMem.siteIdTokenName].PadRight(3, '0')  + objdic["AN:"];
                    logger.Info(string.Format("ServiceBusinessRulesManager::AddSiteId2CustId:: SiteId added after padding(appending character '0') is {0}", objdic[objLSMem.siteIdTokenName].PadRight(3, '0')));
                }
                else
                {
                    objdic["AN:"] = objdic[objLSMem.siteIdTokenName].Substring(0, 3) + objdic["AN:"];
                    logger.Info(string.Format("ServiceBusinessRulesManager::AddSiteId2CustId:: SiteId added after truncating(3 left most characters) is {0}", objdic[objLSMem.siteIdTokenName].Substring(0, 3)));
                }

                //objdic["AN:"] = objdic["AN:"] + objdic["SI:"];
                logger.Info(string.Format("ServiceBusinessRulesManager::AddSiteId2CustId():: Account number(AN+{0}) created as {1}",objLSMem.siteIdTokenName, objdic["AN:"]));
            }
            return objdic;
        }

       /// <summary>
        /// Validate required tokens are present in the received ICOMS Message
       /// </summary>
       /// <param name="objdic"></param>
       /// <param name="str4LookUp"></param>
       /// <returns></returns>
        public string checkRequiredTokensPresent(Dictionary<string, string> objdic, string[] str4LookUp)
        {
            logger.Info("ServiceBusinessRulesManager::checkRequiredTokensPresent() called");
            string strErrCode = string.Empty;
            foreach (string str in str4LookUp)
	        {
                // Check to verify required tokens are available inthe ICOMS Message
                logger.Info(string.Format("ServiceBusinessRulesManager::checkRequiredTokensPresent :: Checking for token \"{0}\" is missing or not ", str));

                // skip the validation for optional tokens
                if (str == "SV:") continue;

                if (!objdic.ContainsKey(str))
                {
                    using (ServiceTranslationManager trMgr = new ServiceTranslationManager())
                    {
                        // If any of token is missing return error code
                        strErrCode = trMgr.getErrorCode4MissingTokens(str);
                        logger.Info("ServiceBusinessRulesManager::checkRequiredTokensPresent() failed");                        
                        return strErrCode;
                    }
                }
            }
            logger.Info("ServiceBusinessRulesManager::checkRequiredTokensPresent() success");
            return string.Empty;             
        }


        /// <summary>
        /// Validate length of required tokens that was present in the received ICOMS Message
        /// </summary>
        /// <param name="objdic"></param>
        /// <param name="str4LookUp"></param>
        /// <returns></returns>
        public string checkRequiredLengthOfTokens(Dictionary<string, string> objdic, ServiceListenerMembers templistenerMembers)
        {
            logger.Info("ServiceBusinessRulesManager::checkRequiredLengthOfTokens() called");

            string strErrCode = string.Empty;
            string strSiteTokenName = templistenerMembers.siteIdTokenName;

            logger.Info(string.Format("ServiceBusinessRulesManager::checkRequiredLengthOfTokens :: siteId TokenName is {0}", strSiteTokenName));

            foreach (KeyValuePair<string, string> pair in objdic)
            {
                using(ServiceTranslationManager trMgr=new ServiceTranslationManager())
                {
                    // Get relavent error code if any mismatch in token length
                    logger.Info(string.Format("ServiceBusinessRulesManager::checkRequiredLengthOfTokens :: Checking length for token \"{0}\" ", pair.ToString()));
                    if (pair.Key.Equals(strSiteTokenName))continue;
                    strErrCode = trMgr.getErrorCode4lengthOfTokens(pair);
                    if (strErrCode != string.Empty)
                    {
                        logger.Info("ServiceBusinessRulesManager::checkRequiredLengthOfTokens() failed");
                        return strErrCode;
                    }
                }

            }
            logger.Info("ServiceBusinessRulesManager::checkRequiredLengthOfTokens() success");
            return string.Empty;            
        }

               
        /// <summary>
        ///  Create native address format from ICOMS "EA" token and send it to 4c interface
        /// </summary>
        /// <param name="objdic"></param>
        /// <param name="objSerLisMem"></param>
        /// <returns></returns>
        public Dictionary<string, string> getNativeFormat4cAddress(Dictionary<string, string> objdic, ServiceListenerMembers objSerLisMem)
        {
            
            logger.Info("ServiceBusinessRulesManager::getNativeFormat4cAddress() called");
            string strCurrEA = objdic["EA:"];
            string strRetEA = string.Empty;
            bool bln4cNtvFlg= Convert.ToBoolean(objSerLisMem.NativeFormat4cFlag);
            logger.Info(string.Format("EA token value is...  {0}", strCurrEA));
            logger.Info(string.Format("DeviceIdFormat value is... {0}", objSerLisMem.DeviceIdFormat));
            logger.Info(string.Format("NativeFormat4cFlag value is... {0}", bln4cNtvFlg));            
            switch (objSerLisMem.DeviceIdFormat)
            {
                case "1":
                    strRetEA = strCurrEA.Substring(0, 12);
                    break;
                case "2":                    
                    if(bln4cNtvFlg)
                    {
                        strRetEA = strCurrEA.Substring(0, 17);
                    }
                    else
	                {
                        strRetEA = strCurrEA.Substring(0, 17).Replace(":", string.Empty);
	                }
                    break;
                case "3":
                    if (bln4cNtvFlg)
                    {
                        strRetEA = strCurrEA.Substring(0, 13);
                    }
                    else
                    {
                        strRetEA = (Convert.ToInt64(strCurrEA.Substring(0, 13))).ToString("X12");
                    }
                    break;
                case "4":
                    if (bln4cNtvFlg)
                    {
                        strRetEA = strCurrEA.Substring(0, 13);
                    }
                    else
                    {
                        strRetEA = (Convert.ToInt64(strCurrEA.Substring(0, 13))).ToString("X12");
                    }
                    break;
                case "5":
                    // 000-NNNNN-NNNNN-NNN
                    strRetEA = strCurrEA.Substring(0, 19);
                    break;
                default:
                    logger.Error("Unknown Device Id format received. Please configure correct device format id.");
                    break;
            }
            logger.Info(string.Format("ICOMS EA({0}) value converted as \"{1}\" to send CRM", strCurrEA,strRetEA));
            objdic["EA:"] = strRetEA;
            return objdic;
        }

        /// <summary>
        ///  get product id list by removing leading 0's
        /// </summary>
        /// <param name="objdic"></param>
        /// <returns></returns>
        public List<string> getProductIdList(Dictionary<string, string> objdic)
        {
            logger.Info("ServiceBusinessRulesManager::getProductIdList() called");
            if (objdic.ContainsKey("SV:")) // token available
            {
                List<string> strPrIds = new List<string>(objdic["SV:"].Split('/'));
                List<Int32> intPrIds = strPrIds.ConvertAll<Int32>(Convert.ToInt32);
                logger.Info(string.Format("ServiceBusinessRulesManager::getProductIdList() :: Product list created as {0}", String.Join(",", intPrIds)));
                return intPrIds.ConvertAll<string>(Convert.ToString);
            }
            else // token not available
            {
                logger.Info("ServiceBusinessRulesManager::getProductIdList() :: No Product list created since \"SV\" token is not available");
                return(new List<string>());
            }                 
           
        }


    }
}
