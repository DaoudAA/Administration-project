using OrderMS.Models;
using OrderMS.Repository;

namespace OrderMS.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepo;

        public OrderService(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public void HandleOrderTicket(OrderTicket orderTicket)
        {
            Console.WriteLine($"Received ticket: {orderTicket.Description}");

            // Update the order's totalPrice based on the ticket
            var order = _orderRepo.GetOrderById((int)orderTicket.OrderId);
            if (order != null)
            {
                //order.totalPrice = orderTicket.Description;  // Example: updating description
                //_orderRepo.updateOrder(order);
                //_orderRepo.Save();
                Console.WriteLine($"Got A ticket about this order {order.OrderId} name {orderTicket.Title} which states that : {orderTicket.Description}");
            }
        }
    }
}
