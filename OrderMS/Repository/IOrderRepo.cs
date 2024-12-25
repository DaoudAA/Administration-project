using OrderMS.Models;

namespace OrderMS.Repository
{
    public interface IOrderRepo
    {
        IEnumerable<Order> GetOrders();
        Order GetOrderById(int OrderId);
        void insertOrder(Order order);

        void updateOrder(Order order);
        void deleteOrder(int OrderId);

        void Save();


    }
}
