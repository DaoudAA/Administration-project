namespace OrderMS.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public List<OrderLine> OrderItems { get; set; } = new List<OrderLine>();

        public long clientId { get; set; }
         public double totalPrice { get; set; }
    }
}
