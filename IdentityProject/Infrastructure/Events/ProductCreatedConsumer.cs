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
        private readonly AppIdentityDbContext _context;

        public ProductCreatedConsumer (AppIdentityDbContext context)
        {
            _context = context;
        }

        public async Task Consume(ConsumeContext<ProductEvent> context)
        {
            Console.Write(context.Message.Id);
            Console.Write(context.Message.Message);
            // _context.Add(article);

            // await _context.SaveChangesAsync();
        }
    }
}