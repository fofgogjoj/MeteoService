using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DynamicSun.Data.Models.Domain
{
    public class WeatherEntity
    {
        public WeatherEntity()
        {
        }
        public WeatherEntity(DateTime date, decimal temperature, decimal air, decimal td, int atmPressure, string direction, int speed, int cloudiness, int h, string vv, string description)
        {
            Date = date;
            Temperature = temperature;
            Air = air;
            Td = td;
            AtmPressure = atmPressure;
            Direction = direction;
            Speed = speed;
            Cloudiness = cloudiness;
            H = h;
            VV = vv;
            Description = description;
        }
        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        [Display(Name = "Температура воздуха")]
        public decimal Temperature { get; set; }

        [Display(Name = "Влажность воздуха")]
        public decimal Air { get; set; }

        [Display(Name = "Точка росы")]
        public decimal Td { get; set; }

        [Display(Name = "Атмосферное давление")]
        public int AtmPressure { get; set; }

        [Display(Name = "Направление ветра")]
        public string Direction { get; set; }

        [Display(Name = "Скорость ветра")]
        public int Speed { get; set; }

        [Display(Name = "Облачность")]
        public int Cloudiness { get; set; }

        [Display(Name = "Нижняя граница облачности")]
        public int H { get; set; }

        [Display(Name = "Горизонтальная видимость")]
        public string VV { get; set; }

        [Display(Name = "Природные явления")]
        public string Description { get; set; }
    }
}
