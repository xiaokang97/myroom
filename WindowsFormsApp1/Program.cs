using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.WinForm;
using WindowsFormsApp1.控件练习;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            //Application.Run(new Pratice());
            ////Application.Run(new Form2());
            Application.Run(new XtraForm1());
            //Application.Run(new ViewAndControl());
            //Application.Run(new GridCV());
        }
    }
}

