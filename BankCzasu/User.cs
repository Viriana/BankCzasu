using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankCzasu
{
    public class User
    {
        //every user's attributes
        public String password;
        public int user_ID;
        public String name;
        public String surname;
        public int age;
        public String login;
        public String sex;
        public String adress;
        public String phoneNumber;
        public String emailAdress;

        public List<Skill> skillsHeld = new List<Skill>();
        public List<Skill> skillsWanted = new List<Skill>();
        public List<User> friends = new List<User>();
        public List<Meeting> calendar = new List<Meeting>();
        //every user's methods
        //constructor is making an object with initial attributes 
        public User(int _id, String _name, String _surname, int _age, String _login, String password, String _sex, String _adress, String _phoneNumber, String _emailAdress)
        {
            this.user_ID = _id;
            this.name = _name;
            this.surname = _surname;
            this.password = password;
            this.age = _age;
            this.login = _login;
            this.sex = _sex;
            this.adress = _adress;
            this.phoneNumber = _phoneNumber;
            this.emailAdress = _emailAdress;

        }

        /////////////////////////////////////////////////////////////////
        public void Delete() { }
        ////////////////////////////////////////////////////////////////
        public void showSkillsHeld()
        {
            foreach (var f in skillsHeld)
            {
                Console.WriteLine(f.ToString());
            }
        }
        public void showSkillsWanted()
        {
            foreach (var f in skillsWanted)
            {
                Console.WriteLine(f);
            }
        }
        public void showMeetings()
        {
            foreach (var f in calendar)
            {
                Console.WriteLine(f);
            }
        }
        public void addSkillHeld(Skill _skill)
        {
            skillsHeld.Add(_skill);
        }
        public void deleteSkillHeld(Skill _skill)
        {
            skillsHeld.Remove(_skill);
        }
        public void addSkillWanted(Skill _skill)
        {
            skillsWanted.Add(_skill);
        }
        public void deleteSkillWanted(Skill _skill)
        {
            skillsWanted.Remove(_skill);
        }
        public void addFriend(User elo)
        {
            friends.Add(elo);
        }
        public void deleteFriend(User _user)
        {
            friends.Remove(_user);
        }
        public void changeUserData(String _name, String _surname)
        {
            name = _name;
            surname = _surname;
        }
        public void changePassword(String pass)
        {
            password = pass;
        }
        public void addMeeting(Meeting meet)
        {
            calendar.Add(meet);
        }
        public void deleteMeeting(Meeting meet)
        {
            calendar.Remove(meet);
        }
        public void deleteAccount(User userdelete, int ID)
        {

            userdelete.Delete();
        }
        public void showProfile(int ID)
        {
            foreach (var f in friends.Where(_user => _user.user_ID == ID).ToArray())
            {
                Console.WriteLine(f);
                Console.WriteLine("PROFIL UZYTKOWNIKA");
            }
        }
        public void showFriendsList()
        {
            foreach (var f in friends)
            {
                Console.WriteLine(f);
            }
        }
        public override String ToString()
        {
            return this.name + " " + this.surname + " lat " + this.age;
        }
    }
}
