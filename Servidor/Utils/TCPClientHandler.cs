using Servidor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Cliente.Models;

namespace Servidor.Utils
{
    public class TCPClientHandler
    {

        public TcpClient ClientSocket;
        public string ClientNo;
        public Juego Juego = null;

        public void startClient(TcpClient clientSocket, string clientNo)
        {
            this.ClientSocket = clientSocket;
            this.ClientNo = clientNo;
            Thread thread = new Thread(startGame);
            thread.Start();
        }

        public void startGame()
        {
            // Buffer para leer data
            Byte[] bytes = new Byte[16384];
            String data = null;

            // Obtener objeto string para lectura y escritura
            NetworkStream stream = ClientSocket.GetStream();

            int i;

            // Enviar el juego inicial
            string json = JsonConvert.SerializeObject(this.Juego);

            // Procesar la data enviada por el cliente
            byte[] response = System.Text.Encoding.ASCII.GetBytes(json);

            // Respuesta de vuelta "callback"
            stream.Write(response, 0, response.Length);
            Console.WriteLine("Sent: {0}", json);

            // Shutdown and end connection
            //ClientSocket.Close();

        }

        public void handleMessage()
        {
            // Buffer para leer data
            Byte[] bytes = new Byte[16384];
            String data = null;

            // Obtener objeto string para lectura y escritura
            NetworkStream stream = ClientSocket.GetStream();

            int i;

            // Loop para recibir la data enviada por el cliente
            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                // Transformar la da a formato ASCII
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                Console.WriteLine("Received from client #{0}: {1} \n", ClientNo, data);

                // Procesar la data enviada por el cliente
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                // Respuesta de vuelta "callback"
                stream.Write(msg, 0, msg.Length);
                Console.WriteLine("Sent: {0}", data);
            }

            // Shutdown and end connection
            ClientSocket.Close();
        }

    }
}
