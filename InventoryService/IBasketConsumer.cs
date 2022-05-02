
namespace InventoryService
{
    public interface IBasketConsumer
    {
        Task CloseQueueAsync();
        void RegisterReceiveMessageHandler();
    }
}