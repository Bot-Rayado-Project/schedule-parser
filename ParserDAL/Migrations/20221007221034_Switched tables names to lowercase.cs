using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParserDAL.Migrations
{
    public partial class Switchedtablesnamestolowercase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "SharedSchedules",
                newName: "shared_schedules");
            migrationBuilder.RenameTable(
                name: "PersonalSchedules",
                newName: "personal_schedules");
            migrationBuilder.RenameTable(
                name: "PersonalChanges",
                newName: "personal_changes");
            migrationBuilder.RenameTable(
                name: "PersonalAnnotations",
                newName: "personal_annotations");
            migrationBuilder.RenameTable(
                name: "HeadmanSchedules",
                newName: "headman_schedules");
            migrationBuilder.RenameTable(
                name: "HeadmanChanges",
                newName: "headman_changes");
            migrationBuilder.RenameTable(
                name: "HeadmanAnnotations",
                newName: "headman_annotations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "shared_schedules",
                newName: "SharedSchedules");
            migrationBuilder.RenameTable(
                name: "personal_schedules",
                newName: "PersonalSchedules");
            migrationBuilder.RenameTable(
                name: "personal_changes",
                newName: "PersonalChanges");
            migrationBuilder.RenameTable(
                name: "personal_annotations",
                newName: "PersonalAnnotations");
            migrationBuilder.RenameTable(
                name: "headman_schedules",
                newName: "HeadmanSchedules");
            migrationBuilder.RenameTable(
                name: "headman_changes",
                newName: "HeadmanChanges");
            migrationBuilder.RenameTable(
                name: "headman_annotations",
                newName: "HeadmanAnnotations");
        }
    }
}
