using EventBooking.Application.Common.Interfaces.Interfaces;
using EventBooking.Application.Common.Interfaces.Persistence;
using EventBooking.Application.Common.Interfaces.Persistence.Repositories;
using EventBooking.Application.Common.Interfaces.Services;
using EventBooking.Infrastructure.Common.Mappings;
using EventBooking.Infrastructure.Persistence;
using EventBooking.Infrastructure.Persistence.Initializer;
using EventBooking.Infrastructure.Persistence.Repositories;
using EventBooking.Infrastructure.Providers;
using EventBooking.Infrastructure.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventBooking.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddMapping();
        service.AddPersistance(configuration);
        service.AddService();
        return service;
    }
    public static IServiceCollection AddPersistance(this IServiceCollection service, IConfiguration configuration)
    {
        #region Settings Configure
        service.Configure<InfrastructureSetting>(configuration.GetSection(InfrastructureSetting.Name));

        #endregion

        var connStr = configuration.GetConnectionString("DefaultConnection");
        service.AddDbContext<EventBookingDbContext>(options => options.UseSqlite(connStr));
        service.AddScoped<IUnitOfWork, UnitOfWork>();
        service.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        service.AddScoped<ICacheService, CacheService>();
        service.AddTransient<MigrationAndSeedRunner>();
        return service;
    }

    public static IServiceCollection AddService(this IServiceCollection service)
    {
        service.AddTransient<IUserService, UserService>();
        return service;
    }

    public static async Task<IApplicationBuilder> InitializeDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.GetService<IServiceProvider>().CreateScope();

        await scope.ServiceProvider.GetRequiredService<MigrationAndSeedRunner>()
           .InitializeAsync();
        return app;
    }
}

