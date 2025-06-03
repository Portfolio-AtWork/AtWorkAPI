using AtWork.Domain.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimeZoneConverter;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<TB_Usuario> TB_Usuario { get; set; }
    public DbSet<TB_Funcionario> TB_Funcionario { get; set; }
    public DbSet<TB_Grupo> TB_Grupo { get; set; }
    public DbSet<TB_Ponto> TB_Ponto { get; set; }
    public DbSet<TB_Grupo_X_Admin> TB_Grupo_X_Admin { get; set; }
    public DbSet<TB_Horario> TB_Horario { get; set; }
    public DbSet<TB_Horario_X_Dia> TB_Horario_X_Dia { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var saoPauloTimeZone = TZConvert.GetTimeZoneInfo("America/Sao_Paulo");

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                // Para DateTime (não-nullable)
                if (property.ClrType == typeof(DateTime))
                {
                    var converter = new ValueConverter<DateTime, DateTime>(
                        v => TimeZoneInfo.ConvertTimeToUtc(v, saoPauloTimeZone),
                        v => TimeZoneInfo.ConvertTimeFromUtc(v, saoPauloTimeZone)
                    );

                    property.SetValueConverter(converter);
                    property.SetValueComparer(new ValueComparer<DateTime>(
                        (d1, d2) => d1 == d2,
                        d => d.GetHashCode(),
                        d => d
                    ));
                }

                // Para DateTime? (nullable)
                else if (property.ClrType == typeof(DateTime?))
                {
                    var converter = new ValueConverter<DateTime?, DateTime?>(
                        v => v.HasValue ? TimeZoneInfo.ConvertTimeToUtc(v.Value, saoPauloTimeZone) : null,
                        v => v.HasValue ? TimeZoneInfo.ConvertTimeFromUtc(v.Value, saoPauloTimeZone) : null
                    );

                    property.SetValueConverter(converter);
                    property.SetValueComparer(new ValueComparer<DateTime?>(
                        (d1, d2) => d1 == d2,
                        d => d.HasValue ? d.Value.GetHashCode() : 0,
                        d => d
                    ));
                }
            }
        }

        base.OnModelCreating(modelBuilder);
    }
}
