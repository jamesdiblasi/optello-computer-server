using ComputerUse.Application;
using ComputerUse.Infrastructure.Anthropic;
using ComputerUse.Infrastructure.OS;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddAnthropicInfrastructure();
builder.Services.AddComputerUseInfrastructureOs(builder.Configuration.GetSection(ComputerUseInfrastructureOsOptions.ConfigurationSectionName));
builder.Services.AddComputerUseApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}*/

app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options
        .WithTitle("ComputerUse Tools")
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
});

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();