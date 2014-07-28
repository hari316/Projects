using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using NUnit.Framework;

using ICOMSProvisioningService;

namespace ICOMS_Nunit_Test
{

    [TestFixture]
    class CRM_Request_UnitTest
    {

        [SetUp()]

        public void Init()
        {
            // some code here, that need to be run at the start of each test case

            Console.WriteLine("Starting Test[CRM_Request_UnitTest]:: Test Case[" + TestContext.CurrentContext.Test.Name + "]");


        }



        [TearDown()]

        public void Clean()
        {

            // code that will be called after each Test case
            Console.WriteLine("Ending Test Case[" + TestContext.CurrentContext.Test.Name + 
                "]::Result [" + TestContext.CurrentContext.Result.Status + "]");

        }


        [Test]

        public void Test_SendRequest()
        {
            SyncRequest synReq = new SyncRequest();
            synReq.CustomerId = "010203040";
            synReq.CustomerStatus = "A";
            CRM4cInterfaceAccessManager crmReq = new CRM4cInterfaceAccessManager(synReq);
            crmReq.UrlPostFix = "";
            String reqBody = "Test Body";           
            HttpStatusCode httpCode = HttpStatusCode.OK;
            bool expRes = crmReq.SendRequest(ref reqBody, ref httpCode);

            Assert.IsFalse(expRes,"Should not able to send Request as URL is not correct");

        }

        [Test]

        public void Test_SendRequest_ValidUrl()
        {
            SyncRequest synReq = new SyncRequest();
            synReq.CustomerId = "010203040";
            synReq.CustomerStatus = "A";
            CRM4cInterfaceAccessManager crmReq = new CRM4cInterfaceAccessManager(synReq);           
            crmReq.UrlPostFix = String.Format("/Customers/{0}", "010203040");
            crmReq.RequestBody = String.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?> <Customer id=\"{0}\" xmlns=\"urn:eventis:crm:2.0\"><IsBarred xmlns=\"urn:eventis:crm:2.0\">{1}</IsBarred></Customer>", "010203040", "false");
            crmReq.Method = "PUT";
            HttpStatusCode httpCode = HttpStatusCode.NotImplemented;
            string ResponseBody = "";
            bool expRes = crmReq.SendRequest(ref ResponseBody, ref httpCode);

            Assert.IsTrue(expRes, "Should be able to send Request to URL");

        }

        [Test]
        
        public void Test_SendRequest_Exception()
        {
            SyncRequest synReq = null;

            CRM4cInterfaceAccessManager crmReq = new CRM4cInterfaceAccessManager(synReq);
            crmReq.UrlPostFix = "";
            String reqBody = "Test Body";
            HttpStatusCode httpCode = HttpStatusCode.OK;
            try
            {
                bool expRes = crmReq.SendRequest(ref reqBody, ref httpCode);
            }
            catch (Exception e)
            {
                Assert.Fail("["+e.Message+"]. All Exceptions should be handeled");
            }
        }


       
    }
}
