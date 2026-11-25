using Nunit_WebTest_Selenium.Pages;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nunit_WebTest_Selenium.Utility
{   
    public class PageManager
    {
        private readonly IWebDriver _driver;
        private HomePageTest? _loginPageTest;

        public PageManager(IWebDriver driver)
        {
            _driver = driver; 
        }

        public HomePageTest homePageTest => _loginPageTest ??= new HomePageTest(_driver);
    }
}
