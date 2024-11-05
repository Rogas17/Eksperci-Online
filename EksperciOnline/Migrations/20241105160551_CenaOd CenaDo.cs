using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EksperciOnline.Migrations
{
    /// <inheritdoc />
    public partial class CenaOdCenaDo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cena",
                table: "Usługi",
                newName: "CenaOd");

            migrationBuilder.AddColumn<double>(
                name: "CenaDo",
                table: "Usługi",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CenaDo",
                table: "Usługi");

            migrationBuilder.RenameColumn(
                name: "CenaOd",
                table: "Usługi",
                newName: "Cena");
        }
    }
}
