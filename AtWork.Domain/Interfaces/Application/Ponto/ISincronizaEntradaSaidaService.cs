using AtWork.Domain.Interfaces.Services;

namespace AtWork.Domain.Interfaces.Application.Ponto
{
    public interface ISincronizaEntradaSaidaService : IBaseService
    {
        Task HandleAsync(DateTime dt_base, Guid ID_Funcionario, CancellationToken ct);
    }
}
