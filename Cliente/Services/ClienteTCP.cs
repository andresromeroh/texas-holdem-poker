using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Cliente.Services
{
    static class ClienteTCP
    {
        static TcpClient server = new TcpClient();
        static StreamWriter writer;
        static StreamReader reader;

        public static void Init(string servidor, Int32 puerto)
        {
            server.Connect(IPAddress.Parse(servidor), puerto);
            writer = new StreamWriter(server.GetStream());
            writer.AutoFlush = true;
            reader = new StreamReader(server.GetStream());
        }

        public static void Write(string json)
        {
            writer.WriteLine(json);
        }

        public static string Read()
        {
            return (reader.ReadLine());
        }

        public static void Disconnect()
        {
            server.Close();
            writer.Close();
            reader.Close();
            server = new TcpClient();
        }

    }
}
