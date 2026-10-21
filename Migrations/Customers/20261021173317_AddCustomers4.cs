using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultipleDBContext.Api.Migrations.Customers
{
    /// <inheritdoc />
    public partial class AddCustomers4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address_Street",
                schema: "MyDemoCustomers",
                table: "Customers",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "Address_PostalCode",
                schema: "MyDemoCustomers",
                table: "Customers",
                newName: "PostalCode");

            migrationBuilder.RenameColumn(
                name: "Address_Country",
                schema: "MyDemoCustomers",
                table: "Customers",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "Address_Coordinates_Longitude",
                schema: "MyDemoCustomers",
                table: "Customers",
                newName: "Longitude");

            migrationBuilder.RenameColumn(
                name: "Address_Coordinates_Latitude",
                schema: "MyDemoCustomers",
                table: "Customers",
                newName: "Latitude");

            migrationBuilder.RenameColumn(
                name: "Address_City",
                schema: "MyDemoCustomers",
                table: "Customers",
                newName: "City");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Street",
                schema: "MyDemoCustomers",
                table: "Customers",
                newName: "Address_Street");

            migrationBuilder.RenameColumn(
                name: "PostalCode",
                schema: "MyDemoCustomers",
                table: "Customers",
                newName: "Address_PostalCode");

            migrationBuilder.RenameColumn(
                name: "Longitude",
                schema: "MyDemoCustomers",
                table: "Customers",
                newName: "Address_Coordinates_Longitude");

            migrationBuilder.RenameColumn(
                name: "Latitude",
                schema: "MyDemoCustomers",
                table: "Customers",
                newName: "Address_Coordinates_Latitude");

            migrationBuilder.RenameColumn(
                name: "Country",
                schema: "MyDemoCustomers",
                table: "Customers",
                newName: "Address_Country");

            migrationBuilder.RenameColumn(
                name: "City",
                schema: "MyDemoCustomers",
                table: "Customers",
                newName: "Address_City");
        }
    }
}
