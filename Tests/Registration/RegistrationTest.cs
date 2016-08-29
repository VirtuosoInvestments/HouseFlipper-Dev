using NUnit.Framework;
using Test.HouseFlipper.Common;

namespace Test.HouseFlipper.Registration
{
    [TestFixture]
    [Category(TestRunType.Integration)]
    public class RegistrationTest
    {
        [SetUp]
        public void Setup()
        {
            /* Setup:
             * 1. Locate the HouseFlipper.Website.Registration.exe
             * 2. Verify file exists             
             */
        }

        [Test]
        public void Help()
        {
            /* Help Test:
             * 1. Run HouseFlipper.Registration.exe -help
             * 2. Veriy help shows 
             *    -environment  Environment to setup the House Flipper website for, i.e. DEMO
             *    -port         Web port binding, i.e. 8080 (default)
             *    -protocol     Web protocol, i.e. http (default)          
             */
        }

        [Test]
        public void AddWebsiteToIIS()
        {
            /* Add Website to IIS test
             * 1. Run HouseFlipper.Website.Registration.exe with args:
             *    a. -environment demo
             *    b. -port 8080            
             */
        }
    }
}
