using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Migrations
{
    public partial class SeedProductInitial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ID", "AddedAt", "Category", "Name", "Price" },
                values: new object[] { 2, new DateTime(2021, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "product2", 20 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ID",
                keyValue: 2);
        }
    }
}
