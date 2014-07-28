﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace ICOMSClient
{
    public partial class FrmICOMSClient : Form
    {
        public FrmICOMSClient()
        {
            InitializeComponent();
        }

        private void btnSendReq_Click(object sender, EventArgs e)
        {            
            txtOPICOMMessage.Text=  Connect(txtIPAddress.Text.Trim(), txtIPPort.Text.Trim(), txtIPICOMMessage.Text.Trim());
        }

        public static string Connect(string server, string strPort, string message)
        {            
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer 
                // connected to the same address as specified by the server, port
                // combination.
                Int32 port = Convert.ToInt32(strPort);
                TcpClient client = new TcpClient(server, port);

                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing.
                NetworkStream stream = client.GetStream();

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);

              

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                
                stream.Close();
                client.Close();

                return responseData;
            }
            catch (ArgumentNullException e)
            {
                
                return e.ToString();
            }
            catch (SocketException e)
            {
                
                return e.ToString();
            }

           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtOPICOMMessage_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtIPICOMMessage.Text = "I000CUI,TI:000000000000000000,AN:010203040,SI:013,HE:A1,ND:B1234,BD:05,TL:Mrs ,LNixpack        ,FN:Joe       ,MI:Y,A1:123 Street St.                  ,A2:Apt#123                         ,CT:Greenville               ,STA,ZP:12345    ,HP:213 123 1234,WP:215 123 1234,AS:A,E1:ENG456789012345,E2:ENG112211221122,E3:ENG334433443344.00039V000CUI,0000000,TI:000000000000000000.select HomePhone from Subscriber where SmsTag='013010203040'213 123 1234select SecondPhone from Subscriber where SmsTag='013010203040'215 123 1234";            
            //txtIPICOMMessage.Text = string.Empty;
        }

    }
}
