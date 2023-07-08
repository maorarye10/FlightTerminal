using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassengersCount = table.Column<int>(type: "int", nullable: false),
                    IsDeparture = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Legs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    CrossingTime = table.Column<int>(type: "int", nullable: false),
                    NextLegs = table.Column<int>(type: "int", nullable: false),
                    ChangePlaneStatus = table.Column<bool>(type: "bit", nullable: false),
                    IsStartingLeg = table.Column<bool>(type: "bit", nullable: false),
                    IsDepartureLeg = table.Column<bool>(type: "bit", nullable: false),
                    CurrFlightId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Legs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Legs_Flights_CurrFlightId",
                        column: x => x.CurrFlightId,
                        principalTable: "Flights",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightId = table.Column<int>(type: "int", nullable: true),
                    LegId = table.Column<int>(type: "int", nullable: true),
                    In = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Out = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logs_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Logs_Legs_LegId",
                        column: x => x.LegId,
                        principalTable: "Legs",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Legs",
                columns: new[] { "Id", "ChangePlaneStatus", "CrossingTime", "CurrFlightId", "IsDepartureLeg", "IsStartingLeg", "NextLegs", "Number" },
                values: new object[,]
                {
                    { 1, false, 0, null, false, true, 1, 256 },
                    { 2, false, 1, null, false, false, 2, 1 },
                    { 3, false, 2, null, false, false, 4, 2 },
                    { 4, false, 3, null, false, false, 8, 4 },
                    { 5, false, 5, null, true, false, 16, 8 },
                    { 6, false, 3, null, false, false, 96, 16 },
                    { 7, true, 10, null, false, false, 128, 32 },
                    { 8, true, 10, null, false, false, 128, 64 },
                    { 9, false, 5, null, false, false, 8, 128 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Legs_CurrFlightId",
                table: "Legs",
                column: "CurrFlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_FlightId",
                table: "Logs",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_LegId",
                table: "Logs",
                column: "LegId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Legs");

            migrationBuilder.DropTable(
                name: "Flights");
        }
    }
}
