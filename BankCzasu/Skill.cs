using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankCzasu
{
    class Skill
    {
        public String name;
        public String exp_level;

        public Skill(String _name, String _exp_level)
        {
            this.name = _name;
            this.exp_level = _exp_level;
        }
        public override string ToString()
        {
            return "Skill: " + this.name + "\tPoziom: " + this.exp_level;
        }
    }
}
