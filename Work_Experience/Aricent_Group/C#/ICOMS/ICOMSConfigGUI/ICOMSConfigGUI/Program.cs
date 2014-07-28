using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using log4net;
using log4net.Config;

namespace ICOMSConfigGUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            log4net.Config.XmlConfigurator.Configure();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmICOMSEditConfig());
        }
    }
}
