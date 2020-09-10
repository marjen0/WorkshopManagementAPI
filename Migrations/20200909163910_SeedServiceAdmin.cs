using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceManagement.Migrations
{
    public partial class SeedServiceAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ServiceAdmin",
                columns: new[] { "ID", "Name", "Price", "RepairTimeInHours" },
                values: new object[] { 1, "Padangu keitimas", 50f, 2 });

            migrationBuilder.InsertData(
                table: "ServiceAdmin",
                columns: new[] { "ID", "Name", "Price", "RepairTimeInHours" },
                values: new object[] { 2, "Stabdziu kaladeliu keitimas", 20f, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ServiceAdmin",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ServiceAdmin",
                keyColumn: "ID",
                keyValue: 2);
        }
    }
}
