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
            //создаём новый экземпляр класса Клиент
            ClientsSet clientSet = new ClientsSet();
            // делаем ссылку на объект, который хранится в textBox-ах
            clientSet.FirstName = textBoxFirstName.Text;
            clientSet.MiddleName = textBoxMiddleName.Text;
            clientSet.LastName = textBoxLastName.Text;
            clientSet.Phone = textBoxPhone.Text;
            clientSet.Email = textBoxEmail.Text;
            //Добавляем в таблицу ClientsSet нового клиента clientSet
            Program.wftDb.ClientsSet.Add(clientSet);
            //Сохраняем изменения в модель wfDb (экземпляр который был создан ранее)
            Program.wftDb.SaveChanges();
            ShowClient();
       }
    void ShowClient()
        {
        //предварительно очищаем listView
            listViewClient.Items.Clear();
        //проходимся по коллекции клиентов, которые находятся в базе с помощью foreach
        foreach (ClientsSet clientsSet in Program.wftDb.ClientsSet)
        {
            //создаём новый элемент в listView
            //для этого создаём новый массив строк
            ListViewItem item = new ListViewItem(new string[]
                {
                    //указваем необходимые поля
                    clientsSet.id.ToString(),clientsSet.FirstName, clientsSet.MiddleName,
                    clientsSet.LastName, clientsSet.Phone,clientsSet.Email
                });
        //указываем по какому тегу будем брать элементы
            item.Tag = clientsSet;
            //добавляем элементы в listView для отображения
            listViewClient.Items.Add(item);
        }
        //выравниваем колонки в listView
        listViewClient.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

        }

    private void buttonEdit_Click(object sender, EventArgs e)
    {
        //условие, если в listView выбран 1 элемент 
        if (listViewClient.SelectedItems.Count == 1)
        {
            //ищем элемент из таблицы по тегу
            ClientsSet clientSet = listViewClient.SelectedItems[0].Tag as ClientsSet;
            //указываем, что может быть измененно
            clientSet.FirstName = textBoxFirstName.Text;
            clientSet.MiddleName = textBoxMiddleName.Text;
            clientSet.LastName = textBoxLastName.Text;
            clientSet.Phone = textBoxPhone.Text;
            clientSet.Email = textBoxEmail.Text;
            //Сохраняем изменения в модель wfDb (экземпляр который был создан ранее)
            Program.wftDb.SaveChanges();
            //отображение в listView
            ShowClient();
        }
    }

    private void listViewClient_SelectedIndexChanged(object sender, EventArgs e)
    {
        //условие, если выбран 1 элемент
        if (listViewClient.SelectedItems.Count == 1)
        {
            //ищем элемент из таблицы по тегу 
            ClientsSet clientSet = listViewClient.SelectedItems[0].Tag as ClientsSet;
            //указываем, что может быть измененно
            textBoxFirstName.Text = clientSet.FirstName;
            textBoxMiddleName.Text = clientSet.MiddleName;
            textBoxLastName.Text = clientSet.LastName;
            textBoxPhone.Text = clientSet.Phone;
            textBoxEmail.Text = clientSet.Email;
        }
        else
        {
            //условие, иначе, если не выбран ни один элемент, то задаём пустые поля
            textBoxFirstName.Text = "";
            textBoxMiddleName.Text = "";
            textBoxLastName.Text = "";
            textBoxPhone.Text = "";
            textBoxEmail.Text = "";

        }
    
    }

    private void buttonDel_Click(object sender, EventArgs e)
    {
    // пробуем совершить действие 
        try
        {
            //еcли выбран 1 элемент из listView
            if (listViewClient.SelectedItems.Count == 1)

            {
                //ищем этот элемент, сверям его
                ClientsSet clientSet = listViewClient.SelectedItems[0].Tag as ClientsSet;
                // удаляем из модели и базы данных
                Program.wftDb.ClientsSet.Remove(clientSet);
                //сохраняем измение
                Program.wftDb.SaveChanges();
                //отображение обновленный список
                ShowClient();

            }
        //очищаем textBox-ы
            textBoxFirstName.Text = "";
            textBoxMiddleName.Text = "";
            textBoxLastName.Text = "";
            textBoxPhone.Text = "";
            textBoxEmail.Text = "";
        
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
    


