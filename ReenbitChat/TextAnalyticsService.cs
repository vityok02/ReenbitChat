using Azure;
using Azure.AI.TextAnalytics;

namespace ReenbitChat;

public class TextAnalyticsService
{
    private readonly TextAnalyticsClient _client;

    public TextAnalyticsService(IConfiguration configuration)
    {
        var endpoint = configuration["AzureTextAnalytics:Endpoint"];
        var key = configuration["AzureTextAnalytics:Key"];
        var credentials = new AzureKeyCredential(key);
        _client = new TextAnalyticsClient(new Uri(endpoint), credentials);
    }

    public async Task<SentimentAnalysisResult> AnalyzeSentimentAsync(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return new SentimentAnalysisResult
            {
                Sentiment = "Neutral",
                PositiveScore = 0,
                NeutralScore = 1,
                NegativeScore = 0
            };
        }

        var response = await _client.AnalyzeSentimentAsync(text);
        var documentSentiment = response.Value;

        return new SentimentAnalysisResult
        {
            Sentiment = documentSentiment.Sentiment.ToString(),
            PositiveScore = documentSentiment.ConfidenceScores.Positive,
            NeutralScore = documentSentiment.ConfidenceScores.Neutral,
            NegativeScore = documentSentiment.ConfidenceScores.Negative
        };
    }
}

public class SentimentAnalysisResult
{
    public string Sentiment { get; set; } = default!;
    public double PositiveScore { get; set; }
    public double NeutralScore { get; set; }
    public double NegativeScore { get; set; }
}