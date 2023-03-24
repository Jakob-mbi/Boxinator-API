using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Boxinator_API.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShipmentStatus",
                table: "ShipmentStatus");

            migrationBuilder.DropIndex(
                name: "IX_ShipmentStatus_StatusListId",
                table: "ShipmentStatus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShipmentStatus",
                table: "ShipmentStatus",
                columns: new[] { "StatusListId", "ShipmentsListId" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Multiplier", "Name" },
                values: new object[,]
                {
                    { 1, 0, "Sweden" },
                    { 2, 0, "Norway" },
                    { 3, 0, "Denmark" },
                    { 4, 0, "Finland" },
                    { 5, 3, "Estonia" },
                    { 6, 3, "Latvia" },
                    { 7, 3, "Lithuania" },
                    { 8, 5, "Germany" },
                    { 9, 5, "Poland" },
                    { 10, 6, "Netherlands" },
                    { 11, 6, "Belgium" },
                    { 12, 6, "Luxembourg" },
                    { 13, 8, "United Kingdom" },
                    { 14, 8, "Ireland" },
                    { 15, 7, "France" },
                    { 16, 9, "Spain" },
                    { 17, 10, "Portugal" },
                    { 18, 6, "Czech Republic" },
                    { 19, 7, "Austria" },
                    { 20, 7, "Switzerland" },
                    { 21, 9, "Italy" },
                    { 22, 12, "Iceland" }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "CREATED" },
                    { 2, "RECIEVED" },
                    { 3, "INTRANSIT" },
                    { 4, "COMPLETED" },
                    { 5, "CANCELLED" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Sub", "ContactNumber", "Country", "DateOfBirth", "ZipCode" },
                values: new object[,]
                {
                    { "44feb5ab-e680-4979-95f6-9cbc18d32077", null, null, null, null },
                    { "9e305eb4-7639-422d-9432-a3e001c6c5b7", null, null, null, null },
                    { "bcc36e9d-c309-4248-b777-0421c370eaba", null, null, null, null },
                    { "c7643ce3-acaa-470e-8f11-a634dccad52a", null, null, null, null },
                    { "e7359cd5-6dec-4f8b-be74-0e3148eaa51f", null, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "Shipments",
                columns: new[] { "Id", "BoxColor", "DestinationID", "Email", "Price", "ReciverName", "UserSub", "Weight" },
                values: new object[,]
                {
                    { 1, "Red", 1, null, 200m, "John Smith", "44feb5ab-e680-4979-95f6-9cbc18d32077", 50 },
                    { 2, "Blue", 14, null, 400m, "Alice Johnson", "44feb5ab-e680-4979-95f6-9cbc18d32077", 60 },
                    { 3, "Green", 10, null, 300m, "Bob Thompson", "44feb5ab-e680-4979-95f6-9cbc18d32077", 70 },
                    { 4, "Yellow", 3, null, 200m, "John Smith", "44feb5ab-e680-4979-95f6-9cbc18d32077", 50 },
                    { 5, "Purple", 8, null, 400m, "Emily Davis", "9e305eb4-7639-422d-9432-a3e001c6c5b7", 20 },
                    { 6, "Orange", 17, null, 300m, "Bob Thompson", "c7643ce3-acaa-470e-8f11-a634dccad52a", 30 },
                    { 7, "Pink", 8, null, 400m, "Emily Davis", "9e305eb4-7639-422d-9432-a3e001c6c5b7", 60 },
                    { 8, "Orange", 17, null, 300m, "Bob Davis", "c7643ce3-acaa-470e-8f11-a634dccad52a", 10 }
                });

            migrationBuilder.InsertData(
                table: "ShipmentStatus",
                columns: new[] { "ShipmentsListId", "StatusListId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 1 },
                    { 6, 1 },
                    { 1, 3 },
                    { 7, 4 },
                    { 8, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentStatus_ShipmentsListId",
                table: "ShipmentStatus",
                column: "ShipmentsListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShipmentStatus",
                table: "ShipmentStatus");

            migrationBuilder.DropIndex(
                name: "IX_ShipmentStatus_ShipmentsListId",
                table: "ShipmentStatus");

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "ShipmentStatus",
                keyColumns: new[] { "ShipmentsListId", "StatusListId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ShipmentStatus",
                keyColumns: new[] { "ShipmentsListId", "StatusListId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "ShipmentStatus",
                keyColumns: new[] { "ShipmentsListId", "StatusListId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "ShipmentStatus",
                keyColumns: new[] { "ShipmentsListId", "StatusListId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "ShipmentStatus",
                keyColumns: new[] { "ShipmentsListId", "StatusListId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "ShipmentStatus",
                keyColumns: new[] { "ShipmentsListId", "StatusListId" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "ShipmentStatus",
                keyColumns: new[] { "ShipmentsListId", "StatusListId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "ShipmentStatus",
                keyColumns: new[] { "ShipmentsListId", "StatusListId" },
                keyValues: new object[] { 7, 4 });

            migrationBuilder.DeleteData(
                table: "ShipmentStatus",
                keyColumns: new[] { "ShipmentsListId", "StatusListId" },
                keyValues: new object[] { 8, 5 });

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Sub",
                keyValue: "bcc36e9d-c309-4248-b777-0421c370eaba");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Sub",
                keyValue: "e7359cd5-6dec-4f8b-be74-0e3148eaa51f");

            migrationBuilder.DeleteData(
                table: "Shipments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Shipments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Shipments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Shipments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Shipments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Shipments",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Shipments",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Shipments",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Sub",
                keyValue: "44feb5ab-e680-4979-95f6-9cbc18d32077");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Sub",
                keyValue: "9e305eb4-7639-422d-9432-a3e001c6c5b7");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Sub",
                keyValue: "c7643ce3-acaa-470e-8f11-a634dccad52a");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShipmentStatus",
                table: "ShipmentStatus",
                columns: new[] { "ShipmentsListId", "StatusListId" });

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentStatus_StatusListId",
                table: "ShipmentStatus",
                column: "StatusListId");
        }
    }
}
