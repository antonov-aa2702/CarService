using CarService.WebApi.Settings;

namespace CarService.WebApi.Settings
{
    public class CarServiceSettingsReader
    {
        public static CarServiceSettings Read(IConfiguration configuration)
        {
            //здесь будет чтение настроек приложения из конфига
            return new CarServiceSettings();
        }
    }
}