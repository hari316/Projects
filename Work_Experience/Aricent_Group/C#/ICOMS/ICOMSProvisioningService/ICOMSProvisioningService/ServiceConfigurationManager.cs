using System;
using System.Collections.Generic;
using System.Xml;
using log4net;
using System.Configuration;

namespace ICOMSProvisioningService
{
   public class ServiceConfigurationManager
    {
       private static readonly ILog logger = LogManager.GetLogger(typeof(ServiceConfigurationManager));

       private XmlDocument xmlDoc;
       public static Dictionary<string,string> _CRMConnectionDetails;
       public static ServiceListenerMembers[] _CRMSiteDetails;
       public static LoadBalancerMembers _LBConnectionDetails;
       public static List<string> _lst4cRetryErrorCodes;
       
       /// <summary>
       /// Load configuration XML file
       /// </summary>        
        public bool LoadServiceConfigFile()
        {
            string cnfgFilePath = string.Empty;
            try
            {
                xmlDoc = new XmlDocument();
                // --* path to load config file from exe path *--
                //cnfgFilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + ConfigurationManager.AppSettings["ICOMSXMLFILEPATH"];
                
                // --* path to load config file from developemt exe path *--
                cnfgFilePath = "../../" + ConfigurationManager.AppSettings["ICOMSXMLFILEPATH"];
                
                logger.Info(string.Format("Configuration file located at {0}", cnfgFilePath));
                
                xmlDoc.Load(cnfgFilePath);
                logger.Info("Service Configuration file loaded successfully");
                return true;
            }
            catch (System.IO.FileNotFoundException)
            {
                logger.Error(string.Format("LoadServiceConfigFile(): Place copy service configuration file to : {0}", cnfgFilePath));
                logger.Error("ServiceConfigurationManager:: Error while loading service config file");
                return false;
            }           
            catch (Exception ex)
            {                
                logger.Error(string.Format("LoadServiceConfigFile(): Exception: {0}", ex.Message));
                logger.Error(string.Format("LoadServiceConfigFile(): Place copy service configuration file to : {0}", cnfgFilePath));
                logger.Error("ServiceConfigurationManager::Error while loading service config file");
                return false;
            }

           
        }


        /// <summary>
        /// Load site id,listeners,port,unit address,customer flag from configuration XML file in to object of ICOMSListenerMembers
        /// </summary> 
        public bool getAllListernsList()
        {
            
            try 
	        {	        
                logger.Info("ServiceConfigurationManager::getAllListernsList() called");
                XmlNodeList xnNodeList;
                ServiceListenerMembers[] lstMem;
                xnNodeList = xmlDoc.SelectNodes("/serviceconfiguration/sites/site");

                lstMem = new ServiceListenerMembers[xnNodeList.Count];
                int i = 0;
                foreach (XmlNode xn in xnNodeList)
                {               
                    ServiceListenerMembers lstMemlst = new ServiceListenerMembers();              
                    lstMemlst.siteid = xn.Attributes["id"].Value;
                    lstMemlst.siteIdTokenName = xn["TokenName4SiteId"].InnerText + ":";
                    lstMemlst.listenerAddress = xn["listenerAddress"].InnerText;
                    lstMemlst.listenerPort = xn["listenerPort"].InnerText;                    
                    lstMemlst.CustomerIdFlag = xn["CustomerIdFlag"].InnerText;
                    lstMemlst.DeviceIdFormat = xn["DeviceIdFormat"].InnerText;
                    lstMemlst.NativeFormat4cFlag = xn["NativeFormat4cFlag"].InnerText;
                    lstMem.SetValue(lstMemlst,i);
                    i++;              
                }
                if (lstMem.Length > 0)
                {
                    _CRMSiteDetails = lstMem;
                    logger.Info(string.Format("{0} Site(s) information was loaded successfully from service config file", lstMem.Length));
                    return true;
                }
                else
                {
                    _CRMSiteDetails = null;
                    logger.Error(" No Site(s) information found to loaded from service config file");
                    return false;
                }                
     
            }
	        catch (Exception ex)
	        {
                logger.Error(string.Format("getAllListernsList(): Exception: {0}", ex.Message));                
                logger.Error("ServiceConfigurationManager::Error while loading Site List from configuration file. Please correct site details from config file");
                return false;
	        }   
        }


       /// <summary>
       /// Load CRM Connection details in to dictionay object
       /// </summary>
        public bool LoadCRMConnectionDetails()
        {
            try
            {            
                logger.Info("ServiceConfigurationManager::LoadCRMConnectionDetails() called");
                XmlNode xnNode;
                _CRMConnectionDetails = new Dictionary<string, string>();
                xnNode = xmlDoc.SelectSingleNode("/serviceconfiguration/CRMconnection");
                if (xnNode == null)
                {
                    logger.Error(" No CRM Connection information found to loaded from service config file");
                    return false;
                }
                if (xnNode.HasChildNodes)
                {
                    foreach (XmlElement x in xnNode)
                    {
                        _CRMConnectionDetails.Add(x.Name.ToUpper(), x.InnerText);
                    }

                    // create list for 4c interface retry error codes
                    if(_CRMConnectionDetails.ContainsKey("HTTPERRORCODES"))
                    {
                        _lst4cRetryErrorCodes = new List<string>(_CRMConnectionDetails["HTTPERRORCODES"].Split(','));                   
                    }

                    logger.Info("CRM Connection information loaded from service config file");
                    return true;
                }
                else
                {
                    logger.Error(" No CRM Connection information found to loaded from service config file");
                    return false;
                }
               
            }
            catch (Exception ex)
            {

                logger.Error(string.Format("LoadCRMConnectionDetails(): Exception: {0}", ex.Message));
                logger.Error("ServiceConfigurationManager::Error while loading CRM Connection from configuration file. Please correct CRM connection details from config file");
                return false;
            }
        }

        /// <summary>
        /// Load Balancer Connection details in to dictionay object
        /// </summary>
        public bool LoadBalancerConnectionDetails()
        {
            try
            {
                logger.Info("ServiceConfigurationManager::LoadBalancerConnectionDetails() called");
                XmlNode xnNode;
                _LBConnectionDetails = new LoadBalancerMembers();             
                xnNode = xmlDoc.SelectSingleNode("/serviceconfiguration/LoadBalancer");
                if (xnNode == null)
                {
                    logger.Warn("The Load Balancer config is missing and the service does not support the healthcheck");
                    return false;
                }
                if (xnNode.HasChildNodes)
                {
                    _LBConnectionDetails.listenerAddress = xnNode["listenerAddress"].InnerText;
                    _LBConnectionDetails.listenerPort = xnNode["listenerPort"].InnerText;

                    logger.Info("Load Balancer Connection information loaded from service config file");
                    if (string.IsNullOrEmpty(_LBConnectionDetails.listenerAddress) || string.IsNullOrWhiteSpace(_LBConnectionDetails.listenerAddress) )
                    {
                        logger.Warn("The Load Balancer config is missing and the service does not support the healthcheck");
                        return false;
                    }
                    return true;
                }
                else
                {
                    logger.Warn("The Load Balancer config is missing and the service does not support the healthcheck");
                    return false;
                }

            }
            catch (Exception ex)
            {

                logger.Error(string.Format("LoadCRMConnectionDetails(): Exception: {0}", ex.Message));
                logger.Error("ServiceConfigurationManager::Error while loading Load Balancer connection from configuration file. Please correct the Load Balancer connection details from config file");
                return false;
            }
        }      
    }
}
