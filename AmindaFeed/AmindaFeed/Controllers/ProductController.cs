using AmindaFeed.Data;
using AmindaFeed.Models;
using AmindaFeed.Repository;
using AmindaFeed.Services;
using Microsoft.AspNetCore.Mvc;

namespace AmindaFeed.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IHttpService _httpService;
        private readonly IMatterhornAdapter _matterhornAdapter;
        private readonly Repository.IProductRepository<AmindaProductDb> _productRepository;
        private readonly IHttpClientFactory _httpClientFactory;


        public ProductController(
            ILogger<ProductController> logger,
            IHttpService httpService,
            IHttpClientFactory httpClientFactory, 
            IMatterhornAdapter matterhornAdapter,
            IProductRepository<AmindaProductDb> productRepository)
        {
            _logger = logger;
            _httpService = httpService;
            _httpClientFactory = httpClientFactory;
            _matterhornAdapter = matterhornAdapter;
            _productRepository = productRepository;
        }

        [HttpGet("GetMatterhornProduct")]
        public async Task<MatterhornProduct> GetAmindaProduct(int productId)
        {
            return await _matterhornAdapter.GetMatterhornProduct(productId);

        }

        [HttpPut("GetMatterhornProducts")]
        public async Task<List<MatterhornProduct>> GetAmindaProducts([FromBody] List<int> productIds)
        {
            return await _matterhornAdapter.GetMatterhornProducts(productIds);

        }

        [HttpGet("GetMatterhornProductsByCategory")]
        public async Task<List<MatterhornProduct>> GetAmindaProductsByCategory(int category)
        {
            return await _matterhornAdapter.GetMatterhornProductsByCategory(category);

        }

        [HttpPost("SetAmindaProductFromMatterhorn")]
        public async Task SetAmindaProductFromMatterhorn(int productId)
        {
            await _matterhornAdapter.SetAmindaProductFromMatterhorn(productId);
        }

        [HttpPost("SetAmindaProductsFromMatterhorn")]
        public async Task SetAmindaProductsFromMatterhorn([FromBody] List<int> productIds)
        {
            await _matterhornAdapter.SetAmindaProductsFromMatterhorn(productIds);
        }

        [HttpGet("GetAllAmindaProducts")]
        public async Task<ActionResult<IEnumerable<AmindaProductDb>>> GetAllAmindaProducts()
        {
            return await _productRepository.GetAllAsync();
        }

    }
}