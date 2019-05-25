using Cliente.Models;
using Servidor.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.Services
{
    public class TCPListenerService
    {
        TcpListener server = null;
        int connectedClients = 0;

        public void startService()
        {
            try
            {
                Juego juego = new Juego();

                // Escuchar en puerto 11000.
                Int32 port = 11000;
                server = new TcpListener(IPAddress.Any, port);

                // Comenzar a escuchar en el puerto indicado
                server.Start();

                // Entrar en el loop de "escucha"
                while (connectedClients < 2)
                {
                    Console.Write("Waiting for a connection... \n");
                    TcpClient client = server.AcceptTcpClient();
                    connectedClients++;

                    Console.WriteLine(" >> " + "Player No " + Convert.ToString(connectedClients) + " connected!");
                    TCPClientHandler clientHandler = new TCPClientHandler();
                    clientHandler.Juego = juego;
                    clientHandler.startClient(client, Convert.ToString(connectedClients));
                }

            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }


            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }
    }
    
}
