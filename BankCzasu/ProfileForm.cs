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
    public partial class ProfileForm : Form
    {
        User currentUser = new User(5, "Marek", "Kowalski", 34, "Dandelion", "qwerty", "male", "Valley of Flowers", "555444333", "dandek@temeria.jp");
        
        public ProfileForm()
        {
            InitializeComponent();
        }


        private void ProfileForm_Load(object sender, EventArgs e)
        {
            OnProfileViewFormLoad(sender, e);
            PopulateLabels();
        }

        protected virtual void OnBtnCalendarClick(object sender, EventArgs e)
        {
            MainDebug.Log("Calendar button clicked", this);
        }

        protected virtual void OnBtnFriendsClick(object sender, EventArgs e)
        {
            MainDebug.Log("Friends button clicked", this);
        }

        protected virtual void OnProfileViewFormLoad(object sender, EventArgs e)
        {
            MainDebug.Log("Form loaded", this);
        }

        void PopulateLabels()
        {
            MainDebug.Log("Labels filled with data", this);
            lblName.Text = currentUser.name;
            lblSurnname.Text = currentUser.surname;
            lblSex.Text = currentUser.sex;
            lblPhone.Text = currentUser.phoneNumber;
            lblAge.Text = currentUser.age.ToString();
            lblEmail.Text = currentUser.emailAdress;
            lblAdress.Text = currentUser.adress;
        }

        void PopulateFLPS()
        {

        }

        void ShowAvatar()
        {

        }


    }
}
