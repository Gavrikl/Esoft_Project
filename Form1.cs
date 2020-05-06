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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void buttonOpenClients_Click(object sender, EventArgs e)
        {
            // Задаём новую форму из класса Клиент и открываем её
            Form formClient = new FormClient();
            formClient.Show();
        }

        private void buttonOpenAgents_Click(object sender, EventArgs e)
        {   // Задаём новую форму из класса Реллтор и открываем её
            Form formAgent = new FormaAgent();
            formAgent.Show();
        }

        private void buttonOpenRealEstates_Click(object sender, EventArgs e)
        {    //Задем новую форму из класса Объекта недвижимости и открываем её
            Form formRealEstate = new FormRealEstate();
            formRealEstate.Show();
        }
    }


}
