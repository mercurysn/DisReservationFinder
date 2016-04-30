using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        private int _month = 10;
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private TimeSpan _lastElapse = new TimeSpan();
        private bool _retainWindow = false;
        private bool _isLoggedIn = false;
        private int _numberOfReservationFound = 0;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            driver.Manage().Window.Maximize();
            baseURL = "https://stage-admin.domain.com.au/";
            verificationErrors = new StringBuilder();
            
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                if (!_retainWindow)
                    driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void Priority_00_SearchForEverything()
        {
            SearchForEverything();
            //SearchForVanDinner();

            Console.WriteLine($"Total Reservation Found: {_numberOfReservationFound}");
        }

        public void SearchForEverything()
        {
            SearchForBogBreakfast();
            //SearchForBOGDinner();
            SearchForCRTDinner();
            SearchForCRTDinnerBackups();
            SearchForBCDinner();
            SearchForFantasmicPackageDay1();
            SearchForFantasmicPackageDay2();
            SearchForOhanaDinner();
            SearchForVanDinner();
        }

        [Test, Explicit]
        public void Priority_01_02_BeOurGuest_MK1_Breakfast()
        {
            SearchForBogBreakfast();
        }

        private void SearchForBogBreakfast()
        {
            SearchCriteria searchCriteria = new SearchCriteria
            {
                FirstDateToSearch = new DateTime(2016, _month, 12),
                LastDateToSearch = new DateTime(2016, _month, 12),
                RestaurantsToSearch = new Dictionary<string, string>
                {
                    {"16660079", "Be Our Guest"},
                },
                TimesToSearch = new[]
                {
                    "08:00",
                    //"08:30",
                    //"80000717", // Lunch
                    //"80000714", // Dinner
                }
            };


            SearchForReservation(searchCriteria.FirstDateToSearch, searchCriteria.LastDateToSearch,
                searchCriteria.TimesToSearch, searchCriteria.RestaurantsToSearch, new DateTime(2016, 10, 12, 8, 0, 0),
                new DateTime(2016, 10, 12, 8, 10, 0));
        }

        [Test, Explicit]
        public void Priority_03_BeOurGuest_Dinner()
        {
            SearchForBOGDinner();
        }

        private void SearchForBOGDinner()
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
                }
            };


            SearchForReservation(searchCriteria.FirstDateToSearch, searchCriteria.LastDateToSearch,
                searchCriteria.TimesToSearch, searchCriteria.RestaurantsToSearch, new DateTime(2016, 10, 18, 19, 00, 0),
                new DateTime(2016, 10, 18, 17, 55, 0));
        }

        [Test, Explicit]
        public void Priority_04_CinderellaRoyalTable_Dinnerr()
        {
            SearchForCRTDinner();
        }

        private void SearchForCRTDinnerBackups()
        {
            SearchCriteria searchCriteria = new SearchCriteria
            {
                FirstDateToSearch = new DateTime(2016, _month, 19),
                LastDateToSearch = new DateTime(2016, _month, 19),
                RestaurantsToSearch = new Dictionary<string, string>
                {
                    {"90002464", "Cinderella's Royal Table (Backup)"},
                },
                TimesToSearch = new[]
                {
                    "18:00",
                    "18:30",
                }
            };


            SearchForReservation(searchCriteria.FirstDateToSearch, searchCriteria.LastDateToSearch,
                searchCriteria.TimesToSearch, searchCriteria.RestaurantsToSearch, new DateTime(2016, 10, 19, 18, 00, 0), new DateTime(2016, 10, 19, 19, 0, 0));
        }

        private void SearchForCRTDinner()
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
                    "21:00",
                }
            };


            SearchForReservation(searchCriteria.FirstDateToSearch, searchCriteria.LastDateToSearch,
                searchCriteria.TimesToSearch, searchCriteria.RestaurantsToSearch, new DateTime(2016, 10, 12, 18, 30, 0));
        }

        [Test, Explicit]
        public void Priority_06_California_Grill_Dinner()
        {
            SearchCriteria searchCriteria = new SearchCriteria
            {
                FirstDateToSearch = new DateTime(2016, _month, 16),
                LastDateToSearch = new DateTime(2016, _month, 16),
                RestaurantsToSearch = new Dictionary<string, string>
                {
                    {"90001336", "California Grill"},
                },
                TimesToSearch = new[]
                {
                    "20:30",
                }
            };


            SearchForReservation(searchCriteria.FirstDateToSearch, searchCriteria.LastDateToSearch,
                searchCriteria.TimesToSearch, searchCriteria.RestaurantsToSearch, new DateTime(2016, 10, 16, 20, 50, 0), new DateTime(2016, 10, 16, 20, 50, 0));
        }

        [Test, Explicit]
        public void Priority_07_BeachesAndCream_Dinner()
        {
            SearchForBCDinner();
        }

        private void SearchForBCDinner()
        {
            SearchCriteria searchCriteria = new SearchCriteria
            {
                FirstDateToSearch = new DateTime(2016, _month, 10),
                LastDateToSearch = new DateTime(2016, _month, 10),
                RestaurantsToSearch = new Dictionary<string, string>
                {
                    {"90001272", "Beaches & Cream Soda Shop"},
                },
                TimesToSearch = new[]
                {
                    "19:00",
                    "19:30",
                    "20:00",
                    "20:30",
                }
            };

            SearchForReservation(searchCriteria.FirstDateToSearch, searchCriteria.LastDateToSearch,
                searchCriteria.TimesToSearch, searchCriteria.RestaurantsToSearch, new DateTime(2016, 10, 10, 19, 30, 0),
                new DateTime(2016, 10, 10, 13, 00, 0));
        }

        [Test, Explicit]
        public void Priority_08_SciFiDineInTheater_Lunch()
        {
            SearchCriteria searchCriteria = new SearchCriteria
            {
                FirstDateToSearch = new DateTime(2016, _month, 14),
                LastDateToSearch = new DateTime(2016, _month, 14),
                RestaurantsToSearch = new Dictionary<string, string>
                {
                    {"90002114", "Sci-Fi Dine-In Theater Restaurant"},
                },
                TimesToSearch = new[]
                {
                    "11:00",
                    "11:30",
                    "12:00",
                    "12:30",
                    "13:00",
                    "10:00",
                }
            };

            SearchForReservation(searchCriteria.FirstDateToSearch, searchCriteria.LastDateToSearch,
                searchCriteria.TimesToSearch, searchCriteria.RestaurantsToSearch, new DateTime(2016, 10, 14, 11, 30, 0), new DateTime(2016, 10, 14, 11, 0, 0));
        }

        [Test, Explicit]
        public void Priority_09_Ohana_Dinner()
        {
            SearchForOhanaDinner();
        }

        private void SearchForOhanaDinner()
        {
            SearchCriteria searchCriteria = new SearchCriteria
            {
                FirstDateToSearch = new DateTime(2016, _month, 13),
                LastDateToSearch = new DateTime(2016, _month, 13),
                RestaurantsToSearch = new Dictionary<string, string>
                {
                    {"90002606", "'Ohana"},
                },
                TimesToSearch = new[]
                {
                    "20:30",
                    "20:00",
                }
            };

            SearchForReservation(searchCriteria.FirstDateToSearch, searchCriteria.LastDateToSearch,
                searchCriteria.TimesToSearch, searchCriteria.RestaurantsToSearch, new DateTime(2016, 10, 13, 19, 45, 0),
                new DateTime(2016, 10, 13, 20, 30, 0));
        }

        [Test, Explicit]
        public void Priority_10_VanNapoliRistoranteEPizzeria_Dinner()
        {
            SearchForVanDinner();
        }

        private void SearchForVanDinner()
        {
            SearchCriteria searchCriteria = new SearchCriteria
            {
                FirstDateToSearch = new DateTime(2016, _month, 17),
                LastDateToSearch = new DateTime(2016, _month, 17),
                RestaurantsToSearch = new Dictionary<string, string>
                {
                    {"15525574", "Van Napoli Ristorante e Pizzeria"},
                },
                TimesToSearch = new[]
                {
                    "21:00",
                }
            };

            SearchForReservation(searchCriteria.FirstDateToSearch, searchCriteria.LastDateToSearch,
                searchCriteria.TimesToSearch, searchCriteria.RestaurantsToSearch, new DateTime(2016, 10, 17, 21, 0, 0),
                new DateTime(2016, 10, 17, 20, 25, 0));
        }

        [Test, Explicit]
        public void Priority_11_SanAngelInnRestaurante_Dinner()
        {
            SearchCriteria searchCriteria = new SearchCriteria
            {
                FirstDateToSearch = new DateTime(2016, _month, 11),
                LastDateToSearch = new DateTime(2016, _month, 11),
                RestaurantsToSearch = new Dictionary<string, string>
                {
                    {"90002100", "San Angel Inn Restaurante"},
                },
                TimesToSearch = new[]
                {
                    "19:30",
                    "19:00",
                    "18:30",
                    "18:00",
                    "17:30",
                }
            };

            SearchForReservation(searchCriteria.FirstDateToSearch, searchCriteria.LastDateToSearch,
                searchCriteria.TimesToSearch, searchCriteria.RestaurantsToSearch, new DateTime(), new DateTime());
        }

        [Test, Explicit]
        public void TestEmail()
        {
            //EmailSender.SendEmail("Beaches & Cream", "10/20/2016", "20:00", $"Beaches & Cream - Search for 10/20/2016 - 20:00 for 5 People");
        }

        [Test, Explicit]
        public void Priority_05_Fantasmic_Package()
        {
            SearchForFantasmicPackageDay1();
        }

        private void SearchForFantasmicPackageDay1()
        {
            SearchCriteria searchCriteria = new SearchCriteria
            {
                FirstDateToSearch = new DateTime(2016, _month, 14),
                LastDateToSearch = new DateTime(2016, _month, 14),
                RestaurantsToSearch = new Dictionary<string, string>
                {
                    {"17736028", "Fantasmic Dinner Package"},
                },
                TimesToSearch = new[]
                {
                    "18:00",
                    "17:30",
                    "17:00",
                }
            };


            SearchForReservation(searchCriteria.FirstDateToSearch, searchCriteria.LastDateToSearch,
                searchCriteria.TimesToSearch, searchCriteria.RestaurantsToSearch, new DateTime(2016, 10, 14, 19, 0, 0),
                new DateTime(2016, 10, 14, 16, 45, 0), true);
        }

        private void SearchForFantasmicPackageDay2()
        {
            SearchCriteria searchCriteria = new SearchCriteria
            {
                FirstDateToSearch = new DateTime(2016, _month, 15),
                LastDateToSearch = new DateTime(2016, _month, 15),
                RestaurantsToSearch = new Dictionary<string, string>
                {
                    {"17736028", "Fantasmic Dinner Package"},
                },
                TimesToSearch = new[]
                {
                    "18:00",
                    "17:30",
                    "17:00",
                }
            };


            SearchForReservation(searchCriteria.FirstDateToSearch, searchCriteria.LastDateToSearch,
                searchCriteria.TimesToSearch, searchCriteria.RestaurantsToSearch, new DateTime(2016, 10, 15, 19, 0, 0),
                new DateTime(2016, 10, 15, 15, 25, 0), true);
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
                searchCriteria.TimesToSearch, searchCriteria.RestaurantsToSearch, new DateTime());
        }

        private void SearchForReservation(DateTime firstDateToSearch, DateTime lastDateToSearch, string[] timesToSearch, Dictionary<string, string> restaurants, DateTime idealTime, DateTime? researvationTime = null, bool isPackage = false)
        {
            _stopwatch.Start();

            Console.WriteLine($"=== {restaurants.First().Value} ===");
            Console.WriteLine($"Ideal Reservation Time: {idealTime.ToString("MM/dd/yyyy hh:mm tt")}");

            if (researvationTime != null)
            {

                Console.WriteLine($"Current Reservation Time: {((DateTime)researvationTime).ToString("MM/dd/yyyy hh:mm tt")}");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("No current reservation.");
                Console.WriteLine();
            }

            if (!_isLoggedIn)
            {
                LogInToSite();

                GoToReservationPage(10000);

                _isLoggedIn = true;
            }

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

                    if (isPackage)
                    {
                        PrintPackageSearchResults(restaurants, dateToSearch, timeToSearch, idealTime, researvationTime);
                    }
                    else
                    {
                        PrintSearchResults(restaurants, dateToSearch, timeToSearch, idealTime, researvationTime);
                    }
                }

                dateToSearch = dateToSearch.AddDays(1);
            }

            
        }

        private void PrintPackageSearchResults(Dictionary<string, string> restaurants, DateTime dateToSearch, string timeToSearch, DateTime idealTime, DateTime? researvationTime = null)
        {
            foreach (var restaurant in restaurants)
            {
                Console.WriteLine(
                    $"{restaurant.Value} - Search for {dateToSearch.ToString("MM/dd/yyyy")} - {timeToSearch} for 5 People - Total Time: {_stopwatch.Elapsed} - Interval: {_stopwatch.Elapsed - _lastElapse}");

                _lastElapse = _stopwatch.Elapsed;

                string timeSlot1 = "";
                string timeSlot2 = "";
                string timeSlot3 = "";

                DateTime? availableTime1;
                DateTime? availableTime2;
                DateTime? availableTime3;

                bool timeSlot1Ok = false;
                bool timeSlot2Ok = false;
                bool timeSlot3Ok = false;


                Dictionary<string, string> packageRestaurants = new Dictionary<string, string>
                {
                    //{"90002245", "The Hollywood Brown Derby" },
                    {"90001744", "Hollywood & Vine" },
                    //{"90001865", "Mama Melrose's Ristorante Italiano" },

                };

                foreach (var packageRestaurant in packageRestaurants)
                {

                    timeSlot1Ok = false;
                    timeSlot2Ok = false;
                    timeSlot3Ok = false;

                    try
                    {
                        timeSlot1 =
                            driver.FindElement(
                                By.XPath(
                                    $"//*[@id=\"withAvailability-alpha-default\"]/li[@data-entityid=\"{restaurant.Key};entityType=Dining-Event\"]/div[1]/div[1]/div/div[2]/span/span/div[@data-facilityid=\"{packageRestaurant.Key}\"]/div/div[2]/div/span[1]/span/span")).Text;

                        availableTime1 = ConstructAvailableTime(dateToSearch, timeSlot1);

                        if (researvationTime == null)
                            timeSlot1Ok = true;
                        else
                        {
                            if (researvationTime < idealTime && availableTime1 > researvationTime)
                            {
                                timeSlot1Ok = true;
                            }

                            if (researvationTime > idealTime && availableTime1 < researvationTime)
                            {
                                timeSlot1Ok = true;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        timeSlot1 = "";
                    }
                    try
                    {
                        timeSlot2 =
                            driver.FindElement(
                                By.XPath(
                                    $"//*[@id=\"withAvailability-alpha-default\"]/li[@data-entityid=\"{restaurant.Key};entityType=Dining-Event\"]/div[1]/div[1]/div/div[2]/span/span/div[@data-facilityid=\"{packageRestaurant.Key}\"]/div/div[2]/div/span[2]/span/span"))
                                .Text;

                        timeSlot2 =
                        driver.FindElement(
                            By.XPath(
                                $"//*[@id=\"withAvailability-alpha-default\"]/li[@data-entityid=\"{restaurant.Key};entityType=restaurant\"]/div[1]/div[1]/div/div[2]/div[2]/span[2]/span/span"))
                            .Text;

                        availableTime2 = ConstructAvailableTime(dateToSearch, timeSlot2);

                        if (researvationTime == null)
                            timeSlot2Ok = true;
                        else
                        {
                            if (researvationTime < idealTime && availableTime2 > researvationTime)
                            {
                                timeSlot2Ok = true;
                            }

                            if (researvationTime > idealTime && availableTime2 < researvationTime)
                            {
                                timeSlot2Ok = true;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        timeSlot2 = "";
                    }
                    try
                    {
                        timeSlot3 =
                            driver.FindElement(
                                By.XPath(
                                    $"//*[@id=\"withAvailability-alpha-default\"]/li[@data-entityid=\"{restaurant.Key};entityType=Dining-Event\"]/div[1]/div[1]/div/div[2]/span/span/div[@data-facilityid=\"{packageRestaurant.Key}\"]/div/div[2]/div/span[3]/span/span"))
                                .Text;

                        

                        timeSlot3 =
                        driver.FindElement(
                            By.XPath(
                                $"//*[@id=\"withAvailability-alpha-default\"]/li[@data-entityid=\"{restaurant.Key};entityType=restaurant\"]/div[1]/div[1]/div/div[2]/div[2]/span[3]/span/span"))
                            .Text;

                        availableTime3 = ConstructAvailableTime(dateToSearch, timeSlot3);

                        if (researvationTime == null)
                            timeSlot3Ok = true;
                        else
                        {
                            if (researvationTime < idealTime && availableTime3 > researvationTime)
                            {
                                timeSlot3Ok = true;
                            }

                            if (researvationTime > idealTime && availableTime3 < researvationTime)
                            {
                                timeSlot3Ok = true;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        timeSlot3 = "";
                    }

                    Console.WriteLine($"{packageRestaurant.Value}:");
                    if (!string.IsNullOrEmpty(timeSlot1))
                    {
                        Console.Write($"{dateToSearch.ToString("MM/dd/yyyy")} - {timeSlot1}");
                        if (timeSlot1Ok)
                        {
                            Console.WriteLine(" - **** BOOK NOW **** ");
                            _numberOfReservationFound++;
                            EmailSender.SendEmail(packageRestaurant.Value, dateToSearch.ToString("MM/dd/yyyy"), timeSlot1, $"{restaurant.Value} - Search for {dateToSearch.ToString("MM/dd/yyyy")} - {timeToSearch} for 5 People", idealTime, researvationTime);
                        }
                        else
                            Console.WriteLine(" - no");
                    }
                    else
                        Console.WriteLine("No reservations available");

                    if (!string.IsNullOrEmpty(timeSlot2))
                    {
                        Console.Write($"{dateToSearch.ToString("MM/dd/yyyy")} - {timeSlot2}");
                        if (timeSlot2Ok)
                        {
                            Console.WriteLine(" - **** BOOK NOW **** ");
                            _numberOfReservationFound++;
                            EmailSender.SendEmail(packageRestaurant.Value, dateToSearch.ToString("MM/dd/yyyy"), timeSlot2, $"{restaurant.Value} - Search for {dateToSearch.ToString("MM/dd/yyyy")} - {timeToSearch} for 5 People", idealTime, researvationTime);

                        }
                        else
                            Console.WriteLine(" - no");
                    }

                    if (!string.IsNullOrEmpty(timeSlot3))
                    {
                        Console.Write($"{dateToSearch.ToString("MM/dd/yyyy")} - {timeSlot3}");
                        if (timeSlot3Ok)
                        {
                            Console.WriteLine(" - **** BOOK NOW **** ");
                            _numberOfReservationFound++;
                            EmailSender.SendEmail(packageRestaurant.Value, dateToSearch.ToString("MM/dd/yyyy"), timeSlot3, $"{restaurant.Value} - Search for {dateToSearch.ToString("MM/dd/yyyy")} - {timeToSearch} for 5 People", idealTime, researvationTime);

                        }
                        else
                            Console.WriteLine(" - no");
                    }

                    Console.WriteLine();
                }
            }
        }

        private void PrintSearchResults(Dictionary<string, string> restaurants, DateTime dateToSearch, string timeToSearch, DateTime idealTime, DateTime? researvationTime = null)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (var restaurant in restaurants)
            {
                Console.WriteLine(
                    $"{restaurant.Value} - Search for {dateToSearch.ToString("MM/dd/yyyy")} - {timeToSearch} for 5 People - Total Time: {_stopwatch.Elapsed} - Interval: {_stopwatch.Elapsed - _lastElapse}");

                _lastElapse = _stopwatch.Elapsed;

                string timeSlot1 = "";
                string timeSlot2 = "";
                string timeSlot3 = "";

                DateTime? availableTime1;
                DateTime? availableTime2;
                DateTime? availableTime3;

                bool timeSlot1Ok = false;
                bool timeSlot2Ok = false;
                bool timeSlot3Ok = false;

                try
                {
                    timeSlot1 =
                        driver.FindElement(
                            By.XPath(
                                $"//*[@id=\"withAvailability-alpha-default\"]/li[@data-entityid=\"{restaurant.Key};entityType=restaurant\"]/div[1]/div[1]/div/div[2]/div[2]/span[1]/span/span"))
                            .Text;

                    availableTime1 = ConstructAvailableTime(dateToSearch, timeSlot1);

                    if (researvationTime == null)
                        timeSlot1Ok = true;
                    else
                    {
                        if (researvationTime < idealTime && availableTime1 > researvationTime)
                        {
                            timeSlot1Ok = true;
                        }

                        if (researvationTime > idealTime && availableTime1 < researvationTime)
                        {
                            timeSlot1Ok = true;
                        }
                    }
                    
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

                    availableTime2 = ConstructAvailableTime(dateToSearch, timeSlot2);

                    if (researvationTime == null)
                        timeSlot2Ok = true;
                    else
                    {
                        if (researvationTime < idealTime && availableTime2 > researvationTime)
                        {
                            timeSlot2Ok = true;
                        }

                        if (researvationTime > idealTime && availableTime2 < researvationTime)
                        {
                            timeSlot2Ok = true;
                        }
                    }
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

                    availableTime3 = ConstructAvailableTime(dateToSearch, timeSlot3);

                    if (researvationTime == null)
                        timeSlot3Ok = true;
                    else
                    {
                        if (researvationTime < idealTime && availableTime3 > researvationTime)
                        {
                            timeSlot3Ok = true;
                        }

                        if (researvationTime > idealTime && availableTime3 < researvationTime)
                        {
                            timeSlot3Ok = true;
                        }
                    }
                }
                catch (Exception)
                {
                }


                if (!string.IsNullOrEmpty(timeSlot1))
                {
                    Console.Write($"{dateToSearch.ToString("MM/dd/yyyy")} - {timeSlot1}");
                    if (timeSlot1Ok)
                    {
                        Console.WriteLine(" - **** BOOK NOW **** ");
                        _numberOfReservationFound++;
                        EmailSender.SendEmail(restaurant.Value, dateToSearch.ToString("MM/dd/yyyy"), timeSlot1, $"Search for {dateToSearch.ToString("MM/dd/yyyy")} - {timeToSearch} for 5 People", idealTime, researvationTime);
                    }
                    else
                        Console.WriteLine(" - no");
                }
                else
                    Console.WriteLine("No reservations available");

                if (!string.IsNullOrEmpty(timeSlot2))
                {
                    Console.Write($"{dateToSearch.ToString("MM/dd/yyyy")} - {timeSlot2}");
                    if (timeSlot2Ok)
                    {
                        Console.WriteLine(" - **** BOOK NOW **** ");
                        _numberOfReservationFound++;
                        EmailSender.SendEmail(restaurant.Value, dateToSearch.ToString("MM/dd/yyyy"), timeSlot2, $"Search for {dateToSearch.ToString("MM/dd/yyyy")} - {timeToSearch} for 5 People", idealTime, researvationTime);

                    }
                    else
                        Console.WriteLine(" - no");
                }

                if (!string.IsNullOrEmpty(timeSlot3))
                {
                    Console.Write($"{dateToSearch.ToString("MM/dd/yyyy")} - {timeSlot3}");
                    if (timeSlot3Ok)
                    {
                        Console.WriteLine(" - **** BOOK NOW **** ");
                        _numberOfReservationFound++;
                        EmailSender.SendEmail(restaurant.Value, dateToSearch.ToString("MM/dd/yyyy"), timeSlot3, $"Search for {dateToSearch.ToString("MM/dd/yyyy")} - {timeToSearch} for 5 People", idealTime, researvationTime);

                    }
                    else
                        Console.WriteLine(" - no");
                }

                Console.WriteLine();
            }
        }

        private DateTime? ConstructAvailableTime(DateTime dateToSearch, string timeSlot)
        {
            string[] timeParts = timeSlot.Split(' ');
            string amPm = timeParts[1];
            string[] hourMinutesPart = timeParts[0].Split(':');

            int hour = amPm == "PM" ? Convert.ToInt32(hourMinutesPart[0]) + 12 : Convert.ToInt32(hourMinutesPart[0]);
            int minute = Convert.ToInt32(hourMinutesPart[1]);

            return new DateTime(dateToSearch.Year, dateToSearch.Month, dateToSearch.Day, hour, minute, 0);
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
            driver.FindElement(By.Id("loginPagePassword")).SendKeys("abcd12345");
            driver.FindElement(By.Id("loginPageSubmitButton")).Click();
        }

        private void DoSearch(DateTime dateToSearch, string timeToSearch, bool openNewTab = true)
        {
            if (_retainWindow)
            {
                if (openNewTab)
                    OpenNewTab();

                GoToReservationPage(4000);
            }

            EnterPartySize();

            EnterSearchTime(timeToSearch);

            EnterSearchDate(dateToSearch);

            driver.FindElement(By.CssSelector("span.buttonText")).Click();

            if (!string.IsNullOrEmpty(driver.FindElement(By.ClassName("warning")).Text))
            {
                Console.WriteLine("Error...");
                throw new Exception();
            }
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