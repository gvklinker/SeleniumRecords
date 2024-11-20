using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Chrome;

namespace SeleniumRecords
{
    [TestClass]
    public class UnitTest1
    {
        private static readonly string DriverDirectory = "C:\\webDrivers";
        private static IWebDriver _driver;

        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            _driver = new ChromeDriver(DriverDirectory);

            //_driver = new EdgeDriver(DriverDirectory);
 
        }

        [ClassCleanup]
        public static void TearDown()
        {
            _driver.Dispose();
        }

        [TestMethod]
        public void TestMethod1()
        {
            string url = "file:\\C:\\Users\\Gustav\\OneDrive - Zealand\\REST\\Prog\\CD\\index.html";
            _driver.Navigate().GoToUrl(url);

            string title = _driver.Title;
            Assert.AreEqual("DR Records", title);

            IWebElement buttonElement = _driver.FindElement(By.Id("getAllButton"));
            buttonElement.Click();

            //IWebElement carList = _driver.FindElement(By.Id("carlist")); // No such element

            //pause(60); // NOT good, will always wait full 60 sec

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10)); // decorator pattern?
            IWebElement carList = wait.Until(d => d.FindElement(By.Id("recordlist")));
            Assert.IsTrue(carList.Text.Contains("sang2"));

            // We already did the waiting in the previous lines, so now we can go back to using the ordinary driver
            ReadOnlyCollection<IWebElement> listElements = _driver.FindElements(By.TagName("li"));
            Assert.AreEqual(2, listElements.Count);

            Assert.IsTrue(listElements[0].Text.Contains("sang1"));

            // XPath, an advanced option to use By.XPath(...)
            // https://www.guru99.com/handling-dynamic-selenium-webdriver.html
        }
        [TestMethod]
        public void TestMethod2()
        {
            Assert.Fail();
        }
    }
}