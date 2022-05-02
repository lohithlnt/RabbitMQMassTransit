using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using SharedLib.Models;
using System.Text;
using System.Text.Json;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        QueueClient queueClient;
        IConfiguration configuration;

        public BasketController(IConfiguration config)
        {
            configuration = config;
            string connection = configuration["AzureServiceBus:Connection"];
            string queue = configuration["AzureServiceBus:Queue"];
            queueClient = new QueueClient(connection, queue);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(Basket basket)
        {
            string strData = JsonSerializer.Serialize(basket);
            Message msg = new Message(Encoding.UTF8.GetBytes(strData));
            await queueClient.SendAsync(msg);
            return Ok();
        }
    }
}
