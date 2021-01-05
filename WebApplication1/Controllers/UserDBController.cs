using LinqModels;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class PeopleController : Controller
    {
        private readonly PostgresDataConnection _connection;

        public PeopleController(PostgresDataConnection connection)
        {
            _connection = connection;
        }

        [HttpGet]
        public Task<User[]> ListPeople()
        {
            return _connection.Users.ToArrayAsync();
        }

        //[HttpGet("{id}")]
        //public Task<User?> GetUser(Guid long)
        //{
        //    return _connection.Users.SingleOrDefaultAsync(User => User.Id == id);
        //}

        //[HttpDelete("{id}")]
        //public Task<int> DeleteUser(Guid id)
        //{
        //    return _connection.People.Where(User => User.Id == id).DeleteAsync();
        //}

        //[HttpPatch]
        //public Task<int> UpdateUser(User User)
        //{
        //    return _connection.UpdateAsync(User);
        //}

        //[HttpPatch("{id}/new-name")]
        //public Task<int> UpdateUserName(Guid id, string newName)
        //{
        //    return _connection.People.Where(User => User.Id == id)
        //        .Set(User => User.Name, newName)
        //        .UpdateAsync();
        //}

        //[HttpPut]
        //public Task<int> InsertUser(User User)
        //{
        //    return _connection.InsertAsync(User);
        //}
    }
}
