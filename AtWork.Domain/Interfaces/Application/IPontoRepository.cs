using AtWork.Domain.Database.Entities;

namespace AtWork.Domain.Interfaces.Application
{
    public interface IPontoRepository : IRepository<TB_Ponto>
    {
        Task<List<TB_Ponto>> GetByFuncionario(Guid ID_Funcionario, CancellationToken ct);
    }
}
