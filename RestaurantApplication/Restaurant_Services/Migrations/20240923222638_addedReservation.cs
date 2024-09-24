using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantServices.Migrations
{
    /// <inheritdoc />
    public partial class addedReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "TableNumber",
                table: "Reservations",
                newName: "TableId");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Reservations",
                newName: "DateReservation");

            migrationBuilder.AddColumn<bool>(
                name: "isOccupied",
                table: "Tables",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TableId",
                table: "Reservations",
                column: "TableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Tables_TableId",
                table: "Reservations",
                column: "TableId",
                principalTable: "Tables",
                principalColumn: "TableId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Tables_TableId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_TableId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "isOccupied",
                table: "Tables");

            migrationBuilder.RenameColumn(
                name: "TableId",
                table: "Reservations",
                newName: "TableNumber");

            migrationBuilder.RenameColumn(
                name: "DateReservation",
                table: "Reservations",
                newName: "Date");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "Reservations",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
