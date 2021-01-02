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

            string sUser = "";
            int sId = -1;
            string sPassword = "";
            using (OdbcConnection connection = new OdbcConnection(builder.ConnectionString))
            {
                command.Connection = connection;
                connection.Open();

                //todo: This needs to be replaced with an object mapper
                OdbcDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    sUser = reader[0].ToString();
                    sId = int.Parse(reader[1].ToString());
                    sPassword = reader[2].ToString();
                    Console.WriteLine("User: " + sUser + ", ID: " + sId + ", Password: " + sPassword);
                }

                reader.Close();
            }


            if (sUser != "" && sId != -1 && sPassword != "")
            {
                // Having a model 'is' the object mapper for server to client language
                Models.User loggedIn = new Models.User(sUser, sId, sPassword);
                Console.WriteLine("Successful login");
                return Json(new { success = true, user = Json(loggedIn)});
            }
            return Json(new { success = false });
        }
    }
}
