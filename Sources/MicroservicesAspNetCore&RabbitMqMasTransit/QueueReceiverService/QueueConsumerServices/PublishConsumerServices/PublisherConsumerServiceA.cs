using CommonResources.Models;
using MassTransit;

namespace QueueReceiverService.QueueConsumerServices.PublishConsumerServices
{
    public class PublisherConsumerServiceA : IConsumer<EventPublishMessage>
    {
        private ILogger<PublisherConsumerServiceA>_logger { get; }

        public PublisherConsumerServiceA(ILogger<PublisherConsumerServiceA> logger)
        {
           _logger = logger;
        }       

        public async Task Consume(ConsumeContext<EventPublishMessage> context)
        {
            var infoA = context.Message;
            _logger.LogInformation($"{nameof(PublisherConsumerServiceA)}. Message: Name-{infoA.Name}; Pin-{infoA.Pin} {DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")}");
        }
    }
}
