using System;
using System.Collections.Generic;
using System.Linq;
using WeatherStationApi._03_Dtos;
using WeatherStationApi._04_Interfaces.Repositories;
using WeatherStationApi._04_Interfaces.Services;
using WeatherStationApi._05_Repositories;

namespace WeatherStationApi._06_Services
{
    public class StationStatusService : IStationStatusService
    {
        private static readonly DataContextFactory _factory = new DataContextFactory();
        private readonly IReadingsRepository _readingsRepository = new ReadingsRepository(_factory);

        public StationsStatusesDto FetchStationStatus(string StationIds)
        {
            //init
            StationsStatusesDto returnDto = new StationsStatusesDto();
            string[] listIDs = StationIds.Split('-');


            foreach (var sID in listIDs)
            {
                int curID = Convert.ToInt32(sID);
                
                StationStatusDto newDto = new StationStatusDto();
                newDto.StationName = curID.ToString();

                var avg_Readings = _readingsRepository
                    .FetchAll()
                    .Where(x => x.ReadingDateTime >= DateTime.Now.AddHours(-1) && x.StationId == curID)
                    .GroupBy(g => g.StationId)
                    .Select(g => new AverageReadingDto(
                        g.Key,
                        g.Average(x => x.Temperature).ToString(),
                        g.Average(x => x.Humidity).ToString(),
                        g.Average(x => x.AirPressure).ToString(),
                        g.Average(x => x.AmbientLight).ToString())
                    );


                var AverageReadings = avg_Readings.ToList();

                try
                {
                    newDto.AverageTemp = AverageReadings.First().AverageTemperature;
                }
                catch (Exception e)
                {
                    newDto.AverageTemp = "0";
                    Console.WriteLine(e.Source + " : " + e.Message);
                }

                try
                {
                    newDto.Humidity = AverageReadings.First().AverageHumidity;
                }
                catch (Exception e)
                {
                    newDto.Humidity = "0";
                    Console.WriteLine(e.Source + " : " + e.Message);
                }

                try
                {
                    newDto.AmbientLight = AverageReadings.First().AverageAmbientLight;
                }
                catch (Exception e)
                {
                    newDto.AmbientLight = "0";
                    Console.WriteLine(e.Source + " : " + e.Message);
                }


                var max_readings = _readingsRepository
                    .FetchAll()
                    .Where(x => x.ReadingDateTime >= DateTime.Now.AddDays(-1) && x.StationId == curID)
                    .GroupBy(g => g.StationId)
                    .Select(g => new MaxReadingDto(
                        g.Key,
                        g.Max(x => x.Temperature).ToString(),
                        g.Max(x => x.Humidity).ToString(),
                        g.Max(x => x.AirPressure).ToString(),
                        g.Max(x => x.AmbientLight).ToString())
                    );



                var MaxReadings = max_readings.ToList();

                try
                {
                    newDto.MaxTemp = MaxReadings.First().MaxTemperature;
                }
                catch (Exception e)
                {
                    newDto.MaxTemp = "0";
                    Console.WriteLine(e.Source + " : " + e.Message);
                }

                var min_readings = _readingsRepository
                    .FetchAll()
                    .Where(x => x.ReadingDateTime >= DateTime.Now.AddDays(-1) && x.StationId == curID)
                    .GroupBy(g => g.StationId)
                    .Select(g => new MinReadingDto(
                        g.Key,
                        g.Min(x => x.Temperature).ToString(),
                        g.Min(x => x.Humidity).ToString(),
                        g.Min(x => x.AirPressure).ToString(),
                        g.Min(x => x.AmbientLight).ToString())
                    );


                var MinReadings = min_readings.ToList();

                try
                {
                    newDto.MinTemp = MinReadings.First().MinTemperature;
                }
                catch (Exception e)
                {
                    newDto.MinTemp = "0";
                    Console.WriteLine(e.Source + " : " + e.Message);
                }

                
                double[] forecast = new ForecastService().FetchForecast(stationID: curID);

                //test values for forecasting (stretch goal)
                newDto.ForecastDay1 = forecast[0].ToString();
                newDto.ForecastDay2 = forecast[1].ToString();
                newDto.ForecastDay3 = forecast[2].ToString();
                newDto.ForecastDay4 = forecast[3].ToString();
                
                returnDto.Readings.Add(newDto);
            }
            

            return returnDto;
        }
    }
}