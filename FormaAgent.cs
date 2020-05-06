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
    public partial class FormaAgent : Form
    {
        public FormaAgent()
        {
            InitializeComponent();
            ShowAgent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //создаём новый экземпляр класса Клиент
            AgentsSet agentSet = new AgentsSet();
            // делаем ссылку на объект, который хранится в textBox-ах
            agentSet.FirstName = textBoxFirstName.Text;
            agentSet.MiddleName = textBoxMiddleName.Text;
            agentSet.LastName = textBoxLastName.Text;
            agentSet.DealShare = Convert.ToInt32(textBoxDealShare.Text);
           

            //Добавляем в таблицу ClientsSet нового клиента clientSet
            Program.wftDb.AgentsSet.Add(agentSet);
            //Сохраняем изменения в модель wfDb (экземпляр который был создан ранее)
            Program.wftDb.SaveChanges();
            ShowAgent();
        }
        void ShowAgent()
        {
            //предварительно очищаем listView
            listViewAgent.Items.Clear();
            //проходимся по коллекции клиентов, которые находятся в базе с помощью foreach
            foreach (AgentsSet agentsSet in Program.wftDb.AgentsSet)
            {
                //создаём новый элемент в listView
                //для этого создаём новый массив строк
                ListViewItem item = new ListViewItem(new string[]
                {
                    //указваем необходимые поля
                    agentsSet.id.ToString(),agentsSet.FirstName, agentsSet.MiddleName,
                    agentsSet.LastName, agentsSet.DealShare.ToString()
                });
                //указываем по какому тегу будем брать элементы
                item.Tag = agentsSet;
                //добавляем элементы в listView для отображения
                listViewAgent.Items.Add(item);
            }
            //выравниваем колонки в listView
            listViewAgent.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

        }

        private void textBoxDealShare_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            //условие, если в listView выбран 1 элемент 
            if (listViewAgent.SelectedItems.Count == 1)
            {
                //ищем элемент из таблицы по тегу
                AgentsSet agentSet = listViewAgent.SelectedItems[0].Tag as AgentsSet;
                //указываем, что может быть измененно
                agentSet.FirstName = textBoxFirstName.Text;
                agentSet.MiddleName = textBoxMiddleName.Text;
                agentSet.LastName = textBoxLastName.Text;
                agentSet.DealShare = Convert.ToInt32(textBoxDealShare.Text);

                //Сохраняем изменения в модель wfDb (экземпляр который был создан ранее)
                Program.wftDb.SaveChanges();
                //отображение в listView
                ShowAgent();
            }


        }

        private void listViewAgent_SelectedIndexChanged(object sender, EventArgs e)
        {
            //условие, если выбран 1 элемент
            if (listViewAgent.SelectedItems.Count == 1)
            {
                //ищем элемент из таблицы по тегу 
                AgentsSet agentSet = listViewAgent.SelectedItems[0].Tag as AgentsSet;
                //указываем, что может быть измененно
                textBoxFirstName.Text = agentSet.FirstName;
                textBoxMiddleName.Text = agentSet.MiddleName;
                textBoxLastName.Text = agentSet.LastName;
                textBoxDealShare.Text = Convert.ToString(agentSet.DealShare);
                
                
            }
            else
            {
                //условие, иначе, если не выбран ни один элемент, то задаём пустые поля
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxDealShare.Text = "";

            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            // пробуем совершить действие 
            try
            {
                //еcли выбран 1 элемент из listView
                if (listViewAgent.SelectedItems.Count == 1)
                {
                    //ищем этот элемент, сверям его
                    AgentsSet agentSet = listViewAgent.SelectedItems[0].Tag as AgentsSet;
                    // удаляем из модели и базы данных
                    Program.wftDb.AgentsSet.Remove(agentSet);
                    //сохраняем измение
                    Program.wftDb.SaveChanges();
                    //отображение обновленный список
                    ShowAgent();

                }
                //очищаем textBox-ы
                textBoxFirstName.Text = "";
                textBoxMiddleName.Text = "";
                textBoxLastName.Text = "";
                textBoxDealShare.Text = "";

            }
            //если возникла какая-то ошибка, к примеру, запись используется, выводим всплывающее сообщение
            catch
            {
                //вызываем метод для всплывающего окна, в котором указываем текст, заголовок, кнопку и икону
                MessageBox.Show("Невозможно удалить, эта запись используется!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
        }
    }
}