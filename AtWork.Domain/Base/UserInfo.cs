using AtWork.Domain.Database;
using Microsoft.AspNetCore.Http;

namespace AtWork.Domain.Base
{
    public class UserInfo
    {
        public string Login { get; set; }
        public Guid ID_Usuario { get; set; }
        public string Nome { get; set; }

        public UserInfo(IHttpContextAccessor httpContextAccessor, DatabaseContext db)
        {
            if (httpContextAccessor.HttpContext is null)
            {
                throw new ArgumentNullException(nameof(httpContextAccessor));
            }

            string login = httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "login").Value;

            var user = (from a in db.TB_Usuario where a.Login == login select a).FirstOrDefault();

            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            Login = user.Login;
            ID_Usuario = user.ID;
            Nome = user.Nome;
        }
    }
}
