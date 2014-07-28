using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ICOMSProvisioningService;

namespace ICOMS_Nunit_Test
{

    [TestFixture()]
    class ServiceBusinessRulesManager_UnitTest
    {

        [SetUp()]

        public void Init()
        {
            // some code here, that need to be run at the start of each test case

            Console.WriteLine("Starting Test[ServiceBusinessRulesManager_UnitTest]:: Test Case[" + TestContext.CurrentContext.Test.Name + "]");


        }



        [TearDown()]

        public void Clean()
        {

            // code that will be called after each Test case
            Console.WriteLine("Ending Test Case[" + TestContext.CurrentContext.Test.Name + "]::Result [" + TestContext.CurrentContext.Result.Status + "]");

        }

        [Test]

        public void Test_AddSiteId2CustId()
        {

            ServiceBusinessRulesManager bm = new ServiceBusinessRulesManager();
            ServiceListenerMembers listMem = new ServiceListenerMembers();
            Dictionary<String, String> testData = new Dictionary<string, string>();
            Dictionary<String, String> expData = bm.AddSiteId2CustId(testData, listMem);

            Assert.IsInstanceOf(typeof(Dictionary<String, String>), expData, "Method does not return correct Object Type");
        }


        [Test]

        public void Test_AddSiteId2CustId_Value()
        {
            ServiceBusinessRulesManager bm = new ServiceBusinessRulesManager();
            ServiceListenerMembers listMem = new ServiceListenerMembers();
            listMem.siteid = "siteid1";
            listMem.listenerAddress = "10.203.232.19";

            listMem.CustomerIdFlag = "true";

            listMem.listenerPort = "12000";

            listMem.siteIdTokenName = "SI:";

            Dictionary<String, String> testData = new Dictionary<string, string>();
            testData.Add("AN:", "010203040");
            testData.Add("AS:", "A");
            testData.Add("SI:", "013");
            testData.Add("TI:", "000000000000000000");

            Dictionary<String, String> expData = bm.AddSiteId2CustId(testData, listMem);

            Assert.IsTrue(expData.ContainsValue("010203040013"), "Not able to add Site ID to Customer ID");
            
        }


        [Test]

        public void Test_AddSiteId2CustId_FalseValue()
        {
            ServiceBusinessRulesManager bm = new ServiceBusinessRulesManager();
            ServiceListenerMembers listMem = new ServiceListenerMembers();
            listMem.siteid = "siteid1";
            listMem.listenerAddress = "10.203.232.19";

            listMem.CustomerIdFlag = "false";

            listMem.listenerPort = "12000";            

            Dictionary<String, String> testData = new Dictionary<string, string>();
            testData.Add("AN:", "010203040");
            testData.Add("AS:", "A");
            testData.Add("SI:", "013");
            testData.Add("TI:", "000000000000000000");

            Dictionary<String, String> expData = bm.AddSiteId2CustId(testData, listMem);

            Assert.IsFalse(expData.ContainsValue("010203040013"), "Site ID should not be added to Customer ID");

        }


        [Test]

        public void Test_checkRequiredTokensPresent()
        {
            ServiceBusinessRulesManager bm = new ServiceBusinessRulesManager();
            Dictionary<String, String> testData = new Dictionary<string, string>();
            testData.Add("AN:", "010203040");
            testData.Add("CL:", "000000995");
            testData.Add("SI:", "013");
            testData.Add("TI:", "000000000000000000");
            String expRes = bm.checkRequiredTokensPresent(testData,ServiceConstantsManager.CONST_CCI);
            Assert.AreEqual("",expRes,"Unable to search Token");
        }
                        
    }
}
