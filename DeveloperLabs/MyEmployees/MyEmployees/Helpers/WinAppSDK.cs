using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Windows.ApplicationModel.WindowsAppRuntime;

namespace MyEmployees.Helpers
{
    /// <summary>
    /// Provides methods to get the status of the WindowsAppRuntime and attempt to initialize it if required
    /// </summary>
    public class WinAppSDK
    {
        /// <summary>
        /// Attempts to get initialize WindowsAppRuntime by deploying required packages if they are not present
        /// </summary>
        public static void initializeWinAppRuntime()
        {
            if (DeploymentManager.GetStatus().Status != DeploymentStatus.Ok)
            {
                // Initialize does a status check, and if the status is not Ok it will attempt to get
                // the WindowsAppRuntime into a good state by deploying packages. Unlike a simple
                // status check, Initialize can sometimes take several seconds to deploy the packages.
                // These should be run on a separate thread so as not to hang your app while the
                // packages deploy. 
                var initializeTask = Task.Run(() => DeploymentManager.Initialize());
                initializeTask.Wait();
                if (initializeTask.Result.Status == DeploymentStatus.Ok)
                {
                    MessageBox.Show("The WindowsAppRuntime was successfully initialized and is now ready for use!");
                }
                else
                {
                    MessageBox.Show("Result ExtendedError: " + initializeTask.Result.ExtendedError.ToString()); 
                    // The WindowsAppRuntime is in a bad state which Initialize() did not fix.
                    // Do error reporting or gather information for submitting a bug.
                    // Gracefully exit the program or carry on without using the WindowsAppRuntime.
                    MessageBox.Show("Initialize() failed to ensure the WindowsAppRuntime.");
                }
            }
            else
            {
                MessageBox.Show("The WindowsAppRuntime was already in an Ok status, no action taken.");
            }
        }

        /// <summary>
        /// Checks to see if the packages required for the WindowsAppRuntime are present
        /// </summary>
        public static void getWinAppRuntimeStatus()
        {
            // GetStatus() is a fast check to see if all of the packages the WindowsAppRuntime
            // requires and expects are present in in an Ok state.
            DeploymentResult result = DeploymentManager.GetStatus();

            // Check the resulting Status.
            if (result.Status == DeploymentStatus.Ok)
            {
                MessageBox.Show("The WindowsAppRuntime is ready for use!");
            }
            else
            {
                // A not-Ok status means it is not ready for us. The Status will indicate the
                // reason it is not Ok, such as some packages need to be installed.
                MessageBox.Show("The WindowsAppRuntime is not ready for use.");
            }
        }
    }
}
