using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Collections.Specialized;
using System.Globalization;
using System.Xml.Serialization;
using TimeSheet_BL.timesheet;
using TimeSheet_SI.timesheet;
using utilities;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]


public class Service : System.Web.Services.WebService
{
    public Service () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Service));

  
    [WebMethod]
    public string HelloWorld() 
    {
        logger.Debug("Service:HelloWorld() called");          
        string result ="Hello World";               
        return result;        
    }


    /// <summary>
    ///     This Method returns Timesheet Details
    /// </summary>
    /// <param name="TransactionID"></param>
    /// <returns>
    /// <paramref name="timesheetEntity"/>
    /// </returns>
    /// <history>
    ///     Hari haran      02/05/2012      created
    /// </history>
    
    [WebMethod]
    public timesheetEntity getTimesheetDetails(string TransactionID)
    {
        logger.Debug("Service:getTimesheetDetails() called");        
        logger.Info(string.Format("Transaction ID received as {0} to get details ", TransactionID));
        try
        {
            timesheetEntity result = new timesheetEntity();
            timesheetInterface getTS_SI = new timesheetInterface();
            result = getTS_SI.getTimesheetDetails_SI(TransactionID);           
            return result;
        }
        catch (OracleException ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            string mailBody = string.Format(timesheet_Constants.mail_BodyFormat, System.DateTime.Now.ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("en-US")), TransactionID, ex.TargetSite.ToString(), ex.ToString());
            webServiceExHandling.Send_Email(timesheet_Constants.Email_Dic, mailBody);

            timesheetEntity Error = new timesheetEntity(0);
            Error.TS_headerDetails.ErrorCode = ex.Code;
            string expCode = ExpType(ex);
            Error.TS_headerDetails.ErrorMessage = timesheet_Constants.cnfgErrMessages[expCode];

            logger.Error(string.Format(" ErrorCode : {0}", Error.TS_headerDetails.ErrorCode.ToString()));
            logger.Error(string.Format(" ErrorMessage : {0}", Error.TS_headerDetails.ErrorMessage));
            logger.Error(string.Format(" ErrorStack : {0}", ex.StackTrace));            
            logger.Error("Service:getTimesheetDetails() returning error");
            return Error;

        }

        catch (Exception ex)
        {

            webServiceExHandling.ExceptionLog(ex);
            string mailBody = string.Format(timesheet_Constants.mail_BodyFormat, System.DateTime.Now.ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("en-US")), TransactionID, ex.TargetSite.ToString(), ex.ToString());
            webServiceExHandling.Send_Email(timesheet_Constants.Email_Dic, mailBody);

            timesheetEntity Error = new timesheetEntity(0);
            Error.TS_headerDetails.ErrorCode = 1;
            Error.TS_headerDetails.ErrorMessage = timesheet_Constants.Error;

            logger.Error(string.Format(" ErrorCode : {0}", Error.TS_headerDetails.ErrorCode.ToString()));
            logger.Error(string.Format(" ErrorMessage : {0}", Error.TS_headerDetails.ErrorMessage));
            logger.Error(string.Format(" ErrorStack : {0}", ex.StackTrace));
            logger.Error("Service:getTimesheetDetails() returning error");
            return Error;
        }
       
    }

  

    /// <summary>
    /// This Method Updates the Timesheet 
    /// </summary>
    /// <param name="TS_Entry"></param>
    /// <returns>
    /// <paramref name="result"/> In case of Success
    /// <paramref name="error"/> In case of Error
    /// </returns>
    /// <history>
    ///     Hari haran      02/05/2012      created
    /// </history>

    [WebMethod]
    public ts_UpdateOutputEntity updateTimesheetEntry([XmlElement("TS_Input")] ts_UpdateInputEntity[] TS_Entry)
    {
        logger.Debug("Service:updateTimesheetEntry() called");
        logger.Debug(string.Format("Transaction ID received as {0} for update  ", TS_Entry[0].TransactionID.ToString()));        

        ts_UpdateOutputEntity result = new ts_UpdateOutputEntity();;

        try
        {
            timesheetInterface updateTS_IS = new timesheetInterface();
            result = updateTS_IS.updateTimesheetEntry_SI(TS_Entry);
            return result;            
        }
        catch (OracleException ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            string mailBody = string.Format(timesheet_Constants.mail_BodyFormat, System.DateTime.Now.ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("en-US")), TS_Entry[0].TransactionID.ToString(), ex.TargetSite.ToString(), ex.ToString());
            webServiceExHandling.Send_Email(timesheet_Constants.Email_Dic, mailBody);
            
            result.StatusFlag = 1;
            string expCode = ExpType(ex);
            result.Message = timesheet_Constants.cnfgErrMessages[expCode];

            logger.Error(string.Format(" ErrorCode : {0}", result.StatusFlag.ToString()));
            logger.Error(string.Format(" ErrorMessage : {0}", result.Message));
            logger.Error(string.Format(" ErrorStack : {0}", ex.StackTrace));
            logger.Error("Service:updateTimesheetEntry() returning error");

            return result;

        }
        catch (Exception ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            string mailBody = string.Format(timesheet_Constants.mail_BodyFormat, System.DateTime.Now.ToString("dd-MMM-yyyy", CultureInfo.CreateSpecificCulture("en-US")), getTransID(TS_Entry), ex.TargetSite.ToString(), ex.ToString());
            webServiceExHandling.Send_Email(timesheet_Constants.Email_Dic, mailBody);

            result.StatusFlag = 1;
            result.Message = timesheet_Constants.Error;

            logger.Error(string.Format(" ErrorCode : {0}", result.StatusFlag.ToString()));
            logger.Error(string.Format(" ErrorMessage : {0}", result.Message));
            logger.Error(string.Format(" ErrorStack : {0}", ex.StackTrace));
            logger.Error("Service:updateTimesheetEntry() returning error");

            return result;
        }

    }

   /* [WebMethod]
    public ts_UpdateOutputEntity updateTimesheetEntry_New(ts_InputEntity TS_Input)
    {
        logger.Info("Method : updateTimesheetEntry Start");

        List<ts_UpdateInputEntity> IS_Entry_List = TS_Input.obj;
        ts_UpdateInputEntity[] TS_Entry = IS_Entry_List.Cast<ts_UpdateInputEntity>().ToArray();

        logger.Debug("Method : updateTimesheetEntry Transaction ID value : " + TS_Entry[0].TransactionID.ToString());

        ts_UpdateOutputEntity result = new ts_UpdateOutputEntity();
        try
        {
            timesheetInterface updateTS_IS = new timesheetInterface();
            result = updateTS_IS.updateTimesheetEntry_SI(TS_Entry);

            logger.Info("Method : updateTimesheetEntry Stop");

            return result;

        }
        catch (OracleException ex)
        {
            logger.Fatal("Database Exception At Web Method : updateTimesheetEntry" + ex.Message.ToString());

            ThrowSoapException soapExp = new ThrowSoapException();
            soapExp.EmailException(ex);
            //soapExp.convertToSoapException(ex);
            string ErrorMsg = ((NameValueCollection)System.Configuration.ConfigurationManager.GetSection("ErrorMessage"))["101"];
            result.StatusFlag = 1;
            result.Message = ErrorMsg;

            logger.Debug("Return object Error : ErrorCode = " + result.StatusFlag.ToString());
            logger.Debug("Return object Error : ErrorMessage = " + result.Message);
            logger.Error("Method : updateTimesheetEntry Stop");

            return result;

        }
        catch (Exception ex)
        {
            logger.Fatal("Exception At Web Method : updateTimesheetEntry" + ex.Message.ToString());

            ThrowSoapException soapExp = new ThrowSoapException();
            soapExp.EmailException(ex);
            //soapExp.convertToSoapException(ex);

            result.StatusFlag = 1;
            result.Message = "ERROR";

            logger.Debug("Return object Error : ErrorCode = " + result.StatusFlag.ToString());
            logger.Debug("Return object Error : ErrorMessage = " + result.Message);
            logger.Error("Method : updateTimesheetEntry Stop");

            return result;
        }

    }*/


    /// <summary>
    /// This Method determines if exception due to conn failure/DB operation
    /// </summary>
    /// <param name="Ex"> Exception </param>
    /// <returns>Error Code</returns>
    /// 
    private string ExpType(OracleException Ex)
    {

        foreach (int ErrNo in timesheet_Constants.dbErrorCodes)
        {
            if (Ex.Code.Equals(ErrNo))
            {
                return "101";
            }
        }
        return "102";
    }

    /// <summary>
    /// This Method returns Transaction ID
    /// </summary>
    /// <param name="Ex"> TS_Entry </param>
    /// <returns>TransactionID</returns>
    /// 
    private string getTransID(ts_UpdateInputEntity[] TS_Entry)
    {
        if (TS_Entry != null)
            return TS_Entry[0].TransactionID.ToString();
        else
            return string.Empty;
    } 
}