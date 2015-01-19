using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Xml.Serialization;
using System.IO;

namespace BankCzasu
{
    public partial class Chat : Form
    {

        Socket sck;
        EndPoint epLocal, epRemote;
        public String bufor;

        

        public Chat()
        {
            InitializeComponent();
            sck = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);


            textLocalIp.Text = GetLocalIP();
            textFriendsIp.Text = GetLocalIP();

        }

        private string GetLocalIP()   //pobiera lokalny adres IP
        {
            IPHostEntry host;
            host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            return "127.0.0.1";
 
        }

        private void MessageCallBack(IAsyncResult aResult)
        {
            try
            {
                int size = sck.EndReceiveFrom(aResult, ref epRemote);

                if (size > 0)
                {
                    byte[] receiveData = new byte[1464];
                    receiveData = (byte[])aResult.AsyncState;

                    ASCIIEncoding eEncoding = new ASCIIEncoding();
                    String receiveMessage = eEncoding.GetString(receiveData);   //konwersja
                    
                    int pos = receiveMessage.IndexOf('\0');
                    if (pos >= 0)
                        receiveMessage = receiveMessage.Substring(0, pos); //usuwa znaki NULL, ktore zostają w xmlu jako krzaki

                    listMessage.Items.Add("Ziomek: " + receiveMessage);


                }

                byte[] buffer = new byte[1500];
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallBack), buffer);


            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        
        private void textMessage_Click(object sender, EventArgs e)
        {
            try
            {
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                byte[] msg = new byte[1500];
                msg = enc.GetBytes(textMessage.Text);

                sck.Send(msg);

                listMessage.Items.Add("Ty: " + textMessage.Text);
          
                textMessage.Clear();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            try
            {
                epLocal = new IPEndPoint(IPAddress.Parse(textLocalIp.Text), Convert.ToInt32(textLocalPort.Text));
                sck.Bind(epLocal);

                epRemote = new IPEndPoint(IPAddress.Parse(textFriendsIp.Text), Convert.ToInt32(textFriendsPort.Text));
                sck.Connect(epRemote);

                byte[] buffer = new byte[1500];
                sck.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(MessageCallBack), buffer);

                startButton.Text = "Połączono";
                startButton.Enabled = false;

                sendButton.Enabled = true;
                textMessage.Focus();


            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                Wiadomosc dopliku = new Wiadomosc();

                foreach (string s in listMessage.Items)
                {
                    bufor += s + "\n";
                }
          
                dopliku.Tresc = bufor;
                Skrzynka.zapisz(dopliku);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void openHistory_Click(object sender, EventArgs e)
        {
            try
            {
                Wiadomosc odbior = new Wiadomosc();
                Skrzynka.odbierz(odbior);
                textHistory.Paste(odbior.Tresc);

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }

        private void textHistory_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
