using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceManagement.Migrations
{
    public partial class RenameServiceAdminToProvidedServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceAdmin");

            migrationBuilder.CreateTable(
                name: "OfferedService",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Price = table.Column<float>(nullable: false),
                    RepairTimeInHours = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferedService", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "OfferedService",
                columns: new[] { "ID", "Name", "Price", "RepairTimeInHours" },
                values: new object[] { 1, "Padangu keitimas", 50f, 2 });

            migrationBuilder.InsertData(
                table: "OfferedService",
                columns: new[] { "ID", "Name", "Price", "RepairTimeInHours" },
                values: new object[] { 2, "Stabdziu kaladeliu keitimas", 20f, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfferedService");

            migrationBuilder.CreateTable(
                name: "ServiceAdmin",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false),
                    RepairTimeInHours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceAdmin", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "ServiceAdmin",
                columns: new[] { "ID", "Name", "Price", "RepairTimeInHours" },
                values: new object[] { 1, "Padangu keitimas", 50f, 2 });

            migrationBuilder.InsertData(
                table: "ServiceAdmin",
                columns: new[] { "ID", "Name", "Price", "RepairTimeInHours" },
                values: new object[] { 2, "Stabdziu kaladeliu keitimas", 20f, 1 });
        }
    }
}
