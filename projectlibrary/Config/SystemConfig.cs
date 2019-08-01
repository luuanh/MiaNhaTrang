using System;
using System.Web.Configuration;

namespace ProjectLibrary.Config
{
    public class SystemConfig
    {
        public static String ConnectionString = WebConfigurationManager.ConnectionStrings[1].ConnectionString;
    }
}