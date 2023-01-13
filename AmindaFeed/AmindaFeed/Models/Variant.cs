using System.Text.Json.Serialization;

public class Variant
{
    [JsonPropertyName("variant_uid")]
    public int VariantUid { get; set; }
    public string Name { get; set; }
    public int Stock { get; set; }
    [JsonPropertyName("max_processing_time")]
    public int MaxProcessingTime { get; set; }
}