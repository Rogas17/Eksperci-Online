using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EksperciOnline.Migrations
{
    /// <inheritdoc />
    public partial class DodanieTabeliZgłoszeń : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Zgłoszenia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UsługaId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Powód = table.Column<string>(type: "TEXT", nullable: false),
                    Opis = table.Column<string>(type: "TEXT", nullable: false),
                    DataZgłoszenia = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CzyRozpatrzone = table.Column<bool>(type: "INTEGER", nullable: false),
                    CzyZablokowane = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zgłoszenia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zgłoszenia_Usługi_UsługaId",
                        column: x => x.UsługaId,
                        principalTable: "Usługi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Zgłoszenia_UsługaId",
                table: "Zgłoszenia",
                column: "UsługaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Zgłoszenia");
        }
    }
}
