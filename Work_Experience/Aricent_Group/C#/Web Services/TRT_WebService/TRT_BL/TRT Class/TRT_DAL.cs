using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using utilities;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Net;
using System.Data.SqlClient;
using TRT_BL.TaxiEmpDetails;
using TRT_BL.TaxiSharepoint;

namespace TRT_BL
{
    public class TRT_DAL
    {
        public TRT_DAL()
        {
            // TODO: Add constructor logic here 
        }

        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(TRT_DAL));

        TRTService TaxiWebService = new TRTService();
        TRTLists TaxiSPService = new TRTLists();

        queryLog QueryLog = new queryLog();

        /// <summary>
        /// This function fetches the Location Details from the Sharepoint web service
        /// </summary>
        /// <param name="SearchCriteria_DAL"></param>
        /// <returns>
        /// <paramref name="TRT_ChargeCodes"/>
        /// </returns>
        /// <history>
        ///     Hari haran      07/05/2012      created
        /// </history>
        /// 
        public TRT_ChargeCodes getAllChargeCodes_DAL(string SearchCriteria_DAL)
        {
            logger.Debug("TaxiRequest_DAL: getAllChargeCodes_DAL() called");

            string chargeCodeXmlString = TaxiWebService.GetAllChargeCodes(SearchCriteria_DAL);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(chargeCodeXmlString);

            XmlNodeList ProjectNodes = xmlDoc.SelectNodes("/NewDataSet/Projects");
            int objectCount = ProjectNodes.Count;

            logger.InfoFormat("No of Records(Charge Codes) returned : {0}", objectCount);

            if (objectCount > 0)
            {
                TRT_ChargeCodes Result = new TRT_ChargeCodes(objectCount);
                Result.TRT_header.statusFlag = 0;
                Result.TRT_header.statusMsg = TRT_Constants.Success;
                int objCounter = 0;
                foreach (XmlElement elem in ProjectNodes)
                {
                    TRT_ChargeCodes.ChargeCode ccId = new TRT_ChargeCodes.ChargeCode();
                    ccId.ID = elem.SelectSingleNode("ProjectId").InnerText;
                    Result.TRT_child[objCounter++] = ccId;
                }

                logger.Debug("TaxiRequest_DAL: getAllChargeCodes_DAL() Stop");

                return Result;
            }
            else
            {
                TRT_ChargeCodes Error = new TRT_ChargeCodes();
                Error.TRT_header.statusFlag = 1;
                Error.TRT_header.statusMsg = "No Charge Code Found";

                logger.Debug("TaxiRequest_DAL: getAllChargeCodes_DAL() Stop");

                return Error;
            }
        }

        /// <summary>
        /// This function fetches the Location Details from the Sharepoint web service
        /// </summary>
        /// <param name="PST_InputEntity"></param>
        /// <returns>
        /// <paramref name="PST_OutputEntity"/>
        /// </returns>
        /// <history>
        ///     Hari haran      07/05/2012      created
        /// </history>
        /// 

