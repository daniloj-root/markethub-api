using MarketHub.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketHub.ViewModels
{
    public class TradeViewModel : Asset
    {
        // inherits name and price from Asset
        public int Quantity { get; set; }
    }
}
