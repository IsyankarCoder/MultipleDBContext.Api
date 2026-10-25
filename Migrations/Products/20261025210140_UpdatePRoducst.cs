using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MultipleDBContext.Api.Migrations.Products
{
    /// <inheritdoc />
    public partial class UpdatePRoducst : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                schema: "MyDemoProducts",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "MyDemoProducts",
                table: "Products");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "MyDemoProducts",
                table: "Products",
                type: "bigint",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                schema: "MyDemoProducts",
                table: "Products",
                column: "Id");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "MyDemoProducts",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "MyDemoProducts",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "PriceTotal",
                schema: "MyDemoProducts",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RowId",
                schema: "MyDemoProducts",
                table: "Products",
                type: "int",
                rowVersion: true,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                schema: "MyDemoProducts",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                schema: "MyDemoProducts",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "MyDemoProducts",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "MyDemoProducts",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PriceTotal",
                schema: "MyDemoProducts",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "RowId",
                schema: "MyDemoProducts",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Tags",
                schema: "MyDemoProducts",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                schema: "MyDemoProducts",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                schema: "MyDemoProducts",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "MyDemoProducts",
                table: "Products");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "MyDemoProducts",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.Sql(
                "UPDATE [MyDemoProducts].[Products] SET [Id] = NEWID() WHERE [Id] IS NULL");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "MyDemoProducts",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                schema: "MyDemoProducts",
                table: "Products",
                column: "Id");
        }
    }
}
