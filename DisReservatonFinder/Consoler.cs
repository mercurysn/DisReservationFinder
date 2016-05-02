using System;
using System.IO;

namespace DisReservatonFinder
{
    public class Consoler
    {
        public static void WriteLine(string stringToWrite = "")
        {
            if (!string.IsNullOrEmpty(stringToWrite))
                File.AppendAllLines("reservation.txt", new[] {stringToWrite});
            Console.WriteLine(stringToWrite);
        }

        public static void Write(string stringToWrite = "")
        {
            if (!string.IsNullOrEmpty(stringToWrite))
                File.AppendAllLines("reservation.txt", new[] { stringToWrite });
            Console.Write(stringToWrite);
        }
    }
}
