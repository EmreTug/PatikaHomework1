using PatikaHomework1.Interfaces;
using PatikaHomework1.Models;

namespace PatikaHomework1.Repositories
{
    // Kullanıcı verilerini yönetir
    public class UserRepository : IUserRepository
    {
        // Kullanıcı verilerinin tutulduğu liste
        private readonly List<User> _users = new List<User>
        {
            new User {Username = "user1", Password = "password1"},
            new User {Username = "user2", Password = "password2"}
        };

        // Kullanıcının doğruluğunu kontrol eder
        public bool ValidateUser(string username, string password)
        {
            // Kullanıcı adı ve şifresinin doğruluğunu kontrol eder
            return _users.Any(user => user.Username == username && user.Password == password);
        }
    }
}
