using AtWork.Domain.Database.Entities;
using AtWork.Services.Rules.Ponto;
using AtWork.Shared.Structs;

namespace AtWork.Tests
{
    public class DefinePontoEhEntradaOuSaidaServiceTests
    {
        private readonly DefinePontoEhEntradaOuSaidaService _service;

        public DefinePontoEhEntradaOuSaidaServiceTests()
        {
            _service = new DefinePontoEhEntradaOuSaidaService();
        }

        [Theory]
        [InlineData(0, TipoPonto.Entrada)]
        [InlineData(1, TipoPonto.Saida)]
        [InlineData(2, TipoPonto.Entrada)]
        [InlineData(3, TipoPonto.Saida)]
        [InlineData(4, TipoPonto.Entrada)]
        public void Handle_CountBased_ReturnsExpectedTipoPonto(int count, string expected)
        {
            // Act
            var result = _service.Handle(count);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Handle_ListBased_ReturnsExpectedTipoPonto()
        {
            // Arrange
            List<TB_Ponto> pontos =
            [
                new TB_Ponto(), // 1
                new TB_Ponto(), // 2
                new TB_Ponto()  // 3
            ];

            // Act
            var result = _service.Handle(pontos);

            // Assert
            Assert.Equal(TipoPonto.Saida, result); // count + 1 = 4 => par => Saída
        }

        [Fact]
        public void Handle_EmptyList_ReturnsEntrada()
        {
            var pontos = new List<TB_Ponto>();

            var result = _service.Handle(pontos);

            Assert.Equal(TipoPonto.Entrada, result);
        }
    }
}
