using AtWork.Domain.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace AtWork.Domain.Database
{
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
            base.OnModelCreating(modelBuilder);
        }
    }
}
