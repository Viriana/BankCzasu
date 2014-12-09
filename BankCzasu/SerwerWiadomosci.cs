using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace BankCzasu
{
    class SerwerWiadomosci
    {

        public void ShoutboxServer(string[] args)
        {
            int i = 0;
            TcpListener soket = new TcpListener(5);
            TcpClient klient_s = new TcpClient();

            soket.Start();
            Console.WriteLine("Czekam");


            while (true)
            {
                i += 1;
                klient_s = soket.AcceptTcpClient();
                Console.WriteLine("Connected guest : " + Convert.ToString(i));


                klienci k = new klienci();
                k.start(klient_s, Convert.ToString(i));


                if (i == 10) break;
            }
        }
    }
}

public class klienci
{
    TcpClient klient;
    string nr;
    public void start(TcpClient podlaczony, string numerek)
    {
        this.klient = podlaczony;
        this.nr = numerek;
        Thread watek = new Thread(czekaj);
        watek.Start();

    }
    private void czekaj()
    {
        while (true)
        {
            byte[] dane = new byte[100];
            string dane_k = null;
            NetworkStream networkStream = klient.GetStream();
            networkStream.Read(dane, 0, dane.Length);
            dane_k = System.Text.Encoding.ASCII.GetString(dane);

            if (dane_k != "koniec")
            {
                Console.WriteLine("guest : " + nr + " send " + dane_k);
            }
            else break;
        }
    }
}