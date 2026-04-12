using DuaBusiness.Application.DependencyInjection;
using DuaBusiness.Infrastructure.DependencyInjection;
using DuaBusiness.Worker.HostedServices;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddHostedService<ProcessingJobBackgroundService>();

var host = builder.Build();
host.Run();
