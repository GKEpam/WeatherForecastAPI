using Microsoft.EntityFrameworkCore;
using WeatherForecastAPI.Application.Features.WeatherForecasts.Queries;
using WeatherForecastAPI.Domain.Entities;
using WeatherForecastAPI.Infrastructure.Data;

namespace WeatherForecastAPI.Tests;

public class GetWeatherForecastQueryHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsCorrectForecasts()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "WeatherForecastTestDb")
            .Options;

        using var context = new ApplicationDbContext(options);
        await context.WeatherForecasts.AddAsync(new WeatherForecast { Date = DateOnly.MinValue, TemperatureC = 25, Summary = "Sunny" });
        await context.SaveChangesAsync();

        var handler = new GetWeatherForecastQueryHandler(context);

        // Act
        var result = await handler.Handle(new GetWeatherForecastQuery(), CancellationToken.None);

        // Assert
        Assert.Single(result);
        var forecast = result.First();
        Assert.Equal("Sunny", forecast.Summary);
    }
}