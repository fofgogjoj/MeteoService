using DynamicSun.Data.Models.Interfaces;

namespace DynamicSun.Data.Domain
{
    public class DataManager
    {
        public IWeatherEntityRepository Weathers { get; set; }

        public DataManager(IWeatherEntityRepository weathersRepository)
        {
            Weathers = weathersRepository;
        }
    }
}
