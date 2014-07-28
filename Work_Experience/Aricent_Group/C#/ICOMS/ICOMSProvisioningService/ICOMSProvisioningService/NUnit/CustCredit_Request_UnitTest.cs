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
    class CustCredit_Request_UnitTest
    {

        [SetUp()]

        public void Init()
        {
            // some code here, that need to be run at the start of each test case

            Console.WriteLine("Starting Test[CustCredit_Request_UnitTest]:: Test Case[" + TestContext.CurrentContext.Test.Name + "]");


        }



        [TearDown()]

        public void Clean()
        {

            // code that will be called after each Test case
            Console.WriteLine("Ending Test Case[" + TestContext.CurrentContext.Test.Name + 
                "]::Result [" + TestContext.CurrentContext.Result.Status + "]");

        }


        [Test]

        public void Test_AddCCI()
        {
            SyncRequest synReq = new SyncRequest();
            synReq.CustomerId = "010203040";
            synReq.CustomerStatus = "A";
            SubscriberSyncRequest ssynReq = new SubscriberSyncRequest();
            ssynReq.creditLimit = 200;
            CustomerCreditRequest ccReq = new CustomerCreditRequest(ssynReq, synReq);
            
           /* ccReq.UrlPostFix = "";
            String reqBody = "Test Body";            
            ccReq.RequestBody = reqBody; */

            String expRes = ccReq.AddCCI();

            Assert.AreEqual("0000000",expRes,"Credit Limit Added correctly");

        }

        [Test]

        public void Test_AddCCI_ValidUrl()
        {
            SyncRequest synReq = new SyncRequest();
            synReq.CustomerId = "010203040";
            synReq.CustomerStatus = "A";
            SubscriberSyncRequest ssynReq = new SubscriberSyncRequest();
            ssynReq.creditLimit = 200;
            CustomerCreditRequest ccReq = new CustomerCreditRequest(ssynReq, synReq);           
         
            String expRes = ccReq.AddCCI();

            Assert.AreEqual("0000000",expRes, "Should be able to add Customer Credit ");

        }

        [Test]
        
        public void Test_AddCCI_Exception()
        {
            SyncRequest synReq = new SyncRequest();
            synReq.CustomerId = "010203040";
            synReq.CustomerStatus = "A";
            SubscriberSyncRequest ssynReq = new SubscriberSyncRequest();
            ssynReq.creditLimit = 200;
            CustomerCreditRequest ccReq = new CustomerCreditRequest(ssynReq, synReq);
            ccReq.UrlPostFix = "";
            String reqBody = "Test Body";

            ccReq.RequestBody = reqBody;
            try
            {
                String expRes = ccReq.AddCCI();
            }
            catch (Exception e)
            {
                Assert.Fail("["+e.Message+"]. All Exceptions should be handeled");
            }
        }



        [Test]

        public void Test_updateCCI()
        {
            SyncRequest synReq = new SyncRequest();
            synReq.CustomerId = "010203040";
            synReq.CustomerStatus = "A";
            SubscriberSyncRequest ssynReq = new SubscriberSyncRequest();
            ssynReq.creditLimit = 200;
            CustomerCreditRequest ccReq = new CustomerCreditRequest(ssynReq, synReq);
            
            String expRes = ccReq.updateCCI();

            Assert.AreEqual("0000000", expRes, "Should be able to update Customer Credit ");

        }

        [Test]

        public void Test_updateCCI_ValidUrl()
        {
            SyncRequest synReq = new SyncRequest();
            synReq.CustomerId = "010203040";
            synReq.CustomerStatus = "A";
            SubscriberSyncRequest ssynReq = new SubscriberSyncRequest();
            ssynReq.creditLimit = 200;
            CustomerCreditRequest ccReq = new CustomerCreditRequest(ssynReq, synReq);
            ccReq.UrlPostFix = "";
            String reqBody = "Test Body";

            ccReq.RequestBody = reqBody;
            String expRes = ccReq.updateCCI();

            Assert.AreEqual("0000000", expRes, "Should be able to update Customer Credit ");

        }

       
    }
}
