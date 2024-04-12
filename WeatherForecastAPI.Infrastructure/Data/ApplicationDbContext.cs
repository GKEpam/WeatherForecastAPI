using Microsoft.EntityFrameworkCore;
using WeatherForecastAPI.Domain.Entities;

namespace WeatherForecastAPI.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public virtual DbSet<WeatherForecast> WeatherForecasts { get; set; }
}
