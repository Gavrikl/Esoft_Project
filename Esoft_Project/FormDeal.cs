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
    public partial class FormDeal : Form
    {
        public FormDeal()
        {
            InitializeComponent();
            ShowSupply();
            ShowDemand();
            ShowDealSet();
        }
        void ShowSupply()
        {
            comboBoxSupply.Items.Clear();
            foreach(SupplySet supplySet in Program.wftDB.SupplySet)
            {
                string[] item =
                {
                    supplySet.Id.ToString()+ ".",
                    "риелтор;"+ supplySet.AgentsSet.LastName,
                    "клиент "+supplySet.ClientsSet.lastName
                };
                comboBoxSupply.Items.Add(string.Join(".", item));
            }
        }
        void ShowDemand()
        {
            comboBoxDemand.Items.Clear();
            foreach (DemandSet demandSet in Program.wftDB.DemandSet)
            {
                string[] item =
                {
                demandSet.Id.ToString()+".",
                 "риелтор;" + demandSet.AgentsSet.LastName,
                    "клиент " + demandSet.ClientsSet.lastName
                };
                comboBoxDemand.Items.Add(string.Join(".", item));
            }
        }


        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboBoxDemand.SelectedItem != null && comboBoxSupply.SelectedItem != null)
            {
                DealSet deal = new DealSet();
                deal.IdSupply = Convert.ToInt32(comboBoxSupply.SelectedItem.ToString().Split('.')[0]);
                deal.IdDemand = Convert.ToInt32(comboBoxDemand.SelectedItem.ToString().Split('.')[0]);
                Program.wftDB.DealSet.Add(deal);
                Program.wftDB.SaveChanges();
                ShowDealSet();
            }
            else MessageBox.Show("данные не выбраны", "ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
             

            
        }

        private void comboBoxSupply_SelectedIndexChanged(object sender, EventArgs e)
        {
            Deductions();
        }

        private void comboBoxDemand_SelectedIndexChanged(object sender, EventArgs e)
        {
            Deductions();
        }
        void Deductions()
        {
            if( comboBoxSupply.SelectedItem !=null && comboBoxDemand.SelectedItem != null)
            {
                SupplySet supplySet = Program.wftDB.SupplySet.Find(Convert.ToInt32(comboBoxSupply.SelectedItem.ToString().Split('.')[0]));
                DemandSet demandSet = Program.wftDB.DemandSet.Find(Convert.ToInt32(comboBoxDemand.SelectedItem.ToString().Split('.')[0]));
                double customerCompanyDeductions = supplySet.Price * 0.03;
                ttextCustomerCompanyDeductions.Text = customerCompanyDeductions.ToString("0.00");
                if (demandSet.AgentsSet.DealShare !=null)
                {
                    double agentCustomerDeductions = customerCompanyDeductions * Convert.ToDouble(demandSet.AgentsSet.DealShare) / 100;
                    textBoxAgentSellerDeductions.Text = agentCustomerDeductions.ToString("0.00");
                }
                else
                {
                    double agentCustomerDeductions = customerCompanyDeductions * 0.45;
                    textBoxAgentSellerDeductions.Text = agentCustomerDeductions.ToString("0.00");
                }
            }
            else
            {
                ttextCustomerCompanyDeductions.Text = " ";
                textBoxAgentSellerDeductions.Text = "";
            }
            if (comboBoxSupply.SelectedItem  != null)
            {
                SupplySet supplySet = Program.wftDB.SupplySet.Find(Convert.ToInt32(comboBoxSupply.SelectedItem.ToString().Split('.')[0]));
                double sellerCompanyDeductions;
                if ( supplySet.RealEstateSet.Type ==0)
                {
                    sellerCompanyDeductions = 36000 + supplySet.Price * 0.01;
                    textBoxSellerCompanyDeductions.Text = sellerCompanyDeductions.ToString("0.00");
                }
                else if( supplySet.RealEstateSet.Type==1)
                {
                    sellerCompanyDeductions = 30000 + supplySet.Price * 0.01;
                    textBoxSellerCompanyDeductions.Text = sellerCompanyDeductions.ToString("0.00");
                }
                else
                {
                    sellerCompanyDeductions = 30000 + supplySet.Price * 0.02;
                    textBoxSellerCompanyDeductions.Text = sellerCompanyDeductions.ToString("0.00");
                }
                if(supplySet.AgentsSet.DealShare != null)
                {
                    double agentSellerDeductions = sellerCompanyDeductions * Convert.ToDouble(supplySet.AgentsSet.DealShare) / 100;
                    textBoxAgentSellerDeductions.Text = agentSellerDeductions.ToString("0.00");
                }
                else
                {
                    double agentSellerDeductions = sellerCompanyDeductions * 0.45;
                    textBoxAgentSellerDeductions.Text = agentSellerDeductions.ToString("0.00");
                }
                
            }
            else
            {
                textBoxAgentSellerDeductions.Text = "";
                textBoxSellerCompanyDeductions.Text = "";
                ttextCustomerCompanyDeductions.Text = "";
                textBoxAgentCustomerDeductions.Text = "";
                
            }
        }
        void ShowDealSet()
        {
            listViewDealSet.Items.Clear();
            foreach(DealSet deal in Program.wftDB.DealSet)
            {
                ListViewItem item = new ListViewItem(new string[]
                {
                    deal.SupplySet.ClientsSet.lastName,
                    deal.SupplySet.AgentsSet.LastName,
                    deal.DemandSet.ClientsSet.lastName,
                    deal.DemandSet.AgentsSet.LastName,
                    deal.SupplySet.RealEstateSet.Address_City+ deal.SupplySet.RealEstateSet.Address_Hoyse+deal.SupplySet.RealEstateSet.Address_Street,
                    deal.SupplySet.Price.ToString()
                });
                item.Tag = deal;
                listViewDealSet.Items.Add(item);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if(listViewDealSet.SelectedItems.Count==1)
            {
                DealSet deal = listViewDealSet.SelectedItems[0].Tag as DealSet;
                deal.IdSupply = Convert.ToInt32(comboBoxSupply.SelectedItem.ToString().Split('.')[0]);
                deal.IdDemand = Convert.ToInt32(comboBoxDemand.SelectedItem.ToString().Split('.')[0]);
                Program.wftDB.SaveChanges();
                ShowDealSet();
            }
        }

        private void listViewDealSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listViewDealSet.SelectedItems.Count==1)
            {
                DealSet deal = listViewDealSet.SelectedItems[0].Tag as DealSet;
                comboBoxSupply.SelectedIndex = comboBoxSupply.FindString(deal.IdSupply.ToString());
                comboBoxDemand.SelectedIndex = comboBoxDemand.FindString(deal.IdSupply.ToString());

            }
            else
            {
                comboBoxSupply.SelectedItem = null;
                comboBoxDemand.SelectedItem = null;
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if(listViewDealSet.SelectedItems.Count==1)
                {
                    DealSet deal = listViewDealSet.SelectedItems[0].Tag as DealSet;
                    Program.wftDB.DealSet.Remove(deal);
                    Program.wftDB.SaveChanges();
                    ShowDealSet();
                }
                comboBoxSupply.SelectedItem = null;
                comboBoxDemand.SelectedItem = null;
            }
            catch
            {
                MessageBox.Show("не возможно удалить","ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
