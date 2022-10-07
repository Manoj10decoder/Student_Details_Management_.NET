using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsManagement.Migrations
{
    public partial class addStudentsPersonalDetailsIdinPersonaldetailsAndAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "StudentsPersonalDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "StudentsAddress",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudentPersonalDetailId",
                table: "StudentsAddress",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "StudentsPersonalDetails");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "StudentsAddress");

            migrationBuilder.DropColumn(
                name: "StudentPersonalDetailId",
                table: "StudentsAddress");
        }
    }
}