        public TRT_LocEntity getLocationsDetails_DAL(string LocId_BAL)
        {
            logger.Debug("TaxiRequest_DAL: getLocationsDetails_DAL() called");

            try
            {
                logger.DebugFormat("Input parameter Location ID : {0} ", LocId_BAL);

                int plotCount = 0;
                int PurposeCount = 0;

                TaxiSPService.Credentials = new NetworkCredential("bgh29309", "Password1", "asian");
                XmlDocument xmlDoc = new XmlDocument();
                TaxiSPService.Url = "http://awstage.intra.aricent.com/sites/applications/TaxiRequest/_vti_bin/Lists.asmx";


                XmlNode ndLists = TaxiSPService.GetList("Locations");


                string listName = "{CB9EE6A3-C0EC-43ED-948F-1F8B78669937}";
                //string viewName = "{7137FFF8-48FF-4C69-8C76-0E3BBD1EA7F9}";
                string rowLimit = "150";

                /*Use the CreateElement method of the document object to create 
                elements for the parameters that use XML.*/
                System.Xml.XmlElement query = xmlDoc.CreateElement("Query");
                System.Xml.XmlElement viewFields =
                    xmlDoc.CreateElement("ViewFields");
                System.Xml.XmlElement queryOptions =
                    xmlDoc.CreateElement("QueryOptions");

                /*To specify values for the parameter elements (optional), assign 
                CAML fragments to the InnerXml property of each element.*/
                query.InnerXml = "<Where><Eq><FieldRef Name=\"LocationCode\"/><Value Type=\"Text\">" + LocId_BAL.ToUpper() + "</Value></Eq></Where>";
                viewFields.InnerXml = "<FieldRef Name=\"PlotCode\" />";
                queryOptions.InnerXml = "";

                XmlNode nodePlotList = TaxiSPService.GetListItems(listName, null, query, viewFields, rowLimit, null, null);

                listName = "{AD9A333C-B271-46D3-A524-5825B7D1AFDC}";
                query.InnerXml = "<Where><Eq><FieldRef Name=\"LocationCode\"/><Value Type=\"Text\">" + LocId_BAL.ToUpper() + "</Value></Eq></Where>";
                viewFields.InnerXml = "<FieldRef Name=\"PurposeCode\" />";

                XmlNode nodePurposeList = TaxiSPService.GetListItems(listName, null, query, viewFields, rowLimit, null, null);

                plotCount = Convert.ToInt32(nodePlotList.ChildNodes[1].Attributes["ItemCount"].Value);
                PurposeCount = Convert.ToInt32(nodePurposeList.ChildNodes[1].Attributes["ItemCount"].Value);

                logger.InfoFormat("No of Records returned for Plot Details : {0}", plotCount);
                logger.InfoFormat("No of Records returned for Purpose Details : {0}", PurposeCount);

                if (plotCount > 0 && PurposeCount > 0)
                {
                    TRT_LocEntity Result = new TRT_LocEntity(plotCount, PurposeCount);
                    Result.TRT_header.statusFlag = 0;
                    Result.TRT_header.statusMsg = TRT_Constants.Success;
                    int PlotObjCounter = 0;
                    int PurposeObjCounter = 0;

                    xmlDoc.LoadXml(nodePlotList.OuterXml);
                    XmlNamespaceManager nsMgr = new XmlNamespaceManager(xmlDoc.NameTable);
                    nsMgr.AddNamespace("sp", "http://schemas.microsoft.com/sharepoint/soap/");
                    nsMgr.AddNamespace("rs", "urn:schemas-microsoft-com:rowset");
                    nsMgr.AddNamespace("z", "#RowsetSchema");

                    XmlNodeList plotList = xmlDoc.SelectNodes(".//z:row", nsMgr);

                    foreach (XmlElement elemPlot in plotList)
                    {

                        TRT_LocEntity.Plot plot = new TRT_LocEntity.Plot();
                        plot.ID = elemPlot.Attributes["ows_PlotCode"].Value;
                        plot.Label = elemPlot.Attributes["ows_PlotDesc"].Value;

                        Result.TRT_child_plot[PlotObjCounter++] = plot;

                    }

                    xmlDoc.LoadXml(nodePurposeList.OuterXml);
                    XmlNodeList purposeList = xmlDoc.SelectNodes(".//z:row", nsMgr);

                    foreach (XmlElement elemPurpose in purposeList)
                    {

                        TRT_LocEntity.Purpose purpose = new TRT_LocEntity.Purpose();
                        purpose.ID = elemPurpose.Attributes["ows_PurposeCode"].Value;
                        purpose.Label = elemPurpose.Attributes["ows_PurposeDescription"].Value;

                        Result.TRT_child_purpose[PurposeObjCounter++] = purpose;

                    }

                    logger.Info("In Success case : Flag = " + Result.TRT_header.statusFlag.ToString());
                    logger.Info("In Success case : Message = " + Result.TRT_header.statusMsg);
                    logger.Debug("Method : getLocationsDetails_DAL Stop");

                    return Result;
                }
                else
                {
                    TRT_LocEntity Error = new TRT_LocEntity();
                    Error.TRT_header.statusFlag = 1;
                    Error.TRT_header.statusMsg = "Invalid Location ID";

                    logger.Error("Flag = " + Error.TRT_header.statusFlag.ToString());
                    logger.Error("Message = " + Error.TRT_header.statusMsg);
                    logger.Error("TaxiRequest_DAL: getLocationsDetails_DAL() Stop");

                    return Error;
                }

            }

            catch (Exception ex)
            {
                logger.Error("Exception  At BAL - getLocationsDetails_DAL  : " + ex.Message.ToString());
                logger.Error("TaxiRequest_DAL: getLocationsDetails_DAL() Stop");
                throw ex;
            }
        }



