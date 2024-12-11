using AtWork.Domain.Database.Entities;
using AtWork.Domain.Interfaces;
using AtWork.Domain.Interfaces.Application;
using AtWork.Shared.Structs;
using MediatR;

namespace AtWork.Domain.Application.Ponto.Commands
{
    public record CreatePontoCommand(Guid ID_Funcionario) : IRequest<bool>;

    public class CreatePontoHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreatePontoCommand, bool>
    {
        public async Task<bool> Handle(CreatePontoCommand command, CancellationToken cancellationToken)
        {
            unitOfWork.BeginTransaction();

            TB_Ponto ponto = new()
            {
                DT_Ponto = DateTime.Now,
                ST_Ponto = StatusPonto.Aprovado,
                ID_Funcionario = command.ID_Funcionario
            };

            await unitOfWork.Repository<IPontoRepository>().Add(ponto, cancellationToken);

            Exception? error = unitOfWork.SaveChangesAsync();

            return error is null;
        }
    }
}
