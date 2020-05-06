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
    public partial class FormRealEstate : Form
    {
        public FormRealEstate()
        {
            InitializeComponent();
            //Так как начинаем мы с квартир, то при загрузке формы comboBoxType должен показывать строчку «Квартира» (в элементе comboBoxType это строка с индексом 0).
            comboBoxType.SelectedIndex = 0;
            ShowRealEstateSet();

        }

        private void FormRealEstate_Load(object sender, EventArgs e)
        {

        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //изменение формы, если выбрана строчка "Квартира" (её индекс 0)
            if (comboBoxType.SelectedIndex == 0)
            {
                //Делаем видимыми нужные элементы
                listViewRealEstateSet_Apartment.Visible = true;
                labelFloor.Visible = true;
                textBoxFloor.Visible = true;
                labelRooms.Visible = true;
                textBoxRooms.Visible = true;

                //Скрываем ненужные элементы
                listViewRealEstateSet_House.Visible = false;
                listViewRealEstateSet_Land.Visible = false;
                labelTotalFloors.Visible = false;
                textBoxTotalFloors.Visible = false;


                //Очищаем все видимы элементы
                textBoxAddress_City.Text = "";
                textBoxAddress_House.Text = "";
                textBoxAddress_Street.Text = "";
                textBoxAddress_Number.Text = "";
                textBoxCoordinate_latitude.Text = "";
                textBoxCoordinate_longitude.Text = "";
                textBoxTotalArea.Text = "";
                textBoxRooms.Text = "";
                textBoxFloor.Text = "";



            }
            // Изменяем формы, если выбрана строчка "Дом" (её индекс 1)
            else if (comboBoxType.SelectedIndex == 1)
            {
                //Делаем видимыми нужные элементы
                listViewRealEstateSet_House.Visible = true;
                labelTotalFloors.Visible = true;
                textBoxTotalFloors.Visible = true;


                //Скрываем ненужные элементы
                listViewRealEstateSet_Land.Visible = false;
                listViewRealEstateSet_Apartment.Visible = false;
                labelFloor.Visible = false;
                textBoxFloor.Visible = false;
                labelRooms.Visible = false;
                textBoxRooms.Visible = false;


                //Очищаем все видимые элементы
                textBoxAddress_City.Text = "";
                textBoxAddress_House.Text = "";
                textBoxAddress_Street.Text = "";
                textBoxAddress_Number.Text = "";
                textBoxCoordinate_latitude.Text = "";
                textBoxCoordinate_longitude.Text = "";
                textBoxTotalArea.Text = "";
                textBoxTotalFloors.Text = "";





            }
            //Изменение формы, если выбрана строчка "Земля" (её индекс 2)
            else if (comboBoxType.SelectedIndex == 2)
            {
                //Делаем видимыми нужные элементы 
                listViewRealEstateSet_Land.Visible = true;
                //Скрываем ненужные элементы
                listViewRealEstateSet_House.Visible = false;
                listViewRealEstateSet_Apartment.Visible = false;
                labelFloor.Visible = false;
                textBoxFloor.Visible = false;
                labelRooms.Visible = false;
                textBoxRooms.Visible = false;
                labelTotalFloors.Visible = false;
                textBoxTotalFloors.Visible = false;


                //Очищаем все видимые элементы
                textBoxAddress_City.Text = "";
                textBoxAddress_House.Text = "";
                textBoxAddress_Street.Text = "";
                textBoxAddress_Number.Text = "";
                textBoxCoordinate_latitude.Text = "";
                textBoxCoordinate_longitude.Text = "";
                textBoxTotalArea.Text = "";


            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //Создаём новый экземпляр класс Объект недвижимости
            RealEstateSet realEstate = new RealEstateSet();
            //Делаем ссылку на объект, который хранится в texBo-ах (сначала общие поля)
            realEstate.Address_City = textBoxAddress_City.Text;
            realEstate.Address_House = textBoxAddress_House.Text;
            realEstate.Address_Street = textBoxAddress_Street.Text;
            realEstate.Address_Number = textBoxAddress_Number.Text;
            realEstate.Coordinate_latitude = Convert.ToDouble(textBoxCoordinate_latitude.Text);
            realEstate.Coordinate_longitude = Convert.ToDouble(textBoxCoordinate_longitude.Text);
            realEstate.TotalArea = Convert.ToDouble(textBoxTotalArea.Text);
            //Дополнительные поля для типа "Квартира"
            if (comboBoxType.SelectedIndex == 0)
            {
                realEstate.Type = 0;
                realEstate.Rooms = Convert.ToInt32(textBoxRooms.Text);
                realEstate.Floor = Convert.ToInt32(textBoxFloor.Text);

            }
            //Дополнительные поля для типа "Дом"
            else if (comboBoxType.SelectedIndex == 1)
            {
                realEstate.Type = 1;
                realEstate.TotalFloors = Convert.ToInt32(textBoxTotalFloors.Text);

            }

            //Дополнительные поля для типа "Земли"
            else
            {
                realEstate.Type = 2;

            }
            //Добавляем в таблицу RealEstateSet новый объект недвижимости realEstate
            Program.wftDb.RealEstateSet.Add(realEstate);
            //Сохраняем изменение в модели wftDb
            Program.wftDb.SaveChanges();
            ShowRealEstateSet();
        }
        void ShowRealEstateSet()
        {
            //Предварительно очищаем все listView
            listViewRealEstateSet_Apartment.Items.Clear();
            listViewRealEstateSet_House.Items.Clear();
            listViewRealEstateSet_Land.Items.Clear();

            //Проходим по коллекции клиентов, которые находятся в базе с помощью foreach
            foreach (RealEstateSet realEstate in Program.wftDb.RealEstateSet)
            {
                //Отображение квартир в listViewRealEstateSet_Apartment
                if (realEstate.Type == 0)
                {
                    //созададим новый элемент в listViewReakEstateSet_Apartment с помощью массива строк
                    ListViewItem item = new ListViewItem(new string[]
                {
                  //указываем необходимые поля
                   realEstate.Address_City,realEstate.Address_Street,realEstate.Address_House,
                   realEstate.Address_Number, realEstate.Coordinate_latitude.ToString(),
                   realEstate.Coordinate_longitude.ToString(),realEstate.TotalArea.ToString(),
                   realEstate.Rooms.ToString(), realEstate.Floor.ToString()

                });
                    //указываем по какому тегу выбраны элементы
                    item.Tag = realEstate;
                    //добавляем элементы в listViewRealEstateSet_Apartment для отображения
                    listViewRealEstateSet_Apartment.Items.Add(item);



                }
                //отображение домов в listViewRealEstateSet_House
                else if (realEstate.Type == 1)
                {
                    //созададим новый элемент в listViewReakEstateSet_House с помощью массива строк
                    ListViewItem item = new ListViewItem(new string[]
                 {
                 realEstate.Address_City,realEstate.Address_Street,realEstate.Address_House,
                  realEstate.Address_Number, realEstate.Coordinate_latitude.ToString(),
                  realEstate.Coordinate_longitude.ToString(),realEstate.TotalArea.ToString(),
                  realEstate.TotalFloors.ToString()
                 });
                    //указываем по какому тегу выбраны элементы
                    item.Tag = realEstate;
                    //добавляем элементы в listViewRealEstateSet_House для отображения
                    listViewRealEstateSet_House.Items.Add(item);

                }
                else
                {
                    ///созададим новый элемент в listViewReakEstateSet_Land с помощью массива строк
                    ListViewItem item = new ListViewItem(new string[]
                {
                    realEstate.Address_City,realEstate.Address_Street,realEstate.Address_House,
                  realEstate.Address_Number, realEstate.Coordinate_latitude.ToString(),
                  realEstate.Coordinate_longitude.ToString(),realEstate.TotalArea.ToString()
                  


                });
                    //указываем по какому тегу выбраны элементы
                    item.Tag = realEstate;
                    //добавляем элементы в listViewRealEstateSet_Land для отображения
                    listViewRealEstateSet_Land.Items.Add(item);


                }

            }

            //выравниваниваем столбцы во всех listView
            listViewRealEstateSet_Apartment.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewRealEstateSet_House.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            listViewRealEstateSet_Land.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

        }

       private void buttonEdit_Click(object sender, EventArgs e) 
{ 
//выбран тип "Квартира", работа с listViewRealEstateSet_Apartment 
if (comboBoxType.SelectedIndex == 0) 
{ 
//если в listView выбран элемент 
if (listViewRealEstateSet_Apartment.SelectedItems.Count == 1) 
{ 
//Ищем элемент из таблицы по тегу 
RealEstateSet realEstate = listViewRealEstateSet_Apartment.SelectedItems[0].Tag as RealEstateSet; 
//указываем, что может быть изменено 
realEstate.Address_City = textBoxAddress_City.Text; 
realEstate.Address_House = textBoxAddress_House.Text; 
realEstate.Address_Street = textBoxAddress_Street.Text; 
realEstate.Address_Number = textBoxAddress_Number.Text; 
realEstate.Coordinate_latitude = Convert.ToDouble(textBoxCoordinate_latitude.Text); 
realEstate.Coordinate_longitude = Convert.ToDouble(textBoxCoordinate_longitude.Text); 
realEstate.TotalArea = Convert.ToDouble(textBoxTotalArea.Text); 
realEstate.Rooms = Convert.ToInt32(textBoxRooms.Text); 
realEstate.Floor = Convert.ToInt32(textBoxFloor.Text); 
//сохраняем изменения в модели wftDb 
Program.wftDb.SaveChanges(); 
//отображаем в listViewRealEstateSet_Apartment 
ShowRealEstateSet(); 
} 
} 
//выбран тип "Дом", работа с listViewRealEstateSet_House 
else if (comboBoxType.SelectedIndex == 1) 
{ 
//если в listView выбран элемент 
if (listViewRealEstateSet_House.SelectedItems.Count == 1) 
{ 
//Ищем элемент из таблицы по тегу 
RealEstateSet realEstate = listViewRealEstateSet_House.SelectedItems[0].Tag as RealEstateSet; 
//указываем, что может быть изменено 
realEstate.Address_City = textBoxAddress_City.Text; 
realEstate.Address_House = textBoxAddress_House.Text; 
realEstate.Address_Street = textBoxAddress_Street.Text; 
realEstate.Address_Number = textBoxAddress_Number.Text; 
realEstate.Coordinate_latitude = Convert.ToDouble(textBoxCoordinate_latitude.Text); 
realEstate.Coordinate_longitude = Convert.ToDouble(textBoxCoordinate_longitude.Text); 
realEstate.TotalArea = Convert.ToDouble(textBoxTotalArea.Text); 
realEstate.TotalFloors = Convert.ToInt32(textBoxTotalFloors.Text); 
//сохраняем изменения в модели wftDb 
Program.wftDb.SaveChanges(); 
//отображаем в listViewRealEstateSet_House 
ShowRealEstateSet(); 
} 
} 
//выбран тип "Земля", работа с listViewRealEstateSet_Land 
else 
{ 
//если в listView выбран элемент 
if (listViewRealEstateSet_Land.SelectedItems.Count == 1) 
{ 
//Ищем элемент из таблицы по тегу 
RealEstateSet realEstate = listViewRealEstateSet_Land.SelectedItems[0].Tag as RealEstateSet; 
//указываем, что может быть изменено 
realEstate.Address_City = textBoxAddress_City.Text; 
realEstate.Address_House = textBoxAddress_House.Text; 
realEstate.Address_Street = textBoxAddress_Street.Text; 
realEstate.Address_Number = textBoxAddress_Number.Text; 
realEstate.Coordinate_latitude = Convert.ToDouble(textBoxCoordinate_latitude.Text); 
realEstate.Coordinate_longitude = Convert.ToDouble(textBoxCoordinate_longitude.Text); 
realEstate.TotalArea = Convert.ToDouble(textBoxTotalArea.Text); 
//сохраняем изменения в модели wftDb 
Program.wftDb.SaveChanges(); 
//отображаем в listViewRealEstateSet_House 
ShowRealEstateSet(); 
} 
} 

}


                    

                


            


        

        private void listViewRealEstateSet_Apartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            //если выбран один элемент 
            if (listViewRealEstateSet_Apartment.SelectedItems.Count == 1)
            {   //изем хлемент из таблицы по тегу
                RealEstateSet realEstate = listViewRealEstateSet_Apartment.SelectedItems[0].Tag as RealEstateSet;
                //указываем, что может быть измененно
                textBoxAddress_City.Text = realEstate.Address_City;
                textBoxAddress_Street.Text = realEstate.Address_Street;
                textBoxAddress_House.Text = realEstate.Address_House;
                textBoxAddress_Number.Text = realEstate.Address_Number;
                textBoxCoordinate_latitude.Text = realEstate.Coordinate_latitude.ToString();
                textBoxCoordinate_longitude.Text = realEstate.Coordinate_longitude.ToString();
                textBoxTotalArea.Text = realEstate.TotalArea.ToString();
                textBoxRooms.Text = realEstate.Rooms.ToString();
                textBoxFloor.Text = realEstate.Floor.ToString();


            }
            else
            {

                //если не выбран ни один элемент, задаём пустые поля
                textBoxAddress_City.Text = "";
                textBoxAddress_House.Text = "";
                textBoxAddress_Street.Text = "";
                textBoxAddress_Number.Text = "";
                textBoxCoordinate_latitude.Text = "";
                textBoxCoordinate_longitude.Text = "";
                textBoxTotalArea.Text = "";
                textBoxRooms.Text = "";
                textBoxFloor.Text = "";

            }
        }

        private void listViewRealEstateSet_House_SelectedIndexChanged(object sender, EventArgs e)
        {
            //если выбран один элемент 
            if (listViewRealEstateSet_House.SelectedItems.Count == 1)
            {   //изем хлемент из таблицы по тегу
                RealEstateSet realEstate = listViewRealEstateSet_House.SelectedItems[0].Tag as RealEstateSet;
                //указываем, что может быть измененно
                textBoxAddress_City.Text = realEstate.Address_City;
                textBoxAddress_Street.Text = realEstate.Address_Street;
                textBoxAddress_House.Text = realEstate.Address_House;
                textBoxAddress_Number.Text = realEstate.Address_Number;
                textBoxCoordinate_latitude.Text = realEstate.Coordinate_latitude.ToString();
                textBoxCoordinate_longitude.Text = realEstate.Coordinate_longitude.ToString();
                textBoxTotalArea.Text = realEstate.TotalArea.ToString();
                textBoxTotalFloors.Text = realEstate.TotalFloors.ToString();


            }
            else
            {

                //если не выбран ни один элемент, задаём пустые поля
                textBoxAddress_City.Text = "";
                textBoxAddress_House.Text = "";
                textBoxAddress_Street.Text = "";
                textBoxAddress_Number.Text = "";
                textBoxCoordinate_latitude.Text = "";
                textBoxCoordinate_longitude.Text = "";
                textBoxTotalArea.Text = "";
                textBoxTotalFloors.Text = "";
            }
        }

        private void listViewRealEstateSet_Land_SelectedIndexChanged(object sender, EventArgs e)
        {
            //если выбран один элемент 
            if (listViewRealEstateSet_Land.SelectedItems.Count == 1)
            {   //изем хлемент из таблицы по тегу
                RealEstateSet realEstate = listViewRealEstateSet_Land.SelectedItems[0].Tag as RealEstateSet;
                //указываем, что может быть измененно
                textBoxAddress_City.Text = realEstate.Address_City;
                textBoxAddress_Street.Text = realEstate.Address_Street;
                textBoxAddress_House.Text = realEstate.Address_House;
                textBoxAddress_Number.Text = realEstate.Address_Number;
                textBoxCoordinate_latitude.Text = realEstate.Coordinate_latitude.ToString();
                textBoxCoordinate_longitude.Text = realEstate.Coordinate_longitude.ToString();
                textBoxTotalArea.Text = realEstate.TotalArea.ToString();


            }
            else
            {

                //если не выбран ни один элемент, задаём пустые поля
                textBoxAddress_City.Text = "";
                textBoxAddress_House.Text = "";
                textBoxAddress_Street.Text = "";
                textBoxAddress_Number.Text = "";
                textBoxCoordinate_latitude.Text = "";
                textBoxCoordinate_longitude.Text = "";
                textBoxTotalArea.Text = "";

            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            //пробуем совершить действие 
            try
            {
                //выбран тип "Квартира", работа с listViewRealEstateSet_Apartment
                if (comboBoxType.SelectedIndex == 0)
                {  //если в listViem выбран элемент
                    if (listViewRealEstateSet_Apartment.SelectedItems.Count == 1)
                    {
                        //ищем этот элемент базе по тегу
                        RealEstateSet realEstate = listViewRealEstateSet_Apartment.SelectedItems[0].Tag as RealEstateSet;
                        //удаляем из модели базы данных
                        Program.wftDb.RealEstateSet.Remove(realEstate);
                        //сохраняем изменения
                        Program.wftDb.SaveChanges();
                        //отображаем обновление список
                        ShowRealEstateSet();



                    }
                    //очищаем текстовые поля
                    textBoxAddress_City.Text = "";
                    textBoxAddress_House.Text = "";
                    textBoxAddress_Street.Text = "";
                    textBoxAddress_Number.Text = "";
                    textBoxCoordinate_latitude.Text = "";
                    textBoxCoordinate_longitude.Text = "";
                    textBoxTotalArea.Text = "";
                    textBoxRooms.Text = "";
                    textBoxFloor.Text = "";

                }
                else if(comboBoxType.SelectedIndex == 1)
                {
                    if (listViewRealEstateSet_House.SelectedItems.Count == 1)
                    {
                        //ищем этот элемент базе по тегу
                        RealEstateSet realEstate = listViewRealEstateSet_House.SelectedItems[0].Tag as RealEstateSet;
                        //удаляем из модели базы данных
                        Program.wftDb.RealEstateSet.Remove(realEstate);
                        //сохраняем изменения
                        Program.wftDb.SaveChanges();
                        //отображаем обновление список
                        ShowRealEstateSet();



                    }
                    //очищаем текстовые поля
                    textBoxAddress_City.Text = "";
                    textBoxAddress_House.Text = "";
                    textBoxAddress_Street.Text = "";
                    textBoxAddress_Number.Text = "";
                    textBoxCoordinate_latitude.Text = "";
                    textBoxCoordinate_longitude.Text = "";
                    textBoxTotalArea.Text = "";
                    textBoxTotalFloors.Text = "";

                }
           
                else
                {
                    if (listViewRealEstateSet_Land.SelectedItems.Count == 1)
                    {
                        //ищем этот элемент базе по тегу
                        RealEstateSet realEstate = listViewRealEstateSet_Land.SelectedItems[0].Tag as RealEstateSet;
                        //удаляем из модели базы данных
                        Program.wftDb.RealEstateSet.Remove(realEstate);
                        //сохраняем изменения
                        Program.wftDb.SaveChanges();
                        //отображаем обновление список
                        ShowRealEstateSet();



                    }
                    //очищаем текстовые поля
                    textBoxAddress_City.Text = "";
                    textBoxAddress_House.Text = "";
                    textBoxAddress_Street.Text = "";
                    textBoxAddress_Number.Text = "";
                    textBoxCoordinate_latitude.Text = "";
                    textBoxCoordinate_longitude.Text = "";
                   
                  

                }
            
            
            }
        //если возникает ошибка 
            catch
            {
                MessageBox.Show("Невозможно удалить, эта запись используется", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }

    }
}
