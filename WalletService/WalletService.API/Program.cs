using WalletService.Infrastructure.Extensions;
using SharedService.Messaging.MassTransit;
using WalletService.Application.Consumers;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// add services container
builder.Services.AddControllers();
builder.Services.AddLogging();
builder.Services.AddInfra(builder.Configuration);
builder.Services.AddMessageBroker(
    builder.Configuration,
    Assembly.GetAssembly(typeof(UserCreatedConsumer))!,
    Assembly.GetAssembly(typeof(WalletInfoConsumer))!,
    Assembly.GetAssembly(typeof(TransferConsumer))!
);

var app = builder.Build();

app.MapControllers();

app.Run();
