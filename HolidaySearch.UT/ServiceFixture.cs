using HolidaySearch.Implementation;
using HolidaySearch.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace HolidaySearch.UT
{
    public class ServiceFixture
    {
        /// <summary>
        /// Service Fixuture IHolidayService,HolidayService
        /// </summary>
        public ServiceFixture()
        {
            ServiceProvider = new ServiceCollection().AddSingleton<IHolidayService, HolidayService>().BuildServiceProvider();
            ServiceProvider.GetService<IHolidayService>();
        }
        public ServiceProvider ServiceProvider { get; private set; }
    }
}
