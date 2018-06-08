using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using FlowerMarket.DataAccess;
namespace FlowerMarket
{
    public class DatabaseManagment
    {
        public class DatabaseManager
        {
            public static string ConnectionStr
            {
                get
                {
                    return WebConfigurationManager
                        .ConnectionStrings["ConnStrNew"]
                        .ConnectionString;
                }
            }

            public static bool GetFlowers()
            {
                try
                {
                    FlowerManager manager = new FlowerManager();
                    var allClients = manager.GetFlower(null);
                    return allClients.Count > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            

        }
    }
}