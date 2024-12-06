using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace ReenbitChat.Hubs;

public class ChatHub : Hub<IChatClient>
{
    private readonly IMemoryCache _cache;
    private readonly TextAnalyticsService _textAnalyticsService;
    private readonly ILogger<ChatHub> _logger;

    public ChatHub(IMemoryCache cache, TextAnalyticsService textAnalyticsService, ILogger<ChatHub> logger)
    {
        _cache = cache;
        _textAnalyticsService = textAnalyticsService;
        _logger = logger;
    }

    public async Task JoinChat(UserConnection connection)
    {
        await Groups
            .AddToGroupAsync(Context.ConnectionId, connection.ChatRoom);

        _cache.Set(Context.ConnectionId, connection);

        await Clients
            .Group(connection.ChatRoom)
            .ReceiveMessage(connection.UserName, $"{connection.UserName} joined the chat", "Neutral");

        _logger.LogInformation($"{connection.UserName} joined the chat");
    }

    public async Task SendMessage(string message)
    {
        var connection = GetConnection(Context.ConnectionId);

        if (connection is null)
        {
            return;
        }

        var sentiment = await _textAnalyticsService.AnalyzeSentimentAsync(message);
        await Clients
            .Group(connection.ChatRoom)
            .ReceiveMessage(connection.UserName, message, sentiment.Sentiment);

        _logger.LogInformation($"{connection.UserName} sent a message: {message}");
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var connection = GetConnection(Context.ConnectionId);

        if (connection is null)
        {
            return;
        }

        _cache.Remove(Context.ConnectionId);

        await Groups
            .RemoveFromGroupAsync(Context.ConnectionId, connection.ChatRoom);

        await Clients
            .Group(connection.ChatRoom)
            .ReceiveMessage("Admin", $"{connection.UserName} left the chat");

        await base.OnDisconnectedAsync(exception);

        _logger.LogInformation($"{connection.UserName} left the chat");
    }

    private UserConnection? GetConnection(string connectionId)
    {
        return _cache.TryGetValue(connectionId, out UserConnection? connection)
            ? connection
            : null;
    }
}
