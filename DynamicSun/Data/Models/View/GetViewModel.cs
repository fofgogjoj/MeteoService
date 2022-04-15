using DynamicSun.Data.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DynamicSun.Data.Models.View
{
    public class GetViewModel
    {
        public IQueryable<WeatherEntity> Weathers { get; set; }
        public PageInfo PageInfo { get; set; }
        public int SelectedYear { get; set; }
        public int SelectedMonth { get; set; }
        public List<SelectListItem> Years {
            get
            {
                List<SelectListItem> list = new List<SelectListItem>();
                for (int i = 10; i <= 22; i++)
                    list.Add(new SelectListItem() { Text = $"20{i}", Value = $"20{i}" });
                return list;
            }
        }
        public List<SelectListItem> Months
        {
            get
            {
                List<SelectListItem> list = new List<SelectListItem>();
                list.Add(new SelectListItem() { Text = "январь", Value = "1" });
                list.Add(new SelectListItem() { Text = "февраль", Value = "2" });
                list.Add(new SelectListItem() { Text = "март", Value = "3" });
                list.Add(new SelectListItem() { Text = "апрель", Value = "4" });
                list.Add(new SelectListItem() { Text = "май", Value = "5" });
                list.Add(new SelectListItem() { Text = "июнь", Value = "6" });
                list.Add(new SelectListItem() { Text = "июль", Value = "7" });
                list.Add(new SelectListItem() { Text = "август", Value = "8" });
                list.Add(new SelectListItem() { Text = "сентябрь", Value = "9" });
                list.Add(new SelectListItem() { Text = "октябрь", Value = "10" });
                list.Add(new SelectListItem() { Text = "ноябрь", Value = "11" });
                list.Add(new SelectListItem() { Text = "декабрь", Value = "12" });

                return list;
            }
        }
    }
}
