using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using log4net;
using log4net.Config;
using System.ServiceProcess;
using System.Diagnostics;


namespace ICOMSProvisioningService
{
 public class ICOMSProvisioningService : ServiceBase
   {

    private static readonly ILog logger = LogManager.GetLogger(typeof(ICOMSProvisioningService));

    public ICOMSProvisioningService()
    {
        this.ServiceName = "ICOMS Provisioning Service";
    }

     public void LogEvent(string message, EventLogEntryType type, int eventId)
     {
         try
         {
             EventLog.WriteEntry(message, type, eventId);
         }
         catch (Exception ex)
         {             
             logger.Error(string.Format("Failed to write message to event log : message {0} : error {1}", message, ex.ToString()));
         }
     }

     static void ListenForLoadBalancer(object tempObj)
     {

         logger.Info("ICOMSProvisioningService::ListenForLoadBalancer() called");

         string strIPAddress = string.Empty;
         LoadBalancerMembers templistenerMembers;
         templistenerMembers = (LoadBalancerMembers)tempObj;
         TcpListener tcpICOMLoadBalancer = new TcpListener(IPAddress.Parse(templistenerMembers.listenerAddress), Convert.ToInt32(templistenerMembers.listenerPort));
         ASCIIEncoding encoder = new ASCIIEncoding();
         // Start listening for client requests.

         try
         {
             int connectCount = 0;
	         const int CONNECTCOUNTINTERVAL = 600;  //Log connect/end connect messages every 600 seconds.
             tcpICOMLoadBalancer.Start();
             strIPAddress = templistenerMembers.listenerAddress + ":" + templistenerMembers.listenerPort;
             int bytesRead;
             logger.Info(string.Format("The server listening at...  {0}", strIPAddress));

             // Client connection loop
             while (true)
             {
                 Socket tcpICOMSocket;
                 bool isClientClosed = false;
                 try
                 {

                     tcpICOMSocket = tcpICOMLoadBalancer.AcceptSocket();

                     string clientIP = tcpICOMSocket.RemoteEndPoint.ToString();
		             //Only Log a message about connection established every so often so we don't fill the logs
		             if (0 == connectCount % CONNECTCOUNTINTERVAL)
		             {
			             logger.Info(string.Format("Connection established: {0}", clientIP));
		             }

                     byte[] message = new byte[4096];

                     //persistent communication loop after client connected
                     while (true)
                     {
                         bytesRead = 0;

                         try
                         {
                             //blocks until a client sends a message
                             bytesRead = tcpICOMSocket.Receive(message, 0, message.Length, 0);
                             if (bytesRead == 0)
                             {
                                 logger.Debug("Message length received zero closing..");
                                 //the client has disconnected from the server                                 
                                 break;
                             }
			                 else
			                 {
                                byte[] buffer = encoder.GetBytes("HTTP/1.1 200 OK\r\n");
                                tcpICOMSocket.Send(buffer);
			                 }
                         }
                         catch (Exception ex)
                         {
                             logger.Warn(string.Format("Client disconnected abruptly from {0}", clientIP));
                             tcpICOMSocket.Close();
                             isClientClosed = true;
                             logger.Error(string.Format("ListenForLoadBalancer(): Exception: {0}", ex.Message.ToString()));
                             logger.Error("ICOMSProvisioningService::ListenForLoadBalancer()");
                             break;
                         }
                     }
                     /* clean up */
                     if (!isClientClosed)
                     {
                         tcpICOMSocket.Close();
                     }
		             //Only Log a message about connection established every so often so we don't fill the logs
		             //Increment the counter after it is checked
		             if (0 == connectCount++ % CONNECTCOUNTINTERVAL)
		             {		  
		                 logger.Info(string.Format("Connection closed from {0}", clientIP));
		             }
                 }
                 catch (Exception ex)
                 {
                     logger.Error(string.Format("ListenForLoadBalancer(): Exception: {0}", ex.Message.ToString()));
                     logger.Error("ICOMSProvisioningService::ListenForLoadBalancer() failing while reading data from client");
                 }

             }

         }
         catch (Exception ex)
         {
             logger.Error(string.Format("ListenForLoadBalancer(): Exception: {0}", ex.Message.ToString()));
             logger.Error("ICOMSProvisioningService::ListenForLoadBalancer() failing while start listening the server");
         }
     }
    static void ListenForRequests(object tempObj)
    {

        logger.Info("ICOMSProvisioningService::ListenForRequests() called");

        string strIPAddress = string.Empty;            
        ServiceListenerMembers templistenerMembers;
        templistenerMembers = (ServiceListenerMembers)tempObj;
        ServiceRunTimeManager objServiceMgr;
        TcpListener tcpICOMServer = new TcpListener(IPAddress.Parse(templistenerMembers.listenerAddress), Convert.ToInt32(templistenerMembers.listenerPort));            
        // Start listening for client requests.

        try
        {
            tcpICOMServer.Start();
            strIPAddress = templistenerMembers.listenerAddress + ":" + templistenerMembers.listenerPort;
            int bytesRead;
            logger.Info(string.Format("The server listening at...  {0}", strIPAddress));            

            // Client connection loop
            while (true)
            {
                //TcpClient tcpICOMClient;
                Socket tcpICOMSocket;
                bool isClientClosed=false;
                try
                {                    
                    //tcpICOMClient = tcpICOMServer.AcceptTcpClient();
                    tcpICOMSocket = tcpICOMServer.AcceptSocket();

                    string clientIP = tcpICOMSocket.RemoteEndPoint.ToString();
                    logger.Info(string.Format("Connection established: {0}", clientIP));
                    //NetworkStream clientStream = tcpICOMClient.GetStream();
                    
                    string strRec = string.Empty;
                    string strSend = string.Empty;
                    byte[] message = new byte[4096];

                    //persistent communication loop after client connected
                    while (true)
                    {
                        bytesRead = 0;

                        try
                        {
                            //blocks until a client sends a message
                            //bytesRead = clientStream.Read(message, 0, 4096);
                            bytesRead = tcpICOMSocket.Receive(message, 0, message.Length, 0);
                            if (bytesRead == 0)
                            {
                                logger.Debug("Message length received zero closing..");
                                //the client has disconnected from the server                                 
                                break;
                            }
                            logger.Debug("++++++++++++++++ MESSAGE PROCESS START +++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                            TimeSpan startTS = System.DateTime.Now.TimeOfDay;
                            logger.Info(string.Format("Conversion of byte data to string from {0} at {1} progress", strIPAddress, startTS));

                            ASCIIEncoding encoder = new ASCIIEncoding();
                            strRec = encoder.GetString(message, 0, bytesRead);

                            logger.Info("Converted string data...");
                            logger.Info(string.Format("{0}", strRec));

                            // Calling Service Run Time Manager of ICOMS Provisioning service
                            objServiceMgr = new ServiceRunTimeManager();
                            strSend = objServiceMgr.processICOMSMessages(strRec, templistenerMembers);

                            ASCIIEncoding asen = new ASCIIEncoding();
                            byte[] buffer = encoder.GetBytes(strSend);
                            //clientStream.Write(buffer, 0, buffer.Length);
                            //clientStream.Flush();
                            tcpICOMSocket.Send(buffer);

                            TimeSpan endTS = System.DateTime.Now.TimeOfDay;
                            logger.Info(string.Format("Response Data to ICOMS...{0}, client={1}  Process Time={2}", strSend, clientIP, endTS.Subtract(startTS)));
                            logger.Debug("++++++++++++++++ MESSAGE PROCESS END +++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                        }
                        catch (Exception ex)
                        {
                            logger.Warn(string.Format("Client disconnected abruptly from {0}", clientIP));
                            //clientStream.Close();                     
                            //tcpICOMClient.Close();
                            tcpICOMSocket.Close();
                            isClientClosed = true;                         
                            logger.Error(string.Format("ListenForRequests(): Exception: {0}", ex.Message.ToString()));
                            logger.Error("ICOMSProvisioningService::ListenForRequests() failing while reading data from client");
                            break;
                        }
                    }
                    /* clean up */
                    if (!isClientClosed)
                    {
                        //clientStream.Close();
                        //tcpICOMClient.Close();
                        tcpICOMSocket.Close();
                    }
                    logger.Info(string.Format("Connection closed from {0}", clientIP));
                }
                catch (Exception ex)
                {
                    logger.Error(string.Format("ListenForRequests(): Exception: {0}", ex.Message.ToString()));
                    logger.Error("ICOMSProvisioningService::ListenForRequests() failing while reading data from client");
                }
               
            }

        }
        catch (Exception ex)
        {
            logger.Error(string.Format("ListenForRequests(): Exception: {0}", ex.Message.ToString()));
            logger.Error("ICOMSProvisioningService::ListenForRequests() failing while start listening the server");              
        }
    }

    
    static void Main()
    {

        // Comment the below line while running application as console application        
        
        // ServiceBase.Run(new ICOMSProvisioningService());
        
        // Place main code here to run application as console application   

         //  ---  Main code ------

        log4net.Config.XmlConfigurator.Configure();
        logger.Info("ICOMSProvisioningService::Main() called");

        ServiceListenerMembers[] arrGetListnerList;
        LoadBalancerMembers getlbListner;
        ServiceConfigurationManager cnfMgr = new ServiceConfigurationManager();

        // Load service Config file
        if (!cnfMgr.LoadServiceConfigFile())
        {
            return;
        }

        // Load Site details from service config file
        if (!cnfMgr.getAllListernsList())
        {
            return;
        }
        arrGetListnerList = ServiceConfigurationManager._CRMSiteDetails;

        // Load CRM details from service config file
        if (!cnfMgr.LoadCRMConnectionDetails())
        {
            return;
        }

        // Load Load Balancer details from service config file
        if (cnfMgr.LoadBalancerConnectionDetails())
        {
            getlbListner = ServiceConfigurationManager._LBConnectionDetails;
            Thread LBlistener = new Thread(new ParameterizedThreadStart(ListenForLoadBalancer));
            LBlistener.IsBackground = true;
            LBlistener.Start(getlbListner);
        }

        // Set Primary CRM when service starting
        CRM4cInterfaceAvailabilityManager.ActiveCRMURL = "P";

        // Check If Backup CRM is exist or Not
        CRM4cInterfaceAvailabilityManager.IsBackupCRMExist = CRM4cInterfaceAvailabilityManager.checkBackupCRMExistence();


        foreach (ServiceListenerMembers obj in arrGetListnerList)
        {
            Thread listener = new Thread(new ParameterizedThreadStart(ListenForRequests));
            listener.IsBackground = true;
            listener.Start(obj);
        }

        Console.WriteLine("ICOMS Provisioning Service Running from DEV. Enviornment");
        Console.ReadLine();
        //  ---  Main code ------ 

        // Place main code here to run application as console application          
    } 
     
      
    protected override void OnStart(string[] args)
    {
        base.OnStart(args);
        logger.Info("/********************* ICOMS Provisioning Service *********************/");

        try
        {

            //  ---  Main code ------

            

            //  ---  Main code ------                 

            // log service started event
            LogEvent("Service started successfully.", EventLogEntryType.Information, 1000);
        }
        catch (Exception ex)
        {
            string err = string.Format("Service failed to start : error {0}", ex.ToString());               
            logger.Error(err);
            LogEvent(err, EventLogEntryType.Error, 1001);
        }
        //TODO: place your start code here
    }

    protected override void OnStop()
    {
        base.OnStop();
           
        //TODO: clean up any variables and stop any threads
        LogEvent("Service stopped successfully.", EventLogEntryType.Information, 1002);
    }


    }
}
