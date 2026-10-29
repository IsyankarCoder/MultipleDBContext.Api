using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace MultipleDBContext.Api.Products
{
    public class Product
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Sku { get; set; } = string.Empty;


        public string Tags { get; set; }
        public string Description { get; set; }

        [Timestamp]
        public int RowId { get; set; }


        public Money TotalPrice { get; set; }
        public Money Price { get; set; }
        public Money TotalAmount { get; set; }
        public Money Amount { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }

    public sealed record Money()
    {
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; } = "TRY";
        public string CurrencySymbol { get; set; }

    }
}
