using System;
using System.Linq;
using WeatherStationApi._03_Dtos;
using WeatherStationApi._04_Interfaces.Repositories;
using WeatherStationApi._04_Interfaces.Services;
using WeatherStationApi._05_Repositories;

namespace WeatherStationApi._06_Services
{
    public class RawReadingsService : IRawReadingsService
    {
        private static readonly DataContextFactory _factory = new DataContextFactory();
        private readonly IReadingsRepository _readingsRepository = new ReadingsRepository(_factory);
        
        public RawReadingsDto FetchRawDayAStationReadings(int StationId)
        {
            var readings =  _readingsRepository
                .FetchAll()
                .Where(x => x.ReadingDateTime.Date == DateTime.Now.Date && x.StationId == StationId)
                .Select(y => new RawReadingDto(
                    y.ReadingDateTime,
                    y.AirPressure,
                    y.AmbientLight,
                    y.Humidity,
                    y.Temperature)
                );
            
            return new RawReadingsDto()
            {
                Readings = readings.ToList()
            };
        }
        
        public RawReadingsDto FetchRawWeekAStationReadings(int StationId)
        {
            var readings =  _readingsRepository
                .FetchAll()
                .Where(x => x.ReadingDateTime.Date >= DateTime.Now.Date.AddDays(-7) && x.StationId == StationId)
                .Select(y => new RawReadingDto(
                    y.ReadingDateTime,
                    y.AirPressure,
                    y.AmbientLight,
                    y.Humidity,
                    y.Temperature)
                );

            return new RawReadingsDto()
            {
                Readings = readings.ToList()
            };
        }
        
        public RawReadingsDto FetchRawMonthAStationReadings(int StationId)
        {
            var readings =  _readingsRepository
                .FetchAll()
                .Where(x => x.ReadingDateTime.Date >= DateTime.Now.Date.AddDays(-31) && x.StationId == StationId)
                .Select(y => new RawReadingDto(
                    y.ReadingDateTime,
                    y.AirPressure,
                    y.AmbientLight,
                    y.Humidity,
                    y.Temperature)
                );

            return new RawReadingsDto()
            {
                Readings = readings.ToList()
            };
        }
        
        public RawReadingsDto FetchRawYearAStationReadings(int StationId)
        {
            var readings =  _readingsRepository
                .FetchAll()
                .Where(x => x.ReadingDateTime.Date >= DateTime.Now.Date.AddDays(-356) && x.StationId == StationId)
                .Select(y => new RawReadingDto(
                    y.ReadingDateTime,
                    y.AirPressure,
                    y.AmbientLight,
                    y.Humidity,
                    y.Temperature)
                );

            return new RawReadingsDto()
            {
                Readings = readings.ToList()
            };
        }
    }
}