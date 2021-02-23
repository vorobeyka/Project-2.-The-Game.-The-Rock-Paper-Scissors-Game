using System;
using System.Linq;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheRockPaperScissors.Server.Enums;
using TheRockPaperScissors.Server.Services;

namespace TheRockPaperScissors.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;
        private readonly ISeriesStorage _seriesStorage;

        public GameController(ISeriesStorage  seriesStorage, ILogger<GameController> logger)
        {
            _seriesStorage = seriesStorage;
            _logger = logger;
        }

        [HttpPost("test/{token}")]
        public async Task<ActionResult> Test(
            [FromServices]ISeriesService series,
            [FromQuery(Name = "token")]string token)
        {
            await Task.Delay(500);
            series.Type = GameType.Test;
            _logger.LogInformation(token);
            //series.FirstId = Guid.Parse(token);
            return Ok(series);
        }

        [HttpPost("private/{token}")]
        public async Task<ActionResult> Private(
            [FromServices]ISeriesService series,
            [FromQuery]string token)
        {
            _logger.LogInformation("syyyyyyyyyyyyyka");
            await Task.Delay(500);
            var id = Guid.Parse(Request.Query["id"]);
            series.FirstId = id;
            series.Type = GameType.Test;
            return Ok(series);
        }

        [HttpPost("public/{token}")]
        public async Task<ActionResult> Public(
            [FromServices]ISeriesService series,
            [FromQuery]string token)
        {
            await Task.Delay(500);
            var id = Guid.Parse(Request.Query["id"]);
            series.FirstId = id;
            series.Type = GameType.Test;
            return Ok(series);
        }
    }
}
