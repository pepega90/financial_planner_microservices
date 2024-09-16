using UserService.Infrastructure.Extensions;
using SharedService.Messaging.MassTransit;

var builder = WebApplication.CreateBuilder(args);

// add services to IOC
builder.Services.AddControllers();
builder.Services.AddLogging();
builder.Services.AddInfra(builder.Configuration);
builder.Services.AddMessageBroker(builder.Configuration);

var app = builder.Build();

// http request pipeline

app.MapControllers();

app.Run();