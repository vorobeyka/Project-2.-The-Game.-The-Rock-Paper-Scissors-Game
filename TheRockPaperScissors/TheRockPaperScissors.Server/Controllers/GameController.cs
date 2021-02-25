using System;
using System.Linq;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheRockPaperScissors.Server.Enums;
using TheRockPaperScissors.Server.Exceptions;
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

        [HttpPost]
        public async Task<ActionResult> Game(
            [FromServices] ISeriesService series,
            [FromBody] Game game)
        {
            await Task.Delay(500);
            var userId = Guid.Parse(game.UserId);

            if (!await _users.ContainAsync(userId)) return NotFound($"Not found user with token {userId}");

            var openSeries = await _seriesStorage.GetAsync(storage =>
                storage.FirstOrDefault(s => s.SecondId == null && s.Type == game.Type && s.GameId == game.GameId));
            var foundSeries = openSeries != null;
            if (!foundSeries) openSeries = series;
            
            try
            {
                openSeries.SetProperties(game);
            }
            catch (SeriesException)
            {
                return BadRequest("Invalid game id or game have maximum users");
            }

            if (!foundSeries) await _seriesStorage.AddAsync(openSeries);
            return Ok(openSeries.GameId);
        }

        [HttpGet("start/{token}")]
        public async Task<ActionResult> Start([FromRoute(Name = "token")] string token)
        {
            await Task.Delay(500);
            var id = Guid.Parse(token);
            var series = await _seriesStorage.GetAsync(storage =>
                storage.FirstOrDefault(series => series.IsRegisteredId(id)));

            var time = 0;
            while (series.SecondId == null && time < 300)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                time++;
            }
            if (time == 300) return NotFound("Time is out! No one connected to you");
            return Ok(true);
        }

        [HttpPost("round")]
        public async Task<ActionResult> Round(
            [FromBody]Round round,
            [FromServices] IRoundService roundService)
        {
            await Task.Delay(500);
            var id = Guid.Parse(round.Id);
            var series = await _seriesStorage.GetByIdAsync(id);
            var openRound = await series.GetOpenRoundAsync() ?? await series.AddRoundAsync(roundService);

            if (!openRound.AddMove(id, round.Move)) return BadRequest("Can't add round(");
            return Ok();
        }

        [HttpGet("roundResult/{token}")]
        public async Task<ActionResult> GetRoundResult([FromRoute(Name = "token")] string token)
        {
            await Task.Delay(500);
            var id = Guid.Parse(token);
            var game = await _seriesStorage.GetByIdAsync(id);
            var round = await game.GetLastRoundAsync();
            var result = await round.GetResultAsync(id);

            if (string.IsNullOrEmpty(result)) return NotFound(await GetSeriesResult(token));
            else return Ok(result);
        }

        [HttpGet("seriesResult/{token}")]
        public async Task<ActionResult> GetSeriesResult([FromRoute(Name = "token")] string token)
        {
            await Task.Delay(500);
            var id = Guid.Parse(token);
            var series = await _seriesStorage.GetByIdAsync(id);
            return Ok(series.GetResult(id));
        }
    }
}
