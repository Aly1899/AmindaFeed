namespace AmindaFeed.Services
{
    public interface IThirdPartyProduct
    {
        public Task<AmindaProduct> ProductMappedToAminda(int productID);

    }
}
