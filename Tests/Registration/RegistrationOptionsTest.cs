using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Test.HouseFlipper.Common;

namespace Test.HouseFlipper.Registration
{
    [TestFixture]
    [Category(TestRunType.Integration)]
    public class RegistrationOptionsTest
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
            Console.WriteLine("Standard Output:\n {0}", output);

            string error = p.StandardError.ReadToEnd();
            if (!string.IsNullOrWhiteSpace(error))
            {
                Console.WriteLine("Standard Error:\n {0}", error);
            }

            Assert.IsTrue(string.IsNullOrWhiteSpace(error), "Error: Was not expecting error output!");
            Assert.IsTrue(!string.IsNullOrWhiteSpace(output), "Error: Expecting -help would show some output, but appears nothing has been printed to the screen!");

            /* Step 3: Verify output
             */
            var expectedOutput = new string[]
            {                            
                "HouseFlipper.WebSite.Registration.exe <options>",
                "OPTIONS:",
                "-help  Shows help information"
            };

            List<int> notFound = new List<int>();
            var actualLines = output.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries );
            var index = -1;
            foreach(var eLine in expectedOutput)
            {
                ++index;
                var expected = eLine.ToLower().Trim();
                var found = false;
                foreach(var aLine in actualLines)
                {
                    if(string.IsNullOrWhiteSpace(aLine))
                    {
                        continue;
                    }
                    var actual = aLine.Trim().ToLower();
                    
                    if(actual.Equals(expected))
                    {
                        found = true;
                        break;
                    }
                }
                if(!found)
                {
                    notFound.Add(index);
                }
            }

            if(notFound.Count>0)
            {
                Console.WriteLine("Error: Following information was not displayed by -help");
                Console.WriteLine("".PadLeft(50,'-'));
                foreach(var i in notFound)
                {
                    var missing = expectedOutput[i];
                    Console.WriteLine(missing);
                }
                Console.WriteLine();
                Assert.Fail();
            }
        }
    }
}
