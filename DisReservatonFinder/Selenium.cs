using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace DisReservatonFinder
{
    [TestFixture]
    public class Selenium
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        private int _month = 9;
        private Stopwatch _stopwatch = new Stopwatch();
        private TimeSpan _lastElapse = new TimeSpan();

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
                //driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test, Explicit]
        public void Priority_01_BeOurGuest_MK1_Breakfast()
        {
            SearchCriteria searchCriteria = new SearchCriteria
            {
                FirstDateToSearch = new DateTime(2016, _month, 12),
                LastDateToSearch = new DateTime(2016, _month, 13),
                RestaurantsToSearch = new Dictionary<string, string>
                {
                    {"16660079", "Be Our Guest"},
                },
                TimesToSearch = new[]
                {
                    "07:30",
                    "08:00",
                    "08:30",
                    "09:00",
                    "80000712", // Breakfast
                    //"80000717", // Lunch
                    //"80000714", // Dinner
                }
            };


            SearchForReservation(searchCriteria.FirstDateToSearch, searchCriteria.LastDateToSearch,
                searchCriteria.TimesToSearch, searchCriteria.RestaurantsToSearch);
        }

        [Test, Explicit]
        public void Priority_02_BeOurGuest_Dinner()
        {
            SearchCriteria searchCriteria = new SearchCriteria
            {
                FirstDateToSearch = new DateTime(2016, _month, 18),
                LastDateToSearch = new DateTime(2016, _month, 18),
                RestaurantsToSearch = new Dictionary<string, string>
                {
                    {"16660079", "Be Our Guest"},
                },
                TimesToSearch = new[]
                {
                    "18:30",
                    "19:00",
                    "18:00",
                    "17:30",
                    "17:00",
                    "16:30",
                    "16:00",
                    "80000714", // Dinner
                }
            };


            SearchForReservation(searchCriteria.FirstDateToSearch, searchCriteria.LastDateToSearch,
                searchCriteria.TimesToSearch, searchCriteria.RestaurantsToSearch);
        }

        [Test, Explicit]
        public void Priority_03_CinderellaRoyalTable_Dinner()
        {
            SearchCriteria searchCriteria = new SearchCriteria
            {
                FirstDateToSearch = new DateTime(2016, _month, 12),
                LastDateToSearch = new DateTime(2016, _month, 12),
                RestaurantsToSearch = new Dictionary<string, string>
                {
                    {"90002464", "Cinderella's Royal Table"},
                },
                TimesToSearch = new[]
                {
                    "18:00",
                    "18:30",
                    "19:00",
                    "19:30",
                    "20:00",
                    "20:30",
                    "17:30",
                    "21:00",
                    "17:00",
                    "16:30",
                    "80000714", // Dinner
                }
            };


            SearchForReservation(searchCriteria.FirstDateToSearch, searchCriteria.LastDateToSearch,
                searchCriteria.TimesToSearch, searchCriteria.RestaurantsToSearch);
        }

        [Test, Explicit]
        public void Priority_99_CinderellaRoyalTable_Test()
        {
            SearchCriteria searchCriteria = new SearchCriteria
            {
                FirstDateToSearch = new DateTime(2016, _month, 01),
                LastDateToSearch = new DateTime(2016, _month, 30),
                RestaurantsToSearch = new Dictionary<string, string>
                {
                    {"90002464", "Cinderella's Royal Table"},
                },
                TimesToSearch = new[]
                {
                    "80000714", // Dinner
                }
            };


            SearchForReservation(searchCriteria.FirstDateToSearch, searchCriteria.LastDateToSearch,
                searchCriteria.TimesToSearch, searchCriteria.RestaurantsToSearch);
        }

        private void SearchForReservation(DateTime firstDateToSearch, DateTime lastDateToSearch, string[] timesToSearch, Dictionary<string, string> restaurants)
        {
            _stopwatch.Start();

            LogInToSite();

            GoToReservationPage(10000);

            DateTime dateToSearch = firstDateToSearch;

            bool firstPass = true;

            while (dateToSearch <= lastDateToSearch)
            {
                foreach (var timeToSearch in timesToSearch)
                {
                    try
                    {
                        DoSearch(dateToSearch, timeToSearch, !firstPass);
                        firstPass = false;
                    }
                    catch (Exception)
                    {
                        try
                        {
                            Thread.Sleep(10000);
                            DoSearch(dateToSearch, timeToSearch);
                            firstPass = false;
                        }
                        catch (Exception)
                        {
                            Thread.Sleep(10000);
                            DoSearch(dateToSearch, timeToSearch);
                            firstPass = false;
                        }
                    }

                    Thread.Sleep(10000);
                    firstPass = false;

                    PrintSearchResults(restaurants, dateToSearch, timeToSearch);
                }

                dateToSearch = dateToSearch.AddDays(1);
            }
        }

        private void PrintSearchResults(Dictionary<string, string> restaurants, DateTime dateToSearch, string timeToSearch)
        {
            foreach (var restaurant in restaurants)
            {
                
                Console.WriteLine(
                    $"{restaurant.Value} - Search for {dateToSearch.ToString("MM/dd/yyyy")} - {timeToSearch} for 5 People - Total Time: {_stopwatch.Elapsed} - Interval: {_stopwatch.Elapsed - _lastElapse}");

                _lastElapse = _stopwatch.Elapsed;

                string timeSlot1 = "";
                string timeSlot2 = "";
                string timeSlot3 = "";

                try
                {
                    timeSlot1 =
                        driver.FindElement(
                            By.XPath(
                                $"//*[@id=\"withAvailability-alpha-default\"]/li[@data-entityid=\"{restaurant.Key};entityType=restaurant\"]/div[1]/div[1]/div/div[2]/div[2]/span[1]/span/span"))
                            .Text;
                }
                catch (Exception)
                {
                }
                try
                {
                    timeSlot2 =
                        driver.FindElement(
                            By.XPath(
                                $"//*[@id=\"withAvailability-alpha-default\"]/li[@data-entityid=\"{restaurant.Key};entityType=restaurant\"]/div[1]/div[1]/div/div[2]/div[2]/span[2]/span/span"))
                            .Text;
                }
                catch (Exception)
                {
                }
                try
                {
                    timeSlot3 =
                        driver.FindElement(
                            By.XPath(
                                $"//*[@id=\"withAvailability-alpha-default\"]/li[@data-entityid=\"{restaurant.Key};entityType=restaurant\"]/div[1]/div[1]/div/div[2]/div[2]/span[3]/span/span"))
                            .Text;
                }
                catch (Exception)
                {
                }

                if (!string.IsNullOrEmpty(timeSlot1))
                    Console.WriteLine($"{dateToSearch.ToString("MM/dd/yyyy")} - {timeSlot1}");
                else
                    Console.WriteLine("No reservations available");

                if (!string.IsNullOrEmpty(timeSlot2))
                    Console.WriteLine($"{dateToSearch.ToString("MM/dd/yyyy")} - {timeSlot2}");

                if (!string.IsNullOrEmpty(timeSlot3))
                    Console.WriteLine($"{dateToSearch.ToString("MM/dd/yyyy")} - {timeSlot3}");

                Console.WriteLine();
            }
        }

        private void GoToReservationPage(int timeWait = 10000)
        {
            driver.Navigate().GoToUrl("https://disneyworld.disney.go.com/dining/#/reservations-accepted");
            Thread.Sleep(timeWait);
            try
            {
                driver.FindElement(By.CssSelector("div.selectedFacetContainer.bar")).Click();
                driver.FindElement(By.Id("diningAvailabilityForm-searchDate")).Clear();
            }
            catch (Exception)
            {
                Thread.Sleep(10000);
                driver.FindElement(By.CssSelector("div.selectedFacetContainer.bar")).Click();
                driver.FindElement(By.Id("diningAvailabilityForm-searchDate")).Clear();
            }
            
        }

        private void LogInToSite()
        {
            driver.Navigate().GoToUrl("https://disneyworld.disney.go.com/authentication/logout/");
            driver.FindElement(By.LinkText("Sign In or Create Account")).Click();
            try
            {
                driver.FindElement(By.Id("loginPageUsername")).Clear();
            }
            catch (Exception)
            {
                Thread.Sleep(1000);
                driver.FindElement(By.Id("loginPageUsername")).Clear();
            }
            driver.FindElement(By.Id("loginPageUsername")).Clear();
            driver.FindElement(By.Id("loginPageUsername")).SendKeys("sng2005@gmail.com");
            driver.FindElement(By.Id("loginPagePassword")).Clear();
            driver.FindElement(By.Id("loginPagePassword")).SendKeys("abcd1234");
            driver.FindElement(By.Id("loginPageSubmitButton")).Click();
        }

        private void DoSearch(DateTime dateToSearch, string timeToSearch, bool openNewTab = true)
        {
            if (openNewTab)
                OpenNewTab();

            GoToReservationPage(4000);

            EnterPartySize();

            EnterSearchTime(timeToSearch);

            EnterSearchDate(dateToSearch);

            driver.FindElement(By.CssSelector("span.buttonText")).Click();
        }

        private void EnterPartySize()
        {
            try
            {
                driver.FindElement(By.Id("partySize")).FindElement(By.CssSelector("option[value='5']")).Click();
            }
            catch (Exception)
            {
                driver.FindElement(By.Id("partySize-wrapper")).Click();
                Thread.Sleep(1000);
                driver.FindElement(By.XPath($"//ol[@id=\"partySize-dropdown-list\"]/li[@data-value=\"5\"]")).Click();
            }
        }

        private void EnterSearchTime(string timeToSearch)
        {
            try
            {
                driver.FindElement(By.Id("diningAvailabilityForm-searchTime"))
                    .FindElement(By.CssSelector($"option[value='{timeToSearch}']"))
                    .Click();
            }
            catch (Exception)
            {
                driver.FindElement(By.Id("searchTime-wrapper")).Click();
                Thread.Sleep(1000);
                driver.FindElement(By.XPath($"//li[@data-value=\"{timeToSearch}\"]")).Click();
            }
        }

        private void EnterSearchDate(DateTime dateToSearch)
        {
            driver.FindElement(By.Id("diningAvailabilityForm-searchDate")).Clear();
            Thread.Sleep(1000);
            driver.FindElement(By.Id("diningAvailabilityForm-searchDate"))
                .SendKeys(dateToSearch.ToString("MM/dd/yyyy"));
        }

        private void OpenNewTab()
        {
            driver.FindElement(By.CssSelector("body")).SendKeys(Keys.Control + "t");
            driver.SwitchTo().Window(driver.CurrentWindowHandle);
            //driver.Navigate().GoToUrl("https://disneyworld.disney.go.com/dining/#/reservations-accepted");
            //driver.SwitchTo().Window(driver.CurrentWindowHandle);
            //driver.FindElement(By.CssSelector("body")).SendKeys(Keys.Enter);
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

    public class SearchCriteria
    {
        public DateTime FirstDateToSearch { get; set; }
        public DateTime LastDateToSearch { get; set; }
        public Dictionary<string, string> RestaurantsToSearch { get; set; }
        public string[] TimesToSearch { get; set; }
    }
}


//SearchCriteria searchCriteria = new SearchCriteria
//{
//    FirstDateToSearch = new DateTime(2016, _month, 12),
//    LastDateToSearch = new DateTime(2016, _month, 13),
//    RestaurantsToSearch = new Dictionary<string, string>
//                {
//                    {"16660079", "Be Our Guest"},
//                    //{ "90001320", "Boma"},
//                },
//    TimesToSearch = new[]
//                {
//                    "08:00",
//                    "08:30",
//                    "09:00",
//"80000717", // Lunch
//"80000714", // Dinner
//                    //"16:00",
//                    //"16:00",
//                    //"17:00",
//                    //"17:00",
//                    //"18:00",
//                    //"18:30",
//                    //"19:00",
//                    //"19:30",
//                    //"20:00",
//                    //"20:30",
//                    //"21:00",
//                    //"21:30",
//                    //"22:00",
//                    //"22:30"
//                }
//};