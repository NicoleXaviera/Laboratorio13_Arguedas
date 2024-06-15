using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Laboratorio13_Arguedas.Migrations
{
    /// <inheritdoc />
    public partial class v2_Crearlabasededatos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Gades_GradeID",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gades",
                table: "Gades");

            migrationBuilder.RenameTable(
                name: "Gades",
                newName: "Grades");

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Courses",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Grades",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Grades",
                table: "Grades",
                column: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Grades_GradeID",
                table: "Students",
                column: "GradeID",
                principalTable: "Grades",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Grades_GradeID",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Grades",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Grades");

            migrationBuilder.RenameTable(
                name: "Grades",
                newName: "Gades");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gades",
                table: "Gades",
                column: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Gades_GradeID",
                table: "Students",
                column: "GradeID",
                principalTable: "Gades",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
