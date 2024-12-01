namespace ReenbitChat.Hubs;

public interface IChatClient
{
    Task ReceiveMessage(string userName, string message);
}
