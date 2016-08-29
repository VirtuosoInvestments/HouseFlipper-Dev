using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.HouseFlipper.Common;

namespace Test.HouseFlipper.Registration
{
    [TestFixture]
    [Category(TestRunType.Integration)]
    public class RegisterEnd2EndTest
    {
        private string exe = @"C:\GitHub\Sites\HouseFlipper\Dev\Registration\bin\debug\HouseFlipper.WebSite.Registration.exe";

        [OneTimeSetUp]
        public void Setup()
        {
            /* Setup:
             * 1. Locate the HouseFlipper.Website.Registration.exe
             * 2. Verify file exists             
             */
            Assert.IsTrue(File.Exists(exe));
        }

        [Test]
        public void RunWithDefaults()
        {
            /* Step 1: Run HouseFlipper.Registration
             */
            Process p = RunExe();

            /* Step 2: Read output from file            
             */
            string output = ReadOutput(p);
            string error = ReadError(p);

            Assert.IsTrue(string.IsNullOrWhiteSpace(error), "Error: Was not expecting error output!");
            Assert.IsTrue(!string.IsNullOrWhiteSpace(output), "Error: Expecting some output, but appears nothing has been printed to the screen!");
        }

        private static string ReadError(Process p)
        {
            string error = p.StandardError.ReadToEnd();
            if (!string.IsNullOrWhiteSpace(error))
            {
                Console.WriteLine("Standard Error:\n {0}", error);
            }

            return error;
        }

        private static string ReadOutput(Process p)
        {
            string output = p.StandardOutput.ReadToEnd();
            Console.WriteLine("Standard Output:\n {0}", output);
            return output;
        }

        private Process RunExe()
        {
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(exe);
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

            return p;
        }
    }
}
