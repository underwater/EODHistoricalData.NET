﻿namespace PortfolioValue.EODHistorical
{
    public abstract class AuthentifiedClient
    {
        protected readonly string _apiToken;

        protected AuthentifiedClient(string apiToken)
        {
            _apiToken = apiToken;
        }
    }
}
