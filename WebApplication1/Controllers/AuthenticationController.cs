using LinqModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(ILogger<AuthenticationController> logger)
        {
            _logger = logger;
        }
        public class LoginInfo
        {
            public string username { get; set; }
            public string password { get; set; }
        }

        [Route("login")]
        [HttpPost]
        public IActionResult AuthenticateUser(LoginInfo loginInfo)
        {
            if (string.IsNullOrEmpty(loginInfo.username) || string.IsNullOrEmpty(loginInfo.password))
            {
                if (string.IsNullOrEmpty(loginInfo.username))
                {
                    return BadRequest("missing username");
                }
                else
                {
                    return BadRequest("missing password");
                }
            }

            User u = PostgresAuthAPI.roleFromLogin(loginInfo.username, loginInfo.password);
            return Json(new { success = (u.Role!=""), u });
        }
    }
}
