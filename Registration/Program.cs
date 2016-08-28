using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseFlipper.WebSite.Registration
{
    class Program
    {
        static void Main(string[] args)
        {
            string webSiteName = "House Flipper - DEV";
            string physicalPath = @"C:\GitHub\Sites\HouseFlipper\Dev\WebSite";
            ServerManager serverManager = new ServerManager();

            //BINDING
            string protocol = "http";
            string binding = "localhost:80:demo.houseflipper.com";            
            Site mySite = serverManager.Sites.Add(webSiteName, protocol, binding, physicalPath);

            mySite.ServerAutoStart = true;
            serverManager.CommitChanges();
        }
    }
}
