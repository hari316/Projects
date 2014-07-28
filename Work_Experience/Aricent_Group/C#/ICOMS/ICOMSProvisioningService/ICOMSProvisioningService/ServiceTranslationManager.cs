using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using log4net;

namespace ICOMSProvisioningService
{
 public class ServiceTranslationManager : IDisposable
    {

     private static readonly ILog logger = LogManager.GetLogger(typeof(ServiceTranslationManager));
        /// <summary>
        ///  Get CUI/CCI/EQI from received ICOMS messages
        /// </summary>
        /// <param name="strICOMIn"></param>
        /// <returns></returns>
        public string findICOMInputFormat(string strICOMIn)
        {
            logger.Info("ServiceTranslationManager::findICOMInputFormat() called");
            if (strICOMIn.IndexOf("CUI") != -1)
            {
                strICOMIn="CUI";
            }
            else if (strICOMIn.IndexOf("CCI") != -1)
            {
                strICOMIn = "CCI";
            }
            else if (strICOMIn.IndexOf("EQI") != -1)
            {
                strICOMIn = "EQI";
            }
            else if (strICOMIn.IndexOf("ACK") != -1)
            {
                strICOMIn = "ACK";
            }
            else
            {
                strICOMIn = "INVALID";
            }

            return strICOMIn;
        }


       /// <summary>
        /// get xxxxxIyyyCUI/xxxxxIyyyCCI/xxxxxIyyyEQI from received ICOMS messages
       /// </summary>
       /// <param name="strICOMIn"></param>
       /// <returns></returns>
        public string getFullInputFormatValue(string strICOMIn)
        {
            logger.Info("ServiceTranslationManager::getFullInputFormatValue() called");
            return strICOMIn.Substring(0, strICOMIn.IndexOf(","));
        }

       /// <summary>
       /// Form responseformat xxxxxVyyyCUI/xxxxxVyyyCCI/xxxxxVyyyEQI from received ICOMS messages
       /// </summary>
       /// <param name="strICOMIn"></param>
       /// <returns></returns>
        public string getFullResponseFormatValue(string strICOMIn)
        {
            logger.Info("ServiceTranslationManager::getFullResponseFormatValue() called");
            return strICOMIn.Substring(0, strICOMIn.IndexOf(",")).Replace("I","V");
        }


        /// <summary>
        /// Create dictionay object to collect key-value pairs for required tokens for received messages from ICOMS
        /// </summary>
        /// <param name="strICOMIn"></param>
        /// <param name="arrlkpStr"></param>
        /// <returns></returns>
        public Dictionary<string, string> getDataFor4cInterface(string strICOMIn, string[] arrlkpStr ,ServiceListenerMembers templistenerMembers)
        {
            logger.Info("ServiceTranslationManager::getDataFor4cInterface() called");
            Dictionary<string, string> dictFor4cInterface = new Dictionary<string, string>();
            string[] arrToken=new string[2];

            foreach (string str in arrlkpStr)  
            {
                arrToken = getKeyValuePair(strICOMIn, str);
                if (arrToken != null) {                   
                    dictFor4cInterface.Add(arrToken[0]+":", arrToken[1]);
                }              
            }

            // Checking for CustomerId Flag id if set to true siteId Token populated to the dictFor4cInterface based on config
            if (Convert.ToBoolean(templistenerMembers.CustomerIdFlag) == true)
            {
                logger.Info(string.Format("ServiceTranslationManager::getDataFor4cInterface() CustomerId Flag set to True ,SiteId token {0}: as per Config", templistenerMembers.siteIdTokenName));

                arrToken = getKeyValuePair(strICOMIn, templistenerMembers.siteIdTokenName);
                dictFor4cInterface.Add(arrToken[0] + ":", arrToken[1]);
            }

            return dictFor4cInterface;
        }


