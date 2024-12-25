using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OrderMS.DBContext;
using OrderMS.Models;

namespace OrderMS.Repository
{
    public class OrderRepo : IOrderRepo
    {
        private readonly OrderContext _context;

        public OrderRepo(OrderContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetOrders()
        {
            return _context.Orders.Include(o => o.OrderItems).ToList();
        }

        public Order GetOrderById(int orderId)
        {
            return _context.Orders.Include(o => o.OrderItems)
                                  .FirstOrDefault(o => o.OrderId == orderId);
        }

        public void insertOrder(Order order)
        {
            _context.Orders.Add(order);
        }

        public void updateOrder(Order order)
        {
            _context.Orders.Update(order);
        }

        public void deleteOrder(int orderId)
        {
            var order = GetOrderById(orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
