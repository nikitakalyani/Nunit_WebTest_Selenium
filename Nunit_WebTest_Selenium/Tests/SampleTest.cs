using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using Nunit_WebTest_Selenium.Utility;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Log;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Xml.Linq;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using static System.Net.Mime.MediaTypeNames;
using CategoryAttribute = NUnit.Framework.CategoryAttribute;

namespace Nunit_WebTest_Selenium.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    public class Tests : BaseTest
    {
        [Test]
        [Category("Smoke")]
        [Retry(2)] // Automatically retries once if the first run fails
        public void Test1()
        {

            //var searchBar =  driver.FindElement(By.XPath("//input[@id='twotabsearchtextbox']"));
            //searchBar.SendKeys("Mobile");

            // var loginPage = new LoginPageTest(Driver);
            pages.homePageTest.SearchText("testuser")
                .ClickSearch();

            Assert.That(Driver.Title, Does.Contain("Dashboard"));
        }
        [Test]
        public void Test2()
        {

            //var searchBar =  driver.FindElement(By.XPath("//input[@id='twotabsearchtextbox']"));
            //searchBar.SendKeys("Mobile");

            // var loginPage = new LoginPageTest(Driver);
            //Driver.Navigate..GoToUrl

            pages.homePageTest.SearchText("testuser")
                .ClickSearch();

            Assert.That(Driver.Title, Does.Contain("Dashboard"));
        }
        [Test]
        public void Test3()
        {

            //var searchBar =  driver.FindElement(By.XPath("//input[@id='twotabsearchtextbox']"));
            //searchBar.SendKeys("Mobile");

            // var loginPage = new LoginPageTest(Driver);
            pages.homePageTest.SearchText("testuser")
                .ClickSearch();

            Assert.That(Driver.Title, Does.Contain("Dashboard"));
        }
    }
}



//• Inheritance: LoginPage and DashboardPage inherit from BasePage, and your test classes (LoginTests) inherit from BaseTest. This lets subclasses reuse common driver setup and element wrapper methods.
//• Encapsulation: Page locators (private readonly By _username = ...) and internal wait logic (private IWebElement GetElement(...)) are marked private, hiding DOM details from test classes and exposing only clean business actions (Login(...)).
//• Abstraction: BasePage hides low-level Selenium complexity (WebDriverWait, FindElements, Clear(), SendKeys()) behind simple, high-level methods like EnterText and Click.

//1. Object-Oriented Programming (OOP) Concepts

//• Inheritance: LoginPage and DashboardPage inherit from BasePage, and your test classes (LoginTests) inherit from BaseTest. This lets subclasses reuse common driver setup and element wrapper methods.
//• Encapsulation: Page locators (private readonly By _username = ...) and internal wait logic (private IWebElement GetElement(...)) are marked private, hiding DOM details from test classes and exposing only clean business actions (Login(...)).
//• Abstraction: BasePage hides low-level Selenium complexity (WebDriverWait, FindElements, Clear(), SendKeys()) behind simple, high-level methods like EnterText and Click.

//2. SOLID Principles

//• Single Responsibility Principle (SRP): Every layer has one clear job:
//	• BaseTest manages browser lifecycle ([SetUp] / [TearDown]).
//	• BasePage handles low-level element waits and interactions.

//	• LoginPage / DashboardPage represent page-specific locators and user flows.
//	• LoginTests handles test steps and assertions.
//• Open/Closed Principle (OCP): BasePage and BaseTest are open for extension (you can add new Page Objects and Test Fixtures by inheriting from them) without modifying their existing core code.
//• Dependency Inversion Principle (DIP): Your Page Objects (LoginPage(IWebDriver driver)) and BasePage depend on the IWebDriver interface (abstraction) rather than a concrete class like ChromeDriver. This means you can switch to EdgeDriver or FirefoxDriver later without changing a single line in your Page Objects.

//3. Dependency Injection (DI) & Design Patterns

