using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using ADAutenticacion;

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

            Console.WriteLine("Nuevo jugador conectado...\n");

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
                        if (Autenticacion.AutenticarUsuario(deserializedJugador.NombreUsuario, deserializedJugador.Password))
                        {
                            Writer.WriteLine(JsonConvert.SerializeObject("true")); // Confirmacion al cliente de autenticacion exitosa
                            Console.WriteLine("Los credenciales del jugador son correctos!\n");
                            Console.WriteLine("Nombre del jugador: " + deserializedJugador.NombreUsuario + "\n");
                            this.Jugador = deserializedJugador; // Set del jugador
                            entrarSala(Socket); // Se entra a la sala
                        } else
                        {
                            Writer.WriteLine(JsonConvert.SerializeObject("false")); // Confirmacion al cliente de autenticacion exitosa
                            Console.WriteLine("Los credenciales del jugador son incorrectos!\n");
                            Disconnect(); // Desconectar el cliente en caso de credenciales incorrectos
                        }

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
