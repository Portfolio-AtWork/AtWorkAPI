using AtWork.Domain.Database;
using AtWork.Shared.Structs;
using Microsoft.AspNetCore.Http;

namespace AtWork.Domain.Base
{
    public class UserInfo
    {
        public string Login { get; set; }
        public Guid ID_Usuario { get; set; }
        public Guid ID_Funcionario { get; set; }
        public string Nome { get; set; }
        public string TP_Login { get; set; }

        private readonly static string[] tipos_login = [TipoLogin.Admin, TipoLogin.Funcionario];

        public UserInfo(IHttpContextAccessor httpContextAccessor, DatabaseContext db)
        {
            if (httpContextAccessor.HttpContext is null)
            {
                throw new ArgumentNullException(nameof(httpContextAccessor));
            }

            string login = httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "login").Value;
            string tp_login = httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == "tp_login").Value;

            if (!tipos_login.Contains(tp_login))
            {
                throw new Exception("Invalid 'tp_login' provided");
            }

            if (tp_login == TipoLogin.Admin)
            {
                var user = (from a in db.TB_Usuario where a.Login == login select a).FirstOrDefault();

                if (user is null)
                {
                    throw new ArgumentNullException(nameof(user));
                }

                Login = user.Login;
                ID_Usuario = user.ID;
                Nome = user.Nome;
            }
            else
            {
                var funcionario = (from a in db.TB_Funcionario where a.Login == login select a).FirstOrDefault();

                if (funcionario is null)
                {
                    throw new ArgumentNullException(nameof(funcionario));
                }

                Login = funcionario.Login;
                ID_Funcionario = funcionario.ID;
                Nome = funcionario.Nome;
            }

            TP_Login = tp_login;
        }
    }
}
