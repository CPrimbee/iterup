using System.Linq;
using Desafio.Api.Entities;

namespace Desafio.Api.Data
{
    public static class UserRepository
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, Username = "batman", Password = "batman", Role="manager" });
            users.Add(new User { Id = 2, Username = "robin", Password = "robin", Role="employee" });
            
        }
    }
}