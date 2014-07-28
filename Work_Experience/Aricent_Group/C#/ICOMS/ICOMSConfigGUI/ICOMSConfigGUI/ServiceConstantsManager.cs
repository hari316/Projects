using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICOMSConfigGUI
{
  public static class ServiceConstantsManager
    {

        // Specify Help text for ICOMS edit configuration controls - Sites
        public const string CONST_ICOMS_HELP_SITE_ID = "Provide Site Id, which uniquely identifies the site. Accepts upto 3 digits";
        public const string CONST_ICOMS_HELP_SITE_NAME = "Provide Site Name, which uniquely identifies the site. Accepts upto 30 Chars";
        public const string CONST_ICOMS_HELP_SITE_LISADD = "Provide Site Server IP Address, which is used by client for TCP Connection / communication.Accepts upto 50 chars";
        public const string CONST_ICOMS_HELP_SITE_LISPORT = "Provide Site Server Port Number, which is used by client for TCP Connection / communication.Accepts upto 5 digits";
        public const string CONST_ICOMS_HELP_SITE_NATFMT4CFLG = "Set value to Native format 4c flag, true - Mostly sends address as it is to 4c. false - performs conversion and sends to 4c";
        public const string CONST_ICOMS_HELP_SITE_CUSIDFLG = "Set value to unit address flag, true - appends \"siteid\" with \"AN\". false - sends only \"AN\"";
        public const string CONST_ICOMS_HELP_SITE_DEVFORMATID = "Set value to Device Format Id, You have provision to set 1-5";


        // Specify Help text for ICOMS edit configuration controls - CRM Connection
        public const string CONST_ICOMS_HELP_CRMCONNECTION_PRIMARYURL = "Provide Primary CRM URL value to send ICOMS message.Accepts upto 75 chars";
        public const string CONST_ICOMS_HELP_CRMCONNECTION_BACKUPURL = "Provide BACKUP CRM URL value to send ICOMS message.Accepts upto 75 chars";
        public const string CONST_ICOMS_HELP_CRMCONNECTION_PRIMARYCCI4CURL = "Provide Primary CRM URL value to send ICOMS CCI message.Accepts upto 75 chars";
        public const string CONST_ICOMS_HELP_CRMCONNECTION_BACKUPCCI4CURL = "Provide BACKUP CRM URL value to send ICOMS CCI message.Accepts upto 75 chars";
        public const string CONST_ICOMS_HELP_CRMCONNECTION_HTTPTIMEOUT = "Provide HTTPTimeout value for service to wait response from 4c interface.Accepts upto 5 digits";
        public const string CONST_ICOMS_HELP_CRMCONNECTION_RETRYNUM = "Provide Retry value for service to retry(n+1) in case of defined failure scenarios.Accepts upto 2 digits";
        public const string CONST_ICOMS_HELP_CRMCONNECTION_RETRYWAITDUR = "Provide Retry Wait Duration value for service to before proceeding for next retry in case of defined failure scenarios.Accepts upto 5 digits";
        public const string CONST_ICOMS_HELP_CRMCONNECTION_HTTPERRCODES = "Provide HTTP Error Codes(, separated value in of case more than one) to proceed for next retry in case of defined failure scenarios.Accepts upto 20 chars. Ex: xxx,xxx";
        public const string CONST_ICOMS_HELP_CRMCONNECTION_FAILOVERTIME = "Provide Fail Over Time value to wait the service before switching the CRM from failed to active.Accepts upto 5 digits";
        public const string CONST_ICOMS_HELP_TV_SELECT = "Navigate to respective node on the tree to see configuration information of selected node ";
        public const string CONST_ICOMS_HELP_BTN_ADDSITE = "Creates new site with the information available on the form when button is clicked";
        public const string CONST_ICOMS_HELP_BTN_REMSITE = "Removes the site informaiton when button is clicked";
        public const string CONST_ICOMS_HELP_BTN_UPDSITE = "Updates the site informaiton when button is clicked";
        public const string CONST_ICOMS_HELP_BTN_UPDCRMCON = "Updates the CRM Connection informaiton when button is clicked";
        public const string CONST_ICOMS_HELP_BTN_LOADDDEF = "Loads default configuration of service";
        public const string CONST_ICOMS_HELP_BTN_SAVE = "Saves all the changes into service configuration";

        // Specify Help text for ICOMS edit configuration controls - Load Balancer Connection
        public const string CONST_ICOMS_HELP_LBCONNECTION_LISADD = "Provide Load Balancer IP Address, which is used by client for TCP Connection / communication.Accepts upto 50 chars";
        public const string CONST_ICOMS_HELP_LBCONNECTION_LISPORT = "Provide Load Balancer Port Number, which is used by client for TCP Connection / communication.Accepts upto 5 digits";
        public const string CONST_ICOMS_HELP_BTN_UPDLBCON = "Updates the Load Balancer Connection information when button is clicked";  

        // warning & info messages
        public const string CONST_ICOMS_INFO_BTN_SAVE_SUCCESS = "Your configuration changes are saved. Still you have provision to bring default configuration by clicking 'Load Default' button";
        public const string CONST_ICOMS_WARN_BTN_LOAD_DEF = "Default configuration will be loaded. Do necessary changes and save the configuration to reflect your changes. Select OK to continue";
        public const string CONST_ICOMS_WARN_BTN_REM_SITE = "Would you like to delete selected site ({0}). Select \"Yes\" to proceed with delete?";
        public const string CONST_ICOMS_INFO_BTN_ADD_SITE_SUCCESS = "Site Added successfully";
        public const string CONST_ICOMS_INFO_BTN_UPD_SITE_SUCCESS = "Site Updated successfully";
        public const string CONST_ICOMS_INFO_BTN_REM_SITE_SUCCESS = "Site Removed successfully";
        public const string CONST_ICOMS_INFO_BTN_UPD_CRM_SUCCESS = "CRM Connection details Updated successfully";
        public const string CONST_ICOMS_INFO_BTN_UPD_LB_SUCCESS = "Load Balancer Connection details Updated successfully";
     
        public const string CONST_ICOMS_MESSAGEBOX_TITLE = "ICOMS Edit Configuration";

        // Validation messages
        public const string CONST_ICOMS_VALIDATE_MSG_DUP_SITE_ID = "Site Id already exist.Please Provide different Site Id.";
        public const string CONST_ICOMS_VALIDATE_MSG_DUP_SITE_NAME = "Site Name already exist.Please Provide different Site Name.";
        public const string CONST_ICOMS_VALIDATE_MSG_DUP_SITE_IP_PORT = "Listner Address and Port combination already exist in other site.Please Provide different IP Address & Port combination.";

        public const string CONST_ICOMS_VALIDATE_MSG_SITE_ID = "Please Provide Site Id";
        public const string CONST_ICOMS_VALIDATE_MSG_SITE_NAME = "Please Provide Site Name";
        public const string CONST_ICOMS_VALIDATE_MSG_SITE_LAdd = "Please Provide Server Listener Address";
        public const string CONST_ICOMS_VALIDATE_MSG_SITEL_PORT = "Please Provide Server Listener Address Port";

        public const string CONST_ICOMS_VALIDATE_MSG_CRMCONN_PURL = "Please Provide Primary CRM URL";
        public const string CONST_ICOMS_VALIDATE_MSG_CRMCONN_BURL = "Please Provide Backup CRM URL";
        public const string CONST_ICOMS_VALIDATE_MSG_CRMCONN_PCCIURL = "Please Provide Primary CRM URL for CCI messages";
        public const string CONST_ICOMS_VALIDATE_MSG_CRMCONN_BCCIURL = "Please Provide Backup CRM URL for CCI messages";
        public const string CONST_ICOMS_VALIDATE_MSG_CRMCONN_HTTPTO = "Please Provide HTTP Time Out Value";
        public const string CONST_ICOMS_VALIDATE_MSG_CRMCONN_RETRY = "Please Provide Retry Value";
        public const string CONST_ICOMS_VALIDATE_MSG_CRMCONN_RETRYWAITDUR = "Please Provide Retry Wait Duration Value";
        public const string CONST_ICOMS_VALIDATE_MSG_CRMCONN_HTTPERRCODS = "Please Provide HTTP Error Code Value(s)";
        public const string CONST_ICOMS_VALIDATE_MSG_CRMCONN_HTTPFALOVER = "Please Provide Fail Over time Value";

        public const string CONST_ICOMS_VALIDATE_MSG_LBCONN_Add = "Please Provide Load Balancer Listener Address";
        public const string CONST_ICOMS_VALIDATE_MSG_LBCONN_Port = "Please Provide Load Balancet Listener Port";

        public const string CONST_ICOMS_VALIDATE_MSG_DIRTY = "Your changes are not updated. Select \"Yes\" to ignore the changes and continue.";













      
    }
}

