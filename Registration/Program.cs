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
            // SAMPLE
            ServerManager serverManager = new ServerManager();
            Site mySite = serverManager.Sites.Add("Racing Cars Site", "d:\\inetpub\\wwwroot\racing",  8080);
            mySite.ServerAutoStart = true;
            serverManager.CommitChanges();
        }
    }
}
