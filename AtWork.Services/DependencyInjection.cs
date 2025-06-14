using AtWork.Domain.Interfaces.Application.Ponto;
using AtWork.Services.Rules.Ponto;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace AtWork.Services
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ISincronizaEntradaSaidaService, SincronizaEntradaSaidaService>();
            services.AddScoped<IDefinePontoEhEntradaOuSaidaService, DefinePontoEhEntradaOuSaidaService>();
        }
    }
}
