using MarketHub.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketHub.Models
{
    public class Order
    {
        public string Courier { get; set; }
        public List<TradeViewModel> StockStats {get; set;} 
    }
}
