using CommonResources.Models;
using MassTransit;

namespace QueueReceiverService.QueueConsumerServices.SenderConsumerServices
{
    public class SenderConsumerService : IConsumer<CommandSendMessage>
    {
        private readonly ILogger<SenderConsumerService> _logger;

        public SenderConsumerService(ILogger<SenderConsumerService> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CommandSendMessage> context)
        {
            var info = context.Message;
            _logger.LogInformation($"{nameof(SenderConsumerService)}. Message: Name-{info.Name}; Deposit-{info.Deposit} {DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")}");
        }
    }
}
