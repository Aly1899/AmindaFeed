namespace AmindaFeed.Services
{
    public interface IHttpService
    {
        public HttpClient CreateConfiguredHttpClient();
        public string CombineUrl(string endpoint);
    }
}