        /// <summary>
        /// get Key-Value pair for specific token
        /// </summary>
        /// <param name="strICOMIn"></param>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public string[] getKeyValuePair(string strICOMIn, string strKey)
        {
            logger.Info("ServiceTranslationManager::getKeyValuePair() called");
            string[] arrTokens=null;
            string strFullToken;
            int strStartPos = strICOMIn.IndexOf(strKey);
            if (strStartPos != -1)
            {  
                arrTokens = new string[2];
                int strNxtDeliStrPos = strICOMIn.IndexOf(",", strStartPos);
                if (strNxtDeliStrPos != -1)
                {
                    strFullToken = strICOMIn.Substring(strStartPos, (strNxtDeliStrPos - strStartPos));
                    logger.Info(string.Format("ServiceTranslationManager::getKeyValuePair() return value... {0}", strFullToken));
                }
                else
                {
                    strFullToken = strICOMIn.Substring(strStartPos);
                    logger.Info(string.Format("ServiceTranslationManager::getKeyValuePair() return value... {0}", strFullToken));
                }                
                //arrTokens = strFullToken.Split(':');              
                arrTokens[0] = strFullToken.Substring(0, strFullToken.IndexOf(":"));
                arrTokens[1] = strFullToken.Substring(strFullToken.IndexOf(":")+1);

            }
            return arrTokens;
        }

        /// <summary>
        /// get value for specific token
        /// </summary>
        /// <param name="strICOMIn"></param>
        /// <param name="strKey"></param>
        /// <returns></returns>
        public string getValue4Key(string strICOMIn, string strKey)
        {
            logger.Info("ServiceTranslationManager::getValue4Key() called");
            string[] arrToken=new string[2];
            string retStr = string.Empty;
            arrToken = getKeyValuePair(strICOMIn, strKey);
            if (arrToken != null)
            {
                retStr= arrToken[1];
            }
            logger.Info(string.Format("ServiceTranslationManager::getValue4Key() returned \"{0}\" for \"{1}\"", retStr, strKey));
            return retStr;
        }

       /// <summary>
        ///  Convert to string message for gnerated tokens for received ICOMS messages
        ///  Ex: AN:010203040,SI:013
       /// </summary>
       /// <param name="d"></param>
       /// <returns></returns>
       public string GetLine(Dictionary<string, string> d)
        {
            logger.Info("ServiceTranslationManager::GetLine() called");
            // Build up each line one-by-one and them trim the end
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, string> pair in d)
            {
               // builder.Append(pair.Key).Append(":").Append(pair.Value).Append(',');
                builder.Append(pair.Key).Append(pair.Value).Append(',');
            }
            string result = builder.ToString();
            // Remove the final delimiter
            result = result.TrimEnd(',');
            return result;
        }


     /// <summary>     
     ///  Format response message format to ICOMS message - 
       ///  Ex: <![CDATA[xxxxxVyyyCUI,<Response Code>,TI:<Transaction ID>.]]>
       ///  Ex: <![CDATA[xxxxxVyyyCCI,<Response Code>,TI:L<Transaction ID>.]]>
       ///  Ex: <![CDATA[xxxxxVyyyEQI,<Response Code>, TI:<Transaction ID>.]]>
     /// </summary>
     /// <param name="strFmtTxt"></param>
     /// <param name="strResFmt"></param>
     /// <param name="strResCode"></param>
     /// <param name="strTIValue"></param>
     /// <returns></returns>
       public string formatResponse(string strFmtTxt,string strResFmt, string strResCode, string strTIValue)
       {
           logger.Info("ServiceTranslationManager::formatResponse() called");
           string buildResMessage = string.Empty;
           string[] strBuildResFmt = new string[2];
           string strCalNumlen = string.Empty;
           string strGetSplitTxt = string.Empty;
           strBuildResFmt = strResFmt.Split('V');                    
           strBuildResFmt[0] = "00000";
           //  strResFmt = strBuildResFmt[0] + "V" + strBuildResFmt[1];
           strResFmt = "V" + strBuildResFmt[1];
                     
           switch (strFmtTxt)
           {
               case "CUI":
                   buildResMessage = string.Format(ServiceConstantsManager.CONST_RES_CUI, strResFmt, strResCode, strTIValue);                                      
                   break;
               case "CCI":
                   buildResMessage = string.Format(ServiceConstantsManager.CONST_RES_CCI, strResFmt, strResCode, strTIValue);
                   break;
               case "EQI":
                   buildResMessage = string.Format(ServiceConstantsManager.CONST_RES_EQI, strResFmt, strResCode, strTIValue);
                   break;
               case "ACK":
                   buildResMessage = string.Format(ServiceConstantsManager.CONST_RES_ACK, strResFmt, strResCode);
                   break;
               default:
                   logger.Warn("Invalid Response format received");
                   buildResMessage = string.Format(ServiceConstantsManager.CONST_RES_XXX, strResFmt, strResCode, strTIValue);
                   break;
           }

           buildResMessage = buildResMessage = buildResMessage + "\r";

           logger.Info(string.Format("ServiceTranslationManager::formatResponse() formatted response...  {0}", buildResMessage.Length.ToString("D5") + buildResMessage.Substring(5)));
           logger.Info("ServiceTranslationManager::formatResponse() success");
           return buildResMessage.Length.ToString("D5") + buildResMessage;
           //return buildResMessage.Substring(5).Length.ToString("D5") + buildResMessage.Substring(5);
       }

