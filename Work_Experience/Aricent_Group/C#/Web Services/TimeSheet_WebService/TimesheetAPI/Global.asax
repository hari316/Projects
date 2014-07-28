<%@ Application Language="C#" %>
<%@ Import Namespace="TimeSheet_BL.timesheet"%>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
        log4net.Config.XmlConfigurator.Configure();
        
        string ts_DBConnectionDetails = string.Empty;
        ts_DBConnectionDetails = System.Configuration.ConfigurationManager.ConnectionStrings["ts_ConnectionString"].ConnectionString;
        databaseLayer ts_dbl = new databaseLayer();
        ts_dbl.connectionInfo = ts_DBConnectionDetails;

        NameValueCollection errorSection = (NameValueCollection)System.Configuration.ConfigurationManager.GetSection("ErrorMessage");
        NameValueCollection emailSection = (NameValueCollection)System.Configuration.ConfigurationManager.GetSection("EmailConfig");
        timesheet_Constants.cnfgErrMessages = timesheet_Constants.LoadTSConfigCustomSection(errorSection);
        timesheet_Constants.Email_Dic = timesheet_Constants.LoadTSConfigCustomSection(emailSection);              
        
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

       
</script>
