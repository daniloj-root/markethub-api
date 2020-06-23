using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using MarketHub.Libraries;
using MarketHub.Models;
using MarketHub.ViewModels;

namespace MarketHub.Repositories
{
    public class TradesRepository
    {
        private TioDbSet tioDbSet { get; set; }

        public TradesRepository()
        {
            tioDbSet = new TioDbSet("trades");
        }

        public Asset GetLastTrade()
        {
            var item = tioDbSet.GetLast();
            return Asset.ToAsset(item);
        }

        public Asset GetLastTradeByStock(string stock)
        {
            var count = tioDbSet.Count - 1;

            for (var i = count; i >= 0; i--)
            {
                var trade = Trade.ToTrade(tioDbSet[i]);

                if (trade.Name == stock)
                    return trade;
            }

            // if can't find the asset, return not found
            throw new KeyNotFoundException();
        }

        public IEnumerable<Asset> GetLastTrades(int count)
        {
            var items = tioDbSet.GetLastByCount(count);

            foreach (var item in items)
            {
                yield return Asset.ToAsset(item);
            }
        }

        // TODO
        public IEnumerable<OrderBook> GetOrderBook(int depth)
        {
            return new List<OrderBook>();
        }
    }
}