using AmindaFeed.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Xml.Serialization;
using System.Xml;
using Formatting = System.Xml.Formatting;
using System.Text;
using System.Net.Http;
using System.Net;
using System;
using AmindaFeed.Services;

namespace AmindaFeed.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IHttpService _httpService;
        private readonly IHttpClientFactory _httpClientFactory;


        public ProductController(
            ILogger<ProductController> logger,
            IHttpService httpService,
            IHttpClientFactory httpClientFactory
            )
        {
            _logger = logger;
            _httpService = httpService;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet(Name = "GetMatterhornProduct")]
        public async Task<MatterhornProduct> GetAmindaProduct(string productId)
        {
            return await _httpService.GetMatterhornProduct(productId);
            
        }

        [HttpPost(Name = "SetAmindaProductFromMatterhorn")]
        public async Task SetAmindaProductFromMatterhorn(string productId)
        {
            _httpService.SetAmindaProductFromMatterhorn(productId);
        }

    }

    
    public class MySerializer<T> where T : class
    {
        public static string Serialize(T obj)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(T));
            using (var sww = new StringWriter())
            {
                using (XmlTextWriter writer = new XmlTextWriter(sww) { Formatting = Formatting.Indented })
                {
                    xsSubmit.Serialize(writer, obj);
                    return sww.ToString();
                }
            }
        }
    }
}