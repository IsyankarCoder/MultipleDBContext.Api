namespace MultipleDBContext.Api.Orders
{
    public class LineItem
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }

        public decimal Price { get; set; }
    }
}
