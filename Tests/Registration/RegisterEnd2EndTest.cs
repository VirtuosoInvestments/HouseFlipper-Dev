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

            /* Step 3: Verify output
             */
            var expectedOutput = new string[]
            {
                "Registering HouseFlipper website",
                "SITE: House Flipper - DEV",
                "URL: http://localhost:8080"
            };
            VerifyExeOutput(output, expectedOutput);

            /* Step 4: Go to Chrome and verify you can access address http://localhost:8080
             */
            throw new NotImplementedException();
        }

        #region Verification methods
        private static void VerifyExeOutput(string output, string[] expectedOutput)
        {
            List<int> notFound = new List<int>();
            var actualLines = output.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var index = -1;
            foreach (var eLine in expectedOutput)
            {
                ++index;
                var expected = eLine.ToLower().Trim();
                var found = false;
                foreach (var aLine in actualLines)
                {
                    if (string.IsNullOrWhiteSpace(aLine))
                    {
                        continue;
                    }
                    var actual = aLine.Trim().ToLower();

                    if (actual.Equals(expected))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    notFound.Add(index);
                }
            }

            if (notFound.Count > 0)
            {
                Console.WriteLine("Error: Following information was not displayed by -help");
                Console.WriteLine("".PadLeft(50, '-'));
                foreach (var i in notFound)
                {
                    var missing = expectedOutput[i];
                    Console.WriteLine(missing);
                }
                Console.WriteLine();
                Assert.Fail();
            }
        }
        #endregion Verification methods

        #region Helper methods
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
        #endregion Helper methods
    }
}
