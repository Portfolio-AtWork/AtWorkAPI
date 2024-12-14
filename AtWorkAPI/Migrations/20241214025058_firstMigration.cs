using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtWorkAPI.Migrations
{
    /// <inheritdoc />
    public partial class firstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_grupo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    st_status = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    dt_cad = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dt_alt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_grupo", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_usuario",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    login = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    senha = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    st_status = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    dt_cad = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dt_alt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TB_Funcionario",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    login = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    senha = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    st_status = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    id_usuario = table.Column<Guid>(type: "uuid", nullable: false),
                    id_grupo = table.Column<Guid>(type: "uuid", nullable: true),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    dt_cad = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dt_alt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_Funcionario", x => x.id);
                    table.ForeignKey(
                        name: "FK_TB_Funcionario_tb_grupo_id_grupo",
                        column: x => x.id_grupo,
                        principalTable: "tb_grupo",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_TB_Funcionario_tb_usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "tb_usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_ponto",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_funcionario = table.Column<Guid>(type: "uuid", nullable: false),
                    dt_ponto = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    st_ponto = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    dt_cad = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dt_alt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_ponto", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_ponto_TB_Funcionario_id_funcionario",
                        column: x => x.id_funcionario,
                        principalTable: "TB_Funcionario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_Funcionario_id_grupo",
                table: "TB_Funcionario",
                column: "id_grupo");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Funcionario_id_usuario",
                table: "TB_Funcionario",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_tb_ponto_id_funcionario",
                table: "tb_ponto",
                column: "id_funcionario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_ponto");

            migrationBuilder.DropTable(
                name: "TB_Funcionario");

            migrationBuilder.DropTable(
                name: "tb_grupo");

            migrationBuilder.DropTable(
                name: "tb_usuario");
        }
    }
}
