using AtWork.Domain.Database.Entities;
using AtWork.Domain.Interfaces.Services.Auth;
using AtWork.Domain.Interfaces.UnitOfWork;
using AtWork.Shared.Extensions;
using AtWork.Shared.Models;
using AtWork.Shared.Structs;
using AtWork.Shared.Structs.Messages;
using FluentValidation;
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
        IValidator<CreateUsuarioCommand> validator
    ) : IRequestHandler<CreateUsuarioCommand, ObjectResponse<bool>>
    {
        public async Task<ObjectResponse<bool>> Handle(CreateUsuarioCommand command, CancellationToken cancellationToken)
        {
            ObjectResponse<bool> result = new();

            if (!await IsValid(command, result, cancellationToken))
            {
                result.Value = false;
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
                result.AddNotification(MessagesStruct.SUCESSO_AO_SALVAR_REGISTRO, Shared.Enums.Models.NotificationKind.Success);
                result.Value = true;
            }
            else
            {
                result.AddNotification(MessagesStruct.FALHA_AO_SALVAR_REGISTRO, Shared.Enums.Models.NotificationKind.Success);
                result.Value = false;
            }

            return result;
        }

        private async Task<bool> IsValid(CreateUsuarioCommand command, ObjectResponse<bool> result, CancellationToken cancellationToken)
        {
            var validation = await validator.ValidateAsync(command, cancellationToken);

            if (!validation.IsValid)
            {
                result.AddNotifications(validation.Errors);
                return false;
            }

            return true;
        }
    }
}
