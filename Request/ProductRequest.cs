namespace MultipleDBContext.Api.Request
{
    public sealed record ProductRequest
        : ListQuery
    {
         public string? SearchTerm {  get; set; }
    }

}
