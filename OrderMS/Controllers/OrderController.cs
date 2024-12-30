using Microsoft.AspNetCore.Mvc;
using OrderMS.Models;
using Dapr.Client;
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
        public async Task<IActionResult> Post([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest("Order is null.");
            }

            var daprClient = new DaprClientBuilder().Build();
            var clientUrl = $"/clients/{order.clientId}/";

            try
            {
                // Debugging log for client URL
                Console.WriteLine($"Invoking method: {clientUrl} on clientservice");

                // Correct service-to-service invocation
                var clientResponse = await daprClient.InvokeMethodAsync<Client>(HttpMethod.Get,"clientms", clientUrl);

                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    _orderRepository.insertOrder(order);
                    _orderRepository.Save();
                    scope.Complete();

                    var message = $"Order {order.OrderId} related to {clientResponse.nom} is passed. Check your email {clientResponse.email}.";
                    return CreatedAtRoute("GetOrderById", new { id = order.OrderId }, new { order, message });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Details: {ex}");
                return StatusCode(500, $"Failed to fetch client details: {ex.Message}");
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
    public class Client
    {
        public int Id { get; set; }
        public string nom { get; set; }
        public string email { get; set; }
        public string telephone { get; set; }
        public string adresse { get; set; }
        public string dateInscription { get; set; }
    }
}
