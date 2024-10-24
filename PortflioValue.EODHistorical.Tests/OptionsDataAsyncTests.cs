﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace PortfolioValue.EODHistorical.Tests
{
    [TestClass]
    [Ignore("No License for this service")]
    public class OptionsDataAsyncTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task options_null_list_throws_exception_async()
        {
            using var client = new EODHistoricalDataAsyncClient(Consts.ApiToken);
            var options = await client.GetOptionsAsync(null, null, null);
        }

        [TestMethod]
        public async Task options_valid_symbols_returns_prices_async()
        {
            using var client = new EODHistoricalDataAsyncClient(Consts.ApiToken, true);
            var options = await client.GetOptionsAsync(Consts.TestSymbol, null, null);
            Assert.IsNotNull(options);
            Assert.IsTrue(options.Data.Count > 0);
        }

        [TestMethod]
        public async Task valid_symbol_split_with_from_date_returns_result_async()
        {
            using var client = new EODHistoricalDataAsyncClient(Consts.ApiToken, true);
            var options = await client.GetOptionsAsync(Consts.TestSymbol, Consts.OptionsStartDate, null);
            Assert.IsNotNull(options);
            Assert.IsTrue(options.Data.Count > 0);
        }

        [TestMethod]
        public async Task valid_symbol_split_with_to_date_returns_result_async()
        {
            using var client = new EODHistoricalDataAsyncClient(Consts.ApiToken, true);
            var options = await client.GetOptionsAsync(Consts.TestSymbol, null, Consts.OptionsEndDate);
            Assert.IsNotNull(options);
            Assert.IsTrue(options.Data.Count > 0);
        }

        [TestMethod]
        public async Task valid_symbol_split_with_from_and_to_date_returns_result_async()
        {
            using var client = new EODHistoricalDataAsyncClient(Consts.ApiToken, true);
            var options = await client.GetOptionsAsync(Consts.TestSymbol, Consts.OptionsStartDate, Consts.OptionsEndDate);
            Assert.IsNotNull(options);
            Assert.IsTrue(options.Data.Count > 0);
        }

        [TestMethod]
        public async Task valid_symbol_split_with_from_and_to_date_and_tradestartdate_returns_result_async()
        {
            using var client = new EODHistoricalDataAsyncClient(Consts.ApiToken, true);
            var options = await client.GetOptionsAsync(Consts.TestSymbol, Consts.OptionsStartDate, Consts.OptionsEndDate, Consts.OptionsTradeStartDate);
            Assert.IsNotNull(options);
            Assert.IsTrue(options.Data.Count > 0);
        }

        [TestMethod]
        public async Task valid_symbol_split_with_from_and_to_date_and_tradesenddate_returns_result_async()
        {
            using var client = new EODHistoricalDataAsyncClient(Consts.ApiToken, true);
            var options = await client.GetOptionsAsync(Consts.TestSymbol, Consts.OptionsStartDate, Consts.OptionsEndDate, null, Consts.OptionsTradeEndDate);
            Assert.IsNotNull(options);
            Assert.IsTrue(options.Data.Count > 0);
        }

        [TestMethod]
        public async Task valid_symbol_split_with_from_and_to_date_and_both_tradedate_returns_result_async()
        {
            using var client = new EODHistoricalDataAsyncClient(Consts.ApiToken, true);
            var options = await client.GetOptionsAsync(Consts.TestSymbol, Consts.OptionsStartDate, Consts.OptionsEndDate, Consts.OptionsTradeStartDate, Consts.OptionsTradeEndDate);
            Assert.IsNotNull(options);
            Assert.IsTrue(options.Data.Count > 0);
        }
    }
}
