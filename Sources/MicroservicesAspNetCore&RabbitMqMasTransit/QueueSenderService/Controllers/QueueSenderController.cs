using CommonResources.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace QueueSenderService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QueueSenderController : ControllerBase
    {
        private readonly ILogger<QueueSenderController> _logger;
        private readonly IBus _bus;
        private readonly IRequestClient<RequestDataMessage> _client;

        public QueueSenderController(ILogger<QueueSenderController> logger, IBus bus, IRequestClient<RequestDataMessage> client)
        {
            _logger = logger;
            _bus = bus;
            _client = client;
        }

        [HttpPost("send-command")]                
        public async Task<IActionResult> SendCommand(string name, int deposit)
        {
            var commandSendMessage = new CommandSendMessage()
            {
                Name = name,
                Deposit = deposit
            };

            var url = new Uri("rabbitmq://localhost/send-command");

            var endpoint = await _bus.GetSendEndpoint(url);
            await endpoint.Send(commandSendMessage);

            _logger.LogInformation($"Command sent. Message: Name-{commandSendMessage.Name}; Deposit-{commandSendMessage.Deposit} {DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")}");
            return Ok("Command sent successfully");
        }

        [HttpPost("publish-event")]
        public async Task<IActionResult> PublishEvent(string name, int pin)
        {       
            var eventPublishMessage = new EventPublishMessage()
            {
                Name = name,
                Pin = pin
            };

            await _bus.Publish(eventPublishMessage);

            _logger.LogInformation($"Event published. Message: Name-{eventPublishMessage.Name}; Pin-{eventPublishMessage.Pin} {DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")}");
            return Ok("Event published successfully");
        }

        [HttpPost("request-response")]
        public async Task<IActionResult> RequestResponse(string type, int amount)
        {
            var requestData = new RequestDataMessage()
            {
                Type = type,
                Amount = amount
            };
            var request = _client.Create(requestData);
            var response = await request.GetResponse<ResponseDataMessage>();

            _logger.LogInformation($"Request-response. Message: Balance-{response.Message.Balance} {DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")}");
            return Ok(response);
        }
    }
}
