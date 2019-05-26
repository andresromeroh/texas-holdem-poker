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

                    if (deserializedJugador != null) // Validar que no sea un jugador null
                    {
                        Writer.WriteLine(JsonConvert.SerializeObject("1")); // Confirmacion al cliente de autenticacion exitosa
                        this.Jugador = deserializedJugador; // Set del jugador
                        entrarSala(Socket); // Se entra a la sala
                    }
                    else
                    {
                        Writer.WriteLine(JsonConvert.SerializeObject("0")); // Error de autenticacion
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
            Sala.Mesa.Juego.Jugadores.Append(this.Jugador);
        }

    }
}
