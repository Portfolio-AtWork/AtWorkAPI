using AtWork.Domain.Database.Entities;
using AtWork.Domain.Interfaces.Services;

namespace AtWork.Domain.Interfaces.Application.Ponto
{
    public interface IDefinePontoEhEntradaOuSaidaService : IBaseService
    {
        string Handle(List<TB_Ponto> pontosJaCadastrados);
        string Handle(int count);
    }
}
