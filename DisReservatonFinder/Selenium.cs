using System;
using System.Collections.Generic;
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
            driver.Navigate().GoToUrl("https://disneyworld.disney.go.com/dining/#/reservations-accepted");
            //driver.FindElement(By.LinkText("Make Reservations")).Click();
            Thread.Sleep(10000);
            driver.FindElement(By.CssSelector("div.selectedFacetContainer.bar")).Click();
            //driver.FindElement(By.CssSelector("#searchTime-wrapper > div.select-toggle.hoverable")).Click();
            driver.FindElement(By.Id("diningAvailabilityForm-searchDate")).Clear();

            DateTime startDate = new DateTime(2016,9,10);
            DateTime endDate = new DateTime(2016,9,17);
            DateTime dateToSearch = startDate;

            while (dateToSearch <= endDate)
            {
                

                

                string[] timesToSearch =
                {
                    //"08:00",
                    //"08:30",
                    //"09:00",
                    //"16:00",
                    //"16:00",
                    //"17:00",
                    //"17:00",
                    //"18:00",
                    "18:30",
                    "19:00",
                    "19:30",
                    "20:00",
                    "20:30",
                    "21:00",
                    "21:30",
                    "22:00",
                    "22:30"
                };

                foreach (var timeToSearch in timesToSearch)
                {
                    try
                    {
                        DoSearch(dateToSearch, timeToSearch);
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(10000);
                        DoSearch(dateToSearch, timeToSearch);
                    }

                    Thread.Sleep(10000);
                    Dictionary<string, string> restaurants = new Dictionary<string, string>
                {
                    {"16660079", "Be Our Guest"},
                };

                    //restaurants.Add("90001320", "Boma");

                    foreach (var restaurant in restaurants)
                    {
                        Console.WriteLine($"{restaurant.Value} - Search for {dateToSearch.ToString("MM/dd/yyyy")} - {timeToSearch} for 5 People");

                        string timeSlot1 = "";
                        string timeSlot2 = "";
                        string timeSlot3 = "";

                        try { timeSlot1 = driver.FindElement(By.XPath($"//*[@id=\"withAvailability-alpha-default\"]/li[@data-entityid=\"{restaurant.Key};entityType=restaurant\"]/div[1]/div[1]/div/div[2]/div[2]/span[1]/span/span")).Text; } catch (Exception) { }
                        try { timeSlot2 = driver.FindElement(By.XPath($"//*[@id=\"withAvailability-alpha-default\"]/li[@data-entityid=\"{restaurant.Key};entityType=restaurant\"]/div[1]/div[1]/div/div[2]/div[2]/span[2]/span/span")).Text; } catch (Exception) { }
                        try { timeSlot3 = driver.FindElement(By.XPath($"//*[@id=\"withAvailability-alpha-default\"]/li[@data-entityid=\"{restaurant.Key};entityType=restaurant\"]/div[1]/div[1]/div/div[2]/div[2]/span[3]/span/span")).Text; } catch (Exception) { }

                        if (!string.IsNullOrEmpty(timeSlot1))
                            Console.WriteLine($"{dateToSearch.ToString("MM/dd/yyyy")} - {timeSlot1}");

                        if (!string.IsNullOrEmpty(timeSlot2))
                            Console.WriteLine($"{dateToSearch.ToString("MM/dd/yyyy")} - {timeSlot2}");

                        if (!string.IsNullOrEmpty(timeSlot3))
                            Console.WriteLine($"{dateToSearch.ToString("MM/dd/yyyy")} - {timeSlot3}");

                        Console.WriteLine();
                    }
                }
                

                dateToSearch = dateToSearch.AddDays(1);
            }



        }

        private void DoSearch(DateTime dateToSearch, string timeToSearch)
        {
            driver.FindElement(By.CssSelector("body")).SendKeys(Keys.Control + "t");

            
            driver.FindElement(By.Id("diningAvailabilityForm-searchDate")).Clear();
            driver.FindElement(By.Id("diningAvailabilityForm-searchDate"))
                .SendKeys(dateToSearch.ToString("MM/dd/yyyy"));
            driver.FindElement(By.Id("diningAvailabilityForm-searchTime"))
                .FindElement(By.CssSelector($"option[value='{timeToSearch}']"))
                .Click();
            driver.FindElement(By.Id("partySize")).FindElement(By.CssSelector("option[value='5']")).Click();
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
