namespace MultipleDBContext.Api.Response
{
    public record ProductResponse(long Id, string Name, decimal Price, string Tags, string CurrencySymbol,
                                       string CurrencyCode);
}
