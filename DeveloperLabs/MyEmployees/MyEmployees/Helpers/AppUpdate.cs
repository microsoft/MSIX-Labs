using ExportDataLibrary;
using System;
using System.IO;
using System.Threading;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Management.Deployment;

namespace MyEmployees.Helpers
{
    public class AppUpdate
    {
        /// <summary>
        /// Calls the function AddPackageAsync to store the new bits and register the update
        /// Relaunches application after update is complete 
        /// Handles any errors by returning the status and stores the error text to be displayed to the user
        /// </summary>
        /// <param name="inputPackageUri">The location of the new version 
        /// ex :"file://.../MyEmployees.Package.msixbundle"
        /// </param>
        /// <returns>Status:0 => success && 1 => error</returns>
        public static int UpdateApplication(String inputPackageUri)
        { 
            int returnValue = 0;
            Uri packageUri = new Uri(inputPackageUri);
            PackageManager packageManager = new PackageManager();
            IAsyncOperationWithProgress<DeploymentResult, DeploymentProgress> deploymentOperation = null;
            try
            {
                deploymentOperation = packageManager.AddPackageAsync(
                        packageUri,
                        null,
                        DeploymentOptions.ForceApplicationShutdown);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                // Store the error text for the UI
                AppUpdateMessageBox.errorText = e.Message;
                AppUpdateMessageBox.onFailureSuggestion = " Please create the new package (refer to the readme) ";
                return 1;
            }

            // Register the active instance of an application for restart
            uint res = RelaunchHelper.RegisterApplicationRestart(null, RelaunchHelper.RestartFlags.NONE);

            // Listen to the event that the deployment is complete
            ManualResetEvent opCompletedEvent = new ManualResetEvent(false);
            deploymentOperation.Completed = (depProgress, status) => { opCompletedEvent.Set(); };  
            opCompletedEvent.WaitOne();

            // Check the status of the operation
            if (deploymentOperation.Status == AsyncStatus.Error)
            {
                DeploymentResult deploymentResult = deploymentOperation.GetResults();
                Console.WriteLine("Error code: {0}", deploymentOperation.ErrorCode);
                Console.WriteLine("Error text: {0}", deploymentResult.ErrorText);
                // Store the error text for the UI
                AppUpdateMessageBox.errorText = deploymentResult.ErrorText;
                returnValue = 1;
            }
            else if (deploymentOperation.Status == AsyncStatus.Canceled)
            {
                Console.WriteLine("Installation canceled");
            }
            else if (deploymentOperation.Status == AsyncStatus.Completed)
            {
                Console.WriteLine("Installation succeeded");
            }
            else
            {
                returnValue = 1;
                Console.WriteLine("Installation status unknown");
            }
            return returnValue;
        }

        /// <summary>
        /// Reads a file and returns the data as a string
        /// </summary>
        /// <returns>The version data of the new package</returns>
        public static string GetVersionDataFromServer ()
        {         
            try
            {
                // Can be replaced with a cdn file location ex file://.../version.txt
                return File.ReadAllText(Form1.inputPackageVersionUri);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return "0.0.0.0";
            }
        }

        /// <summary>
        /// Calls the function UpdateApplication 
        /// <remarks>Must have a new version available at the Url path</remarks>
        /// </summary>
        /// <returns>Status: 0 => success && 1 => error</returns>
        public static int UpdateNowHelper()
        {
            // Can be replaced with a cdn file location ex file://.../MyEmployees.Package.msixbundle
            return UpdateApplication(Form1.inputPackageUri);
        }

        /// <summary>
        /// Checks for an update on a server by comparing version data
        /// </summary>
        /// <remarks>Must have a local file version.txt available at the path containing the version formart ex: 4.0.0.5 </remarks>
        /// <returns>Whether the update is of a higher version</returns>
        public static bool CheckforUpdate()
        {
           
            var newVersion = new Version(GetVersionDataFromServer());
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