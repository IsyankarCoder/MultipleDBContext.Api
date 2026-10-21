using System.ComponentModel.DataAnnotations;

namespace MultipleDBContext.Api.Shipments
{
    public sealed class Shipment
    {
        public int Id { get; set; }
        public required string ShipmentNumber { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public required ShippingDetails Shipping { get; set; }
        public List<ShipmentNote> Notes { get; set; } = [];

        [Timestamp]
        public byte[] RowVersion { get; set; } = [];
    }

    public sealed record ShippingDetails
    {
        public required string Recipient { get; init; }
        public required string City { get; init; }
        public required string Country { get; init; }
        public string? TrackingNumber { get; init; }
    }


    public sealed record ShipmentNote(string Text, DateTimeOffset AddedAt);
}
