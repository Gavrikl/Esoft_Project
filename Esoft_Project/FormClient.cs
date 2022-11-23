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
{
    public partial class FormClient : Form
    {
        public FormClient()
        {
            InitializeComponent();
            ShowClient();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // СОЗДАНИЕ НОВОГО ЭКЗЕМПЛЯРА КЛАССА КЛИЕНТА
            ClientsSet clientsSet = new ClientsSet();
            //ссылки на обьекты которые в текс боксе
            clientsSet.FirserName = textBoxFirstName.Text;
            clientsSet.MiddleName = textBoxMiddleName.Text;
            clientsSet.lastName = textBoxLastName.Text;
            clientsSet.Phome = textBoxPhone.Text;
            clientsSet.Email = textBoxEmail.Text;
            Program.wftDB.ClientsSet.Add(clientsSet);
            Program.wftDB.SaveChanges();
            ShowClient();
            //






        }
        void ShowClient()
        {
            ListViewClient.Items.Clear();
            foreach (ClientsSet clientsSet in Program.wftDB.ClientsSet)
            {
                ListViewItem item = new ListViewItem(new string[]
                    {
                        clientsSet.Id.ToString(), 
                        clientsSet.FirserName,
                        clientsSet.MiddleName, 
                        clientsSet.lastName,
                        clientsSet.Phome, 
                        clientsSet.Email
                    });
                item.Tag = clientsSet;
                ListViewClient.Items.Add(item);
            }
            ListViewClient.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize); 
        }


        private void listViewClien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListViewClient.SelectedItems.Count == 1)
            {
                ClientsSet clientsSet = ListViewClient.SelectedItems[0].Tag as ClientsSet;
                textBoxFirstName.Text = clientsSet.FirserName;
                textBoxMiddleName.Text = clientsSet.MiddleName;
                textBoxLastName.Text = clientsSet.lastName;
                textBoxPhone.Text = clientsSet.Phome;
                textBoxEmail.Text = clientsSet.Email;

            }
            else
            {
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxPhone.Text = "";
                textBoxEmail.Text = "";
            }
        }

        private void FormClient_Load(object sender, EventArgs e)
        {
            
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (ListViewClient.SelectedItems.Count == 1)
            {
                ClientsSet clientsSet = ListViewClient.SelectedItems[0].Tag as ClientsSet;
                clientsSet.FirserName = textBoxFirstName.Text;
                clientsSet.MiddleName = textBoxMiddleName.Text;
                clientsSet.lastName = textBoxLastName.Text;
                clientsSet.Phome = textBoxPhone.Text;
                clientsSet.Email = textBoxEmail.Text;
                Program.wftDB.SaveChanges();
                ShowClient();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                
                
                if(ListViewClient.SelectedItems.Count == 1)
                {
                    ClientsSet clientsSet = ListViewClient.SelectedItems[0].Tag as ClientsSet;
                    Program.wftDB.ClientsSet.Remove(clientsSet);
                    Program.wftDB.SaveChanges();
                    ShowClient();
                }
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxPhone.Text = "";
                textBoxEmail.Text = "";
            }
            catch
            {
                MessageBox.Show("не возможно удалить эту запись, эта запись используется!", "ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
