using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Migrations
{
    public partial class SeedProductInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ID", "AddedAt", "Category", "Name", "Price" },
                values: new object[] { 1, new DateTime(2021, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "product1", 100 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 1);
        }
    }
}
