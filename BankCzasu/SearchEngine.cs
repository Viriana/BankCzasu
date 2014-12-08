using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankCzasu
{
    class SearchEngine
    {
        private string nameCriterion;
        private string surnameCriterion;
        private int ageCriterion;
        private List<Skill> skillsCriterion;
        private bool sortByName;
        private bool sortBySurname;
        private bool sortByAge;

        private static SearchEngine _instance;
        public static SearchEngine Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new SearchEngine();
                }
                return _instance;
            }
        }

        private SearchEngine()
        {
            sortByName = true;
            sortBySurname = false;
            sortByAge = false;
            nameCriterion = "";
            surnameCriterion = "";
            ageCriterion = -1;
            skillsCriterion = null;
        }

        private bool PorownajUmiejetnosci(List<Skill> skillsA, List<Skill> skillsB)
        {
            bool x = true;

            if(skillsA.Count != skillsB.Count)
            {
                return false;
            }

            for (int i = 0; i < skillsA.Count; i++)
            {
                if(skillsA[i].name != skillsB[i].name || skillsA[i].exp_level != skillsB[i].exp_level)
                {
                    x = false;
                }
            }

            return x;
        }

        public List<User> Find()
        {
            List<User> toRet = new List<User>();

            /*coś w stylu:
Select(Table.uzytkownicy, uzytkownicy.id_uzytkownika, uzytkownicy.imie, uzytkownicy.nazwisko, uzytkownicy.wiek);

Później dla każdego użytkownika robisz pętle w której:
Znajdujesz wszystkie rekordy z tabeli wybrane umiejętności o jego id:
SelectWhere(Tables.wybrane_umiejetnosci, new Dictionary<Enum, String>() {{umiejetnosci.status_umiejetnosci, "'posiadana'"}, {wybrane_umiejetnosci.id_uzytkownika, id_uzytkownika (tego z tej iteracji)}}, wybrane_umiejetnosci.id_umiejetnosci);

i później dla wszystkich znalezionych id umiejetnosci wyciągasz umiejetności z tabeli umiejętności:
SelectWhere(Tables.umiejetnosci, new Dictionary<Enum, String>() {{umiejetnosci.id_umiejetnosci, id_umiejetnosci (z iteracji)}}, umiejetnosci.nazwa);

I np dodajesz tą umiejętność do listy zwróconej przez pierwszy select (ten z użytkownikami), będziesz miał wtedy w niej listę użytkowników z podanymi kolumnami + ich wszystkie umiejetnosci*/

            List<List<string>> returnedSelect = DataBase.Select(Tables.uzytkownicy, uzytkownicy.id_uzytkownika, uzytkownicy.imie, uzytkownicy.nazwisko, uzytkownicy.wiek, uzytkownicy.plec, uzytkownicy.adres, uzytkownicy.telefon, uzytkownicy.mail);

            string[] id = new string[returnedSelect.Count];
            string[] imie = new string[returnedSelect.Count];
            string[] nazwisko = new string[returnedSelect.Count];
            string[] wiek = new string[returnedSelect.Count];
            string[] plec = new string[returnedSelect.Count];
            string[] adres = new string[returnedSelect.Count];
            string[] telefon = new string[returnedSelect.Count];
            string[] mail = new string[returnedSelect.Count];
            Dictionary<Enum, string> id_do_logowania = new Dictionary<Enum, string>();

            for(int i = 0; i < returnedSelect.Count; i++)
            {
                id[i] = returnedSelect[i][0];
                imie[i] = returnedSelect[i][1];
                nazwisko[i] = returnedSelect[i][2];
                wiek[i] = returnedSelect[i][3];
                plec[i] = returnedSelect[i][4];
                adres[i] = returnedSelect[i][5];
                telefon[i] = returnedSelect[i][6];
                mail[i] = returnedSelect[i][7];
                id_do_logowania.Add(dane_logowania.id_uzytkownika, id[i]);
            }

            List<List<string>> logIDs = DataBase.SelectWhere(Tables.dane_logowania, id_do_logowania, dane_logowania.haslo, dane_logowania.id_logowania);
            string[] id_logowania = new string[logIDs.Count];
            string[] passwords = new string[logIDs.Count];
            for (int i = 0; i < logIDs.Count; i++)
            {
                id_logowania[i] = logIDs[i][1];
                passwords[i] = logIDs[i][0];
            }

            string[][] idUmiejetnosci = new string[id.Length][];
            string[][] doswiadczenie = new string[id.Length][];
            for(int i = 0; i < id.Length; i++)
            {
                List<List<string>> skills = DataBase.SelectWhere(Tables.wybrane_umiejetnosci, new Dictionary<Enum, string>() { { wybrane_umiejetnosci.status_umiejetnosci, "'posiadana'" }, { wybrane_umiejetnosci.id_uzytkownika, id[i] } }, wybrane_umiejetnosci.id_umiejetnosci, wybrane_umiejetnosci.poziom);
                idUmiejetnosci[i] = new string[skills.Count];
                doswiadczenie[i] = new string[skills.Count];
                for(int j = 0; j <skills.Count;j++)
                {
                    idUmiejetnosci[i][j] = skills[j][0];
                    doswiadczenie[i][j] = skills[j][1];
                }
            }

            string[][] nazwaUmiejetnosci = new string[id.Length][];

            for (int i = 0; i < id.Length;i++)
            {
                nazwaUmiejetnosci[i] = new string[idUmiejetnosci[i].Length];
                for(int j = 0; j < idUmiejetnosci[i].Length; j++)
                {
                    List<List<string>> skills = DataBase.SelectWhere(Tables.umiejetnosci, new Dictionary<Enum, string>() { { umiejetnosci.id_umiejetnosci, idUmiejetnosci[i][j] } }, umiejetnosci.nazwa);
                    for(int k=0;k<skills.Count; k++)
                    {
                        nazwaUmiejetnosci[i][k] = skills[k][0];
                    }
                }
            }

            List<List<Skill>> toCompare = new List<List<Skill>>();
            for (int i = 0; i < id.Length; i++)
            {
                toCompare.Add(new List<Skill>());
                for(int j = 0; j < idUmiejetnosci.Length; j++)
                {
                    toCompare[i].Add(new Skill(nazwaUmiejetnosci[i][j], doswiadczenie[i][j]));
                }
            }

            for (int i = 0; i < id.Length; i++)
            {
                if (nameCriterion != "" && surnameCriterion != "" && ageCriterion != -1 && skillsCriterion != null)
                {
                    if(imie[i] == nameCriterion && nazwisko[i] == surnameCriterion && Convert.ToInt32(wiek[i]) == ageCriterion && PorownajUmiejetnosci(toCompare[i], skillsCriterion))
                    {
                        int intID = Convert.ToInt32(id[i]);
                        int intAge = Convert.ToInt32(wiek[i]);
                        User newUser = new User(intID, imie[i], nazwisko[i], intAge, id_logowania[i], passwords[i], plec[i], adres[i], telefon[i], mail[i]);
                        foreach(Skill s in skillsCriterion)
                        {
                            newUser.addSkillHeld(s);
                        }
                        toRet.Add(newUser);
                    }
                }
                else if (nameCriterion != "" && surnameCriterion != "" && ageCriterion != -1)
                {
                    if (imie[i] == nameCriterion && nazwisko[i] == surnameCriterion && Convert.ToInt32(wiek[i]) == ageCriterion)
                    {
                        int intID = Convert.ToInt32(id[i]);
                        int intAge = Convert.ToInt32(wiek[i]);
                        User newUser = new User(intID, imie[i], nazwisko[i], intAge, id_logowania[i], passwords[i], plec[i], adres[i], telefon[i], mail[i]);
                        toRet.Add(newUser);
                    }
                }
                else if (nameCriterion != "" && surnameCriterion != "" && skillsCriterion != null)
                {
                    if (imie[i] == nameCriterion && nazwisko[i] == surnameCriterion && PorownajUmiejetnosci(toCompare[i], skillsCriterion))
                    {
                        int intID = Convert.ToInt32(id[i]);
                        int intAge = Convert.ToInt32(wiek[i]);
                        User newUser = new User(intID, imie[i], nazwisko[i], intAge, id_logowania[i], passwords[i], plec[i], adres[i], telefon[i], mail[i]);
                        foreach (Skill s in skillsCriterion)
                        {
                            newUser.addSkillHeld(s);
                        }
                        toRet.Add(newUser);
                    }
                }
                else if (nameCriterion != "" && ageCriterion != -1 && skillsCriterion != null)
                {
                    if (imie[i] == nameCriterion && Convert.ToInt32(wiek[i]) == ageCriterion && PorownajUmiejetnosci(toCompare[i], skillsCriterion))
                    {
                        int intID = Convert.ToInt32(id[i]);
                        int intAge = Convert.ToInt32(wiek[i]);
                        User newUser = new User(intID, imie[i], nazwisko[i], intAge, id_logowania[i], passwords[i], plec[i], adres[i], telefon[i], mail[i]);
                        foreach (Skill s in skillsCriterion)
                        {
                            newUser.addSkillHeld(s);
                        }
                        toRet.Add(newUser);
                    }
                }
                else if (surnameCriterion != "" && ageCriterion != -1 && skillsCriterion != null)
                {
                    if (nazwisko[i] == surnameCriterion && Convert.ToInt32(wiek[i]) == ageCriterion && PorownajUmiejetnosci(toCompare[i], skillsCriterion))
                    {
                        int intID = Convert.ToInt32(id[i]);
                        int intAge = Convert.ToInt32(wiek[i]);
                        User newUser = new User(intID, imie[i], nazwisko[i], intAge, id_logowania[i], passwords[i], plec[i], adres[i], telefon[i], mail[i]);
                        foreach (Skill s in skillsCriterion)
                        {
                            newUser.addSkillHeld(s);
                        }
                        toRet.Add(newUser);
                    }
                }
                else if (nameCriterion != "" && surnameCriterion != "")
                {
                    if (imie[i] == nameCriterion && nazwisko[i] == surnameCriterion)
                    {
                        int intID = Convert.ToInt32(id[i]);
                        int intAge = Convert.ToInt32(wiek[i]);
                        User newUser = new User(intID, imie[i], nazwisko[i], intAge, id_logowania[i], passwords[i], plec[i], adres[i], telefon[i], mail[i]);
                        toRet.Add(newUser);
                    }
                }
                else if (nameCriterion != "" && ageCriterion != -1)
                {
                    if (imie[i] == nameCriterion && Convert.ToInt32(wiek[i]) == ageCriterion)
                    {
                        int intID = Convert.ToInt32(id[i]);
                        int intAge = Convert.ToInt32(wiek[i]);
                        User newUser = new User(intID, imie[i], nazwisko[i], intAge, id_logowania[i], passwords[i], plec[i], adres[i], telefon[i], mail[i]);
                        toRet.Add(newUser);
                    }
                }
                else if (nameCriterion != "" && skillsCriterion != null)
                {
                    if (imie[i] == nameCriterion && PorownajUmiejetnosci(toCompare[i], skillsCriterion))
                    {
                        int intID = Convert.ToInt32(id[i]);
                        int intAge = Convert.ToInt32(wiek[i]);
                        User newUser = new User(intID, imie[i], nazwisko[i], intAge, id_logowania[i], passwords[i], plec[i], adres[i], telefon[i], mail[i]);
                        foreach (Skill s in skillsCriterion)
                        {
                            newUser.addSkillHeld(s);
                        }
                        toRet.Add(newUser);
                    }
                }
                else if (surnameCriterion != "" && ageCriterion != -1)
                {
                    if (nazwisko[i] == surnameCriterion && Convert.ToInt32(wiek[i]) == ageCriterion)
                    {
                        int intID = Convert.ToInt32(id[i]);
                        int intAge = Convert.ToInt32(wiek[i]);
                        User newUser = new User(intID, imie[i], nazwisko[i], intAge, id_logowania[i], passwords[i], plec[i], adres[i], telefon[i], mail[i]);
                        toRet.Add(newUser);
                    }
                }
                else if (surnameCriterion != "" && skillsCriterion != null)
                {
                    if (nazwisko[i] == surnameCriterion && PorownajUmiejetnosci(toCompare[i], skillsCriterion))
                    {
                        int intID = Convert.ToInt32(id[i]);
                        int intAge = Convert.ToInt32(wiek[i]);
                        User newUser = new User(intID, imie[i], nazwisko[i], intAge, id_logowania[i], passwords[i], plec[i], adres[i], telefon[i], mail[i]);
                        foreach (Skill s in skillsCriterion)
                        {
                            newUser.addSkillHeld(s);
                        }
                        toRet.Add(newUser);
                    }
                }
                else if (ageCriterion != -1 && skillsCriterion != null)
                {
                    if (Convert.ToInt32(wiek[i]) == ageCriterion && PorownajUmiejetnosci(toCompare[i], skillsCriterion))
                    {
                        int intID = Convert.ToInt32(id[i]);
                        int intAge = Convert.ToInt32(wiek[i]);
                        User newUser = new User(intID, imie[i], nazwisko[i], intAge, id_logowania[i], passwords[i], plec[i], adres[i], telefon[i], mail[i]);
                        foreach (Skill s in skillsCriterion)
                        {
                            newUser.addSkillHeld(s);
                        }
                        toRet.Add(newUser);
                    }
                }
                else if (nameCriterion != "")
                {
                    if (imie[i] == nameCriterion)
                    {
                        int intID = Convert.ToInt32(id[i]);
                        int intAge = Convert.ToInt32(wiek[i]);
                        User newUser = new User(intID, imie[i], nazwisko[i], intAge, id_logowania[i], passwords[i], plec[i], adres[i], telefon[i], mail[i]);
                        toRet.Add(newUser);
                    }
                }
                else if (surnameCriterion != "")
                {
                    if (nazwisko[i] == surnameCriterion)
                    {
                        int intID = Convert.ToInt32(id[i]);
                        int intAge = Convert.ToInt32(wiek[i]);
                        User newUser = new User(intID, imie[i], nazwisko[i], intAge, id_logowania[i], passwords[i], plec[i], adres[i], telefon[i], mail[i]);
                        toRet.Add(newUser);
                    }
                }
                else if (ageCriterion != -1)
                {
                    if (Convert.ToInt32(wiek[i]) == ageCriterion)
                    {
                        int intID = Convert.ToInt32(id[i]);
                        int intAge = Convert.ToInt32(wiek[i]);
                        User newUser = new User(intID, imie[i], nazwisko[i], intAge, id_logowania[i], passwords[i], plec[i], adres[i], telefon[i], mail[i]);
                        toRet.Add(newUser);
                    }
                }
                else if (skillsCriterion != null)
                {
                    if (PorownajUmiejetnosci(toCompare[i], skillsCriterion))
                    {
                        int intID = Convert.ToInt32(id[i]);
                        int intAge = Convert.ToInt32(wiek[i]);
                        User newUser = new User(intID, imie[i], nazwisko[i], intAge, id_logowania[i], passwords[i], plec[i], adres[i], telefon[i], mail[i]);
                        foreach (Skill s in skillsCriterion)
                        {
                            newUser.addSkillHeld(s);
                        }
                        toRet.Add(newUser);
                    }
                }
                else
                {
                    int intID = Convert.ToInt32(id[i]);
                    int intAge = Convert.ToInt32(wiek[i]);
                    User newUser = new User(intID, imie[i], nazwisko[i], intAge, id_logowania[i], passwords[i], plec[i], adres[i], telefon[i], mail[i]);
                    toRet.Add(newUser);
                }
            }

            if (sortByAge)
            {
                for (int j = 0; j < toRet.Count; j++)
                {
                    for (int i = 1; i < toRet.Count; i++)
                    {
                        if (toRet[i].age < toRet[i - 1].age)
                        {
                            User temp = toRet[i];
                            toRet[i] = toRet[i - 1];
                            toRet[i - 1] = temp;
                        }
                    }
                }
            }
            else if (sortByName)
            {
                for (int j = 0; j < toRet.Count; j++)
                {
                    for (int i = 1; i < toRet.Count; i++)
                    {
                        if (String.Compare(toRet[i].name, toRet[i - 1].name) == -1)
                        {
                            User temp = toRet[i];
                            toRet[i] = toRet[i - 1];
                            toRet[i - 1] = temp;
                        }
                    }
                }
            }
            else if (sortBySurname)
            {
                for (int j = 0; j < toRet.Count; j++)
                {
                    for (int i = 1; i < toRet.Count; i++)
                    {
                        if (String.Compare(toRet[i].surname, toRet[i - 1].surname) == -1)
                        {
                            User temp = toRet[i];
                            toRet[i] = toRet[i - 1];
                            toRet[i - 1] = temp;
                        }
                    }
                }
            }

            SetCriterions("", "", -1, null);

            return toRet;
        }

        void SetCriterions(string _name, string _surname, int _age, List<Skill> _skills)
        {
            nameCriterion = _name;
            surnameCriterion = _surname;
            ageCriterion = _age;
            skillsCriterion = _skills;
        }

        void SortByName()
        {
            sortByName = true;
            sortBySurname = false;
            sortByAge = false;
        }

        void SortBySurname()
        {
            sortByName = false;
            sortBySurname = true;
            sortByAge = false;
        }

        void SortByAge()
        {
            sortByName = false;
            sortBySurname = false;
            sortByAge = true;
        }
    }
}
