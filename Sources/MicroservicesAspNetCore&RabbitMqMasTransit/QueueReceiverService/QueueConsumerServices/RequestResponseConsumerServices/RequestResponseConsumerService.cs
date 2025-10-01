using CommonResources.Models;
using MassTransit;

namespace QueueReceiverService.QueueConsumerServices.RequestResponseConsumerServices
{
    public class RequestResponseConsumerService : IConsumer<RequestDataMessage>
    {
        private readonly ILogger<RequestResponseConsumerService> _logger;

        public RequestResponseConsumerService(ILogger<RequestResponseConsumerService> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<RequestDataMessage> context)
        {
            var requestData = context.Message;
            var responseData = new ResponseDataMessage
            {
                Balance = 10000 - requestData.Amount
            };
            _logger.LogInformation($"{nameof(RequestResponseConsumerService)}. Message: Type-{requestData.Type}; Amount-{requestData.Amount} {DateTime.Now.ToString("dd/MM/yyyy hh:mm:tt")}");

            await context.RespondAsync(responseData);
        }
    }
}
