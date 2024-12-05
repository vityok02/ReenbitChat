namespace ReenbitChat.Hubs;

public interface IChatClient
{
    Task ReceiveMessage(string message);
    Task ReceiveMessage(string userName, string message);
    Task ReceiveMessage(string userName, string message, string sentiment);
}

public class UserMessage
{
    public string UserName { get; set; }
    public string Text { get; set; }
    public SentimentAnalysisResult Sentiment { get; set; }

    public UserMessage() { }

    public UserMessage(string userName, string text, SentimentAnalysisResult sentiment)
    {
        UserName = userName;
        Text = text;
        Sentiment = sentiment;
    }
}