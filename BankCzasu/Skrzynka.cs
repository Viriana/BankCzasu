using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BankCzasu
{
    class Skrzynka
    {

        public static void zapisz(Wiadomosc zapis)
        {
            StreamWriter wr = new StreamWriter("historia.xml");

            XmlSerializer serializer = new XmlSerializer(typeof(Wiadomosc));
            serializer.Serialize(wr, zapis);
            wr.Flush();
            wr.Close();
        }


        public static void odbierz(Wiadomosc odczyt)
        {
            XmlSerializer serializer1 = new XmlSerializer(typeof(Wiadomosc));
            string path = "historia.xml";
            StreamReader reader = new StreamReader(path);

            odczyt = (Wiadomosc)serializer1.Deserialize(reader);
            reader.Close();

        }
    }
}
