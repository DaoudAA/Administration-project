using System.Text.Json.Serialization;

namespace OrderMS.Models
{
    public class OrderTicket
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("clientId")]
        public long ClientId { get; set; }

        [JsonPropertyName("orderId")]
        public long OrderId { get; set; }
    }
}
