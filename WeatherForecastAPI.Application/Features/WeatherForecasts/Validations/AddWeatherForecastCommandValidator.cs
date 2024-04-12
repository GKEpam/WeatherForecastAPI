using FluentValidation;
using WeatherForecastAPI.Application.Features.WeatherForecasts.Commands;

namespace WeatherForecastAPI.Application.Features.WeatherForecasts.Validations;

public class AddWeatherForecastCommandValidator : AbstractValidator<AddWeatherForecastCommand>
{
    public AddWeatherForecastCommandValidator()
    {
        RuleFor(request => request.WeatherForecast.Date).NotEmpty();
        RuleFor(request => request.WeatherForecast.TemperatureC).InclusiveBetween(-20, 55);
        RuleFor(request => request.WeatherForecast.Summary).NotEmpty().MaximumLength(200);
    }
}
