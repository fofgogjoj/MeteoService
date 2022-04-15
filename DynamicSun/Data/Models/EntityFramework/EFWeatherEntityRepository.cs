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
            DateTime dateBegin = Convert.ToDateTime(year + "-01-01 00:00:00.0000000");
            DateTime dateEnd = Convert.ToDateTime(year + "-12-31 23:59:59.9999999");
            return context.WeatherEntities.Where(x => x.Date >= dateBegin && x.Date <= dateEnd);
        }
        public IQueryable<WeatherEntity> GetWeatherByMonth(int year, int month)
        {
            string dateEndpart = string.Empty;
            if (month == 12)
                dateEndpart = (year + 1) + "-" + 1;
            else
                dateEndpart = year + "-" + (month + 1);
            DateTime dateBegin = Convert.ToDateTime(year + "-" + month + "-01 00:00:00.0000000");
            DateTime dateEnd = Convert.ToDateTime(dateEndpart + "-01 00:00:00.0000000");
            return context.WeatherEntities.Where(x => x.Date >= dateBegin && x.Date < dateEnd);
        }
        public void SaveWeatherList(List<WeatherEntity> entity)
        {
            entity.ForEach(n => context.Add(n));
            context.SaveChanges();
        }
    }
}
