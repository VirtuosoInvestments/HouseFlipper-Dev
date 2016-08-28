using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HouseFlipper.WebSite.Registration
{
    class Program
    {
        static void Main(string[] args)
        {
            string env = "dev";
            string webSiteName = string.Format("House Flipper - {0}",env.ToUpper());
            string physicalPath = @"C:\GitHub\Sites\HouseFlipper\Dev\WebSite";
            ServerManager serverManager = new ServerManager();

            //BINDING1 - TRY
            string protocol = "http";            
            int port = 8080;
            //string binding = string.Format("localhost:{0}:{1}.houseflipper.com",port,env);            
            //Site mySite = serverManager.Sites.Add(webSiteName, protocol, binding, physicalPath);

            //BINDING2 - TRY
            var ipaddress = "localhost";
            var hostName = Environment.MachineName;
            //var binding2 =
            //string.Format("{0}:{1}:{2}", ipaddress, port, hostName);
            //Site mySite = serverManager.Sites.Add(webSiteName, protocol, binding2, physicalPath);

            //BINDING3 - TRY
            var hostName2 = Dns.GetHostName();
            var ipaddress2 = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
            string binding3 = string.Format(
                "{0}://{1}:{2}:{3}.houseflipper.com",protocol,ipaddress2,port,env);
            //Site mySite = serverManager.Sites.Add(webSiteName, binding3, physicalPath, new byte[] { });

            //BINDING4 - TRY
            string binding4 = string.Format(
                "{0}://{1}:{2}",protocol,ipaddress,port);
            //Site mySite = serverManager.Sites.Add(webSiteName, binding4, physicalPath, new byte[] { });

            //BINDING5 - SIMPLE TRY
            Site mySite = serverManager.Sites.Add(webSiteName, physicalPath,  port);

            //------------------------------------------
            mySite.ServerAutoStart = true;
            serverManager.CommitChanges();

            Console.WriteLine("Program ended. Hit any key to exit!");
            Console.ReadKey();
        }
    }
}
