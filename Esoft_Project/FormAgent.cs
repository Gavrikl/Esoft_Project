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
    public partial class FormAgent : Form
    {
        public FormAgent()
        {
            InitializeComponent();
            ShowAgent();
        }
        void ShowAgent()
        {
            listViewAgent.Items.Clear();
            foreach (AgentsSet agentsSet in Program.wftDB.AgentsSet)
            {
                ListViewItem item = new ListViewItem(new string[]
                {
                    agentsSet.Id.ToString(),
                        agentsSet.FirstName ,
                        agentsSet.MiddleName,
                        agentsSet.LastName ,
                       Convert.ToString (agentsSet.DealShare)
                });
                item.Tag = agentsSet;
                listViewAgent.Items.Add(item);
            }
            listViewAgent.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AgentsSet agentsSet = new AgentsSet();
            agentsSet.FirstName = textBoxFirstName.Text;
            agentsSet.MiddleName = textBoxMiddleName.Text;
            agentsSet.LastName = textBoxLastName.Text;
            agentsSet.DealShare = Convert.ToInt32(textBoxDealshare.Text);
            Program.wftDB.AgentsSet.Add(agentsSet);
            Program.wftDB.SaveChanges();
            ShowAgent();

        }

        private void listViewAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewAgent.SelectedItems.Count == 1)
            {
                AgentsSet agentsSet = listViewAgent.SelectedItems[0].Tag as AgentsSet;
                textBoxFirstName.Text = agentsSet.FirstName;
                textBoxMiddleName.Text = agentsSet.MiddleName;
                textBoxLastName.Text = agentsSet.LastName;
                textBoxDealshare.Text = Convert.ToString(agentsSet.DealShare);


            }
            else
            {
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxDealshare.Text = "";


            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listViewAgent.SelectedItems.Count == 1)
            {
                AgentsSet agentsSet = listViewAgent.SelectedItems[0].Tag as AgentsSet;
                agentsSet.FirstName = textBoxFirstName.Text;
                agentsSet.MiddleName = textBoxMiddleName.Text;
                agentsSet.LastName = textBoxLastName.Text;
                agentsSet.DealShare = Convert.ToInt32(textBoxDealshare.Text);
                Program.wftDB.SaveChanges();
                ShowAgent();
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {


                if (listViewAgent.SelectedItems.Count == 1)
                {
                    AgentsSet agentsSet = listViewAgent.SelectedItems[0].Tag as AgentsSet;
                    Program.wftDB.AgentsSet.Remove(agentsSet);
                    Program.wftDB.SaveChanges();
                    ShowAgent();
                }
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxDealshare.Text = "";
            }
            catch
            {
                MessageBox.Show("не возможно удалить эту запись, эта запись используется!", "ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
