using CarService.WebApi.Settings;

namespace CarService.WebApi.Settings
{
    public class CarServiceSettingsReader
    {
        public static CarServiceSettings Read(IConfiguration configuration)
        { 
            return new CarServiceSettings()
            {
                CarServiceDbContextConnectionString = configuration.GetValue<string>("CarServiceDbContext")
            };
        }
    }
}