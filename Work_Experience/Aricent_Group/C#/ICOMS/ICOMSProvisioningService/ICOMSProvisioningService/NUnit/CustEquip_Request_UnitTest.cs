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
    class CustEquip_Request_UnitTest
    {

        [SetUp()]

        public void Init()
        {
            // some code here, that need to be run at the start of each test case

            Console.WriteLine("Starting Test[CustEquip_Request_UnitTest]:: Test Case[" + TestContext.CurrentContext.Test.Name + "]");


        }



        [TearDown()]

        public void Clean()
        {

            // code that will be called after each Test case
            Console.WriteLine("Ending Test Case[" + TestContext.CurrentContext.Test.Name + 
                "]::Result [" + TestContext.CurrentContext.Result.Status + "]");

        }


        [Test]

        public void Test_AddEQI()
        {
            SyncRequest synReq = new SyncRequest();
            synReq.CustomerId = "010203040";
            synReq.CustomerStatus = "A";
            EquipmentSyncRequest esynReq = new EquipmentSyncRequest();
            esynReq.CustomerId = "010203040";
            esynReq.macAddress = "00:00:40:C4:2A:1E";
            esynReq.offeringId = new List<string>("0001234/0001236/0001237".Split('/'));
            esynReq.smartCardId = "";
            CustomerEquipmentRequest ceReq = new CustomerEquipmentRequest(esynReq, synReq);          
            ServiceRunTimeManager srvRunMgr = new ServiceRunTimeManager();
            String expRes = srvRunMgr.transalteResCode2ICOM4m4C(ceReq.AddEQI(), "AddEQI");
            Assert.AreEqual("0000901", expRes, "Should not able to add customer Equipment as Device Id is passes as incorrect format");

        }

        [Test]

        public void Test_AddEQI_ValidUrl()
        {
            SyncRequest synReq = new SyncRequest();
            synReq.CustomerId = "010203551";
            synReq.CustomerStatus = "A";
            EquipmentSyncRequest esynReq = new EquipmentSyncRequest();
            esynReq.CustomerId = "010203551";
            esynReq.macAddress = "00002986685810000000000000000551";
            esynReq.offeringId = new List<string>("0001234/0001236/0001237".Split('/'));
            esynReq.smartCardId = "";
            CustomerEquipmentRequest ceReq = new CustomerEquipmentRequest(esynReq, synReq);
            ServiceRunTimeManager srvRunMgr = new ServiceRunTimeManager();
            String expRes = srvRunMgr.transalteResCode2ICOM4m4C(ceReq.AddEQI(), "AddEQI");

            Assert.AreEqual("0000000", expRes, "Should be able to add Customer Equipment ");

        }

        

        [Test]

        public void Test_UpdateEQI()
        {
            SyncRequest synReq = new SyncRequest();
            synReq.CustomerId = "010203040";
            synReq.CustomerStatus = "A";
            EquipmentSyncRequest esynReq = new EquipmentSyncRequest();
            esynReq.CustomerId = "010203040";
            esynReq.macAddress = "00002986685810000000000000000001";
            esynReq.offeringId = new List<string>("0005432".Split('/'));
            esynReq.smartCardId = "";
            CustomerEquipmentRequest ceReq = new CustomerEquipmentRequest(esynReq, synReq);
            ServiceRunTimeManager srvRunMgr = new ServiceRunTimeManager();
            String expRes = srvRunMgr.transalteResCode2ICOM4m4C(ceReq.UpdateEQI(), "UpdateEQI");

            Assert.AreEqual("0000901", expRes, "Should not able to update customer Equipment as product id is not in correct format");

        }

        [Test]

        public void Test_UpdateEQI_ValidUrl()
        {
            SyncRequest synReq = new SyncRequest();
            synReq.CustomerId = "010203040";
            synReq.CustomerStatus = "A";
            EquipmentSyncRequest esynReq = new EquipmentSyncRequest();
            esynReq.CustomerId = "010203040";
            esynReq.macAddress = "00002986685810000000000000000551";
            esynReq.offeringId = new List<string>("1234567".Split('/'));
            esynReq.smartCardId = "";
            CustomerEquipmentRequest ceReq = new CustomerEquipmentRequest(esynReq, synReq);
            ServiceRunTimeManager srvRunMgr = new ServiceRunTimeManager();
            String expRes = srvRunMgr.transalteResCode2ICOM4m4C(ceReq.UpdateEQI(), "UpdateEQI");

            Assert.AreEqual("0000000", expRes, "Should be able to update Customer Equipment ");

        }


        [Test]

        public void Test_DeleteEQI()
        {
            SyncRequest synReq = new SyncRequest();
            synReq.CustomerId = "010203040";
            synReq.CustomerStatus = "A";
            EquipmentSyncRequest esynReq = new EquipmentSyncRequest();
            esynReq.CustomerId = "010203040";
            esynReq.macAddress = "00002986685810000000000000000987123";
            esynReq.offeringId = new List<string>("0001234/0001236/0001237".Split('/'));
            esynReq.smartCardId = "";
            CustomerEquipmentRequest ceReq = new CustomerEquipmentRequest(esynReq, synReq);                    
            ServiceRunTimeManager srvRunMgr = new ServiceRunTimeManager();
            String expRes = srvRunMgr.transalteResCode2ICOM4m4C(ceReq.DeleteEQI(), "DeleteEQI");

            Assert.AreEqual("0000301", expRes, "Should not be able to delete customer Equipment as device id is passed as incorrect value");
        }


        [Test]

        public void Test_DeleteEQI_ValidUrl()
        {
            SyncRequest synReq = new SyncRequest();
            synReq.CustomerId = "010203551";
            synReq.CustomerStatus = "A";
            EquipmentSyncRequest esynReq = new EquipmentSyncRequest();
            esynReq.CustomerId = "010203551";
            esynReq.macAddress = "00002986685810000000000000000551";
            esynReq.offeringId = new List<string>("1234567".Split('/'));
            esynReq.smartCardId = "";
            CustomerEquipmentRequest ceReq = new CustomerEquipmentRequest(esynReq, synReq);                   
            ServiceRunTimeManager srvRunMgr = new ServiceRunTimeManager();
            String expRes = srvRunMgr.transalteResCode2ICOM4m4C(ceReq.DeleteEQI(), "DeleteEQI");

            Assert.AreEqual("0000000", expRes, "Should be able to delete Customer Equipment ");

        }
        
       
    }
}
