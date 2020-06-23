using MarketHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MarketHub.ViewModels
{
    public class OrderBook
    { 
        public List<Order> Orders { get; set; }
    }
}
