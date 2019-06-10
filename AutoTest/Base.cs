using AutomationScripts.Logger;
using AutoTest;
using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace AutoTest
{
    public abstract class Base : ObjectFactory
    {
        public static ExtentLoggerReport ReportLog { get; set; }

        public TestContext context { get; set; }
        
        public ExtentTest TestLog { get; set; }



        
    }
}
