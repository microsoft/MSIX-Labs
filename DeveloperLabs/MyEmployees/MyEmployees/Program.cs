using System;
using System.Windows.Forms;

namespace ExportDataLibrary
{
    static class Program
    {
        public static Form1 _instance;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            _instance = new Form1();
            Application.Run(_instance);
        }
    }
}
