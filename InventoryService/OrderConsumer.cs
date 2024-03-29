﻿using MassTransit;
using SharedLib.Models;

namespace InventoryService
{
    public class OrderConsumer:IConsumer<Order>
    {
        public async Task Consume(ConsumeContext<Order> context)
        {
            var msg = context.Message;
            await Console.Out.WriteLineAsync(msg.Details);
        }
    }
}
