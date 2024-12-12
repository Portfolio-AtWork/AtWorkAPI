using AtWork.Domain.Database.Entities;
using AtWork.Domain.Interfaces.UnitOfWork;
using AtWork.Shared.Enums.Models;
using AtWork.Shared.Extensions;
using AtWork.Shared.Models;
using MediatR;

namespace AtWork.Domain.Application.Ponto.Commands
{
    public record DeletePontoCommand(Guid ID) : IRequest<ObjectResponse<bool>>;

    public class DeletePontoHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeletePontoCommand, ObjectResponse<bool>>
    {
        public async Task<ObjectResponse<bool>> Handle(DeletePontoCommand command, CancellationToken cancellationToken)
        {
            ObjectResponse<bool> result = new();
            unitOfWork.BeginTransaction();

            TB_Ponto? ponto = await unitOfWork.Repository.GetAsync<TB_Ponto>(item => item.ID == command.ID, cancellationToken);

            if (ponto is null)
            {
                result.AddNotification("Ponto informado não existe", NotificationKind.Warning);
                return result;
            }

            await unitOfWork.Repository.DeleteAsync(ponto, cancellationToken);

            result.Value = unitOfWork.SaveChangesAsync().Ok();
            return result;
        }
    }
}
