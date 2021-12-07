﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EODHistoricalData.NET
{
    public class EODHistoricalDataClient : AuthentifiedClient
    {
        public const string DateFormat = "yyyy-MM-dd";

        public EODHistoricalDataClient(string api, bool useProxy = false) : base(api)
        {
            _useProxy = useProxy;
        }

        StockPriceDataClient _stockPriceDataClient;
        SplitDividendClient _splitDividendClient;
        OptionsDataClient _optionsClient;
        CalendarDataClient _calenderClient;
        FundamentalDataClient _fundamentalDataClient;
        ExchangesDataClient _exchangesDataClient;

        bool _useProxy = false;

        public List<HistoricalPrice> GetHistoricalPrices(string symbol, DateTime? startDate, DateTime? endDate)
        {
            if (symbol == null)
                throw new ArgumentNullException("Symbol is null, cannot query prices.");

            if (_stockPriceDataClient == null)
                _stockPriceDataClient = new StockPriceDataClient(_apiToken, _useProxy);

            return _stockPriceDataClient.GetHistoricalPrices(symbol, startDate, endDate);
        }

        public RealTimePrice GetRealTimePrice(string symbol)
        {
            if (symbol == null)
                throw new ArgumentNullException("Symbols list is null. Cannot get realtime prices.");

            if (_stockPriceDataClient == null)
                _stockPriceDataClient = new StockPriceDataClient(_apiToken, _useProxy);

            return _stockPriceDataClient.GetRealTimePrice(symbol);
        }

        public List<RealTimePrice> GetRealTimePrices(string[] symbols)
        {
            if (symbols == null)
                throw new ArgumentNullException("Symbols list is null. Cannot get realtime prices.");

            if (symbols.Length == 0)
                throw new ArgumentNullException("Symbols list is empty. Cannot get realtime prices.");

            if (symbols.Any(x => x == null))
                throw new ArgumentNullException("Symbols list contains null elements. Cannot get realtime prices.");

            if (_stockPriceDataClient == null)
                _stockPriceDataClient = new StockPriceDataClient(_apiToken, _useProxy);

            return _stockPriceDataClient.GetRealTimePrices(symbols);
        }

        public List<Dividend> GetDividends(string symbol, DateTime? startDate, DateTime? endDate)
        {
            if (symbol == null)
                throw new ArgumentNullException("Symbol is null, cannot query dividends.");

            if (_splitDividendClient == null)
                _splitDividendClient = new SplitDividendClient(_apiToken, _useProxy);

            return _splitDividendClient.GetDividends(symbol, startDate, endDate);
        }

        public List<ShareSplit> GetShareSplits(string symbol, DateTime? startDate, DateTime? endDate)
        {
            if (symbol == null)
                throw new ArgumentNullException("Symbol is null, cannot query splits.");

            if (_splitDividendClient == null)
                _splitDividendClient = new SplitDividendClient(_apiToken, _useProxy);

            return _splitDividendClient.GetShareSplits(symbol, startDate, endDate);
        }


        public Options GetOptions(string symbol, DateTime? startDate, DateTime? endDate, DateTime? startTradeDate = null, DateTime? endTradeDate = null)
        {
            if (symbol == null)
                throw new ArgumentNullException("Symbol is null, cannot query options data.");

            if (_optionsClient == null)
                _optionsClient = new OptionsDataClient(_apiToken, _useProxy);

            return _optionsClient.GetOptions(symbol, startDate, endDate);
        }

        public Earnings GetEarnings(DateTime? startDate = null, DateTime? endDate = null, string[] symbols = null)
        {
            if (_calenderClient == null)
                _calenderClient = new CalendarDataClient(_apiToken, _useProxy);

            return _calenderClient.GetEarnings(startDate, endDate, symbols);
        }

        public Ipos GetIpos(DateTime? startDate = null, DateTime? endDate = null, string[] symbols = null)
        {
            if (_calenderClient == null)
                _calenderClient = new CalendarDataClient(_apiToken, _useProxy);

            return _calenderClient.GetIpos(startDate, endDate, symbols);
        }

        public IncomingSplits GetIncomingSplits(DateTime? startDate = null, DateTime? endDate = null, string[] symbols = null)
        {
            if (_calenderClient == null)
                _calenderClient = new CalendarDataClient(_apiToken, _useProxy);

            return _calenderClient.GetIncomingSplits(startDate, endDate, symbols);
        }

        public FundamentalStock GetFundamentalStock(string symbol)
        {
            return GetFundamentalStock((new[] { symbol }).ToList()).FirstOrDefault();
        }

        public IList<FundamentalStock> GetFundamentalStock(IList<string> symbols)
        {
            if (_fundamentalDataClient == null)
                _fundamentalDataClient = new FundamentalDataClient(_apiToken, _useProxy);

            return symbols.Select(x => _fundamentalDataClient.GetFundamentalStock(x)).ToList();
        }

        /// <summary>
        /// To get an access to bulk fundamentals API,
        /// you should subscribe to ‘Extended Fundamentals’ package,
        /// more details we will provide by request: support@eodhistoricaldata.com.
        /// 
        /// It doesn’t work for entire US exchange,
        /// instead of it you should request each exchange separately,
        /// at the moment we do support:
        /// NASDAQ, NYSE (or ‘NYSE MKT’), BATS, and AMEX.
        /// All non-US exchanges supported as is.
        ///
        /// By default offset = 0 and limit = 1000
        /// </summary>
        /// <param name="exchange">The exchange code</param>
        /// <param name="offset">The number of records to skip</param>
        /// <param name="limit">If the ‘limit’ parameter is bigger than 1000, it will be reset to 1000.</param>
        /// <returns></returns>
        public BulkFundamentalStocks GetBulkFundamentalStocks(string exchange, int offset = 0, int limit = 1000)
        {
            if (_fundamentalDataClient == null)
                _fundamentalDataClient = new FundamentalDataClient(_apiToken, _useProxy);
            return _fundamentalDataClient.GetBulkFundamentalsStocks(exchange, offset, limit);
        }

        public FundamentalFund GetFundamentalFund(string symbol)
        {
            if (_fundamentalDataClient == null)
                _fundamentalDataClient = new FundamentalDataClient(_apiToken, _useProxy);

            return _fundamentalDataClient.GetFundamentalFund(symbol);
        }
        public FundamentalETF GetFundamentalETF(string symbol)
        {
            if (_fundamentalDataClient == null)
                _fundamentalDataClient = new FundamentalDataClient(_apiToken, _useProxy);

            return _fundamentalDataClient.GetFundamentalETF(symbol);
        }

        public IndexComposition GetIndexComposition(string symbol)
        {
            if (_fundamentalDataClient == null)
                _fundamentalDataClient = new FundamentalDataClient(_apiToken, _useProxy);

            return _fundamentalDataClient.GetIndexComposition(symbol);
        }

        public List<Instrument> GetExchangeInstruments(string exchangeCode)
        {
            if (_fundamentalDataClient == null)
                _fundamentalDataClient = new FundamentalDataClient(_apiToken, _useProxy);

            return _fundamentalDataClient.GetExchangeInstruments(exchangeCode);
        }
        
        public List<Exchange> GetExchangeList()
        {
            if (_exchangesDataClient == null)
                _exchangesDataClient = new ExchangesDataClient(_apiToken, _useProxy);

            return _exchangesDataClient.GetExchanges();
        }
    }
}
