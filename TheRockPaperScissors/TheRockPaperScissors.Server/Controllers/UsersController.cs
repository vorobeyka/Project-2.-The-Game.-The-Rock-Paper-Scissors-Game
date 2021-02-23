using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheRockPaperScissors.Server.Services;
using TheRockPaperScissors.Server.Models;

namespace TheRockPaperScissors.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(UserService));
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<ActionResult<Guid>> Login([FromBody]User user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            /*var login = HttpContext.Request.Query["login"].FirstOrDefault();
            var password = HttpContext.Request.Query["password"].FirstOrDefault();*/

            //_logger.LogInformation($"Try to login with login {login} and password {password}");

            var token = await _userService.LoginUserAsync(user.Login, user.Password);
            
            //ar user = await _userService.GetUser(login, password);
            if (token == null)
            {
                _logger.LogInformation($"Invalid login '{user.Login}' or password '{user.Password}'");

                return BadRequest($"Invalid login or password");
            }

            _logger.LogInformation($"Success to login {user.Login}");

            return Ok(token);
        }

        [HttpPost("register")]
        public async Task<ActionResult<Guid>> Register([FromBody]User user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            //var login = HttpContext.Request.Query["login"].FirstOrDefault();
            //var password = HttpContext.Request.Query["password"].FirstOrDefault();

            //_logger.LogInformation($"Try to register with login {login} and password {password}");

            var token = await _userService.RegisterUserAsync(user);

            /*if (!IsPasswordValid(user.password))
            {
                _logger.LogInformation($"Not valid password");

                return BadRequest("Password length must be more than 5 and less than 100");
            }
            if (!IsLoginValid(login))
            {
                _logger.LogInformation($"Not valid login");

                return BadRequest("Login length must be more than 2 and less than 100");
            }*/

            if (token != null)
            {
                _logger.LogInformation($"Registered user with login '{user.Login}' and password '{user.Password}'");
                return Ok(token);
            }
            return BadRequest("Ooops! Something was wrong...");
        }

        public string GetHashString(string s)
        {
            //переводим строку в байт-массим  
            byte[] bytes = Encoding.Unicode.GetBytes(s);

            //создаем объект для получения средст шифрования  
            MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = CSP.ComputeHash(bytes);
            string hash = string.Empty;

            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }

        private static bool IsPasswordValid(string password)
        {
            return password.Length > 5 && password.Length < 100;
        }

        private static bool IsLoginValid(string login)
        {
            return login.Length > 2 && login.Length < 100;
        }
    }
}
