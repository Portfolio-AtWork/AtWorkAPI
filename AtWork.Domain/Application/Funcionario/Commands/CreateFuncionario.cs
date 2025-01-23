using AtWork.Domain.Base;
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

namespace AtWork.Domain.Application.Funcionario.Commands
{
    public record CreateFuncionarioCommand(string Nome, string Login, string Senha, string ConfirmarSenha, Guid ID_Grupo, string Email) : IRequest<ObjectResponse<bool>>;

    public class CreateFuncionarioHandler(IUnitOfWork unitOfWork, UserInfo userInfo, IPasswordHashService passwordHashService, IBaseValidator<CreateFuncionarioCommand, bool> validator) : IRequestHandler<CreateFuncionarioCommand, ObjectResponse<bool>>
    {
        public async Task<ObjectResponse<bool>> Handle(CreateFuncionarioCommand command, CancellationToken cancellationToken)
        {
            ObjectResponse<bool> result = new();

            if (!await validator.IsValidAsync(command, result, cancellationToken))
            {
                return result;
            }

            string pHash = passwordHashService.Hash(command.Senha);

            TB_Funcionario funcionario = new()
            {
                Email = command.Email,
                ID_Grupo = command.ID_Grupo,
                ID_Usuario = userInfo.ID_Usuario,
                Nome = command.Nome,
                ST_Status = StatusRegistro.Ativo,
                Senha = pHash,
                Login = command.Login,
            };

            await unitOfWork.Repository.AddAsync(funcionario, cancellationToken);

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
