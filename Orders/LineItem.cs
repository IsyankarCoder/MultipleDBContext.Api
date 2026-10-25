namespace MultipleDBContext.Api.Orders
{
    public class LineItem
    {
        public Guid Id { get; set; }
        public long ProductId { get; set; }

        public decimal Price { get; set; }
    }
}
