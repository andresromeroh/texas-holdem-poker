using Cliente.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Tests
{
    class Test
    {
        static void Main(string[] args)
        {
            Juego juego = new Juego();
            TCPClientService clientService = new TCPClientService();
            clientService.startService("3.14.121.156", 11000);
            string json = JsonConvert.SerializeObject(juego);
            Console.WriteLine("OBJETO JUEGO:");
            Console.WriteLine(json);
            clientService.sendMessage(json);
        }
    }
}
