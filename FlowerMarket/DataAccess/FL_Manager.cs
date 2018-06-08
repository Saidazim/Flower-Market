using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Common;
using System.Web.Configuration;
using System.Data.SqlClient;
using FlowerMarket.Models;

namespace FlowerMarket
{
    public class FL_Manager
    {
        public static string connectionString {
            get {
                return WebConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            }
        }
        private static FL_Manager _instance { get; set; }
        public static FL_Manager instance {
            get {
                if (_instance==null){
                    _instance = new FL_Manager();

                }
                return _instance;
            }
        }
       
   
      

       

    }
}