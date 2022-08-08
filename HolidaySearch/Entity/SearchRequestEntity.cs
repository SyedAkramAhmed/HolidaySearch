using System;

namespace HolidaySearch.Entity
{
    public class SearchRequestEntity
    {
        public string DepartingFrom { get; set; }
        public string TravelingTo { get; set; }
        public DateTime DepartureDate { get; set; }
        public int Duration { get; set; }
    }
}