//• Constructor Injection (Manual DI): Passing IWebDriver into PageManager(IWebDriver driver) and LoginPage(IWebDriver driver) : base(driver) injects the browser dependency from the outside instead of having pages create their own driver instances.
//• Page Object Model (POM): Separating UI locators/interactions from test assertions.
//• Lazy Initialization Pattern (??=): Used in PageManager (public LoginPage Login => _loginPage ??= new LoginPage(_driver);) so page objects are created only on first access.
//• Fluent Interface / Method Chaining: Returning this or new DashboardPage(Driver) from action methods so calls can be chained cleanly (Pages.Login.EnterUsername(...).EnterPassword(...).ClickLogin()).

//4. Modern C# Language Features

//• Lambda Expressions & Delegates (Func<IWebDriver, IWebElement?>): Used inside _wait.Until(d => { ... }). WebDriverWait.Until accepts a Func<T, TResult> delegate, and your lambda expression provides the polling condition inline.
//• LINQ (FirstOrDefault, ElementAtOrDefault, Select): Used to safely query collections returned by FindElements(...) without throwing index or empty-sequence exceptions.
//• Optional & Named Parameters: Used in EnterText(By locator, string text, int index = 0) and Click(_actionButtons, index: 1) to keep common calls concise while supporting extra options.
//• Expression-Bodied Members (=>): Used for concise properties and single-line methods (private By UsernameInput => ...).
//• Null-Coalescing Assignment (??=) & Null-Conditional Operator (?.): Used in PageManager for lazy loading and in TearDown (Driver?.Quit()) for safe cleanup.
//• String Interpolation ($"") & Raw/Escaped Strings: Used to build dynamic XPath locators cleanly.

//1. Object-Oriented Programming (OOP) Concepts

//• Inheritance: LoginPage and DashboardPage inherit from BasePage, and your test classes (LoginTests) inherit from BaseTest. This lets subclasses reuse common driver setup and element wrapper methods.
//• Encapsulation: Page locators (private readonly By _username = ...) and internal wait logic (private IWebElement GetElement(...)) are marked private, hiding DOM details from test classes and exposing only clean business actions (Login(...)).
//• Abstraction: BasePage hides low-level Selenium complexity (WebDriverWait, FindElements, Clear(), SendKeys()) behind simple, high-level methods like EnterText and Click.

//2. SOLID Principles

//• Single Responsibility Principle (SRP): Every layer has one clear job:
//	• BaseTest manages browser lifecycle ([SetUp] / [TearDown]).
//	• BasePage handles low-level element waits and interactions.
//	• LoginPage / DashboardPage represent page-specific locators and user flows.
//	• LoginTests handles test steps and assertions.
//• Open/Closed Principle (OCP): BasePage and BaseTest are open for extension (you can add new Page Objects and Test Fixtures by inheriting from them) without modifying their existing core code.
//• Dependency Inversion Principle (DIP): Your Page Objects (LoginPage(IWebDriver driver)) and BasePage depend on the IWebDriver interface (abstraction) rather than a concrete class like ChromeDriver. This means you can switch to EdgeDriver or FirefoxDriver later without changing a single line in your Page Objects.

//3. Dependency Injection (DI) & Design Patterns

//• Constructor Injection (Manual DI): Passing IWebDriver into PageManager(IWebDriver driver) and LoginPage(IWebDriver driver) : base(driver) injects the browser dependency from the outside instead of having pages create their own driver instances.
//• Page Object Model (POM): Separating UI locators/interactions from test assertions.
//• Lazy Initialization Pattern (??=): Used in PageManager (public LoginPage Login => _loginPage ??= new LoginPage(_driver);) so page objects are created only on first access.
//• Fluent Interface / Method Chaining: Returning this or new DashboardPage(Driver) from action methods so calls can be chained cleanly (Pages.Login.EnterUsername(...).EnterPassword(...).ClickLogin()).

//4. Modern C# Language Features

