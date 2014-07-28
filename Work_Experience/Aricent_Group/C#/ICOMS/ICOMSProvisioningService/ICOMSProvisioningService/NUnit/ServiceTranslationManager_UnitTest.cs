using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using ICOMSProvisioningService;

namespace ICOMS_Nunit_Test
{

    [TestFixture()]
    class ServiceTranslationManager_UnitTest
    {

        [SetUp()]

        public void Init()
        {
            // some code here, that need to be run at the start of each test case

            Console.WriteLine("Starting Test[ServiceTranslationManager_UnitTest]:: Test Case[" + TestContext.CurrentContext.Test.Name + "]");


        }



        [TearDown()]

        public void Clean()
        {

            // code that will be called after each Test case
            Console.WriteLine("Ending Test Case[" + TestContext.CurrentContext.Test.Name + "]::Result [" + TestContext.CurrentContext.Result.Status + "]");

        }


        [Test]
        public void Test_findICOMInputFormat()
        {
            ServiceTranslationManager transMngr = new ServiceTranslationManager();
            String testData = "I000CUI,TI:000000000000000000,AN:010203040,SI:013,HE:C3,ND:B4567,BD:05,TL:Mrs ,LN:Customer       ,FN:Test      ,MI:E,A1:222 Avenue St.                  ,A2:Apt#123                         ,CT:Greensburg               ,ST:PA,ZP:12345    ,HP:213 123 1234,WP:215 123 1234,AS:A,E1:ENG000102030405.00039V000CUI,0000000,TI:000000000000000000.select LastName from Subscriber where SmsTag='013010203040'CUSTOMERselect FirstName from Subscriber where SmsTag='013010203040'TEST";
            
            String expData = transMngr.findICOMInputFormat(testData);
            Assert.AreEqual( "CUI", expData,"Wrong Input Format");

            testData = "I000CCI,TI:000000000000000000,AN:010203040,SI:013,CA:AB,CL:000000995,CP:0000000.00039V000CCI,0000000,TI:000000000000000000.select CreditLimit from Subscriber where SmsTag='013010203040'995";
            expData = transMngr.findICOMInputFormat(testData);
            Assert.AreEqual("CCI", expData, "Wrong Input Format");

            testData = "I000EQI,TI:000000000000000000,AN:010203040,SI:013,ES:A,EA:00002986685930000000000000000000,S#:ABC45678901234567890123456789012,PA:ABCDEFGH,SV:0001234/0001236/0001237,AC:A.00039V000EQI,0000257,TI:000000000000000000.select * from Settop where MacAddr='000011cd5231'<empty>";
            expData = transMngr.findICOMInputFormat(testData);
            Assert.AreEqual("EQI", expData, "Wrong Input Format");

            testData = "I000DER,TI:000000000000000000,AN:010203040,SI:013,ES:A";
            expData = transMngr.findICOMInputFormat(testData);
            Assert.AreEqual("INVALID", expData, "Problem in resolving the input format");
        }


        [Test]

        public void Test_getFullInputFormatValue()
        {
            ServiceTranslationManager transMngr = new ServiceTranslationManager();
            String testData = "I000CUI,TI:000000000000000000,AN:010203040,SI:013,HE:C3,ND:B4567,BD:05,TL:Mrs ,LN:Customer       ,FN:Test      ,MI:E,A1:222 Avenue St.                  ,A2:Apt#123                         ,CT:Greensburg               ,ST:PA,ZP:12345    ,HP:213 123 1234,WP:215 123 1234,AS:A,E1:ENG000102030405.00039V000CUI,0000000,TI:000000000000000000.select LastName from Subscriber where SmsTag='013010203040'CUSTOMERselect FirstName from Subscriber where SmsTag='013010203040'TEST";

            String expData = transMngr.getFullInputFormatValue(testData);

            Assert.AreEqual("I000CUI", expData, "Invalid Input format");

        }



        [Test]

        public void Test_getDataFor4cInterface()
        {
            ServiceTranslationManager transMngr = new ServiceTranslationManager();
            ServiceListenerMembers testListenMember = new ServiceListenerMembers();
            testListenMember.siteIdTokenName = "SI:";
            String InputData = "I000CUI,TI:000000000000000000,AN:010203040,SI:013,HE:C3,ND:B4567,BD:05,TL:Mrs ,LN:Customer       ,FN:Test      ,MI:E,A1:222 Avenue St.                  ,A2:Apt#123                         ,CT:Greensburg               ,ST:PA,ZP:12345    ,HP:213 123 1234,WP:215 123 1234,AS:A,E1:ENG000102030405.00039V000CUI,0000000,TI:000000000000000000.select LastName from Subscriber where SmsTag='013010203040'CUSTOMERselect FirstName from Subscriber where SmsTag='013010203040'TEST";

            Assert.IsInstanceOf(typeof(Dictionary<String, String>), transMngr.getDataFor4cInterface(InputData, ServiceConstantsManager.CONST_CUI, testListenMember), "Invalid data returned");
            Dictionary<String, String> expData = transMngr.getDataFor4cInterface(InputData, ServiceConstantsManager.CONST_CUI, testListenMember);
            Assert.AreEqual(expData.Count, ServiceConstantsManager.CONST_CUI.Length, "Data count is not valid");

        }


        [Test]

