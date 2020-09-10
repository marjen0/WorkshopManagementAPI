using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceManagement.Migrations
{
    public partial class RemoveFKFromService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_Repair_RepairID",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Workshop_WorkshopID",
                table: "Service");

            migrationBuilder.AlterColumn<int>(
                name: "WorkshopID",
                table: "Service",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RepairID",
                table: "Service",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Repair_RepairID",
                table: "Service",
                column: "RepairID",
                principalTable: "Repair",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Workshop_WorkshopID",
                table: "Service",
                column: "WorkshopID",
                principalTable: "Workshop",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Service_Repair_RepairID",
                table: "Service");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Workshop_WorkshopID",
                table: "Service");

            migrationBuilder.AlterColumn<int>(
                name: "WorkshopID",
                table: "Service",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RepairID",
                table: "Service",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Repair_RepairID",
                table: "Service",
                column: "RepairID",
                principalTable: "Repair",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Workshop_WorkshopID",
                table: "Service",
                column: "WorkshopID",
                principalTable: "Workshop",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
