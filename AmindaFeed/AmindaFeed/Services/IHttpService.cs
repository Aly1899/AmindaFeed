using AmindaFeed.Models;

namespace AmindaFeed.Services
{
    public interface IHttpService
    {
        Task<MatterhornProduct> GetMatterhornProduct(string productId);
        Task<List<MatterhornProduct>> GetMatterhornProducts(List<string> productIds);
        Task SetAmindaProductFromMatterhorn(string productId);
    }
}
