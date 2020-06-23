using MarketHub.Enums;
using MarketHub.ViewModels;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MarketHub.Models
{
    public class Trade : Asset
    {
        public string Broker { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TradeType Type { get; set; }

        public static Trade ToTrade(Item item)
        {
            var stock = item.Value.Split('_');
            return new Trade()
            {
                Type = stock[0] == "S" ? TradeType.Sell : TradeType.Buy, 
                Name = stock[1],
                Price = int.Parse(stock[2]),
                Broker = stock[3]
            };
        }
    }
}
