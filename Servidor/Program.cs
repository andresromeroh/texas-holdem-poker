using Cliente.Models;
using Servidor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Net;

namespace Servidor
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener serverSocket = new TcpListener(IPAddress.Any, 11000);
            TcpClient clientSocket = null;

            serverSocket.Start();
            Console.WriteLine("Servidor iniciado...");

            Sala.Init();

            while (true)
            {
                clientSocket = serverSocket.AcceptTcpClient();
                //Client client = new Client(clientSocket);
            }
        }
    }
}
