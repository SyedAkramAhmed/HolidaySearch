using HolidaySearch.Entity;

namespace HolidaySearch.Interface
{
    public interface IHolidayService
    {
        SearchResponseEntity HolidaySearch(SearchRequestEntity search);
    }
}
