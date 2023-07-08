using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class logAndLegUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Flights_FlightId",
                table: "Logs");

            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Legs_LegId",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Logs_FlightId",
                table: "Logs");

            migrationBuilder.DropIndex(
                name: "IX_Logs_LegId",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "FlightId",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "LegId",
                table: "Logs");

            migrationBuilder.AddColumn<string>(
                name: "FlightNum",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LegNum",
                table: "Logs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RepresentationalNumber",
                table: "Legs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 1,
                column: "RepresentationalNumber",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 2,
                column: "RepresentationalNumber",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 3,
                column: "RepresentationalNumber",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 4,
                column: "RepresentationalNumber",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 5,
                column: "RepresentationalNumber",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 6,
                column: "RepresentationalNumber",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 7,
                column: "RepresentationalNumber",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 8,
                column: "RepresentationalNumber",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 9,
                column: "RepresentationalNumber",
                value: 8);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlightNum",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "LegNum",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "RepresentationalNumber",
                table: "Legs");

            migrationBuilder.AddColumn<int>(
                name: "FlightId",
                table: "Logs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LegId",
                table: "Logs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Logs_FlightId",
                table: "Logs",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_LegId",
                table: "Logs",
                column: "LegId");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Flights_FlightId",
                table: "Logs",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Legs_LegId",
                table: "Logs",
                column: "LegId",
                principalTable: "Legs",
                principalColumn: "Id");
        }
    }
}
