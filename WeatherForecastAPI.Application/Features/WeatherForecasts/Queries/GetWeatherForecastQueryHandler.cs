using MediatR;
using Microsoft.EntityFrameworkCore;
using WeatherForecastAPI.Application.Features.WeatherForecasts.Models;
using WeatherForecastAPI.Infrastructure.Data;

namespace WeatherForecastAPI.Application.Features.WeatherForecasts.Queries;

public record GetWeatherForecastQuery : IRequest<IEnumerable<WeatherForecastModel>>;

public class GetWeatherForecastQueryHandler(ApplicationDbContext context) : IRequestHandler<GetWeatherForecastQuery, IEnumerable<WeatherForecastModel>>
{
    public async Task<IEnumerable<WeatherForecastModel>> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
    {
        var weatherForecasts = await context.WeatherForecasts
            .AsNoTracking()
            .Select(t => new WeatherForecastModel(t.Date, t.TemperatureC, t.Summary))
            .ToListAsync(cancellationToken);

        return weatherForecasts;
    }
}
