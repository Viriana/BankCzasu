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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnMyProfile_Click(object sender, EventArgs e)
        {
            MyProfileViewForm form = new MyProfileViewForm();
            form.ShowDialog();
        }

        private void TestProfileForm_Click(object sender, EventArgs e)
        {
            ProfileForm form = new ProfileForm();
            form.ShowDialog();
        }

        private void ChatButton_Click(object sender, EventArgs e)
        {
            Chat form = new Chat();
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SearchUsersForm form = new SearchUsersForm();
            form.ShowDialog();
        }
    }
}
