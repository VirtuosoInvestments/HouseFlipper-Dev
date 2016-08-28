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
            int port = 8080;
            ServerManager serverManager = new ServerManager();            
            Site mySite = serverManager.Sites.Add(webSiteName, physicalPath,  port);
            mySite.ServerAutoStart = true;
            serverManager.CommitChanges();
        }
    }
}
