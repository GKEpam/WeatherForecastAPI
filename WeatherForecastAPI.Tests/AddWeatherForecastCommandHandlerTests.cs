using Microsoft.EntityFrameworkCore;
using WeatherForecastAPI.Application.Features.WeatherForecasts.Commands;
using WeatherForecastAPI.Application.Features.WeatherForecasts.Models;
using WeatherForecastAPI.Application.Features.WeatherForecasts.Validations;
using WeatherForecastAPI.Infrastructure.Data;

namespace WeatherForecastAPI.Tests;

public class AddWeatherForecastCommandHandlerTests
{
    [Fact]
    public async Task Handle_AddsWeatherForecastCorrectly()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "AddForecastTestDb")
            .Options;

        await using var context = new ApplicationDbContext(options);
        var handler = new AddWeatherForecastCommandHandler(context);

        var model = new WeatherForecastModel(DateOnly.MinValue, 20, "Sunny");
        var command = new AddWeatherForecastCommand(model);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var weatherForecast = await context.WeatherForecasts.FirstOrDefaultAsync();
        Assert.NotNull(weatherForecast);
        Assert.Equal("Sunny", weatherForecast.Summary);
        Assert.Equal(20, weatherForecast.TemperatureC);
    }

    [Fact]
    public void Should_Validate_When_CommandIsValid()
    {
        // Arrange
        var validator = new AddWeatherForecastCommandValidator();
        var model = new WeatherForecastModel(DateOnly.FromDateTime(DateTime.Now), 20, "Sunny");
        var command = new AddWeatherForecastCommand(model);

        // Act
        var result = validator.Validate(command);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(-21)]
    [InlineData(56)]
    public void Should_Invalidate_When_TemperatureIsOutOfRange(int tempC)
    {
        // Arrange
        var validator = new AddWeatherForecastCommandValidator();

        var model = new WeatherForecastModel(DateOnly.FromDateTime(DateTime.Now), tempC, "Sunny");
        var command = new AddWeatherForecastCommand(model);

        // Act
        var result = validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "WeatherForecast.TemperatureC");
    }
}
