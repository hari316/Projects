using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.Xml.Serialization;
using TRT_BL;
using TRT_SI;
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
        logger.Info("Method : Hello World Start"); 
        string result = "Hello World";       
        logger.Info("Method : Hello World Stop");
        return result;        
    }

    /// <summary>
    ///     This Method authenticates the user
    /// </summary>
    /// <param name="UserId,UserPwd"></param>
    /// <returns>
    /// <paramref name="TRT_AuthUser"/>
    /// </returns>
    /// <history>
    ///     Hari haran      23/08/2012      created
    /// </history>
    /// 

    [WebMethod]
    public TRT_AuthUser authenticateUser(string UserId, string UserPwd)
    {
        logger.Debug("Service: authenticateUser() called");
        logger.InfoFormat("Input parameter Id : {0} ", UserId);
        logger.InfoFormat("Input parameter Password : {0} ", UserPwd);

        try
        {
            TRT_AuthUser result = new TRT_AuthUser();
            TRTInterface authUsr_SI = new TRTInterface();
            result = authUsr_SI.authenticateUser_SI(UserId, UserPwd);

            return result;
        }
        catch (Exception ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(SCT_Constants.mail_BodyFormat, System.DateTime.Now.ToString("F"), PReqNo, ex.TargetSite.ToString(), ex.ToString());
            //webServiceExHandling.Send_Email(SCT_Constants.Email_Dic, mailBody);

            TRT_AuthUser Error = new TRT_AuthUser();
            Error.StatusFlag = 1;
            Error.Message = TRT_Constants.Error;

            logger.Error("ErrorCode = " + Error.StatusFlag.ToString());
            logger.Error("ErrorMessage = " + Error.Message);
            logger.Error("Service: authenticateUser() returning error");

            return Error;
        }
    }

    /// <summary>
    ///     This Method gets all the Charge Codes based on search criteria
    /// </summary>
    /// <param name="SearchCriteria"></param>
    /// <returns>
    /// <paramref name="TRT_ChargeCodes"/>
    /// </returns>
    /// <history>
    ///     Hari haran      23/08/2012      created
    /// </history>
    /// 

    [WebMethod]
    public TRT_ChargeCodes getAllChargeCodes(string SearchCriteria)
    {
        logger.Debug("Service: getAllChargeCodes() called");
        logger.InfoFormat("Input parameter Search String : {0} ", SearchCriteria);    

        try
        {
            TRT_ChargeCodes result = new TRT_ChargeCodes();
            TRTInterface getCC_SI = new TRTInterface();
            result = getCC_SI.getAllChargeCodes_SI(SearchCriteria);
            return result;
        }
        catch (Exception ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(SCT_Constants.mail_BodyFormat, System.DateTime.Now.ToString("F"), PReqNo, ex.TargetSite.ToString(), ex.ToString());
            //webServiceExHandling.Send_Email(SCT_Constants.Email_Dic, mailBody);

            TRT_ChargeCodes Error = new TRT_ChargeCodes();
            Error.TRT_header.statusFlag = 1;
            Error.TRT_header.statusMsg = TRT_Constants.Error;

            logger.Error("ErrorCode = " + Error.TRT_header.statusFlag.ToString());
            logger.Error("ErrorMessage = " + Error.TRT_header.statusMsg);
            logger.Error("Service: getAllChargeCodes() returning error");

            return Error;
        }
    }


    /// <summary>
    ///     This Method gets details of the plot and purpose based on location ID
    /// </summary>
    /// <param name="LocId"></param>
    /// <returns>
    /// <paramref name="TRT_LocEntity"/>
    /// </returns>
    /// <history>
    ///     Hari haran      24/08/2012      created
    /// </history>
    /// 

    [WebMethod]
    public TRT_LocEntity getLocationDetails(string LocId)
    {
        logger.Debug("Service: getLocationDetails() called");
        logger.InfoFormat("Input parameter Location ID : {0} ", LocId);

        try
        {
            TRT_LocEntity result = new TRT_LocEntity();
            TRTInterface getCC_SI = new TRTInterface();
            result = getCC_SI.getLocationsDetails_SI(LocId);
            return result;
        }
        catch (Exception ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(SCT_Constants.mail_BodyFormat, System.DateTime.Now.ToString("F"), PReqNo, ex.TargetSite.ToString(), ex.ToString());
            //webServiceExHandling.Send_Email(SCT_Constants.Email_Dic, mailBody);

            TRT_LocEntity Error = new TRT_LocEntity();
            Error.TRT_header.statusFlag = 1;
            Error.TRT_header.statusMsg = TRT_Constants.Error;

            logger.Error("ErrorCode = " + Error.TRT_header.statusFlag.ToString());
            logger.Error("ErrorMessage = " + Error.TRT_header.statusMsg);
            logger.Error("Service: getLocationsDetails() returning error");
            return Error;
        }
    }

    /// <summary>
    ///     This Method gets details of the plot and purpose based on location ID
    /// </summary>
    /// <param name="LocId"></param>
    /// <returns>
    /// <paramref name="TRT_LocEntity"/>
    /// </returns>
    /// <history>
    ///     Hari haran      24/08/2012      created
    /// </history>
    /// 

    [WebMethod]
    public TRT_OutputEntity updateTaxiRequestDetails(TRT_InputEntity TRT_Entry)
    {
        logger.Debug("Service: updateTaxiRequestDetails() called");
        logger.InfoFormat("Input parameter Employee ID : {0} ", TRT_Entry.empNo);

        try
        {
            TRT_OutputEntity result = new TRT_OutputEntity();
            TRTInterface updateTRD_SI = new TRTInterface();
            result = updateTRD_SI.updateTaxiRequestDetails_SI(TRT_Entry);
            return result;
        }
        catch (SqlException ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(PST_Constants.mail_BodyFormat,System.DateTime.Now.ToString("F"),transId,ex.TargetSite.ToString(),ex.ToString());
            //webServiceExHandling.Send_Email(PST_Constants.Email_Dic, mailBody);
            webServiceExHandling.Send_Email(TRT_Constants.Email_Dic, "Testing");

            TRT_OutputEntity Error = new TRT_OutputEntity();
            Error.statusFlag = ex.ErrorCode;
            string expCode = ExpType(ex);
            Error.statusMsg = TRT_Constants.cnfgErrMessages[expCode];

            logger.Error("ErrorCode = " + Error.statusFlag.ToString());
            logger.Error("ErrorMessage = " + Error.statusMsg);
            logger.Error("Service: updateTaxiRequestDetails() returning error");

            return Error;
        }
        catch (Exception ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(SCT_Constants.mail_BodyFormat, System.DateTime.Now.ToString("F"), PReqNo, ex.TargetSite.ToString(), ex.ToString());
            //webServiceExHandling.Send_Email(PST_Constants.Email_Dic, mailBody);

            TRT_OutputEntity Error = new TRT_OutputEntity();
            Error.statusFlag = 1;
            Error.statusMsg = TRT_Constants.Error;

            logger.Error("ErrorCode = " + Error.statusFlag.ToString());
            logger.Error("ErrorMessage = " + Error.statusMsg);
            logger.Error("Service: updateTaxiRequestDetails() returning error");

            return Error;
        }
        
    }
    /// <summary>
    /// This Method determines if exception due to conn failure/DB operation
    /// </summary>
    /// <param name="Ex"> Exception </param>
    /// <returns>Error Code</returns>
    /// 
    private string ExpType(SqlException Ex)
    {

        foreach (int ErrNo in TRT_Constants.dbstatusFlags)
        {
            if (Ex.ErrorCode.Equals(ErrNo))
            {
                return "101";
            }
        }
        return "102";
    }

}