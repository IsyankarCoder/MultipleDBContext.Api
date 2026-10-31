using Microsoft.AspNetCore.Mvc;

namespace MultipleDBContext.Api.Request
{
    public abstract record ListQuery
        : PagedRequest
    {
        [FromQuery(Name = "sort")]
        public string[] Sort { get; set; } = [];
    }
}
