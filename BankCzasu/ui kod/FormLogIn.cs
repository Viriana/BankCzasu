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
            MainDebug.Log("Initialized", this);


            lblLoginResult.Text = "";
            GlobalProps.formLogIn = this;
            GlobalProps.mainForm = new MainForm();
        }

        private void FormLogIn_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool result = LoginInUser.instance.Login(tbEmail.Text, tbPassword.Text);// LoginInUser.instance.Login(tbEmail.Text, tbPassword.Text);

            if (result)
            {
                MainDebug.Log("Logged in");
                this.Hide();
                GlobalProps.mainForm.Show();
                
            }
            else
            {
                lblLoginResult.Text = "Wrong login or password, try again.";
                MainDebug.Log("Login error");
            }
        }

        private void btnForgotPassword_Click(object sender, EventArgs e)
        {
            string question = LoginInUser.instance.GetQuestion(tbEmail.Text);
            FormPasswordReminder formPR = new FormPasswordReminder(tbEmail.Text, question);

            formPR.ShowDialog();
        }

        void SetCurrentUser(User user)
        {

        }

        bool ValidateLoginInfo()
        {
            return true;
        }
    }
}
