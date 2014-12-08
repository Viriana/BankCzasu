using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankCzasu
{
    class Test
    {
        public String nameNew;
        public String subnameNew;

        static void TestUser(string[] args)
        {
            Console.WriteLine("BANK CZASU");
            Console.WriteLine();
            //adding and testing users
            User new_1 = new User(0, "Anna", "Iksinska", 25, "kumpela", "admin1", "w", "Lodz, Piotrkowska 1 m. 1", "889-000-123", "abc@default.pl");
            User new_2 = new User(1, "Jan", "Kowalski", 22, "kumpel", "admin2", "m", "Lodz, Piotrkowska 2 m. 2", "758-333-789", "def@default.pl");
            User new_3 = new User(2, "Joanna", "Nowak", 20, "kumpelka", "admin3", "w", "Lodz, Piotrkowska 3 m. 3", "345-789-234", "ghi@default.pl");
            new_1.addFriend(new_2);
            Console.WriteLine("Lista znajomych uzytkownika {0}", new_1.ToString());
            new_1.showFriendsList();
            new_1.addFriend(new_3);
            Console.WriteLine();
            Console.WriteLine("Lista znajomych uzytkownika {0} po dodaniu znajomego", new_1.ToString());
            new_1.showFriendsList();
            new_1.deleteFriend(new_2);
            Console.WriteLine();
            Console.WriteLine("Lista znajomych uzytkownika {0} po usunieciu znajomego", new_1.ToString());
            new_1.showFriendsList();
            Console.WriteLine();
            //adding and testing skills
            Skill skill1 = new Skill("Gra na gitarze", "Zaawansowany");
            Skill skill2 = new Skill("Pranie", "Podstawowy");
            Skill skill3 = new Skill("Programowanie", "Zaawansowany");
            new_2.addSkillHeld(skill1);
            new_2.addSkillHeld(skill2);
            new_2.addSkillHeld(skill3);
            Console.WriteLine();
            Console.WriteLine("Umiejętności użytkownika {0}", new_2.ToString());
            new_2.showSkillsHeld();
            new_2.deleteSkillHeld(skill1);
            Console.WriteLine();
            Console.WriteLine("Umiejętności użytkownika {0} po usunieciu umiejętności", new_2.ToString());
            new_2.showSkillsHeld();
            Console.WriteLine();
            Console.WriteLine();
            Skill skill4 = new Skill("Gra w tenisa", "Zaawansowany");
            Skill skill5 = new Skill("Pływanie", "Podstawowy");
            Skill skill6 = new Skill("Zmywanie", "Podstawowy");
            new_3.addSkillWanted(skill4);
            new_3.addSkillWanted(skill5);
            new_3.addSkillWanted(skill6);
            Console.WriteLine("Umiejętności poszukiwane użytkownika {0}", new_3.ToString());
            new_3.showSkillsWanted();
            new_3.deleteSkillWanted(skill4);
            Console.WriteLine();
            Console.WriteLine("Umiejętności poszukiwane użytkownika {0} po usunieciu umiejętności", new_3.ToString());
            new_3.showSkillsWanted();
            Console.WriteLine();
            Console.WriteLine();
            //adding and testing meetings
            Meeting meet1 = new Meeting(0, "Wspólna nauka gry na pianinie", 2014, 12, 22, 15, new_3);
            Meeting meet2 = new Meeting(1, "Wspólne śpiewanie", 2014, 12, 30, 17, new_3);
            new_3.addMeeting(meet1);
            new_3.addMeeting(meet2);
            Console.WriteLine("Wydarzenia użytkownika {0}", new_3.ToString());
            new_3.showMeetings();
            new_3.deleteMeeting(meet1);
            Console.WriteLine();
            Console.WriteLine("Wydarzenia użytkownika {0} po usunieciu wydarzenia 1", new_3.ToString());
            new_3.showMeetings();
            Console.WriteLine("Zmiana danych użytkownika");
            Console.WriteLine("Dane użytkownika przed zmianą:");
            Console.WriteLine(new_1.ToString() + new_1.password);
            Console.WriteLine("Wprowadź nowe imie");
            String nameNew = Console.ReadLine();
            Console.WriteLine("Wprowadź nowe nazwisko");
            String subnameNew = Console.ReadLine();
            new_1.changeUserData(nameNew, subnameNew);
            Console.WriteLine("Zmienione dane");
            Console.WriteLine(new_1.ToString());
            Console.WriteLine("Zmiana hasła");
            String d = "";
            while (d != new_1.password)
            {
                Console.WriteLine("Podaj stare hasło");
                d = Console.ReadLine();
                if (d == new_1.password)
                {
                    Console.WriteLine("Podaj nowe haslo");
                    new_1.changePassword(Console.ReadLine());
                    Console.WriteLine("hasło zostało zmienione. Nowe hasło to: {0}", new_1.password);
                    break;
                }
                else Console.WriteLine("Zle haslo. Spróbuj ponownie:");

            }
            Console.ReadKey();

        }
    }
}

