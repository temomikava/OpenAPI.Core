using IntegrationEvents;
using MassTransit;
using OpenAPI.Core.Data;
using SharedKernel;

namespace OpenAPI.Core.IntegrationEventHandlers
{
    public class OrderCreatedIntegrationEventHandler : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
    {
        private readonly ILogger<OrderCreatedIntegrationEventHandler> logger;
        private readonly IRepository<Payment, int> repository;

        public OrderCreatedIntegrationEventHandler(ILogger<OrderCreatedIntegrationEventHandler> logger, IRepository<Payment,int> repository)
        {
            this.logger = logger;
            this.repository = repository;
        }
        public async Task Consume(ConsumeContext<OrderCreatedIntegrationEvent> context)
        {
            var message = context.Message;
            var payment = new Payment
            {
                Amount = message.Amount,
                Currency = message.Currency,
                OrderId = message.OrderId,
            };
            await repository.CreateAsync(payment);
            logger.LogInformation($"Event Consumed Successfully");
        }
    }
}
