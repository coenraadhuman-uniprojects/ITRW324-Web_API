using System;
using WeatherStationApi._03_Dtos;
using WeatherStationApi._04_Interfaces.Repositories;
using WeatherStationApi._04_Interfaces.Services;
using WeatherStationApi._05_Repositories;
using System.Linq;

namespace WeatherStationApi._06_Services
{
    public class LastDayReadingsService: IFetchReadingsService
    {

        private static readonly DataContextFactory _factory = new DataContextFactory();
        private readonly IReadingsRepository _readingsRepository = new ReadingsRepository(_factory);

        public ReadingsDto FetchReadings()
        {
            var readings =  _readingsRepository
                .FetchAll()
                .Where(x => x.ReadingDateTime >= DateTime.Now.AddDays(-1))
                .Select(x => new ReadingDto(x));

            
            return new ReadingsDto
            {
                Readings = readings.ToList()
            };
        }
        
    }
}