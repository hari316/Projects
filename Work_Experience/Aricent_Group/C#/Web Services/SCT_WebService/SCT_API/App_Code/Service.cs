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
using SCT_BL.SCT;
using SCT_SI.SCT;
using utilities;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]


public class Service : System.Web.Services.WebService
{
    public Service()
    {
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
    /// This Method Authenticates the User
    /// </summary>
    /// <param name="UserId and UserPwd"></param>
    /// <returns>
    /// <paramref name="result"/> In case of Success
    /// <paramref name="error"/> In case of Error
    /// </returns>
    /// <history>
    ///     Hari haran      22/08/2012      created
    /// </history>
    /// 

    [WebMethod]
    public SCT_AuthUser authenticateUser(string UserId, string UserPwd)
    {
        logger.Info("Method : authenticateUser Start");
        logger.DebugFormat("Input parameter Id : {0} ", UserId);
        logger.DebugFormat("Input parameter Password : {0} ", UserPwd);
         
        try
        {
            SCT_AuthUser result = new SCT_AuthUser();
            SCTInterface authUsr_SI = new SCTInterface();
            result = authUsr_SI.authenticateUser_SI(UserId, UserPwd);

            logger.Info("Method : authenticateUser Stop");

            return result;
        }
        catch (Exception ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(SCT_Constants.mail_BodyFormat, System.DateTime.Now.ToString("F"), PReqNo, ex.TargetSite.ToString(), ex.ToString());
            //webServiceExHandling.Send_Email(SCT_Constants.Email_Dic, mailBody);

            SCT_AuthUser Error = new SCT_AuthUser();
            Error.StatusFlag = 1;
            Error.Message = SCT_Constants.Error;

            logger.Debug("Return object Error : ErrorCode = " + Error.StatusFlag.ToString());
            logger.Debug("Return object Error : ErrorMessage = " + Error.Message);
            logger.Error("Method : searchEmployee Stop");

            return Error;
        }
    }

    /// <summary>
    /// This Method Search results based on search criteria
    /// </summary>
    /// <param name="Name,MgrId and Flag"></param>
    /// <returns>
    /// <paramref name="result"/> In case of Success
    /// <paramref name="error"/> In case of Error
    /// </returns>
    /// <history>
    ///     Hari haran      27/07/2012      created
    /// </history>
    /// 

    [WebMethod]
    public SCT_Emp searchEmployee(string Name, string MgrId, string Flag)
    {
        logger.Info("Method : searchEmployee Start");
        logger.DebugFormat("Input parameter Name : {0} ", Name);
        logger.DebugFormat("Input parameter ManagerId : {0} ", MgrId);

        try
        {
            SCT_Emp result = new SCT_Emp();
            SCTInterface search_SI = new SCTInterface();
            result = search_SI.searchEmployee_SI(Name, MgrId, Flag);

            logger.Info("Method : searchEmployee Stop");

            return result;
        }
        catch (SqlException ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(SCT_Constants.mail_BodyFormat,System.DateTime.Now.ToString("F"),PReqNo,ex.TargetSite.ToString(),ex.ToString());
            //webServiceExHandling.Send_Email(SCT_Constants.Email_Dic, mailBody);

            SCT_Emp Error = new SCT_Emp();
            Error.SCT_headerDetails.StatusFlag = ex.Number;
            string expCode = ExpType(ex);
            Error.SCT_headerDetails.StatusMsg = SCT_Constants.cnfgErrMessages[expCode];

            logger.Debug("Return object Error : Status Flag = " + Error.SCT_headerDetails.StatusFlag.ToString());
            logger.Debug("Return object Error : Status Message = " + Error.SCT_headerDetails.StatusMsg);
            logger.Error("Method : searchEmployee Stop");

            return Error;
        }

        catch (Exception ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(SCT_Constants.mail_BodyFormat, System.DateTime.Now.ToString("F"), PReqNo, ex.TargetSite.ToString(), ex.ToString());
            //webServiceExHandling.Send_Email(SCT_Constants.Email_Dic, mailBody);

            SCT_Emp Error = new SCT_Emp();
            Error.SCT_headerDetails.StatusFlag = 1;
            Error.SCT_headerDetails.StatusMsg = SCT_Constants.Error;

            logger.Debug("Return object Error : ErrorCode = " + Error.SCT_headerDetails.StatusFlag.ToString());
            logger.Debug("Return object Error : ErrorMessage = " + Error.SCT_headerDetails.StatusMsg);
            logger.Error("Method : searchEmployee Stop");

            return Error;
        }

    }

    [WebMethod]
    public SCT_RChange getReasonForChange()
    {
        logger.Info("Method : getReasonForChange Start");

        try
        {
            SCT_RChange result = new SCT_RChange();
            SCTInterface PA_SI = new SCTInterface();
            result = PA_SI.getReasonForChange_SI();

            logger.Info("Method : getReasonForChange Stop");

            return result;
        }
        catch (SqlException ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(SCT_Constants.mail_BodyFormat,System.DateTime.Now.ToString("F"),PReqNo,ex.TargetSite.ToString(),ex.ToString());
            //webServiceExHandling.Send_Email(SCT_Constants.Email_Dic, mailBody);

            SCT_RChange Error = new SCT_RChange();
            Error.SCT_header.StatusFlag = ex.Number;
            string expCode = ExpType(ex);
            Error.SCT_header.StatusMsg = SCT_Constants.cnfgErrMessages[expCode];

            logger.Debug("Return object Error : Status Flag = " + Error.SCT_header.StatusFlag.ToString());
            logger.Debug("Return object Error : Status Message = " + Error.SCT_header.StatusMsg);
            logger.Error("Method : getReasonForChange Stop");

            return Error;
        }

        catch (Exception ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(SCT_Constants.mail_BodyFormat, System.DateTime.Now.ToString("F"), PReqNo, ex.TargetSite.ToString(), ex.ToString());
            //webServiceExHandling.Send_Email(SCT_Constants.Email_Dic, mailBody);

            SCT_RChange Error = new SCT_RChange();
            Error.SCT_header.StatusFlag = 1;
            Error.SCT_header.StatusMsg = SCT_Constants.Error;

            logger.Debug("Return object Error : ErrorCode = " + Error.SCT_header.StatusFlag.ToString());
            logger.Debug("Return object Error : ErrorMessage = " + Error.SCT_header.StatusMsg);
            logger.Error("Method : getReasonForChange Stop");

            return Error;
        }

    }


    [WebMethod]
    public SCT_PA getPersonnelArea()
    {
        logger.Info("Method : getPersonnelArea Start");

        try
        {
            SCT_PA result = new SCT_PA();
            SCTInterface PA_SI = new SCTInterface();
            result = PA_SI.getPersonnelArea_SI();

            logger.Info("Method : getPersonnelArea Stop");

            return result;
        }
        catch (SqlException ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(SCT_Constants.mail_BodyFormat,System.DateTime.Now.ToString("F"),PReqNo,ex.TargetSite.ToString(),ex.ToString());
            //webServiceExHandling.Send_Email(SCT_Constants.Email_Dic, mailBody);

            SCT_PA Error = new SCT_PA();
            Error.SCT_header.StatusFlag = ex.Number;
            string expCode = ExpType(ex);
            Error.SCT_header.StatusMsg = SCT_Constants.cnfgErrMessages[expCode];

            logger.Debug("Return object Error : Status Flag = " + Error.SCT_header.StatusFlag.ToString());
            logger.Debug("Return object Error : Status Message = " + Error.SCT_header.StatusMsg);
            logger.Error("Method : getPersonnelArea Stop");

            return Error;
        }

        catch (Exception ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(SCT_Constants.mail_BodyFormat, System.DateTime.Now.ToString("F"), PReqNo, ex.TargetSite.ToString(), ex.ToString());
            //webServiceExHandling.Send_Email(SCT_Constants.Email_Dic, mailBody);

            SCT_PA Error = new SCT_PA();
            Error.SCT_header.StatusFlag = 1;
            Error.SCT_header.StatusMsg = SCT_Constants.Error;

            logger.Debug("Return object Error : ErrorCode = " + Error.SCT_header.StatusFlag.ToString());
            logger.Debug("Return object Error : ErrorMessage = " + Error.SCT_header.StatusMsg);
            logger.Error("Method : searchEmployee Stop");

            return Error;
        }

    }


    [WebMethod]
    public SCT_PSA getPersonnelSubArea()
    {
        logger.Info("Method : getPersonnelSubArea Start");

        try
        {
            SCT_PSA result = new SCT_PSA();
            SCTInterface PSA_SI = new SCTInterface();
            result = PSA_SI.getPersonnelSubArea_SI();

            logger.Info("Method : getPersonnelSubArea Stop");

            return result;
        }
        catch (SqlException ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(SCT_Constants.mail_BodyFormat,System.DateTime.Now.ToString("F"),PReqNo,ex.TargetSite.ToString(),ex.ToString());
            //webServiceExHandling.Send_Email(SCT_Constants.Email_Dic, mailBody);

            SCT_PSA Error = new SCT_PSA();
            Error.SCT_header.StatusFlag = ex.Number;
            string expCode = ExpType(ex);
            Error.SCT_header.StatusMsg = SCT_Constants.cnfgErrMessages[expCode];

            logger.Debug("Return object Error : Status Flag = " + Error.SCT_header.StatusFlag.ToString());
            logger.Debug("Return object Error : Status Message = " + Error.SCT_header.StatusMsg);
            logger.Error("Method : getPersonnelSubArea Stop");

            return Error;
        }

        catch (Exception ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(SCT_Constants.mail_BodyFormat, System.DateTime.Now.ToString("F"), PReqNo, ex.TargetSite.ToString(), ex.ToString());
            //webServiceExHandling.Send_Email(SCT_Constants.Email_Dic, mailBody);

            SCT_PSA Error = new SCT_PSA();
            Error.SCT_header.StatusFlag = 1;
            Error.SCT_header.StatusMsg = SCT_Constants.Error;

            logger.Debug("Return object Error : ErrorCode = " + Error.SCT_header.StatusFlag.ToString());
            logger.Debug("Return object Error : ErrorMessage = " + Error.SCT_header.StatusMsg);
            logger.Error("Method : getPersonnelSubArea Stop");

            return Error;
        }

    }

    [WebMethod]
    public SCT_ORGUnit getOrganizationalUnit()
    {
        logger.Info("Method : getOrganizationalUnit Start");

        try
        {
            SCT_ORGUnit result = new SCT_ORGUnit();
            SCTInterface ORG_SI = new SCTInterface();
            result = ORG_SI.getOrganizationalUnit_SI();

            logger.Info("Method : getOrganizationalUnit Stop");

            return result;
        }
        catch (SqlException ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(SCT_Constants.mail_BodyFormat,System.DateTime.Now.ToString("F"),PReqNo,ex.TargetSite.ToString(),ex.ToString());
            //webServiceExHandling.Send_Email(SCT_Constants.Email_Dic, mailBody);

            SCT_ORGUnit Error = new SCT_ORGUnit();
            Error.SCT_header.StatusFlag = ex.Number;
            string expCode = ExpType(ex);
            Error.SCT_header.StatusMsg = SCT_Constants.cnfgErrMessages[expCode];

            logger.Debug("Return object Error : Status Flag = " + Error.SCT_header.StatusFlag.ToString());
            logger.Debug("Return object Error : Status Message = " + Error.SCT_header.StatusMsg);
            logger.Error("Method : getOrganizationalUnit Stop");

            return Error;
        }

        catch (Exception ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(SCT_Constants.mail_BodyFormat, System.DateTime.Now.ToString("F"), PReqNo, ex.TargetSite.ToString(), ex.ToString());
            //webServiceExHandling.Send_Email(SCT_Constants.Email_Dic, mailBody);

            SCT_ORGUnit Error = new SCT_ORGUnit();
            Error.SCT_header.StatusFlag = 1;
            Error.SCT_header.StatusMsg = SCT_Constants.Error;

            logger.Debug("Return object Error : ErrorCode = " + Error.SCT_header.StatusFlag.ToString());
            logger.Debug("Return object Error : ErrorMessage = " + Error.SCT_header.StatusMsg);
            logger.Error("Method : searchEmployee Stop");

            return Error;
        }

    }

    [WebMethod]
    public SCT_CC getCostCenter()
    {
        logger.Info("Method : getCostCenter Start");

        try
        {
            SCT_CC result = new SCT_CC();
            SCTInterface CC_SI = new SCTInterface();
            result = CC_SI.getCostCenter_SI();

            logger.Info("Method : getCostCenter Stop");

            return result;
        }
        catch (SqlException ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(SCT_Constants.mail_BodyFormat,System.DateTime.Now.ToString("F"),PReqNo,ex.TargetSite.ToString(),ex.ToString());
            //webServiceExHandling.Send_Email(SCT_Constants.Email_Dic, mailBody);

            SCT_CC Error = new SCT_CC();
            Error.SCT_header.StatusFlag = ex.Number;
            string expCode = ExpType(ex);
            Error.SCT_header.StatusMsg = SCT_Constants.cnfgErrMessages[expCode];

            logger.Debug("Return object Error : Status Flag = " + Error.SCT_header.StatusFlag.ToString());
            logger.Debug("Return object Error : Status Message = " + Error.SCT_header.StatusMsg);
            logger.Error("Method : getCostCenter Stop");

            return Error;
        }

        catch (Exception ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(SCT_Constants.mail_BodyFormat, System.DateTime.Now.ToString("F"), PReqNo, ex.TargetSite.ToString(), ex.ToString());
            //webServiceExHandling.Send_Email(SCT_Constants.Email_Dic, mailBody);

            SCT_CC Error = new SCT_CC();
            Error.SCT_header.StatusFlag = 1;
            Error.SCT_header.StatusMsg = SCT_Constants.Error;

            logger.Debug("Return object Error : ErrorCode = " + Error.SCT_header.StatusFlag.ToString());
            logger.Debug("Return object Error : ErrorMessage = " + Error.SCT_header.StatusMsg);
            logger.Error("Method : getCostCenter Stop");

            return Error;
        }

    }

    [WebMethod]
    public SCT_BU getBusinessUnit()
    {
        logger.Info("Method : getBusinessUnit Start");

        try
        {
            SCT_BU result = new SCT_BU();
            SCTInterface BU_SI = new SCTInterface();
            result = BU_SI.getBusinessUnit_SI();

            logger.Info("Method : getBusinessUnit Stop");

            return result;
        }
        catch (SqlException ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(SCT_Constants.mail_BodyFormat,System.DateTime.Now.ToString("F"),PReqNo,ex.TargetSite.ToString(),ex.ToString());
            //webServiceExHandling.Send_Email(SCT_Constants.Email_Dic, mailBody);

            SCT_BU Error = new SCT_BU();
            Error.SCT_header.StatusFlag = ex.Number;
            string expCode = ExpType(ex);
            Error.SCT_header.StatusMsg = SCT_Constants.cnfgErrMessages[expCode];

            logger.Debug("Return object Error : Status Flag = " + Error.SCT_header.StatusFlag.ToString());
            logger.Debug("Return object Error : Status Message = " + Error.SCT_header.StatusMsg);
            logger.Error("Method : getPersonnelArea Stop");

            return Error;
        }

        catch (Exception ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(SCT_Constants.mail_BodyFormat, System.DateTime.Now.ToString("F"), PReqNo, ex.TargetSite.ToString(), ex.ToString());
            //webServiceExHandling.Send_Email(SCT_Constants.Email_Dic, mailBody);

            SCT_BU Error = new SCT_BU();
            Error.SCT_header.StatusFlag = 1;
            Error.SCT_header.StatusMsg = SCT_Constants.Error;

            logger.Debug("Return object Error : ErrorCode = " + Error.SCT_header.StatusFlag.ToString());
            logger.Debug("Return object Error : ErrorMessage = " + Error.SCT_header.StatusMsg);
            logger.Error("Method : searchEmployee Stop");

            return Error;
        }

    }

    [WebMethod]
    public SCT_SBU getStrategicBusinessUnit()
    {
        logger.Info("Method : getStrategicBusinessUnit Start");

        try
        {
            SCT_SBU result = new SCT_SBU();
            SCTInterface SBU_SI = new SCTInterface();
            result = SBU_SI.getStrategicBusinessUnit_SI();

            logger.Info("Method : getStrategicBusinessUnit Stop");

            return result;
        }
        catch (SqlException ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(SCT_Constants.mail_BodyFormat,System.DateTime.Now.ToString("F"),PReqNo,ex.TargetSite.ToString(),ex.ToString());
            //webServiceExHandling.Send_Email(SCT_Constants.Email_Dic, mailBody);

            SCT_SBU Error = new SCT_SBU();
            Error.SCT_header.StatusFlag = ex.Number;
            string expCode = ExpType(ex);
            Error.SCT_header.StatusMsg = SCT_Constants.cnfgErrMessages[expCode];

            logger.Debug("Return object Error : Status Flag = " + Error.SCT_header.StatusFlag.ToString());
            logger.Debug("Return object Error : Status Message = " + Error.SCT_header.StatusMsg);
            logger.Error("Method : getStrategicBusinessUnit Stop");

            return Error;
        }

        catch (Exception ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(SCT_Constants.mail_BodyFormat, System.DateTime.Now.ToString("F"), PReqNo, ex.TargetSite.ToString(), ex.ToString());
            //webServiceExHandling.Send_Email(SCT_Constants.Email_Dic, mailBody);

            SCT_SBU Error = new SCT_SBU();
            Error.SCT_header.StatusFlag = 1;
            Error.SCT_header.StatusMsg = SCT_Constants.Error;

            logger.Debug("Return object Error : ErrorCode = " + Error.SCT_header.StatusFlag.ToString());
            logger.Debug("Return object Error : ErrorMessage = " + Error.SCT_header.StatusMsg);
            logger.Error("Method : getStrategicBusinessUnit Stop");

            return Error;
        }

    }
    [WebMethod]
    public SCT_Horizontal getHorizontal()
    {
        logger.Info("Method : getHorizontal Start");

        try
        {
            SCT_Horizontal result = new SCT_Horizontal();
            SCTInterface get_SI = new SCTInterface();
            result = get_SI.getHorizontal_SI();

            logger.Info("Method : getHorizontal Stop");

            return result;
        }
        catch (SqlException ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(SCT_Constants.mail_BodyFormat,System.DateTime.Now.ToString("F"),PReqNo,ex.TargetSite.ToString(),ex.ToString());
            //webServiceExHandling.Send_Email(SCT_Constants.Email_Dic, mailBody);

            SCT_Horizontal Error = new SCT_Horizontal();
            Error.SCT_header.StatusFlag = ex.Number;
            string expCode = ExpType(ex);
            Error.SCT_header.StatusMsg = SCT_Constants.cnfgErrMessages[expCode];

            logger.Debug("Return object Error : Status Flag = " + Error.SCT_header.StatusFlag.ToString());
            logger.Debug("Return object Error : Status Message = " + Error.SCT_header.StatusMsg);
            logger.Error("Method : getHorizontal Stop");

            return Error;
        }

        catch (Exception ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(SCT_Constants.mail_BodyFormat, System.DateTime.Now.ToString("F"), PReqNo, ex.TargetSite.ToString(), ex.ToString());
            //webServiceExHandling.Send_Email(SCT_Constants.Email_Dic, mailBody);

            SCT_Horizontal Error = new SCT_Horizontal();
            Error.SCT_header.StatusFlag = 1;
            Error.SCT_header.StatusMsg = SCT_Constants.Error;

            logger.Debug("Return object Error : ErrorCode = " + Error.SCT_header.StatusFlag.ToString());
            logger.Debug("Return object Error : ErrorMessage = " + Error.SCT_header.StatusMsg);
            logger.Error("Method : getHorizontal Stop");

            return Error;
        }

    }

    [WebMethod]
    public SCT_BLoc getBaseLocation()
    {
        logger.Info("Method : getBaseLocation Start");

        try
        {
            SCT_BLoc result = new SCT_BLoc();
            SCTInterface BLoc_SI = new SCTInterface();
            result = BLoc_SI.getBaseLocation_SI();

            logger.Info("Method : getBaseLocation Stop");

            return result;
        }
        catch (SqlException ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(SCT_Constants.mail_BodyFormat,System.DateTime.Now.ToString("F"),PReqNo,ex.TargetSite.ToString(),ex.ToString());
            //webServiceExHandling.Send_Email(SCT_Constants.Email_Dic, mailBody);

            SCT_BLoc Error = new SCT_BLoc();
            Error.SCT_header.StatusFlag = ex.Number;
            string expCode = ExpType(ex);
            Error.SCT_header.StatusMsg = SCT_Constants.cnfgErrMessages[expCode];

            logger.Debug("Return object Error : Status Flag = " + Error.SCT_header.StatusFlag.ToString());
            logger.Debug("Return object Error : Status Message = " + Error.SCT_header.StatusMsg);
            logger.Error("Method : getBaseLocation Stop");

            return Error;
        }

        catch (Exception ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(SCT_Constants.mail_BodyFormat, System.DateTime.Now.ToString("F"), PReqNo, ex.TargetSite.ToString(), ex.ToString());
            //webServiceExHandling.Send_Email(SCT_Constants.Email_Dic, mailBody);

            SCT_BLoc Error = new SCT_BLoc();
            Error.SCT_header.StatusFlag = 1;
            Error.SCT_header.StatusMsg = SCT_Constants.Error;

            logger.Debug("Return object Error : ErrorCode = " + Error.SCT_header.StatusFlag.ToString());
            logger.Debug("Return object Error : ErrorMessage = " + Error.SCT_header.StatusMsg);
            logger.Error("Method : getBaseLocation Stop");

            return Error;
        }

    }

    [WebMethod]
    public SCT_CDeploy getCenterOfDeployment()
    {
        logger.Info("Method : getCenterOfDeployment Start");

        try
        {
            SCT_CDeploy result = new SCT_CDeploy();
            SCTInterface CDevp_SI = new SCTInterface();
            result = CDevp_SI.getCenterOfDeployment_SI();

            logger.Info("Method : getCenterOfDeployment Stop");

            return result;
        }
        catch (SqlException ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(SCT_Constants.mail_BodyFormat,System.DateTime.Now.ToString("F"),PReqNo,ex.TargetSite.ToString(),ex.ToString());
            //webServiceExHandling.Send_Email(SCT_Constants.Email_Dic, mailBody);

            SCT_CDeploy Error = new SCT_CDeploy();
            Error.SCT_header.StatusFlag = ex.Number;
            string expCode = ExpType(ex);
            Error.SCT_header.StatusMsg = SCT_Constants.cnfgErrMessages[expCode];

            logger.Debug("Return object Error : Status Flag = " + Error.SCT_header.StatusFlag.ToString());
            logger.Debug("Return object Error : Status Message = " + Error.SCT_header.StatusMsg);
            logger.Error("Method : getCenterOfDeployment Stop");

            return Error;
        }

        catch (Exception ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(SCT_Constants.mail_BodyFormat, System.DateTime.Now.ToString("F"), PReqNo, ex.TargetSite.ToString(), ex.ToString());
            //webServiceExHandling.Send_Email(SCT_Constants.Email_Dic, mailBody);

            SCT_CDeploy Error = new SCT_CDeploy();
            Error.SCT_header.StatusFlag = 1;
            Error.SCT_header.StatusMsg = SCT_Constants.Error;

            logger.Debug("Return object Error : ErrorCode = " + Error.SCT_header.StatusFlag.ToString());
            logger.Debug("Return object Error : ErrorMessage = " + Error.SCT_header.StatusMsg);
            logger.Error("Method : getCenterOfDeployment Stop");

            return Error;
        }

    }

    /// <summary>
    /// This Method fetches employee Details
    /// </summary>
    /// <param name="EmpId and MgrId"></param>
    /// <returns>
    /// <paramref name="result"/> In case of Success
    /// <paramref name="error"/> In case of Error
    /// </returns>
    /// <history>
    ///     Hari haran      27/07/2012      created
    /// </history>
    /// 

    [WebMethod]
    public SCT_Entity getSCEmployeeDetails(string EmpId, string MgrId)
    {
        logger.Info("Method : getSCEmployeeDetails Start");
        logger.DebugFormat("Input parameter EmployeeId : {0} ", EmpId);
        logger.DebugFormat("Input parameter ManagerId : {0} ", MgrId);

        try
        {
            SCT_Entity result = new SCT_Entity();
            SCTInterface search_SI = new SCTInterface();
            result = search_SI.getSCEmployeeDetails_SI(EmpId, MgrId);

            logger.Info("Method : getSCEmployeeDetails Stop");

            return result;
        }
        catch (SqlException ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(SCT_Constants.mail_BodyFormat,System.DateTime.Now.ToString("F"),PReqNo,ex.TargetSite.ToString(),ex.ToString());
            //webServiceExHandling.Send_Email(SCT_Constants.Email_Dic, mailBody);

            SCT_Entity Error = new SCT_Entity();
            Error.StatusFlag = ex.Number;
            string expCode = ExpType(ex);
            Error.StatusMsg = SCT_Constants.cnfgErrMessages[expCode];

            logger.Debug("Return object Error : Status Flag = " + Error.StatusFlag.ToString());
            logger.Debug("Return object Error : Status Message = " + Error.StatusMsg);
            logger.Error("Method : getSCEmployeeDetails Stop");

            return Error;
        }

        catch (Exception ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(SCT_Constants.mail_BodyFormat, System.DateTime.Now.ToString("F"), PReqNo, ex.TargetSite.ToString(), ex.ToString());
            //webServiceExHandling.Send_Email(SCT_Constants.Email_Dic, mailBody);

            SCT_Entity Error = new SCT_Entity();
            Error.StatusFlag = 1;
            Error.StatusMsg = SCT_Constants.Error;

            logger.Debug("Return object Error : ErrorCode = " + Error.StatusFlag.ToString());
            logger.Debug("Return object Error : ErrorMessage = " + Error.StatusMsg);
            logger.Error("Method : getSCEmployeeDetails Stop");

            return Error;
        }

    }

    /// <summary>
    /// This Method Updates the SCT Details
    /// </summary>
    /// <param name="SCT_Entry"></param>
    /// <returns>
    /// <paramref name="result"/> In case of Success
    /// <paramref name="error"/> In case of Error
    /// </returns>
    /// <history>
    ///     Hari haran      27/07/2012      created
    /// </history>
    /// 

    [WebMethod]
    public SCT_UpdateOutputEntity updateSCEmployeeDetails([XmlElement("SCT_Input")] SCT_UpdateInputEntity SCT_Entry)
    {
        logger.Info("Method : updateSCEmployeeDetails Start");
        logger.Debug("EmployeeID value : " + SCT_Entry.EmpNo.ToString());

        SCT_UpdateOutputEntity result = new SCT_UpdateOutputEntity();
        try
        {
            SCTInterface updateSCT_IS = new SCTInterface();
            result = updateSCT_IS.updateSCEmployeeDetails_SI(SCT_Entry);

            logger.Info("Method : updateSCEmployeeDetails Stop");

            return result;
        }
        catch (SqlException ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            // string mailBody = string.Format(SCT_Constants.mail_BodyFormat, System.DateTime.Now.ToString("F"), IS_Entry[0].PReqNo, ex.TargetSite.ToString(), ex.ToString());
            // webServiceExHandling.Send_Email(SCT_Constants.Email_Dic, mailBody);

            result.StatusFlag = 1;
            string expCode = ExpType(ex);
            result.Message = SCT_Constants.cnfgErrMessages[expCode];

            logger.Debug("Return object Error : ErrorCode = " + result.StatusFlag.ToString());
            logger.Debug("Return object Error : ErrorMessage = " + result.Message);
            logger.Error("Method : updateSCEmployeeDetails Stop");

            return result;

        }
        catch (Exception ex)
        {
            webServiceExHandling.ExceptionLog(ex);
            //string mailBody = string.Format(SCT_Constants.mail_BodyFormat, System.DateTime.Now.ToString("F"), getPreqNo(IS_Entry), ex.TargetSite.ToString(), ex.ToString());
            //webServiceExHandling.Send_Email(SCT_Constants.Email_Dic, mailBody);

            result.StatusFlag = 1;
            result.Message = SCT_Constants.Error;

            logger.Debug("Return object Error : ErrorCode = " + result.StatusFlag.ToString());
            logger.Debug("Return object Error : ErrorMessage = " + result.Message);
            logger.Error("Method : updateSCEmployeeDetails Stop");

            return result;
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

        foreach (int ErrNo in SCT_Constants.dbErrorCodes)
        {
            if (Ex.Number.Equals(ErrNo))
            {
                return "101";
            }
        }
        return "102";
    }

}