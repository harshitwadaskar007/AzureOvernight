using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutomationScripts.Logger
{
    public class ExtentLoggerReport
    {
        public ExtentHtmlReporter htmlReporter;
        public ExtentReports extent;
        ExtentTest test;
        public static bool isPass;
        private string filePath;
        private string outputPath;

        public ExtentLoggerReport(TestContext testContext, string user, string env, string releaseVer)
        {
            outputPath = testContext.TestDirectory;
            filePath = $"{testContext.TestDirectory}\\{testContext.Test.ClassName}.html";
            htmlReporter = new ExtentHtmlReporter(filePath);
            extent = new ExtentReports();
            extent.AddSystemInfo("User", user);
            extent.AddSystemInfo("Environment", env);
            extent.AddSystemInfo("Release Version", releaseVer);
        }

        public ExtentTest InitializeTestReporting(string testName)
        {
            extent.AttachReporter(htmlReporter);
            test = extent.CreateTest(testName);
            isPass = true;

            return test;
        }

        public void Logger(string message)
        {
            test.Log(Status.Info, message);
        }

        public void AssertIsTrue(bool condition, string message)
        {
            try
            {
                Assert.IsTrue(condition);
                test.Log(Status.Pass, message);
                test.Pass("Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath("screenshot.png").Build());
                test.Pass("Screenshot").AddScreenCaptureFromPath("screenshot.png");
            }
            catch (Exception ex)
            {
                isPass = false;
                test.Log(Status.Fail, message);
                test.Log(Status.Fail, ex.ToString());
                //test.Fail("Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath("D:\\One Bupa Provider\\Automation Report\\screenshot.png").Build());
                //test.Fail("Screenshot").AddScreenCaptureFromPath("D:\\One Bupa Provider\\Automation Report\\screenshot.png");
                throw;
            }
        }


        public void AttachScreenshot(string filename)
        {
            test.Info("Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath(filename).Build());
            test.Info("Screenshot").AddScreenCaptureFromPath(filename);
        }

        public void Flush()
        {
            extent.Flush();
        }

        public void CopyToOutputDirectory()
        {
            /*var file = new FileInfo(filePath);
            var trxLocation = Path.GetFullPath(Path.Combine(outputPath, "..\\.."));
            File.Copy(filePath, $"{trxLocation}\\{file.Name}");*/
        }
    }
}
