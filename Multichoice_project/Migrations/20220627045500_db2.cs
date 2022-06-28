using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Multichoice_project.Migrations
{
    public partial class db2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NumberOfCorrectAnswers",
                table: "Results",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfTest",
                table: "Results",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfTest",
                table: "Results");

            migrationBuilder.AlterColumn<string>(
                name: "NumberOfCorrectAnswers",
                table: "Results",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
