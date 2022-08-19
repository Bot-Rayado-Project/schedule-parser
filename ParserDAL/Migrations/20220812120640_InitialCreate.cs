using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParserDAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HeadmanAnnotations",
                columns: table => new
                {
                    StreamGroup = table.Column<string>(type: "text", nullable: false),
                    Day = table.Column<string>(type: "text", nullable: false),
                    Parity = table.Column<string>(type: "text", nullable: false),
                    Annotation = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "HeadmanChanges",
                columns: table => new
                {
                    StreamGroup = table.Column<string>(type: "text", nullable: false),
                    Day = table.Column<string>(type: "text", nullable: false),
                    Parity = table.Column<string>(type: "text", nullable: false),
                    PairNumber = table.Column<int>(type: "integer", nullable: false),
                    Changes = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "HeadmanSchedules",
                columns: table => new
                {
                    StreamGroup = table.Column<string>(type: "text", nullable: false),
                    Day = table.Column<string>(type: "text", nullable: false),
                    Parity = table.Column<string>(type: "text", nullable: false),
                    Schedule = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "PersonalAnnotations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    StreamGroup = table.Column<string>(type: "text", nullable: false),
                    Day = table.Column<string>(type: "text", nullable: false),
                    Parity = table.Column<string>(type: "text", nullable: false),
                    Annotation = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "PersonalChanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    StreamGroup = table.Column<string>(type: "text", nullable: false),
                    Day = table.Column<string>(type: "text", nullable: false),
                    Parity = table.Column<string>(type: "text", nullable: false),
                    PairNumber = table.Column<int>(type: "integer", nullable: false),
                    Changes = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "PersonalSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    StreamGroup = table.Column<string>(type: "text", nullable: false),
                    Day = table.Column<string>(type: "text", nullable: false),
                    Parity = table.Column<string>(type: "text", nullable: false),
                    Schedule = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "SharedSchedules",
                columns: table => new
                {
                    StreamGroup = table.Column<string>(type: "text", nullable: false),
                    Day = table.Column<string>(type: "text", nullable: false),
                    Parity = table.Column<string>(type: "text", nullable: false),
                    Schedule = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeadmanAnnotations");

            migrationBuilder.DropTable(
                name: "HeadmanChanges");

            migrationBuilder.DropTable(
                name: "HeadmanSchedules");

            migrationBuilder.DropTable(
                name: "PersonalAnnotations");

            migrationBuilder.DropTable(
                name: "PersonalChanges");

            migrationBuilder.DropTable(
                name: "PersonalSchedules");

            migrationBuilder.DropTable(
                name: "SharedSchedules");
        }
    }
}
