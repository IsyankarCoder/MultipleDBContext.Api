using System.ComponentModel.DataAnnotations;

namespace MultipleDBContext.Api.Products
{
    public class Product
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string Tags {  get; set; }
        public string Description { get; set; }

        [Timestamp]
        public int RowId {  get; set; }


        public decimal? PriceTotal { get; set; }

        public DateTime CreatedDate { get; set; } 
        public DateTime UpdatedDate { get; set; }

    }
}
