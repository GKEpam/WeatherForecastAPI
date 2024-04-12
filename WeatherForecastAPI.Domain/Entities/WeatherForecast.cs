namespace WeatherForecastAPI.Domain.Entities;

public class WeatherForecast
{
    public int Id { get; set; }

    public int TemperatureC { get; set; }

    public DateOnly Date { get; set; }

    public string? Summary { get; set; }
}
