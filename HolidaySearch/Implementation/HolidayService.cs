using HolidaySearch.Entity;
using HolidaySearch.Interface;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HolidaySearch.Implementation
{
    public class HolidayService : IHolidayService
    {
        /// <summary>
        /// Flight & Hotel Search
        /// </summary>
        /// <param name="search"></param>
        /// <returns>Flights</returns>
        /// <returns>Hotels</returns>
        public SearchResponseEntity HolidaySearch(SearchRequestEntity search)
        {
            SearchResponseEntity response = new SearchResponseEntity();
            response.Flights = new List<FlightEntity>();
            response.Hotels = new List<HotelEntity>();

            var flightDetails = Flights().Where(x => (search.DepartingFrom == null || x.From == search.DepartingFrom)
            && x.To == search.TravelingTo
            && x.DepartureDateTime == search.DepartureDate).OrderBy(x => x.Price);

            var hotelDetails = Hotels().Where(x => flightDetails.Any(y => x.ArrivalDateTime == y.DepartureDateTime
            && x.LocalAirports.Contains(y.To) && x.Nights == search.Duration)).OrderBy(o => o.PricePerNight);

            response.Flights.AddRange(flightDetails);
            response.Hotels.AddRange(hotelDetails);
            return response;
        }
        /// <summary>
        /// Load Flights JSON
        /// </summary>
        /// <returns></returns>
        internal List<FlightEntity> Flights()
        {
            string flightsJson = File.ReadAllText("App_Data/flights.json");
            return JsonConvert.DeserializeObject<List<FlightEntity>>(flightsJson);
        }
        /// <summary>
        /// Load Hotels JSON
        /// </summary>
        /// <returns></returns>
        internal List<HotelEntity> Hotels()
        {
            string hotelsJson = File.ReadAllText("App_Data/hotels.json");
            return JsonConvert.DeserializeObject<List<HotelEntity>>(hotelsJson);
        }
    }
}
