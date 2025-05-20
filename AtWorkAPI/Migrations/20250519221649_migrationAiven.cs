using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtWorkAPI.Migrations
{
    /// <inheritdoc />
    public partial class migrationAiven : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_Horario",
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
                    table.PrimaryKey("PK_TB_Horario", x => x.id);
                    table.ForeignKey(
                        name: "FK_TB_Horario_tb_usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "tb_usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_Horario_X_Dia",
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
                    table.PrimaryKey("PK_TB_Horario_X_Dia", x => x.id);
                    table.ForeignKey(
                        name: "FK_TB_Horario_X_Dia_TB_Horario_id_horario",
                        column: x => x.id_horario,
                        principalTable: "TB_Horario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_Horario_id_usuario",
                table: "TB_Horario",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_TB_Horario_X_Dia_id_horario",
                table: "TB_Horario_X_Dia",
                column: "id_horario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_Horario_X_Dia");

            migrationBuilder.DropTable(
                name: "TB_Horario");
        }
    }
}
