using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceManagement.Migrations
{
    public partial class RemoveFieldsFromServiceAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAdmin_Repair_RepairID",
                table: "ServiceAdmin");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceAdmin_Workshop_WorkshopID",
                table: "ServiceAdmin");

            migrationBuilder.DropIndex(
                name: "IX_ServiceAdmin_RepairID",
                table: "ServiceAdmin");

            migrationBuilder.DropIndex(
                name: "IX_ServiceAdmin_WorkshopID",
                table: "ServiceAdmin");

            migrationBuilder.DropColumn(
                name: "RepairID",
                table: "ServiceAdmin");

            migrationBuilder.DropColumn(
                name: "WorkshopID",
                table: "ServiceAdmin");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RepairID",
                table: "ServiceAdmin",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkshopID",
                table: "ServiceAdmin",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAdmin_RepairID",
                table: "ServiceAdmin",
                column: "RepairID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAdmin_WorkshopID",
                table: "ServiceAdmin",
                column: "WorkshopID");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAdmin_Repair_RepairID",
                table: "ServiceAdmin",
                column: "RepairID",
                principalTable: "Repair",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceAdmin_Workshop_WorkshopID",
                table: "ServiceAdmin",
                column: "WorkshopID",
                principalTable: "Workshop",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