       /// <summary>
       /// replace  a letter for a given string
       /// </summary>
       /// <param name="i"></param>
       /// <param name="value"></param>
       /// <param name="word"></param>
       /// <returns></returns>
       public string ReplaceAtIndex(int i, char value, string word) 
       {
           logger.Info("ServiceTranslationManager::ReplaceAtIndex() called");
           char[] letters = word.ToCharArray();
           letters[i] = value; 
           return string.Join("", letters);
       }

     /// <summary>
     /// Get respective error code for respective missing token
     /// </summary>
     /// <param name="tknName"></param>
     /// <returns></returns>
       public string getErrorCode4MissingTokens(string tknName)
       {
           logger.Info("ServiceTranslationManager::getErrorCode4MissingTokens() called");
           string retErrCode = string.Empty;
           switch (tknName)
           {
               case "TI:":
                   retErrCode = ServiceConstantsManager.CONST_ICOMS_ERR_MISSING_TI; 
                   logger.Error(string.Format(" Error Code ({0}) return for missing token ({1})", retErrCode, tknName));
                   break;
               case "AN:":
                   retErrCode = ServiceConstantsManager.CONST_ICOMS_ERR_MISSING_AN;
                   logger.Error(string.Format(" Error Code ({0}) return for missing token ({1})", retErrCode, tknName));
                   break;
               case "SI:":
                   retErrCode = ServiceConstantsManager.CONST_ICOMS_ERR_MISSING_SI;
                   logger.Error(string.Format(" Error Code ({0}) return for missing token ({1})", retErrCode, tknName));
                   break;
               case "AS:":
                   retErrCode = ServiceConstantsManager.CONST_ICOMS_ERR_MISSING_AS;
                   logger.Error(string.Format(" Error Code ({0}) return for missing token ({1})", retErrCode, tknName));
                   break;
               case "CL:":
                   retErrCode = ServiceConstantsManager.CONST_ICOMS_ERR_MISSING_CL;
                   logger.Error(string.Format(" Error Code ({0}) return for missing token ({1})", retErrCode, tknName));
                   break;
               case "ES:":
                   retErrCode = ServiceConstantsManager.CONST_ICOMS_ERR_MISSING_ES;
                   logger.Error(string.Format(" Error Code ({0}) return for missing token ({1})", retErrCode, tknName));
                   break;
               case "EA:":
                   retErrCode = ServiceConstantsManager.CONST_ICOMS_ERR_MISSING_EA;
                   logger.Error(string.Format(" Error Code ({0}) return for missing token ({1})", retErrCode, tknName));
                   break;
               case "AC:":
                   retErrCode = ServiceConstantsManager.CONST_ICOMS_ERR_MISSING_AC;
                   logger.Error(string.Format(" Error Code ({0}) return for missing token ({1})", retErrCode, tknName));
                   break;     
               default :
                   retErrCode = ServiceConstantsManager.CONST_ICOMS_ERR_MISSING_DEF;
                   logger.Error(string.Format(" Error Code ({0}) return for missing token ({1})", retErrCode, tknName));
                   break;     
           }
           return retErrCode;
       }

