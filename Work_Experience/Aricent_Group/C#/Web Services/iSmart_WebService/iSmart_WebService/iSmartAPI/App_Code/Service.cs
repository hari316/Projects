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
using iSmart_BL.iSmart;
using iSmart_SI.iSmart;
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
        string result ="Hello World";       
        logger.Info("Method : Hello World Stop");
        return result;        
    }


    /// <summary>
    ///     This Method returns iSmart Details
    /// </summary>
    /// <param name="P.Req.No"></param>
    /// <returns>
    /// <paramref name="iSmartEntity"/>
    /// </returns>
    /// <history>
    ///     Hari haran      07/05/2012      created
    /// </history>
    
    [WebMethod]
    public iSmartEntity getPurchaseRequestDetails(string PReqNo)
    {
        logger.Debug("Service:getPurchaseRequestDetails() called");
        logger.InfoFormat("PReqNo  received as {0} to get details",PReqNo);

        try
        {
            iSmartEntity result = new iSmartEntity();
            iSmartInterface getIS_SI = new iSmartInterface();
            result = getIS_SI.getPurchaseRequestDetails_SI(PReqNo);
            return result;
        }
        catch (SqlException ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            string mailBody = string.Format(iSmart_Constants.mail_BodyFormat,System.DateTime.Now.ToString("F"),PReqNo,ex.TargetSite.ToString(),ex.ToString());
            webServiceExHandling.Send_Email(iSmart_Constants.Email_Dic, mailBody);

            iSmartEntity Error = new iSmartEntity();
            Error.IS_headerDetails.ErrorCode = ex.Number;
            string expCode = ExpType(ex);
            Error.IS_headerDetails.ErrorMessage = iSmart_Constants.cnfgErrMessages[expCode];

            logger.Error("ErrorCode : " + Error.IS_headerDetails.ErrorCode.ToString());
            logger.Error("ErrorMessage : " + Error.IS_headerDetails.ErrorMessage);
            logger.Error(string.Format(" ErrorStack : {0}", ex.StackTrace));
            logger.Error("Service:getPurchaseRequestDetails() returning error");

            return Error;
        }

        catch (Exception ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            string mailBody = string.Format(iSmart_Constants.mail_BodyFormat, System.DateTime.Now.ToString("F"), PReqNo, ex.TargetSite.ToString(), ex.ToString());
            webServiceExHandling.Send_Email(iSmart_Constants.Email_Dic, mailBody);

            iSmartEntity Error = new iSmartEntity();
            Error.IS_headerDetails.ErrorCode = 1;
            Error.IS_headerDetails.ErrorMessage = iSmart_Constants.Error;
           
            logger.Error("ErrorCode : " + Error.IS_headerDetails.ErrorCode.ToString());
            logger.Error("ErrorMessage : " + Error.IS_headerDetails.ErrorMessage);
            logger.Error(string.Format(" ErrorStack : {0}", ex.StackTrace)); 
            logger.Error("Service:getPurchaseRequestDetails() returning error");

            return Error;
        }
       
    }


    /// <summary>
    /// This Method Updates the iSmart Details
    /// </summary>
    /// <param name="TS_Entry"></param>
    /// <returns>
    /// <paramref name="result"/> In case of Success
    /// <paramref name="error"/> In case of Error
    /// </returns>
    /// <history>
    ///     Hari haran      08/05/2012      created
    /// </history>
    /// 

    [WebMethod]
    public iSmart_UpdateOutputEntity updatePurchaseRequest([XmlElement("IS_Input")] iSmart_UpdateInputEntity[] IS_Entry)
    {
        logger.Debug("Service:updatePurchaseRequest() called");
        logger.Debug(string.Format("PReqNo. received as {0} for update  ", IS_Entry[0].PReqNo.ToString()));

        iSmart_UpdateOutputEntity result = new iSmart_UpdateOutputEntity();
        try
        {
            iSmartInterface updateTS_IS = new iSmartInterface();
            result = updateTS_IS.updatePurchaseRequest_SI(IS_Entry);
            return result;
        }
        catch (SqlException ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            string mailBody = string.Format(iSmart_Constants.mail_BodyFormat, System.DateTime.Now.ToString("F"), IS_Entry[0].PReqNo, ex.TargetSite.ToString(), ex.ToString());
            webServiceExHandling.Send_Email(iSmart_Constants.Email_Dic, mailBody);

            result.StatusFlag = 1;
            string expCode = ExpType(ex);
            result.Message = iSmart_Constants.cnfgErrMessages[expCode];

            logger.Error(string.Format(" ErrorCode : {0}", result.StatusFlag.ToString()));
            logger.Error(string.Format(" ErrorMessage : {0}", result.Message));
            logger.Error(string.Format(" ErrorStack : {0}", ex.StackTrace));
            logger.Error("Service:updatePurchaseRequest() returning error");

            return result;

        }
        catch (Exception ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            string mailBody = string.Format(iSmart_Constants.mail_BodyFormat, System.DateTime.Now.ToString("F"), getPreqNo(IS_Entry), ex.TargetSite.ToString(), ex.ToString());
            webServiceExHandling.Send_Email(iSmart_Constants.Email_Dic, mailBody);
           
            result.StatusFlag = 1;
            result.Message = iSmart_Constants.Error;

            logger.Error(string.Format(" ErrorCode : {0}", result.StatusFlag.ToString()));
            logger.Error(string.Format(" ErrorMessage : {0}", result.Message));
            logger.Error(string.Format(" ErrorStack : {0}", ex.StackTrace));
            logger.Error("Service:updatePurchaseRequest() returning error");

            return result;
        }
    }
    /*
    [WebMethod]
    public iSmart_UpdateOutputEntity updatePurchaseRequest_New( iSmart_InputEntity IS_Input)
    {
        logger.Info("Method : updateiSmartEntry Start");
        List<iSmart_UpdateInputEntity>IS_Entry_List = IS_Input.obj;
        iSmart_UpdateInputEntity[] IS_Entry = IS_Entry_List.Cast<iSmart_UpdateInputEntity>().ToArray();
      
        logger.Debug("Method : updateiSmartEntry PReqNo. value : " + IS_Entry[0].PReqNo.ToString());

        iSmart_UpdateOutputEntity result = new iSmart_UpdateOutputEntity();
        try
        {
            iSmartInterface updateTS_IS = new iSmartInterface();
            result = updateTS_IS.updatePurchaseRequest_SI(IS_Entry);

            logger.Info("Method : updateiSmartEntry Stop");
            
            return result;            
        }
        catch (SqlException ex)
        {
            logger.Fatal("Database Exception At Web Method : updateiSmartEntry" + ex.Message.ToString());

            int ErrConn = 18456;
            ThrowSoapException soapExp = new ThrowSoapException();            
            soapExp.EmailException(ex);
            //soapExp.convertToSoapException(ex);
            result.StatusFlag = 1;

            if (ex.Number.Equals(ErrConn))
            {
                string ErrorConnMsg = ((NameValueCollection)System.Configuration.ConfigurationManager.GetSection("ErrorMessage"))["101"];
                result.Message = ErrorConnMsg;
            }
            else
            {
                string ErrorDBMsg = ((NameValueCollection)System.Configuration.ConfigurationManager.GetSection("ErrorMessage"))["102"];
                result.Message = ErrorDBMsg;
            }
            logger.Debug("Return object Error : ErrorCode = " + result.StatusFlag.ToString());
            logger.Debug("Return object Error : ErrorMessage = " + result.Message);
            logger.Error("Method : updateiSmartEntry Stop");

            return result;

        }
        catch (Exception ex)
        {
            logger.Fatal("Exception At Web Method : updateiSmartEntry" + ex.Message.ToString());

            ThrowSoapException soapExp = new ThrowSoapException();            
            soapExp.EmailException(ex);
            //soapExp.convertToSoapException(ex);           
            result.StatusFlag = 1;
            result.Message = "ERROR";

            logger.Debug("Return object Error : ErrorCode = " + result.StatusFlag.ToString());
            logger.Debug("Return object Error : ErrorMessage = " + result.Message);
            logger.Error("Method : updateiSmartEntry Stop");

            return result;
        }
    }
    */

    /// <summary>
    /// This Method determines if exception due to conn failure/DB operation
    /// </summary>
    /// <param name="Ex"> Exception </param>
    /// <returns>Error Code</returns>
    /// 
    private string ExpType(SqlException Ex)
    {

        foreach (int ErrNo in iSmart_Constants.dbErrorCodes)
        {
            if (Ex.Number.Equals(ErrNo))
            {
                return "101";
            }
        }
        return "102";
    }

    /// <summary>
    /// This Method returns PreqNo
    /// </summary>
    /// <param name="Ex"> TS_Entry </param>
    /// <returns>Preq No</returns>
    /// 
    private string getPreqNo(iSmart_UpdateInputEntity[] IS_Entry)
    {
        if (IS_Entry != null)
            return IS_Entry[0].PReqNo.ToString();
        else
            return string.Empty;
    } 
}