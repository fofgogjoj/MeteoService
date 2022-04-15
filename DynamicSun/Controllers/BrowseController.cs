using DynamicSun.Data.Domain;
using DynamicSun.Data.Models;
using DynamicSun.Data.Models.Domain;
using DynamicSun.Data.Models.View;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DynamicSun.Controllers
{
    public class BrowseController : Controller
    {
        private readonly DataManager dataManager;
        public BrowseController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Get(int page = 1, int selectedYear = 0, int selectedMonth = 0)
        {
            int pageSize = 5;
            IQueryable<WeatherEntity> itemsPerPages;
            PageInfo pageInfo;

            if (selectedMonth != 0 && selectedYear != 0)
            {
                itemsPerPages = dataManager.Weathers.GetWeatherByMonth(selectedYear, selectedMonth).OrderBy(x => x.Date).Skip((page - 1) * pageSize).Take(pageSize);
                pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = dataManager.Weathers.GetWeatherByMonth(selectedYear, selectedMonth).Count() };
            }
            else if (selectedYear != 0)
            {
                itemsPerPages = dataManager.Weathers.GetWeatherByYear(selectedYear).OrderBy(x => x.Date).Skip((page - 1) * pageSize).Take(pageSize);
                pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = dataManager.Weathers.GetWeatherByYear(selectedYear).Count() };
            }
            else
            {
                itemsPerPages = dataManager.Weathers.GetWeathers().OrderBy(x => x.Date).Skip((page - 1) * pageSize).Take(pageSize); //надо ToList() ???????
                pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = dataManager.Weathers.GetWeathers().Count() };
            }
            GetViewModel ivm = new GetViewModel { PageInfo = pageInfo, Weathers = itemsPerPages };
            this.ViewBag.Pager = pageInfo;
            this.ViewBag.selectedYears = selectedYear;
            this.ViewBag.selectedMonths = selectedMonth;
            return View(ivm);
        }
    }
}
