using System;
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

                selenium.SetupTest();
                selenium.SearchForEverything();
                selenium.TeardownTest();
            }
            catch (Exception)
            {
            }
        }
    }
}
