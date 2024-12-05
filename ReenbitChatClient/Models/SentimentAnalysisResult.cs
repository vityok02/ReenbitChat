namespace ReenbitChatClient.Models;

public class SentimentAnalysisResult
{
    public string Sentiment { get; set; } = default!;
    public double PositiveScore { get; set; }
    public double NeutralScore { get; set; }
    public double NegativeScore { get; set; }
}