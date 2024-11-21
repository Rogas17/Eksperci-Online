using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EksperciOnline.Migrations
{
    /// <inheritdoc />
    public partial class AddingLikeFunctionality : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceLike",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ServiceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UsługaId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceLike", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceLike_Usługi_UsługaId",
                        column: x => x.UsługaId,
                        principalTable: "Usługi",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceLike_UsługaId",
                table: "ServiceLike",
                column: "UsługaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceLike");
        }
    }
}
