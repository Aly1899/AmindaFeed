using System.Text.Json.Serialization;

namespace AmindaFeed.Models
{
    public class MatterhornCategory
    {
        [JsonPropertyName("category_id")]
        public int Id { get; set; }

        [JsonPropertyName("category_name")]
        public string Name { get; set; }
    }
}
