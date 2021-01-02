using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Data.Odbc;

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


            // postgresql read for login
            OdbcConnectionStringBuilder builder =
            new OdbcConnectionStringBuilder();

            builder.ConnectionString =
                "Driver={PostgreSQL ODBC Driver(UNICODE)};Server=localhost;Port=5432;Database=postgres;UID=main_admin;PWD=jeff";

            string queryString = "SELECT \"user\", id, password FROM public.\"user\" WHERE \"user\" = \'chef\' AND password = \'boomer\';";
            OdbcCommand command = new OdbcCommand(queryString);

            using (OdbcConnection connection = new OdbcConnection(builder.ConnectionString))
            {
                command.Connection = connection;
                connection.Open();


                OdbcDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader[0] + " - " + reader[1]);
                }

                reader.Close();
            }

            Console.WriteLine("Successful login");
            return Json(new { success = true });
        }
    }
}
