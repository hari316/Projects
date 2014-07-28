using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;
using utilities;
using System.Net.Mail;
using System.Collections.Specialized;
using SCT_BL.SCT;
using log4net;


/// <summary>
/// Summary description for ThrowSoapException
/// </summary>
public class ThrowSoapException : WebService
{
    public ThrowSoapException()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(ThrowSoapException));

    public void convertToSoapException(Exception ex)
    {

        string errCode = string.Empty;
        string errDesc = string.Empty;
        string errSource = string.Empty;

        if (ex is myCustomException)
        {
            myCustomException custEx;
            custEx = (myCustomException)ex;
            errCode = custEx.ErrorCode.ToString();
            errDesc = custEx.Message;
            errSource = errorCodeValue(custEx.ErrorCode);
        }
        else
        {
            errCode = "UnKnown Code Error";
            errDesc = ex.Message.ToString();
            errSource = string.Format("Error type {0} with error message {1} occured", ex.GetType().Name, ex.Message);
        }

        //EmailException(ex);
        createSoapException(errCode, errSource, errDesc);
    }

    private void createSoapException(string errCode, string errSource, string errDesc)
    {
        // Build the detail element of the SOAP fault.
        System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
        System.Xml.XmlNode node = doc.CreateNode(XmlNodeType.Element,
             SoapException.DetailElementName.Name, SoapException.DetailElementName.Namespace);

        // Build specific details for the SoapException.
        // Add first child of detail XML element.
        System.Xml.XmlNode messageNode = doc.CreateNode(XmlNodeType.Element, "ErrorCode", "http://tempuri.org/");
        System.Xml.XmlNode messageTextNode = doc.CreateNode(XmlNodeType.Text, "ErrorCodeText", "http://tempuri.org/");
        messageTextNode.Value = errCode;
        messageNode.AppendChild(messageTextNode);

        // Add second child of detail XML element with an attribute.

        System.Xml.XmlNode sourceNode = doc.CreateNode(XmlNodeType.Element, "ErrorSource", "http://tempuri.org/");
        System.Xml.XmlNode sourceTextNode = doc.CreateNode(XmlNodeType.Text, "ErrorSourceText", "http://tempuri.org/");
        sourceTextNode.Value = errSource;
        sourceNode.AppendChild(sourceTextNode);

        // Add third child of detail XML element with an attribute.

        System.Xml.XmlNode descrNode = doc.CreateNode(XmlNodeType.Element, "ErrorDescription", "http://tempuri.org/");
        System.Xml.XmlNode descrTextNode = doc.CreateNode(XmlNodeType.Text, "ErrorDecriptionText", "http://tempuri.org/");
        descrTextNode.Value = errDesc;
        descrNode.AppendChild(descrTextNode);


        // Append the three child elements to the node.
        node.AppendChild(messageNode);
        node.AppendChild(sourceNode);
        node.AppendChild(descrNode);

        //Throw the exception    
        SoapException se = new SoapException("Fault occurred", SoapException.ClientFaultCode, Context.Request.Url.AbsoluteUri, node);
        throw se;
    }


    private string errorCodeValue(int errCode)
    {

        Dictionary<int, string> Error = new Dictionary<int, string>();

        Error.Add(11, " Error : Method Name - serviceInterface : getPurchaseRequestDetails ");
        Error.Add(12, " Error : Method Name - serviceInterface : updatePurchaseRequest ");

        Error.Add(21, " Error : Method Name - BAL : getPurchaseRequestDetails ");
        Error.Add(22, " Error : Method Name - BAL : updatePurchaseRequest ");

        Error.Add(31, " Error : Method Name - DAL : getPurchaseRequestDetails ");
        Error.Add(32, " Error : Method Name - DAL : updatePurchaseRequest ");

        return Error[errCode];


    }

}