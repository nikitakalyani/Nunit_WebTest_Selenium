using Nunit_WebTest_Selenium.Utility;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nunit_WebTest_Selenium.Pages
{
    public class HomePageTest : BasePage
    {
        private readonly IWebDriver _driver;

        public HomePageTest(IWebDriver driver): base(driver)
        {
            _driver = driver;
        }

        // Locators
        private By SearchBox => By.XPath("//input[@id='twotabsearchtextbox']");
        private By SearchButton => By.XPath("//input[@id='nav-search-submit-button']");
        private By LoginButton => By.Id("loginBtn");

        // Actions
        public HomePageTest SearchText(string searchtext)
        {
            EnterText(SearchBox, searchtext);
            return this;
        }

        public HomePageTest ClickSearch()
        {
            ClickButton(SearchButton);
            return this;
        }
    }
}
