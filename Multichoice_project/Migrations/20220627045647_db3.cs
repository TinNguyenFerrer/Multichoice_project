using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Multichoice_project.Migrations
{
    public partial class db3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfTest",
                table: "Results",
                newName: "NumberQuestionOfTest");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberQuestionOfTest",
                table: "Results",
                newName: "NumberOfTest");
        }
    }
}
