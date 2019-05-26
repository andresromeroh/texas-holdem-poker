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
        //static string username;
        //static string password;
        //static int fichas;

        public static void Init(string servidor, Int32 puerto)
        {
            server.Connect(IPAddress.Parse("3.14.121.156"), 11000);
            writer = new StreamWriter(server.GetStream());
            writer.AutoFlush = true;
            reader = new StreamReader(server.GetStream());
        }

        //public static string Username
        //{
        //    get { return username; }
        //    set { username = value; }
        //}

        //public static int Fichas
        //{
        //    get { return fichas; }
        //    set { fichas = value; }
        //}

        public static void Write(string json)
        {
            writer.WriteLine(json);
        }

        public static string Read()
        {
            return (reader.ReadLine());
        }
    }
}
