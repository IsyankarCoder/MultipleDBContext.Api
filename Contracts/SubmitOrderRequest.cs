namespace MultipleDBContext.Api.Contracts
{
    public class SubmitOrderRequest
    {
        public List<Guid> ProductIds { get; set; } = new();
    }
}
