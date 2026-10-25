namespace MultipleDBContext.Api.Contracts
{
    public class SubmitOrderRequest
    {
        public List<long> ProductIds { get; set; } = new();
    }
}
