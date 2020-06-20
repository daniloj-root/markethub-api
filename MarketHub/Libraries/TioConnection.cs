using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace tioLogReplay
{
    public class TioConnection
    {
        public const string TIO_DEFAULT_ADDRESS = "localhost";
        private const int TIO_DEFAULT_PORT = 2605;

        private TcpClient Client { get; set; }
        private NetworkStream Stream { get; set; }

        public TioConnection(string address)
        {
            string hostname;
            int port;

            // if input address has only digits, set socket address as localhost:digits
            if (Regex.IsMatch(address, "^[0-9]*$"))
            {
                hostname = TIO_DEFAULT_ADDRESS;
                port = int.Parse(address);
                this.Client = new TcpClient(hostname, port);
                this.Stream = Client.GetStream();
            }

            // if input address doesn't have a port, use tio default port
            if (!address.Contains(':'))
            {
                hostname = address;
                port = TIO_DEFAULT_PORT;
                this.Client = new TcpClient(hostname, port);
                this.Stream = Client.GetStream();
            }

            // if input address an address and a port, split them
            var socket = address.Split(':');
            hostname = socket[0];
            port = int.Parse(socket[1]);
            this.Client = new TcpClient(hostname, port);
            this.Stream = Client.GetStream();
        }

        // remove "answer ok " from response
        public string ParseAnswer(string response)
        {
            var arrRest= response.Skip(10);
            return string.Join(',', arrRest).Trim(',');
        }
        
        // this is a generic method to send commands to tio
        public string SendCommand(string line)
        {
            var data = Encoding.ASCII.GetBytes(line);
            Stream.Write(data, 0, data.Length);

            var bytes = new byte[128];
            var length = Stream.Read(bytes, 0, bytes.Length);
            return ParseAnswer(Encoding.ASCII.GetString(bytes, 0, length));
        }
    }
}

