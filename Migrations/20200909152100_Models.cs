using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceManagement.Migrations
{
    public partial class Models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: true),
                    Role = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    YearsOfExperience = table.Column<int>(nullable: true),
                    Salary = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    RegistrationNumber = table.Column<string>(maxLength: 6, nullable: false),
                    Make = table.Column<string>(maxLength: 50, nullable: true),
                    Model = table.Column<string>(maxLength: 50, nullable: true),
                    EngineCapacity = table.Column<float>(nullable: false),
                    FuelType = table.Column<int>(nullable: false),
                    ManufactureDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.RegistrationNumber);
                });

            migrationBuilder.CreateTable(
                name: "Registration",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateRegistered = table.Column<DateTime>(nullable: false),
                    DateOfRepair = table.Column<DateTime>(nullable: false),
                    VehicleRegistrationNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registration", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Registration_Vehicle_VehicleRegistrationNumber",
                        column: x => x.VehicleRegistrationNumber,
                        principalTable: "Vehicle",
                        principalColumn: "RegistrationNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Repair",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleID = table.Column<int>(nullable: false),
                    MechanicID = table.Column<int>(nullable: false),
                    VehicleRegistrationNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repair", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Repair_User_MechanicID",
                        column: x => x.MechanicID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Repair_Vehicle_VehicleRegistrationNumber",
                        column: x => x.VehicleRegistrationNumber,
                        principalTable: "Vehicle",
                        principalColumn: "RegistrationNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workshop",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    City = table.Column<string>(maxLength: 50, nullable: true),
                    Street = table.Column<string>(maxLength: 50, nullable: true),
                    BuildingNumber = table.Column<int>(nullable: false),
                    PostalCode = table.Column<string>(maxLength: 5, nullable: true),
                    RegistrationID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workshop", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Workshop_Registration_RegistrationID",
                        column: x => x.RegistrationID,
                        principalTable: "Registration",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Service",
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
                    table.PrimaryKey("PK_Service", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Service_Repair_RepairID",
                        column: x => x.RepairID,
                        principalTable: "Repair",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Service_Workshop_WorkshopID",
                        column: x => x.WorkshopID,
                        principalTable: "Workshop",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Registration_VehicleRegistrationNumber",
                table: "Registration",
                column: "VehicleRegistrationNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Repair_MechanicID",
                table: "Repair",
                column: "MechanicID");

            migrationBuilder.CreateIndex(
                name: "IX_Repair_VehicleRegistrationNumber",
                table: "Repair",
                column: "VehicleRegistrationNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Service_RepairID",
                table: "Service",
                column: "RepairID");

            migrationBuilder.CreateIndex(
                name: "IX_Service_WorkshopID",
                table: "Service",
                column: "WorkshopID");

            migrationBuilder.CreateIndex(
                name: "IX_Workshop_RegistrationID",
                table: "Workshop",
                column: "RegistrationID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Repair");

            migrationBuilder.DropTable(
                name: "Workshop");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Registration");

            migrationBuilder.DropTable(
                name: "Vehicle");
        }
    }
}
