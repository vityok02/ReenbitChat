using Microsoft.Azure.SignalR;
using ReenbitChat;
using ReenbitChat.Hubs;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    var connection = builder.Configuration
//        .GetConnectionString("Redis");

//    options.Configuration = connection;
//});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(Environment.GetEnvironmentVariable("CORS_URI") ?? "http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddSignalR()
    .AddAzureSignalR(options =>
    {
        options.ConnectionString = builder.Configuration.GetConnectionString("AzureSignalR");
    });
builder.Services.AddSingleton<TextAnalyticsService>();

builder.Services.AddLogging();
builder.Services.AddMemoryCache();

var app = builder.Build();

app.UseCors();

app.MapGet("/", () => "Hello world!");
app.MapHub<ChatHub>("/chathub");

app.Run();
