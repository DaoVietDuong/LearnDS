using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D.MoveOn.Common;
using D.MoveOn.Common.Dispatchers;
using D.MoveOn.Common.RabbitMQ;
using D.MoveOn.Order.Messages.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace D.MoveOn.Order.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly IDispatcher _dispatcher;
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IDispatcher dispatcher,
                                         IBusPublisher busPublisher,
                                         ILogger<WeatherForecastController> logger)
        {
            this._dispatcher = dispatcher;
            this._busPublisher = busPublisher;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("CreateIem")]
        public async Task<IActionResult> CreateItem(CreateOrderCommand command){
            await _dispatcher.SendAsync(command);
             await _busPublisher.SendAsync(command, CorrelationContext.Empty);
             return Accepted();
        }
    }
}
