using System.Collections.Generic;
using System.Linq;
using Desafio.Api.Entities;

namespace Desafio.Api.Data
{
    public static class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, Username = "desafio", Password = "desafio", Role="manager" });
            return users.Where(x => x.Username.ToLower() == username.ToLower() && x.Password == password).FirstOrDefault();
        }
    }
}