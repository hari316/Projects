using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace ICOMSProvisioningService
{
   public class CRM4cInterfaceAvailabilityManager
   {
       private static readonly ILog logger = LogManager.GetLogger(typeof(CRM4cInterfaceAvailabilityManager));    
       private static string _activeCRMURL;
       private static string _activeCRMURL4CCI;
       private static string _activeCRMName;
       private static string _activeCRMFullName;
       private static bool _isMsgGoing2Primary;
       private static bool _isMsgGoing2Backup;
       private static bool _isAllCRMsFailed;   
       private static bool _isBackupCRMExist;   
       
       public static string ActiveCRMURL
       {
           get {return _activeCRMURL;}
           set {
               switch (value.ToUpper())
               {
                   case "P" :                       
                       _activeCRMName = "P";
                       _activeCRMFullName = "PRIMARY";
                       _activeCRMURL = ServiceConfigurationManager._CRMConnectionDetails["PRIMARYURL"];
                       _activeCRMURL4CCI = ServiceConfigurationManager._CRMConnectionDetails["PRIMARYCCI4CURL"];
                        
                       break;
                   case "B" :
                       _activeCRMName = "B";
                       _activeCRMFullName = "BACKUP";
                       _activeCRMURL = ServiceConfigurationManager._CRMConnectionDetails["BACKUPURL"];
                       _activeCRMURL4CCI = ServiceConfigurationManager._CRMConnectionDetails["BACKUPCCI4CURL"];
                        break;
                   default:
                        _activeCRMName = "P";
                        _activeCRMFullName = "PRIMARY";
                        _activeCRMURL = ServiceConfigurationManager._CRMConnectionDetails["PRIMARYURL"];
                        _activeCRMURL4CCI = ServiceConfigurationManager._CRMConnectionDetails["PRIMARYCCI4CURL"];
                       break;
               }
                
           }
       }

       public static string ActiveCRMURL4CCI
       {
           get { return _activeCRMURL4CCI; }
       }

       public static string getActiveCRMName
       {
           get { return _activeCRMName; }
       }

       public static string getActiveCRMFullName
       {
           get { return _activeCRMFullName; }
       }

       public static bool IsBackupCRMExist
       {
           get { return _isBackupCRMExist; }
           set { _isBackupCRMExist = value; }
       }

       public static string changeActiveCRM(string currActCRM)
       {
           if (currActCRM.ToUpper().Equals("P"))                    
           {               
               return "B";
           }
           else
           {               
               return "P";
           }           
       }

       public static bool IsMsgGoing2Primary
       {
           get { return _isMsgGoing2Primary; }
           set { _isMsgGoing2Primary = value; }
       }

       public static bool IsMsgGoing2Backup
       {
           get { return _isMsgGoing2Backup; }
           set { _isMsgGoing2Backup = value; }
       }

       public static bool IsAllCRMsFailed
       {
           get { return _isAllCRMsFailed; }
           set { _isAllCRMsFailed = value; }
       }

       public static bool checkBackupCRMExistence()
       {
           if (ServiceConfigurationManager._CRMConnectionDetails.ContainsKey("BACKUPURL"))
           {
               if (ServiceConfigurationManager._CRMConnectionDetails["BACKUPURL"].Trim().Length > 0)
               {
                   return true;
               }
           }
           return false;
       }
              
   }
}
