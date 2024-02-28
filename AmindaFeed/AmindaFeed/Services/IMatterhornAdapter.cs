using AmindaFeed.Models;

namespace AmindaFeed.Services
{
    public interface IMatterhornAdapter
    {
        Task<MatterhornProduct> GetMatterhornProduct(int productId);
        Task<List<MatterhornProduct>> GetMatterhornProducts(List<int> productIds);
        Task<List<MatterhornProduct>> GetMatterhornProductsByCategory(int category);
        Task SetAmindaProductFromMatterhorn(int productId);
        void SetAmindaProductsFromMatterhorn(List<int> productIds);
    }
}