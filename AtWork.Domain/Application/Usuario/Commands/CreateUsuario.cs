using AtWork.Domain.Database.Entities;
using AtWork.Domain.Interfaces.Services.Auth;
using AtWork.Domain.Interfaces.Services.Validator;
using AtWork.Domain.Interfaces.UnitOfWork;
using AtWork.Shared.Enums.Models;
using AtWork.Shared.Extensions;
using AtWork.Shared.Models;
using AtWork.Shared.Structs;
using AtWork.Shared.Structs.Messages;
using MediatR;

namespace AtWork.Domain.Application.Usuario.Commands
{
    public record CreateUsuarioCommand(
        string Login,
        string Nome,
        string Senha,
        string ConfirmarSenha,
        string Email
    ) : IRequest<ObjectResponse<bool>>;

    public class CreateUsuarioHandler(
        IUnitOfWork unitOfWork,
        IPasswordHashService passwordHashService,
        IBaseValidator<CreateUsuarioCommand, bool> validator
    ) : IRequestHandler<CreateUsuarioCommand, ObjectResponse<bool>>
    {
        public async Task<ObjectResponse<bool>> Handle(CreateUsuarioCommand command, CancellationToken cancellationToken)
        {
            ObjectResponse<bool> result = new();

            if (!await validator.IsValidAsync(command, result, cancellationToken))
            {
                return result;
            }

            unitOfWork.BeginTransaction();

            string pHash = passwordHashService.Hash(command.Senha);

            TB_Usuario usuario = new()
            {
                Email = command.Email.ToLower(),
                Login = command.Login,
                Nome = command.Nome,
                Senha = pHash,
                ST_Status = StatusRegistro.Ativo
            };

            await unitOfWork.Repository.AddAsync(usuario, cancellationToken);

            bool saved = (await unitOfWork.SaveChangesAsync(cancellationToken)).Ok();

            if (saved)
            {
                result.AddNotification(MessagesStruct.SUCESSO_AO_SALVAR_REGISTRO, NotificationKind.Success);
                result.Value = true;
            }
            else
            {
                result.AddNotification(MessagesStruct.FALHA_AO_SALVAR_REGISTRO, NotificationKind.Warning);
                result.Value = false;
            }

            return result;
        }
    }
}
