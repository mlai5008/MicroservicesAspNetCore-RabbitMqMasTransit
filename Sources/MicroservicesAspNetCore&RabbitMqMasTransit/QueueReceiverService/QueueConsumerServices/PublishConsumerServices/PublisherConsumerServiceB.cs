using CommonResources.Models;
using MassTransit;

namespace QueueReceiverService.QueueConsumerServices.RequestResponseConsumerServices
{
    public class PublisherConsumerServiceB : IConsumer<EventPublishMessage>
    {
        private readonly ILogger<PublisherConsumerServiceB> _logger;

        public PublisherConsumerServiceB(ILogger<PublisherConsumerServiceB> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<EventPublishMessage> context)
        {
            var infoB = context.Message;
            _logger.LogInformation($"{nameof(PublisherConsumerServiceB)}. Message: Name-{infoB.Name}; Pin-{infoB.Pin} {DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")}");
        }
    }
}
