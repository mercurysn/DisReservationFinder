using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace DisReservatonFinder
{
    [TestFixture]
    public class Selenium
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "https://stage-admin.domain.com.au/";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void TheSeleniumTest()
        {
            driver.Navigate().GoToUrl("https://disneyworld.disney.go.com/authentication/logout/");
            // ERROR: Caught exception [ERROR: Unsupported command [selectWindow | null | ]]
            driver.FindElement(By.LinkText("Sign In or Create Account")).Click();
            driver.FindElement(By.Id("loginPageUsername")).Clear();
            driver.FindElement(By.Id("loginPageUsername")).SendKeys("sng2005@gmail.com");
            driver.FindElement(By.Id("loginPagePassword")).Clear();
            driver.FindElement(By.Id("loginPagePassword")).SendKeys("abcd1234");
            driver.FindElement(By.Id("loginPageSubmitButton")).Click();
            driver.FindElement(By.LinkText("Make Reservations")).Click();
            driver.FindElement(By.CssSelector("div.selectedFacetContainer.bar")).Click();
            driver.FindElement(By.CssSelector("#searchTime-wrapper > div.select-toggle.hoverable")).Click();
            driver.FindElement(By.Id("diningAvailabilityForm-searchDate")).Clear();
            driver.FindElement(By.Id("diningAvailabilityForm-searchDate")).SendKeys("09/09/2016");
            driver.FindElement(By.CssSelector("#diningAvailabilityForm-searchTime-6 > span.rawOption")).Click();
            driver.FindElement(By.CssSelector("#partySize-wrapper > div.select-toggle.hoverable")).Click();
            driver.FindElement(By.Id("partySize-4")).Click();
            driver.FindElement(By.CssSelector("span.buttonText")).Click();
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
