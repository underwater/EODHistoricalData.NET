﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using EODHistoricalData.NET;
//
//    var options = Options.FromJson(jsonString);

namespace PortfolioValue.EODHistorical
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Options
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("exchange")]
        public string Exchange { get; set; }
        
        [JsonProperty("lastTradeDate")]
        public DateTime? LastTradeDate { get; set; }
        
        [JsonProperty("lastTradePrice")]
        public decimal? LastTradePrice { get; set; }

        [JsonProperty("data")]
        public List<Datum> Data { get; set; }
    }

    public partial class Datum
    {
        [JsonProperty("expirationDate")]
        public DateTimeOffset? ExpirationDate { get; set; }

        [JsonProperty("options")]
        public OptionsClass Options { get; set; }
    }

    public partial class OptionsClass
    {
        [JsonProperty("CALL")]
        public List<Characteristics> Call { get; set; }

        [JsonProperty("PUT")]
        public List<Characteristics> Put { get; set; }
    }

    public partial class Characteristics
    {
        [JsonProperty("contractName")]
        public string ContractName { get; set; }

        [JsonProperty("contractSize")]
        public ContractSize ContractSize { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("type")]
        public OptionsTypeEnum OptionType { get; set; }

        [JsonProperty("inTheMoney")]
        public bool InTheMoney { get; set; }

        [JsonProperty("lastTradeDateTime")]
        public string LastTradeDateTimeString { get; set; }

        [JsonProperty("expirationDate")]
        public DateTimeOffset? ExpirationDate { get; set; }

        [JsonProperty("strike")]
        public decimal Strike { get; set; }

        [JsonProperty("lastPrice")]
        public decimal? LastPrice { get; set; }

        [JsonProperty("bid")]
        public decimal? Bid { get; set; }

        [JsonProperty("ask")]
        public decimal? Ask { get; set; }

        [JsonProperty("change")]
        public decimal? Change { get; set; }

        [JsonProperty("changePercent")]
        public decimal? ChangePercent { get; set; }

        [JsonProperty("volume")]
        public long? Volume { get; set; }

        [JsonProperty("openInterest")]
        public long? OpenInterest { get; set; }

        [JsonProperty("impliedVolatility")]
        public decimal? ImpliedVolatility { get; set; }

        [JsonProperty("delta")]
        public decimal Delta { get; set; }

        [JsonProperty("gamma")]
        public decimal Gamma { get; set; }

        [JsonProperty("theta")]
        public decimal Theta { get; set; }

        [JsonProperty("vega")]
        public decimal Vega { get; set; }

        [JsonProperty("rho")]
        public decimal Rho { get; set; }

        [JsonProperty("theoretical")]
        public decimal Theoretical { get; set; }

        [JsonProperty("intrinsicValue")]
        public decimal IntrinsicValue { get; set; }

        [JsonProperty("timeValue")]
        public decimal TimeValue { get; set; }

        [JsonProperty("updatedAt")]
        public DateTimeOffset UpdatedAt { get; set; }

        [JsonIgnore]
        public DateTime? LastTradeDateTime { get; set; }
    }

    public enum ContractSize { Empty, Regular };
    
    public enum OptionsTypeEnum { Call, Put };
    
    public partial class Options
    {
        static void SetLastTradeDatime(List<Characteristics> options)
        {
            if (options != null)
            {
                foreach (Characteristics charac in options)
                {
                    if (!charac.LastTradeDateTimeString.StartsWith("0000"))
                        charac.LastTradeDateTime = DateTime.Parse(charac.LastTradeDateTimeString, CultureInfo.InvariantCulture);
                }
            }
        }

        public static Options FromJson(string json)
        {
            Options result = JsonConvert.DeserializeObject<Options>(json, PortfolioValue.EODHistorical.Converter.Settings);
            foreach (Datum datum in result.Data)
            {
                SetLastTradeDatime(datum.Options.Call);
                SetLastTradeDatime(datum.Options.Put);
            }
            return result;
        }
    }

    public static class Serialize
    {
        public static string ToJson(this Options self) => JsonConvert.SerializeObject(self, PortfolioValue.EODHistorical.Converter.Settings);
    }

    internal static class Converter
    {
        public static List<string> Errors = new List<string>();
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                ContractSizeConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal },
                new NullConverter(),
            },
            Error = delegate (object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
            {
                Errors.Add(args.ErrorContext.Error.Message);
                args.ErrorContext.Handled = true;
            },
        };
    }

    internal class ContractSizeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ContractSize) || t == typeof(ContractSize?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "":
                    return ContractSize.Empty;
                case "REGULAR":
                    return ContractSize.Regular;
            }
            throw new Exception("Cannot unmarshal type ContractSize");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ContractSize)untypedValue;
            switch (value)
            {
                case ContractSize.Empty:
                    serializer.Serialize(writer, "");
                    return;
                case ContractSize.Regular:
                    serializer.Serialize(writer, "REGULAR");
                    return;
            }
            throw new Exception("Cannot marshal type ContractSize");
        }

        public static readonly ContractSizeConverter Singleton = new ContractSizeConverter();
    }

    internal class OptionsTypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(OptionsTypeEnum) || t == typeof(OptionsTypeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "CALL":
                    return OptionsTypeEnum.Call;
                case "PUT":
                    return OptionsTypeEnum.Put;
            }
            throw new Exception("Cannot unmarshal type TypeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (OptionsTypeEnum)untypedValue;
            switch (value)
            {
                case OptionsTypeEnum.Call:
                    serializer.Serialize(writer, "CALL");
                    return;
                case OptionsTypeEnum.Put:
                    serializer.Serialize(writer, "PUT");
                    return;
            }
            throw new Exception("Cannot marshal type TypeEnum");
        }

        public static readonly OptionsTypeEnumConverter Singleton = new OptionsTypeEnumConverter();
    }
}
