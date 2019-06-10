using AutomationScripts.Logger;
using AutoTest;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoTest
{
    [TestFixture]
    public class ScriptClass : Base
    {
        private TestContext testContext;
        private const string TestCategory = "Run";
        protected ExtentTest _test;
        
        [OneTimeSetUp]
        public void BeforeTest()
        {
            try
            {
                testContext = TestContext.CurrentContext;
                _extent = new ExtentReports();
                ReportLog = new ExtentLoggerReport(testContext, Config.UserLogger, Config.EnvLogger, Config.ReleaseLogger);

            }
            catch (Exception e)
            {
                throw (e);
            }
        }

        [SetUp]
        public void BeforeSetup()
        {
            testContext = TestContext.CurrentContext;
            _test = _extent.CreateTest(testContext.Test.MethodName);
            TestLog = ReportLog.InitializeTestReporting(testContext.Test.MethodName);
            driver = Initialize(testContext);
        }
        
        [Category(TestCategory)]
        [Test]
        public void FirstTest()
        {
            driver.Navigate().GoToUrl("https://www.google.com/");
            ReportLog.AssertIsTrue(driver.Url.Contains("google"), "we are sucessfully navigated to Google home page");
            driver.Close();
        }

        [Category(TestCategory)]
        [Test]
        public void SecondTest()
        {
            driver.Navigate().GoToUrl("https://www.google.com/");
            ReportLog.AssertIsTrue(driver.Url.Contains("google"), "we are sucessfully navigated to Google home page");
            driver.Close();
        }

        [Category(TestCategory)]
        [Test]
        public void ThirdTest()
        {
            driver.Navigate().GoToUrl("https://www.pulse.datamatics.com/");
            ReportLog.AssertIsTrue(driver.Url.Contains("google"), "we are sucessfully navigated to Google home page");
            driver.Close();
        }
        
        [OneTimeTearDown]
        public void AfterClass()
        {
            try
            {
                _extent.Flush();
            }
            catch (Exception e)
            {
                throw (e);
            }
            driver.Quit();
        }

        [TearDown]
        public void Cleanup()
        {
            if (testContext.Result.Outcome.Status.ToString() == "Passed")
            {
                TestLog.Log(Status.Pass, "Test passed");
            }
            else if (testContext.Result.Outcome.Status.ToString() == "Failed")
            {
                TestLog.Log(Status.Fail, "Test failed");

                try
                {
                    TestLog.Log(Status.Fail, "Error Message" + GetErrorMessage(context));
                }
                catch (Exception)
                {
                    TestLog.Log(Status.Fail, "Unable to capture error message");
                }

                try
                {
                    TestLog.Log(Status.Fail, "Stack Trace: " + GetStackTrace(context));
                }
                catch (Exception)
                {
                    TestLog.Log(Status.Fail, "Unable to capture stack trace");
                }
            }

            //Screenshot(TestContext);
            //Common.CloseApplication();
            ReportLog.Flush();
        }

       
        private string GetStackTrace(TestContext testContext)
        {
            const BindingFlags privateGetterFlags = System.Reflection.BindingFlags.GetField |
            System.Reflection.BindingFlags.GetProperty |
            System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.FlattenHierarchy;

            // Get hold of TestContext.m_currentResult.m_errorInfo.m_stackTrace (contains the stack trace details from log)
            var field = testContext.GetType().GetField("m_currentResult", privateGetterFlags);
            if (field == null)
            {
                throw new InvalidOperationException("Unable to get stack trace");
            }
            var m_currentResult = field.GetValue(testContext);
            field = m_currentResult.GetType().GetField("m_errorInfo", privateGetterFlags);
            if (field == null)
            {
                throw new InvalidOperationException("Unable to get stack trace");
            }
            var m_StackTrace = field.GetValue(m_currentResult);
            field = m_StackTrace.GetType().GetField("m_stackTrace", privateGetterFlags);
            if (field == null)
            {
                throw new InvalidOperationException("Unable to get stack trace");
            }
            return (string)field.GetValue(m_StackTrace);

        }

        private string GetErrorMessage(TestContext testContext)
        {
            const BindingFlags privateGetterFlags = System.Reflection.BindingFlags.GetField |
            System.Reflection.BindingFlags.GetProperty |
            System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.Instance |
            System.Reflection.BindingFlags.FlattenHierarchy;

            // Get hold of TestContext.m_currentResult.m_errorInfo.m_message (contains the exception text that was thrown)
            var field = testContext.GetType().GetField("m_currentResult", privateGetterFlags);
            if (field == null)
            {
                throw new InvalidOperationException("Unable to get error message");
            }
            var m_currentResult = field.GetValue(testContext);
            field = m_currentResult.GetType().GetField("m_errorInfo", privateGetterFlags);
            if (field == null)
            {
                throw new InvalidOperationException("Unable to get error message");
            }
            var m_errorInfo = field.GetValue(m_currentResult);
            field = m_errorInfo.GetType().GetField("m_message", privateGetterFlags);
            if (field == null)
            {
                throw new InvalidOperationException("Unable to get error message");
            }
            return (string)field.GetValue(m_errorInfo);

        }


    }
}
