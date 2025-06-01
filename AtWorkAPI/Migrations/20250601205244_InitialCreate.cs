using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtWorkAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_Horario_tb_usuario_id_usuario",
                table: "TB_Horario");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_Horario_X_Dia_TB_Horario_id_horario",
                table: "TB_Horario_X_Dia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_Horario_X_Dia",
                table: "TB_Horario_X_Dia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_Horario",
                table: "TB_Horario");

            migrationBuilder.RenameTable(
                name: "TB_Horario_X_Dia",
                newName: "tb_horario_x_dia");

            migrationBuilder.RenameTable(
                name: "TB_Horario",
                newName: "tb_horario");

            migrationBuilder.RenameIndex(
                name: "IX_TB_Horario_X_Dia_id_horario",
                table: "tb_horario_x_dia",
                newName: "IX_tb_horario_x_dia_id_horario");

            migrationBuilder.RenameIndex(
                name: "IX_TB_Horario_id_usuario",
                table: "tb_horario",
                newName: "IX_tb_horario_id_usuario");

            migrationBuilder.AddColumn<string>(
                name: "tp_ponto",
                table: "tb_ponto",
                type: "character varying(1)",
                maxLength: 1,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_horario_x_dia",
                table: "tb_horario_x_dia",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_horario",
                table: "tb_horario",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_horario_tb_usuario_id_usuario",
                table: "tb_horario",
                column: "id_usuario",
                principalTable: "tb_usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_horario_x_dia_tb_horario_id_horario",
                table: "tb_horario_x_dia",
                column: "id_horario",
                principalTable: "tb_horario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_horario_tb_usuario_id_usuario",
                table: "tb_horario");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_horario_x_dia_tb_horario_id_horario",
                table: "tb_horario_x_dia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_horario_x_dia",
                table: "tb_horario_x_dia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_horario",
                table: "tb_horario");

            migrationBuilder.DropColumn(
                name: "tp_ponto",
                table: "tb_ponto");

            migrationBuilder.RenameTable(
                name: "tb_horario_x_dia",
                newName: "TB_Horario_X_Dia");

            migrationBuilder.RenameTable(
                name: "tb_horario",
                newName: "TB_Horario");

            migrationBuilder.RenameIndex(
                name: "IX_tb_horario_x_dia_id_horario",
                table: "TB_Horario_X_Dia",
                newName: "IX_TB_Horario_X_Dia_id_horario");

            migrationBuilder.RenameIndex(
                name: "IX_tb_horario_id_usuario",
                table: "TB_Horario",
                newName: "IX_TB_Horario_id_usuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_Horario_X_Dia",
                table: "TB_Horario_X_Dia",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_Horario",
                table: "TB_Horario",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Horario_tb_usuario_id_usuario",
                table: "TB_Horario",
                column: "id_usuario",
                principalTable: "tb_usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_Horario_X_Dia_TB_Horario_id_horario",
                table: "TB_Horario_X_Dia",
                column: "id_horario",
                principalTable: "TB_Horario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
