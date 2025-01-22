using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtWorkAPI.Migrations
{
    /// <inheritdoc />
    public partial class GrupoXAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_tb_grupo_x_admin_id_grupo",
                table: "tb_grupo_x_admin",
                column: "id_grupo");

            migrationBuilder.CreateIndex(
                name: "IX_tb_grupo_x_admin_id_usuario",
                table: "tb_grupo_x_admin",
                column: "id_usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_grupo_x_admin");
        }
    }
}
