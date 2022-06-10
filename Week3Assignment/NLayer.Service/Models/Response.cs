using System.Text.Json.Serialization;

namespace NLayer.Service.Models
{
    internal class Response<T>
    {
        public T Data { get; set; }

        public List<string> Errors { get; set; }

        [JsonIgnore]
        public int Status { get; set; }
    }
}
