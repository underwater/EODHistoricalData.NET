﻿using System;
using System.Net.Http;
using System.Text;

namespace PortfolioValue.EODHistorical
{
    internal class CalendarDataClient : HttpApiClient
    {
        internal CalendarDataClient(string apiToken, bool useProxy) : base(apiToken, useProxy) { }

        private const string EarningsUrl = @"https://eodhistoricaldata.com/api/calendar/earnings?api_token={0}&fmt=json{1}";
        private const string IposUrl = @"https://eodhistoricaldata.com/api/calendar/ipos?api_token={0}&fmt=json{1}";
        private const string SplitsUrl = @"https://eodhistoricaldata.com/api/calendar/splits?api_token={0}&fmt=json{1}";

        private static StringBuilder HandleParameters(DateTime? startDate, DateTime? endDate, string[] symbols)
        {
            var sb = new StringBuilder();
            if (startDate != null || endDate != null)
                sb.Append(Utils.GetDateParametersAsString(startDate, endDate, "&"));
            if (symbols != null && symbols.Length > 0)
                sb.Append($"&symbols={string.Join(",", symbols)}");
            return sb;
        }

        internal Earnings GetEarnings(DateTime? startDate = null, DateTime? endDate = null, string[] symbols = null)
        {
            var sb = HandleParameters(startDate, endDate, symbols);
            return ExecuteQuery(string.Format(EarningsUrl, _apiToken, sb), GetEarningsFromResponse);
        }

        private Earnings GetEarningsFromResponse(HttpResponseMessage response)
        {
            return Earnings.FromJson(response.Content.ReadAsStringAsync().Result);
        }

        internal Ipos GetIpos(DateTime? startDate = null, DateTime? endDate = null, string[] symbols = null)
        {
            var sb = HandleParameters(startDate, endDate, symbols);
            return ExecuteQuery(string.Format(IposUrl, _apiToken, sb), GetIposFromResponse);
        }

        private Ipos GetIposFromResponse(HttpResponseMessage response)
        {
            return Ipos.FromJson(response.Content.ReadAsStringAsync().Result);
        }

        internal IncomingSplits GetIncomingSplits(DateTime? startDate = null, DateTime? endDate = null, string[] symbols = null)
        {
            var sb = HandleParameters(startDate, endDate, symbols);
            return ExecuteQuery(string.Format(SplitsUrl, _apiToken, sb), GetIncomingSplitsFromResponse);
        }

        private IncomingSplits GetIncomingSplitsFromResponse(HttpResponseMessage response)
        {
            return IncomingSplits.FromJson(response.Content.ReadAsStringAsync().Result);
        }
    }
}
