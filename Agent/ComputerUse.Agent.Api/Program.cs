using ComputerUse.Agent.Api.Hubs;
using ComputerUse.Agent.Infrastructure;
using ComputerUse.Agent.Infrastructure.Anthropic;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddComputerUseAgentInfrastructure(builder.Configuration);
builder.Services.AddComputerUseAgentInfrastructureAnthropic();

builder.Services.AddHttpClient();

builder.Services.AddSignalR(hubOptions =>
{
    hubOptions.ClientTimeoutInterval = TimeSpan.FromMinutes(5);
});

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{*/
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("ComputerUse Tools")
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
/*}*/

app.UseSerilogRequestLogging();

app.UseRouting();
//app.UseHttpsRedirection();

app.UseCors(x => x
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin => true) // allow any origin 
        .AllowCredentials());

app.UseAuthorization();

app.MapControllers();

app.MapHub<AIMessagesHub>("/messages");

app.Run();
