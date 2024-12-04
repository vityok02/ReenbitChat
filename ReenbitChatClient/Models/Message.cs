namespace ReenbitChatClient.Models;

public class Message
{
    public string UserName { get; set; }
    public string Text { get; set; }
    public SentimentAnalysisResult Sentiment { get; set; }

    public Message() { }

    public Message(string userName, string text)
    {
        UserName = userName;
        Text = text;
    }
}