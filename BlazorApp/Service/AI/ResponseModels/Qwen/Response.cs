using System.Text.Json.Serialization;

namespace BlazorApp.Service.AI.ResponseModels.Qwen;

public class Output
{
    [JsonPropertyName("finish_reason")]
    public string FinishReason { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; }
}

public class Usage
{
    [JsonPropertyName("total_tokens")]
    public int TotalTokens { get; set; }

    [JsonPropertyName("input_tokens")]
    public int InputTokens { get; set; }

    [JsonPropertyName("output_tokens")]
    public int OutputTokens { get; set; }
}

public class Response
{
    [JsonPropertyName("output")]
    public Output Output { get; set; }

    [JsonPropertyName("usage")]
    public Usage Usage { get; set; }

    [JsonPropertyName("request_id")]
    public string RequestId { get; set; }
}
