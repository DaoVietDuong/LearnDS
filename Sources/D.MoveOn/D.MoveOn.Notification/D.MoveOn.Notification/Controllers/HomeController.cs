using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D.MoveOn.Notification.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace D.MoveOn.Notification.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<HomeController> _logger;
        private readonly ILufHubService _lufHubService;

        public HomeController(ILogger<HomeController> logger, ILufHubService lufHubService)
        {
            _logger = logger;
            this._lufHubService = lufHubService;
        }
        [HttpGet("Test")]
        public async Task<IActionResult> Test() {
            await _lufHubService.PublishToAllAsync("Hello everyone", new { id = 1, name = "Duong" });
            return Ok("Hello");
        }

        [HttpGet("")]
        public IActionResult Get() => Ok("Welcome demo");

        [HttpGet("Ping")]
        public IActionResult Ping() => Ok();
    }
}
