using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceManagement.Migrations
{
    public partial class AddAdminServicesFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceAdmin",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Price = table.Column<float>(nullable: false),
                    RepairTimeInHours = table.Column<int>(nullable: false),
                    WorkshopID = table.Column<int>(nullable: false),
                    RepairID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceAdmin", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ServiceAdmin_Repair_RepairID",
                        column: x => x.RepairID,
                        principalTable: "Repair",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceAdmin_Workshop_WorkshopID",
                        column: x => x.WorkshopID,
                        principalTable: "Workshop",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAdmin_RepairID",
                table: "ServiceAdmin",
                column: "RepairID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAdmin_WorkshopID",
                table: "ServiceAdmin",
                column: "WorkshopID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceAdmin");
        }
    }
}
