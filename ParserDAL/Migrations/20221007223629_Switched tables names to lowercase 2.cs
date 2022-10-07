using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParserDAL.Migrations
{
    public partial class Switchedtablesnamestolowercase2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SharedSchedules",
                table: "SharedSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonalSchedules",
                table: "PersonalSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonalChanges",
                table: "PersonalChanges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonalAnnotations",
                table: "PersonalAnnotations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HeadmanSchedules",
                table: "HeadmanSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HeadmanChanges",
                table: "HeadmanChanges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HeadmanAnnotations",
                table: "HeadmanAnnotations");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_shared_schedules",
                table: "shared_schedules",
                columns: new[] { "stream_group", "day", "parity" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_personal_schedules",
                table: "personal_schedules",
                columns: new[] { "id", "stream_group", "day", "parity" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_personal_changes",
                table: "personal_changes",
                columns: new[] { "id", "stream_group", "day", "parity" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_personal_annotations",
                table: "personal_annotations",
                columns: new[] { "id", "stream_group", "day", "parity" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_headman_schedules",
                table: "headman_schedules",
                columns: new[] { "stream_group", "day", "parity" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_headman_changes",
                table: "headman_changes",
                columns: new[] { "stream_group", "day", "parity" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_headman_annotations",
                table: "headman_annotations",
                columns: new[] { "stream_group", "day", "parity" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_shared_schedules",
                table: "shared_schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_personal_schedules",
                table: "personal_schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_personal_changes",
                table: "personal_changes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_personal_annotations",
                table: "personal_annotations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_headman_schedules",
                table: "headman_schedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_headman_changes",
                table: "headman_changes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_headman_annotations",
                table: "headman_annotations");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_SharedSchedules",
                table: "SharedSchedules",
                columns: new[] { "stream_group", "day", "parity" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonalSchedules",
                table: "PersonalSchedules",
                columns: new[] { "id", "stream_group", "day", "parity" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonalChanges",
                table: "PersonalChanges",
                columns: new[] { "id", "stream_group", "day", "parity" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonalAnnotations",
                table: "PersonalAnnotations",
                columns: new[] { "id", "stream_group", "day", "parity" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_HeadmanSchedules",
                table: "HeadmanSchedules",
                columns: new[] { "stream_group", "day", "parity" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_HeadmanChanges",
                table: "HeadmanChanges",
                columns: new[] { "stream_group", "day", "parity" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_HeadmanAnnotations",
                table: "HeadmanAnnotations",
                columns: new[] { "stream_group", "day", "parity" });
        }
    }
}
