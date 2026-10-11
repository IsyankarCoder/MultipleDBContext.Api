using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MultipleDBContext.Api.Migrations.Products
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "MyDemoProducts");

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "MyDemoProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "MyDemoProducts",
                table: "Products",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Product #1", 100m },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Product #2", 1001m },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Product #3", 1002m },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Product #4", 1003m },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "Product #5", 1004m },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "Product #6", 1005m },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "Product #7", 1006m },
                    { new Guid("88888888-8888-8888-8888-888888888888"), "Product #8", 1007m },
                    { new Guid("99999999-9999-9999-9999-999999999999"), "Product #9", 1008m },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Product #10", 1009m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products",
                schema: "MyDemoProducts");
        }
    }
}
