using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Esoft_Project
{       public struct User
             {
              public string login;
              public string password;
              public string type;
             }
    public partial class FormAuthorization : Form
    {
       
         public static User users= new User();
        public FormAuthorization()
        {
            InitializeComponent();
        }

        private void FormAuthorization_Load(object sender, EventArgs e)
        {

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if  (textBoxLogin.Text == " " && textBoxPassword.Text== " ")
            {
                MessageBox.Show("введите данные", "ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                bool key = false;
                foreach (Users user in Program.wftDB.Users)
                {
                    if( textBoxLogin.Text == user.Login && textBoxPassword.Text== user.Password)
                    {
                        key = true;
                        users.login = user.Login ;
                        users.password = user.Password;
                        users.type = user.Type;
                    }
                }
                if(!key)
                {
                    MessageBox.Show("проверьте данные", "пользователь не найден", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBoxLogin.Text = " ";
                    textBoxPassword.Text = " ";
                }
                else
                {
                    
                    Menu menu=new Menu();
                    menu.Show();
                    this.Hide();

                }
            }
        }
    }
}
