using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EODHistoricalData.NET.Tests
{
    [TestClass]
    public class FundamentalDataTests
    {
        [TestMethod]
        public void fundamental_stock_returns_data()
        {
            using var client = new EODHistoricalDataAsyncClient(Consts.ApiToken, true);
            var fundamental = client.GetFundamentalStockAsync(Consts.TestSymbol);
            Assert.IsNotNull(fundamental);
        }
        
        [TestMethod]
        public void fundamental_etf_returns_data()
        {
            using var client = new EODHistoricalDataAsyncClient(Consts.ApiToken, true);
            var fundamental = client.GetFundamentalETFAsync(Consts.TestETF);
            Assert.IsNotNull(fundamental);
        }

        [TestMethod]
        public void fundamental_fund_returns_data()
        {
            using var client = new EODHistoricalDataAsyncClient(Consts.ApiToken, true);
            var fundamental = client.GetFundamentalFundAsync(Consts.TestFund);
            Assert.IsNotNull(fundamental);
        }

        [TestMethod]
        public void index_composition_returns_data()
        {
            using var client = new EODHistoricalDataAsyncClient(Consts.ApiToken, true);
            var index = client.GetIndexCompositionAsync(Consts.TestIndex);
            Assert.IsNotNull(index);
        }
        
        [TestMethod]
        public void exchange_instruments_returns_data()
        {
            using var client = new EODHistoricalDataAsyncClient(Consts.ApiToken, true);
            var instruments = client.GetExchangeInstrumentsAsync(Consts.Exchange);
            Assert.IsNotNull(instruments);
            //Assert.IsNotNull(instruments.Count > 1000);
        }
    }
}
