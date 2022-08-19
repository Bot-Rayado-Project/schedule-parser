using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParserDAL.Migrations
{
    public partial class SwitchedToLowerCase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Schedule",
                table: "SharedSchedules",
                newName: "schedule");

            migrationBuilder.RenameColumn(
                name: "Parity",
                table: "SharedSchedules",
                newName: "parity");

            migrationBuilder.RenameColumn(
                name: "Day",
                table: "SharedSchedules",
                newName: "day");

            migrationBuilder.RenameColumn(
                name: "StreamGroup",
                table: "SharedSchedules",
                newName: "stream_group");

            migrationBuilder.RenameColumn(
                name: "Schedule",
                table: "PersonalSchedules",
                newName: "schedule");

            migrationBuilder.RenameColumn(
                name: "Parity",
                table: "PersonalSchedules",
                newName: "parity");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PersonalSchedules",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Day",
                table: "PersonalSchedules",
                newName: "day");

            migrationBuilder.RenameColumn(
                name: "StreamGroup",
                table: "PersonalSchedules",
                newName: "stream_group");

            migrationBuilder.RenameColumn(
                name: "Parity",
                table: "PersonalChanges",
                newName: "parity");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PersonalChanges",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Day",
                table: "PersonalChanges",
                newName: "day");

            migrationBuilder.RenameColumn(
                name: "Changes",
                table: "PersonalChanges",
                newName: "changes");

            migrationBuilder.RenameColumn(
                name: "StreamGroup",
                table: "PersonalChanges",
                newName: "stream_group");

            migrationBuilder.RenameColumn(
                name: "PairNumber",
                table: "PersonalChanges",
                newName: "pair_number");

            migrationBuilder.RenameColumn(
                name: "Parity",
                table: "PersonalAnnotations",
                newName: "parity");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PersonalAnnotations",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Day",
                table: "PersonalAnnotations",
                newName: "day");

            migrationBuilder.RenameColumn(
                name: "Annotation",
                table: "PersonalAnnotations",
                newName: "annotation");

            migrationBuilder.RenameColumn(
                name: "StreamGroup",
                table: "PersonalAnnotations",
                newName: "stream_group");

            migrationBuilder.RenameColumn(
                name: "Schedule",
                table: "HeadmanSchedules",
                newName: "schedule");

            migrationBuilder.RenameColumn(
                name: "Parity",
                table: "HeadmanSchedules",
                newName: "parity");

            migrationBuilder.RenameColumn(
                name: "Day",
                table: "HeadmanSchedules",
                newName: "day");

            migrationBuilder.RenameColumn(
                name: "StreamGroup",
                table: "HeadmanSchedules",
                newName: "stream_group");

            migrationBuilder.RenameColumn(
                name: "Parity",
                table: "HeadmanChanges",
                newName: "parity");

            migrationBuilder.RenameColumn(
                name: "Day",
                table: "HeadmanChanges",
                newName: "day");

            migrationBuilder.RenameColumn(
                name: "Changes",
                table: "HeadmanChanges",
                newName: "changes");

            migrationBuilder.RenameColumn(
                name: "StreamGroup",
                table: "HeadmanChanges",
                newName: "stream_group");

            migrationBuilder.RenameColumn(
                name: "PairNumber",
                table: "HeadmanChanges",
                newName: "pair_number");

            migrationBuilder.RenameColumn(
                name: "Parity",
                table: "HeadmanAnnotations",
                newName: "parity");

            migrationBuilder.RenameColumn(
                name: "Day",
                table: "HeadmanAnnotations",
                newName: "day");

            migrationBuilder.RenameColumn(
                name: "Annotation",
                table: "HeadmanAnnotations",
                newName: "annotation");

            migrationBuilder.RenameColumn(
                name: "StreamGroup",
                table: "HeadmanAnnotations",
                newName: "stream_group");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "schedule",
                table: "SharedSchedules",
                newName: "Schedule");

            migrationBuilder.RenameColumn(
                name: "parity",
                table: "SharedSchedules",
                newName: "Parity");

            migrationBuilder.RenameColumn(
                name: "day",
                table: "SharedSchedules",
                newName: "Day");

            migrationBuilder.RenameColumn(
                name: "stream_group",
                table: "SharedSchedules",
                newName: "StreamGroup");

            migrationBuilder.RenameColumn(
                name: "schedule",
                table: "PersonalSchedules",
                newName: "Schedule");

            migrationBuilder.RenameColumn(
                name: "parity",
                table: "PersonalSchedules",
                newName: "Parity");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "PersonalSchedules",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "day",
                table: "PersonalSchedules",
                newName: "Day");

            migrationBuilder.RenameColumn(
                name: "stream_group",
                table: "PersonalSchedules",
                newName: "StreamGroup");

            migrationBuilder.RenameColumn(
                name: "parity",
                table: "PersonalChanges",
                newName: "Parity");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "PersonalChanges",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "day",
                table: "PersonalChanges",
                newName: "Day");

            migrationBuilder.RenameColumn(
                name: "changes",
                table: "PersonalChanges",
                newName: "Changes");

            migrationBuilder.RenameColumn(
                name: "stream_group",
                table: "PersonalChanges",
                newName: "StreamGroup");

            migrationBuilder.RenameColumn(
                name: "pair_number",
                table: "PersonalChanges",
                newName: "PairNumber");

            migrationBuilder.RenameColumn(
                name: "parity",
                table: "PersonalAnnotations",
                newName: "Parity");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "PersonalAnnotations",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "day",
                table: "PersonalAnnotations",
                newName: "Day");

            migrationBuilder.RenameColumn(
                name: "annotation",
                table: "PersonalAnnotations",
                newName: "Annotation");

            migrationBuilder.RenameColumn(
                name: "stream_group",
                table: "PersonalAnnotations",
                newName: "StreamGroup");

            migrationBuilder.RenameColumn(
                name: "schedule",
                table: "HeadmanSchedules",
                newName: "Schedule");

            migrationBuilder.RenameColumn(
                name: "parity",
                table: "HeadmanSchedules",
                newName: "Parity");

            migrationBuilder.RenameColumn(
                name: "day",
                table: "HeadmanSchedules",
                newName: "Day");

            migrationBuilder.RenameColumn(
                name: "stream_group",
                table: "HeadmanSchedules",
                newName: "StreamGroup");

            migrationBuilder.RenameColumn(
                name: "parity",
                table: "HeadmanChanges",
                newName: "Parity");

            migrationBuilder.RenameColumn(
                name: "day",
                table: "HeadmanChanges",
                newName: "Day");

            migrationBuilder.RenameColumn(
                name: "changes",
                table: "HeadmanChanges",
                newName: "Changes");

            migrationBuilder.RenameColumn(
                name: "stream_group",
                table: "HeadmanChanges",
                newName: "StreamGroup");

            migrationBuilder.RenameColumn(
                name: "pair_number",
                table: "HeadmanChanges",
                newName: "PairNumber");

            migrationBuilder.RenameColumn(
                name: "parity",
                table: "HeadmanAnnotations",
                newName: "Parity");

            migrationBuilder.RenameColumn(
                name: "day",
                table: "HeadmanAnnotations",
                newName: "Day");

            migrationBuilder.RenameColumn(
                name: "annotation",
                table: "HeadmanAnnotations",
                newName: "Annotation");

            migrationBuilder.RenameColumn(
                name: "stream_group",
                table: "HeadmanAnnotations",
                newName: "StreamGroup");
        }
    }
}
