using AmindaFeed.Models;

namespace AmindaFeed.Services
{
    public interface IMatterhornAdapter
    {
        Task<MatterhornProduct> GetMatterhornProduct(string productId);
        Task<List<MatterhornProduct>> GetMatterhornProducts(List<string> productIds);
        Task<List<MatterhornProduct>> GetMatterhornProductsByCategory(int category);
        Task SetAmindaProductFromMatterhorn(string productId);
        void SetAmindaProductsFromMatterhorn(List<string> productIds);
    }
}