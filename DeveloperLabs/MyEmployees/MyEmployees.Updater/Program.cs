using MyEmployeesUpdater.ComUpdater;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.ApplicationModel.Background;

namespace MyEmployeesUpdater
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// This application's main function is to update the MyEmployees application whenever a newer version package is available.
        /// </summary>
        [STAThread]
        static void Main()
        {
            BackgroundUpdateRegister registerBackgroundUpdate = new BackgroundUpdateRegister();
            TimeTrigger trigger = new TimeTrigger(15, false);
            registerBackgroundUpdate.RegisterBackgroundTaskWithSystem("BackgroundUpdater", trigger);
        }
    }
}
