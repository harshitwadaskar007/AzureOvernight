using System;
using System.Collections.Generic;
using System.Configuration;

namespace AutoTest
{
    public class ConfigData
    {
        public string DriverPathNew { get { return ConfigurationManager.AppSettings.Get("ChromeDriverPath"); } }
        public string UserLogger { get { return ConfigurationManager.AppSettings.Get("User"); } }
        public string EnvLogger { get { return ConfigurationManager.AppSettings.Get("Env"); } }
        public string ReleaseLogger { get { return ConfigurationManager.AppSettings.Get("Release"); } }
        
    }
}
