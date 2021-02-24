using System;
using System.Linq;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheRockPaperScissors.Server.Enums;
using TheRockPaperScissors.Server.Models;
using TheRockPaperScissors.Server.Services;

namespace TheRockPaperScissors.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;
        private readonly ISeriesStorage _seriesStorage;
        private readonly IStorage<Guid, User> _users;

        public GameController(
            ISeriesStorage seriesStorage,
            IStorage<Guid, User> users,
            ILogger<GameController> logger)
        {
            _seriesStorage = seriesStorage;
            _users = users;
            _logger = logger;
        }

        [HttpGet("test/{token}")]
        public async Task<ActionResult> Test(
            [FromServices]ISeriesService series,
            [FromRoute(Name = "token")]string token)
        {
            await Task.Delay(500);
            var guid = Guid.Parse(token);

            if (await _users.ContainAsync(guid)) return NotFound($"Not found user with token {guid}");

            series.Type = GameType.Test;
            series.FirstId = guid;
            await _seriesStorage.AddAsync(series);
            return Ok(token);
        }

        [HttpPost("private/{token}")]
        public async Task<ActionResult> PrivateCreate(
            [FromServices]ISeriesService series,
            [FromRoute(Name = "token")]string token)
        {
            await Task.Delay(500);
            var guid = Guid.Parse(token);

            if (!await _users.ContainAsync(guid)) return NotFound($"Not found user with token {guid}");

            series.Type = GameType.Private;
            series.FirstId = guid;
            series.GameId = new Random().Next(1000, 9999).ToString();
            await _seriesStorage.AddAsync(series);
            return Ok(token);
        }

        [HttpPost("private/{token}/{id}")]
        public async Task<ActionResult> PrivateConnect(
            [FromRoute(Name = "token")]string token,
            [FromRoute(Name = "id")]string id)
        {
            await Task.Delay(500);
            var guid = Guid.Parse(token);

            if (!await _users.ContainAsync(guid)) return NotFound($"Not found user with token {guid}");

            var series = await _seriesStorage.GetAsync(storage =>
                storage.FirstOrDefault(s => s.GameId == id));

            if (series == null) return NotFound("Not found series");
            series.SecondId = Guid.Parse(token);
            return Ok(series);
        }

        [HttpPost("public/{token}")]
        public async Task<ActionResult> Public(
            [FromServices] ISeriesService series,
            [FromRoute(Name = "token")]string token)
        {
            await Task.Delay(500);
            var guid = Guid.Parse(token);

            if (!await _users.ContainAsync(guid)) return NotFound($"Not found user with token {guid}");

            var openSeries = await _seriesStorage.GetAsync(storage =>
                storage.FirstOrDefault(s => s.SecondId == null));

            if (openSeries != null)
            {
                openSeries.SecondId = guid;
            }
            else
            {
                series.FirstId = guid;
                series.Type = GameType.Public;
                await _seriesStorage.AddAsync(series);
            }
            return Ok();
        }
    }
}
