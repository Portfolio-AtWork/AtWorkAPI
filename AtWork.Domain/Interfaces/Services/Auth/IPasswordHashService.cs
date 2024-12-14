namespace AtWork.Domain.Interfaces.Services.Auth
{
    public interface IPasswordHashService : IBaseService
    {
        public string Hash(string password);
        public bool VerifyPassword(string password, string hash);
    }
}
