using MarketHub.Models;

namespace MarketHub.ViewModels
{
    public class Asset 
    {
        public string Name { get; set; }
        public int Price { get; set; }

        public static Asset ToAsset(Item item)
        {
            var stock = item.Value.Split('_');
            return new Asset()
            {
                Name = stock[0],
                Price = int.Parse(stock[1])
            };
        }
    }
}
