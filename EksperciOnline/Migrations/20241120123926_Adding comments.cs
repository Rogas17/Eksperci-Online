using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EksperciOnline.Migrations
{
    /// <inheritdoc />
    public partial class Addingcomments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceComment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Grade = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ServiceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UsługaId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceComment_Usługi_UsługaId",
                        column: x => x.UsługaId,
                        principalTable: "Usługi",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceComment_UsługaId",
                table: "ServiceComment",
                column: "UsługaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceComment");
        }
    }
}
