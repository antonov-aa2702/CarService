using CarService.DataAccess;
using CarService.WebApi.Settings;
using Microsoft.EntityFrameworkCore;

namespace CarService.WebApi.IoC;

public static class DbContextConfigurator
{
    public static void ConfigureService(IServiceCollection services, CarServiceSettings settings)
    {
        services.AddDbContextFactory<CarServiceDbContext>(
            options => { options.UseSqlServer(settings.CarServiceDbContextConnectionString); },
            ServiceLifetime.Scoped);
    }

    public static void ConfigureApplication(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<CarServiceDbContext>>();
        using var context = contextFactory.CreateDbContext();
        context.Database.Migrate(); //makes last migrations to db and creates database if it doesn't exist
    }
}