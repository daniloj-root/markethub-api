using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using MarketHub.Models;
using MarketHub.ViewModels;
using tioLogReplay;

namespace MarketHub.Libraries
{
    public class TioDbSet : List<Container>
    {
        public TioConnection Tio { get; set; }

        public string Container { get; set; }
        public int Handle { get; set; }

        public new Container this[int index]
        {
            get
            {
                var line = $"get {this.Handle} key int {index}";
                var answer = Tio.SendCommand(line);

                // format is "data key {key} value {value} metadata {metadata}" 
                var results = answer.Split(' ');
                var value = results[2];
                var metadata = results[4];

                return new Container()
                {
                    Name = this.Container,
                    Value = value,
                    Metadata = metadata
                };
            }
        }

        private new int Count
        {
            get
            {
                var line = $"get_count {Handle}";
                var answer = Tio.SendCommand(line);
                var count = int.Parse(answer);
                return count;
            }
        }

        public TioDbSet(string container)
        {
            Tio = new TioConnection("localhost:2605");
            this.Container = container;
            this.Handle = GetHandle();
        }

        public int GetHandle()
        {
            var line = $"open {Container}";

            var answer = Tio.SendCommand(line);
            if (answer.Contains("error"))
                throw new KeyNotFoundException();

            var handle = int.Parse(answer);

            return handle;
        }

        public Container GetLast()
        {
            return this[Count - 1];
        }

        public IEnumerable<Container> GetLast(int count)
        {
            var length = Count;
            for (var i = 1; i <= count; i++)
            {
                yield return this[length - 1];
            } 
        }
    }
}
