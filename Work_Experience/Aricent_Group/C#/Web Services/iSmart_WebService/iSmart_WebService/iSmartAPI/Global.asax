<%@ Application Language="C#" %>
<%@ Import Namespace="iSmart_BL.iSmart"%>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
        // Code that runs on application startup
        log4net.Config.XmlConfigurator.Configure();

        string is_DBConnectionDetails = string.Empty;
        is_DBConnectionDetails = System.Configuration.ConfigurationManager.ConnectionStrings["is_ConnectionString"].ConnectionString;
        databaseLayer is_dbl = new databaseLayer();
        is_dbl.connectionInfo = is_DBConnectionDetails;
        
        NameValueCollection errorSection = (NameValueCollection)System.Configuration.ConfigurationManager.GetSection("ErrorMessage");        
        NameValueCollection emailSection = (NameValueCollection)System.Configuration.ConfigurationManager.GetSection("EmailConfig");
        iSmart_Constants.cnfgErrMessages = iSmart_Constants.LoadISConfigCustomSection(errorSection);
        iSmart_Constants.Email_Dic = iSmart_Constants.LoadISConfigCustomSection(emailSection);
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
