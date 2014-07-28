using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.IO;
using System.Net;
using System.Collections.Specialized;
using log4net;
using System.Xml;
using System.Configuration;


/// <summary>
/// Summary description for TraceExtension
/// </summary>
/// 

public class TraceExtension : SoapExtension
{
    public TraceExtension()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(TraceExtension));
    private bool enbLogSOAPReqRes = Convert.ToBoolean(ConfigurationManager.AppSettings["enableLoggingSOAPReqAndRes"]);

    Stream oldStream;
    Stream newStream;
    string filename;

    private static XmlDocument xmlRequest;
    /// <summary>
    /// Gets the outgoing XML request sent to PayPal
    /// </summary>
    public static XmlDocument XmlRequest
    {
        get { return xmlRequest; }
    }

    private static XmlDocument xmlResponse;
    /// <summary>
    /// Gets the incoming XML response sent from PayPal
    /// </summary>
    public static XmlDocument XmlResponse
    {
        get { return xmlResponse; }
    }

    // Save the Stream representing the SOAP request or SOAP response into
    // a local memory buffer.
    public override Stream ChainStream(Stream stream)
    {
        oldStream = stream;
        newStream = new MemoryStream();
        return newStream;
    }

    // When the SOAP extension is accessed for the first time, the XML Web
    // service method it is applied to is accessed to store the file
    // name passed in, using the corresponding SoapExtensionAttribute.	
    public override object GetInitializer(LogicalMethodInfo methodInfo, SoapExtensionAttribute attribute)
    {
        return ((TraceExtensionAttribute)attribute).Filename;
    }

    // The SOAP extension was configured to run using a configuration file
    // instead of an attribute applied to a specific XML Web service
    // method.
    public override object GetInitializer(Type WebServiceType)
    {
        // Return a file name to log the trace information to, based on the
        // type.
        //return "C:\\Timesheet_Log\\Req_response\\" + WebServiceType.FullName + ".log";

        string PATH = ((NameValueCollection)System.Configuration.ConfigurationManager.GetSection("LogPath"))["PATH"];
        System.IO.Directory.CreateDirectory(PATH);
        return (PATH + WebServiceType.FullName + DateTime.Now.ToString("_dd.MM.yyyy") + ".log");

    }

    // Receive the file name stored by GetInitializer and store it in a
    // member variable for this specific instance.
    public override void Initialize(object initializer)
    {
        filename = (string)initializer;
    }

    //  If the SoapMessageStage is such that the SoapRequest or
    //  SoapResponse is still in the SOAP format to be sent or received,
    //  save it out to a file.
    public override void ProcessMessage(SoapMessage message)
    {
        switch (message.Stage)
        {
            case SoapMessageStage.BeforeSerialize:
                break;
            case SoapMessageStage.AfterSerialize:
                //WriteOutput(message);
                xmlRequest = GetSoapEnvelope(newStream);
                Copy(newStream, oldStream);
                if (enbLogSOAPReqRes)
                {
                    logger.Info(" SOAP Response ");
                    logger.Info(xmlRequest.InnerXml);
                    logger.Info(" ######################## TaxiRequest API Process (END) ######################## ");
                }
                else
                {
                    logger.Info(" ######################## TaxiRequest API Process (END) ######################## ");
                }
                break;
            case SoapMessageStage.BeforeDeserialize:
                //WriteInput(message);
                Copy(oldStream, newStream);
                xmlResponse = GetSoapEnvelope(newStream);
                if (enbLogSOAPReqRes)
                {

                    logger.Info(" ######################## TaxiRequest API Process (START) ###################### ");
                    logger.Info(" SOAP Request ");
                    logger.Info(xmlResponse.InnerXml);
                }
                else
                {
                    logger.Info(" ######################## TaxiRequest API Process (START) ###################### ");
                }
                break;
            case SoapMessageStage.AfterDeserialize:
                break;
            default:
                throw new Exception("invalid stage");
        }
    }

    public void WriteOutput(SoapMessage message)
    {
        newStream.Position = 0;
        FileStream fs = new FileStream(filename, FileMode.Append,
            FileAccess.Write);
        StreamWriter w = new StreamWriter(fs);

        string soapString = (message is SoapServerMessage) ? "SoapResponse" : "SoapRequest";
        w.WriteLine("----- " + soapString + " at " + DateTime.Now);
        w.Flush();
        Copy(newStream, fs);
        w.Close();
        newStream.Position = 0;
        Copy(newStream, oldStream);
    }

    public void WriteInput(SoapMessage message)
    {
        Copy(oldStream, newStream);
        FileStream fs = new FileStream(filename, FileMode.Append,
            FileAccess.Write);
        StreamWriter w = new StreamWriter(fs);

        string soapString = (message is SoapServerMessage) ? "SoapRequest" : "SoapResponse";
        w.WriteLine("---------------------------------------------");
        w.WriteLine("----- " + soapString + " at " + DateTime.Now);
        w.Flush();
        newStream.Position = 0;
        newStream.ToString();
        Copy(newStream, fs);
        w.Close();
        newStream.Position = 0;
    }

    private XmlDocument GetSoapEnvelope(Stream stream)
    {
        XmlDocument xml = new XmlDocument();
        stream.Position = 0;
        StreamReader reader = new StreamReader(stream);
        xml.LoadXml(reader.ReadToEnd());
        stream.Position = 0;
        return xml;
    }

    void Copy(Stream from, Stream to)
    {
        TextReader reader = new StreamReader(from);
        TextWriter writer = new StreamWriter(to);
        writer.WriteLine(reader.ReadToEnd());
        writer.Flush();
    }
}

// Create a SoapExtensionAttribute for the SOAP Extension that can be
// applied to an XML Web service method.
[AttributeUsage(AttributeTargets.Method)]
public class TraceExtensionAttribute : SoapExtensionAttribute
{

    private string filename = "D:\\log_files\\TRT\\Request_Response\\TRT_log.txt";
    private int priority;

    public override Type ExtensionType
    {
        get { return typeof(TraceExtension); }
    }

    public override int Priority
    {
        get { return priority; }
        set { priority = value; }
    }

    public string Filename
    {
        get
        {
            return filename;
        }
        set
        {
            filename = value;
        }
    }
}