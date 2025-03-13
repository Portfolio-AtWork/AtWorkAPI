using AtWork.Domain.Interfaces.Services.Auth;
using System.Text;

namespace AtWork.Services.Auth
{
    public class PasswordHashService : IPasswordHashService
    {
        public string Hash(string password)
        {
            ArgumentException.ThrowIfNullOrEmpty(password);

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            string weakHash = Convert.ToBase64String(passwordBytes);
            return weakHash;
        }

        public bool VerifyPassword(string password, string hash)
        {
            ArgumentException.ThrowIfNullOrEmpty(password);
            ArgumentException.ThrowIfNullOrEmpty(hash);

            bool valid = Hash(password) == hash;
            return valid;
        }
    }
}
