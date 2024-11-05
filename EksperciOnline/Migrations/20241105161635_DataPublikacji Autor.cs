using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EksperciOnline.Migrations
{
    /// <inheritdoc />
    public partial class DataPublikacjiAutor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Autor",
                table: "Usługi",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataPulikacji",
                table: "Usługi",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Autor",
                table: "Usługi");

            migrationBuilder.DropColumn(
                name: "DataPulikacji",
                table: "Usługi");
        }
    }
}
