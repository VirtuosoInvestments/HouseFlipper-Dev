using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;
using Test.HouseFlipper.Common;

namespace Test.HouseFlipper.Registration
{
    [TestFixture]
    [Category(TestRunType.Integration)]
    public class RegistrationTest
    {
        private string exe = @"C:\GitHub\Sites\HouseFlipper\Dev\Registration\bin\debug\HouseFlipper.WebSite.Registration.exe";
        [SetUp]
        public void Setup()
        {
            /* Setup:
             * 1. Locate the HouseFlipper.Website.Registration.exe
             * 2. Verify file exists             
             */
            Assert.IsTrue(File.Exists(exe));
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
             *    -directory    Physical location of website root directory  
             */

            /* Step 1: Run HouseFlipper.Registration -help
             */

            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(exe, "-help");
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.Start();
            var exited = p.WaitForExit((int)TimeSpan.FromMinutes(1).TotalMilliseconds);
            if (!exited)
            {
                //kill it
                p.Kill();
                Assert.Fail();
            }

            /* Step 2: Read output from file            
             */
            string output = p.StandardOutput.ReadToEnd();
            Console.WriteLine("Output: {0}", output);

            string error = p.StandardError.ReadToEnd();
            Console.WriteLine("Error: {0}", error);

            Assert.IsTrue(string.IsNullOrWhiteSpace(error));
            Assert.IsTrue(!string.IsNullOrWhiteSpace(output));
        }
    }
}
