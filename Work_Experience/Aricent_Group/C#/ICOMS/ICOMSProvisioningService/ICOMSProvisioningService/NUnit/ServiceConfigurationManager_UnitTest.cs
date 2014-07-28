using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using ICOMSProvisioningService;

namespace ICOMS_Nunit_Test
{

    [TestFixture()]

    public class ServiceConfigurationManager_UnitTest

    {

 
 

        [SetUp()]

        public void Init()

        {
            // some code here, that need to be run at the start of each test case

            Console.WriteLine("Starting Test[ServiceConfigurationManager_UnitTest]:: Test Case[" + TestContext.CurrentContext.Test.Name + "]");
            
       
        }

     

        [TearDown()]

        public void Clean()

        {

        // code that will be called after each Test case
            Console.WriteLine("Ending Test Case[" + TestContext.CurrentContext.Test.Name + "]::Result [" + TestContext.CurrentContext.Result.Status + "]");

        }

     
        [Test]

        public void Test_getAllListernsList()
        {
            ServiceConfigurationManager cm = new ServiceConfigurationManager();
            ServiceListenerMembers[] lstMem = ServiceConfigurationManager._CRMSiteDetails;
            Assert.IsInstanceOf(typeof(Array), lstMem,"Retuned object does not match with Expected Object");
            Assert.IsInstanceOf(typeof(ServiceListenerMembers), lstMem[0], "Retuned object does not match with Expected Object");
        }


    }

}
