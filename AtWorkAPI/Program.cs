using AtWork.Domain;
using AtWork.Domain.Base;
using AtWork.Domain.Database;
using AtWork.Domain.Interfaces.Services.Auth;
using AtWork.Domain.Interfaces.Services.Validator;
using AtWork.Domain.Interfaces.UnitOfWork;
using AtWork.Infra.UnitOfWork;
using AtWork.Services;
using AtWork.Services.Auth;
using AtWork.Services.Validator;
using AtWorkAPI.Converters;
using AtWorkAPI.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Globalization;
using System.Text;
using TimeZoneConverter;

namespace AtWorkAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Cultura padr�o
            CultureInfo cultureInfo = new("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            // Fuso padr�o da aplica��o (voc� pode guardar em algum lugar est�tico)
            TimeZoneInfo defaultTimeZone = TZConvert.GetTimeZoneInfo("America/Sao_Paulo");

            var builder = WebApplication.CreateBuilder(args);

            string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(connectionString, b => b.MigrationsAssembly("AtWork.Domain")));

            builder.Services.AddDomain();
            builder.Services.AddServices();

            builder.Services.AddScoped<IBaseRepository, BaseRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IPasswordHashService, PasswordHashService>();
            builder.Services.AddScoped(typeof(IBaseValidator<,>), typeof(BaseValidator<,>));

            // Adicionar servi�os ao cont�iner
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null; // Desativa a convers�o camelCase
                options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
                options.JsonSerializerOptions.Converters.Add(new NullableDateTimeConverter());
            });

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<UserInfo>();

            // Configurar o Swagger/OpenAPI
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AtWork API",
                    Version = "v1",
                    Description = "API para o projeto AtWork"
                });

                // Configura��o do esquema de autentica��o Bearer
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Insira o token JWT no formato: Bearer {seu token}"
                });

                // Configura��o para aplicar o Bearer automaticamente em todas as requisi��es
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            IConfigurationSection? jwtSettings = builder.Configuration.GetSection("Jwt");

            if (jwtSettings is null)
            {
                throw new Exception("'jwtSettings' can not be null, check out your appsettings.json");
            }

            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                db.Database.Migrate(); // Aplica as migrations pendentes

                DatabaseSeeder.Seed(db); // Popula dados iniciais
            }

            app.UseMiddleware<AtWorkMiddleware>();

            // Configurar o pipeline de requisi��o HTTP
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
            app.UseAuthentication(); // Certifique-se de adicionar este middleware antes do Authorization
            app.UseAuthorization();

            app.UseCors("AllowAll");

            app.MapControllers();

            app.Run();
        }
    }
}
