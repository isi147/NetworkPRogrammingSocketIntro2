// See https://aka.ms/new-console-template for more information
using System.Net.Sockets;
using System.Net;
using System.Text;

var client = new Socket(AddressFamily.InterNetwork,
                         SocketType.Stream,
                         ProtocolType.Tcp);


var ip = IPAddress.Parse("192.168.56.1");
var port = 45678;
var serverEP = new IPEndPoint(ip, port);


try
{
    client.Connect(serverEP);

    if (client.Connected)
    {
        Console.WriteLine("Connected to the server...");

        Task.Run(() =>
        {
            var buffer = new byte[1024];
            var len = 0;
            var msg = string.Empty;

            while (true)
            {
                len = client.Receive(buffer);
                msg = Encoding.Default.GetString(buffer, 0, len);
                Console.WriteLine(msg);
            }

        });


        while (true)
        {
            var msg = Console.ReadLine();
            var buffer = Encoding.Default.GetBytes(msg);
            client.Send(buffer);
        }
    }
    else
    {
        Console.WriteLine("Can't connect to the server...");
    }
}
catch (Exception ex)
{
    Console.WriteLine("EX: Can't connect to the server...");
    Console.WriteLine(ex.Message);
}