namespace ICOMSClient
{
    partial class FrmICOMSClient
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIPAddress = new System.Windows.Forms.TextBox();
            this.txtIPPort = new System.Windows.Forms.TextBox();
            this.txtIPICOMMessage = new System.Windows.Forms.TextBox();
            this.btnSendReq = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtOPICOMMessage = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnConnectClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(47, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "PORT :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "ICOMS IP Message :";
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Location = new System.Drawing.Point(147, 34);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(199, 20);
            this.txtIPAddress.TabIndex = 3;
            this.txtIPAddress.Text = "10.204.255.6";
            // 
            // txtIPPort
            // 
            this.txtIPPort.Location = new System.Drawing.Point(147, 65);
            this.txtIPPort.Name = "txtIPPort";
            this.txtIPPort.Size = new System.Drawing.Size(74, 20);
            this.txtIPPort.TabIndex = 4;
            this.txtIPPort.Text = "14000";
            // 
            // txtIPICOMMessage
            // 
            this.txtIPICOMMessage.Location = new System.Drawing.Point(147, 106);
            this.txtIPICOMMessage.Multiline = true;
            this.txtIPICOMMessage.Name = "txtIPICOMMessage";
            this.txtIPICOMMessage.Size = new System.Drawing.Size(607, 171);
            this.txtIPICOMMessage.TabIndex = 5;
            // 
            // btnSendReq
            // 
            this.btnSendReq.Location = new System.Drawing.Point(205, 344);
            this.btnSendReq.Name = "btnSendReq";
            this.btnSendReq.Size = new System.Drawing.Size(98, 27);
            this.btnSendReq.TabIndex = 6;
            this.btnSendReq.Text = "Send Request";
            this.btnSendReq.UseVisualStyleBackColor = true;
            this.btnSendReq.Click += new System.EventHandler(this.btnSendReq_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(320, 344);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(102, 27);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtOPICOMMessage
            // 
            this.txtOPICOMMessage.Location = new System.Drawing.Point(147, 292);
            this.txtOPICOMMessage.Name = "txtOPICOMMessage";
            this.txtOPICOMMessage.Size = new System.Drawing.Size(607, 20);
            this.txtOPICOMMessage.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 295);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "ICOMS OP Message :";
            // 
            // btnConnectClose
            // 
            this.btnConnectClose.Location = new System.Drawing.Point(364, 30);
            this.btnConnectClose.Name = "btnConnectClose";
            this.btnConnectClose.Size = new System.Drawing.Size(98, 27);
            this.btnConnectClose.TabIndex = 10;
            this.btnConnectClose.Text = "Connect";
            this.btnConnectClose.UseVisualStyleBackColor = true;
            this.btnConnectClose.Click += new System.EventHandler(this.btnConnectClose_Click);
            // 
            // FrmICOMSClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 399);
            this.Controls.Add(this.btnConnectClose);
            this.Controls.Add(this.txtOPICOMMessage);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSendReq);
            this.Controls.Add(this.txtIPICOMMessage);
            this.Controls.Add(this.txtIPPort);
            this.Controls.Add(this.txtIPAddress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FrmICOMSClient";
            this.Text = "ICOMS Client";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtIPAddress;
        private System.Windows.Forms.TextBox txtIPPort;
        private System.Windows.Forms.TextBox txtIPICOMMessage;
        private System.Windows.Forms.Button btnSendReq;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtOPICOMMessage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnConnectClose;
    }
}

