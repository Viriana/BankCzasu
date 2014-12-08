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

        private bool SkillCompare(List<Skill> skillsA, List<Skill> skillsB)
        {
            bool x = true;

            if((skillsA.Count) != skillsB.Count)
            {
                return false;
            }

            for (int i = 0; i < skillsB.Count; i++)
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

            List<List<string>> returnedSelect = DataBase.Select(Tables.uzytkownicy, uzytkownicy.id_uzytkownika, uzytkownicy.imie, uzytkownicy.nazwisko, uzytkownicy.wiek, uzytkownicy.plec, uzytkownicy.adres, uzytkownicy.telefon, uzytkownicy.mail);

            string[] id = new string[returnedSelect.Count];
            string[] names = new string[returnedSelect.Count];
            string[] surnames = new string[returnedSelect.Count];
            string[] ages = new string[returnedSelect.Count];
            string[] sex = new string[returnedSelect.Count];
            string[] adresses = new string[returnedSelect.Count];
            string[] phoneNumbers = new string[returnedSelect.Count];
            string[] emails = new string[returnedSelect.Count];
            Dictionary<Enum, string> id_do_logowania = new Dictionary<Enum, string>();

            for(int i = 0; i < returnedSelect.Count; i++)
            {
                id[i] = returnedSelect[i][0];
                names[i] = returnedSelect[i][1];
                surnames[i] = returnedSelect[i][2];
                ages[i] = returnedSelect[i][3];
                sex[i] = returnedSelect[i][4];
                adresses[i] = returnedSelect[i][5];
                phoneNumbers[i] = returnedSelect[i][6];
                emails[i] = returnedSelect[i][7];
            }

            string[][] skillID = new string[id.Length][];
            string[][] experience = new string[id.Length][];
            for(int i = 0; i < id.Length; i++)
            {
                List<List<string>> skills = DataBase.SelectWhere(Tables.wybrane_umiejetnosci, new Dictionary<Enum, string>() { { wybrane_umiejetnosci.status_umiejetnosci, "'posiadana'" }, { wybrane_umiejetnosci.id_uzytkownik, id[i] } }, wybrane_umiejetnosci.id_umiejetnosci, wybrane_umiejetnosci.poziom);
                skillID[i] = new string[skills.Count - 1];
                experience[i] = new string[skills.Count - 1];
                for(int j = 0; j < skills.Count - 1;j++)
                {
                    if (skills[j].Count > 1)
                    {
                        skillID[i][j] = skills[j][0];
                        experience[i][j] = skills[j][1];
                    }
                    else
                    {
                        skillID[i][j] = "";
                        experience[i][j] = "";
                    }
                }
            }

            string[][] skillName = new string[id.Length][];

            for (int i = 0; i < id.Length;i++)
            {
                skillName[i] = new string[skillID[i].Length];
                for(int j = 0; j < skillID[i].Length; j++)
                {
                    List<List<string>> skills = DataBase.SelectWhere(Tables.umiejetnosci, new Dictionary<Enum, string>() { { umiejetnosci.id_umiejetnosci, skillID[i][j] } }, umiejetnosci.nazwa);
                    for(int k=0;k<skills.Count; k++)
                    {
                        skillName[i][k] = skills[k][0];
                    }
                }
            }

            List<List<Skill>> toCompare = new List<List<Skill>>();
            for (int i = 0; i < id.Length; i++)
            {
                toCompare.Add(new List<Skill>());
                for(int j = 0; j < skillID[i].Length; j++)
                {
                    toCompare[i].Add(new Skill(skillName[i][j],experience[i][j]));
                }
            }

            for (int i = 0; i < id.Length; i++)
            {
                if (nameCriterion != "" && surnameCriterion != "" && ageCriterion != -1 && skillsCriterion != null)
                {
                    if(names[i] == nameCriterion && surnames[i] == surnameCriterion && Convert.ToInt32(ages[i]) == ageCriterion && SkillCompare(toCompare[i], skillsCriterion))
                    {
                        int intID = Convert.ToInt32(id[i]);
                        int intAge = Convert.ToInt32(ages[i]);
                        User newUser = new User(intID, names[i], surnames[i], intAge, "", "", sex[i], adresses[i], phoneNumbers[i], emails[i]);
                        foreach(Skill s in skillsCriterion)
                        {
                            newUser.addSkillHeld(s);
                        }
                        toRet.Add(newUser);
                    }
                }
                else if (nameCriterion != "" && surnameCriterion != "" && ageCriterion != -1)
                {
                    if (names[i] == nameCriterion && surnames[i] == surnameCriterion && Convert.ToInt32(ages[i]) == ageCriterion)
                    {
                        int intID = Convert.ToInt32(id[i]);
                        int intAge = Convert.ToInt32(ages[i]);
                        User newUser = new User(intID, names[i], surnames[i], intAge, "", "", sex[i], adresses[i], phoneNumbers[i], emails[i]);
                        toRet.Add(newUser);
                    }
                }
                else if (nameCriterion != "" && surnameCriterion != "" && skillsCriterion != null)
                {
                    if (names[i] == nameCriterion && surnames[i] == surnameCriterion && SkillCompare(toCompare[i], skillsCriterion))
                    {
                        int intID = Convert.ToInt32(id[i]);
                        int intAge = Convert.ToInt32(ages[i]);
                        User newUser = new User(intID, names[i], surnames[i], intAge, "", "", sex[i], adresses[i], phoneNumbers[i], emails[i]);
                        foreach (Skill s in skillsCriterion)
                        {
                            newUser.addSkillHeld(s);
                        }
                        toRet.Add(newUser);
                    }
                }
                else if (nameCriterion != "" && ageCriterion != -1 && skillsCriterion != null)
                {
                    if (names[i] == nameCriterion && Convert.ToInt32(ages[i]) == ageCriterion && SkillCompare(toCompare[i], skillsCriterion))
                    {
                        int intID = Convert.ToInt32(id[i]);
                        int intAge = Convert.ToInt32(ages[i]);
                        User newUser = new User(intID, names[i], surnames[i], intAge, "", "", sex[i], adresses[i], phoneNumbers[i], emails[i]);
                        foreach (Skill s in skillsCriterion)
                        {
                            newUser.addSkillHeld(s);
                        }
                        toRet.Add(newUser);
                    }
                }
                else if (surnameCriterion != "" && ageCriterion != -1 && skillsCriterion != null)
                {
                    if (surnames[i] == surnameCriterion && Convert.ToInt32(ages[i]) == ageCriterion && SkillCompare(toCompare[i], skillsCriterion))
                    {
                        int intID = Convert.ToInt32(id[i]);
                        int intAge = Convert.ToInt32(ages[i]);
                        User newUser = new User(intID, names[i], surnames[i], intAge, "", "", sex[i], adresses[i], phoneNumbers[i], emails[i]);
                        foreach (Skill s in skillsCriterion)
                        {
                            newUser.addSkillHeld(s);
                        }
                        toRet.Add(newUser);
                    }
                }
                else if (nameCriterion != "" && surnameCriterion != "")
                {
                    if (names[i] == nameCriterion && surnames[i] == surnameCriterion)
                    {
                        int intID = Convert.ToInt32(id[i]);
                        int intAge = Convert.ToInt32(ages[i]);
                        User newUser = new User(intID, names[i], surnames[i], intAge, "", "", sex[i], adresses[i], phoneNumbers[i], emails[i]);
                        toRet.Add(newUser);
                    }
                }
                else if (nameCriterion != "" && ageCriterion != -1)
                {
                    if (names[i] == nameCriterion && Convert.ToInt32(ages[i]) == ageCriterion)
                    {
                        int intID = Convert.ToInt32(id[i]);
                        int intAge = Convert.ToInt32(ages[i]);
                        User newUser = new User(intID, names[i], surnames[i], intAge, "", "", sex[i], adresses[i], phoneNumbers[i], emails[i]);
                        toRet.Add(newUser);
                    }
                }
                else if (nameCriterion != "" && skillsCriterion != null)
                {
                    if (names[i] == nameCriterion && SkillCompare(toCompare[i], skillsCriterion))
                    {
                        int intID = Convert.ToInt32(id[i]);
                        int intAge = Convert.ToInt32(ages[i]);
                        User newUser = new User(intID, names[i], surnames[i], intAge, "", "", sex[i], adresses[i], phoneNumbers[i], emails[i]);
                        foreach (Skill s in skillsCriterion)
                        {
                            newUser.addSkillHeld(s);
                        }
                        toRet.Add(newUser);
                    }
                }
                else if (surnameCriterion != "" && ageCriterion != -1)
                {
                    if (surnames[i] == surnameCriterion && Convert.ToInt32(ages[i]) == ageCriterion)
                    {
                        int intID = Convert.ToInt32(id[i]);
                        int intAge = Convert.ToInt32(ages[i]);
                        User newUser = new User(intID, names[i], surnames[i], intAge, "", "", sex[i], adresses[i], phoneNumbers[i], emails[i]);
                        toRet.Add(newUser);
                    }
                }
                else if (surnameCriterion != "" && skillsCriterion != null)
                {
                    if (surnames[i] == surnameCriterion && SkillCompare(toCompare[i], skillsCriterion))
                    {
                        int intID = Convert.ToInt32(id[i]);
                        int intAge = Convert.ToInt32(ages[i]);
                        User newUser = new User(intID, names[i], surnames[i], intAge, "", "", sex[i], adresses[i], phoneNumbers[i], emails[i]);
                        foreach (Skill s in skillsCriterion)
                        {
                            newUser.addSkillHeld(s);
                        }
                        toRet.Add(newUser);
                    }
                }
                else if (ageCriterion != -1 && skillsCriterion != null)
                {
                    if (Convert.ToInt32(ages[i]) == ageCriterion && SkillCompare(toCompare[i], skillsCriterion))
                    {
                        int intID = Convert.ToInt32(id[i]);
                        int intAge = Convert.ToInt32(ages[i]);
                        User newUser = new User(intID, names[i], surnames[i], intAge, "", "", sex[i], adresses[i], phoneNumbers[i], emails[i]);
                        foreach (Skill s in skillsCriterion)
                        {
                            newUser.addSkillHeld(s);
                        }
                        toRet.Add(newUser);
                    }
                }
                else if (nameCriterion != "")
                {
                    if (names[i] == nameCriterion)
                    {
                        int intID = Convert.ToInt32(id[i]);
                        int intAge = Convert.ToInt32(ages[i]);
                        User newUser = new User(intID, names[i], surnames[i], intAge, "", "", sex[i], adresses[i], phoneNumbers[i], emails[i]);
                        toRet.Add(newUser);
                    }
                }
                else if (surnameCriterion != "")
                {
                    if (surnames[i] == surnameCriterion)
                    {
                        int intID = Convert.ToInt32(id[i]);
                        int intAge = Convert.ToInt32(ages[i]);
                        User newUser = new User(intID, names[i], surnames[i], intAge, "", "", sex[i], adresses[i], phoneNumbers[i], emails[i]);
                        toRet.Add(newUser);
                    }
                }
                else if (ageCriterion != -1)
                {
                    if (Convert.ToInt32(ages[i]) == ageCriterion)
                    {
                        int intID = Convert.ToInt32(id[i]);
                        int intAge = Convert.ToInt32(ages[i]);
                        User newUser = new User(intID, names[i], surnames[i], intAge, "", "", sex[i], adresses[i], phoneNumbers[i], emails[i]);
                        toRet.Add(newUser);
                    }
                }
                else if (skillsCriterion != null)
                {
                    if (SkillCompare(toCompare[i], skillsCriterion))
                    {
                        int intID = Convert.ToInt32(id[i]);
                        int intAge = Convert.ToInt32(ages[i]);
                        User newUser = new User(intID, names[i], surnames[i], intAge, "", "", sex[i], adresses[i], phoneNumbers[i], emails[i]);
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
                    int intAge = Convert.ToInt32(ages[i]);
                    User newUser = new User(intID, names[i], surnames[i], intAge, "", "", sex[i], adresses[i], phoneNumbers[i], emails[i]);
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

        public void SetCriterions(string _name, string _surname, int _age, List<Skill> _skills)
        {
            nameCriterion = _name;
            surnameCriterion = _surname;
            ageCriterion = _age;
            skillsCriterion = _skills;
        }

        public void SortByName()
        {
            sortByName = true;
            sortBySurname = false;
            sortByAge = false;
        }

        public void SortBySurname()
        {
            sortByName = false;
            sortBySurname = true;
            sortByAge = false;
        }

        public void SortByAge()
        {
            sortByName = false;
            sortBySurname = false;
            sortByAge = true;
        }
    }
}
