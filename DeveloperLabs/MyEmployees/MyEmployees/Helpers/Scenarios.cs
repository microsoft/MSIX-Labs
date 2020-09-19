using ExportDataLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Storage;

namespace MyEmployees.Helpers
{
    class Scenarios
    {
        static readonly string updateNowbutton_text = "Update now";
        static readonly string updateLaterbutton_text = "Update later";
        static readonly string updateAvailable = "There is an update available for My Employees";
        static readonly string upToDate = "My Employees is already up-to-date";
        static readonly string appName = "MyEmployees";

        /// <summary>
        /// Implements the UI for the app update scenario 
        /// </summary>
        /// <param name="message">The tag label of the message box</param>
        /// <param name="button1_Text">The message box button's text: Update now</param>
        /// <param name="button2_Text">The message box button's text: Update Later</param>
        public static void AppUpdateHelper(String message, String button1_Text, string button2_Text)
        {
            // Creates a message box that handles the UI and the updating of the package 
            AppUpdateMessageBox messageBox = new AppUpdateMessageBox(appName, message, button1_Text, button2_Text);
            messageBox.ShowDialog();
        }

        /// <summary>
        /// Initiates the scenario app update
        /// </summary>
        public static void InitiateAppUpdate()
        {
            bool checkForUpdate = AppUpdate.CheckforUpdate();
            if (checkForUpdate)
            {
                AppUpdateHelper(updateAvailable, updateNowbutton_text, updateLaterbutton_text);
            }
            else
            {
                MessageBox.Show(upToDate, appName, MessageBoxButtons.OK);
            }
        }
    }
}
