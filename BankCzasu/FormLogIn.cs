using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankCzasu
{
    public partial class FormLogIn : Form
    {
        public FormLogIn()
        {
            InitializeComponent();
            MainDebug.Log("Initialized form", this);
            MainDebug.Log("Debug ok");
            //Uncomment following to see SearchEngine results
            //nameCriterion - if equals "" then it's ignored
            //surnameCriterion - if equals "" then it's ignored
            //ageCriterion - if equals -1 then it's ignored
            //skillsCriterion - if equals null then it's ignored

            /*List<Skill> crit = new List<Skill>();
            crit.Add(new Skill("gra na perkusji", "8"));
            SearchEngine.Instance.SetCriterions("", "", -1, crit);
            List<User> tmp = SearchEngine.Instance.Find();
            MainDebug.Log(tmp.Count.ToString());*/
        }
    }
}
