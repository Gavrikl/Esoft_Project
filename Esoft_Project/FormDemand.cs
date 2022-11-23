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
    public partial class FormDemand : Form
    {
        public FormDemand()
        {
            InitializeComponent();
            ShowAgents();
            ShowClients();
            ShowRealEstateSet();
            comboBoxType.SelectedIndex = 0;
        }

        void ShowAgents()
        {
            //очистка
            comboBoxAgents.Items.Clear();
            foreach (AgentsSet agentsSet in Program.wftDB.AgentsSet)
            {
                string[] item =
                {
                    agentsSet.Id.ToString()+".",
                    agentsSet.FirstName,
                    agentsSet.LastName
                };
                comboBoxAgents.Items.Add(string.Join(" ", item));

            }

        }
       
        void ShowClients()
        {
            //очистка
            comboBoxClients.Items.Clear();
            foreach (ClientsSet clientsSet in Program.wftDB.ClientsSet)
            {
                string[] item =
                {
                    clientsSet.Id.ToString()+".",
                    clientsSet.FirserName,
                     clientsSet.lastName
                };
                comboBoxClients.Items.Add(string.Join(" ", item));

            }

        }

        void ShowRealEstateSet()
        {
            listViewRealEstateSet_Apartment.Items.Clear();
            listViewRealEstateSet_House.Items.Clear();
            listViewRealEstateSet_Land.Items.Clear();

            foreach (DemandSet demand in Program.wftDB.DemandSet)
            {
                if (demand.Type == 0)
                {
                    ListViewItem item = new ListViewItem(new string[]
                    {
                         demand.IdAgent.ToString(),
                         demand.IdClient.ToString(),
                         demand.MinPrice.ToString(),
                         demand.MaxPrice.ToString(),
                         demand.MinArea.ToString(),
                         demand.MaxArea .ToString(),
                         demand.MinRooms.ToString(),
                         demand.MaxRooms.ToString(),
                         demand.MinFloor.ToString(),
                         demand.MaxFloor.ToString(),
                     });
                    item.Tag = demand;
                    listViewRealEstateSet_Apartment.Items.Add(item);
                }
                else if (demand.Type == 1)
                {
                    ListViewItem item = new ListViewItem(new string[]
                    {
                         demand.IdAgent.ToString(),
                         demand.IdClient.ToString(),
                         demand.MinPrice.ToString(),
                         demand.MaxPrice.ToString(),
                         demand.MinArea.ToString(),
                         demand.MaxArea .ToString(),
                         demand.MinRooms.ToString(),
                         demand.MaxRooms.ToString(),
                         demand.MinFloors.ToString(),
                         demand.MaxFloors.ToString(),
                     });
                    item.Tag = demand;
                    listViewRealEstateSet_House.Items.Add(item);
                }
                else if (demand.Type == 2)
                {
                    ListViewItem item = new ListViewItem(new string[]
                      {
                         demand.IdAgent.ToString(),
                         demand.IdClient.ToString(),
                         demand.MinPrice.ToString(),
                         demand.MaxPrice.ToString(),
                         demand.MinArea.ToString(),
                         demand.MaxArea .ToString(),
                       });
                    item.Tag = demand;
                    listViewRealEstateSet_Land.Items.Add(item);
                }
            }
            listViewRealEstateSet_Apartment.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewRealEstateSet_House.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewRealEstateSet_Land.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

        }

        private void labelRealEstate_Click(object sender, EventArgs e)
        {

        }

        private void listViewRealEstateSet_Land_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboBoxAgents.SelectedItem != null &&
                 comboBoxClients.SelectedItem != null &&
                 comboBoxType.SelectedItem != null)

            {
                DemandSet demand = new DemandSet();
                demand.IdAgent = Convert.ToInt32(comboBoxAgents.SelectedItem.ToString().Split('.')[0]);
                demand.IdClient = Convert.ToInt32(comboBoxClients.SelectedItem.ToString().Split('.')[0]);
                demand.MinPrice = Convert.ToInt32(textBoxMinPrice.Text);
                demand.MaxPrice = Convert.ToInt32(textBoxMaxPrice.Text);
                demand.MinArea = Convert.ToInt32(textBoxMinArea.Text);
                demand.MaxArea = Convert.ToInt32(textBoxMaxArea.Text);
                if (comboBoxType.SelectedIndex == 0)
                {
                    demand.Type = 0;
                    demand.MinRooms = Convert.ToInt32(textBoxMinRooms.Text);
                    demand.MaxRooms = Convert.ToInt32(textBoxMaxRooms.Text);
                    demand.MinFloor = Convert.ToInt32(textBoxMinFloor.Text);
                    demand.MaxFloor = Convert.ToInt32(textBoxMaxFloor.Text);
                }
                else if (comboBoxType.SelectedIndex == 1)
                {
                    demand.Type = 1;
                    demand.MinRooms = Convert.ToInt32(textBoxMinRooms.Text);
                    demand.MaxRooms = Convert.ToInt32(textBoxMaxRooms.Text);
                    demand.MinFloors = Convert.ToInt32(textBoxMinFloors.Text);
                    demand.MaxFloors = Convert.ToInt32(textBoxMinFloors.Text);
                }
                else
                {
                    demand.Type = 2;
                }
                Program.wftDB.DemandSet.Add(demand);
                Program.wftDB.SaveChanges();
                ShowRealEstateSet();
                

            }

            else MessageBox.Show("данные не выбраны", "ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxType.SelectedIndex == 0)
                {
                    if (listViewRealEstateSet_Apartment.SelectedItems.Count == 1)
                    {
                        DemandSet demand = listViewRealEstateSet_Apartment.SelectedItems[0].Tag as DemandSet;
                        Program.wftDB.DemandSet.Remove(demand);
                        Program.wftDB.SaveChanges();
                        ShowRealEstateSet();

                    }
                    comboBoxAgents.Text = "";
                    comboBoxClients.Text = "";
                    textBoxMinFloor.Text = "";
                    textBoxMaxFloor.Text = "";
                    textBoxMinRooms.Text = "";
                    textBoxMaxRooms.Text = " ";
                    textBoxMinPrice.Text = "";
                    textBoxMaxPrice.Text = "";
                    textBoxMinArea.Text = "";
                    textBoxMaxArea.Text = "";

                }
                else if (comboBoxType.SelectedIndex == 1)
                {
                    if (listViewRealEstateSet_House.SelectedItems.Count == 1)
                    {
                        DemandSet demand = listViewRealEstateSet_House.SelectedItems[0].Tag as DemandSet;
                        Program.wftDB.DemandSet.Remove(demand);
                        Program.wftDB.SaveChanges();
                        ShowRealEstateSet();
                    }
                    comboBoxAgents.Text = "";
                    comboBoxClients.Text = "";
                    textBoxMinFloors.Text = "";
                    textBoxMaxFloors.Text = "";
                    textBoxMinRooms.Text = "";
                    textBoxMaxRooms.Text = " ";
                    textBoxMinPrice.Text = "";
                    textBoxMaxPrice.Text = "";
                    textBoxMinArea.Text = "";
                    textBoxMaxArea.Text = "";
                }
                else
                {
                    if (listViewRealEstateSet_Land.SelectedItems.Count == 1)
                    {
                        DemandSet demand = listViewRealEstateSet_Land.SelectedItems[0].Tag as DemandSet;
                        Program.wftDB.DemandSet.Remove(demand);
                        Program.wftDB.SaveChanges();
                        ShowRealEstateSet();
                    }
                    comboBoxAgents.Text = "";
                    comboBoxClients.Text = "";
                    textBoxMinPrice.Text = "";
                    textBoxMaxPrice.Text = "";
                    textBoxMinArea.Text = "";
                    textBoxMaxArea.Text = "";
                }
            }
            catch
            {
                MessageBox.Show(" не возможно удалить");
            }
        }
        
        

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            {
                if (comboBoxType.SelectedIndex == 0)
                {
                    if (listViewRealEstateSet_Apartment.SelectedItems.Count == 1)
                    {
                        DemandSet demand = listViewRealEstateSet_Apartment.SelectedItems[0].Tag as DemandSet;
                        demand.IdAgent = Convert.ToInt32(comboBoxAgents.SelectedItem.ToString().Split('.')[0]);
                        demand.IdClient = Convert.ToInt32(comboBoxClients.SelectedItem.ToString().Split('.')[0]);
                        demand.MinPrice = Convert.ToInt32(textBoxMinPrice.Text);
                        demand.MaxPrice = Convert.ToInt32(textBoxMaxPrice.Text);
                        demand.MinArea = Convert.ToInt32(textBoxMinArea.Text);
                        demand.MaxArea = Convert.ToInt32(textBoxMaxArea.Text);
                        demand.Type = 0;
                        demand.MinRooms = Convert.ToInt32(textBoxMinRooms.Text);
                        demand.MaxRooms = Convert.ToInt32(textBoxMaxRooms.Text);
                        demand.MinFloor = Convert.ToInt32(textBoxMinFloor.Text);
                        demand.MaxFloor = Convert.ToInt32(textBoxMaxFloor.Text);
                        Program.wftDB.SaveChanges();
                        ShowRealEstateSet();
                    }
                }
                else if (comboBoxType.SelectedIndex == 0)
                {
                    if (listViewRealEstateSet_House.SelectedItems.Count == 1)
                    {
                        DemandSet demand = listViewRealEstateSet_House.SelectedItems[0].Tag as DemandSet;
                        demand.IdAgent = Convert.ToInt32(comboBoxAgents.SelectedItem.ToString().Split('.')[0]);
                        demand.IdClient = Convert.ToInt32(comboBoxClients.SelectedItem.ToString().Split('.')[0]);
                        demand.MinPrice = Convert.ToInt32(textBoxMinPrice.Text);
                        demand.MaxPrice = Convert.ToInt32(textBoxMaxPrice.Text);
                        demand.MinArea = Convert.ToInt32(textBoxMinArea.Text);
                        demand.MaxArea = Convert.ToInt32(textBoxMaxArea.Text);
                        demand.Type = 1;
                        demand.MinFloors = Convert.ToInt32(textBoxMinFloors.Text);
                        demand.MaxFloors = Convert.ToInt32(textBoxMinFloors.Text);
                        Program.wftDB.SaveChanges();
                        ShowRealEstateSet();
                    }
                }
                else
                {
                    if (listViewRealEstateSet_Land.SelectedItems.Count == 1)
                    {
                        DemandSet demand = listViewRealEstateSet_Land.SelectedItems[0].Tag as DemandSet;
                        demand.IdAgent = Convert.ToInt32(comboBoxAgents.SelectedItem.ToString().Split('.')[0]);
                        demand.IdClient = Convert.ToInt32(comboBoxClients.SelectedItem.ToString().Split('.')[0]);
                        demand.MinPrice = Convert.ToInt32(textBoxMinPrice.Text);
                        demand.MaxPrice = Convert.ToInt32(textBoxMaxPrice.Text);
                        demand.MinArea = Convert.ToInt32(textBoxMinArea.Text);
                        demand.MaxArea = Convert.ToInt32(textBoxMaxArea.Text);
                        demand.Type = 2;
                        Program.wftDB.SaveChanges();
                        ShowRealEstateSet();
                    }
                }
            }
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
    {
        if (comboBoxType.SelectedIndex == 0)
        {
            listViewRealEstateSet_Apartment.Visible = true;
            labelMinFloor.Visible = true;
            textBoxMinFloor.Visible = true;
            labelMaxFloor.Visible = true;
            textBoxMaxFloor.Visible = true;
            labelMinRooms.Visible = true;
            textBoxMinRooms.Visible = true;
            labelMaxRooms.Visible = true;
            textBoxMaxRooms.Visible = true;

            listViewRealEstateSet_House.Visible = false;
            listViewRealEstateSet_Land.Visible = false;
            labelMinFloors.Visible = false;
            textBoxMinFloors.Visible = false;
            labelMaxFloor.Visible = false;
            textBoxMaxFloors.Visible = false;

            textBoxMinFloor.Text = "";
            textBoxMaxFloor.Text = "";
            textBoxMinRooms.Text = "";
            textBoxMaxRooms.Text = "";
        }
        else if (comboBoxType.SelectedIndex == 1)
        {
            listViewRealEstateSet_House.Visible = true;
            labelMinRooms.Visible = true;
            textBoxMinRooms.Visible = true;
            labelMaxRooms.Visible = true;
            textBoxMaxRooms.Visible = true;
            labelMinFloors.Visible = true;
            textBoxMinFloors.Visible = true;
            labelMamFloors.Visible = true;
            textBoxMaxFloors.Visible = true;

            listViewRealEstateSet_Apartment.Visible = false;
            listViewRealEstateSet_Land.Visible = false;
            labelMinFloor.Visible = false;
            textBoxMinFloor.Visible = false;
            labelMaxFloor.Visible = false;
            textBoxMaxFloor.Visible = false;
            textBoxMinFloors.Text = "";
            textBoxMaxFloors.Text = "";
            textBoxMinRooms.Text = "";
            textBoxMaxRooms.Text = "";
        }
        else if (comboBoxType.SelectedIndex == 2)
        {
            listViewRealEstateSet_Land.Visible = true;
            labelMinRooms.Visible = false;
            textBoxMinRooms.Visible = false;
            labelMaxRooms.Visible = false;
            textBoxMaxRooms.Visible = false;
            labelMinFloors.Visible = false;
            textBoxMinFloors.Visible = false;
            labelMamFloors.Visible = false;
            textBoxMaxFloors.Visible = false;

            listViewRealEstateSet_Apartment.Visible = false;
            listViewRealEstateSet_House.Visible = false;
            labelMinFloor.Visible = false;
            textBoxMinFloor.Visible = false;
            labelMaxFloor.Visible = false;
            textBoxMaxFloor.Visible = false;
        }
    }
}

        private void FormDemand_Load(object sender, EventArgs e)
        {

        }
    }
}
