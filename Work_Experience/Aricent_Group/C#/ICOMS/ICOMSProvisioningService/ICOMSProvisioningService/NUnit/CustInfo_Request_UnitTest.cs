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
    class CustInfo_Request_UnitTest
    {

        [SetUp()]

        public void Init()
        {
            // some code here, that need to be run at the start of each test case

            Console.WriteLine("Starting Test[CustInfo_Request_UnitTest]:: Test Case[" + TestContext.CurrentContext.Test.Name + "]");


        }



        [TearDown()]

        public void Clean()
        {

            // code that will be called after each Test case
            Console.WriteLine("Ending Test Case[" + TestContext.CurrentContext.Test.Name + 
                "]::Result [" + TestContext.CurrentContext.Result.Status + "]");

        }


        [Test]

        public void Test_AddCustomer()
        {
            SyncRequest synReq = new SyncRequest();
            synReq.CustomerId = "010203040";
            synReq.CustomerStatus = "A";            
            CustomerInformationRequest ciReq = new CustomerInformationRequest("010203040", synReq);           
            String expRes = ciReq.AddCustomer();            
            Assert.AreEqual("0000000", expRes, "Customer Added successfully");
        }



        [Test]

        public void Test_AddCustomer_ValidUrl()
        {
            SyncRequest synReq = new SyncRequest();
            synReq.CustomerId = "010203040";
            synReq.CustomerStatus = "A";

            CustomerInformationRequest ciReq = new CustomerInformationRequest("010203040", synReq);
            ciReq.UrlPostFix = "";
            String reqBody = "Test Body";

            ciReq.RequestBody = reqBody;
            String expRes = ciReq.AddCustomer();

            Assert.AreEqual("0000000", expRes, "Should able to add customer info as URL is correct");

        }

        
        
       
    }
}
