using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Servidor
{
    public class Cliente
    {
        public TcpClient Socket;
        public Thread Thread;
        public StreamReader Reader;
        public StreamWriter Writer;
        public Jugador Jugador = null;

        public Cliente(TcpClient Socket)
        {
            this.Socket = Socket;
            Reader = new StreamReader(this.Socket.GetStream()); //Obtener stream de lectura
            Writer = new StreamWriter(this.Socket.GetStream()); //Obtener stream de escritura
            Writer.AutoFlush = true;

            Console.WriteLine("Nuevo jugador conectado!\n");

            /* Inicia el hilo individual del jugador, para volver a escuchar en el puerto
             * por nuevos jugadores que desean unirse */
            Thread = new Thread(Login); 
            Thread.Start();
        }

        private void Login() //Aqui se implementa la autenticacion Active Directory
        {
            while (Jugador == null) //Mientras el jugador no haya sido definido
            {
                try
                {
                    string json = Reader.ReadLine(); // json enviado desde el cliente
                    Jugador deserializedJugador = JsonConvert.DeserializeObject<Jugador>(json); // Esperado: Un objeto jugador en formato JSON
                    Console.WriteLine("Nombre del jugador:" + deserializedJugador.NombreUsuario + "\n");

                    if (deserializedJugador != null) // Validar que no sea un jugador null
                    {
                        Writer.WriteLine(JsonConvert.SerializeObject("true")); // Confirmacion al cliente de autenticacion exitosa
                        Console.WriteLine("Los credenciales del jugador son correctos!\n");
                        this.Jugador = deserializedJugador; // Set del jugador
                        entrarSala(Socket); // Se entra a la sala
                    }
                    else
                    {
                        Writer.WriteLine(JsonConvert.SerializeObject("false")); // Error de autenticacion
                        Thread.Sleep(5000);
                    }
                }
                catch
                {
                    Disconnect(); // Desconectar el cliente en caso de error
                }
            }
        }

        public void Disconnect()
        {
            Reader.Close();
            Writer.Close();
            Socket.Close();
        }

        private void entrarSala(TcpClient socket)
        {
            Sala.Mesa.Add(this);
        }

    }
}
