using AtWork.Domain.Interfaces.Application.Ponto;
using AtWork.Services.Rules.Ponto;
using Microsoft.Extensions.DependencyInjection;

namespace AtWork.Services
{
    public static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ISincronizaEntradaSaidaService, SincronizaEntradaSaidaService>();
        }
    }
}
