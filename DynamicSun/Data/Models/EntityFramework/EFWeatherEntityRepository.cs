using DynamicSun.Data.Domain;
using DynamicSun.Data.Models.Domain;
using DynamicSun.Data.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DynamicSun.Data.Models.EntityFramework
{
    public class EFWeatherEntityRepository : IWeatherEntityRepository
    {
        private readonly AppDbContext context;
        public EFWeatherEntityRepository(AppDbContext context)
        {
            this.context = context;
        }
        public IQueryable<WeatherEntity> GetWeathers()
        {
            return context.WeatherEntities;
        }
        public IQueryable<WeatherEntity> GetWeatherByDate(DateTime date)
        {
            return context.WeatherEntities.Where(x => x.Date == date);
        }
        public IQueryable<WeatherEntity> GetWeatherByYear(int year)
        {
            DateTime dateBegin = new DateTime(year, 1, 1);
            DateTime dateEnd = new DateTime(year + 1, 1, 1);
            return context.WeatherEntities.Where(x => x.Date >= dateBegin && x.Date < dateEnd);
        }
        public IQueryable<WeatherEntity> GetWeatherByMonth(int year, int month)
        {
            DateTime dateEnd = new DateTime();
            if (month == 12)
                dateEnd = new DateTime(year + 1, 1, 1);
            else
                dateEnd = new DateTime(year, month + 1, 1);
            DateTime dateBegin = new DateTime(year, month, 1);
            return context.WeatherEntities.Where(x => x.Date >= dateBegin && x.Date < dateEnd);
        }
        public void SaveWeatherList(List<WeatherEntity> entity)
        {
            entity.ForEach(n => context.Add(n));
            context.SaveChanges();
        }
    }
}
