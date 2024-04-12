using MediatR;
using WeatherForecastAPI.Application.Features.WeatherForecasts.Models;
using WeatherForecastAPI.Domain.Entities;
using WeatherForecastAPI.Infrastructure.Data;

namespace WeatherForecastAPI.Application.Features.WeatherForecasts.Commands;

public record AddWeatherForecastCommand(WeatherForecastModel WeatherForecast) : IRequest<bool>;

public class AddWeatherForecastCommandHandler(ApplicationDbContext context) : IRequestHandler<AddWeatherForecastCommand, bool>
{
    public async Task<bool> Handle(AddWeatherForecastCommand request, CancellationToken cancellationToken)
    {
        WeatherForecast entity = new()
        {
            Date = request.WeatherForecast.Date,
            TemperatureC = request.WeatherForecast.TemperatureC,
            Summary = request.WeatherForecast.Summary
        };

        await context.WeatherForecasts.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
