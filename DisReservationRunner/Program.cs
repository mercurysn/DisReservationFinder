using System;
using System.IO;
using DisReservatonFinder;

namespace DisReservationRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Selenium selenium = new Selenium();

                try
                {
                    selenium.SetupTest();
                    selenium.SearchForEverything();
                    selenium.TeardownTest();

                    File.WriteAllText(args[0], $"Ran to end: {DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")}");
                }
                catch (Exception ex)
                {
                    selenium.TeardownTest();
                    File.WriteAllText(args[0], $"Ran to end - Error: {DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")}\r\n{ex.Message}");
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText(args[0], $"Ran to end - Error: {DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")}\r\n{ex.Message}");
            }
        }
    }
}
