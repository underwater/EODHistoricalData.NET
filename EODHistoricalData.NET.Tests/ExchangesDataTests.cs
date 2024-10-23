using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace PortfolioValue.EODHistorical.Tests
{
    [TestClass]
    public class ExchangesDataTests
    {
        [TestMethod]
        public async Task exchange_list_returns_data()
        {
            using var client = new EODHistoricalDataAsyncClient(Consts.ApiToken, true);
            var exchanges = await client.GetExchangeListAsync();
            Assert.IsNotNull(exchanges);
            Assert.IsNotNull(exchanges.Count > 50);
        }
    }
}
