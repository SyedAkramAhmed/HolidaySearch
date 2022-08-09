using HolidaySearch.Implementation;
using HolidaySearch.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace HolidaySearch
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection().AddSingleton<IHolidayService, HolidayService>().BuildServiceProvider();
            var holidayService = serviceProvider.GetService<IHolidayService>();
            var response = holidayService.HolidaySearch(new Entity.SearchRequestEntity()
            {
                DepartingFrom = "MAN",
                TravelingTo = "LPA",
                DepartureDate = new DateTime(2022, 11, 10),
                Duration = 14
            });
            Console.WriteLine("Flight Id        : {0}", response.Flights.First().Id);
            Console.WriteLine("Departing From   : {0}", response.Flights.First().From);
            Console.WriteLine("Traveling To     : {0}", response.Flights.First().To);
            Console.WriteLine("Flight Price     : {0}", $"£{response.Flights.First().Price}");
            Console.WriteLine("Hotel Id         : {0}", response.Hotels.First().Id);
            Console.WriteLine("Hotel Name       : {0}", response.Hotels.First().Name);
            Console.WriteLine("Hotel Price      : {0}", $"£{response.Hotels.First().PricePerNight}");
            Console.WriteLine("******************************");
            Console.WriteLine("Total Price      : {0}", $"£{response.Flights.First().Price + response.Hotels.First().PricePerNight * response.Hotels.First().Nights}");
        }
    }
}
