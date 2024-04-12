using MediatR.Extensions.FluentValidation.AspNetCore;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WeatherForecastAPI.Application.Behaviors;
using WeatherForecastAPI.Infrastructure.Data;

namespace WeatherForecastAPI.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddTransient(typeof(IRequestExceptionAction<,>), typeof(LogExceptionActionBehavior<,>));

        services.AddFluentValidation([Assembly.GetExecutingAssembly()]);

        return services;
    }
}
