using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace University.Data.Migrations
{
    public partial class SeedEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "DateTime", "Description", "Name", "ProfessorId", "StudentId" },
                values: new object[] { 1, new DateTime(2024, 4, 21, 14, 0, 0, 0, DateTimeKind.Unspecified), "Employers have been invited onto campus to offer internships or even jobs to students.", "Career Fair", null, null });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "DateTime", "Description", "Name", "ProfessorId", "StudentId" },
                values: new object[] { 2, new DateTime(2024, 5, 17, 19, 30, 0, 0, DateTimeKind.Unspecified), "Everyone (even professors) is invited to a fun game night. Games are provided :)", "Game Night", null, null });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "DateTime", "Description", "Name", "ProfessorId", "StudentId" },
                values: new object[] { 3, new DateTime(2024, 5, 12, 13, 30, 0, 0, DateTimeKind.Unspecified), "Our students will present their essays on macroeconomic problems during our annual conference.", "Macroeconomics Student Conferece", null, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "DateTime",
                table: "Events");
        }
    }
}
