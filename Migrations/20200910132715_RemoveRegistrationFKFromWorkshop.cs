using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceManagement.Migrations
{
    public partial class RemoveRegistrationFKFromWorkshop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrationID",
                table: "Workshop");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegistrationID",
                table: "Workshop",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
