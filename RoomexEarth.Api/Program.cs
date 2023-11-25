using RoomexEarth.Api;
using Serilog;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Host
    .UseSerilog((hostBuilderContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration));

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

IoC.RegisterServices(builder);

var app = builder.Build();

app
    .UseSwagger(_ => _.RouteTemplate = "{documentname}/swagger.json")
    .UseSwaggerUI(_ => _.RoutePrefix = "");

Endpoints.Map(app);

app.Run();
