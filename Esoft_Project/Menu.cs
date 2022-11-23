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
            if (FormAuthorization.users.type == "agent") buttonOpenAgents.Enabled = false;
            labelHello.Text = "Привествую тебя, !" + FormAuthorization.users.login;
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void buttonOpenClients_Click(object sender, EventArgs e)
        {
            //форма из класса клиент открывам
            Form formClient = new FormClient();
            formClient.Show();
        }

        private void buttonOpenAgents_Click(object sender, EventArgs e)
        {
            Form FormAgent = new FormAgent();
            FormAgent.Show();
        }

        private void buttonOpenRealEstates_Click(object sender, EventArgs e)
        {
            Form formRealEstate = new FormRealEstate();
            formRealEstate.Show();
        }

        private void buttonOpenSupplies_Click(object sender, EventArgs e)
        {
            Form FormDemand = new FormDemand();
            FormDemand.Show();
        }

        private void buttonOpenDemands_Click(object sender, EventArgs e)
        {
            Form formSupply = new FormSupply();
            formSupply.Show();

        }

        private void buttonOpenDeals_Click(object sender, EventArgs e)
        {
            Form FormDeal = new FormDeal();
            FormDeal.Show();
        }
    }
}
