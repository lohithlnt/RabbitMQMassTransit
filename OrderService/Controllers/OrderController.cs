using MassTransit;
using Microsoft.AspNetCore.Mvc;
using SharedLib.Models;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;
        public OrderController(IPublishEndpoint PublishEndpoint)
        {
            this._publishEndpoint = PublishEndpoint;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            await _publishEndpoint.Publish<Order>(order);
            return Ok();
        }  
    }
}
