﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Required for COM background tasks
using System.Runtime.InteropServices;
using Windows.ApplicationModel.Background;

// Required for Package and PackageManager API's
using Windows.ApplicationModel;
using Windows.Management.Deployment;
using Windows.Foundation;
using System.Threading;

namespace MyEmployeesUpdater.ComUpdater
{
    // COM attributes to determine how the background task can be accessed
    [ComVisible(true)]
    [Guid("095D47F4-030E-4AFF-8963-9CB33D63F682")]
    
    // Implements the background updater task using the IBackgroundTask interface
    public sealed class ComBackgroundUpdate : IBackgroundTask
    {
        private volatile int cleanupTask = 0;
        
        // Hard-coded variables representing the new version number and the URI to the package you would like to update to
        string newVersion = "2.0.0.0";
        string inputPackageUri = "c:\\temp\\MyEmployees.Package_2.0.0.0_Test\\MyEmployees.Package_2.0.0.0_x64.msixbundle";
        
        /// <summary>
        /// This is a required method that performs the work of the background task.
        /// </summary>
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            // Add the cancellation handler.
            taskInstance.Canceled += OnCanceled;

            // Check for Update if and only if there has not been a cancellation
            if (cleanupTask == 0)
            {
               // Check version data to see if a new update exists
                Package package = Package.Current;
                PackageId packageId = package.Id;
                PackageVersion packageVersion = packageId.Version;
                var currentVersion = new Version(string.Format("{0}.{1}.{2}.{3}", packageVersion.Major, packageVersion.Minor, packageVersion.Build, packageVersion.Revision));
                var updVersion = new Version(newVersion);

                // If a new version exists, update the application
                if (updVersion.CompareTo(currentVersion) > 0)
                {
                    // Adds the newer package using the AddPackageAsync() method
                    PackageManager packageManager = new PackageManager();
                    IAsyncOperationWithProgress<DeploymentResult, DeploymentProgress> deploymentOperation = null;
                    Uri packageUri = new Uri(inputPackageUri);

                    try
                    {
                        // Note: AddPackageAsync is inconsistent on certain versions of Windows 10. Consider using AddPackageByUriAsync() instead.
                        deploymentOperation = packageManager.AddPackageAsync(
                            packageUri,
                            null,
                            DeploymentOptions.None);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    // Listen to the event that the deployment is complete
                    ManualResetEvent opCompletedEvent = new ManualResetEvent(false);
                    deploymentOperation.Completed = (depProgress, status) => { opCompletedEvent.Set(); };
                    opCompletedEvent.WaitOne();

                }
            }
            return;
        }

        /// <summary>
        /// Background Task Cancellation Handler
        /// </summary>
        public void OnCanceled(IBackgroundTaskInstance taskInstance, BackgroundTaskCancellationReason cancellationReason)
        {
            // Set the flag to indicate to the main thread that it should stop performing
            // work and exit.
            cleanupTask = 1;

            return;
        }
    }
}
