using System.Text.Json.Serialization;

namespace AmindaFeed.Models
{
    public class MatterhornProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonPropertyName("creation_date")]
        public string CreationDate { get; set; }
        public string Color { get; set; }
        [JsonPropertyName("category_name")]
        public string CategoryName { get; set; }
        [JsonPropertyName("category_path")]
        public string CategoryPath { get; set; }
        [JsonPropertyName("brand_id")]
        public int BrandId { get; set; }
        public string Brand { get; set; }
        [JsonPropertyName("stock_total")]
        public int StockTotal { get; set; }
        [JsonPropertyName("price_net")]
        public double PriceNet { get; set; }
        public string Url { get; set; }
        public List<string> Images { get; set; }
        [JsonPropertyName("new_collection")]
        public string NewCollection { get; set; }
        public List<Variant> Variants { get; set; }
        [JsonPropertyName("other_colors")]
        public List<int> OtherColors { get; set; }
        [JsonPropertyName("prices")]
        public Dictionary<string, double> Prices { get; set; }

        [JsonPropertyName("size_table_txt")]
        public string SizeTableTxt { get; set; }
        [JsonPropertyName("size_table_html")]
        public string SizeTableHtml { get; set; }
    }
}