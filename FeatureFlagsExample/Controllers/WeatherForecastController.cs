using FeatureFlagsExample.Enums;
using FeatureFlagsExample.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;

namespace FeatureFlagsExample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private static readonly string[] Regions = new[]
    {
        "Brazil", "EUA", "Polonia", "Portugal"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IFeatureManager _featureManager;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IFeatureManager featureManager)
    {
        _logger = logger;
        _featureManager = featureManager;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<dynamic>> Get()
    {
        var featureFlagName = FeatureFlags.FeatureFlagWeatherForecast.GetDescription();

        if (await _featureManager.IsEnabledAsync(featureFlagName))
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecastV2
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                    Region = Regions[Random.Shared.Next(Regions.Length)]
                })
                .ToArray();
        }
        
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)],
            })
            .ToArray();
      
        
    }
}