using ExportDataLibrary;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Storage;
using Windows.Storage.Pickers;

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

        /// <summary>
        /// Reads the input packages version data from a local file, and writes the result to the application settings container
        /// </summary>
        public static void StoreVersionData()
        {
            try
            {
                // Can be replaced with a cdn file location ex file://.../version.txt
                ApplicationData.Current.LocalSettings.Values["newVersion"] = File.ReadAllText(Form1.inputPackageVersionUri);
            }
            catch (Exception e)
            {
                ApplicationData.Current.LocalSettings.Values["newVersion"] = "0.0.0.0";
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Initiates the scenario background task
        /// </summary>
        public static void InitiateBackgroundCheck()
        {
            // For permission reasons, StoreVersionData is needed to be called for a file that is stored locally. It is not required to call StoreVersionData for web server locations
            StoreVersionData();
            BackgroundUpdateSample.BackgroundTaskImplementation();
        }

        /// <summary>
        /// Gets the main window handle of the associated process
        /// </summary>
        private static IntPtr GetMainWindowHandle()
        {
            return System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle;
        }

        /// <summary>
        /// Pops up a file picker that allows the user to pick a single file
        /// </summary>
        /// <returns>A StorageFile object that represents the file the user picked</returns>
        public static async Task<StorageFile> PickFileAsync()
        {
            // Creates the picker object and sets some of its properties
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");
            openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            // Assigns the window handle to the file pickers' UI
            IInitializeWithWindow initWindow = (IInitializeWithWindow)(object)openPicker;
            initWindow.Initialize(GetMainWindowHandle());
            // Opens the file picker for the user to pick a single file
            StorageFile file = await openPicker.PickSingleFileAsync();
            return file;
        }
    }
}
