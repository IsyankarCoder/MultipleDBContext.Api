namespace MultipleDBContext.Api.Response
{
    public sealed record CursorResponse<T>
    (
        long Cursor,
        T Data
    );
}
