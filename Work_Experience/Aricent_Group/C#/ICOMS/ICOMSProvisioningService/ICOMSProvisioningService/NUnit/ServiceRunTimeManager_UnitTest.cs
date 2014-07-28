using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using ICOMSProvisioningService;

namespace ICOMS_Nunit_Test
{

    [TestFixture]
    class ServiceRunTimeManager_UnitTest
    {


        [SetUp()]

        public void Init()
        {
            // some code here, that need to be run at the start of each test case

            Console.WriteLine("Starting Test[ServiceRunTimeManager_UnitTest]:: Test Case[" + TestContext.CurrentContext.Test.Name + "]");


        }



        [TearDown()]

        public void Clean()
        {

            // code that will be called after each Test case
            Console.WriteLine("Ending Test Case[" + TestContext.CurrentContext.Test.Name + "]::Result [" + TestContext.CurrentContext.Result.Status + "]");

        }


        [Test]

        public void Test_processICOMMessages()
        {
            ServiceRunTimeManager srtMngr = new ServiceRunTimeManager();
            ServiceListenerMembers listMem = new ServiceListenerMembers();
            listMem.siteid = "siteid1";
            listMem.listenerAddress = "10.203.232.19";

            listMem.CustomerIdFlag = "true";

            listMem.listenerPort = "12000";

            String testData = "I000CUI,TI:000000000000000000,AN:010203040,SI:013,HE:C3,ND:B4567,BD:05,TL:Mrs ,LN:Customer       ,FN:Test      ,MI:E,A1:222 Avenue St.                  ,A2:Apt#123                         ,CT:Greensburg               ,ST:PA,ZP:12345    ,HP:213 123 1234,WP:215 123 1234,AS:A,E1:ENG000102030405.00039V000CUI,0000000,TI:000000000000000000.select LastName from Subscriber where SmsTag='013010203040'CUSTOMERselect FirstName from Subscriber where SmsTag='013010203040'TEST";
            String expData = "00047V000CUI,0000000,TI:000000000000000000.";

            Assert.AreEqual(expData, srtMngr.processICOMSMessages(testData, listMem), "ICOM Message is not processed correctly");

        }


        [Test]

        public void Test_processCUIMessage()
        {
            ServiceRunTimeManager srtMngr = new ServiceRunTimeManager();
            ServiceListenerMembers listMem = new ServiceListenerMembers();
            listMem.siteid = "siteid1";
            listMem.listenerAddress = "10.203.232.19";

            listMem.CustomerIdFlag = "true";

            listMem.listenerPort = "12000";            

            String testData = "I000CUI,TI:000000000000000000,AN:010203040,SI:013,HE:C3,ND:B4567,BD:05,TL:Mrs ,LN:Customer       ,FN:Test      ,MI:E,A1:222 Avenue St.                  ,A2:Apt#123                         ,CT:Greensburg               ,ST:PA,ZP:12345    ,HP:213 123 1234,WP:215 123 1234,AS:A,E1:ENG000102030405.00039V000CUI,0000000,TI:000000000000000000.select LastName from Subscriber where SmsTag='013010203040'CUSTOMERselect FirstName from Subscriber where SmsTag='013010203040'TEST";
            String expData_Success = "0000000";
            String expData_Fail= "0000901";
            String resp = srtMngr.processCUIMessage(testData, listMem);
            if (resp.Equals(expData_Success) || resp.Equals(expData_Fail))
            {
                Assert.Pass("CUI Message is processed correctly");
            }
            else
            {
                Assert.Fail("CUI Message is not processed correctly. Response [" + resp+"]");
            }
            
        }


        [Test]

        public void Test_processCUIMessage_InvInput()
        {

            ServiceRunTimeManager srtMngr = new ServiceRunTimeManager();
            ServiceListenerMembers listMem = new ServiceListenerMembers();
            listMem.siteid = "siteid1";
            listMem.listenerAddress = "10.203.232.19";

            listMem.CustomerIdFlag = "true";

            listMem.listenerPort = "12000";            

            String testData = "I000CCI,TI:000000000000000000,AN:010203040,SI:013,CA:AB,CL:000000995,CP:0000000.00039V000CCI,0000000,TI:000000000000000000.select CreditLimit from Subscriber where SmsTag='013010203040'995";
            String expData = "0000114";
            String resp = srtMngr.processCUIMessage(testData, listMem);
            Assert.AreEqual(expData, resp, "CUI Message is not processed correctly. Response [" + resp + "]");
        }



        [Test]

