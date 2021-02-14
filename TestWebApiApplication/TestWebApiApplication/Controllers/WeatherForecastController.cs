using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestWebApiApplication.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private readonly ILogger<WeatherForecastController> logger;

		private static readonly string[] Summaries =
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		public WeatherForecastController(ILogger<WeatherForecastController> logger)
		{
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		[HttpGet]
		public IEnumerable<WeatherForecast> Get()
		{
			logger.LogInformation("Preparing Weather Forecast ...");

			var rng = new Random();
			var forecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateTime.Now.AddDays(index),
				TemperatureC = rng.Next(-20, 55),
				Summary = Summaries[rng.Next(Summaries.Length)]
			})
			.ToArray();

			logger.LogInformation("Forecast was prepared successfully. First summary is {FirstSummary}", forecast.First().Summary);

			return forecast;
		}
	}
}
