using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.ConsoleRunner;
using NUnit.Framework;
using NUnit.Core;
using log4net;
using ICOMSProvisioningService;

namespace ICOMS_Nunit_Test
{
    [SetUpFixture]
    class ICOMS_Test_Engine
    {
        
     /*
     public static void Main()
        {
            Console.WriteLine("Starting ICOMS Test Engine...");
            NUnit.ConsoleRunner.Runner.Main(new string[]
            {
                System.Reflection.Assembly.GetExecutingAssembly().Location, 
            });
            


        } */
         

        [SetUp]
        public void init()
        {
            log4net.Config.XmlConfigurator.Configure();
            ILog logger = LogManager.GetLogger(typeof(ICOMS_Test_Engine));
            logger.Info("--------------------------------------------------------------");
            logger.Info("Starting Unit Testing");
            logger.Info("--------------------------------------------------------------");
            logger.Info("Load CRM Connection Details");
            ServiceConfigurationManager srvCnfgMgr = new ServiceConfigurationManager();
            srvCnfgMgr.LoadServiceConfigFile();
            srvCnfgMgr.getAllListernsList();
            srvCnfgMgr.LoadCRMConnectionDetails();
            Console.WriteLine("Starting Unit Testing");
            
        }

        [TearDown]
        public void clean()
        {
            ILog logger = LogManager.GetLogger(typeof(ICOMS_Test_Engine));
            logger.Info("--------------------------------------------------------------");
            logger.Info("Stopping Unit Testing");
            logger.Info("--------------------------------------------------------------");
            Console.WriteLine("Stopping Unit Testing");
        }
        


    }
}
