using System;
using System.Net.Sockets;
using System.Net;
using ADAutenticacion;

namespace Servidor
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener serverSocket = new TcpListener(IPAddress.Any, 11000);
            TcpClient clientSocket = null;

            serverSocket.Start();
            Autenticacion.Init();
            Sala.Init();

            Console.WriteLine("Servidor iniciado!\n");

            while (true)
            {
                Console.WriteLine("Escuchando en puerto: 11000!\n");
                clientSocket = serverSocket.AcceptTcpClient(); // Aceptar el Jugador entrante
                Cliente cliente = new Cliente(clientSocket); // Crear un nuevo objeto Cliente el cual dirigira la logica de juego del jugador
            }
        }
    }
}
