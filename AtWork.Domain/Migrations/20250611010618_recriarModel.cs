using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtWork.Domain.Migrations
{
    /// <inheritdoc />
    public partial class recriarModel : Migration
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
                name: "tb_funcionario",
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
                    table.PrimaryKey("PK_tb_funcionario", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_funcionario_tb_grupo_id_grupo",
                        column: x => x.id_grupo,
                        principalTable: "tb_grupo",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_tb_funcionario_tb_usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "tb_usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_grupo_x_admin",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_usuario = table.Column<Guid>(type: "uuid", nullable: false),
                    id_grupo = table.Column<Guid>(type: "uuid", nullable: false),
                    dt_cad = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dt_alt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_grupo_x_admin", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_grupo_x_admin_tb_grupo_id_grupo",
                        column: x => x.id_grupo,
                        principalTable: "tb_grupo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_grupo_x_admin_tb_usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "tb_usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_horario",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_usuario = table.Column<Guid>(type: "uuid", nullable: false),
                    st_status = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    dt_cad = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dt_alt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_horario", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_horario_tb_usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "tb_usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_justificativa",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_funcionario = table.Column<Guid>(type: "uuid", nullable: false),
                    justificativa = table.Column<string>(type: "text", nullable: false),
                    imagem_justificativa = table.Column<byte[]>(type: "bytea", nullable: true),
                    imagem_content_type = table.Column<string>(type: "text", nullable: true),
                    st_status = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    dt_justificativa = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dt_cad = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dt_alt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_justificativa", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_justificativa_tb_funcionario_id_funcionario",
                        column: x => x.id_funcionario,
                        principalTable: "tb_funcionario",
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
                    tp_ponto = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    dt_cad = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dt_alt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_ponto", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_ponto_tb_funcionario_id_funcionario",
                        column: x => x.id_funcionario,
                        principalTable: "tb_funcionario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_horario_x_dia",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_horario = table.Column<Guid>(type: "uuid", nullable: false),
                    dia_da_semana = table.Column<string>(type: "text", nullable: false),
                    hora_inicio = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    hora_final = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    st_status = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    dt_cad = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    dt_alt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_horario_x_dia", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_horario_x_dia_tb_horario_id_horario",
                        column: x => x.id_horario,
                        principalTable: "tb_horario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_funcionario_id_grupo",
                table: "tb_funcionario",
                column: "id_grupo");

            migrationBuilder.CreateIndex(
                name: "IX_tb_funcionario_id_usuario",
                table: "tb_funcionario",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_tb_grupo_x_admin_id_grupo",
                table: "tb_grupo_x_admin",
                column: "id_grupo");

            migrationBuilder.CreateIndex(
                name: "IX_tb_grupo_x_admin_id_usuario",
                table: "tb_grupo_x_admin",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_tb_horario_id_usuario",
                table: "tb_horario",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_tb_horario_x_dia_id_horario",
                table: "tb_horario_x_dia",
                column: "id_horario");

            migrationBuilder.CreateIndex(
                name: "IX_tb_justificativa_id_funcionario",
                table: "tb_justificativa",
                column: "id_funcionario");

            migrationBuilder.CreateIndex(
                name: "IX_tb_ponto_id_funcionario",
                table: "tb_ponto",
                column: "id_funcionario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_grupo_x_admin");

            migrationBuilder.DropTable(
                name: "tb_horario_x_dia");

            migrationBuilder.DropTable(
                name: "tb_justificativa");

            migrationBuilder.DropTable(
                name: "tb_ponto");

            migrationBuilder.DropTable(
                name: "tb_horario");

            migrationBuilder.DropTable(
                name: "tb_funcionario");

            migrationBuilder.DropTable(
                name: "tb_grupo");

            migrationBuilder.DropTable(
                name: "tb_usuario");
        }
    }
}