     /// <summary>
       /// Get respective error code if there is any mismatch in the length of token
     /// </summary>
     /// <param name="pair"></param>
     /// <returns></returns>
       public string getErrorCode4lengthOfTokens(KeyValuePair<string,string> pair)
       {
           logger.Info("ServiceTranslationManager::getErrorCode4lengthOfTokens() called");
           string retErrCode = string.Empty;
           switch (pair.Key)
           {
               case "TI:":
                   if (pair.Value.Length != ServiceConstantsManager.CONST_ICOMS_MSG_LEN_TI)
                   {
                       logger.Error(string.Format("{0}-{1} token length is not correct", pair.Key,pair.Value));
                       retErrCode = ServiceConstantsManager.CONST_ICOMS_ERR_LEN_TI;                       
                   }
                   break;
               case "AN:":
                   if (pair.Value.Length != ServiceConstantsManager.CONST_ICOMS_MSG_LEN_AN)
                   {
                       logger.Error(string.Format("{0}-{1} token length is not correct", pair.Key, pair.Value));
                       retErrCode = ServiceConstantsManager.CONST_ICOMS_ERR_LEN_AN;
                   }
                   break;

               //case "SI:":
               //    if (pair.Value.Length != ServiceConstantsManager.CONST_ICOMS_MSG_LEN_SI)
               //    {
               //        logger.Error(string.Format("{0}-{1} token length is not correct", pair.Key, pair.Value));
               //        retErrCode = ServiceConstantsManager.CONST_ICOMS_ERR_LEN_SI;
               //    }
               //    break;

               case "AS:":
                   if (pair.Value.Length != ServiceConstantsManager.CONST_ICOMS_MSG_LEN_AS)
                   {
                       logger.Error(string.Format("{0}-{1} token length is not correct", pair.Key, pair.Value));
                       retErrCode = ServiceConstantsManager.CONST_ICOMS_ERR_LEN_AS;
                   }
                   break;
               case "CL:":
                   if (pair.Value.Length != ServiceConstantsManager.CONST_ICOMS_MSG_LEN_CL)
                   {
                       logger.Error(string.Format("{0}-{1} token length is not correct", pair.Key, pair.Value));
                       retErrCode = ServiceConstantsManager.CONST_ICOMS_ERR_LEN_CL;
                   }
                   break;
               case "ES:":
                   if (pair.Value.Length != ServiceConstantsManager.CONST_ICOMS_MSG_LEN_ES || pair.Value.ToUpper() != "A" && pair.Value.ToUpper() != "I")
                   {
                       logger.Error(string.Format("{0}-{1} token length is not correct", pair.Key, pair.Value));
                       retErrCode = ServiceConstantsManager.CONST_ICOMS_ERR_LEN_ES;
                   }
                   break;
               case "EA:":
                   if (pair.Value.Length != ServiceConstantsManager.CONST_ICOMS_MSG_LEN_EA)
                   {
                       logger.Error(string.Format("{0}-{1} token length is not correct", pair.Key, pair.Value));
                       retErrCode = ServiceConstantsManager.CONST_ICOMS_ERR_LEN_EA;
                   }
                   break;
               case "AC:":
                   if (pair.Value.Length != ServiceConstantsManager.CONST_ICOMS_MSG_LEN_AC)
                   {
                       logger.Error(string.Format("{0}-{1} token length is not correct", pair.Key, pair.Value));
                       retErrCode = ServiceConstantsManager.CONST_ICOMS_ERR_LEN_AC;
                   }
                   break;
               case "SV:":
                   Regex exp4SV = new Regex("^\\d{1,"+ ServiceConstantsManager.CONST_ICOMS_MSG_LEN_SV+"}(/\\d{1,"+ServiceConstantsManager.CONST_ICOMS_MSG_LEN_SV+"})*$"); 
                    if(!exp4SV.IsMatch(pair.Value))
                    {
                       logger.Error(string.Format("{0}-{1} token length is not correct", pair.Key, pair.Value));
                       retErrCode = ServiceConstantsManager.CONST_ICOMS_ERR_LEN_SV;
                    }
                   /*
                   if (pair.Value.Length < ServiceConstantsManager.CONST_ICOMS_MSG_LEN_SV)
                   {
                       logger.Error(string.Format("{0}-{1} token length is not correct", pair.Key, pair.Value));
                       retErrCode = ServiceConstantsManager.CONST_ICOMS_ERR_LEN_SV;
                   } */
                   break;              
           }
           return retErrCode;
       }


       void IDisposable.Dispose()
       {           
       }

    }
}
