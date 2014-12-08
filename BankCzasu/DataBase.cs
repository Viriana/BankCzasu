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

    public enum dane_logowania
    {
        id_logowania,
        id_uzytkownika,
        haslo,
        id_pytania_kon,
        odp_pytanie_kon
    };
    public enum dostepnosc
    {
        id_dostepnosc,
        id_uzytkownika,
        dzien,
        od_godziny,
        do_godziny
    };
    public enum komentarz
    {
        id_komentarz,
        id_od_kogo,
        id_do_kogo,
        data,
        ocena,
        tresc
    };
    public enum ocena_umiejetnosci
    {
        id_oceny,
        id_dla_kogo,
        id_od_kogo,
        data,
        id_umiejetnosc,
        ocena,
        komentarz
    };
    public enum posiadane_tagi
    {
        id_posiadanego_tagu,
        id_tagu,
        id_uzytkownika
    };
    public enum pytania_kontrolne
    {
        id_pytania,
        pytanie
    };
    public enum tagi
    {
        id_tagu,
        nazwa
    };
    public enum umiejetnosci
    {
        id_umiejetnosci,
        nazwa
    };
    public enum uzytkownicy
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
    public enum wiadomosc
    {
        id_wiadomosci,
        id_dla_kogo,
        id_od_kogo,
        data,
        tresc
    };
    public enum wybrane_umiejetnosci
    {
        id_wybranej_umiejetnosci,
        status_umiejetnosci,
        id_umiejetnosci,
        id_uzytkownik,
        poziom,
        priorytet
    };
    public enum wydarzenie
    {
        id_wydarzenia,
        id_uzytkownika,
        data,
        od_godziny,
        do_godziny,
        tresc
    };
    public enum znajomi
    {
        id_znajomych,
        id_uzytkownika1,
        id_uzytkownika2
    };

    public static class DataBase
    {
        private const String URI = "http://bankczasu2014.cba.pl/index.php";
        private const String TRUE = "TRUE;";
        private const String NULL = "NULL;";
        private const String SQL_START = "SQL_START;";
        private const String SQL_END = "SQL_END;";

        private static String GetResponse(string response)
        {
            string sqlResponse = "";

            int SQLstart = response.IndexOf(SQL_START) + SQL_START.Length;
            int SQLend = response.IndexOf(SQL_END);

            sqlResponse = response.Substring(SQLstart, SQLend - SQLstart);

            return sqlResponse;
        }

        private static String POST(String query)
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

            return GetResponse(System.Text.Encoding.UTF8.GetString(response));
        }

        public static List<List<String>> Select(Tables table, params Enum[] fields)
        {
            string select = "";
            string from = "";
            string query = "";

            List<List<String>> returnList = new List<List<string>>();

            from = table.ToString();

            //pars columns to select
            if (fields.Length == 0)
                select = "*";
            else
            {
                for (int i = 0; i < fields.Length; i++)
                {
                    select += fields[i].ToString();
                    if (i < fields.Length - 1)
                        select += ", ";
                }
            }

            query = String.Format("SELECT {0} FROM {1}", select, from);

            string response = POST(query);

            //get from 'response' list
            if (response.Equals(NULL))
            {
                returnList.Add(new List<string>());
                returnList[0].Add(response);
            }
            else
            {
                string[] rows = response.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string row in rows)
                {
                    returnList.Add(new List<string>());
                    string[] values = row.Split(new string[] { "}" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string value in values)
                        returnList[returnList.Count - 1].Add(value);
                }
            }

            return returnList;
        }
        public static List<List<String>> SelectWhere(Tables table, Dictionary<Enum, String> fields, params Enum[] columns)
        {
            string select = "";
            string where = "";
            string query = "";

            List<List<String>> returnList = new List<List<string>>();


            //pars columns to select
            if (columns.Length == 0)
                select = "*";
            else
            {
                for (int i = 0; i < columns.Length; i++)
                {
                    select += columns[i].ToString();
                    if (i < columns.Length - 1)
                        select += ", ";
                }
            }

            //pars where
            foreach (KeyValuePair<Enum, String> field in fields)
            {
                string column = field.Key.ToString();
                string value = field.Value;

                where += column + " = " + value;

                if (!field.Equals(fields.Last()))
                {
                    where += " AND ";
                }
            }

            query = String.Format("SELECT {0} FROM {1} WHERE {2}", select, table.ToString(), where);

            string response = POST(query);

            //get from 'response' list
            if (response.Equals(NULL))
            {
                returnList.Add(new List<string>());
                returnList[0].Add(response);
            }
            else
            {
                string[] rows = response.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string row in rows)
                {
                    returnList.Add(new List<string>());
                    string[] values = row.Split(new string[] { "}" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string value in values)
                        returnList[returnList.Count - 1].Add(value);
                }
            }

            return returnList;
        }
        public static bool Insert(Tables table, Dictionary<Enum, String> fields)
        {
            string into = "";
            string columns = "";
            string values = "";
            string query = "";

            into = table.ToString();

            //geting columns end values to insert
            foreach (KeyValuePair<Enum, String> field in fields)
            {
                columns += field.Key.ToString();
                values += field.Value;

                if (!field.Equals(fields.Last()))
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
        public static bool Delete(Tables table, Dictionary<Enum, String> fields)
        {
            string where = "";
            string query = "";

            //pars where
            foreach (KeyValuePair<Enum, String> field in fields)
            {
                string column = field.Key.ToString();
                string value = field.Value;

                where += column + " = " + value;

                if (!field.Equals(fields.Last()))
                {
                    where += " ADD ";
                }
            }

            query = String.Format("DELETE FROM {0} WHERE {1}", table.ToString(), where);

            string response = POST(query);

            if (response.Equals(NULL))
                return false;

            return true;
        }
        public static bool Update(Tables table, Dictionary<Enum, String> columns, Dictionary<Enum, String> fields)
        {
            string values = "";
            string query = "";
            string where = "";

            //geting columns and new values
            foreach (KeyValuePair<Enum, String> column in columns)
            {
                values += column.Key.ToString() + "=" + column.Value;
                if (!column.Equals(fields.Last()))
                {
                    values += ", ";
                }
            }

            //pars where
            foreach (KeyValuePair<Enum, String> field in fields)
            {
                string column = field.Key.ToString();
                string value = field.Value;

                where += column + " = " + value;

                if (!field.Equals(fields.Last()))
                {
                    where += " ADD ";
                }
            }

            query = String.Format("UPDATE {0} SET {1} WHERE {2}", table.ToString(), values, where);

            string response = POST(query);

            if (response.Equals(NULL))
                return false;

            return true;
        }
    }
}
