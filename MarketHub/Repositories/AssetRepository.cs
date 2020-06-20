using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using MarketHub.Libraries;
using MarketHub.ViewModels;

namespace MarketHub.Repositories
{
    public class AssetRepository
    {
        private TioDbSet tioDbSet { get; set; }

        public AssetRepository(string container)
        {
            tioDbSet = new TioDbSet(container);
        }

        public Asset GetLast()
        {
            var container = tioDbSet[tioDbSet.Count - 1];

            return new Asset()
            {
                Name = container.Name,
                Price = int.Parse(container.Value)
            };
        }

        public IEnumerable<Asset> GetLast(int count)
        {
            var containers = tioDbSet.GetLast(count);

            foreach (var container in containers)
            {
                yield return new Asset()
                {
                    Name = container.Name,
                    Price = int.Parse(container.Value)
                };
            }
        }
    }
}