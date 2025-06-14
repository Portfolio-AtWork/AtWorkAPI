using AtWork.Domain.Database.Entities;
using AtWork.Domain.Interfaces.Application.Ponto;
using AtWork.Shared.Structs;

namespace AtWork.Services.Rules.Ponto
{
    public class DefinePontoEhEntradaOuSaidaService : IDefinePontoEhEntradaOuSaidaService
    {
        public string Handle(List<TB_Ponto> pontos)
        {
            return Handle(pontos.Count);
        }

        public string Handle(int count)
        {
            return ((count + 1) % 2) == 0 ? TipoPonto.Saida : TipoPonto.Entrada;
        }
    }
}
