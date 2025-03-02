using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtWorkAPI.Migrations
{
    /// <inheritdoc />
    public partial class fixTabelaTbFuncionario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_Funcionario_tb_grupo_id_grupo",
                table: "TB_Funcionario");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_Funcionario_tb_usuario_id_usuario",
                table: "TB_Funcionario");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_ponto_TB_Funcionario_id_funcionario",
                table: "tb_ponto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_Funcionario",
                table: "TB_Funcionario");

            migrationBuilder.RenameTable(
                name: "TB_Funcionario",
                newName: "tb_funcionario");

            migrationBuilder.RenameIndex(
                name: "IX_TB_Funcionario_id_usuario",
                table: "tb_funcionario",
                newName: "IX_tb_funcionario_id_usuario");

            migrationBuilder.RenameIndex(
                name: "IX_TB_Funcionario_id_grupo",
                table: "tb_funcionario",
                newName: "IX_tb_funcionario_id_grupo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_funcionario",
                table: "tb_funcionario",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_funcionario_tb_grupo_id_grupo",
                table: "tb_funcionario",
                column: "id_grupo",
                principalTable: "tb_grupo",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_funcionario_tb_usuario_id_usuario",
                table: "tb_funcionario",
                column: "id_usuario",
                principalTable: "tb_usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_ponto_tb_funcionario_id_funcionario",
                table: "tb_ponto",
                column: "id_funcionario",
                principalTable: "tb_funcionario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_funcionario_tb_grupo_id_grupo",
                table: "tb_funcionario");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_funcionario_tb_usuario_id_usuario",
                table: "tb_funcionario");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_ponto_tb_funcionario_id_funcionario",
                table: "tb_ponto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_funcionario",
                table: "tb_funcionario");

            migrationBuilder.RenameTable(
                name: "tb_funcionario",
                newName: "TB_Funcionario");

            migrationBuilder.RenameIndex(
                name: "IX_tb_funcionario_id_usuario",
                table: "TB_Funcionario",
                newName: "IX_TB_Funcionario_id_usuario");

            migrationBuilder.RenameIndex(
                name: "IX_tb_funcionario_id_grupo",
                table: "TB_Funcionario",
                newName: "IX_TB_Funcionario_id_grupo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_Funcionario",
                table: "TB_Funcionario",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Funcionario_tb_grupo_id_grupo",
                table: "TB_Funcionario",
                column: "id_grupo",
                principalTable: "tb_grupo",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Funcionario_tb_usuario_id_usuario",
                table: "TB_Funcionario",
                column: "id_usuario",
                principalTable: "tb_usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_ponto_TB_Funcionario_id_funcionario",
                table: "tb_ponto",
                column: "id_funcionario",
                principalTable: "TB_Funcionario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