//• Lambda Expressions & Delegates (Func<IWebDriver, IWebElement?>): Used inside _wait.Until(d => { ... }). WebDriverWait.Until accepts a Func<T, TResult> delegate, and your lambda expression provides the polling condition inline.
//• LINQ (FirstOrDefault, ElementAtOrDefault, Select): Used to safely query collections returned by FindElements(...) without throwing index or empty-sequence exceptions.
//• Optional & Named Parameters: Used in EnterText(By locator, string text, int index = 0) and Click(_actionButtons, index: 1) to keep common calls concise while supporting extra options.
//• Expression-Bodied Members (=>): Used for concise properties and single-line methods (private By UsernameInput => ...).
//• Null-Coalescing Assignment (??=) & Null-Conditional Operator (?.): Used in PageManager for lazy loading and in TearDown (Driver?.Quit()) for safe cleanup.
//• String Interpolation ($"") & Raw/Escaped Strings: Used to build dynamic XPath locators cleanly.


//Use.runsettings for test - runner execution settings(NumberOfTestWorkers / parallelism, browser choice, environment name) and appsettings.json for rich structured application data(URLs, credentials, timeouts per environment).

//    .runsettings integrates natively with dotnet test, Visual Studio Test Explorer, and CI / CD pipelines(Azure DevOps / GitHub Actions) without writing custom JSON parsers for runner - level switches, while appsettings.json cleanly holds nested environment data.

//    Visual Studio: Top menu -> Test -> Configure Run Settings -> Select Solution Wide runsettings File -> pick test.runsettings.


//3.Checklist for Safe Parallel Execution

//• Never use static fields for test data or elements inside your Page Objects or Test classes(your private readonly By...fields are read - only and stateless, so they are 100% thread-safe).
//• Use separate test accounts/data if two tests modify the same user record at the same time (e.g., one test changing a user's password while another logs in with that same user).
//• Cap LevelOfParallelism locally: Each Chrome instance uses ~200–400 MB of RAM. Setting [assembly: LevelOfParallelism(3)] or 4 prevents your local PC from slowing down or timing out during page loads.
//    A static field belongs to the class, not to an individual thread—so every parallel test reads and overwrites the exact same memory location at the same time.
//    // ❌ BAD: Never use static mutable fields for test data or elements
//[TestFixture]
//public class OrderTests : BaseTest
//{
//    private static string _createdOrderId; // Shared across ALL threads!

//    [Test]
//    public void CreateOrderA()
//    {
//        _createdOrderId = "ORD-101"; // Thread 1 sets this...
//        // ...while Thread 1 is waiting here, Thread 2 overwrites _createdOrderId = "ORD-202"!
//        Assert.That(Pages.Orders.GetOrderTitle(_createdOrderId), Is.EqualTo("Order A")); // FAILS randomly!
//    }

//    [Test]
//    public void CreateOrderB()
//    {
//        _createdOrderId = "ORD-202"; // Thread 2 overwrites Thread 1's variable!
//        Assert.That(Pages.Orders.GetOrderTitle(_createdOrderId), Is.EqualTo("Order B"));



        
//How to Write It Safely Instead

//Keep test variables local inside the [Test] method (or non-static instance fields if [Parallelizable(ParallelScope.Fixtures)] is used, though local variables inside [Test] are always 100% thread-safe):
//csharp
//// ✅ GOOD: Local variables live on each thread's own stack
//[Test]
//public void CreateOrderA()
//{
//    string createdOrderId = "ORD-101"; // Isolated to Thread 1
//    Assert.That(Pages.Orders.GetOrderTitle(createdOrderId), Is.EqualTo("Order A"));
//}
//How to Write It Safely Instead

//Keep test variables local inside the [Test] method (or non-static instance fields if [Parallelizable(ParallelScope.Fixtures)] is used, though local variables inside [Test] are always 100% thread-safe):
//csharp
//// ✅ GOOD: Local variables live on each thread's own stack
//[Test]
//public void CreateOrderA()
//{
//    string createdOrderId = "ORD-101"; // Isolated to Thread 1
//    Assert.That(Pages.Orders.GetOrderTitle(createdOrderId), Is.EqualTo("Order A"));
//}

//    (Note: private static readonly By _loginBtn = By.Id("login"); IS safe because By is immutable and readonly—it never changes during execution.)






