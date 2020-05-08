using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Esoft_Project
{
    static class Program
    {    // создание статического экземпляра класса модели ADD.EDM
        public static WTFTutorialEntities12 wftDb = new WTFTutorialEntities12();
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormAuthorization());


        }
    

    }
    //создадим структуру для хранения данных введенного пользователя(её мы можем использовать и на других формах)

    public struct User
    {
      public string login;
      public string password;
       public string type;



    }
}
