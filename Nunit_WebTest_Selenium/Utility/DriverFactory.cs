using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nunit_WebTest_Selenium.Utility
{
    public static class DriverFactory
    {
        public static string ThreadDownloadFolder =>
    Path.Combine(TestContext.CurrentContext.WorkDirectory, "Downloads", Environment.CurrentManagedThreadId.ToString());

        public static IWebDriver CreateDriver()
        {
            string browser = TestContext.Parameters.Get("Browser");
            var chromeoptions = GetBrowserOptions(browser);
            IWebDriver driver = null;
            switch (browser) 
            {
                case "Edge":
                    driver = new EdgeDriver();
                    break;
                default:
                    driver = new ChromeDriver(chromeoptions);
                    break;
                    
            }

            driver.Manage().Window.Maximize();
            return driver;
        }

        private static ChromeOptions GetBrowserOptions(string browser)
        {
            Directory.CreateDirectory(ThreadDownloadFolder);
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("download.default_directory", ThreadDownloadFolder);
            chromeOptions.AddUserProfilePreference("download.prompt_for_download", false);
            return chromeOptions;

        }
    }
}
