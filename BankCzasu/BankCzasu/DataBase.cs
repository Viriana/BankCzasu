using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankCzasu
{
    public enum Tables
    {
        dane_logowania,
        dostepnosc,
        komentarz,
        ocena_umiejetnosci,
        posiadane_tagi,
        pytania_kontrolne,
        tagi,
        umiejetnosci,
        uzytkownicy,
        wiadomosc,
        wybrane_umiejetnosci,
        wydarzenie,
        znajomi
    };

    public enum  dane_logowania
    {
        id_logowania,
        id_uzytkownika,
        haslo,
        id_pytania_kon,
        odp_pytanie_kon
    };
    public enum  dostepnosc 
    {
        id_dostepnosc,
        id_uzytkownika,
        dzien,
        od_godziny,
        do_godziny
    };
    public enum  komentarz 
    {
        id_komentarz,
        id_od_kogo,
        id_do_kogo,
        data,
        ocena,
        tresc
    };
    public enum  ocena_umiejetnosci
    {
        id_oceny,
        id_dla_kogo,
        id_od_kogo,
        data,
        id_umiejetnosc,
        ocena,
        komentarz
    };
    public enum  posiadane_tagi
    {
        id_posiadanego_tagu,
        id_tagu,
        id_uzytkownika
    };
    public enum  pytania_kontrolne
    {
        id_pytania, 
        pytanie
    };
    public enum  tagi
    {
        id_tagu,
        nazwa
    };
    public enum  umiejetnosci
    {
        id_umiejetnosci,
        nazwa
    };
    public enum  uzytkownicy
    {
        id_uzytkownika,
        imie,
        nazwisko,
        wiek,
        telefon,
        adres,
        mail,
        plec
    };
    public enum  wiadomosc
    {
        id_wiadomosci,
        id_dla_kogo,
        id_od_kogo,
        data,
        tresc
    };
    public enum  wybrane_umiejetnosci
    {
        id_wybranej_umiejetnosci,
        status_umiejetnosci,
        id_umiejetnosci,
        id_uzytkownika,
        poziom,
        priorytet
    };
    public enum  wydarzenie
    {
        id_wydarzenia,
        id_uzytkownika,
        data,
        od_godziny,
        do_godziny,
        tresc
    };
    public enum  znajomi 
    {
        id_znajomych,
        id_uzytkownika1,
        id_uzytkownika2
    };

    public class DataBase
    {
        private static DataBase _instance;
        public static DataBase instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DataBase();

                return _instance;
            }
        }

        private DataBase() { }

        private const String URI = "http://bankczasu2014.io/index.php";
        private const String TRUE = "TRUE;";
        private const String NULL = "NULL;";

        private String POST(String query)
        {
            byte[] response = null;

            using (WebClient client = new WebClient())
            {
                response =
                    client.UploadValues(URI, new NameValueCollection()
                    {
                        {"query", query}
                    });

            }

            return System.Text.Encoding.UTF8.GetString(response);
        }

        public List< List<String> > Select(Tables table, params Enum[] fields)
        {
            string select   = "";
            string from     = "";
            string query    = "";

            List<List<String>> returnList = new List<List<string>>();

            from = table.ToString();

            if (fields.Length == 0)
                select = "*";
            else
            {
                for (int i = 0; i < fields.Length; i++ )
                {
                    select += fields[i].ToString();
                    if (i < fields.Length - 1)
                        select += ", ";
                }
            }

            query = String.Format("SELECT {0} FROM {1}", select, from);

            string response = POST(query);

            if(response.Equals(NULL))
            {
                returnList.Add(new List<string>());
                returnList[0].Add(response);
            }
            else
            {
                string[] rows = response.Split(new string[]{";"}, StringSplitOptions.RemoveEmptyEntries);
                foreach(string row in rows)
                {
                    returnList.Add(new List<string>());
                    string[] values = row.Split(new string[] { "}" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string value in values)
                        returnList[returnList.Count - 1].Add(value);
                }
            }

            return returnList;
        }

        public bool Insert(Tables table, Dictionary<Enum, String> fields)
        {
            string into     = "";
            string columns  = "";
            string values   = "";
            string query    = "";

            into = table.ToString();

            foreach(KeyValuePair<Enum, String> field in fields)
            {
                columns += field.Key.ToString();
                values += field.Value;

                if(!field.Equals(fields.Last()))
                {
                    columns += ", ";
                    values += ", ";
                }
            }

            query = String.Format("INSERT INTO {0} ({1}) VALUES ({2})", into, columns, values);

            string response = POST(query);

            if (response.Equals(NULL))
                return false;

            return true;
        }

        public bool Delete(Tables table, Enum column, string fieldEqual)
        {
            string query = String.Format("DELETE FROM {0} WHERE {1} = {2}", table.ToString(), column.ToString(), fieldEqual);

            string response = POST(query);

            if (response.Equals(NULL))
                return false;

            return true;
        }

        public bool Update(Tables table, Dictionary<Enum, String> fields, Enum column, string fieldEqual)
        {
            string values   = "";
            string query    = "";

            foreach (KeyValuePair<Enum, String> field in fields)
            {
                values += field.Key.ToString() + "='" + field.Value + "'";
                if (!field.Equals(fields.Last()))
                {
                    values += ", ";
                }
            }

            query = String.Format("UPDATE {0} SET {1} WHERE {2} = {3}", table.ToString(), values, column.ToString(), fieldEqual);

            string response = POST(query);

            if (response.Equals(NULL))
                return false;

            return true;
        }
    }
}
