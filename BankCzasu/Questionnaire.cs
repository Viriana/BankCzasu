using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankCzasu
{
    class Questionnaire
    {
        public DateTime date;
        public String subject;
        public String additional;

        public Questionnaire(DateTime _date, String _subject, String _additional)
        {
	    this.date = _date;
	    this.subject = _subject;
	    this.additional = _additional;
        }
        public override string ToString()
        {
            return "Data: " + this.date + "\tTemat: " + this.subject + "\tDodatkowe: " + this.additional;
        }
    }
}