        /// <summary>
        /// This function fetches the Location Details from the Sharepoint web service
        /// </summary>
        /// <param name="PST_InputEntity"></param>
        /// <returns>
        /// <paramref name="PST_OutputEntity"/>
        /// </returns>
        /// <history>
        ///     Hari haran      07/05/2012      created
        /// </history>
        /// 

        public TRT_OutputEntity updateTaxiRequestDetails_DAL_New(TRT_InputEntity TRT_EntryDAL)
        {

            logger.Debug("TaxiRequest_DAL: updateTaxiRequestDetails_DAL_New() called");

            try
            {
                string flag = string.Empty;
                TRT_OutputEntity Result = new TRT_OutputEntity();

                string empDetailXmlString = TaxiWebService.GetEmployeeDetails(TRT_EntryDAL.empNo);
                XmlDocument xmlEmpDoc = new XmlDocument();
                xmlEmpDoc.LoadXml(empDetailXmlString);

                //XmlNode root = xmlDoc.DocumentElement;

                //XmlNode AllocCCode = root.SelectSingleNode("/NewDataSet/Projects");
                //GeneralInformationNode.SelectSingleNode("ProjectId").InnerText;
                XmlNode EmpNode;
                EmpNode = xmlEmpDoc.SelectSingleNode("/NewDataSet/Employees");
                if (EmpNode == null)
                {
                    Result.statusFlag = 1;
                    Result.statusMsg = TRT_Constants.InvalidEmpID;
                }
                else
                {
                    string EmpName = EmpNode.SelectSingleNode("employeename").InnerText;
                    string OrgUnit = EmpNode.SelectSingleNode("DeptName").InnerText;
                    string Supervisor= EmpNode.SelectSingleNode("SupervisorName").InnerText;
                    string SupervisorMailID = EmpNode.SelectSingleNode("SupervisorMailID").InnerText;
                    string EmpMailID = EmpNode.SelectSingleNode("EmpEmailID").InnerText;
                    string ReportTime = TRT_EntryDAL.hhTime + ":" + TRT_EntryDAL.mmTime;

                    TaxiSPService.Credentials = new NetworkCredential("bgh29309", "Password1", "asian");
                    XmlDocument xmlDoc = new XmlDocument();
                    TaxiSPService.Url = "http://awstage.intra.aricent.com/sites/applications/TaxiRequest/_vti_bin/Lists.asmx";

                    string listName = "TaxiRequestDetails";
                    //string viewName = "{7137FFF8-48FF-4C69-8C76-0E3BBD1EA7F9}";
                    //string rowLimit = "150";

                    System.Xml.XmlNode ndListView = TaxiSPService.GetListAndView(listName, "");
                    string strListID = ndListView.ChildNodes[0].Attributes["Name"].Value;
                    string strViewID = ndListView.ChildNodes[1].Attributes["Name"].Value;

                    /*Create an XmlDocument object and construct a Batch element and its
                    attributes. Note that an empty ViewName parameter causes the method to use the default view. */
                
                    System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                    System.Xml.XmlElement batchElement = doc.CreateElement("Batch");
                    batchElement.SetAttribute("OnError", "Continue");
                    batchElement.SetAttribute("ListVersion", "1");
                    batchElement.SetAttribute("ViewName", strViewID);

                    /*Specify methods for the batch post using CAML. To update or delete, 
                    specify the ID of the item, and to update or add, specify 
                    the value to place in the specified column.*/
                    XmlNode listXml = TaxiSPService.GetList(listName);

                    batchElement.InnerXml = "<Method ID='1' Cmd='New'>" +
                       "<Field Name='EmployeeNumber'>" + TRT_EntryDAL.empNo + "</Field>" +
                       "<Field Name='Title'>" + EmpName + "</Field>" +
                       "<Field Name='OUCode'>" + OrgUnit + "</Field>" +
                       "<Field Name='ChargeCode'>" + TRT_EntryDAL.chargeCode + "</Field>" +
                       "<Field Name='PurposeCode'>" + TRT_EntryDAL.purpose + "</Field>" +
                       "<Field Name='VehcileType'>" + TRT_EntryDAL.vehicleType + "</Field>" +
                       "<Field Name='PlotCode'>" + TRT_EntryDAL.plot + "</Field>" +
                       "<Field Name='Requireddate'>" + TRT_EntryDAL.reqDate + "</Field>" +
                       "<Field Name='FromPlace'>" + TRT_EntryDAL.fromPlace + "</Field>" +
                       "<Field Name='ToPlace'>" + TRT_EntryDAL.toPlace + "</Field>" +
                       "<Field Name='LandMark'>" + TRT_EntryDAL.landmark + "</Field>" +
                       "<Field Name='ContactNo'>" + TRT_EntryDAL.contactNum + "</Field>" +
                       "<Field Name='FlightDetails'>" + TRT_EntryDAL.flightDetails + "</Field>" +
                       "<Field Name='Comments'>" + TRT_EntryDAL.comments + "</Field>" +
                       //"<Field Name='Minutes'>" + "SBU" + "</Field>" +
                       "<Field Name='Duration'>" + TRT_EntryDAL.duration + "</Field>" +
                       "<Field Name='RowStatus'>" + ReportTime + "</Field>" +
                       "<Field Name='LocationCode'>" + TRT_EntryDAL.location + "</Field>" +
                       "<Field Name='Approved_x0020_By'>" + Supervisor + "</Field>" +
                       "</Method>";

                    /*Update list items. This example uses the list GUID, which is recommended, 
                    but the list display name will also work.*/
                    XmlNode opRes = TaxiSPService.UpdateListItems(strListID, batchElement);
                    flag = opRes.InnerText;
                    if (flag.CompareTo("0x00000000") == 0)
                    {
                        Result.statusFlag = 0;
                        Result.statusMsg = TRT_Constants.Success;

                        logger.Info("In Success case : Flag = " + Result.statusFlag.ToString());
                        logger.Info("In Success case : Message = " + Result.statusMsg);

                        string mailBody = string.Format(TRT_Constants.mail_BodyFormat, TRT_EntryDAL.empNo, EmpName, OrgUnit, Supervisor, TRT_EntryDAL.chargeCode,
                            TRT_EntryDAL.purpose, TRT_EntryDAL.vehicleType, TRT_EntryDAL.reqDate,TRT_EntryDAL.fromPlace, TRT_EntryDAL.toPlace, TRT_EntryDAL.plot,
                            TRT_EntryDAL.flightDetails, ReportTime, TRT_EntryDAL.duration, TRT_EntryDAL.landmark, TRT_EntryDAL.contactNum, TRT_EntryDAL.comments,
                            System.DateTime.Today.ToString("dd/MM/yyyy HH:mm:ss"));
                        webServiceExHandling.Send_Email(TRT_Constants.Email_Dic, mailBody);
                    }
                    else
                    {
                        Result.statusFlag = 1;
                        Result.statusMsg = TRT_Constants.Error;

                        logger.Error("Flag = " + Result.statusFlag.ToString());
                        logger.Error("Message = " + Result.statusMsg);
                    }

                }

                logger.Debug("TaxiRequest_DAL: updateTaxiRequestDetails_DAL_New() Stop");

                return Result;
            }
            catch (SqlException dbEx)
            {
                logger.Error("Exception Occured At TRT_DAL - updateTaxiRequestDetails_DAL_New");
                logger.Error("Exception Code : " + dbEx.ErrorCode.ToString());
                logger.Error("Exception Description : " + dbEx.Message.ToString());
                logger.Error("TaxiRequest_DAL: updateTaxiRequestDetails_DAL_New() Stop");


                //tsTrans.Rollback();
                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Error("Exception Occured At TRT_DAL - updateTaxiRequestDetails_DAL_New  : " + ex.Message.ToString());
                logger.Error("TaxiRequest_DAL: updateTaxiRequestDetails_DAL_New() Stop");

                // tsTrans.Rollback();
                //throw new myCustomException(31, ex.Message);
                throw ex;
            }

        }

