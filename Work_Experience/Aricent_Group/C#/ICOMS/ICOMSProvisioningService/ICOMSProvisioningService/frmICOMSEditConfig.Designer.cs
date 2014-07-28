namespace ICOMSProvisioningService
{
    partial class frmICOMSEditConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("site1");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("site2");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("site3");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("site4");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("site5");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("sites", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("CRMConnection");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Load Balancer");
            this.tvICOMSEditConfig = new System.Windows.Forms.TreeView();
            this.grpEditConfiguration = new System.Windows.Forms.GroupBox();
            this.grpDefSaveConfig = new System.Windows.Forms.GroupBox();
            this.btnSiteConfigSave = new System.Windows.Forms.Button();
            this.btnSiteConfigDefault = new System.Windows.Forms.Button();
            this.grpSiteARU = new System.Windows.Forms.GroupBox();
            this.txtTokenName4SiteId = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbDeviceIdFmt = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtSiteName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbCusIdFlag = new System.Windows.Forms.ComboBox();
            this.cmbNatFrmt4cFlag = new System.Windows.Forms.ComboBox();
            this.btnUpdateSite = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtlistenerPort = new System.Windows.Forms.TextBox();
            this.lblListnerPort = new System.Windows.Forms.Label();
            this.txtListenerAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSiteID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddSite = new System.Windows.Forms.Button();
            this.btnRemoveSite = new System.Windows.Forms.Button();
            this.grpCRMConnection = new System.Windows.Forms.GroupBox();
            this.txtBackUpCCI4cURL = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtPrimaryCCI4cURL = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtFailOverTime = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtHTTPErrorCodes = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnCRMConnUpdate = new System.Windows.Forms.Button();
            this.txtRetryWaitDuration = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRetryNumbers = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtHTTPTimeout = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBackUpURL = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPrimaryURL = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.grpLBConnection = new System.Windows.Forms.GroupBox();
            this.btnLBConnUpdate = new System.Windows.Forms.Button();
            this.txtLBPort = new System.Windows.Forms.TextBox();
            this.Label_LBPort = new System.Windows.Forms.Label();
            this.txtLBAddress = new System.Windows.Forms.TextBox();
            this.Label_LBAddr = new System.Windows.Forms.Label();
            this.txtHelpInfo = new System.Windows.Forms.TextBox();
            this.grpEditConfiguration.SuspendLayout();
            this.grpDefSaveConfig.SuspendLayout();
            this.grpSiteARU.SuspendLayout();
            this.grpCRMConnection.SuspendLayout();
            this.grpLBConnection.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvICOMSEditConfig
            // 
            this.tvICOMSEditConfig.Location = new System.Drawing.Point(6, 19);
            this.tvICOMSEditConfig.Name = "tvICOMSEditConfig";
            treeNode1.Name = "Node2";
            treeNode1.Text = "site1";
            treeNode2.Name = "Node4";
            treeNode2.Text = "site2";
            treeNode3.Name = "Node5";
            treeNode3.Text = "site3";
            treeNode4.Name = "Node6";
            treeNode4.Text = "site4";
            treeNode5.Name = "Node7";
            treeNode5.Text = "site5";
            treeNode6.Name = "Node0";
            treeNode6.Text = "sites";
            treeNode7.Name = "Node8";
            treeNode7.Text = "CRMConnection";
            treeNode8.Name = "Node9";
            treeNode8.Text = "Load Balancer";
            this.tvICOMSEditConfig.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7,
            treeNode8});
            this.tvICOMSEditConfig.Size = new System.Drawing.Size(191, 365);
            this.tvICOMSEditConfig.TabIndex = 0;
            this.tvICOMSEditConfig.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvICOMSEditConfig_BeforeSelect);
            this.tvICOMSEditConfig.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvICOMSEditConfig_AfterSelect);
            this.tvICOMSEditConfig.Enter += new System.EventHandler(this.tvICOMSEditConfig_Enter);
            this.tvICOMSEditConfig.Leave += new System.EventHandler(this.tvICOMSEditConfig_Leave);
            // 
            // grpEditConfiguration
            // 
            this.grpEditConfiguration.Controls.Add(this.grpDefSaveConfig);
            this.grpEditConfiguration.Controls.Add(this.tvICOMSEditConfig);
            this.grpEditConfiguration.Controls.Add(this.grpLBConnection);
            this.grpEditConfiguration.Controls.Add(this.grpSiteARU);
            this.grpEditConfiguration.Controls.Add(this.grpCRMConnection);
            this.grpEditConfiguration.Location = new System.Drawing.Point(12, 12);
            this.grpEditConfiguration.Name = "grpEditConfiguration";
            this.grpEditConfiguration.Size = new System.Drawing.Size(603, 451);
            this.grpEditConfiguration.TabIndex = 4;
            this.grpEditConfiguration.TabStop = false;
            this.grpEditConfiguration.Text = "Edit Configuration";
            // 
            // grpDefSaveConfig
            // 
            this.grpDefSaveConfig.Controls.Add(this.btnSiteConfigSave);
            this.grpDefSaveConfig.Controls.Add(this.btnSiteConfigDefault);
            this.grpDefSaveConfig.Location = new System.Drawing.Point(203, 344);
            this.grpDefSaveConfig.Name = "grpDefSaveConfig";
            this.grpDefSaveConfig.Size = new System.Drawing.Size(393, 56);
            this.grpDefSaveConfig.TabIndex = 21;
            this.grpDefSaveConfig.TabStop = false;
            this.grpDefSaveConfig.Text = "Configuration";
            // 
            // btnSiteConfigSave
            // 
            this.btnSiteConfigSave.Location = new System.Drawing.Point(196, 19);
            this.btnSiteConfigSave.Name = "btnSiteConfigSave";
            this.btnSiteConfigSave.Size = new System.Drawing.Size(80, 30);
            this.btnSiteConfigSave.TabIndex = 23;
            this.btnSiteConfigSave.Text = "Save";
            this.btnSiteConfigSave.UseVisualStyleBackColor = true;
            this.btnSiteConfigSave.Click += new System.EventHandler(this.btnSiteConfigSave_Click);
            this.btnSiteConfigSave.Enter += new System.EventHandler(this.btnSiteConfigSave_Enter);
            this.btnSiteConfigSave.Leave += new System.EventHandler(this.btnSiteConfigSave_Leave);
            // 
            // btnSiteConfigDefault
            // 
            this.btnSiteConfigDefault.Location = new System.Drawing.Point(110, 19);
            this.btnSiteConfigDefault.Name = "btnSiteConfigDefault";
            this.btnSiteConfigDefault.Size = new System.Drawing.Size(80, 30);
            this.btnSiteConfigDefault.TabIndex = 22;
            this.btnSiteConfigDefault.Text = "Load Default";
            this.btnSiteConfigDefault.UseVisualStyleBackColor = true;
            this.btnSiteConfigDefault.Click += new System.EventHandler(this.btnSiteConfigDefault_Click);
            this.btnSiteConfigDefault.Enter += new System.EventHandler(this.btnSiteConfigDefault_Enter);
            this.btnSiteConfigDefault.Leave += new System.EventHandler(this.btnSiteConfigDefault_Leave);
            // 
            // grpSiteARU
            // 
            this.grpSiteARU.Controls.Add(this.txtTokenName4SiteId);
            this.grpSiteARU.Controls.Add(this.label14);
            this.grpSiteARU.Controls.Add(this.cmbDeviceIdFmt);
            this.grpSiteARU.Controls.Add(this.label13);
            this.grpSiteARU.Controls.Add(this.txtSiteName);
            this.grpSiteARU.Controls.Add(this.label3);
            this.grpSiteARU.Controls.Add(this.cmbCusIdFlag);
            this.grpSiteARU.Controls.Add(this.cmbNatFrmt4cFlag);
            this.grpSiteARU.Controls.Add(this.btnUpdateSite);
            this.grpSiteARU.Controls.Add(this.label5);
            this.grpSiteARU.Controls.Add(this.label4);
            this.grpSiteARU.Controls.Add(this.txtlistenerPort);
            this.grpSiteARU.Controls.Add(this.lblListnerPort);
            this.grpSiteARU.Controls.Add(this.txtListenerAddress);
            this.grpSiteARU.Controls.Add(this.label2);
            this.grpSiteARU.Controls.Add(this.txtSiteID);
            this.grpSiteARU.Controls.Add(this.label1);
            this.grpSiteARU.Controls.Add(this.btnAddSite);
            this.grpSiteARU.Controls.Add(this.btnRemoveSite);
            this.grpSiteARU.Location = new System.Drawing.Point(203, 19);
            this.grpSiteARU.Name = "grpSiteARU";
            this.grpSiteARU.Size = new System.Drawing.Size(393, 313);
            this.grpSiteARU.TabIndex = 1;
            this.grpSiteARU.TabStop = false;
            this.grpSiteARU.Text = "Site";
            this.grpSiteARU.Visible = false;
            // 
            // txtTokenName4SiteId
            // 
            this.txtTokenName4SiteId.Location = new System.Drawing.Point(134, 119);
            this.txtTokenName4SiteId.MaxLength = 5;
            this.txtTokenName4SiteId.Name = "txtTokenName4SiteId";
            this.txtTokenName4SiteId.Size = new System.Drawing.Size(84, 20);
            this.txtTokenName4SiteId.TabIndex = 34;
            this.txtTokenName4SiteId.TextChanged += new System.EventHandler(this.unSavedChangesDetected);
            this.txtTokenName4SiteId.Enter += new System.EventHandler(this.txtSiteIdTokenName_Enter);
            this.txtTokenName4SiteId.Leave += new System.EventHandler(this.txtSiteIdTokenName_Leave);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(14, 122);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(99, 13);
            this.label14.TabIndex = 33;
            this.label14.Text = "TokenName4SiteId";
            // 
            // cmbDeviceIdFmt
            // 
            this.cmbDeviceIdFmt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeviceIdFmt.FormattingEnabled = true;
            this.cmbDeviceIdFmt.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cmbDeviceIdFmt.Location = new System.Drawing.Point(134, 172);
            this.cmbDeviceIdFmt.Name = "cmbDeviceIdFmt";
            this.cmbDeviceIdFmt.Size = new System.Drawing.Size(82, 21);
            this.cmbDeviceIdFmt.TabIndex = 7;
            this.cmbDeviceIdFmt.SelectedIndexChanged += new System.EventHandler(this.unSavedChangesDetected);
            this.cmbDeviceIdFmt.Enter += new System.EventHandler(this.cmbDeviceIdFmt_Enter);
            this.cmbDeviceIdFmt.Leave += new System.EventHandler(this.cmbDeviceIdFmt_Leave);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(14, 175);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(82, 13);
            this.label13.TabIndex = 31;
            this.label13.Text = "DeviceIdFormat";
            // 
            // txtSiteName
            // 
            this.txtSiteName.Location = new System.Drawing.Point(134, 42);
            this.txtSiteName.MaxLength = 30;
            this.txtSiteName.Name = "txtSiteName";
            this.txtSiteName.Size = new System.Drawing.Size(158, 20);
            this.txtSiteName.TabIndex = 3;
            this.txtSiteName.TextChanged += new System.EventHandler(this.unSavedChangesDetected);
            this.txtSiteName.Enter += new System.EventHandler(this.txtSiteName_Enter);
            this.txtSiteName.Leave += new System.EventHandler(this.txtSiteName_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "SiteName";
            // 
            // cmbCusIdFlag
            // 
            this.cmbCusIdFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCusIdFlag.FormattingEnabled = true;
            this.cmbCusIdFlag.Items.AddRange(new object[] {
            "False",
            "True"});
            this.cmbCusIdFlag.Location = new System.Drawing.Point(134, 145);
            this.cmbCusIdFlag.Name = "cmbCusIdFlag";
            this.cmbCusIdFlag.Size = new System.Drawing.Size(82, 21);
            this.cmbCusIdFlag.TabIndex = 6;
            this.cmbCusIdFlag.SelectedIndexChanged += new System.EventHandler(this.unSavedChangesDetected);
            this.cmbCusIdFlag.Enter += new System.EventHandler(this.cmbCusIdFlag_Enter);
            this.cmbCusIdFlag.Leave += new System.EventHandler(this.cmbCusIdFlag_Leave);
            // 
            // cmbNatFrmt4cFlag
            // 
            this.cmbNatFrmt4cFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNatFrmt4cFlag.FormattingEnabled = true;
            this.cmbNatFrmt4cFlag.Items.AddRange(new object[] {
            "False",
            "True"});
            this.cmbNatFrmt4cFlag.Location = new System.Drawing.Point(134, 199);
            this.cmbNatFrmt4cFlag.Name = "cmbNatFrmt4cFlag";
            this.cmbNatFrmt4cFlag.Size = new System.Drawing.Size(82, 21);
            this.cmbNatFrmt4cFlag.TabIndex = 8;
            this.cmbNatFrmt4cFlag.SelectedIndexChanged += new System.EventHandler(this.unSavedChangesDetected);
            this.cmbNatFrmt4cFlag.Enter += new System.EventHandler(this.cmbNatFrmt4cFlag_Enter);
            this.cmbNatFrmt4cFlag.Leave += new System.EventHandler(this.cmbNatFrmt4cFlag_Leave);
            // 
            // btnUpdateSite
            // 
            this.btnUpdateSite.Location = new System.Drawing.Point(148, 245);
            this.btnUpdateSite.Name = "btnUpdateSite";
            this.btnUpdateSite.Size = new System.Drawing.Size(70, 30);
            this.btnUpdateSite.TabIndex = 10;
            this.btnUpdateSite.Text = "Update";
            this.btnUpdateSite.UseVisualStyleBackColor = true;
            this.btnUpdateSite.Click += new System.EventHandler(this.btnUpdateSite_Click);
            this.btnUpdateSite.Enter += new System.EventHandler(this.btnUpdateSite_Enter);
            this.btnUpdateSite.Leave += new System.EventHandler(this.btnUpdateSite_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "CustomerIdFlag";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 202);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "NativeFormat4cFlag";
            // 
            // txtlistenerPort
            // 
            this.txtlistenerPort.Location = new System.Drawing.Point(134, 94);
            this.txtlistenerPort.MaxLength = 5;
            this.txtlistenerPort.Name = "txtlistenerPort";
            this.txtlistenerPort.Size = new System.Drawing.Size(84, 20);
            this.txtlistenerPort.TabIndex = 5;
            this.txtlistenerPort.TextChanged += new System.EventHandler(this.unSavedChangesDetected);
            this.txtlistenerPort.Enter += new System.EventHandler(this.txtlistenerPort_Enter);
            this.txtlistenerPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtlistenerPort_KeyPress);
            this.txtlistenerPort.Leave += new System.EventHandler(this.txtlistenerPort_Leave);
            // 
            // lblListnerPort
            // 
            this.lblListnerPort.AutoSize = true;
            this.lblListnerPort.Location = new System.Drawing.Point(14, 97);
            this.lblListnerPort.Name = "lblListnerPort";
            this.lblListnerPort.Size = new System.Drawing.Size(59, 13);
            this.lblListnerPort.TabIndex = 22;
            this.lblListnerPort.Text = "listenerPort";
            // 
            // txtListenerAddress
            // 
            this.txtListenerAddress.Location = new System.Drawing.Point(134, 68);
            this.txtListenerAddress.MaxLength = 50;
            this.txtListenerAddress.Name = "txtListenerAddress";
            this.txtListenerAddress.Size = new System.Drawing.Size(158, 20);
            this.txtListenerAddress.TabIndex = 4;
            this.txtListenerAddress.TextChanged += new System.EventHandler(this.unSavedChangesDetected);
            this.txtListenerAddress.Enter += new System.EventHandler(this.txtListenerAddress_Enter);
            this.txtListenerAddress.Leave += new System.EventHandler(this.txtListenerAddress_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "listenerAddress";
            // 
            // txtSiteID
            // 
            this.txtSiteID.Location = new System.Drawing.Point(134, 16);
            this.txtSiteID.MaxLength = 3;
            this.txtSiteID.Name = "txtSiteID";
            this.txtSiteID.Size = new System.Drawing.Size(158, 20);
            this.txtSiteID.TabIndex = 2;
            this.txtSiteID.TextChanged += new System.EventHandler(this.unSavedChangesDetected);
            this.txtSiteID.Enter += new System.EventHandler(this.txtSiteID_Enter);
            this.txtSiteID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSiteID_KeyPress);
            this.txtSiteID.Leave += new System.EventHandler(this.txtSiteID_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "SiteId";
            // 
            // btnAddSite
            // 
            this.btnAddSite.Location = new System.Drawing.Point(71, 245);
            this.btnAddSite.Name = "btnAddSite";
            this.btnAddSite.Size = new System.Drawing.Size(70, 30);
            this.btnAddSite.TabIndex = 9;
            this.btnAddSite.Text = "Add";
            this.btnAddSite.UseVisualStyleBackColor = true;
            this.btnAddSite.Click += new System.EventHandler(this.btnAddSite_Click);
            this.btnAddSite.Enter += new System.EventHandler(this.btnAddSite_Enter);
            this.btnAddSite.Leave += new System.EventHandler(this.btnAddSite_Leave);
            // 
            // btnRemoveSite
            // 
            this.btnRemoveSite.Location = new System.Drawing.Point(224, 245);
            this.btnRemoveSite.Name = "btnRemoveSite";
            this.btnRemoveSite.Size = new System.Drawing.Size(70, 30);
            this.btnRemoveSite.TabIndex = 11;
            this.btnRemoveSite.Text = "Remove";
            this.btnRemoveSite.UseVisualStyleBackColor = true;
            this.btnRemoveSite.Click += new System.EventHandler(this.btnRemoveSite_Click);
            this.btnRemoveSite.Enter += new System.EventHandler(this.btnRemoveSite_Enter);
            this.btnRemoveSite.Leave += new System.EventHandler(this.btnRemoveSite_Leave);
            // 
            // grpCRMConnection
            // 
            this.grpCRMConnection.Controls.Add(this.txtBackUpCCI4cURL);
            this.grpCRMConnection.Controls.Add(this.label15);
            this.grpCRMConnection.Controls.Add(this.txtPrimaryCCI4cURL);
            this.grpCRMConnection.Controls.Add(this.label16);
            this.grpCRMConnection.Controls.Add(this.txtFailOverTime);
            this.grpCRMConnection.Controls.Add(this.label12);
            this.grpCRMConnection.Controls.Add(this.txtHTTPErrorCodes);
            this.grpCRMConnection.Controls.Add(this.label11);
            this.grpCRMConnection.Controls.Add(this.btnCRMConnUpdate);
            this.grpCRMConnection.Controls.Add(this.txtRetryWaitDuration);
            this.grpCRMConnection.Controls.Add(this.label6);
            this.grpCRMConnection.Controls.Add(this.txtRetryNumbers);
            this.grpCRMConnection.Controls.Add(this.label7);
            this.grpCRMConnection.Controls.Add(this.txtHTTPTimeout);
            this.grpCRMConnection.Controls.Add(this.label8);
            this.grpCRMConnection.Controls.Add(this.txtBackUpURL);
            this.grpCRMConnection.Controls.Add(this.label9);
            this.grpCRMConnection.Controls.Add(this.txtPrimaryURL);
            this.grpCRMConnection.Controls.Add(this.label10);
            this.grpCRMConnection.Location = new System.Drawing.Point(203, 18);
            this.grpCRMConnection.Name = "grpCRMConnection";
            this.grpCRMConnection.Size = new System.Drawing.Size(393, 320);
            this.grpCRMConnection.TabIndex = 12;
            this.grpCRMConnection.TabStop = false;
            this.grpCRMConnection.Text = "CRMConnection";
            this.grpCRMConnection.Visible = false;
            // 
            // txtBackUpCCI4cURL
            // 
            this.txtBackUpCCI4cURL.Location = new System.Drawing.Point(120, 107);
            this.txtBackUpCCI4cURL.MaxLength = 75;
            this.txtBackUpCCI4cURL.Name = "txtBackUpCCI4cURL";
            this.txtBackUpCCI4cURL.Size = new System.Drawing.Size(267, 20);
            this.txtBackUpCCI4cURL.TabIndex = 33;
            this.txtBackUpCCI4cURL.TextChanged += new System.EventHandler(this.unSavedChangesDetected);
            this.txtBackUpCCI4cURL.Enter += new System.EventHandler(this.txtPrimaryCCI4cURL_Enter);
            this.txtBackUpCCI4cURL.Leave += new System.EventHandler(this.txtPrimaryCCI4cURL_Leave);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(16, 107);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(95, 13);
            this.label15.TabIndex = 35;
            this.label15.Text = "BackupCCI4cURL";
            // 
            // txtPrimaryCCI4cURL
            // 
            this.txtPrimaryCCI4cURL.Location = new System.Drawing.Point(120, 81);
            this.txtPrimaryCCI4cURL.MaxLength = 75;
            this.txtPrimaryCCI4cURL.Name = "txtPrimaryCCI4cURL";
            this.txtPrimaryCCI4cURL.Size = new System.Drawing.Size(267, 20);
            this.txtPrimaryCCI4cURL.TabIndex = 32;
            this.txtPrimaryCCI4cURL.TextChanged += new System.EventHandler(this.unSavedChangesDetected);
            this.txtPrimaryCCI4cURL.Enter += new System.EventHandler(this.txtPrimaryCCI4cURL_Enter);
            this.txtPrimaryCCI4cURL.Leave += new System.EventHandler(this.txtPrimaryCCI4cURL_Leave);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(16, 81);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(92, 13);
            this.label16.TabIndex = 34;
            this.label16.Text = "PrimaryCCI4cURL";
            // 
            // txtFailOverTime
            // 
            this.txtFailOverTime.Location = new System.Drawing.Point(120, 238);
            this.txtFailOverTime.MaxLength = 5;
            this.txtFailOverTime.Name = "txtFailOverTime";
            this.txtFailOverTime.Size = new System.Drawing.Size(131, 20);
            this.txtFailOverTime.TabIndex = 19;
            this.txtFailOverTime.TextChanged += new System.EventHandler(this.unSavedChangesDetected);
            this.txtFailOverTime.Enter += new System.EventHandler(this.txtFailOverTime_Enter);
            this.txtFailOverTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFailOverTime_KeyPress);
            this.txtFailOverTime.Leave += new System.EventHandler(this.txtFailOverTime_Leave);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(16, 238);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 13);
            this.label12.TabIndex = 31;
            this.label12.Text = "FailoverTime";
            // 
            // txtHTTPErrorCodes
            // 
            this.txtHTTPErrorCodes.Location = new System.Drawing.Point(120, 211);
            this.txtHTTPErrorCodes.MaxLength = 20;
            this.txtHTTPErrorCodes.Name = "txtHTTPErrorCodes";
            this.txtHTTPErrorCodes.Size = new System.Drawing.Size(131, 20);
            this.txtHTTPErrorCodes.TabIndex = 18;
            this.txtHTTPErrorCodes.TextChanged += new System.EventHandler(this.unSavedChangesDetected);
            this.txtHTTPErrorCodes.Enter += new System.EventHandler(this.txtHTTPErrorCodes_Enter);
            this.txtHTTPErrorCodes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHTTPErrorCodes_KeyPress);
            this.txtHTTPErrorCodes.Resize += new System.EventHandler(this.txtHTTPErrorCodes_Resize);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(16, 211);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(88, 13);
            this.label11.TabIndex = 29;
            this.label11.Text = "HTTPErrorCodes";
            // 
            // btnCRMConnUpdate
            // 
            this.btnCRMConnUpdate.Location = new System.Drawing.Point(151, 272);
            this.btnCRMConnUpdate.Name = "btnCRMConnUpdate";
            this.btnCRMConnUpdate.Size = new System.Drawing.Size(70, 30);
            this.btnCRMConnUpdate.TabIndex = 20;
            this.btnCRMConnUpdate.Text = "Update";
            this.btnCRMConnUpdate.UseVisualStyleBackColor = true;
            this.btnCRMConnUpdate.Click += new System.EventHandler(this.btnCRMConnUpdate_Click);
            this.btnCRMConnUpdate.Enter += new System.EventHandler(this.btnCRMConnUpdate_Enter);
            this.btnCRMConnUpdate.Leave += new System.EventHandler(this.btnCRMConnUpdate_Leave);
            // 
            // txtRetryWaitDuration
            // 
            this.txtRetryWaitDuration.Location = new System.Drawing.Point(120, 185);
            this.txtRetryWaitDuration.MaxLength = 5;
            this.txtRetryWaitDuration.Name = "txtRetryWaitDuration";
            this.txtRetryWaitDuration.Size = new System.Drawing.Size(131, 20);
            this.txtRetryWaitDuration.TabIndex = 17;
            this.txtRetryWaitDuration.TextChanged += new System.EventHandler(this.unSavedChangesDetected);
            this.txtRetryWaitDuration.Enter += new System.EventHandler(this.txtRetryWaitDuration_Enter);
            this.txtRetryWaitDuration.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRetryWaitDuration_KeyPress);
            this.txtRetryWaitDuration.Leave += new System.EventHandler(this.txtRetryWaitDuration_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 185);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "RetryWaitDuration";
            // 
            // txtRetryNumbers
            // 
            this.txtRetryNumbers.Location = new System.Drawing.Point(120, 159);
            this.txtRetryNumbers.MaxLength = 2;
            this.txtRetryNumbers.Name = "txtRetryNumbers";
            this.txtRetryNumbers.Size = new System.Drawing.Size(131, 20);
            this.txtRetryNumbers.TabIndex = 16;
            this.txtRetryNumbers.TextChanged += new System.EventHandler(this.unSavedChangesDetected);
            this.txtRetryNumbers.Enter += new System.EventHandler(this.txtRetryNumbers_Enter);
            this.txtRetryNumbers.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRetryNumbers_KeyPress);
            this.txtRetryNumbers.Leave += new System.EventHandler(this.txtRetryNumbers_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 159);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "RetryNumbers";
            // 
            // txtHTTPTimeout
            // 
            this.txtHTTPTimeout.Location = new System.Drawing.Point(120, 133);
            this.txtHTTPTimeout.MaxLength = 5;
            this.txtHTTPTimeout.Name = "txtHTTPTimeout";
            this.txtHTTPTimeout.Size = new System.Drawing.Size(131, 20);
            this.txtHTTPTimeout.TabIndex = 15;
            this.txtHTTPTimeout.TextChanged += new System.EventHandler(this.unSavedChangesDetected);
            this.txtHTTPTimeout.Enter += new System.EventHandler(this.txtHTTPTimeout_Enter);
            this.txtHTTPTimeout.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHTTPTimeout_KeyPress);
            this.txtHTTPTimeout.Leave += new System.EventHandler(this.txtHTTPTimeout_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 133);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 22;
            this.label8.Text = "HTTPtimeout";
            // 
            // txtBackUpURL
            // 
            this.txtBackUpURL.Location = new System.Drawing.Point(120, 54);
            this.txtBackUpURL.MaxLength = 75;
            this.txtBackUpURL.Name = "txtBackUpURL";
            this.txtBackUpURL.Size = new System.Drawing.Size(267, 20);
            this.txtBackUpURL.TabIndex = 14;
            this.txtBackUpURL.TextChanged += new System.EventHandler(this.unSavedChangesDetected);
            this.txtBackUpURL.Enter += new System.EventHandler(this.txtBackUpURL_Enter);
            this.txtBackUpURL.Leave += new System.EventHandler(this.txtBackUpURL_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "BackupURL";
            // 
            // txtPrimaryURL
            // 
            this.txtPrimaryURL.Location = new System.Drawing.Point(120, 28);
            this.txtPrimaryURL.MaxLength = 75;
            this.txtPrimaryURL.Name = "txtPrimaryURL";
            this.txtPrimaryURL.Size = new System.Drawing.Size(267, 20);
            this.txtPrimaryURL.TabIndex = 13;
            this.txtPrimaryURL.TextChanged += new System.EventHandler(this.unSavedChangesDetected);
            this.txtPrimaryURL.Enter += new System.EventHandler(this.txtPrimaryURL_Enter);
            this.txtPrimaryURL.Leave += new System.EventHandler(this.txtPrimaryURL_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "PrimaryURL";
            // 
            // grpLBConnection
            // 
            this.grpLBConnection.Controls.Add(this.btnLBConnUpdate);
            this.grpLBConnection.Controls.Add(this.txtLBPort);
            this.grpLBConnection.Controls.Add(this.Label_LBPort);
            this.grpLBConnection.Controls.Add(this.txtLBAddress);
            this.grpLBConnection.Controls.Add(this.Label_LBAddr);
            this.grpLBConnection.Location = new System.Drawing.Point(203, 19);
            this.grpLBConnection.Name = "grpLBConnection";
            this.grpLBConnection.Size = new System.Drawing.Size(398, 319);
            this.grpLBConnection.TabIndex = 35;
            this.grpLBConnection.TabStop = false;
            this.grpLBConnection.Text = "Load Balancer Connection";
            // 
            // btnLBConnUpdate
            // 
            this.btnLBConnUpdate.Location = new System.Drawing.Point(149, 134);
            this.btnLBConnUpdate.Name = "btnLBConnUpdate";
            this.btnLBConnUpdate.Size = new System.Drawing.Size(70, 30);
            this.btnLBConnUpdate.TabIndex = 29;
            this.btnLBConnUpdate.Text = "Update";
            this.btnLBConnUpdate.UseVisualStyleBackColor = true;
            this.btnLBConnUpdate.Click += new System.EventHandler(this.btnLBConnUpdate_Click);
            this.btnLBConnUpdate.Enter += new System.EventHandler(this.btnLBConnUpdate_Enter);
            this.btnLBConnUpdate.Leave += new System.EventHandler(this.btnLBConnUpdate_Leave);
            // 
            // txtLBPort
            // 
            this.txtLBPort.Location = new System.Drawing.Point(145, 65);
            this.txtLBPort.MaxLength = 5;
            this.txtLBPort.Name = "txtLBPort";
            this.txtLBPort.Size = new System.Drawing.Size(131, 20);
            this.txtLBPort.TabIndex = 26;
            this.txtLBPort.TextChanged += new System.EventHandler(this.unSavedChangesDetected);
            this.txtLBPort.Enter += new System.EventHandler(this.txtLBPort_Enter);
            this.txtLBPort.Leave += new System.EventHandler(this.txtLBPort_Leave);
            // 
            // Label_LBPort
            // 
            this.Label_LBPort.AutoSize = true;
            this.Label_LBPort.Location = new System.Drawing.Point(16, 61);
            this.Label_LBPort.Name = "Label_LBPort";
            this.Label_LBPort.Size = new System.Drawing.Size(63, 13);
            this.Label_LBPort.TabIndex = 28;
            this.Label_LBPort.Text = "ListenerPort";
            // 
            // txtLBAddress
            // 
            this.txtLBAddress.Location = new System.Drawing.Point(145, 35);
            this.txtLBAddress.MaxLength = 50;
            this.txtLBAddress.Name = "txtLBAddress";
            this.txtLBAddress.Size = new System.Drawing.Size(131, 20);
            this.txtLBAddress.TabIndex = 25;
            this.txtLBAddress.TextChanged += new System.EventHandler(this.unSavedChangesDetected);
            this.txtLBAddress.Enter += new System.EventHandler(this.txtLBAddress_Enter);
            this.txtLBAddress.Leave += new System.EventHandler(this.txtLBAddress_Leave);
            // 
            // Label_LBAddr
            // 
            this.Label_LBAddr.AutoSize = true;
            this.Label_LBAddr.Location = new System.Drawing.Point(16, 35);
            this.Label_LBAddr.Name = "Label_LBAddr";
            this.Label_LBAddr.Size = new System.Drawing.Size(82, 13);
            this.Label_LBAddr.TabIndex = 27;
            this.Label_LBAddr.Text = "ListenerAddress";
            // 
            // txtHelpInfo
            // 
            this.txtHelpInfo.Location = new System.Drawing.Point(18, 418);
            this.txtHelpInfo.Multiline = true;
            this.txtHelpInfo.Name = "txtHelpInfo";
            this.txtHelpInfo.ReadOnly = true;
            this.txtHelpInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHelpInfo.Size = new System.Drawing.Size(589, 38);
            this.txtHelpInfo.TabIndex = 24;
            // 
            // frmICOMSEditConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 472);
            this.Controls.Add(this.txtHelpInfo);
            this.Controls.Add(this.grpEditConfiguration);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmICOMSEditConfig";
            this.Text = "ICOMS Provisioning Service";
            this.Load += new System.EventHandler(this.frmICOMSEditConfig_Load);
            this.grpEditConfiguration.ResumeLayout(false);
            this.grpDefSaveConfig.ResumeLayout(false);
            this.grpSiteARU.ResumeLayout(false);
            this.grpSiteARU.PerformLayout();
            this.grpCRMConnection.ResumeLayout(false);
            this.grpCRMConnection.PerformLayout();
            this.grpLBConnection.ResumeLayout(false);
            this.grpLBConnection.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvICOMSEditConfig;
        private System.Windows.Forms.GroupBox grpEditConfiguration;
        private System.Windows.Forms.GroupBox grpDefSaveConfig;
        private System.Windows.Forms.Button btnSiteConfigSave;
        private System.Windows.Forms.Button btnSiteConfigDefault;
        private System.Windows.Forms.GroupBox grpCRMConnection;
        private System.Windows.Forms.GroupBox grpLBConnection;
        private System.Windows.Forms.TextBox txtFailOverTime;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtHTTPErrorCodes;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnCRMConnUpdate;
        private System.Windows.Forms.TextBox txtRetryWaitDuration;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRetryNumbers;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtHTTPTimeout;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBackUpURL;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPrimaryURL;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox grpSiteARU;
        private System.Windows.Forms.ComboBox cmbCusIdFlag;
        private System.Windows.Forms.ComboBox cmbNatFrmt4cFlag;
        private System.Windows.Forms.Button btnUpdateSite;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtlistenerPort;
        private System.Windows.Forms.Label lblListnerPort;
        private System.Windows.Forms.TextBox txtListenerAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSiteID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddSite;
        private System.Windows.Forms.Button btnRemoveSite;
        private System.Windows.Forms.TextBox txtHelpInfo;
        private System.Windows.Forms.TextBox txtSiteName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbDeviceIdFmt;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtTokenName4SiteId;
        private System.Windows.Forms.TextBox txtBackUpCCI4cURL;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtPrimaryCCI4cURL;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnLBConnUpdate;
        private System.Windows.Forms.TextBox txtLBPort;
        private System.Windows.Forms.Label Label_LBPort;
        private System.Windows.Forms.TextBox txtLBAddress;
        private System.Windows.Forms.Label Label_LBAddr;
    }
}

