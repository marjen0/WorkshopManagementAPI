using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceManagement.Migrations
{
    public partial class AlterNavigationProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workshop_Registration_RegistrationID",
                table: "Workshop");

            migrationBuilder.DropIndex(
                name: "IX_Workshop_RegistrationID",
                table: "Workshop");

            migrationBuilder.AddColumn<int>(
                name: "WorkshopID",
                table: "Registration",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Registration_WorkshopID",
                table: "Registration",
                column: "WorkshopID");

            migrationBuilder.AddForeignKey(
                name: "FK_Registration_Workshop_WorkshopID",
                table: "Registration",
                column: "WorkshopID",
                principalTable: "Workshop",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registration_Workshop_WorkshopID",
                table: "Registration");

            migrationBuilder.DropIndex(
                name: "IX_Registration_WorkshopID",
                table: "Registration");

            migrationBuilder.DropColumn(
                name: "WorkshopID",
                table: "Registration");

            migrationBuilder.CreateIndex(
                name: "IX_Workshop_RegistrationID",
                table: "Workshop",
                column: "RegistrationID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Workshop_Registration_RegistrationID",
                table: "Workshop",
                column: "RegistrationID",
                principalTable: "Registration",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
