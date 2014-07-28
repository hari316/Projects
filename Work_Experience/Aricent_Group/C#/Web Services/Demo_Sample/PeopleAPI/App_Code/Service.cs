using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;
using person.SI;
using person.BL;
using logDB;
using System.IO;
using System.Net;




[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]


public class Service : System.Web.Services.WebService
{
    public int authFlag = 0;
    
    private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(Service));

    public AuthenticationSoapHeader AuthHeader; 

    public Service () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        logger.Info("Control Flow : hello world method called");
        return "Hello World";
    }
   
     
    [WebMethod, SoapHeader("AuthHeader")]
    public personEntity[] viewDetail()
    {
        try
        {
            logger.Info("Control Flow : viewDetail web Method Started");
            string access = AuthHeader.authenticate(); 
            personEntity[] result = null;
            if (AuthHeader.authFlag == 1)
            {
                logger.Info("Authenticated successfully : " + access);
                serviceInterface viewDetailSI = new serviceInterface();
                result = viewDetailSI.viewDetail();
            }
            else
            {           
                logger.Fatal("Authentication failed : " + access);
            }
            logger.Info("Control Flow : viewDetail web Method Stopped");
            return result;
        }
        catch (Exception ex)
        {
            logger.Error("Control Flow : viewDetail web Method Stopped");
            ThrowSoapException soapExp = new ThrowSoapException();
            soapExp.convertToSoapException(ex);
            return null;
        }
        
    
    }

 
    [WebMethod, SoapHeader("AuthHeader")]
    public string insertDetail( personEntity p)
    {
        try
        {
            logger.Info("Control Flow : insertDetail web Method started");
            string access = AuthHeader.authenticate();
            string result = string.Empty;
            if (AuthHeader.authFlag == 1)
            {
                logger.Info("Authenticated successfully : " + access);
                serviceInterface insertDetailSI = new serviceInterface();
                result = insertDetailSI.insertDetail(p);
            }
            else
            {
                result = access;
                logger.Fatal("Authentication failed : " + access);
            }
            logger.Info("Control Flow : insertDetail web Method stopped");
            return result;
        }
        catch (Exception ex)
        {
            logger.Error("Control Flow : insertDetail web Method stopped");
            ThrowSoapException soapExp = new ThrowSoapException();
            soapExp.convertToSoapException(ex);
            return string.Empty;
        }
       

    }

    [WebMethod, SoapHeader("AuthHeader")]
    public string updateDetail(personEntity p)
    {
       try
       {
            logger.Info("Control Flow : updateDetail web Method Started");
            string access = AuthHeader.authenticate();
            string result = string.Empty;
            if (AuthHeader.authFlag == 1)
            {	        
		        logger.Info("Authenticated successfully : " + access);
                serviceInterface updateSI = new serviceInterface();
                result = updateSI.updateDetail(p);
            }
            else
            {
                result = access;
                logger.Info("Authenticated Failed : " + access);
            }
            logger.Info("Control Flow : updateDetail web Method Stopped");
            return result;
        }
	    catch (Exception ex)
	    {
            logger.Error("Control Flow : updateDetail web Method Stopped");
		    ThrowSoapException soapExp = new ThrowSoapException();
            soapExp.convertToSoapException(ex);
            return string.Empty;
	    }                
       
    }



    [WebMethod, SoapHeader("AuthHeader")]
    public string deleteDetail(int id)
    {    
        try
        {
            logger.Info("Control Flow : deleteDetail web Method Started");
            string access = AuthHeader.authenticate();
            string result = string.Empty;
            
            if (AuthHeader.authFlag == 1)
            {
                logger.Info("Authenticated successfully : " + access);
                serviceInterface deleteSI = new serviceInterface();
                result = deleteSI.delete(id);
            }

            else
            {
                result = access;
                logger.Info("Authenticated Failed : " + access);
            }
            logger.Info("Control Flow : deleteDetail web Method Stopped");
            return result;

        }
        catch (Exception ex)
        {
            logger.Error("Control Flow : updateDetail web Method Stopped");
            ThrowSoapException soapExp = new ThrowSoapException();
            soapExp.convertToSoapException(ex);
            return string.Empty;
        }    
    }

}
