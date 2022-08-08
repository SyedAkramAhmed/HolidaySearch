using System.Collections.Generic;

namespace HolidaySearch.Entity
{
    public class SearchResponseEntity
    {
        public List<FlightEntity> Flights { get; set; }
        public List<HotelEntity> Hotels { get; set; }
    }
}
