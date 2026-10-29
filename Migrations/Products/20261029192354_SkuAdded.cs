using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultipleDBContext.Api.Migrations.Products
{
    /// <inheritdoc />
    public partial class SkuAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sku",
                schema: "MyDemoProducts",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sku",
                schema: "MyDemoProducts",
                table: "Products");
        }
    }
}
