using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EksperciOnline.Migrations
{
    /// <inheritdoc />
    public partial class PierwszaMigracja : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategorie",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    NazwaKategorii = table.Column<string>(type: "TEXT", nullable: false),
                    UrlZdjęcia = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategorie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usługi",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Tytuł = table.Column<string>(type: "TEXT", nullable: false),
                    Lokalizacja = table.Column<string>(type: "TEXT", nullable: false),
                    NrTelefonu = table.Column<int>(type: "INTEGER", nullable: true),
                    Cena = table.Column<double>(type: "REAL", nullable: false),
                    Opis = table.Column<string>(type: "TEXT", nullable: false),
                    KrótkiOpis = table.Column<string>(type: "TEXT", nullable: false),
                    Widoczność = table.Column<bool>(type: "INTEGER", nullable: false),
                    UrlZdjęcia = table.Column<string>(type: "TEXT", nullable: true),
                    UrlBaneru = table.Column<string>(type: "TEXT", nullable: true),
                    KategoriaId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usługi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usługi_Kategorie_KategoriaId",
                        column: x => x.KategoriaId,
                        principalTable: "Kategorie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usługi_KategoriaId",
                table: "Usługi",
                column: "KategoriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usługi");

            migrationBuilder.DropTable(
                name: "Kategorie");
        }
    }
}
