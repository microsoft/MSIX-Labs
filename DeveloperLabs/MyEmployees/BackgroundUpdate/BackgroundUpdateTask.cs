using System;
using System.IO;
using System.Net;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Background;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using System.Threading.Tasks;

namespace BackgroundUpdate
{
    public sealed class BackgroundUpdateTask : IBackgroundTask
    {
        string inputPackageVersionData = "";

        /// <summary>
        /// Performs the work of a background task. The system calls this method when the associated background task has been triggered
        /// </summary>
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            // Stores the result of the background task in the application settings container
            SetUpdateAvailable(CheckforUpdate());
            BackgroundTaskDeferral _deferral = taskInstance.GetDeferral();
            // Stores the new employees records in the ApplicationData's local folder
            await DownloadNewEmployeesRecordsAsync();
            _deferral.Complete();
        }

        /// <summary>
        /// Creates a download operation and initiates the download from a web server
        /// </summary>
        private static async Task DownloadNewEmployeesRecordsAsync()
        {
            Uri source = new Uri("https://contososoftwaremyemp.blob.core.windows.net/$web/EmployeeData.csv");
            var localFolder = ApplicationData.Current.LocalFolder;
            // Creates or replaces DownloadTemp.CSV in the ApplicationData's local folder
            StorageFile file = await localFolder.CreateFileAsync("DownloadTemp.CSV", CreationCollisionOption.ReplaceExisting);
            BackgroundDownloader downloader = new BackgroundDownloader();
            DownloadOperation download = downloader.CreateDownload(source, file);
            await download.StartAsync();
        }

        /// <summary>
        /// Writes the result of CheckforUpdate to the application settings container
        /// </summary>
        /// <param name="isFound">Stores the result of CheckforUpdate</param>
        private void SetUpdateAvailable(bool isFound)
        {
            // Sets the result of the background task in a local setting 'isUpdateAvailable'
            ApplicationData.Current.LocalSettings.Values["isUpdateAvailable"] = isFound;
        }

        /// <summary>
        /// Reads the input package version data from a hosted url and stores the result
        /// </summary>
        /// <remarks>
        /// Code from: https://docs.microsoft.com/en-us/windows/msix/non-store-developer-updates 
        /// </remarks>
        private async void GetVersionDataFromWebServerAsync()
        {
            WebClient client = new WebClient();
            Stream stream = client.OpenRead("https://trial3.azurewebsites.net/HRApp/Version.txt");
            StreamReader reader = new StreamReader(stream);
            inputPackageVersionData = await reader.ReadToEndAsync();
        }

        /// <summary>
        /// Reads the version data from the application settings container
        /// </summary>
        /// <remarks>Version data of the input package must be stored in a local setting, in this case: 'newVersion'</remarks>
        /// <returns>The version data of the new package</returns>
        public static string GetVersionDataFromLocalServer()
        {
            return (string)ApplicationData.Current.LocalSettings.Values["newVersion"];
        }

        /// <summary>
        /// Checks for an update on a server by comparing version data
        /// </summary>
        /// <returns>Whether the update is of a higher version</returns>
        private bool CheckforUpdate()
        {
            // GetVersionDataFromWebServerAsync can be used in place of GetVersionDataFromLocalServer to demonstrate a web Uri can be used instead of a local 'version.txt'
            // GetVersionDataFromWebServerAsync();
            inputPackageVersionData = GetVersionDataFromLocalServer();
            var newVersion = new Version(inputPackageVersionData);
            Package package = Package.Current;
            PackageVersion packageVersion = package.Id.Version;
            var currentVersion = new Version(string.Format("{0}.{1}.{2}.{3}", packageVersion.Major, packageVersion.Minor, packageVersion.Build, packageVersion.Revision));
            if (newVersion.CompareTo(currentVersion) > 0)
            {
                return true;
            }
            return false;
        }
    }
}