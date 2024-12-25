using System.Text.Json.Serialization;
namespace OrderMS.Models
{
    public class OrderLine
    {
        public int Id { get; set; }
        public required string ProductName { get; set; }
        public double Price { get; set; } 
        public int Quantity { get; set; }

        public int OrderId { get; set; }

        [JsonIgnore]
        public Order? Order { get; set; }
    }
}