        public void Test_processCCIMessage()
        {
            ServiceRunTimeManager srtMngr = new ServiceRunTimeManager();
            ServiceListenerMembers listMem = new ServiceListenerMembers();
            listMem.siteid = "siteid1";
            listMem.listenerAddress = "10.203.232.19";

            listMem.CustomerIdFlag = "true";

            listMem.listenerPort = "12000";            

            String testData = "I000CCI,TI:000000000000000000,AN:010203040,SI:013,CA:AB,CL:000000995,CP:0000000.00039V000CCI,0000000,TI:000000000000000000.select CreditLimit from Subscriber where SmsTag='013010203040'995";
            String expData_Success = "0000000";
            String expData_Fail = "0000901";
            String resp = srtMngr.processCCIMessage(testData, listMem);
            if (resp.Equals(expData_Success) || resp.Equals(expData_Fail))
            {
                Assert.Pass("CCI Message is processed correctly");
            }
            else
            {
                Assert.Fail("CCI Message is not processed correctly. Response [" + resp+"]");
            }

        }



        [Test]

        public void Test_processCCIMessage_InvInput()
        {

            ServiceRunTimeManager srtMngr = new ServiceRunTimeManager();
            ServiceListenerMembers listMem = new ServiceListenerMembers();
            listMem.siteid = "siteid1";
            listMem.listenerAddress = "10.203.232.19";

            listMem.CustomerIdFlag = "true";

            listMem.listenerPort = "12000";            

            String testData = "I000CUI,TI:000000000000000000,AN:010203040,SI:013,HE:C3,ND:B4567,BD:05,TL:Mrs ,LN:Customer       ,FN:Test      ,MI:E,A1:222 Avenue St.                  ,A2:Apt#123                         ,CT:Greensburg               ,ST:PA,ZP:12345    ,HP:213 123 1234,WP:215 123 1234,AS:A,E1:ENG000102030405.00039V000CUI,0000000,TI:000000000000000000.select LastName from Subscriber where SmsTag='013010203040'CUSTOMERselect FirstName from Subscriber where SmsTag='013010203040'TEST";
            String expData = "0000116";
            String resp = srtMngr.processCCIMessage(testData, listMem);
            Assert.AreEqual(expData, resp, "CCI Message is not processed correctly. Response [" + resp + "]");
        }


        [Test]

        public void Test_processEQIMessage()
        {
            ServiceRunTimeManager srtMngr = new ServiceRunTimeManager();
            ServiceListenerMembers listMem = new ServiceListenerMembers();
            listMem.siteid = "siteid1";
            listMem.listenerAddress = "10.203.232.19";

            listMem.CustomerIdFlag = "true";

            listMem.listenerPort = "12000";            

            String testData = "I000EQI,TI:000000000000000000,AN:010203040,SI:013,ES:A,EA:00002986685930000000000000000000,S#:ABC45678901234567890123456789012,PA:ABCDEFGH,SV:0001234/0001236/0001237,AC:A,0000257,TI:000000000000000000.select * from Settop where MacAddr='000011cd5231'<empty>";
            String expData_Success = "0000000";
            String expData_Fail = "0000901";
            String resp = srtMngr.processEQIMessage(testData, listMem);
            if (resp.Equals(expData_Success) || resp.Equals(expData_Fail))
            {
                Assert.Pass("EQI Message is processed correctly");
            }
            else
            {
                Assert.Fail("EQI Message is not processed correctly. Response [" + resp + "]");
            }

        }



        [Test]

        public void Test_processEQIMessage_InvInput()
        {

            ServiceRunTimeManager srtMngr = new ServiceRunTimeManager();
            ServiceListenerMembers listMem = new ServiceListenerMembers();
            listMem.siteid = "siteid1";
            listMem.listenerAddress = "10.203.232.19";

            listMem.CustomerIdFlag = "true";

            listMem.listenerPort = "12000";            

            String testData = "I000CUI,TI:000000000000000000,AN:010203040,SI:013,HE:C3,ND:B4567,BD:05,TL:Mrs ,LN:Customer       ,FN:Test      ,MI:E,A1:222 Avenue St.                  ,A2:Apt#123                         ,CT:Greensburg               ,ST:PA,ZP:12345    ,HP:213 123 1234,WP:215 123 1234,AS:A,E1:ENG000102030405.00039V000CUI,0000000,TI:000000000000000000.select LastName from Subscriber where SmsTag='013010203040'CUSTOMERselect FirstName from Subscriber where SmsTag='013010203040'TEST";
            String expData = "0000119";
            String resp = srtMngr.processEQIMessage(testData, listMem);
            Assert.AreEqual(expData, resp, "EQI Message is not processed correctly. Response [" + resp + "]");
        }


    }
}
