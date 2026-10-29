using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultipleDBContext.Api.Migrations.Products
{
    /// <inheritdoc />
    public partial class ComplexMoneyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                schema: "MyDemoProducts",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PriceTotal",
                schema: "MyDemoProducts",
                table: "Products");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount_Amount",
                schema: "MyDemoProducts",
                table: "Products",
                type: "decimal(19,6)",
                precision: 19,
                scale: 6,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Amount_CurrencyCode",
                schema: "MyDemoProducts",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Amount_CurrencySymbol",
                schema: "MyDemoProducts",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price_Amount",
                schema: "MyDemoProducts",
                table: "Products",
                type: "decimal(19,6)",
                precision: 19,
                scale: 6,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Price_CurrencyCode",
                schema: "MyDemoProducts",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Price_CurrencySymbol",
                schema: "MyDemoProducts",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount_Amount",
                schema: "MyDemoProducts",
                table: "Products",
                type: "decimal(19,6)",
                precision: 19,
                scale: 6,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "TotalAmount_CurrencyCode",
                schema: "MyDemoProducts",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TotalAmount_CurrencySymbol",
                schema: "MyDemoProducts",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice_Amount",
                schema: "MyDemoProducts",
                table: "Products",
                type: "decimal(19,6)",
                precision: 19,
                scale: 6,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "TotalPrice_CurrencyCode",
                schema: "MyDemoProducts",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TotalPrice_CurrencySymbol",
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
                name: "Amount_Amount",
                schema: "MyDemoProducts",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Amount_CurrencyCode",
                schema: "MyDemoProducts",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Amount_CurrencySymbol",
                schema: "MyDemoProducts",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price_Amount",
                schema: "MyDemoProducts",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price_CurrencyCode",
                schema: "MyDemoProducts",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price_CurrencySymbol",
                schema: "MyDemoProducts",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TotalAmount_Amount",
                schema: "MyDemoProducts",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TotalAmount_CurrencyCode",
                schema: "MyDemoProducts",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TotalAmount_CurrencySymbol",
                schema: "MyDemoProducts",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TotalPrice_Amount",
                schema: "MyDemoProducts",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TotalPrice_CurrencyCode",
                schema: "MyDemoProducts",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TotalPrice_CurrencySymbol",
                schema: "MyDemoProducts",
                table: "Products");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                schema: "MyDemoProducts",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceTotal",
                schema: "MyDemoProducts",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
