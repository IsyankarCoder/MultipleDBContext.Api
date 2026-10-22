using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultipleDBContext.Api.Migrations.Shipments
{
    /// <inheritdoc />
    public partial class AddShipmentIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ShipmentNumber",
                table: "Shipments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Shipment_Status_CreatedAtUtc",
                table: "Shipments",
                columns: new[] { "ShipmentNumber", "CreatedAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Shipment_Status_CreatedAtUtc",
                table: "Shipments");

            migrationBuilder.AlterColumn<string>(
                name: "ShipmentNumber",
                table: "Shipments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
