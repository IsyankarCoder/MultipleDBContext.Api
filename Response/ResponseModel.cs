using System.Text.Json.Serialization;

namespace MultipleDBContext.Api.Response
{
    //C#'ta record yazdığında varsayılan olarak record class kabul edilir.
    /// <summary>
    ///public record User(int Id);       // Record class
    ///public record class User(int Id); // Record class
    ///public record struct User(int Id); // Record struct
    /// </summary>
    /// <typeparam name="T"></typeparam>

    public record class ResponseModel<T>
    {
        public ResponseModel()
        {
            Success = true;
            Message = string.Empty;
            Data = default(T);
        }
        public bool Success { get; set; }
        public string Message { get; set; }


        [JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string>? Errors { get; set; }
    }
}
