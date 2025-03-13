using AtWork.Services.Auth;

namespace AtWork.Tests
{
    public class PasswordTests
    {
        [Fact(DisplayName = "Given a password, return its hash")]
        public void GivenAPassword_ReturnItsHash()
        {
            const string password = "123";
            const string expectedHash = "MTIz";

            var passwordHasher = new PasswordHashService();

            string generatedHash = passwordHasher.Hash(password);

            Assert.Equal(expectedHash, generatedHash);
        }

        [Fact(DisplayName = "Given an empty password, throw exception")]
        public void GivenAnEmptyPassword_ThrowException()
        {
            const string emptyPassword = "";

            var passwordHasher = new PasswordHashService();

            var exception = Assert.Throws<ArgumentException>(() => passwordHasher.Hash(emptyPassword));

            Assert.NotNull(exception);
        }

        [Fact(DisplayName = "Given a hash, verify the password")]
        public void GivenAHash_VerifyThePassword()
        {
            const string hash = "MTIz";
            const string expectedPassword = "123";

            var passwordHasher = new PasswordHashService();

            bool generatedHash = passwordHasher.VerifyPassword(expectedPassword, hash);

            Assert.True(generatedHash);
        }

        [Fact(DisplayName = "Given an empty hash, throw exception")]
        public void GivenAnEmptyHash_ThrowException()
        {
            const string emptyHash = "";
            const string emptyPassword = "";

            var passwordHasher = new PasswordHashService();

            var exception = Assert.Throws<ArgumentException>(() => passwordHasher.VerifyPassword(emptyHash, emptyPassword));

            Assert.NotNull(exception);
        }
    }
}
