using Cliente.Models;
using Servidor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Servidor
{
    class Program
    {
        static void Main(string[] args)
        {
            TCPListenerService server = new TCPListenerService();
            server.startService();
        }
    }
}
