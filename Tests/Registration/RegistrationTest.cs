using NUnit.Framework;
using System;
using System.Diagnostics;
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
            
            /* Step 1: Run HouseFlipper.Registration -help
             */
            var exe = @"C:\C:\GitHub\Sites\HouseFlipper\Dev\Registration\bin\debug\HouseFlipper.WebSite.Registration.exe";
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(exe, "-help");
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardError = true;            
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.Start();

            /* Step 2: Read output from file            
             */
            string output = p.StandardOutput.ReadToEnd();
            string error = p.StandardError.ReadToEnd();
            p.WaitForExit((int)TimeSpan.FromMinutes(1).TotalMilliseconds);
            Assert.IsTrue(string.IsNullOrWhiteSpace(error));
            Assert.IsTrue(!string.IsNullOrWhiteSpace(output));
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
