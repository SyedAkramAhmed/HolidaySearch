using HolidaySearch.Entity;
using HolidaySearch.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Xunit;

namespace HolidaySearch.UT
{
    public class HolidayServiceTest : IClassFixture<ServiceFixture>
    {
        private readonly ServiceProvider _serviceProvider;
        public HolidayServiceTest(ServiceFixture fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }
        [Fact(DisplayName = "It should return 3 flights and 1 hotel, when given search parameters")]
        public void SearchShouldReturn_When_AllSearchParametersProvided()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var holidayService = scope.ServiceProvider.GetRequiredService<IHolidayService>();
                var response = holidayService.HolidaySearch(new SearchRequestEntity()
                {
                    DepartingFrom = "MAN",
                    TravelingTo = "AGP",
                    DepartureDate = new DateTime(2023, 7, 1),
                    Duration = 7
                });
                Assert.Single(response.Flights);
                Assert.Single(response.Hotels);
            }
        }
        [Fact(DisplayName = "It should return 4 flights and 2 hotel, when given search parameters")]
        public void SearchShouldReturn_When_OnlyTravelingArilineGiven()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var holidayService = scope.ServiceProvider.GetRequiredService<IHolidayService>();
                var response = holidayService.HolidaySearch(new SearchRequestEntity()
                {
                    TravelingTo = "PMI",
                    DepartureDate = new DateTime(2023, 6, 15),
                    Duration = 10
                });
                Assert.Equal(4, response.Flights.Count);
                Assert.Equal(2, response.Hotels.Count);
            }
        }
        [Fact(DisplayName = "It should return 0 flights and 0 hotel, when duration not exist")]
        public void SearchShouldReturn_NoSearchDetails_When_Duration_NotExist()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var holidayService = scope.ServiceProvider.GetRequiredService<IHolidayService>();
                var response = holidayService.HolidaySearch(new SearchRequestEntity()
                {
                    DepartingFrom = "MAN",
                    TravelingTo = "AGP",
                    DepartureDate = new DateTime(2023, 7, 1),
                    Duration = 21
                });
                Assert.Single(response.Flights);
                Assert.Empty(response.Hotels);
            }
        }
        [Fact(DisplayName = "It should return 0 flights and 0 hotel, when traveling from not exist")]
        public void SearchShouldReturn_NoSearchDetails_When_TravelingFrom_NotExist()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var holidayService = scope.ServiceProvider.GetRequiredService<IHolidayService>();
                var response = holidayService.HolidaySearch(new SearchRequestEntity()
                {
                    DepartingFrom = "ABC",
                    TravelingTo = "AGP",
                    DepartureDate = new DateTime(2023, 7, 1),
                    Duration = 7
                });
                Assert.Empty(response.Flights);
                Assert.Empty(response.Hotels);
            }
        }
        [Fact(DisplayName = "It should return 0 flights and 0 hotel, when traveling to not exist")]
        public void SearchShouldReturn_NoSearchDetails_When_TravelingTo_NotExist()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var holidayService = scope.ServiceProvider.GetRequiredService<IHolidayService>();
                var response = holidayService.HolidaySearch(new SearchRequestEntity()
                {
                    DepartingFrom = "MAN",
                    TravelingTo = "ABC",
                    DepartureDate = new DateTime(2023, 7, 1),
                    Duration = 7
                });
                Assert.Empty(response.Flights);
                Assert.Empty(response.Hotels);
            }
        }
        [Fact(DisplayName = "It should return 0 flights and 0 hotel, when traveling from and to not exist")]
        public void SearchShouldReturn_NoSearchDetails_When_TravelingFromAndTo_NotExist()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var holidayService = scope.ServiceProvider.GetRequiredService<IHolidayService>();
                var response = holidayService.HolidaySearch(new SearchRequestEntity()
                {
                    DepartingFrom = "ABC",
                    TravelingTo = "ABC",
                    DepartureDate = new DateTime(2023, 7, 1),
                    Duration = 7
                });
                Assert.Empty(response.Flights);
                Assert.Empty(response.Hotels);
            }
        }
        [Fact(DisplayName = "It should return 0 flights and 0 hotel, when departure datetime is past")]
        public void SearchShouldReturn_NoSearchDetails_When_DepartureDateTime_IsPastDate()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var holidayService = scope.ServiceProvider.GetRequiredService<IHolidayService>();
                var response = holidayService.HolidaySearch(new SearchRequestEntity()
                {
                    DepartingFrom = "MAN",
                    TravelingTo = "AGP",
                    DepartureDate = new DateTime(2021, 7, 1),
                    Duration = 7
                });
                Assert.Empty(response.Flights);
                Assert.Empty(response.Hotels);
            }
        }
        [Fact(DisplayName = "It should return 0 flights and 0 hotel, when departure datetime is empty")]
        public void SearchShouldReturn_NoSearchDetails_When_DepartureDateTime_IsEmpty()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var holidayService = scope.ServiceProvider.GetRequiredService<IHolidayService>();
                var response = holidayService.HolidaySearch(new SearchRequestEntity()
                {
                    DepartingFrom = "MAN",
                    TravelingTo = "AGP",
                    DepartureDate = new DateTime(),
                    Duration = 7
                });
                Assert.Empty(response.Flights);
                Assert.Empty(response.Hotels);
            }
        }
        [Fact(DisplayName = "It should return the best flight search results")]
        public void SearchShouldReturn_When_AllSearchParametersProvided_BestSearch()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var holidayService = scope.ServiceProvider.GetRequiredService<IHolidayService>();
                var response = holidayService.HolidaySearch(new SearchRequestEntity()
                {
                    DepartingFrom = "MAN",
                    TravelingTo = "LPA",
                    DepartureDate = new DateTime(2022, 11, 10),
                    Duration = 14
                });
                Assert.Single(response.Flights);
                Assert.Single(response.Hotels);
                Assert.Equal(7, response.Flights.First().Id);
                Assert.Equal("MAN", response.Flights.First().From);
                Assert.Equal("LPA", response.Flights.First().To);
                Assert.Equal(125, response.Flights.First().Price);
                Assert.Equal(6, response.Hotels.First().Id);
                Assert.Equal("Club Maspalomas Suites and Spa", response.Hotels.First().Name);
                Assert.Equal(75, response.Hotels.First().PricePerNight);
                Assert.Equal(1175, response.Flights.First().Price + response.Hotels.First().PricePerNight * response.Hotels.First().Nights);
            }
        }
    }
}