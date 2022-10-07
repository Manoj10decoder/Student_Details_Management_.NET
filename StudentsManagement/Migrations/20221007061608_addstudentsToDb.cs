using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentsManagement.Migrations
{
    public partial class addstudentsToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentsAddress",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(nullable: true),
                    Pincode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsAddress", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentsPersonalDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    MotherName = table.Column<string>(nullable: true),
                    FatherName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<long>(nullable: false),
                    StudentsAddressId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsPersonalDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentsPersonalDetails_StudentsAddress_StudentsAddressId",
                        column: x => x.StudentsAddressId,
                        principalTable: "StudentsAddress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    RegisterNumber = table.Column<long>(nullable: false),
                    Standard = table.Column<int>(nullable: false),
                    Section = table.Column<string>(nullable: true),
                    AadhaarNumber = table.Column<long>(nullable: false),
                    StudentsPersonalDetailsId = table.Column<int>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_StudentsPersonalDetails_StudentsPersonalDetailsId",
                        column: x => x.StudentsPersonalDetailsId,
                        principalTable: "StudentsPersonalDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentsPersonalDetailsId",
                table: "Students",
                column: "StudentsPersonalDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsPersonalDetails_StudentsAddressId",
                table: "StudentsPersonalDetails",
                column: "StudentsAddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "StudentsPersonalDetails");

            migrationBuilder.DropTable(
                name: "StudentsAddress");
        }
    }
}
