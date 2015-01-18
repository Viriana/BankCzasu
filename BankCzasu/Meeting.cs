using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankCzasu
{
    public class Meeting
    {
        public int event_ID;
        public int user_ID;
        public String eventName;
        public String Hour;
        public DateTime date;
        public Meeting(int _count, String _eventName, int _year, int _month, int _day, int _hour, User _user)
        {
            this.event_ID = _count;
            this.user_ID = _user.user_ID;
            this.eventName = _eventName;
            this.date = new DateTime(_year, _month, _day, _hour, 0, 0, 0);
        }
        public override string ToString()
        {
            return "Wydarzenie: " + this.eventName + " odbędzie się " + this.date.ToShortDateString() + " " + this.date.ToShortTimeString();
        }

    }
}
