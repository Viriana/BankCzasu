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
    public partial class MyProfileViewForm : ProfileForm
    {
        public MyProfileViewForm()
        {
            currentUser = LoginInUser.instance.logInUser;
            InitializeComponent();
        }

        void OnBtnEditClick(Object sender, EventArgs e)
        {
            MainDebug.Log("Button edit clicked", this);
        }
    }
}
