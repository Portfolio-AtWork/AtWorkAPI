using AtWork.Domain.Database.Entities;
using AtWork.Shared.Extensions;
using AtWork.Shared.Structs;
using System.Diagnostics.CodeAnalysis;

namespace AtWork.Domain.Database
{
    [ExcludeFromCodeCoverage]
    public static class DatabaseSeeder
    {
        //private const string ImageB64 = @"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";
        private const string defaultPasswordHash = "MTIz";

        private static TB_Usuario CriaUsuarioObj(string email, string nome, string login)
        {
            return new TB_Usuario()
            {
                Email = email,
                Nome = nome,
                Login = login,
                Senha = defaultPasswordHash,
                ST_Status = StatusRegistro.Ativo,
                DT_Alt = DateTime.Now.ToBrazilianTime(),
                DT_Cad = DateTime.Now.ToBrazilianTime()
            };
        }

        private static TB_Grupo CriaGrupoObj()
        {
            return new()
            {
                Nome = "Desenvolvedores",
                ST_Status = StatusRegistro.Ativo,
                DT_Alt = DateTime.Now.ToBrazilianTime(),
                DT_Cad = DateTime.Now.ToBrazilianTime(),
            };
        }

        private static TB_Grupo_X_Admin CriaVinculoObj(Guid id_usuario, Guid id_grupo)
        {
            return new()
            {
                ID_Grupo = id_grupo,
                ID_Usuario = id_usuario,
                DT_Alt = DateTime.Now.ToBrazilianTime(),
                DT_Cad = DateTime.Now.ToBrazilianTime(),
            };
        }

        private static TB_Funcionario CriaFuncionarioObj(TB_Usuario usuario, TB_Grupo grupo)
        {
            Guid guid = Guid.NewGuid();
            string fakeEmail = guid.ToString().Replace("-", "") + "@fake.com";

            string login = usuario.Login + "_funcionario";

            return new()
            {
                Email = fakeEmail,
                ID_Usuario = usuario.ID,
                Nome = login,
                Login = login,
                Senha = defaultPasswordHash,
                ST_Status = StatusRegistro.Ativo,
                DT_Alt = DateTime.Now.ToBrazilianTime(),
                DT_Cad = DateTime.Now.ToBrazilianTime(),
                ID_Grupo = grupo.ID
            };
        }

        public static void Seed(DatabaseContext context)
        {
            if (context.TB_Usuario.Any())
                return;

            List<TB_Usuario> usuarios = [
                CriaUsuarioObj("gustavoh1709@hotmail.com", "Gustavo Henrique Borges", "gustavo"),
                CriaUsuarioObj("a@a.com", "Usuário 1", "usuario_1"),
                CriaUsuarioObj("b@b.com", "Usuário 2", "usuario_2"),
                CriaUsuarioObj("c@c.com", "Usuário 3", "usuario_3"),
                CriaUsuarioObj("d@d.com", "Usuário 4", "usuario_4"),
                CriaUsuarioObj("e@e.com", "Usuário 5", "usuario_5")
            ];

            foreach (TB_Usuario usuario in usuarios)
            {
                context.TB_Usuario.Add(usuario);

                TB_Grupo grupo = CriaGrupoObj();
                context.TB_Grupo.Add(grupo);

                TB_Grupo_X_Admin grupo_x_admin = CriaVinculoObj(usuario.ID, grupo.ID);
                context.TB_Grupo_X_Admin.Add(grupo_x_admin);

                TB_Funcionario funcionario = CriaFuncionarioObj(usuario, grupo);
                context.TB_Funcionario.Add(funcionario);

                context.SaveChanges();
            }

            // Popule outras tabelas aqui...
        }
    }
}