        public void Test_getKeyValuePair()
        {
            ServiceTranslationManager transMngr = new ServiceTranslationManager();
            String InputData = "I000CUI,TI:000000000000000000,AN:010203040,SI:013,HE:C3,ND:B4567,BD:05,TL:Mrs ,LN:Customer       ,FN:Test      ,MI:E,A1:222 Avenue St.                  ,A2:Apt#123                         ,CT:Greensburg               ,ST:PA,ZP:12345    ,HP:213 123 1234,WP:215 123 1234,AS:A,E1:ENG000102030405.00039V000CUI,0000000,TI:000000000000000000.select LastName from Subscriber where SmsTag='013010203040'CUSTOMERselect FirstName from Subscriber where SmsTag='013010203040'TEST";

            String[] expdata = { "AN", "010203040" };
            Assert.AreEqual(expdata, transMngr.getKeyValuePair(InputData, "AN"), "Unable to get correct value for key provided");

        }


        [Test]

        public void Test_getValue4Key()
        {
            ServiceTranslationManager transMngr = new ServiceTranslationManager();
            String InputData = "I000CUI,TI:000000000000000000,AN:010203040,SI:013,HE:C3,ND:B4567,BD:05,TL:Mrs ,LN:Customer       ,FN:Test      ,MI:E,A1:222 Avenue St.                  ,A2:Apt#123                         ,CT:Greensburg               ,ST:PA,ZP:12345    ,HP:213 123 1234,WP:215 123 1234,AS:A,E1:ENG000102030405.00039V000CUI,0000000,TI:000000000000000000.select LastName from Subscriber where SmsTag='013010203040'CUSTOMERselect FirstName from Subscriber where SmsTag='013010203040'TEST";

            String expdata = "010203040";

            Assert.AreEqual(expdata, transMngr.getValue4Key(InputData, "AN"), "Unable to get correct value for key provided");

        }


        [Test]

        public void Test_GetLine()
        {
            ServiceTranslationManager transMngr = new ServiceTranslationManager();
            Dictionary<String, String> testData = new Dictionary<string, string>();
            testData.Add("AN:", "010203040");
            testData.Add("SI:", "013");

            String expData = "AN:010203040,SI:013";
            Assert.AreEqual(expData, transMngr.GetLine(testData),"Invalid data returned");

        }


        [Test]

        public void Test_formatResponse()
        {
            ServiceTranslationManager transMngr = new ServiceTranslationManager();
            string strFmtTxt = "CUI";
            string strResFmt = "00000V000CUI";
            string strResCode = "100";
            string strTIValue = "000000000000000000";

            string expData = transMngr.formatResponse(strFmtTxt, strResFmt, strResCode, strTIValue);
            string testData = "00043V000CUI,100,TI:000000000000000000.";
            Assert.AreEqual(testData, expData,"Invalid response returned");

        }


        [Test]

        public void Test_ReplaceAtIndex()
        {
            ServiceTranslationManager transMngr = new ServiceTranslationManager();
            String testData = "testEdata";
            String expData = "testAdata";
            Assert.AreEqual(expData, transMngr.ReplaceAtIndex(4,'A', testData),"Invalid character replaced");
        }


        [Test]

        public void Test_getErrorCode4MissingTokens()
        {
            ServiceTranslationManager transMngr = new ServiceTranslationManager();

            Assert.AreEqual("0000100", transMngr.getErrorCode4MissingTokens("TI:"), "Invalid Error code returned.");
            Assert.AreEqual("0000101", transMngr.getErrorCode4MissingTokens("AN:"), "Invalid Error code returned.");
            Assert.AreEqual("0000102", transMngr.getErrorCode4MissingTokens("SI:"), "Invalid Error code returned.");
            Assert.AreEqual("0000114", transMngr.getErrorCode4MissingTokens("AS:"), "Invalid Error code returned.");
            Assert.AreEqual("0000116", transMngr.getErrorCode4MissingTokens("CL:"), "Invalid Error code returned.");
            Assert.AreEqual("0000117", transMngr.getErrorCode4MissingTokens("ES:"), "Invalid Error code returned.");
            Assert.AreEqual("0000119", transMngr.getErrorCode4MissingTokens("EA:"), "Invalid Error code returned.");
            Assert.AreEqual("0000121", transMngr.getErrorCode4MissingTokens("AC:"), "Invalid Error code returned.");

        }



        [Test]

        public void Test_getErrorCode4lengthOfTokens()
        {
            ServiceTranslationManager transMngr = new ServiceTranslationManager();
          


            Assert.AreEqual("0000200", transMngr.getErrorCode4lengthOfTokens(new KeyValuePair<String, String>("TI:", "0000000000000000000")), "Invalid Error code returned.");
            Assert.AreEqual("0000201", transMngr.getErrorCode4lengthOfTokens(new KeyValuePair<String, String>("AN:", "0102030400")), "Invalid Error code returned.");
            Assert.AreEqual("0000202", transMngr.getErrorCode4lengthOfTokens(new KeyValuePair<String, String>("SI:", "0130")), "Invalid Error code returned.");
            Assert.AreEqual("0000214", transMngr.getErrorCode4lengthOfTokens(new KeyValuePair<String, String>("AS:", "A0")), "Invalid Error code returned.");
            Assert.AreEqual("0000216", transMngr.getErrorCode4lengthOfTokens(new KeyValuePair<String, String>("CL:", "0000009950")), "Invalid Error code returned.");
            Assert.AreEqual("0000217", transMngr.getErrorCode4lengthOfTokens(new KeyValuePair<String, String>("ES:", "A0")), "Invalid Error code returned.");
            Assert.AreEqual("0000219", transMngr.getErrorCode4lengthOfTokens(new KeyValuePair<String, String>("EA:", "000029866859300000000000000000000")), "Invalid Error code returned.");
            Assert.AreEqual("0000221", transMngr.getErrorCode4lengthOfTokens(new KeyValuePair<String, String>("AC:", "A0")), "Invalid Error code returned.");
            Assert.AreEqual("0000257", transMngr.getErrorCode4lengthOfTokens(new KeyValuePair<String, String>("SV:", "000123")), "Invalid Error code returned.");

        }

    }
}
