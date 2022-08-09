using HolidaySearch.Entity;

namespace HolidaySearch.Interface
{
    public interface IHolidayService
    {
        /// <summary>
        /// Flight & Hotel Search
        /// </summary>
        /// <param name="search"></param>
        /// <returns>Flights</returns>
        /// <returns>Hotels</returns>
        SearchResponseEntity HolidaySearch(SearchRequestEntity search);
    }
}
