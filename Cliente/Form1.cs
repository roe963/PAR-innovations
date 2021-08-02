using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliente
{
    public partial class Form1 : Form
    {
        private const string FILENAME = "client.log";
        private const string STATUSCONNECTED = "Connected";
        private const string STATUSDISCONNECTED = "Disconnected";
        private const int NUMBEROFMESSAGES = 100;
        private const int NUMBEROFFILES = 10;
        private const int SIZEOFFILE = 10240;
        private const int TIMEINMINUTES = 5;

        readonly IPAddress ipAddress;
        readonly IPEndPoint remoteEP;
        Socket sender;
        ComunicationMessages.Message msg;

        public Form1()
        {
            InitializeComponent();

            // Establish the remote endpoint for the socket.  
            ipAddress = IPAddress.Loopback;
            remoteEP = new IPEndPoint(ipAddress, 9876);


            // Verify conection every second and connect to
            // the server if this is not established
            Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);

                    if (!VerifyConnection(sender))
                    {
                        ConnectToServer();
                    }
                }
            });

            // Verify if a message is sent in last 5 seconds and 
            // send one if not
            Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(5000);

                    if (VerifyTimePastFromTheLastMessage())
                    {
                        msg = new ComunicationMessages.Message
                        {
                            Time = DateTime.Now,
                            Content = string.Empty
                        };

                        string txt = msg.Time.ToString() + ":\t";

                        CheckLogFiles();
                        Record(txt);
                        if (textBoxMessage.Text.Length > 0)
                        {
                            textBoxMessage.Clear();
                        }

                        if (VerifyConnection(sender))
                        {
                            SendMessage(JsonSerializer.Serialize(msg));
                        }
                    }
                }
            });
        }

        // Data buffer for incoming data.  
        byte[] bytes = new byte[1024];

        public void ConnectToServer()
        {

            // Connect to a remote device.  
            try
            {
                // Create a TCP/IP  socket.  
                sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.  
                try
                {
                    sender.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}",
                        sender.RemoteEndPoint.ToString());

                    UpdateConexionStatus(STATUSCONNECTED);

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                    UpdateConexionStatus(STATUSDISCONNECTED);
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                    UpdateConexionStatus(STATUSDISCONNECTED);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                    UpdateConexionStatus(STATUSDISCONNECTED);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void SendMessage(string input)
        {
            try
            {
                // Encode the data string into a byte array.  
                byte[] msg = Encoding.ASCII.GetBytes(input);

                // Send the data through the socket.  
                int bytesSent = sender.Send(msg);

                // Receive the response from the remote device.  
                int bytesRec = sender.Receive(bytes);

                // Json to object
                ComunicationMessages.Response response = JsonSerializer.Deserialize<ComunicationMessages.Response>(Encoding.ASCII.GetString(bytes, 0, bytesRec));

                string txt = response.Time.ToString();

                if (response.Size > 0)
                {
                    txt += ":\tCharacters: " + response.Size
                    + "\tCapitals: " + response.ContainsCapitals
                    + ", " + response.AmountOfCapital
                    + "\tNumbers: " + response.ContainsNumbers
                    + ", [ " + string.Join(", ", response.AllNumbersInAscendingOrder)
                    + " ]";
                }

                Record(txt);


                listBoxMessages.BeginInvoke(new Action(() =>
                {
                    while (listBoxMessages.Items.Count > NUMBEROFMESSAGES)
                    {
                        listBoxMessages.Items.RemoveAt(listBoxMessages.Items.Count - 1);
                    }
                }));

                ConnectToServer();
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }
        }

        private void PreviousSendAction()
        {
            msg = new ComunicationMessages.Message
            {
                Time = DateTime.Now,
                Content = textBoxMessage.Text.Trim()
            };

            string txt = msg.Time.ToString() + ":\t" + msg.Content;

            CheckLogFiles();
            Record(txt);
            textBoxMessage.Clear();

            if (VerifyConnection(sender))
            {
                SendMessage(JsonSerializer.Serialize(msg));
            }
            else
            {
                MessageBox.Show("Connection lost");
            }
        }

        // Write messages in the listbox and log
        private void Record(string txt)
        {
            listBoxMessages.BeginInvoke(new Action(() =>
            {
                listBoxMessages.Items.Insert(0, txt);
            }));
            using StreamWriter w = File.AppendText(FILENAME);
            Logger.Log(txt, w);
        }

        // Verify log files and delete the older
        private void CheckLogFiles()
        {
            FileInfo fi = new FileInfo(FILENAME);
            if (fi.Exists && fi.Length > SIZEOFFILE)
            {
                var myLogFiles = Directory
                    .EnumerateFiles(Directory.GetCurrentDirectory(), "*.log", SearchOption.AllDirectories)
                    .Select(f => new FileInfo(f))
                    .OrderByDescending(x => x.LastWriteTimeUtc)
                    .ToArray();

                if (myLogFiles.Length > NUMBEROFFILES)
                {
                    myLogFiles.Where(f => DateTime.UtcNow.Subtract(f.LastWriteTimeUtc).TotalMinutes > TIMEINMINUTES)
                        .Select(x => x)
                        .ToList()
                        .ForEach(f =>
                        {
                            Record(DateTime.Now.ToString() + ":\t" + f.Name + "\t DELETED");
                            f.Delete();
                        });
                }
                try
                {
                    File.Copy(FILENAME, "cliente " + fi.LastWriteTimeUtc.ToString("yyyy-MM-dd HH·mm·ss") + ".log");
                }
                catch (IOException ioe)
                {
                    Console.WriteLine("{0}", ioe.Message);
                }
            }
        }

        private void UpdateConexionStatus(string txt)
        {
            textBoxState.BeginInvoke(new Action(() =>
            {
                textBoxState.Text = txt;
            }));
        }

        private bool VerifyConnection(Socket handler)
        {
            if (handler != null)
            {
                return (bool)handler.Connected;
            }
            return false;
        }

        private bool VerifyTimePastFromTheLastMessage()
        {
            if (msg == null || DateTime.Now.Subtract(msg.Time).Seconds > 5)
            {
                return true;
            }
            return false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            PreviousSendAction();
        }

        private void TextBoxMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                PreviousSendAction();
            }
        }
    }
}
