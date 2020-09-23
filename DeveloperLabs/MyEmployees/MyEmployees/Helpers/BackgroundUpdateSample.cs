using ExportDataLibrary;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.ApplicationModel.Background;
using Windows.Storage;

namespace MyEmployees.Helpers
{
    class BackgroundUpdateSample
    {
        static readonly string upToDate = "MyEmployees is already up-to-date!";
        static readonly string taskName = "AppUpdateBackgroundTask";
        static readonly string taskEntryPoint = "BackgroundUpdate.BackgroundUpdateTask";
        // A system trigger that goes off every 15 minutes as long as the device is plugged in to AC power
        static MaintenanceTrigger trigger = new MaintenanceTrigger(15, true);
        // For testing purposes you can use the trigger below to activate the background task immediately by changing the time zone
        // static SystemTrigger trigger = new SystemTrigger(SystemTriggerType.TimeZoneChange, true);

        /// <summary>
        /// Calls the function RegisterBackgroundTask with the desired task name, and entry point (namespace.classname)
        /// </summary>
        public static async void BackgroundTaskImplementation()
        {
            await RegisterBackgroundTask(taskName, taskEntryPoint);
        }

        /// <summary>
        /// Handles the registration of the background task and sets the trigger 
        /// </summary>
        /// <param name="taskName">Uniquely identifies a background task</param>
        /// <param name="taskEntryPoint">Sets the enrty point for the background task</param>
        /// <remarks>The code must have created a background task with an entry point declared in the manifest, or the registration will throw an exception</remarks>
        private static async Task RegisterBackgroundTask(String taskName, String taskEntryPoint)
        {
            // Finds out whether the background task is already registered, then unregisters it
            foreach (var cur in BackgroundTaskRegistration.AllTasks)
            {
                if (cur.Value.Name == taskName)
                {
                    cur.Value.Unregister(true);
                }
            }
            // Creates an instance of a background task using the BackgroundTaskBuilder
            BackgroundTaskBuilder builder = new BackgroundTaskBuilder();
            builder.Name = taskName;
            builder.SetTrigger(trigger);
            builder.TaskEntryPoint = taskEntryPoint;
            // Requests that the app be permitted to run background tasks
            var status = await BackgroundExecutionManager.RequestAccessAsync();
            if (status != BackgroundAccessStatus.DeniedByUser && status != BackgroundAccessStatus.DeniedBySystemPolicy)
            {
                var task = builder.Register();

                // Handles the completion of the background task
                task.Completed += new BackgroundTaskCompletedEventHandler(OnCompleted);
            }
        }

        /// <summary>
        /// Creates an event handler function to handle completed background task events
        /// </summary>
        private static void OnCompleted(IBackgroundTaskRegistration task, BackgroundTaskCompletedEventArgs args)
        {
            // Reads data from a local setting populated and created by the background task
            bool updateIsFound = (bool)ApplicationData.Current.LocalSettings.Values["isUpdateAvailable"];
            if (updateIsFound)
            {
                // Pops a toast notification when an update is detected
                Toast.ToastNotificationSample.ImplementToastNotification();
            }
            else
            {
                MessageBox.Show(upToDate);
            }
        }
    }
}
