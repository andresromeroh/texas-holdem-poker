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
        static string name;

        public static void Init(string servidor, Int32 puerto, string username)
        {
            server.Connect(IPAddress.Parse(servidor), puerto);
            writer = new StreamWriter(server.GetStream());
            writer.AutoFlush = true;
            reader = new StreamReader(server.GetStream());
            name = username;
        }

        public static void Write(string json)
        {
            writer.WriteLine(json);
        }

        public static string Read()
        {
            return (reader.ReadLine());
        }

        public static string Name()
        {
            return name;
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
