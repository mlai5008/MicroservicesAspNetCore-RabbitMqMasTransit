using MassTransit;
using QueueReceiverService.QueueConsumerServices.PublishConsumerServices;
using QueueReceiverService.QueueConsumerServices.RequestResponseConsumerServices;
using QueueReceiverService.QueueConsumerServices.SenderConsumerServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<PublisherConsumerServiceA>();
    x.AddConsumer<PublisherConsumerServiceB>();
    x.AddConsumer<SenderConsumerService>();
    x.AddConsumer<RequestResponseConsumerService>();

    x.UsingRabbitMq((context, config) =>
    {
        config.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        config.ReceiveEndpoint("send-command", e =>
        {
            e.Consumer<SenderConsumerService>(context);
        });

        config.ReceiveEndpoint("publish-event", e =>
        {
            e.Consumer<PublisherConsumerServiceA>(context);
            e.Consumer<PublisherConsumerServiceB>(context);
        });        
        config.ReceiveEndpoint("request-response", e =>
        {
            e.Consumer<RequestResponseConsumerService>(context);
        });
    });
});

//builder.Services.AddMassTransitHostedService();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
