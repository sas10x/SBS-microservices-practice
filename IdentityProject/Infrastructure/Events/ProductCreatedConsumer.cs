using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Events;
using Infrastructure.Identity;
using MassTransit;

namespace Infrastructure.Events
{
    public class ProductCreatedConsumer : IConsumer<ProductEvent>
    {
        public ProductCreatedConsumer ()
        {
           
        }

        public async Task Consume(ConsumeContext<ProductEvent> productEvent)
        {
            Console.WriteLine("Consuming animal created " + productEvent.Message.Id);
        }
    }
}