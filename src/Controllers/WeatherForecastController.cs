using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace SampleWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public record TempZone(int LowTempF, int HighTempF, string Name);

        private static readonly TempZone[] Zones =
        {
            new TempZone(LowTempF: -999,  HighTempF: 39, Name: "Freezing"),
            new TempZone(LowTempF: 40, HighTempF: 59, Name: "Chilly"),
            new TempZone(LowTempF: 60, HighTempF: 79, Name: "Mild"),
            new TempZone(LowTempF: 80, HighTempF: 89, Name: "Warm"),
            new TempZone(LowTempF: 90, HighTempF: 999, Name: "Hot"),
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var rng = new Random();
            int tempF = rng.Next(0, 100);
            Option<TempZone> zone = Zones.FirstOrDefault(t => t.LowTempF < tempF
                                                           && t.HighTempF >= tempF);
            var result = new
            {
                Date = DateTime.Parse("2021.01.06 5:15pm"),
                TemperatureF = tempF,
                Summary = zone.Match(Some: v => v.Name, None: () => "n/a"),
                SourceMachine = Environment.MachineName
            };
            return Ok(result);
        }
    }
}
