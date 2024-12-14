using AtWork.Domain;
using AtWork.Domain.Database;
using AtWork.Domain.Interfaces.Services.Auth;
using AtWork.Domain.Interfaces.Services.Validator;
using AtWork.Domain.Interfaces.UnitOfWork;
using AtWork.Infra.UnitOfWork;
using AtWork.Services.Auth;
using AtWork.Services.Validator;
using AtWorkAPI.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace AtWorkAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString, b => b.MigrationsAssembly("AtWorkAPI")));

            builder.Services.AddDomain();

            builder.Services.AddScoped<IBaseRepository, BaseRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IPasswordHashService, PasswordHashService>();
            builder.Services.AddScoped(typeof(IBaseValidator<,>), typeof(BaseValidator<,>));


            // Adicionar serviços ao contêiner
            builder.Services.AddControllers();

            // Configurar o Swagger/OpenAPI
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AtWork API",
                    Version = "v1",
                    Description = "API para o projeto AtWork"
                });
            });

            var app = builder.Build();

            app.UseMiddleware<AtWorkMiddleware>();

            // Configurar o pipeline de requisição HTTP
            if (app.Environment.IsDevelopment())
            {
                // Habilitar o Swagger no ambiente de desenvolvimento
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AtWork API v1");
                    c.RoutePrefix = string.Empty; // Swagger na raiz (opcional)
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
