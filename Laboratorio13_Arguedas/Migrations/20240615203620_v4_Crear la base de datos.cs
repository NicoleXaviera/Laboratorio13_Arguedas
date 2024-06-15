using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Laboratorio13_Arguedas.Migrations
{
    /// <inheritdoc />
    public partial class v4_Crearlabasededatos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirtsName",
                table: "Students",
                newName: "FirstName");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Students",
                newName: "FirtsName");
        }
    }
}
