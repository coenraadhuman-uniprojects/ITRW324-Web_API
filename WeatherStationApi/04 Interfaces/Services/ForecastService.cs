using System;
using WeatherStationApi._03_Dtos;

namespace WeatherStationApi._04_Interfaces.Services
{
    public interface IForecastService
    {
        ForecastDto FetchForecast(int stationId, DateTime Date);
    }
}
