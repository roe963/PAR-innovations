using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Servidor
{
    public partial class Form1 : Form
    {
        Socket handler;

        public Form1()
        {
            InitializeComponent();

            Task.Run(() => StartListening());

            // Verify conection every second and connect to
            // the server if this is not established
            Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    VerifyConnection(handler);
                }
            });
        }

        // Incoming data from the client.  
        private string data = null;

        private void StartListening()
        {
            // Data buffer for incoming data.  
            byte[] bytes = new Byte[1024];

            // Establish the local endpoint for the socket.
            IPAddress ipAddress = IPAddress.Loopback;
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 9876);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and
            // listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                // Start listening for connections.  
                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    // Program is suspended while waiting for an incoming connection.  
                    handler = listener.Accept();

                    data = null;

                    // An incoming connection needs to be processed.  
                    int bytesRec = handler.Receive(bytes);

                    ComunicationMessages.Message message = JsonSerializer.Deserialize<ComunicationMessages.Message>(Encoding.ASCII.GetString(bytes, 0, bytesRec));

                    // Show the data on the console.  
                    Console.WriteLine("Text received : {0}", data);

                    ComunicationMessages.Response response = new ComunicationMessages.Response
                    {
                        Time = DateTime.Now

                    };
                    if (message.Content != null && message.Content.Length > 0)
                    {
                        response.Size = message.Content.Length;
                        response.SecondChar = message.Content[1];
                        response.ContainsCapitals = message.Content.Any(char.IsUpper);
                        response.AmountOfCapital = message.Content.Count(char.IsUpper);
                        response.ContainsNumbers = message.Content.Any(char.IsDigit);
                        response.AllNumbersInAscendingOrder = message.Content.Where(char.IsDigit).Select(x => (int)Char.GetNumericValue(x)).OrderBy(x => x).ToArray();
                    }

                    // Echo the data back to the client.  
                    byte[] msg = Encoding.ASCII.GetBytes(JsonSerializer.Serialize(response));

                    handler.Send(msg);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
        }


        private void VerifyConnection(Socket handler)
        {
            var aux = "Disconnected";

            if (handler != null)
            {
                _ = (bool)handler.Connected ? aux = "Connected" : "Disconnected";
            }

            textBoxState.BeginInvoke(new Action(() =>
            {
                textBoxState.Text = aux;
            }));
        }
    }
}
