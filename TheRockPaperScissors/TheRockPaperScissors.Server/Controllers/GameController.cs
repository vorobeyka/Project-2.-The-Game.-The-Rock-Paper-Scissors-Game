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

        [HttpPost]
        public async Task<ActionResult> Game(
            [FromServices] ISeriesService series,
            [FromBody] Game game)
        {
            await Task.Delay(500);
            _logger.LogInformation(game.UserId);
            var userId = Guid.Parse(game.UserId);

            if (!await _users.ContainAsync(userId)) return NotFound($"Not found user with token {userId}");

            var openSeries = await _seriesStorage.GetAsync(storage =>
                storage.FirstOrDefault(s => s.SecondId == null && s.Type == game.Type && s.GameId == game.GameId))
                ?? series;

            openSeries.Type = game.Type;
            try
            {
                openSeries.SetProperties(userId, game.GameId);
            }
            catch (Exception)
            {
                return BadRequest("Invalid private id or room is full");
            }

            await _seriesStorage.AddAsync(openSeries);
            return Ok(openSeries.GameId);
        }

        [HttpGet("start/{token}")]
        public async Task<ActionResult> Start([FromRoute(Name = "token")] string token)
        {
            await Task.Delay(500);
            var id = Guid.Parse(token);
            var game = await _seriesStorage.GetAsync(storage =>
                storage.FirstOrDefault(series => series.IsRegisteredId(id)));

            var time = 0;
            while (game.SecondId == null && time < 300)
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
            var game = await _seriesStorage.GetAsync(storage => storage.FirstOrDefault(series => series.IsRegisteredId(id)));
            var openRound = game.RoundCount == 0
                ? await game.AddRoundAsync(roundService)
                : await game.GetOpenRoundAsync();

            _logger.LogInformation(game.RoundCount.ToString());
            if (!openRound.AddMove(id, round.Move)) return BadRequest("Can't add round(");
            return Ok();
        }

        [HttpGet("roundResult/{token}")]
        public async Task<ActionResult> GetRoundResult([FromRoute(Name = "token")] string token)
        {
            await Task.Delay(500);
            var id = Guid.Parse(token);
            var game = await _seriesStorage.GetAsync(storage =>storage.FirstOrDefault(series => series.IsRegisteredId(id)));
            var round = await game.GetLastRoundAsync();
            var result = await round.GetResultAsync(id);

            var otherId = id == game.FirstId ? game.SecondId : game.FirstId;
            var str = result;
            str += await round.GetResultAsync((Guid)otherId);


            if (string.IsNullOrEmpty(result)) return await GetSeriesResult();
            else return Ok(str);
        }

        [HttpGet("seriesResult")]
        public async Task<ActionResult> GetSeriesResult()
        {
            await Task.Delay(500);
            return Ok("Series result");
        }

        /*[HttpPost("test/{token}")]
        public async Task<ActionResult> Test(
            [FromServices]ISeriesService series,
            [FromRoute(Name = "token")] string token)
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
            [FromRoute(Name = "token")] string token)
        {
            await Task.Delay(500);
            var guid = Guid.Parse(token);

            if (!await _users.ContainAsync(guid)) return NotFound($"Not found user with token {guid}");

            var gameId = new Random().Next(1000, 9999).ToString();
            series.Type = GameType.Private;
            series.FirstId = guid;
            series.GameId = gameId;
            await _seriesStorage.AddAsync(series);
            return Ok(gameId);
        }

        [HttpPost("private/{token}/{id}")]
        public async Task<ActionResult> PrivateConnect(
            [FromRoute(Name = "token")] string token,
            [FromRoute(Name = "id")] string id)
        {
            await Task.Delay(500);
            var guid = Guid.Parse(token);

            if (!await _users.ContainAsync(guid)) return NotFound($"Not found user with token {guid}");

            var series = await _seriesStorage.GetAsync(storage =>
                storage.FirstOrDefault(s => s.GameId == id));

            if (series == null) return NotFound("Not found series");
            series.SecondId = Guid.Parse(token);
            return Ok();
        }

        [HttpPost("public/{token}")]
        public async Task<ActionResult> Public(
            [FromServices] ISeriesService series,
            [FromRoute(Name = "token")] string token)
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
        }*/
    }
}
