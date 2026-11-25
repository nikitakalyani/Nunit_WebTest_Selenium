using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nunit_WebTest_Selenium.Utility
{
    public class BasePage
    {
        protected readonly IWebDriver Driver;
        private readonly WebDriverWait _wait;

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private IWebElement GetElement(By locator, bool mustBeEnabled, int index = 0)
        {
            var element = _wait.Until(d =>
            {
                var el = Driver.FindElements(locator).ElementAtOrDefault(index);
                if (el == null || el.Displayed)
                    return null;

                if (mustBeEnabled && !el.Enabled)
                    return null;

                return el;
            });

            return element;
        }

        public void EnterText(By Locator, string text, int index = 0)
        {
            var element = GetElement(Locator, true, index);
            element.SendKeys(text);
        }

        public void ClickButton(By Locator, int index = 0 )
        {
            var element = GetElement(Locator, true, index);
            element.Click();
        }

        protected void SelectByText(By locator, string text, int index = 0)
        {
            var select = new SelectElement(GetElement(locator, true, index));
            select.SelectByText(text);
        }

        protected void JsClick(By locator, int index = 0)
        {
            var el = GetElement(locator, true, index);
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", el);
        }

        protected void ScrollToElement(By locator, int index = 0)
        {
            var el = GetElement(locator, true, index);
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView({block:'center'});", el);
        }

        protected void Hover(By locator, int index = 0)
        {
            var el = GetElement(locator, true, index);
            new Actions(Driver).MoveToElement(el).Perform();
        }

        protected void UploadFile(By fileInputLocator, string relativeFilePath)
        {
            var fullPath = Path.Combine(TestContext.CurrentContext.TestDirectory, relativeFilePath);
            // File inputs are often hidden, so use FindElement directly instead of Displayed check
            Driver.FindElement(fileInputLocator).SendKeys(fullPath);
        }

        protected void AcceptAlert()
        {
            _wait.Until(d => { try { d.SwitchTo().Alert().Accept(); return true; } catch { return false; } });
        }

        protected void SwitchToFrame(By frameLocator) =>
            _wait.Until(d => { d.SwitchTo().Frame(d.FindElement(frameLocator)); return true; });

        protected void SwitchToDefaultContent() => Driver.SwitchTo().DefaultContent();

        protected void SwitchToLatestTab() => Driver.SwitchTo().Window(Driver.WindowHandles.Last());

    }
}
