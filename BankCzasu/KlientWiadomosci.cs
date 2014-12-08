using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BankCzasu
{
    class KlientWiadomosci
    {
        public void ShoutboxClient(string[] args)
        {

            TcpClient client = new TcpClient();
            client.Connect("127.0.0.1", 5);
            while (true)
            {

                Console.WriteLine("Podaj tekst do wysłania");
                string msg = Console.ReadLine();


                NetworkStream serverStream = client.GetStream();
                byte[] outStream = System.Text.Encoding.ASCII.GetBytes(msg);
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();

                if (msg == "koniec$")
                    break;
            }
            client.Close();
            Console.WriteLine("Zakonczono");
            Console.ReadLine();
        }
    }
}
