using System;
using System.Threading.Tasks;

namespace Grocery.App.Views
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

    public static class UserStore
    {
        private static readonly List<User> users = new();

        public static bool UserExists(string username)
        {
            return users.Exists(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public static void AddUser(User user)
        {
            users.Add(user);
        }
    }

    public partial class RegisterUserView
    {
        public async Task<string> RegisterUserAsync(string username, string password, string email)
        {
            await Task.Delay(100);

            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(email))
            {
                return "All fields are required.";
            }

            if (UserStore.UserExists(username))
            {
                return "Username already exists.";
            }

            var user = new User
            {
                Password = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password)),
                Username = username,
                Email = email
            };

            UserStore.AddUser(user);

            return "Registration successful.";
        }
    }
}
