using PatikaHomework1.Interfaces;
using PatikaHomework1.Models;

namespace PatikaHomework1.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>
    {
        new User {Username = "user1", Password = "password1"},
        new User {Username = "user2", Password = "password2"}
    };

        public bool ValidateUser(string username, string password)
        {
            return _users.Any(user => user.Username == username && user.Password == password);
        }
    }

  
}
