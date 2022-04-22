using DynamicSun.Data.Domain;
using DynamicSun.Data.Models.Domain;
using DynamicSun.Data.Models.View;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DynamicSun.Controllers
{
    public class ImportController : Controller
    {
        private readonly DataManager dataManager;
        public ImportController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Import(List<IFormFile> fileExcel)
        {
            List<WeatherEntity> weatherList = new List<WeatherEntity>();
            List<WeatherEntity> resultList = new List<WeatherEntity>();
            IQueryable<WeatherEntity> dublicateRows;
            string message = string.Empty;

            foreach (IFormFile postedFile in fileExcel)
            {
                weatherList.AddRange(GetExcelDataTable(postedFile));
            }


            foreach (WeatherEntity weather in weatherList)
            {
                dublicateRows = dataManager.Weathers.GetWeatherByDate(weather.Date);
                if (dublicateRows.ToList().Count == 0)
                    resultList.Add(weather);
            }

            if (resultList.Count > 0)
            {
                dataManager.Weathers.SaveWeatherList(resultList);
                message = "Импорт прошёл успешно!";
            }
            else
            {
                message = "Нет данных для импорта. Попробуйте ещё раз!";
            }

            ImportViewModel ivm = new ImportViewModel { Message = message, RowsCount = resultList.Count };

            return View(ivm);
        }

        public static WeatherEntity Convert(DataRow datarow)
        {
            WeatherEntity item = new WeatherEntity();
            try
            {
                item = new WeatherEntity(
                       DateTime.Parse(datarow[0] + " " + datarow[1]),
                       decimal.Parse(datarow[2].ToString()),
                       decimal.Parse(datarow[3].ToString()),
                       decimal.Parse(datarow[4].ToString()),
                       datarow[5].ToString() == " " ? 0 : int.Parse(datarow[5].ToString()),
                       datarow[6].ToString() == " " ? string.Empty : datarow[6].ToString(),
                       datarow[7].ToString() == " " ? 0 : int.Parse(datarow[7].ToString()),
                       datarow[8].ToString() == " " ? 0 : int.Parse(datarow[8].ToString()),
                       datarow[9].ToString() == " " ? 0 : int.Parse(datarow[9].ToString()),
                       datarow[10].ToString() == " " ? string.Empty : datarow[10].ToString(),
                       datarow[11].ToString() == " " ? string.Empty : datarow[11].ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return item;

        }

        public static IWorkbook GetWorkBook(IFormFile formFile)
        {
            IWorkbook Workbook = null;
            try
            {
                using (var file = formFile.OpenReadStream())
                {
                    Workbook = new XSSFWorkbook(file);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Workbook;
        }

        public static List<WeatherEntity> GetExcelDataTable(IFormFile formFile)
        {
            DataTable table = new DataTable();
            List<WeatherEntity> weather = new List<WeatherEntity>();
            IWorkbook Workbook = GetWorkBook(formFile);

            if (Workbook == null)
                return weather;

            ISheet sheet = Workbook.GetSheetAt(0);

            IRow headerRow = sheet.GetRow(2);
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i, MissingCellPolicy.RETURN_NULL_AND_BLANK).StringCellValue);
                table.Columns.Add(column);
            }
            for (int k = 0; k < Workbook.NumberOfSheets; k++)
            {

                sheet = Workbook.GetSheetAt(k);

                headerRow = sheet.GetRow(2);
                cellCount = headerRow.LastCellNum;
                int rowCount = sheet.LastRowNum;


                for (int i = (sheet.FirstRowNum + 4); i <= rowCount; i++)
                {
                    IRow row = sheet.GetRow(i);
                    DataRow dataRow = table.NewRow();
                    if (row != null)
                    {
                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null)
                            {
                                dataRow[j] = GetCellValue(row.GetCell(j, MissingCellPolicy.RETURN_NULL_AND_BLANK));
                            }
                        }
                    }
                    weather.Add(Convert(dataRow));
                }
            }

            return weather;
        }

        private static string GetCellValue(ICell cell)
        {
            if (cell == null)
            {
                return string.Empty;
            }

            switch (cell.CellType)
            {
                case CellType.Blank:
                    return string.Empty;
                case CellType.Boolean:
                    return cell.BooleanCellValue.ToString();
                case CellType.Error:
                    return cell.ErrorCellValue.ToString();
                case CellType.Numeric:
                case CellType.Unknown:
                default:
                    return cell.ToString();
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Formula:
                    try
                    {
                        HSSFFormulaEvaluator e = new HSSFFormulaEvaluator(cell.Sheet.Workbook);

                        e.EvaluateInCell(cell);
                        return cell.ToString();
                    }
                    catch
                    {
                        return cell.NumericCellValue.ToString();
                    }
            }
        }
    }
}
