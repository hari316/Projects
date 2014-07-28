using System;

namespace ICOMSProvisioningService
{

    public class ServiceConstantsManager
    {

        // Specify required tokens to be extracted from each ICOMS Message(CUI/CCI/EQI)
        public static readonly string[] CONST_CUI = new string[] { "AN:", "AS:", "TI:" };
        public static readonly string[] CONST_CCI = new string[] { "AN:", "CL:", "TI:" };
        public static readonly string[] CONST_EQI = new string[] { "AN:", "EA:", "SV:", "TI:", "AC:", "ES:" };

        // Specify response format to ICOMS message(CUI/CCI/EQI)
        // XXX - format used for other then CUI/CCI/EQI
        public static readonly string CONST_RES_CUI = "{0},{1},TI:{2}.";
        public static readonly string CONST_RES_CCI = "{0},{1},TI:L{2}.";
        public static readonly string CONST_RES_EQI = "{0},{1},TI:{2}.";
        public static readonly string CONST_RES_ACK = "{0},{1}.";
        public static readonly string CONST_RES_XXX = "{0},{1},TI:{2}.";

        // Specify each token length for ICOMS message
        public const int CONST_ICOMS_MSG_LEN_TI = 18;
        public const int CONST_ICOMS_MSG_LEN_AN = 9;
        public const int CONST_ICOMS_MSG_LEN_SI = 3;
        public const int CONST_ICOMS_MSG_LEN_AS = 1;
        public const int CONST_ICOMS_MSG_LEN_CL = 9;
        public const int CONST_ICOMS_MSG_LEN_ES = 1;
        public const int CONST_ICOMS_MSG_LEN_EA = 32;
        public const int CONST_ICOMS_MSG_LEN_AC = 1;
        public const int CONST_ICOMS_MSG_LEN_SV = 7;

        // Specify error code for missing tokens from ICOMS message
        public const string CONST_ICOMS_ERR_MISSING_TI = "0000100";
        public const string CONST_ICOMS_ERR_MISSING_AN = "0000101";
        public const string CONST_ICOMS_ERR_MISSING_SI = "0000102";
        public const string CONST_ICOMS_ERR_MISSING_AS = "0000114";
        public const string CONST_ICOMS_ERR_MISSING_CL = "0000116";
        public const string CONST_ICOMS_ERR_MISSING_ES = "0000117";
        public const string CONST_ICOMS_ERR_MISSING_EA = "0000119";
        public const string CONST_ICOMS_ERR_MISSING_AC = "0000121";
        public const string CONST_ICOMS_ERR_MISSING_DEF = "0000901";


        // Specify error code for length mismatch tokens from ICOMS message
        public const string CONST_ICOMS_ERR_LEN_TI = "0000200";
        public const string CONST_ICOMS_ERR_LEN_AN = "0000201";
        public const string CONST_ICOMS_ERR_LEN_SI = "0000202";
        public const string CONST_ICOMS_ERR_LEN_AS = "0000214";
        public const string CONST_ICOMS_ERR_LEN_CL = "0000216";
        public const string CONST_ICOMS_ERR_LEN_ES = "0000217";
        public const string CONST_ICOMS_ERR_LEN_EA = "0000219";
        public const string CONST_ICOMS_ERR_LEN_AC = "0000221";
        public const string CONST_ICOMS_ERR_LEN_SV = "0000257";

        // Specify error code for CRMs(Priamry/Backup)
        public const string CONST_ICOMS_ERR_CRM_PnB_FAILED = "0000908";


        // Specify Help text for ICOMS edit configuration controls - Sites
        public const string CONST_ICOMS_HELP_SITE_ID = "Provide Site Id, which uniquely identifies the site. Accepts 3 digits value";
        public const string CONST_ICOMS_HELP_SITE_NAME = "Provide Site Name, which uniquely identifies the site. Accepts upto 30 Chars";
        public const string CONST_ICOMS_HELP_SITE_LISADD = "Provide Site Server IP Address, which is used by client for TCP Connection / communication.Accepts upto 50 chars";
        public const string CONST_ICOMS_HELP_SITE_LISPORT = "Provide Site Server Port Number, which is used by client for TCP Connection / communication.Accepts upto 5 digits";
        public const string CONST_ICOMS_HELP_SITE_TOKEN_NAME4SID = "Provide Token Name for Site ID. Accepts 2 Chars only";
        public const string CONST_ICOMS_HELP_SITE_NATFMT4CFLG = "Set value to Native format 4c flag, true - Mostly sends address as it is to 4c. false - performs conversion and sends to 4c";
        public const string CONST_ICOMS_HELP_SITE_CUSIDFLG = "Set value to Customer Id flag, true - appends \"siteid\" with \"AN\". false - sends only \"AN\"";
        public const string CONST_ICOMS_HELP_SITE_DEVFORMATID = "Set value to Device Id Format, You have provision to set 1-5";

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
        public const string CONST_ICOMS_HELP_LBCONNECTION_LISADD = "Provide Load Balancer Listening IP Address, which is used by client for TCP Connection / communication.Accepts upto 50 chars";
        public const string CONST_ICOMS_HELP_LBCONNECTION_LISPORT = "Provide Load Balancer Listening Port Number, which is used by client for TCP Connection / communication.Accepts upto 5 digits";
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

        // warning & info messages
        public const string CONST_ICOMS_VALIDATE_MSG_DUP_SITE_ID = "Site Id already exist.Please Provide different Site Id.";
        public const string CONST_ICOMS_VALIDATE_MSG_DUP_SITE_NAME = "Site Name already exist.Please Provide different Site Name.";
        public const string CONST_ICOMS_VALIDATE_MSG_DUP_SITE_IP_PORT = "Listner Address and Port combination already exist in other site.Please Provide different IP Address & Port combination";

        public const string CONST_ICOMS_VALIDATE_MSG_SITE_ID = "Please Provide Site Id";
        public const string CONST_ICOMS_VALIDATE_MSG_SITE_NAME = "Please Provide Site Name";
        public const string CONST_ICOMS_VALIDATE_MSG_SITE_LAdd = "Please Provide Server Listener Address";
        public const string CONST_ICOMS_VALIDATE_MSG_SITEL_PORT = "Please Provide Server Listener Address Port";
        public const string CONST_ICOMS_VALIDATE_MSG_TOKEN_NAME4SITE_ID = "Please Provide Valid Token Name for Site Id ";

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
