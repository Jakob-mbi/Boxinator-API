using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Boxinator_API.Migrations
{
    /// <inheritdoc />
    public partial class addData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Shipments",
                columns: new[] { "Id", "BoxColor", "DestinationID", "Email", "Price", "ReciverName", "UserSub", "Weight" },
                values: new object[,]
                {
                    { 7, "Pink", 8, null, 400m, "Emily Davis", "9e305eb4-7639-422d-9432-a3e001c6c5b7", 60 },
                    { 8, "Orange", 17, null, 300m, "Bob Davis", "c7643ce3-acaa-470e-8f11-a634dccad52a", 10 }
                });

            migrationBuilder.InsertData(
                table: "ShipmentStatus",
                columns: new[] { "ShipmentsListId", "StatusListId" },
                values: new object[,]
                {
                    { 7, 4 },
                    { 8, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ShipmentStatus",
                keyColumns: new[] { "ShipmentsListId", "StatusListId" },
                keyValues: new object[] { 7, 4 });

            migrationBuilder.DeleteData(
                table: "ShipmentStatus",
                keyColumns: new[] { "ShipmentsListId", "StatusListId" },
                keyValues: new object[] { 8, 5 });

            migrationBuilder.DeleteData(
                table: "Shipments",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Shipments",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
