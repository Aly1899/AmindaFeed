namespace AmindaFeed.Services
{
    public interface IThirdPartyProduct
    {
        public Task<AmindaProduct> ProductMappedToAminda(string productID);

    }
}
