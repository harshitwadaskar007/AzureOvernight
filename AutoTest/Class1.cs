using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OvernightExecution
{
    [TestFixture]
    public class Program
    {
        public static void Main(string[] args)
        {

        }

        [Category("Run")]
        [Test]
        public void FirstTest()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            IWebDriver driver = new ChromeDriver(path, chromeOptions);
            driver.Navigate().GoToUrl("https://www.google.com/");
            driver.Close();
        }

        [Category("Run")]
        [Test]
        public void SecondTest()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            IWebDriver driver = new ChromeDriver(path, chromeOptions);
            driver.Navigate().GoToUrl("https://www.google.com/");
            driver.Close();
        }

        [Category("Run")]
        [Test]
        public void ThirdTest()
        {

            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            IWebDriver driver = new ChromeDriver(path, chromeOptions);
            driver.Navigate().GoToUrl("https://www.google.com/");
            IWebElement element = driver.FindElement(By.XPath("ABCD"));
            element.Click();
            driver.Close();
        }
    }
}
