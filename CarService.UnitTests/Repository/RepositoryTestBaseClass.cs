using CarService.DataAccess;
using CarService.WebApi.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarService.UnitTests.Repository;

public class RepositoryTestsBaseClass
{
    public RepositoryTestsBaseClass()
    {
        //13.11 - лекция по бизнес логике 
        //3 лаба включает тесты - дедлайн 13.11
        //20.11 - практика по бизнес логике - делаем лабу 4 - 27.11 включать тесты
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        Settings = CarServiceSettingsReader.Read(configuration);
        ServiceProvider = ConfigureServiceProvider();

        DbContextFactory = ServiceProvider.GetRequiredService<IDbContextFactory<CarServiceDbContext>>();
    }

    private IServiceProvider ConfigureServiceProvider()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDbContextFactory<CarServiceDbContext>(
            options => { options.UseSqlServer(Settings.CarServiceDbContextConnectionString); },
            ServiceLifetime.Scoped);
        return serviceCollection.BuildServiceProvider();
    }

    protected readonly CarServiceSettings Settings;
    protected readonly IDbContextFactory<CarServiceDbContext> DbContextFactory;
    protected readonly IServiceProvider ServiceProvider;
}