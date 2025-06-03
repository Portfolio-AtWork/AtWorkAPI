using AtWork.Domain.Database.Entities;
using AtWork.Domain.Interfaces.Services.Auth;
using AtWork.Domain.Interfaces.UnitOfWork;
using AtWork.Shared.Enums.Models;
using AtWork.Shared.Models;
using AtWork.Shared.Structs;
using AtWork.Shared.Structs.Messages;
using MediatR;

namespace AtWork.Domain.Application.Login.Request
{
    public record AuthRequest(string Login, string Senha, string TP_Login) : IRequest<ObjectResponse<AuthResult?>>;
    public record AuthResult(string Nome, string Login, string TP_Login, string Email);

    public class AuthHandler(
        IUnitOfWork unitOfWork,
        IPasswordHashService passwordHashService
    ) : IRequestHandler<AuthRequest, ObjectResponse<AuthResult?>>
    {
        public async Task<ObjectResponse<AuthResult?>> Handle(AuthRequest request, CancellationToken cancellationToken)
        {
            ObjectResponse<AuthResult?> result = new();

            if (request.TP_Login == TipoLogin.Admin)
            {
                TB_Usuario? usuario = await unitOfWork.Repository.GetAsync<TB_Usuario>(item => item.Login == request.Login, cancellationToken);

                if (usuario is null || usuario.ST_Status == StatusRegistro.Cancelado)
                {
                    result.Value = null;
                    result.AddNotification(MessagesStruct.USUARIO_OU_SENHA_INCORRETOS, NotificationKind.Warning);
                    return result;
                }

                bool validaSenha = passwordHashService.VerifyPassword(request.Senha, usuario.Senha);
                if (!validaSenha)
                {
                    result.Value = null;
                    result.AddNotification(MessagesStruct.USUARIO_OU_SENHA_INCORRETOS, NotificationKind.Warning);
                    return result;
                }

                result.Value = new AuthResult(usuario.Nome, usuario.Login, TipoLogin.Admin, usuario.Email);
                return result;
            }
            else if (request.TP_Login == TipoLogin.Funcionario)
            {
                TB_Funcionario? funcionario = await unitOfWork.Repository.GetAsync<TB_Funcionario>(item => item.Login == request.Login, cancellationToken);

                if (funcionario is null || funcionario.ST_Status == StatusRegistro.Cancelado)
                {
                    result.Value = null;
                    result.AddNotification(MessagesStruct.USUARIO_OU_SENHA_INCORRETOS, NotificationKind.Warning);
                    return result;
                }

                bool validaSenha = passwordHashService.VerifyPassword(request.Senha, funcionario.Senha);
                if (!validaSenha)
                {
                    result.Value = null;
                    result.AddNotification(MessagesStruct.USUARIO_OU_SENHA_INCORRETOS, NotificationKind.Warning);
                    return result;
                }

                result.Value = new AuthResult(funcionario.Nome, funcionario.Login, TipoLogin.Funcionario, funcionario.Email);
                return result;
            }
            else
            {
                result.Value = null;
                result.AddNotification(MessagesStruct.TIPO_LOGIN_INFORMADO_NAO_EH_VALIDO, NotificationKind.Warning);
            }

            return result;
        }
    }
}
