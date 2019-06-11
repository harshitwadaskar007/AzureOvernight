using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoTest
{
    public partial class ObjectFactory
    {
        public static IWebDriver driver;
        protected ExtentReports _extent;
        public IWebDriver Initialize(TestContext testContext)
        {
            try
            {
                //To create report directory and add HTML report into it
                var driverPath = Path.Combine(testContext.TestDirectory, Config.DriverPathNew);
                driverPath = Path.GetFullPath(driverPath);

                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var chromeOptions = new ChromeOptions();
                chromeOptions.AddAdditionalCapability("useAutomationExtension", false);
                //chromeOptions.AddArguments("headless");
                driver = new ChromeDriver(path, chromeOptions);
            }
            catch (Exception e)
            {
                throw (e);
            }

            Extention.driver = driver;
            return driver;
        }

        

        #region Lazy Loading
        Lazy<ConfigData> lazyConfigData = new Lazy<ConfigData>(() =>
        {
            ConfigData internalObject = new ConfigData();
            return internalObject;
        });
        
        #endregion

        #region Page Object Instances

        public ConfigData Config { get { return lazyConfigData.Value; } }
        #endregion

    }
}