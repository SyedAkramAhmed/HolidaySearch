using Newtonsoft.Json;
using System;

namespace HolidaySearch.Entity
{
    public class FlightEntity
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("airline")]
        public string Airline { get; set; }
        [JsonProperty("from")]
        public string From { get; set; }
        [JsonProperty("to")]
        public string To { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("departure_date")]
        public DateTime DepartureDateTime { get; set; }
    }
}
