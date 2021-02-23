using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheRockPaperScissors.Server.Services;

namespace TheRockPaperScissors.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;

        public GameController(ILogger<GameController> logger)
        {
            _logger = logger;
        }

        /*[HttpGet("Test/{id}")]
        public Task<ActionResult<string>> Test()
        {

        }

        [HttpGet("Private/{id}")]
        public Task<ActionResult<string>> Private()
        {

        }

        [HttpGet("Public/{id}")]
        public Task<ActionResult<string>> Public()
        {

        }*/
    }
}
