using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Collections.Specialized;
using System.Xml.Serialization;
using PST_BL;
using PST_SI;
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
    ///     This Method returns PST search Details
    /// </summary>
    /// <param name="searchCriteria"></param>
    /// <returns>
    /// <paramref name="PST_InputEntity"/>
    /// </returns>
    /// <history>
    ///     Hari haran      22/07/2012      created
    /// </history>
    
    [WebMethod]
    public PST_OutputEntity employeeSearch(PST_InputEntity searchCriteria)
    {
        logger.Info("Method : employeeSearch Start");

        try
        {
            PST_OutputEntity result = new PST_OutputEntity();
            PSTInterface search_SI = new PSTInterface();
            result = search_SI.employeeSearch_SI(searchCriteria);

            logger.Info("Method : employeeSearch Stop");
            
            return result;
        }
        catch (OracleException ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(PST_Constants.mail_BodyFormat,System.DateTime.Now.ToString("F"),transId,ex.TargetSite.ToString(),ex.ToString());
            //webServiceExHandling.Send_Email(PST_Constants.Email_Dic, mailBody);

            PST_OutputEntity Error = new PST_OutputEntity();
            Error.PST_headerDetails.statusFlag = ex.ErrorCode;
            string expCode = ExpType(ex);
            Error.PST_headerDetails.statusMsg = PST_Constants.cnfgErrMessages[expCode];

            logger.Debug("Return object Error : statusFlag = " + Error.PST_headerDetails.statusFlag.ToString());
            logger.Debug("Return object Error : statusMsg = " + Error.PST_headerDetails.statusMsg);
            logger.Error("Method : employeeSearch Stop");

            return Error;
        }

        catch (Exception ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(PST_Constants.mail_BodyFormat, System.DateTime.Now.ToString("F"), transId, ex.TargetSite.ToString(), ex.ToString());
            //webServiceExHandling.Send_Email(PST_Constants.Email_Dic, mailBody);

            PST_OutputEntity Error = new PST_OutputEntity();
            Error.PST_headerDetails.statusFlag = 1;
            Error.PST_headerDetails.statusMsg = PST_Constants.Error;
           
            logger.Debug("Return object Error : statusFlag = " + Error.PST_headerDetails.statusFlag.ToString());
            logger.Debug("Return object Error : statusMsg = " + Error.PST_headerDetails.statusMsg);
            logger.Error("Method : employeeSearch Stop");

            return Error;
        }
       
    }

    /// <summary>
    /// This Method determines if exception due to conn failure/DB operation
    /// </summary>
    /// <param name="Ex"> Exception </param>
    /// <returns>Error Code</returns>
    /// 
    private string ExpType(OracleException Ex)
    {

        foreach (int ErrNo in PST_Constants.dbstatusFlags)
        {
            if (Ex.ErrorCode.Equals(ErrNo))
            {
                return "101";
            }
        }
        return "102";
    }

}