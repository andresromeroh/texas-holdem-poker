using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    class TCPClientService
    {
        TcpClient client = null;
        NetworkStream stream = null;
        Byte[] data = null;

        public void startService(string server, Int32 port)
        {

            try
            {
                // Crear el cliente TCP
                // Nota, para que este cliente funcione se necesita un servidor TCP
                // conectado a la IP y escuchando en el puerto indicados por las variables server y port
                client = new TcpClient();
                client.Connect(server, port);

                // Obtener un strem del cliente para lectura y escritura
                stream = client.GetStream();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }

        public void stopService()
        {
            stream.Close();
            client.Close();
        }

        public void sendMessage(string json)
        {
            // Convertir la informacion a ASCII para ser guardada como un arreglo de Bytes
            data = System.Text.Encoding.ASCII.GetBytes(json);
            // Enviar el mensaje al servidor conectado
            stream.Write(data, 0, data.Length);
            Console.WriteLine("Sent: {0}", json);
        }

        public void handleResponse()
        {
            // Recibir la respuesta TcpServer.response
            // Buffer para almacenar la respuesta
            data = new Byte[16384];

            // String para almacenar la representacion ASCII
            String responseData = String.Empty;

            // Leer el primer lote de bytes de respuesta del TcpServer
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Console.WriteLine("Received: {0}", responseData);
        }

    }
}
