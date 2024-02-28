using System.Net.Http.Headers;

namespace AmindaFeed.Services
{
    public class HttpService : IHttpService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly string _baseUrl;

        public HttpService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _baseUrl = _configuration.GetValue<string>("Matterhorn:BaseUrl") ?? throw new InvalidOperationException();
        }

        public HttpClient CreateConfiguredHttpClient()
        {
            var httpClient = _httpClientFactory.CreateClient();

            httpClient.BaseAddress = new Uri(_baseUrl);

            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(_configuration.GetValue<string>("Matterhorn:ApiKey"));

            return httpClient;
        }

        public string CombineUrl(string endpoint)
        {
            // Combine base URL and endpoint
            return $"{_baseUrl.TrimEnd('/')}/{endpoint.TrimStart('/')}";
        }
    }
}
