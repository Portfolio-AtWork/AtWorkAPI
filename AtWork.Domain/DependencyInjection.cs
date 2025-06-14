using AtWork.Domain.Application.Funcionario.Commands;
using AtWork.Domain.Application.Funcionario.Validators;
using AtWork.Domain.Application.Justificativa.Commands;
using AtWork.Domain.Application.Justificativa.Validators;
using AtWork.Domain.Application.Ponto.Commands;
using AtWork.Domain.Application.Ponto.Validators;
using AtWork.Domain.Application.Usuario.Commands;
using AtWork.Domain.Application.Usuario.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace AtWork.Domain
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        public static void AddDomain(this IServiceCollection services)
        {
            services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            services.AddScoped<IValidator<CreateUsuarioCommand>, CreateUsuarioValidator>();
            services.AddScoped<IValidator<CreateFuncionarioCommand>, CreateFuncionarioValidator>();
            services.AddScoped<IValidator<CreatePontoManualCommand>, CreatePontoManualValidator>();
            services.AddScoped<IValidator<CreateJustificativaCommand>, CreateJustificativaValidator>();
        }
    }
}
