namespace ReenbitChatClient.Models;

public record Message(string? UserName, string Text, string? Sentiment = null!);
