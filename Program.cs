using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Esoft_Project
{
    static class Program
    {    // создание статического экземпляра класса модели ADD.EDM
        public static WTFTutorialEntities5 wftDb = new WTFTutorialEntities5();
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Menu());
            Application.Run(new FormClient());
            Application.Run(new FormaAgent());
            Application.Run(new FormRealEstate());
        }
    }
}
