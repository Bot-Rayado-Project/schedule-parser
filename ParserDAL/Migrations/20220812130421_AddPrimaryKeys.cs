using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParserDAL.Migrations
{
    public partial class AddPrimaryKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "stream_group",
                table: "SharedSchedules",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "parity",
                table: "SharedSchedules",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "day",
                table: "SharedSchedules",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "stream_group",
                table: "PersonalSchedules",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "parity",
                table: "PersonalSchedules",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "day",
                table: "PersonalSchedules",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "stream_group",
                table: "PersonalChanges",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "parity",
                table: "PersonalChanges",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "day",
                table: "PersonalChanges",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "stream_group",
                table: "PersonalAnnotations",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "parity",
                table: "PersonalAnnotations",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "day",
                table: "PersonalAnnotations",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "stream_group",
                table: "HeadmanSchedules",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "parity",
                table: "HeadmanSchedules",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "day",
                table: "HeadmanSchedules",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "stream_group",
                table: "HeadmanChanges",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "parity",
                table: "HeadmanChanges",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "day",
                table: "HeadmanChanges",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "stream_group",
                table: "HeadmanAnnotations",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "parity",
                table: "HeadmanAnnotations",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "day",
                table: "HeadmanAnnotations",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "parity",
                table: "SharedSchedules",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "day",
                table: "SharedSchedules",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "stream_group",
                table: "SharedSchedules",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "parity",
                table: "PersonalSchedules",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "day",
                table: "PersonalSchedules",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "stream_group",
                table: "PersonalSchedules",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "parity",
                table: "PersonalChanges",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "day",
                table: "PersonalChanges",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "stream_group",
                table: "PersonalChanges",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "parity",
                table: "PersonalAnnotations",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "day",
                table: "PersonalAnnotations",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "stream_group",
                table: "PersonalAnnotations",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "parity",
                table: "HeadmanSchedules",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "day",
                table: "HeadmanSchedules",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "stream_group",
                table: "HeadmanSchedules",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "parity",
                table: "HeadmanChanges",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "day",
                table: "HeadmanChanges",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "stream_group",
                table: "HeadmanChanges",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "parity",
                table: "HeadmanAnnotations",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "day",
                table: "HeadmanAnnotations",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "stream_group",
                table: "HeadmanAnnotations",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);
        }
    }
}
