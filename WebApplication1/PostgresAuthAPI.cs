using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using LinqModels;
using LinqToDB.Data;

//This is pretty much a hack to get different user logins into postgresql. 
//current linq api only allows one setting at a time in DataConnection. 
public class PostgresAuthAPI
{
	private PostgresAuthAPI()
	{
	}

	public static User roleFromLogin(string user, string password){
		//get the role type from the user table
		DataConnection.DefaultSettings = new PostgreSQLDbSettings("Server = localhost; Port = 5432; Database = postgres; User Id = read_users; Password = jeff; Pooling = true; MinPoolSize = 10; MaxPoolSize = 100;");

		User completeUser = new User();
		using (var db = new PostgresDB())
		{
			var q =
				from u in db.Users
				where u.UserColumn == user && u.Password == password
				select u;

			bool userFound = false;
			bool bDuplicateUser = false;
			foreach (var u in q) {
				if (userFound) {
					bDuplicateUser = true;
					break;
				}
				userFound = true;
				completeUser = u;

				Console.WriteLine(completeUser.Id + ", " + completeUser.UserColumn + ", " + completeUser.Password + ", " + completeUser.Role);
			}
			if (bDuplicateUser) { 
				//raise warning
			}
		}

		return completeUser;


		//return the role back to be cookied and used by the client.

		//set the settings to be that role as given per resource acquisition by client.

		//DataConnection.DefaultSettings = new PostgreSQLDbSettings("Server = localhost; Port = 5432; Database = postgres; User Id = read_users; Password = jeff; Pooling = true; MinPoolSize = 10; MaxPoolSize = 100;");


	}
}
