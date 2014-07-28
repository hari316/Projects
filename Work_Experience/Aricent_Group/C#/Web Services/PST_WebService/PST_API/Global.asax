<%@ Application Language="C#" %>
<%@ Import Namespace="PST_BL"%>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
        // Code that runs on application startup
        log4net.Config.XmlConfigurator.Configure();

        string PST_DBConnectionDetails = string.Empty;
        PST_DBConnectionDetails = System.Configuration.ConfigurationManager.ConnectionStrings["PST_ConnectionString"].ConnectionString;
        databaseLayer PST_dbl = new databaseLayer();
        PST_dbl.connectionInfo = PST_DBConnectionDetails;
        PST_dbl.myConnectionInfo = System.Configuration.ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        
        NameValueCollection errorSection = (NameValueCollection)System.Configuration.ConfigurationManager.GetSection("statusMsg");        
        NameValueCollection emailSection = (NameValueCollection)System.Configuration.ConfigurationManager.GetSection("EmailConfig");
        PST_Constants.cnfgErrMessages = PST_Constants.LoadISConfigCustomSection(errorSection);
        PST_Constants.Email_Dic = PST_Constants.LoadISConfigCustomSection(emailSection);
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
