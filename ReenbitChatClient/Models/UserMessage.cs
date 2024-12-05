namespace ReenbitChatClient.Models;

public record UserMessage(string UserName, string Text, string? Sentiment = null!)
    : Message(UserName, Text);
