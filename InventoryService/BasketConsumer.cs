using Microsoft.Azure.ServiceBus;
using SharedLib.Models;
using System.Text;
using System.Text.Json;

namespace InventoryService
{
    public class BasketConsumer : IBasketConsumer
    {
        QueueClient queueClient;
        IConfiguration configuration;

        public BasketConsumer(IConfiguration config)
        {
            configuration = config;
            string connection = configuration["AzureServiceBus:Connection"];
            string queue = configuration["AzureServiceBus:Queue"];
            queueClient = new QueueClient(connection, queue);
        }
        public async Task CloseQueueAsync()
        {
            await queueClient.CloseAsync();
        }
        public void RegisterReceiveMessageHandler()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };
            queueClient.RegisterMessageHandler(ProcessMessageAsync, messageHandlerOptions);
        }
        private async Task ProcessMessageAsync(Message message, CancellationToken token)
        {
            var basket = JsonSerializer.Deserialize<Basket>(Encoding.UTF8.GetString(message.Body));

            //To do : Check stock availblity
            await Console.Out.WriteLineAsync(basket.Product);

            await queueClient.CompleteAsync(message.SystemProperties.LockToken);
            await CloseQueueAsync();
        }
        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            return Task.CompletedTask;
        }
    }
}
