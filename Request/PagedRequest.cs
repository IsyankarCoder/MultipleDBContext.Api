namespace MultipleDBContext.Api.Request
{
    public abstract record PagedRequest
    {
        public const int MaxSize = 100;
        public int Page { get; set; }
        public int PageSize { get; set; }

        public int Cursor {  get; set; }

        public bool HasValidPaging =>
            Page >= 1 &&
            PageSize >= 1 &&
            PageSize <= MaxSize;
    }

}
