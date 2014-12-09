using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankCzasu
{
    class LoginInUser
    {

        public User logInUser { get; private set; }

        private LoginInUser(){}
        private static LoginInUser _instance;
        public static LoginInUser instance
        {
            get
            {
                if (_instance == null)
                    _instance = new LoginInUser();

                return _instance;
            }
        }

        public bool Login(String mail, String password)
        {
            List<List<String>> helpList = DataBase.SelectWhere(Tables.uzytkownicy, new Dictionary<Enum, string>() { { uzytkownicy.mail, DataBase.ToSQLString(mail) } }, uzytkownicy.id_uzytkownika);
            String userID = helpList[0][0];

            if (userID.Equals(DataBase.NULL))
                return false;

            helpList = DataBase.SelectWhere(Tables.dane_logowania, new Dictionary<Enum, string>() { { dane_logowania.id_uzytkownika, userID } }, dane_logowania.haslo);
            String userPass = helpList[0][0];

            if (userPass.Equals(DataBase.NULL) || !userPass.Equals(password))
                return false;

            helpList = DataBase.SelectWhere(Tables.uzytkownicy, new Dictionary<Enum, string>() { { uzytkownicy.id_uzytkownika, userID } });
            logInUser = new User(int.Parse(userID), helpList[0][1], helpList[0][2], int.Parse(helpList[0][3]), mail, password, helpList[0][7], helpList[0][5], helpList[0][4], mail);

            if (logInUser == null)
                return false;

            return true;
        }

        public bool Registration(String name, String surname, String age, String phone, String adress, String mail, String sex, String password, String questionID, String answer)
        {
            sex = sex.ToLower().ElementAt(0).ToString();

            string SQLname      = DataBase.ToSQLString(name);
            string SQLsurname   = DataBase.ToSQLString(surname);
            string SQLphone     = DataBase.ToSQLString(phone);
            string SQLadress    = DataBase.ToSQLString(adress);
            string SQLmail      = DataBase.ToSQLString(mail);
            string SQLsex       = DataBase.ToSQLString(sex);

            string SQLpassword  = DataBase.ToSQLString(password);
            string SQLanswer    = DataBase.ToSQLString(answer);

            if(!(sex.Equals("m") || sex.Equals("k")) ||
                !mail.Contains("@"))
                return false;

            if (!DataBase.Insert(Tables.uzytkownicy,
                new Dictionary<Enum, String>()
                {
                    {uzytkownicy.imie,      SQLname},
                    {uzytkownicy.nazwisko,  SQLsurname},
                    {uzytkownicy.wiek,      age},
                    {uzytkownicy.telefon,   SQLphone},
                    {uzytkownicy.adres,     SQLadress},
                    {uzytkownicy.mail,      SQLmail},
                    {uzytkownicy.plec,      SQLsex}
                }))
                return false;

            List<List<String>> user = DataBase.SelectWhere(Tables.uzytkownicy, new Dictionary<Enum, string>() { { uzytkownicy.mail, SQLmail } }, uzytkownicy.id_uzytkownika);
            if(user.Equals(DataBase.NULL))
            {
                DataBase.Delete(Tables.uzytkownicy, new Dictionary<Enum, string>() { { uzytkownicy.mail, SQLmail } });
                return false;
            }

            string userID = user[0][0];

            if (DataBase.Insert(Tables.dane_logowania, new Dictionary<Enum, string>()
                {
                    {dane_logowania.id_uzytkownika, userID},
                    {dane_logowania.haslo, SQLpassword},
                    {dane_logowania.id_pytanie_kon, questionID},
                    {dane_logowania.odp_pytanie_kon, SQLanswer}
                }))
            {
                Login(mail, password);
                return true; 
            }


            DataBase.Delete(Tables.uzytkownicy, new Dictionary<Enum, string>() { { uzytkownicy.mail, SQLmail } });

            return false;
        }

        public String GetQuestion(String mail)
        {
            List<List<String>> helpList = DataBase.SelectWhere(Tables.uzytkownicy, new Dictionary<Enum, string>() { { uzytkownicy.mail, DataBase.ToSQLString(mail) } }, uzytkownicy.id_uzytkownika);
            String userID = helpList[0][0];

            if (userID.Equals(DataBase.NULL))
                return "GetQuestion Error userID";

            helpList = DataBase.SelectWhere(Tables.dane_logowania, new Dictionary<Enum, string>() { { dane_logowania.id_uzytkownika, userID } }, dane_logowania.id_pytanie_kon);
            String questionID = helpList[0][0];

            if (questionID.Equals(DataBase.NULL))
                return "GetQuestion Error questionID";

            helpList = DataBase.SelectWhere(Tables.pytania_kontrolne, new Dictionary<Enum, string>() { { pytania_kontrolne.id_pytania, questionID } }, pytania_kontrolne.pytanie);
            String question = helpList[0][0];

            if (question.Equals(DataBase.NULL))
                return "GetQuestion Error question";

            return question;
        }

        public bool RemindPassword(String mail, String answer)
        {
            List<List<String>> helpList = DataBase.SelectWhere(Tables.uzytkownicy, new Dictionary<Enum, string>() { { uzytkownicy.mail, DataBase.ToSQLString(mail) } }, uzytkownicy.id_uzytkownika);
            String userID = helpList[0][0];

            if (userID.Equals(DataBase.NULL))
                return false;

            helpList = DataBase.SelectWhere(Tables.dane_logowania, new Dictionary<Enum, string>() { { dane_logowania.id_uzytkownika, userID } }, dane_logowania.odp_pytanie_kon);
            String ans = helpList[0][0];

            if (ans.Equals(DataBase.NULL))
                return false;

            if (!ans.Equals(answer))
                return false;

            return true;
        }

        public bool ChangePassword(String mail, String newPassword)
        {
            List<List<String>> helpList = DataBase.SelectWhere(Tables.uzytkownicy, new Dictionary<Enum, string>() { { uzytkownicy.mail, DataBase.ToSQLString(mail) } }, uzytkownicy.id_uzytkownika);
            String userID = helpList[0][0];

            if (userID.Equals(DataBase.NULL))
                return false;

            helpList = DataBase.SelectWhere(Tables.dane_logowania, new Dictionary<Enum, string>() { { dane_logowania.id_uzytkownika, userID } }, dane_logowania.id_logowania);
            String logID = helpList[0][0];

            if (logID.Equals(DataBase.NULL))
                return false;

            return DataBase.Update(Tables.dane_logowania, new Dictionary<Enum, string>() { { dane_logowania.haslo, DataBase.ToSQLString(newPassword) } }, new Dictionary<Enum, string>() { { dane_logowania.id_logowania, logID } });
        }

    }
}
