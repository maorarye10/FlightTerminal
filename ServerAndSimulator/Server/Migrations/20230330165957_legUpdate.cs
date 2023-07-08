using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class legUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Legs_Flights_CurrFlightId",
                table: "Legs");

            migrationBuilder.DropIndex(
                name: "IX_Legs_CurrFlightId",
                table: "Legs");

            migrationBuilder.DropColumn(
                name: "CurrFlightId",
                table: "Legs");

            migrationBuilder.AddColumn<bool>(
                name: "IsOccupied",
                table: "Legs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsOccupied",
                value: false);

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsOccupied",
                value: false);

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsOccupied",
                value: false);

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsOccupied",
                value: false);

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 5,
                column: "IsOccupied",
                value: false);

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 6,
                column: "IsOccupied",
                value: false);

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 7,
                column: "IsOccupied",
                value: false);

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 8,
                column: "IsOccupied",
                value: false);

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 9,
                column: "IsOccupied",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOccupied",
                table: "Legs");

            migrationBuilder.AddColumn<int>(
                name: "CurrFlightId",
                table: "Legs",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 1,
                column: "CurrFlightId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 2,
                column: "CurrFlightId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 3,
                column: "CurrFlightId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 4,
                column: "CurrFlightId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 5,
                column: "CurrFlightId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 6,
                column: "CurrFlightId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 7,
                column: "CurrFlightId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 8,
                column: "CurrFlightId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Legs",
                keyColumn: "Id",
                keyValue: 9,
                column: "CurrFlightId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Legs_CurrFlightId",
                table: "Legs",
                column: "CurrFlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Legs_Flights_CurrFlightId",
                table: "Legs",
                column: "CurrFlightId",
                principalTable: "Flights",
                principalColumn: "Id");
        }
    }
}
