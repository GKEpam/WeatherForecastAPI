using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WeatherForecastAPI.Application.Features.WeatherForecasts.Commands;
using WeatherForecastAPI.Application.Features.WeatherForecasts.Models;
using WeatherForecastAPI.Application.Features.WeatherForecasts.Queries;

namespace WeatherForecastAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<WeatherForecastModel>>> Get()
    {
        var query = new GetWeatherForecastQuery();
        var result = await mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add(WeatherForecastModel model)
    {
        try
        {
            var result = await mediator.Send(new AddWeatherForecastCommand(model));
            return Ok(result);
        }
        catch (ValidationException ex)
        {
            var errors = ex.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
            return BadRequest(new { Errors = errors });
        }
    }
}
