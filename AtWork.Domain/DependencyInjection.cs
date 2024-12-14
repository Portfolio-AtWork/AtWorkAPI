using AtWork.Domain.Application.Usuario.Commands;
using AtWork.Domain.Application.Usuario.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AtWork.Domain
{
    public static class DependencyInjection
    {
        public static void AddDomain(this IServiceCollection services)
        {
            services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            services.AddScoped<IValidator<CreateUsuarioCommand>, CreateUsuarioValidator>();
        }
    }
}
