using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BankCzasu
{
    class Rates
    {
        public int user_ID;
        public int mark;
        public int meet;
        public DateTime available;
        public String subject;
        public String additional;
        public String comment;
        public List<Rates> RatesScale = new List<Rates>();
        //setting mark for user
        public void setMark(int _mark)
        {
            this.mark = _mark;
        }
        //setting comment for user
        public void setComment(String _comment)
        {
            this.comment = _comment;
        }
        //setting availability for meeting
        public void Available(DateTime _available)
        {
            this.available = _available;
        }
        //setting theme for meeting
        public void setSubject(String _subject)
        {
            this.subject = _subject;
        }
        //setting additional informations about meeting
        public void setAdditional(String _additional)
        {
            this.additional = _additional;
        }
        public override string ToString()
        {
            return "Wystawiłeś ocenę: " + this.mark + "użytkownikowi " + this.user_ID;
        }
    }
}