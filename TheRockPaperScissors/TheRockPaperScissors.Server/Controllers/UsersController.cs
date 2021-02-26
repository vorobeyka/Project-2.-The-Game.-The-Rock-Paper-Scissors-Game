using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheRockPaperScissors.Server.Services;
using TheRockPaperScissors.Server.Models;
using System.Net.Mime;
using TheRockPaperScissors.Server.Services.Impl;

namespace TheRockPaperScissors.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUsersStorage _authorizedUsers;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService,
                               IUsersStorage authorizedUsers,
                               ILogger<UsersController> logger)
        {
            _authorizedUsers = authorizedUsers ?? throw new ArgumentNullException(nameof(authorizedUsers));
            _userService = userService ?? throw new ArgumentNullException(nameof(UserService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost("login")]
        public async Task<ActionResult<Guid>> Login([FromBody]User user)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            if (await _authorizedUsers.ContainValueAsync(user)) 
                return BadRequest("User are authorized");

            var loginResult = await _userService.LoginUserAsync(user.Login, GetHashString(user.Password));
            Console.WriteLine(user.Password);
            var token = loginResult.Item1;

            if (token == null)
            {
                _logger.LogInformation($"Invalid login '{user.Login}' or password '{user.Password}'");
                return BadRequest($"Invalid login or password");
            }

            _logger.LogInformation($"Success to login {user.Login}");
            await _authorizedUsers.AddAsync((Guid)token, loginResult.Item2);

            return Ok(token);
        }

        [HttpPost("register")]
        public async Task<ActionResult<Guid>> Register(
            [FromBody]User user)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);
            var hash = GetHashString(user.Password);
            user.Password = hash;
            var token = await _userService.RegisterUserAsync(user);
            Console.WriteLine(user.Password);
            if (token != null)
            {
                _logger.LogInformation($"Registered user with login '{user.Login}'");
                await _authorizedUsers.AddAsync((Guid)token, user);
                return Ok(token);
            }

            return BadRequest("Ooops! Something was wrong...");
        }

        public string GetHashString(string s)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(s);
            MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();
            byte[] byteHash = CSP.ComputeHash(bytes);
            string hash = string.Empty;
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }
    }
}