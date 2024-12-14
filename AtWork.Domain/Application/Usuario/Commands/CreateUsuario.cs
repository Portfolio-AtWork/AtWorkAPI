using AtWork.Domain.Database.Entities;
using AtWork.Domain.Interfaces.Services.Auth;
using AtWork.Domain.Interfaces.UnitOfWork;
using AtWork.Shared.Extensions;
using AtWork.Shared.Models;
using AtWork.Shared.Structs;
using MediatR;

namespace AtWork.Domain.Application.Usuario.Commands
{
    public record CreateUsuarioCommand(
        string Login,
        string Nome,
        string Senha,
        string Email
    ) : IRequest<ObjectResponse<bool>>;

    public class CreateUsuarioHandler(IUnitOfWork unitOfWork, IPasswordHashService passwordHashService) : IRequestHandler<CreateUsuarioCommand, ObjectResponse<bool>>
    {
        public async Task<ObjectResponse<bool>> Handle(CreateUsuarioCommand request, CancellationToken cancellationToken)
        {
            ObjectResponse<bool> result = new();

            unitOfWork.BeginTransaction();

            string pHash = passwordHashService.Hash(request.Senha);

            TB_Usuario usuario = new()
            {
                Email = request.Email,
                Login = request.Login,
                Nome = request.Nome,
                Senha = pHash,
                ST_Status = StatusRegistro.Ativo
            };

            await unitOfWork.Repository.AddAsync(usuario, cancellationToken);

            bool saved = (await unitOfWork.SaveChangesAsync(cancellationToken)).Ok();
            result.Value = saved;

            return result;
        }
    }
}
