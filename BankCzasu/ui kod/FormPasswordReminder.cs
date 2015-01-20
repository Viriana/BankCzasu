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
    public partial class FormPasswordReminder : Form
    {
        string email;

        public FormPasswordReminder(string email, string question)
        {
            this.email = email;
            InitializeComponent();
            lblQuestion.Text = question;
           
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string answer = tbAnswer.Text;

            if (LoginInUser.instance.RemindPassword(email, answer))
            {
                MessageBox.Show("Password sent via email", "Remind password");
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong answer", "Remind password");
            }
        }
    }
}
