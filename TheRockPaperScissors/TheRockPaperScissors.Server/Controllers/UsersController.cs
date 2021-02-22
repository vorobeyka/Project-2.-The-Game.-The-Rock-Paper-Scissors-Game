using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Threading;
using TheRockPaperScissors.Server.Models;
using TheRockPaperScissors.Server.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Security.Cryptography;


namespace TheRockPaperScissors.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        //private string Path = "users.json";

        private ConcurrentQueue<User> Users = new ConcurrentQueue<User>();

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string login, string password)
        {
            if (ModelState.IsValid)
            {
                var user = Users.FirstOrDefault(u => u.Login == login && u.Password == GetHashString(password));
                if (user != null)
                {
                    return RedirectToAction("");
                }
                ModelState.AddModelError("", "Incorrect login or password!");
            }
            return RedirectToAction("Login", "User");
        }

        [HttpGet("Register")]
        public async Task<IActionResult> Register(string login, string password)
        {
            if (ModelState.IsValid)
            {
                User user = Users.FirstOrDefault(u => u.Login == login);
                if (user == null)
                {
                    Users.Append(new User { Login = login, Password = GetHashString(password) });
                    return Ok(Guid.NewGuid());
                }
                else
                    ModelState.AddModelError("", "Incorrect login and/or password!");
            }
            return RedirectToAction("Register", "Owners");
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

        public bool IsPasswordValid(string password)
        {
            return password.Length > 5;
        }
    }
}
