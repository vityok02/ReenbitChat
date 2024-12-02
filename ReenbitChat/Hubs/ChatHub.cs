using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace ReenbitChat.Hubs;

public class ChatHub : Hub<IChatClient>
{
    private readonly IDistributedCache _cache;

    public ChatHub(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task JoinChat(UserConnection connection)
    {
        await Groups
            .AddToGroupAsync(Context.ConnectionId, connection.ChatRoom);

        var stringConnection = JsonSerializer
            .Serialize(connection);

        await _cache
            .SetStringAsync(Context.ConnectionId, stringConnection);

        await Clients
            .Group(connection.ChatRoom)
            .ReceiveMessage("Admin", $"{connection.UserName} joined the chat");
    }

    public async Task SendMessage(string message)
    {
        var connection = await GetConnectionAsync(Context.ConnectionId);

        if (connection is null)
        {
            return;
        }

        await Clients
            .Group(connection.ChatRoom)
            .ReceiveMessage(connection.UserName, message);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var connection = await GetConnectionAsync(Context.ConnectionId);

        if (connection is null)
        {
            return;
        }

        await _cache
            .RemoveAsync(Context.ConnectionId);

        await Groups
            .RemoveFromGroupAsync(Context.ConnectionId, connection.ChatRoom);

        await Clients
            .Group(connection.ChatRoom)
            .ReceiveMessage("Admin", $"{connection.UserName} left the chat");

        await base.OnDisconnectedAsync(exception);
    }

    private async Task<UserConnection?> GetConnectionAsync(string connectionId)
    {
        var stringConnection = await _cache.GetAsync(connectionId);

        return JsonSerializer
            .Deserialize<UserConnection>(stringConnection);
    }
}