        /// <summary>
        /// This function fetches the Location Details from the Sharepoint web service
        /// </summary>
        /// <param name="PST_InputEntity"></param>
        /// <returns>
        /// <paramref name="PST_OutputEntity"/>
        /// </returns>
        /// <history>
        ///     Hari haran      07/05/2012      created
        /// </history>
        /// 

        public TRT_OutputEntity updateTaxiRequestDetails_DAL(TRT_InputEntity TRT_EntryDAL)
        {

            databaseLayer dbConStr = new databaseLayer();
            string ConnStr = dbConStr.connectionInfo;

            logger.Debug("Connection string : " + ConnStr);

            SqlConnection Conn = new SqlConnection(ConnStr);

            if (Conn.State == System.Data.ConnectionState.Closed)
            {
                Conn.Open();
                logger.Debug("Connection Status opened ");
            }

            logger.Info("Connetion to the database established");

            try
            {
                int flag = 0;
                string reportTime = TRT_EntryDAL.hhTime.ToString() + ":" + TRT_EntryDAL.mmTime.ToString();

                string Query = "INSERT INTO [AricentMobileAppDB].[dbo].[TaxiReqDetails]";
                Query += "([EmpNo],[Location],[Plot],[Purpose],[VehicleType],[ChargeCode],[FromPlace],[ToPlace],[Landmark],[ContactNo],[FlightDetails],[Comments],[RequiredDate],[ReportTime],[Duration])";
                Query += "VALUES(@EmpNo,@Location,@Plot,@Purpose,@VehicleType,@ChargeCode,@FromPlace,@ToPlace,@Landmark,@ContactNo,@FlightDetails,@Comments,@RequiredDate,@ReportTime,@Duration)";
                
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Conn;
                cmd.CommandText = Query;
                
                cmd.Parameters.AddWithValue("@EmpNo", TRT_EntryDAL.empNo);
                cmd.Parameters.AddWithValue("@Location", TRT_EntryDAL.location);
                cmd.Parameters.AddWithValue("@Plot", TRT_EntryDAL.plot);
                cmd.Parameters.AddWithValue("@Purpose", TRT_EntryDAL.purpose);
                cmd.Parameters.AddWithValue("@VehicleType", TRT_EntryDAL.vehicleType);
                cmd.Parameters.AddWithValue("@ChargeCode", TRT_EntryDAL.chargeCode);
                cmd.Parameters.AddWithValue("@FromPlace", TRT_EntryDAL.fromPlace);
                cmd.Parameters.AddWithValue("@ToPlace", TRT_EntryDAL.toPlace);
                cmd.Parameters.AddWithValue("@Landmark", TRT_EntryDAL.landmark);
                cmd.Parameters.AddWithValue("@ContactNo", TRT_EntryDAL.contactNum);
                cmd.Parameters.AddWithValue("@FlightDetails", TRT_EntryDAL.flightDetails);
                cmd.Parameters.AddWithValue("@Comments", TRT_EntryDAL.comments);
                cmd.Parameters.AddWithValue("@RequiredDate", TRT_EntryDAL.reqDate);
                cmd.Parameters.AddWithValue("@ReportTime", "20:00");
                cmd.Parameters.AddWithValue("@Duration", TRT_EntryDAL.duration);

                flag = cmd.ExecuteNonQuery();

                TRT_OutputEntity Result = new TRT_OutputEntity();

                if (flag == 1)
                {
                    Result.statusFlag = 0;
                    Result.statusMsg = TRT_Constants.Success;
                    string mailBody = string.Format(TRT_Constants.mail_BodyFormat,TRT_EntryDAL.empNo,TRT_EntryDAL.location);
                    webServiceExHandling.Send_Email(TRT_Constants.Email_Dic, mailBody);
                }
                else
                {
                    Result.statusFlag = 1;
                    Result.statusMsg = TRT_Constants.Error;
                }
                return Result;
            }
            catch (SqlException dbEx)
            {
                logger.Fatal("Exception Occured At TRT_DAL - updateTaxiRequestDetails_DAL");
                logger.Debug("Exception Code : " + dbEx.ErrorCode.ToString());
                logger.Debug("Exception Description : " + dbEx.Message.ToString());
                logger.Error("Error : updateTaxiRequestDetails_DAL Stop");

                //tsTrans.Rollback();
                throw dbEx;
            }
            catch (Exception ex)
            {
                logger.Fatal("Exception Occured At TRT_DAL - updateTaxiRequestDetails_DAL  : " + ex.Message.ToString());
                logger.Error("Error : updateTaxiRequestDetails_DAL Stop");

                // tsTrans.Rollback();
                //throw new myCustomException(31, ex.Message);
                throw ex;
            }
            finally
            {
                logger.Debug("Connection to the database Closed ");

                Conn.Dispose();

            }
        }
    }

}
