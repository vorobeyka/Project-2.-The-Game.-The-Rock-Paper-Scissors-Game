using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheRockPaperScissors.Server.Services;

namespace TheRockPaperScissors.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;
        private readonly ILogger<StatisticsController> _logger;

        public StatisticsController(
            IDatabaseService databaseService,
            ILogger<StatisticsController> logger)
        {
            _databaseService = databaseService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Public()
        {
            var result = await _databaseService.GetPublicStatisticsAsync();
            return Ok(result);
        }

        [HttpGet("{login}")]
        public async Task<ActionResult<string>> Public([FromRoute(Name = "login")] string login)
        {
            var result = await _databaseService.GetUserStatisticsAsync(login);
            return Ok(result);
        }
    }
}
