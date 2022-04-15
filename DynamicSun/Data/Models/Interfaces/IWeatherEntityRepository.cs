using DynamicSun.Data.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DynamicSun.Data.Models.Interfaces
{
    public interface IWeatherEntityRepository
    {
        IQueryable<WeatherEntity> GetWeathers();
        IQueryable<WeatherEntity> GetWeatherByDate(DateTime date);
        IQueryable<WeatherEntity> GetWeatherByYear(int year);
        IQueryable<WeatherEntity> GetWeatherByMonth(int year, int month);
        void SaveWeatherList(List<WeatherEntity> entity);
    }
}
