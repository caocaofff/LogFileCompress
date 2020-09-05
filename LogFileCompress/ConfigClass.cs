using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace LogFileCompress
{
    class ConfigClass
    {
        public static string LogFilePath = ConfigurationManager.ConnectionStrings["LogFilePath"].ConnectionString;

        public static string Days = ConfigurationManager.ConnectionStrings["Days"].ConnectionString;

        public static string TargetPath = ConfigurationManager.ConnectionStrings["TargetPath"].ConnectionString;

        public static string IsCompress = ConfigurationManager.ConnectionStrings["IsCompress"].ConnectionString;

        public static string IsDeleteDirectory = ConfigurationManager.ConnectionStrings["IsDeleteDirectory"].ConnectionString;
       
    }
}
