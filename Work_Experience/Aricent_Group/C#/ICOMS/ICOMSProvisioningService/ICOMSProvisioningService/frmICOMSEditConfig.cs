using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Net;
using System.Net.Sockets;
using log4net;
using log4net.Config;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace ICOMSProvisioningService
{
    public partial class frmICOMSEditConfig : Form
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(frmICOMSEditConfig));

       XmlDocument xmlDoc_ICOMS_Curr;
       XmlDocument xmlDoc_ICOMS_Def;
       List<string> lstSiteIds=new List<string>();
       List<string> lstSiteNames=new List<string>();
       List<string> lstSiteIPAndPort = new List<string>();
       bool isDirtyChanged=false;       
       string strEditNodeSiteId;
       string strEditNodeSiteName;
       string strEditNodeSiteIPandPort;
       string cnfgFilePath = string.Empty;
       

        public frmICOMSEditConfig()
        {
            InitializeComponent();    
            
        }         
      
        /// <summary>
        /// loading values based on the selection from tree view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvICOMSEditConfig_AfterSelect(object sender, TreeViewEventArgs e)
        {
            logger.Info("ICOMSEditConfiguration::tvICOMSEditConfig_AfterSelect() called");
            logger.Info(string.Format("tvICOMSEditConfig_AfterSelect():: Selected Node Name \"{0}\"",e.Node.Tag.ToString()));
            try
            {
                switch (e.Node.Tag.ToString().ToUpper())
                {
                    case "SITES":
                        grpSiteARU.Visible = true;
                        clearAllFieldsOfSite();
                        btnRemoveSite.Visible = false;
                        btnUpdateSite.Visible = false;
                        grpCRMConnection.Visible = false;
                        grpLBConnection.Visible = false;
                        break;
                    case "SITE":
                        grpCRMConnection.Visible = false;
                        grpLBConnection.Visible = false;
                        grpSiteARU.Visible = true;
                        btnRemoveSite.Visible = true;
                        btnUpdateSite.Visible = true;
                        manageNode2CntrlAndCntrl2Node("1");
                        break;
                    case "CRMCONNECTION":
                        grpSiteARU.Visible = false;
                        grpCRMConnection.Visible = true;
                        grpLBConnection.Visible = false;
                        manageNode2CntrlAndCntrl2Node("1");
                        break;
                    case "LOADBALANCER":
                        grpSiteARU.Visible = false;
                        grpCRMConnection.Visible = false;
                        grpLBConnection.Visible = true;
                        manageNode2CntrlAndCntrl2Node("1");
                        break;
                    case "SERVICECONFIGURATION":
                        break;
                    case "VODPACKAGE":
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("tvICOMSEditConfig_AfterSelect(): Err Msg: {0}", ex.Message));
                logger.Error(string.Format("tvICOMSEditConfig_AfterSelect(): Stack Trace: {0}", ex.StackTrace));
                logger.Error("ICOMSEditConfiguration::tvICOMSEditConfig_AfterSelect() throwing error");
            }
        }

        /// <summary>
        /// reading app.config file during installation
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetConfigurationValue(string key)
        {
            logger.Info("ICOMSEditConfiguration::GetConfigurationValue() called");
            var service = Assembly.GetAssembly(typeof(ICOMSServiceInstaller));
            Configuration config = ConfigurationManager.OpenExeConfiguration(service.Location);
            if (config.AppSettings.Settings[key] == null)
            {                
                logger.Error(string.Format("GetConfigurationValue(): Settings collection does not contain the requested key: {0}", key));
            }
            return config.AppSettings.Settings[key].Value;
        } 

        /// <summary>
        /// loading config file on form load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmICOMSEditConfig_Load(object sender, EventArgs e)
        {
            logger.Info("ICOMSEditConfiguration::frmICOMSEditConfig_Load() called");
            try
            {                
                xmlDoc_ICOMS_Curr = new XmlDocument();
                cnfgFilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + GetConfigurationValue("ICOMSXMLFILEPATH");
                Uri path = new Uri(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase) + GetConfigurationValue("ICOMSXMLFILEPATH"));
                cnfgFilePath = path.LocalPath;
                xmlDoc_ICOMS_Curr.Load(cnfgFilePath);   //load the XMl file            
                logger.Info(cnfgFilePath);
                xmlDoc_ICOMS_Def = (XmlDocument)xmlDoc_ICOMS_Curr.Clone();
                loadICOMSConfiguration(xmlDoc_ICOMS_Curr);
                isDirtyChanged = false;

            }
            catch (Exception ex)
            {
                logger.Error(string.Format("frmICOMSEditConfig_Load(): Err Msg: {0}", ex.Message));
                logger.Error(string.Format("frmICOMSEditConfig_Load(): Stack Trace: {0}", ex.StackTrace));
                logger.Error("ICOMSEditConfiguration::frmICOMSEditConfig_Load() throwing error");
            }
        }

       /// <summary>
       /// creating new tree view node
       /// </summary>
       /// <param name="tndName"></param>
       /// <param name="tndText"></param>
       /// <param name="tndTag"></param>
       /// <returns></returns>
        private TreeNode getTreeNode(string tndName, string tndText,string tndTag)
        {
            logger.Info("ICOMSEditConfiguration::getTreeNode() called");            
            TreeNode newTreeNode = new TreeNode();
            newTreeNode.Text = tndText;
            newTreeNode.Name = tndName;
            newTreeNode.Tag = tndTag;
            logger.Info(string.Format("getTreeNode():: Tree Node Created. Name :'{0}' Text:'{1}' Tag:'{2}' ", tndName, tndText, tndTag));
            return newTreeNode;
        }

        /// <summary>
        /// Manage information flow from treeview to controls and vice-versa
        /// </summary>
        /// <param name="isNodeUpdateFlag"></param>
        private void manageNode2CntrlAndCntrl2Node(string isNodeUpdateFlag)
        {

            logger.Info("ICOMSEditConfiguration::manageNode2CntrlAndCntrl2Node() called");
            try
            {
                XmlNode siteNodeInfo;
                TreeNode tn = tvICOMSEditConfig.SelectedNode;

                logger.Info(string.Format("manageNode2CntrlAndCntrl2Node():: Selected Node Name \"{0}\"", tn.Tag.ToString()));

                switch (tn.Tag.ToString().ToUpper())
                {
                    case "SITES":
                        break;
                    case "SITE":
                        siteNodeInfo = xmlDoc_ICOMS_Curr.SelectSingleNode(string.Format("/serviceconfiguration/sites/site[@id=\"{0}\"]", tn.Name));
                        if (isNodeUpdateFlag.Equals("1"))
                        {
                            populateValuesOnControls4Site(siteNodeInfo);
                        }
                        else if (isNodeUpdateFlag.Equals("2"))
                        {
                            populateValuesOnNodes4Site(siteNodeInfo);
                        }
                        else if (isNodeUpdateFlag.Equals("3"))
                        {
                            populateValuesOnNodes4Site(siteNodeInfo);
                        }
                        break;
                    case "CRMCONNECTION":
                        siteNodeInfo = xmlDoc_ICOMS_Curr.SelectSingleNode("/serviceconfiguration/CRMconnection");
                        if (isNodeUpdateFlag.Equals("1"))
                        {
                            populateValuesOnControls4CRMConn(siteNodeInfo);
                        }
                        else if (isNodeUpdateFlag.Equals("2"))
                        {
                            populateValuesOnNodes4CRMConn(siteNodeInfo);
                        }
                        break;
                    case "LOADBALANCER":
                        siteNodeInfo = xmlDoc_ICOMS_Curr.SelectSingleNode("/serviceconfiguration/LoadBalancer");
                        if (isNodeUpdateFlag.Equals("1"))
                        {
                            populateValuesOnControls4LoadBalancerConn(siteNodeInfo);
                        }
                        else if (isNodeUpdateFlag.Equals("2"))
                        {
                            populateValuesOnNodes4LoadBalancerConn(siteNodeInfo);
                        }
                        break;
                    case "SERVICECONFIGURATION":
                        break;
                    case "VODPACKAGE":
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("manageNode2CntrlAndCntrl2Node(): Err Msg: {0}", ex.Message));
                logger.Error(string.Format("manageNode2CntrlAndCntrl2Node(): Stack Trace: {0}", ex.StackTrace));
                logger.Error("ICOMSEditConfiguration::manageNode2CntrlAndCntrl2Node() throwing error");
            }
        }
                
        /// <summary>
        /// Populate information on controls based on selection of tree view node for site
        /// </summary>
        /// <param name="nde"></param>
        private void populateValuesOnControls4Site(XmlNode nde)
        {
            logger.Info("ICOMSEditConfiguration::populateValuesOnControls4Site() called");
            try
            {
                logger.Info(string.Format("populateValuesOnControls4Site():: Selected XML Node Information \"{0}\"", nde.OuterXml));

                strEditNodeSiteId = nde.Attributes["id"].Value;
                strEditNodeSiteName = nde.Attributes["name"].Value;
                strEditNodeSiteIPandPort = nde["listenerAddress"].InnerText + nde["listenerPort"].InnerText;
                txtSiteID.Text = nde.Attributes["id"].Value;
                txtSiteName.Text = nde.Attributes["name"].Value;
                txtListenerAddress.Text = nde["listenerAddress"].InnerText;
                txtlistenerPort.Text = nde["listenerPort"].InnerText;
                txtTokenName4SiteId.Text = nde["TokenName4SiteId"].InnerText;
                cmbNatFrmt4cFlag.SelectedIndex = getValueForComboBox(nde["NativeFormat4cFlag"].InnerText);
                cmbDeviceIdFmt.SelectedItem = nde["DeviceIdFormat"].InnerText;
                cmbCusIdFlag.SelectedIndex = getValueForComboBox(nde["CustomerIdFlag"].InnerText);
                isDirtyChanged = false;
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("populateValuesOnControls4Site(): Err Msg: {0}", ex.Message));
                logger.Error(string.Format("populateValuesOnControls4Site(): Stack Trace: {0}", ex.StackTrace));
                logger.Error("ICOMSEditConfiguration::populateValuesOnControls4Site() throwing error");
            }
        }

        /// <summary>
        /// update information from controls to Tree view node when update happens for a site
        /// </summary>
        /// <param name="nde"></param>
        private void populateValuesOnNodes4Site(XmlNode nde)
        {
            logger.Info("ICOMSEditConfiguration::populateValuesOnNodes4Site() called");
            try
            {
                nde.Attributes["id"].Value = txtSiteID.Text.Trim();
                nde.Attributes["name"].Value = txtSiteName.Text.Trim();
                nde["listenerAddress"].InnerText = txtListenerAddress.Text.Trim();
                nde["listenerPort"].InnerText = txtlistenerPort.Text.Trim();
                nde["TokenName4SiteId"].InnerText = txtTokenName4SiteId.Text.Trim();
                nde["CustomerIdFlag"].InnerText = getValueForNodeFromComboBox(cmbCusIdFlag.SelectedIndex);
                nde["DeviceIdFormat"].InnerText = cmbDeviceIdFmt.SelectedItem.ToString();
                nde["NativeFormat4cFlag"].InnerText = getValueForNodeFromComboBox(cmbNatFrmt4cFlag.SelectedIndex);
                               
                tvICOMSEditConfig.SelectedNode.Name = txtSiteID.Text.Trim();
                tvICOMSEditConfig.SelectedNode.Text = txtSiteName.Text.Trim();

                lstSiteIds.Remove(strEditNodeSiteId);
                lstSiteNames.Remove(strEditNodeSiteName);
                lstSiteIPAndPort.Remove(strEditNodeSiteIPandPort);

                lstSiteIds.Add(txtSiteID.Text.Trim());
                lstSiteNames.Add(txtSiteName.Text.Trim());
                lstSiteIPAndPort.Add(txtListenerAddress.Text.Trim() + txtlistenerPort.Text.Trim());

                strEditNodeSiteId = txtSiteID.Text.Trim();
                strEditNodeSiteName = txtSiteName.Text.Trim();
                strEditNodeSiteIPandPort = txtListenerAddress.Text.Trim() + txtlistenerPort.Text.Trim();
                logger.Info(string.Format("populateValuesOnControls4Site():: Updated XML Node Information \"{0}\"", nde.OuterXml));
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("populateValuesOnControls4Site(): Err Msg: {0}", ex.Message));
                logger.Error(string.Format("populateValuesOnControls4Site(): Stack Trace: {0}", ex.StackTrace));
                logger.Error("ICOMSEditConfiguration::populateValuesOnControls4Site() throwing error");
            }
        }

        /// <summary>
        /// Populate information on controls based on selection of tree view node for CRM Connection
        /// </summary>
        /// <param name="nde"></param>
        private void populateValuesOnControls4CRMConn(XmlNode nde)
        {
            logger.Info("ICOMSEditConfiguration::populateValuesOnControls4CRMConn() called");
            try
            {
                logger.Info(string.Format("populateValuesOnControls4CRMConn():: Selected XML Node Information \"{0}\"", nde.OuterXml));
                txtPrimaryURL.Text = nde["PrimaryURL"].InnerText;
                txtBackUpURL.Text = nde["BackupURL"].InnerText;
                txtPrimaryCCI4cURL.Text = nde["PrimaryCCI4cURL"].InnerText;
                txtBackUpCCI4cURL.Text = nde["BackupCCI4cURL"].InnerText;
                txtHTTPTimeout.Text = nde["HTTPtimeout"].InnerText;
                txtRetryNumbers.Text = nde["RetryNumbers"].InnerText;
                txtRetryWaitDuration.Text = nde["RetryWaitDuration"].InnerText;
                txtHTTPErrorCodes.Text = nde["HTTPErrorCodes"].InnerText;
                txtFailOverTime.Text = nde["FailoverTime"].InnerText;
                isDirtyChanged = false;
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("populateValuesOnControls4CRMConn(): Err Msg: {0}", ex.Message));
                logger.Error(string.Format("populateValuesOnControls4CRMConn(): Stack Trace: {0}", ex.StackTrace));
                logger.Error("ICOMSEditConfiguration::populateValuesOnControls4CRMConn() throwing error");
            }
            
        }

        /// <summary>
        /// update information from controls to Tree view node when update happens for a CRM Connection
        /// </summary>
        /// <param name="nde"></param>
        private void populateValuesOnNodes4CRMConn(XmlNode nde)
        {
            logger.Info("ICOMSEditConfiguration::populateValuesOnNodes4CRMConn() called");
            try
            {
                nde["PrimaryURL"].InnerText = txtPrimaryURL.Text.Trim();
                nde["BackupURL"].InnerText = txtBackUpURL.Text.Trim();
                nde["PrimaryCCI4cURL"].InnerText = txtPrimaryCCI4cURL.Text.Trim();
                nde["BackupCCI4cURL"].InnerText = txtBackUpCCI4cURL.Text.Trim();
                nde["HTTPtimeout"].InnerText = txtHTTPTimeout.Text.Trim();
                nde["RetryNumbers"].InnerText = txtRetryNumbers.Text.Trim();
                nde["RetryWaitDuration"].InnerText = txtRetryWaitDuration.Text.Trim();
                nde["HTTPErrorCodes"].InnerText = txtHTTPErrorCodes.Text.Trim();
                nde["FailoverTime"].InnerText = txtFailOverTime.Text.Trim();
                logger.Info(string.Format("populateValuesOnNodes4CRMConn():: Updated XML Node Information \"{0}\"", nde.OuterXml));
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("populateValuesOnNodes4CRMConn(): Err Msg: {0}", ex.Message));
                logger.Error(string.Format("populateValuesOnNodes4CRMConn(): Stack Trace: {0}", ex.StackTrace));
                logger.Error("ICOMSEditConfiguration::populateValuesOnNodes4CRMConn() throwing error");
            }

        }

        /// <summary>
        /// Populate information on controls based on selection of tree view node for Load Balancer Connection
        /// </summary>
        /// <param name="nde"></param>
        private void populateValuesOnControls4LoadBalancerConn(XmlNode nde)
        {
            logger.Info("ICOMSEditConfiguration::populateValuesOnControls4LoadBalancerConn() called");
            try
            {
                logger.Info(string.Format("populateValuesOnControls4LoadBalancerConn():: Selected XML Node Information \"{0}\"", nde.OuterXml));
                txtLBAddress.Text = nde["listenerAddress"].InnerText;
                txtLBPort.Text = nde["listenerPort"].InnerText;
                isDirtyChanged = false;
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("populateValuesOnControls4LoadBalancerConn(): Err Msg: {0}", ex.Message));
                logger.Error(string.Format("populateValuesOnControls4LoadBalancerConn(): Stack Trace: {0}", ex.StackTrace));
                logger.Error("ICOMSEditConfiguration::populateValuesOnControls4LoadBalancerConn() throwing error");
            }

        }

        /// <summary>
        /// update information from controls to Tree view node when update happens for a Load Balancer Connection
        /// </summary>
        /// <param name="nde"></param>
        private void populateValuesOnNodes4LoadBalancerConn(XmlNode nde)
        {
            logger.Info("ICOMSEditConfiguration::populateValuesOnNodes4LoadBalancerConn() called");
            try
            {
                nde["listenerAddress"].InnerText = txtLBAddress.Text.Trim();
                nde["listenerPort"].InnerText = txtLBPort.Text.Trim();

                logger.Info(string.Format("populateValuesOnNodes4LoadBalancerConn():: Updated XML Node Information \"{0}\"", nde.OuterXml));
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("populateValuesOnNodes4LoadBalancerConn(): Err Msg: {0}", ex.Message));
                logger.Error(string.Format("populateValuesOnNodes4LoadBalancerConn(): Stack Trace: {0}", ex.StackTrace));
                logger.Error("ICOMSEditConfiguration::populateValuesOnNodes4LoadBalancerConn() throwing error");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strVal"></param>
        /// <returns></returns>
        private int getValueForComboBox(string strVal)
        {
            logger.Info("ICOMSEditConfiguration::getValueForComboBox() called");
            int retVal = 0;
            if (Convert.ToBoolean(strVal).Equals(true))
            {
                retVal= 1;
            }
            logger.Info(string.Format("getValueForComboBox():: return value is \"{0}\"", retVal));
            return retVal;
        }

        private string getValueForNodeFromComboBox(int cbIndexVal)
        {
            logger.Info("ICOMSEditConfiguration::getValueForNodeFromComboBox() called");
            string retVal = "false";
            if (cbIndexVal.Equals(1))
            {
                retVal = "true";
            }
            logger.Info(string.Format("getValueForNodeFromComboBox():: return value is \"{0}\"", retVal));
            return retVal;            
        }
             
        /// <summary>
        ///  update CRM Connection Node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCRMConnUpdate_Click(object sender, EventArgs e)
        {
            logger.Info("ICOMSEditConfiguration::btnCRMConnUpdate_Click() called");
            try
            {
                if (!checkValidation4CRMConnection())
                {
                    logger.Warn("checkValidation4CRMConnection::CRM Connection Information Validation failedValidation failed");
                    return;
                }
                isDirtyChanged = false;
                manageNode2CntrlAndCntrl2Node("2");
                MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_INFO_BTN_UPD_CRM_SUCCESS, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("checkValidation4CRMConnection(): Err Msg: {0}", ex.Message));
                logger.Error(string.Format("checkValidation4CRMConnection(): Stack Trace: {0}", ex.StackTrace));
                logger.Error("ICOMSEditConfiguration::checkValidation4CRMConnection() throwing error");
            }
        }

        /// <summary>
        ///  update Load Balancer Connection Node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLBConnUpdate_Click(object sender, EventArgs e)
        {
            logger.Info("ICOMSEditConfiguration::btnLBConnUpdate_Click() called");
            try
            {
                if (!checkValidation4LoadBalancerConnection())
                {
                    logger.Warn("checkValidation4LoadBalancerConnection::Load Balancer Connection Information Validation failed");
                    return;
                }
                isDirtyChanged = false;
                manageNode2CntrlAndCntrl2Node("2");
                MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_INFO_BTN_UPD_LB_SUCCESS, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("checkValidation4LoadBalancerConnection(): Err Msg: {0}", ex.Message));
                logger.Error(string.Format("checkValidation4LoadBalancerConnection(): Stack Trace: {0}", ex.StackTrace));
                logger.Error("ICOMSEditConfiguration::checkValidation4LoadBalancerConnection() throwing error");
            }
        }

        /// <summary>
        /// update Site Information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateSite_Click(object sender, EventArgs e)
        {
            logger.Info("ICOMSEditConfiguration::btnUpdateSite_Click() called");
            try
            {
                if (!checkValidation4Site())
                {
                    logger.Warn("btnUpdateSite_Click:: Site Information Validation failed");
                    return;
                }

                if (!txtSiteID.Text.Trim().Equals(strEditNodeSiteId))
                {

                    if (lstSiteIds.Contains(txtSiteID.Text.Trim(), StringComparer.OrdinalIgnoreCase))
                    {
                        logger.Warn("btnUpdateSite_Click::Site ID Validation failed due to duplication");
                        MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_VALIDATE_MSG_DUP_SITE_ID, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtSiteID.Focus();
                        return;
                    }
                }

                if (!txtSiteName.Text.Trim().ToUpper().Equals(strEditNodeSiteName.ToUpper()))
                {
                    if (lstSiteNames.Contains(txtSiteName.Text.Trim(), StringComparer.OrdinalIgnoreCase))
                    {
                        logger.Warn("btnUpdateSite_Click::Site Name Validation failed due to duplication");
                        MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_VALIDATE_MSG_DUP_SITE_NAME, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtSiteName.Focus();
                        return;
                    }
                }
                 
                if (!((txtListenerAddress.Text.Trim()+txtlistenerPort.Text.Trim()).ToUpper().Equals(strEditNodeSiteIPandPort.ToUpper())))
                {
                    if(lstSiteIPAndPort.Contains(txtListenerAddress.Text.Trim()+txtlistenerPort.Text.Trim(),StringComparer.OrdinalIgnoreCase))
                    {
                        logger.Warn("btnUpdateSite_Click::Listner Address and Port combination already exist in other site");
                        MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_VALIDATE_MSG_DUP_SITE_IP_PORT, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        txtlistenerPort.Focus();
                        return;
                    }
                }


                isDirtyChanged = false;
                manageNode2CntrlAndCntrl2Node("2");
                MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_INFO_BTN_UPD_SITE_SUCCESS, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("btnUpdateSite_Click(): Err Msg: {0}", ex.Message));
                logger.Error(string.Format("btnUpdateSite_Click(): Stack Trace: {0}", ex.StackTrace));
                logger.Error("ICOMSEditConfiguration::btnUpdateSite_Click() throwing error");
            }
        }

        /// <summary>
        /// SAVE configuration information to XML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSiteConfigSave_Click(object sender, EventArgs e)
        {
            logger.Info("ICOMSEditConfiguration::btnSiteConfigSave_Click() called");
            try
            {
                if (!IsIgnoreChanges()) return;
                logger.Info(cnfgFilePath);
                xmlDoc_ICOMS_Curr.Save(cnfgFilePath);
                isDirtyChanged = false;
                MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_INFO_BTN_SAVE_SUCCESS, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("btnSiteConfigSave_Click(): Err Msg: {0}", ex.Message));
                logger.Error(string.Format("btnSiteConfigSave_Click(): Stack Trace: {0}", ex.StackTrace));
                logger.Error("ICOMSEditConfiguration::btnSiteConfigSave_Click() throwing error");
            }
        }       

        /// <summary>
        /// Loading config file
        /// </summary>
        /// <param name="xmlDoc"></param>
        private void loadICOMSConfiguration(XmlDocument xmlDoc)
        {
            try
            {
                logger.Info("ICOMSEditConfiguration::loadICOMSConfiguration() called");
                tvICOMSEditConfig.Nodes.Clear();

                tvICOMSEditConfig.Nodes.Add(getTreeNode("sites", "sites", "sites"));
                tvICOMSEditConfig.Nodes.Add(getTreeNode("CRMconnection", "CRMconnection", "CRMconnection"));
                tvICOMSEditConfig.Nodes.Add(getTreeNode("LoadBalancer", "LoadBalancer", "LoadBalancer"));

                tvICOMSEditConfig.SelectedNode = tvICOMSEditConfig.Nodes[0];
                XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("/serviceconfiguration/sites/site");
              
                lstSiteIds.Clear();
                lstSiteNames.Clear();
                lstSiteIPAndPort.Clear();

                foreach (XmlNode nde in nodeList)
                {
                    tvICOMSEditConfig.SelectedNode.Nodes.Add(getTreeNode(nde.Attributes.GetNamedItem("id").Value, nde.Attributes.GetNamedItem("name").Value, "site"));
                    lstSiteIds.Add(nde.Attributes.GetNamedItem("id").Value);
                    lstSiteNames.Add(nde.Attributes.GetNamedItem("name").Value);
                    lstSiteIPAndPort.Add(nde["listenerAddress"].InnerText+nde["listenerPort"].InnerText);
                }

                logger.Info(string.Format("loadICOMSConfiguration():: Site Id List created  \"{0}\"", string.Join(",",lstSiteIds.ToArray())));
                logger.Info(string.Format("loadICOMSConfiguration():: Site Name List created \"{0}\"", string.Join(",",lstSiteNames.ToArray())));

                xmlDoc_ICOMS_Curr = (XmlDocument) xmlDoc.Clone();
                tvICOMSEditConfig.ExpandAll();
                tvICOMSEditConfig.Focus();
            }
            catch (XmlException xmlEx)
            {
                logger.Error(string.Format("loadICOMSConfiguration(): Err Msg: {0}", xmlEx.Message));
                logger.Error(string.Format("loadICOMSConfiguration(): Stack Trace: {0}", xmlEx.StackTrace));
                logger.Error("ICOMSEditConfiguration::loadICOMSConfiguration() throwing error");
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("loadICOMSConfiguration(): Err Msg: {0}", ex.Message));
                logger.Error(string.Format("loadICOMSConfiguration(): Stack Trace: {0}", ex.StackTrace));
                logger.Error("ICOMSEditConfiguration::loadICOMSConfiguration() throwing error");
            }
        }

        /// <summary>
        /// loads default configuration file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSiteConfigDefault_Click(object sender, EventArgs e)
        {

            logger.Info("ICOMSEditConfiguration::btnSiteConfigDefault_Click() called");
            try
            {
                if (!IsIgnoreChanges()) return;
                DialogResult result = MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_WARN_BTN_LOAD_DEF, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result.Equals(DialogResult.OK))
                {
                    loadICOMSConfiguration(xmlDoc_ICOMS_Def);
                }
                isDirtyChanged = false;
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("btnSiteConfigDefault_Click(): Err Msg: {0}", ex.Message));
                logger.Error(string.Format("btnSiteConfigDefault_Click(): Stack Trace: {0}", ex.StackTrace));
                logger.Error("ICOMSEditConfiguration::btnSiteConfigDefault_Click() throwing error");
                MessageBox.Show(ex.Message);
            }
        }

        private void txtSiteID_KeyPress(object sender, KeyPressEventArgs e)
        {           
            if (!((e.KeyChar > 47 && e.KeyChar < 58) || e.KeyChar==8))
            {
                e.Handled = true;
            }
        }

        private void txtSiteID_Enter(object sender, EventArgs e)
        {
            setHelpMessage4Controls(ServiceConstantsManager.CONST_ICOMS_HELP_SITE_ID);
        }

        private void txtPrimaryURL_Enter(object sender, EventArgs e)
        {
            setHelpMessage4Controls(ServiceConstantsManager.CONST_ICOMS_HELP_CRMCONNECTION_PRIMARYURL);
        }

        private void txtPrimaryURL_Leave(object sender, EventArgs e)
        {
            setHelpMessage4Empty();
        }

        private void txtPrimaryCCI4cURL_Enter(object sender, EventArgs e)
        {
            setHelpMessage4Controls(ServiceConstantsManager.CONST_ICOMS_HELP_CRMCONNECTION_PRIMARYCCI4CURL);
        }

        private void txtPrimaryCCI4cURL_Leave(object sender, EventArgs e)
        {
            setHelpMessage4Empty();
        }

        private void txtLBAddress_Enter(object sender, EventArgs e)
        {
            setHelpMessage4Controls(ServiceConstantsManager.CONST_ICOMS_HELP_LBCONNECTION_LISADD);
        }

        private void txtLBAddress_Leave(object sender, EventArgs e)
        {
            setHelpMessage4Empty();
        }

        private void txtLBPort_Enter(object sender, EventArgs e)
        {
            setHelpMessage4Controls(ServiceConstantsManager.CONST_ICOMS_HELP_LBCONNECTION_LISPORT);
        }

        private void txtLBPort_Leave(object sender, EventArgs e)
        {
            setHelpMessage4Empty();
        }

        private void txtSiteID_Leave(object sender, EventArgs e)
        {
            setHelpMessage4Empty();
        }

        private void setHelpMessage4Controls(string strHlpMsg)
        {
            txtHelpInfo.Text = strHlpMsg;
        }

        private void setHelpMessage4Empty()
        {
            txtHelpInfo.Text = "";
        }

        private void txtBackUpURL_Enter(object sender, EventArgs e)
        {
            setHelpMessage4Controls(ServiceConstantsManager.CONST_ICOMS_HELP_CRMCONNECTION_BACKUPURL);
        }

        private void txtBackUpCCI4cURL_Enter(object sender, EventArgs e)
        {
            setHelpMessage4Controls(ServiceConstantsManager.CONST_ICOMS_HELP_CRMCONNECTION_BACKUPCCI4CURL);
        }

        private void txtHTTPTimeout_Enter(object sender, EventArgs e)
        {
            setHelpMessage4Controls(ServiceConstantsManager.CONST_ICOMS_HELP_CRMCONNECTION_HTTPTIMEOUT);
        }

        private void txtRetryNumbers_Enter(object sender, EventArgs e)
        {
            setHelpMessage4Controls(ServiceConstantsManager.CONST_ICOMS_HELP_CRMCONNECTION_RETRYNUM);
        }

        private void txtRetryWaitDuration_Enter(object sender, EventArgs e)
        {
            setHelpMessage4Controls(ServiceConstantsManager.CONST_ICOMS_HELP_CRMCONNECTION_RETRYWAITDUR);
        }

        private void txtHTTPErrorCodes_Enter(object sender, EventArgs e)
        {
            setHelpMessage4Controls(ServiceConstantsManager.CONST_ICOMS_HELP_CRMCONNECTION_HTTPERRCODES);
        }

        private void txtFailOverTime_Enter(object sender, EventArgs e)
        {
            setHelpMessage4Controls(ServiceConstantsManager.CONST_ICOMS_HELP_CRMCONNECTION_FAILOVERTIME);
        }

        private void txtBackUpURL_Leave(object sender, EventArgs e)
        {
            setHelpMessage4Empty();
        }

        private void txtBackUpCCI4cURL_Leave(object sender, EventArgs e)
        {
            setHelpMessage4Empty();
        }

        private void txtHTTPTimeout_Leave(object sender, EventArgs e)
        {
            setHelpMessage4Empty();
        }

        private void txtRetryNumbers_Leave(object sender, EventArgs e)
        {
            setHelpMessage4Empty();
        }

        private void txtRetryWaitDuration_Leave(object sender, EventArgs e)
        {
            setHelpMessage4Empty();
        }

        private void txtHTTPErrorCodes_Resize(object sender, EventArgs e)
        {
            setHelpMessage4Empty();
        }

        private void txtFailOverTime_Leave(object sender, EventArgs e)
        {
            setHelpMessage4Empty();
        }

        private void txtSiteName_Enter(object sender, EventArgs e)
        {
            setHelpMessage4Controls(ServiceConstantsManager.CONST_ICOMS_HELP_SITE_NAME);
        }

        private void txtListenerAddress_Enter(object sender, EventArgs e)
        {
            setHelpMessage4Controls(ServiceConstantsManager.CONST_ICOMS_HELP_SITE_LISADD);
        }

        private void txtlistenerPort_Enter(object sender, EventArgs e)
        {
            setHelpMessage4Controls(ServiceConstantsManager.CONST_ICOMS_HELP_SITE_LISPORT);
        }

        private void txtSiteName_Leave(object sender, EventArgs e)
        {
            setHelpMessage4Empty();
        }

        private void txtListenerAddress_Leave(object sender, EventArgs e)
        {
            setHelpMessage4Empty();
        }

        private void txtlistenerPort_Leave(object sender, EventArgs e)
        {
            setHelpMessage4Empty();
        }

        private void cmbNatFrmt4cFlag_Enter(object sender, EventArgs e)
        {
            setHelpMessage4Controls(ServiceConstantsManager.CONST_ICOMS_HELP_SITE_NATFMT4CFLG);
        }

        private void cmbCusIdFlag_Enter(object sender, EventArgs e)
        {
            setHelpMessage4Controls(ServiceConstantsManager.CONST_ICOMS_HELP_SITE_CUSIDFLG);
        }

        private void cmbCusIdFlag_Leave(object sender, EventArgs e)
        {
            setHelpMessage4Empty();
        }

        private void cmbNatFrmt4cFlag_Leave(object sender, EventArgs e)
        {
            setHelpMessage4Empty();
        }

        /// <summary>
        /// validation for site
        /// </summary>
        /// <returns></returns>
        private bool checkValidation4Site()
        {
            logger.Info("ICOMSEditConfiguration::checkValidation4Site() called");

            if (!(txtSiteID.Text.Trim().Length.Equals(3)))
            {
                logger.Warn("checkValidation4Site::Invalid site id");
                MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_VALIDATE_MSG_SITE_ID, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtSiteID.Focus();
                return false;
            }

            if (txtSiteName.Text.Trim().Length.Equals(0))
            {
                logger.Warn("checkValidation4Site::Invalid site name");
                MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_VALIDATE_MSG_SITE_NAME, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtSiteName.Focus();
                return false;
            }

            if (txtListenerAddress.Text.Trim().Length.Equals(0))
            {
                logger.Warn("checkValidation4Site::Invalid Listener Address");
                MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_VALIDATE_MSG_SITE_LAdd, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtListenerAddress.Focus();
                return false;
            }

            try
            {
                IPAddress siteIPAddr = IPAddress.Parse(txtListenerAddress.Text.Trim());
                txtListenerAddress.Text = siteIPAddr.ToString();
            }
            catch (Exception)
            {
                logger.Warn("checkValidation4Site::Invalid Listener Address");
                MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_VALIDATE_MSG_SITE_LAdd, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtListenerAddress.Focus();
                return false;
            }
            
            if (txtlistenerPort.Text.Trim().Length.Equals(0))
            {
                logger.Warn("checkValidation4Site::Invalid Listener port");
                MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_VALIDATE_MSG_SITEL_PORT, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtlistenerPort.Focus();
                return false;
            }
          
            if (!txtTokenName4SiteId.Text.Trim().Length.Equals(2))
            {
                logger.Warn("checkValidation4Site::Invalid Token Name for SiteId");
                MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_VALIDATE_MSG_TOKEN_NAME4SITE_ID, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtTokenName4SiteId.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// validation for CRM Connection
        /// </summary>
        /// <returns></returns>
        private bool checkValidation4CRMConnection()
        {
            logger.Info("ICOMSEditConfiguration::checkValidation4CRMConnection() called");
              if (txtPrimaryURL.Text.Trim().Length.Equals(0))
                {
                    logger.Warn("checkValidation4CRMConnection::Invalid Primary URL");
                    MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_VALIDATE_MSG_CRMCONN_PURL, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtPrimaryURL.Focus();
                    return false;
                }

                if (txtBackUpURL.Text.Trim().Length.Equals(0))
                {
                    logger.Warn("checkValidation4CRMConnection::Invalid Backup URL");
                    MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_VALIDATE_MSG_CRMCONN_BURL, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtBackUpURL.Focus();
                    return false;
                }
                if (txtPrimaryCCI4cURL.Text.Trim().Length.Equals(0))
                {
                    logger.Warn("checkValidation4CRMConnection::Invalid Primary CCI4c URL");
                    MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_VALIDATE_MSG_CRMCONN_PCCIURL, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtPrimaryCCI4cURL.Focus();
                    return false;
                }

                if (txtBackUpCCI4cURL.Text.Trim().Length.Equals(0))
                {
                    logger.Warn("checkValidation4CRMConnection::Invalid Backup CCI4c URL");
                    MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_VALIDATE_MSG_CRMCONN_BCCIURL, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtBackUpCCI4cURL.Focus();
                    return false;
                }
                if (txtHTTPTimeout.Text.Trim().Length.Equals(0))
                {
                    logger.Warn("checkValidation4CRMConnection::Invalid HTTP Timeout");
                    MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_VALIDATE_MSG_CRMCONN_HTTPTO, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtHTTPTimeout.Focus();
                    return false;
                }

                if (txtRetryNumbers.Text.Trim().Length.Equals(0))
                {
                    logger.Warn("checkValidation4CRMConnection::Invalid Retry Numaber");
                    MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_VALIDATE_MSG_CRMCONN_RETRY, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtRetryNumbers.Focus();
                    return false;
                }

                if (txtRetryWaitDuration.Text.Trim().Length.Equals(0))
                {
                    logger.Warn("checkValidation4CRMConnection::Invalid Retry wait duration");
                    MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_VALIDATE_MSG_CRMCONN_RETRYWAITDUR, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtRetryWaitDuration.Focus();
                    return false;
                }

                if (txtHTTPErrorCodes.Text.Trim().Length.Equals(0))
                {
                    logger.Warn("checkValidation4CRMConnection::Invalid Http Error Codes");
                    MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_VALIDATE_MSG_CRMCONN_HTTPERRCODS, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtHTTPErrorCodes.Focus();
                    return false;
                }

                if (txtFailOverTime.Text.Trim().Length.Equals(0))
                {
                    logger.Warn("checkValidation4CRMConnection::Invalid Failed Over Time");
                    MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_VALIDATE_MSG_CRMCONN_HTTPFALOVER, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtFailOverTime.Focus();
                    return false;
                }
                return true;
        }
        /// <summary>
        /// validation for Load Balancer Connection
        /// </summary>
        /// <returns></returns>
        private bool checkValidation4LoadBalancerConnection()
        {
            logger.Info("ICOMSEditConfiguration::checkValidation4LoadBalancerConnection() called");
            if (txtLBAddress.Text.Trim().Length.Equals(0))
            {
                logger.Warn("checkValidation4LoadBalancerConnection::Invalid Listener Address");
                MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_VALIDATE_MSG_LBCONN_Add, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtPrimaryURL.Focus();
                return false;
            }

            if (txtLBPort.Text.Trim().Length.Equals(0))
            {
                logger.Warn("checkValidation4LoadBalancerConnection::Invalid Listener PORT");
                MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_VALIDATE_MSG_LBCONN_Port, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtBackUpURL.Focus();
                return false;
            }

            try
            {
                IPAddress lbIPAddr = IPAddress.Parse(txtLBAddress.Text.Trim());
                txtLBAddress.Text = lbIPAddr.ToString();
            }
            catch (Exception)
            {
                logger.Warn("checkValidation4Site::Invalid Listener Address");
                MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_VALIDATE_MSG_SITE_LAdd, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtListenerAddress.Focus();
                return false;
            }

            return true;
        }

        private void txtlistenerPort_KeyPress(object sender, KeyPressEventArgs e)
        {          
            if (!((e.KeyChar > 47 && e.KeyChar < 58) || e.KeyChar == 8))
            {
                e.Handled = true;
            }
        }

        private void txtHTTPTimeout_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar > 47 && e.KeyChar < 58) || e.KeyChar == 8))
            {
                e.Handled = true;
            }
        }

        private void txtRetryNumbers_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar > 47 && e.KeyChar < 58) || e.KeyChar == 8))
            {
                e.Handled = true;
            }
        }

        private void txtRetryWaitDuration_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar > 47 && e.KeyChar < 58) || e.KeyChar == 8))
            {
                e.Handled = true;
            }
        }

        private void txtHTTPErrorCodes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar > 47 && e.KeyChar < 58) || e.KeyChar == 8))
            {
                e.Handled = true;
            }
        }

        private void txtFailOverTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar > 47 && e.KeyChar < 58) || e.KeyChar == 8))
            {
                e.Handled = true;
            }
        }

        private void unSavedChangesDetected(object sender, EventArgs e)
        {            
            isDirtyChanged = true;
        }

      
        private void tvICOMSEditConfig_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (!IsIgnoreChanges()) e.Cancel = true;            
        }

        private DialogResult getUnsavedChangesResult()
        {
            DialogResult resultYesNo = MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_VALIDATE_MSG_DIRTY, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            return resultYesNo;
        }

        /// <summary>
        /// Adding new site information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddSite_Click(object sender, EventArgs e)
        {
            logger.Info("ICOMSEditConfiguration::btnAddSite_Click() called");
            try
            {
                if (!checkValidation4Site())
                {
                    logger.Warn("btnAddSite_Click::() Site Validation failed");
                    return;
                }

                if (lstSiteIds.Contains(txtSiteID.Text.Trim(), StringComparer.OrdinalIgnoreCase))
                {
                    logger.Warn("btnAddSite_Click::() Site id Validation failed due to duplication");
                    MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_VALIDATE_MSG_DUP_SITE_ID, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtSiteID.Focus();
                    return;
                }

                if (lstSiteNames.Contains(txtSiteName.Text.Trim(), StringComparer.OrdinalIgnoreCase))
                {
                    logger.Warn("btnAddSite_Click::() Site Name Validation failed due to duplication");
                    MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_VALIDATE_MSG_DUP_SITE_NAME, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtSiteName.Focus();
                    return;
                }

                if(lstSiteIPAndPort.Contains(txtListenerAddress.Text.Trim()+txtlistenerPort.Text.Trim(),StringComparer.OrdinalIgnoreCase))
                {
                    logger.Warn("btnUpdateSite_Click::Listner Address and Port combination already exist in other site");
                    MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_VALIDATE_MSG_DUP_SITE_IP_PORT, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtlistenerPort.Focus();
                    return;
                }

                isDirtyChanged = false;
                createNewSiteXMLNode(txtSiteID.Text.Trim(), txtSiteName.Text.Trim(), txtListenerAddress.Text.Trim(), txtlistenerPort.Text.Trim(),txtTokenName4SiteId.Text.Trim(), getValueForNodeFromComboBox(cmbNatFrmt4cFlag.SelectedIndex), getValueForNodeFromComboBox(cmbCusIdFlag.SelectedIndex),cmbDeviceIdFmt.SelectedItem.ToString());
                TreeNode[] found = tvICOMSEditConfig.Nodes.Find("sites", true);
                TreeNode tn = getTreeNode(txtSiteID.Text.Trim(), txtSiteName.Text.Trim(), "site");
                found[0].Nodes.Add(tn);
                tvICOMSEditConfig.SelectedNode = tn;
                tvICOMSEditConfig.Focus();
                lstSiteIds.Add(txtSiteID.Text.Trim());
                lstSiteNames.Add(txtSiteName.Text.Trim());
                lstSiteIPAndPort.Add(txtListenerAddress.Text.Trim() + txtlistenerPort.Text.Trim());
                logger.Info(string.Format("btnAddSite_Click():: New Site Id added into site id list \"{0}\"", txtSiteID.Text.Trim()));
                logger.Info(string.Format("btnAddSite_Click():: values of Site Id List \"{0}\"", string.Join(",", lstSiteIds.ToArray())));
                logger.Info(string.Format("btnAddSite_Click():: New Site Name added into site Name list \"{0}\"", txtSiteName.Text.Trim()));
                logger.Info(string.Format("btnAddSite_Click():: Values of Site Name List \"{0}\"", string.Join(",", lstSiteNames.ToArray())));
                logger.Info(string.Format("btnAddSite_Click():: New Listner Address & Port combination added into list \"{0}\"", txtSiteName.Text.Trim()));
                logger.Info(string.Format("btnAddSite_Click():: Values of Listner Address & Port combination list \"{0}\"", string.Join(",", lstSiteNames.ToArray())));
                MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_INFO_BTN_ADD_SITE_SUCCESS, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("btnAddSite_Click(): Err Msg: {0}", ex.Message));
                logger.Error(string.Format("btnAddSite_Click(): Stack Trace: {0}", ex.StackTrace));
                logger.Error("ICOMSEditConfiguration::btnAddSite_Click() throwing error");
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// creating new XML node for site
        /// </summary>
        /// <param name="strId"></param>
        /// <param name="strName"></param>
        /// <param name="strlAdd"></param>
        /// <param name="strlPrt"></param>
        /// <param name="strNatFmt4cFlg"></param>
        /// <param name="strCusFlg"></param>
        /// <param name="strDevIdFmt"></param>
        private void createNewSiteXMLNode(string strId, string strName, string strlAdd, string strlPrt, string strToken4Site, string strNatFmt4cFlg, string strCusFlg, string strDevIdFmt)
        {
            logger.Info("ICOMSEditConfiguration::createNewSiteXMLNode() called");
            try
            {
                XmlElement newXNParent;
                XmlElement newXNLChild;

                newXNParent = xmlDoc_ICOMS_Curr.CreateElement("site");
                newXNParent.SetAttribute("id", strId);
                newXNParent.SetAttribute("name", strName);

                newXNLChild = xmlDoc_ICOMS_Curr.CreateElement("listenerAddress");
                newXNLChild.InnerText = strlAdd;
                newXNParent.AppendChild(newXNLChild);

                newXNLChild = xmlDoc_ICOMS_Curr.CreateElement("listenerPort");
                newXNLChild.InnerText = strlPrt;
                newXNParent.AppendChild(newXNLChild);

                newXNLChild = xmlDoc_ICOMS_Curr.CreateElement("TokenName4SiteId");
                newXNLChild.InnerText = strToken4Site;
                newXNParent.AppendChild(newXNLChild);

                newXNLChild = xmlDoc_ICOMS_Curr.CreateElement("CustomerIdFlag");
                newXNLChild.InnerText = strCusFlg;
                newXNParent.AppendChild(newXNLChild);

                newXNLChild = xmlDoc_ICOMS_Curr.CreateElement("DeviceIdFormat");
                newXNLChild.InnerText = strDevIdFmt;
                newXNParent.AppendChild(newXNLChild);

                newXNLChild = xmlDoc_ICOMS_Curr.CreateElement("NativeFormat4cFlag");
                newXNLChild.InnerText = strNatFmt4cFlg;
                newXNParent.AppendChild(newXNLChild);
               
                logger.Info(string.Format("createNewSiteXMLNode():: New XML Node created for Site \"{0}\"", newXNLChild.OuterXml));

                XmlNode nodeList = xmlDoc_ICOMS_Curr.SelectSingleNode("/serviceconfiguration/sites");
                nodeList.AppendChild(newXNParent);
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("createNewSiteXMLNode(): Err Msg : {0}", ex.Message));
                logger.Error(string.Format("createNewSiteXMLNode(): Stack Trace: {0}", ex.StackTrace));
                logger.Error("ICOMSEditConfiguration::createNewSiteXMLNode() throwing error");
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// deleting a site from XML
        /// </summary>
        /// <param name="strId"></param>
        /// <param name="strName"></param>
        private void removeSiteXMLNode(string strId, string strName)
        {
            try
            {
                logger.Info("ICOMSEditConfiguration::removeSiteXMLNode() called");
                XmlNode siteRemNode = xmlDoc_ICOMS_Curr.SelectSingleNode(string.Format("/serviceconfiguration/sites/site[@id=\"{0}\" and @name=\"{1}\"]", strId, strName));
                logger.Info(string.Format("removeSiteXMLNode():: Removed XML Node for selected Site \"{0}\"", siteRemNode.OuterXml));
                siteRemNode.ParentNode.RemoveChild(siteRemNode);
                lstSiteIds.Remove(strEditNodeSiteId);
                lstSiteNames.Remove(strEditNodeSiteName);
                lstSiteIPAndPort.Remove(strEditNodeSiteIPandPort);
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("removeSiteXMLNode(): Err Msg: {0}", ex.Message));
                logger.Error(string.Format("removeSiteXMLNode(): Stack Trace: {0}", ex.StackTrace));
                logger.Error("ICOMSEditConfiguration::removeSiteXMLNode() throwing error");
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// deleting site from XML & tree view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveSite_Click(object sender, EventArgs e)
        {
            logger.Info("ICOMSEditConfiguration::btnRemoveSite_Click() called");
            try
            {
                DialogResult resultYesNo = MessageBox.Show(string.Format(ServiceConstantsManager.CONST_ICOMS_WARN_BTN_REM_SITE, tvICOMSEditConfig.SelectedNode.Text), ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (resultYesNo.Equals(DialogResult.Yes))
                {
                    removeSiteXMLNode(tvICOMSEditConfig.SelectedNode.Name, tvICOMSEditConfig.SelectedNode.Text);
                    logger.Info(string.Format("btnRemoveSite_Click():: Removed Tree Node for selected Site \"{0}\"", tvICOMSEditConfig.SelectedNode.Text));
                    tvICOMSEditConfig.SelectedNode.Remove();
                   // MessageBox.Show(ServiceConstantsManager.CONST_ICOMS_INFO_BTN_REM_SITE_SUCCESS, ServiceConstantsManager.CONST_ICOMS_MESSAGEBOX_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                isDirtyChanged = false;
            }
            catch (Exception ex)
            {
                logger.Error(string.Format("btnRemoveSite_Click(): Err Msg : {0}", ex.Message));
                logger.Error(string.Format("btnRemoveSite_Click(): Stack Trace: {0}", ex.StackTrace));
                logger.Error("ICOMSEditConfiguration::btnRemoveSite_Click() throwing error");
                MessageBox.Show(ex.Message);               
            }         
        }

        private void tvICOMSEditConfig_Enter(object sender, EventArgs e)
        {
            setHelpMessage4Controls(ServiceConstantsManager.CONST_ICOMS_HELP_TV_SELECT);
        }

        private void tvICOMSEditConfig_Leave(object sender, EventArgs e)
        {
            setHelpMessage4Empty();
        }

        private void btnAddSite_Enter(object sender, EventArgs e)
        {
            setHelpMessage4Controls(ServiceConstantsManager.CONST_ICOMS_HELP_BTN_ADDSITE);
        }

        private void btnUpdateSite_Enter(object sender, EventArgs e)
        {
            setHelpMessage4Controls(ServiceConstantsManager.CONST_ICOMS_HELP_BTN_UPDSITE);
        }

        private void btnRemoveSite_Enter(object sender, EventArgs e)
        {
            setHelpMessage4Controls(ServiceConstantsManager.CONST_ICOMS_HELP_BTN_REMSITE);
        }

        private void btnCRMConnUpdate_Enter(object sender, EventArgs e)
        {
            setHelpMessage4Controls(ServiceConstantsManager.CONST_ICOMS_HELP_BTN_UPDCRMCON);
        }

        private void btnAddSite_Leave(object sender, EventArgs e)
        {
            setHelpMessage4Empty();
        }

        private void btnUpdateSite_Leave(object sender, EventArgs e)
        {
            setHelpMessage4Empty();
        }

        private void btnRemoveSite_Leave(object sender, EventArgs e)
        {
            setHelpMessage4Empty();
        }

        private void btnCRMConnUpdate_Leave(object sender, EventArgs e)
        {
            setHelpMessage4Empty();
        }

        private void btnSiteConfigDefault_Enter(object sender, EventArgs e)
        {
            setHelpMessage4Controls(ServiceConstantsManager.CONST_ICOMS_HELP_BTN_LOADDDEF);
        }

        private void btnSiteConfigSave_Enter(object sender, EventArgs e)
        {
            setHelpMessage4Controls(ServiceConstantsManager.CONST_ICOMS_HELP_BTN_SAVE);
        }

        private void btnSiteConfigDefault_Leave(object sender, EventArgs e)
        {
            setHelpMessage4Empty();
        }

        private void btnSiteConfigSave_Leave(object sender, EventArgs e)
        {
            setHelpMessage4Empty();
        }

        private void btnLBConnUpdate_Enter(object sender, EventArgs e)
        {
            setHelpMessage4Controls(ServiceConstantsManager.CONST_ICOMS_HELP_BTN_UPDLBCON);
        }

        private void btnLBConnUpdate_Leave(object sender, EventArgs e)
        {
            setHelpMessage4Empty();
        }
        /// <summary>
        /// clearing all the fields
        /// </summary>
        private void clearAllFieldsOfSite()
        {
            logger.Info("ICOMSEditConfiguration::clearAllFieldsOfSite() called");
            txtSiteID.Text = "";
            txtSiteName.Text = "";
            txtListenerAddress.Text = "";
            txtlistenerPort.Text = "";
            txtTokenName4SiteId.Text = "";
            cmbCusIdFlag.SelectedIndex = 0;
            cmbDeviceIdFmt.SelectedIndex = 0;
			cmbNatFrmt4cFlag.SelectedIndex = 0;
            isDirtyChanged = false;
        }
        
        /// <summary>
        /// Gets confirmation from user to continue without saving the changes
        /// </summary>
        /// <returns></returns>
        private bool IsIgnoreChanges()
        {
           if (isDirtyChanged)
            {
                if (getUnsavedChangesResult().Equals(DialogResult.No))
                {
                    return false;
                }
                else
                {
                    isDirtyChanged = false;
                }
            }
           return true;
    	}
     private void cmbDeviceIdFmt_Leave(object sender, EventArgs e)
     {
     	setHelpMessage4Empty();
     }

     private void cmbDeviceIdFmt_Enter(object sender, EventArgs e)
     {
     	setHelpMessage4Controls(ServiceConstantsManager.CONST_ICOMS_HELP_SITE_DEVFORMATID);         
     }

     private void txtSiteIdTokenName_Enter(object sender, EventArgs e)
     {
         setHelpMessage4Controls(ServiceConstantsManager.CONST_ICOMS_HELP_SITE_TOKEN_NAME4SID);
     }

     private void txtSiteIdTokenName_Leave(object sender, EventArgs e)
     {
         setHelpMessage4Empty();
     }
            
   }
}
