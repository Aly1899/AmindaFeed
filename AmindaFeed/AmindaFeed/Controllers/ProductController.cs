using AmindaFeed.Models;
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
        private readonly IHttpClientFactory _httpClientFactory;


        public ProductController(
            ILogger<ProductController> logger,
            IHttpService httpService,
            IHttpClientFactory httpClientFactory, IMatterhornAdapter matterhornAdapter)
        {
            _logger = logger;
            _httpService = httpService;
            _httpClientFactory = httpClientFactory;
            _matterhornAdapter = matterhornAdapter;
        }

        [HttpGet("GetMatterhornProduct")]
        public async Task<MatterhornProduct> GetAmindaProduct(string productId)
        {
            return await _matterhornAdapter.GetMatterhornProduct(productId);

        }

        [HttpPut("GetMatterhornProducts")]
        public async Task<List<MatterhornProduct>> GetAmindaProducts([FromBody] List<string> productIds)
        {
            return await _matterhornAdapter.GetMatterhornProducts(productIds);

        }

        [HttpPost("SetAmindaProductFromMatterhorn")]
        public async Task SetAmindaProductFromMatterhorn(string productId)
        {
            await _httpService.SetAmindaProductFromMatterhorn(productId);
        }

        [HttpPost("SetAmindaProductsFromMatterhorn")]
        public void SetAmindaProductsFromMatterhorn([FromBody] List<string> productIds)
        {
            _httpService.SetAmindaProductsFromMatterhorn(productIds);
        }

    }
}