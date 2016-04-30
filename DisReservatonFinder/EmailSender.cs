using System;
using System.Net;
using System.Net.Mail;

namespace DisReservatonFinder
{
    public class EmailSender
    {
        public static void SendEmail(string restaurantName, string searchDate, string searchTime, string heading, DateTime idealTime, DateTime? reservationTime)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("sngpush@gmail.com", "sngpush01"),
                EnableSsl = true
            };
            string ressieTIme = reservationTime == null
                ? ""
                : ((DateTime) reservationTime).ToString("MM/dd/yyyy hh:mm tt");

            client.Send("sngpush@gmail.com", "sngpush@gmail.com", $"{restaurantName} found!", $"{searchDate} - {searchTime}\r\n{heading}\r\nRestaurant: {restaurantName}\r\nIdeal Time: {idealTime.ToString("MM/dd/yyyy hh:mm tt")}\r\nCurrent Reservation Time: {ressieTIme}");
        }
    }
}
