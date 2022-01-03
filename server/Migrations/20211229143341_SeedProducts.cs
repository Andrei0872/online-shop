using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Migrations
{
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 2,
                column: "Category",
                value: 0);

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ID", "AddedAt", "Category", "Name", "Price" },
                values: new object[,]
                {
                    { 3, new DateTime(2021, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "product3", 25 },
                    { 4, new DateTime(2021, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "product4", 70 },
                    { 5, new DateTime(2021, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "product5", 65 },
                    { 6, new DateTime(2021, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "product6", 200 },
                    { 7, new DateTime(2021, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "product7", 55 },
                    { 8, new DateTime(2021, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "product8", 90 },
                    { 9, new DateTime(2021, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "product9", 120 },
                    { 10, new DateTime(2021, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "product10", 320 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 2,
                column: "Category",
                value: 1);
        }
    }
}
