using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Required for BackgroundTaskBuilder APIs.
using Windows.ApplicationModel.Background;

// Required for COM registration API (RegisterTypeForComClients).
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;

namespace MyEmployeesUpdater.ComUpdater
{
    public class BackgroundUpdateRegister
    {

        /// <summary>
        /// Register a background task with the specified taskEntryPoint, name, and trigger
        /// </summary>
        /// <param name="name">A name for the background task.</param>
        /// <param name="trigger">The trigger for the background task.</param>
        public static void RegisterBackgroundTaskWithSystem(string taskName, IBackgroundTrigger trigger)
        {
            // If the task is already active, the function will be returned here rather than registering another instance of it
            foreach (var regIterator in BackgroundTaskRegistration.AllTasks)
            {
                if (regIterator.Value.Name == taskName)
                {
                    return;
                }
            }

            // Build an instance of the task with taskEntrypoint, name, and trigger
            BackgroundTaskBuilder builder = new BackgroundTaskBuilder();
            builder.SetTrigger(trigger);
            builder.Name = taskName;
            builder.SetTaskEntryPointClsid(typeof(ComBackgroundUpdate).GUID);

            // Register the task if it has not been registered
            BackgroundTaskRegistration registration;
            //try
            //{
            registration = builder.Register();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //    registration = null;
            //}
            RegisterProcessForBackgroundTask(typeof(ComBackgroundUpdate));
        }

        /// <summary>
        /// This method register this process as the COM server for the specified
        /// background task class until this process exits or is terminated.
        ///
        /// The process that is responsible for handling a particular background
        /// task must call RegisterTypeForComClients on the IBackgroundTask
        /// derived class. So long as this process is registered with the
        /// aforementioned API, it will be the process that has instances of the
        /// background task invoked.
        /// </summary>
        public static void RegisterProcessForBackgroundTask(Type backgroundTaskClass)
        {
            RegistrationServices registrationServices = new RegistrationServices();
            registrationServices.RegisterTypeForComClients(backgroundTaskClass,
                                                           RegistrationClassContext.LocalServer,
                                                           RegistrationConnectionType.MultipleUse);
        }

    }
}
