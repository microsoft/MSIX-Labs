using ExportDataLibrary;
using System;
using System.Drawing;
using System.Drawing.Imaging;
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
        public static Image shareImage = null;

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

        /// <summary>
        /// Launches the default email app and creates a new message with the specified email address
        /// </summary>
        /// <param name="email">The specified email address</param>
        public static async void LaunchMailApp(string email)
        {
            String path = "mailto:" + email;
            var success = await Windows.System.Launcher.LaunchUriAsync(new Uri(path));
            if (!success)
            {
                MessageBox.Show("The mail uri launcher has failed");
            }
        }

        /// <summary>
        /// Launches the default app associated with the specified file (in this case the Photos app)
        /// </summary>
        /// <param name="file">The specified file</param>
        public static async void LaunchPhotosApp(StorageFile file)
        {
            // The URI to launch
            var success = await Windows.System.Launcher.LaunchFileAsync(file);
            if (!success)
            {
                MessageBox.Show("The file launcher has failed");
            }
        }

        /// <summary>
        /// Launches the Windows Maps app and passes along a query
        /// </summary>
        /// <param name="address">The address that is passed as part of the query</param>
        public static async void LaunchMapsApp(String address)
        {
            String query = "?Where=" + address;
            String path = Path.Combine("bingmaps:", query);
            var success = await Windows.System.Launcher.LaunchUriAsync(new Uri(path));
            if (!success)
            {
                MessageBox.Show("The map uri launcher has failed");
            }
        }

        /// <summary>
        /// Initiates the scenario share and pops up the standard share UI
        /// </summary>
        public static void InitiateShare()
        {
            Share.RegisterForSharing(GetMainWindowHandle());
            ShareDataTransferManager.ShowShareUIForWindow(GetMainWindowHandle());
        }

        /// <summary>
        /// Saves an image to the share file in Png format  
        /// </summary>
        public static async void SetShareFileAsync()
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            // Creates or replaces ShareImage.png in the ApplicationData's local folder
            StorageFile sharefile = await localFolder.CreateFileAsync(Share.imageFileName, CreationCollisionOption.ReplaceExisting);
            shareImage.Save(sharefile.Path, ImageFormat.Png);
        }

        /// <summary>
        /// Searches for an optional package in the main package dependencies
        /// </summary>
        /// <returns>The HrData folder shared from the optional package or null if there is no optional package</returns>
        public static async Task<StorageFolder> LoadDataFromOptionalPackageAsync()
        {
            foreach (var package in Windows.ApplicationModel.Package.Current.Dependencies)
            {
                if (package.IsOptional)
                {
                    return await LoadHrData(package);
                }
            }
            MessageBox.Show("Please install the optional package. (Refer to the readme for further instructions)");
            return null;
        }

        /// <summary>
        /// Retrieves the HrData folder from the optional package
        /// </summary>
        /// <param name="package">The optional package that contains the HrData folder</param>
        /// <returns>The HrData folder shared from the optional package</returns>
        public static async Task<StorageFolder> LoadHrData(Windows.ApplicationModel.Package package)
        {
            StorageFolder appInstalledFolder = package.InstalledLocation;
            return await appInstalledFolder.GetFolderAsync("HrData");
        }
    }
}
