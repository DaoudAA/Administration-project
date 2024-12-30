using OrderMS.Models;

namespace OrderMS.Services
{
    public interface IOrderService
    {

        void HandleOrderTicket(OrderTicket orderTicket);
    }
}
