using Microsoft.AspNetCore.Mvc;
using OrderMS.Models;
using OrderMS.Repository;
using System.Transactions;

namespace OrderMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepo _orderRepository;

        public OrderController(IOrderRepo orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var orders = _orderRepository.GetOrders();
            return Ok(orders);
        }

        [HttpGet("{id}", Name = "GetOrderById")]
        public IActionResult Get(int id)
        {
            var order = _orderRepository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest("Order is null.");
            }

            using (var scope = new TransactionScope())
            {
                _orderRepository.insertOrder(order);
                _orderRepository.Save();
                scope.Complete();
                return CreatedAtRoute("GetOrderById", new { id = order.OrderId }, order);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest("Order is null.");
            }

            using (var scope = new TransactionScope())
            {
                _orderRepository.updateOrder(order);
                _orderRepository.Save();
                scope.Complete();
                return Ok(order);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var order = _orderRepository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            using (var scope = new TransactionScope())
            {
                _orderRepository.deleteOrder(id);
                _orderRepository.Save();
                scope.Complete();
                return Ok();
            }
        }
    }
}
