using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nunit_WebTest_Selenium.Utility
{
    [Parallelizable(ParallelScope.All)]
    public class BaseTest
    {
        private static readonly ThreadLocal<IWebDriver?> _driver = new ();
        private static readonly ThreadLocal<PageManager> _pages = new ();
        protected IWebDriver Driver => _driver.Value!;
        protected PageManager pages => _pages.Value!;
        protected string BaseUrl => TestContext.Parameters.Get("BaseUrl");

        [SetUp]
        public void Setup()
        {
            var driver = DriverFactory.CreateDriver();
            _driver.Value = driver;
            _pages.Value = new PageManager(Driver);
            driver.Navigate().GoToUrl(BaseUrl);
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
                var browserLogs = Driver.Manage().Logs.GetLog(LogType.Browser)
    .Where(l => l.Level == LogLevel.Severe)
    .Select(l => l.Message);

                foreach (var err in browserLogs)
                    TestReportTracker.Log($"[JS Console Error] {err}");

                // Take screenshot only if the current thread's test failed
                if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed && _driver.Value != null)
                {
                    CaptureScreenshot(TestContext.CurrentContext.Test.Name);
                }
            }
            finally
            {
                if (_driver.Value != null)
                {
                    _driver.Value.Quit();
                    _driver.Value.Dispose();
                    _driver.Value = null;
                }
                _pages.Value = null;
            }
        }

        private void CaptureScreenshot(string testName)
        {
            var folder = Path.Combine(TestContext.CurrentContext.WorkDirectory, "Screenshots");
            Directory.CreateDirectory(folder); // Thread-safe even if multiple threads call it simultaneously

            // Sanitize test name (parameterized tests contain quotes/parentheses)
            var safeName = string.Concat(testName.Split(Path.GetInvalidFileNameChars()));
            var fileName = $"{safeName}_{DateTime.Now:yyyyMMdd_HHmmss_fff}.png";
            var fullPath = Path.Combine(folder, fileName);

            var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
            screenshot.SaveAsFile(fullPath);

            // Attaches screenshot directly to NUnit / Test Explorer / CI report!
            TestContext.AddTestAttachment(fullPath, "Failure Screenshot");

        }
    }
}
